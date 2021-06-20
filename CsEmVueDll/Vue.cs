using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.Runtime;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CsEmVue
{
    public class Vue
    {
        Customer m_customer = null;
        LoginInfo m_loginInfo = null;
        string m_tokenStorage = null;

        public async Task Login(string tokenStorage, Func<(string username, string password)> getUsernamePasswordCallback)
        {
            m_tokenStorage = tokenStorage;

            if (File.Exists(tokenStorage))
                m_loginInfo = JsonConvert.DeserializeObject<LoginInfo>(File.ReadAllText(m_tokenStorage));

            try
            {
                if (m_loginInfo != null)
                    await EnsureValidTokens();
            }
            catch (NotAuthorizedException)
            {
                m_loginInfo = null;
            }

            if (m_loginInfo == null)
                await LoginWithPassword(getUsernamePasswordCallback);
        }

        async Task LoginWithPassword(Func<(string username, string password)> getUsernamePasswordCallback)
        {
            var (userName, password) = getUsernamePasswordCallback();

            using var provider = new AmazonCognitoIdentityProviderClient(new AnonymousAWSCredentials(), RegionEndpoint.USEast2);
            var userPool = new CognitoUserPool(Constants.USER_POOL, Constants.CLIENT_ID, provider);
            var user = new CognitoUser(userName, Constants.CLIENT_ID, userPool, provider);
            var authRequest = new InitiateSrpAuthRequest() { Password = password };

            var authResponse = await user.StartWithSrpAuthAsync(authRequest);

            var result = authResponse.AuthenticationResult;
            m_loginInfo = new LoginInfo
            {
                UserName = userName,
                Access = result.AccessToken,
                Id = result.IdToken,
                Refresh = result.RefreshToken,
                Issued = DateTime.UtcNow,
                Expiration = DateTime.UtcNow.AddSeconds(result.ExpiresIn),
            };

            StoreLoginInfo(m_tokenStorage, m_loginInfo);
        }

        public async Task<Customer> GetCustomer()
        {
            await EnsureValidTokens();

            var url = string.Format(Constants.API_ROOT + Constants.API_CUSTOMER, m_loginInfo.UserName);
            var response = await SendGetRequest<Customer>(url);
            m_customer = response;
            return m_customer;
        }

        public async Task<IEnumerable<Device>> GetDevices()
        {
            await EnsureCustomer();

            var url = string.Format(Constants.API_ROOT + Constants.API_CUSTOMER_DEVICES, m_customer.CustomerGid);
            var response = await SendGetRequest<GetDevicesResponse>(url);
            return response.Devices;
        }

        public async Task<IEnumerable<ChannelUsage>> GetDevicesUsage(DateTime time, Scale scale, Unit unit, IEnumerable<Device> devices)
        {
            var gids = string.Join('+', devices.Select(d => d.DeviceGid));
            var url = string.Format(Constants.API_ROOT + Constants.API_DEVICES_USAGE, gids, FormatDateTime(time), scale, unit);
            var response = await SendGetRequest<GetDevicesUsageResponse>(url);
            return response.ChannelUsages;
        }

        public async Task GetDeviceLocationProperties(Device device)
        {
            var url = string.Format(Constants.API_ROOT + Constants.API_DEVICE_PROPERTIES, device.DeviceGid);
            var response = await SendGetRequest<LocationProperties>(url);
            device.LocationProperties = response;
        }

        public async Task<Usage> GetChartUsage(Channel channel, DateTime start, DateTime end, Scale scale, Unit unit)
        {
            if (channel.ChannelNum == "MainsFromGrid" || channel.ChannelNum == "MainsToGrid")
                return new Usage() { Start = start, Usages = new List<double>() };

            var url = string.Format(Constants.API_ROOT + Constants.API_CHART_USAGE, channel.DeviceGid, channel.ChannelNum, FormatDateTime(start), FormatDateTime(end), scale, unit);
            var response = await SendGetRequest<Usage>(url);

            return response;
        }

        /// <summary>
        /// Untested
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Outlet>> GetOutlets()
        {
            await EnsureCustomer ();

            var url = string.Format(Constants.API_ROOT + Constants.API_GET_OUTLETS, m_customer.CustomerGid);
            var response = await SendGetRequest<IEnumerable<Outlet>>(url);
            return response;
        }

        /// <summary>
        /// Untested
        /// </summary>
        /// <param name="outlet"></param>
        /// <param name="on"></param>
        /// <returns></returns>
        public async Task<Outlet> UpdateOutlet(Outlet outlet, bool on)
        {
            outlet.OutletOn = on;
            var json = JsonConvert.SerializeObject(outlet);

            var url = Constants.API_ROOT + Constants.API_OUTLET;
            var response = await SendPutRequest<Outlet>(url, json);
            return response;
        }

        async Task<T> SendGetRequest<T>(string url)
        {
            var request = await CreateRequest(url);
            var response = await request.GetResponseAsync();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var responseObject = JsonConvert.DeserializeObject<T>(responseString);
            return responseObject;
        }

        async Task<T> SendPutRequest<T>(string url, string body)
        {
            var request = await CreateRequest(url);
            var requestStream = await request.GetRequestStreamAsync();
            {
                using var writer = new StreamWriter(requestStream);
                writer.Write(body);
            }
            var response = await request.GetResponseAsync();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var responseObject = JsonConvert.DeserializeObject<T>(responseString);
            return responseObject;
        }

        async Task<WebRequest> CreateRequest(string url)
        {
            await EnsureValidTokens();

            var request = WebRequest.Create(url);
            request.Headers.Add("authtoken", m_loginInfo.Id);
            return request;
        }

        async Task EnsureValidTokens()
        {
            if (m_loginInfo == null)
                throw new InvalidOperationException("Must login first");

            if (m_loginInfo.Expiration > DateTime.UtcNow.AddMinutes(1))
                return;

            using var provider = new AmazonCognitoIdentityProviderClient(new AnonymousAWSCredentials(), RegionEndpoint.USEast2);
            var userPool = new CognitoUserPool(Constants.USER_POOL, Constants.CLIENT_ID, provider);

            var user = new CognitoUser(m_loginInfo.UserName, Constants.CLIENT_ID, userPool, provider)
            {
                SessionTokens = new CognitoUserSession(m_loginInfo.Id, m_loginInfo.Access, m_loginInfo.Refresh, m_loginInfo.Issued, m_loginInfo.Expiration)
            };
            var refreshTokenRequest = new InitiateRefreshTokenAuthRequest();
            refreshTokenRequest.AuthFlowType = AuthFlowType.REFRESH_TOKEN_AUTH;
            var authResponse = await user.StartWithRefreshTokenAuthAsync(refreshTokenRequest);

            var result = authResponse.AuthenticationResult;
            m_loginInfo = new LoginInfo
            {
                UserName = m_loginInfo.UserName,
                Access = result.AccessToken,
                Id = result.IdToken,
                Refresh = result.RefreshToken,
                Issued = DateTime.UtcNow,
                Expiration = DateTime.UtcNow.AddSeconds(result.ExpiresIn),
            };

            StoreLoginInfo(m_tokenStorage, m_loginInfo);
        }

        async Task EnsureCustomer()
        {
            if (m_customer != null)
                return;

            await EnsureValidTokens();
            await GetCustomer();
        }

        static string FormatDateTime(DateTime time)
        {
            return time.ToString("o");
        }

        static void StoreLoginInfo(string path, LoginInfo loginInfo)
        {
            if (string.IsNullOrWhiteSpace(path))
                return;

            if (!File.Exists(path))
                File.Create(path);

            var json = JsonConvert.SerializeObject(loginInfo);
            File.WriteAllText(path, json);
        }

        static public (string username, string password) GetUsernameAndPasswordWithConsole()
        {
            Console.Write("Enter email: ");
            var username = Console.ReadLine();

            Console.Write("Enter password: ");
            var passwordBuilder = new System.Text.StringBuilder();
            while (true)
            {
                var passwordChar = Console.ReadKey(true).KeyChar;
                var newLine = '\r';

                if (passwordChar == newLine)
                {
                    break;
                }
                else
                {
                    passwordBuilder.Append(passwordChar.ToString());
                }
            }

            Console.WriteLine();

            return (username.Trim(), passwordBuilder.ToString().Trim());
        }
    }
}
