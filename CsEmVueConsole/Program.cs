using CsEmVue;
using System;
using System.Linq;

namespace CsEmVueConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var tokenFile = "tokens.txt";
            var vue = new Vue();
            vue.Login(tokenFile, Vue.GetUsernameAndPasswordWithConsole).Wait();
            var devices = vue.GetDevices().Result;
            var devicesUsage = vue.GetDevicesUsage(DateTime.UtcNow, Scale.Day, Unit.KilowattHours, devices).Result;
            var chartUsage = vue.GetChartUsage(devices.First().Devices.First().Channels.ElementAt(1), DateTime.UtcNow.AddDays(-1), DateTime.UtcNow, Scale.Minute, Unit.KilowattHours).Result;
        }
    }
}
