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
         var devicesUsage = vue.GetDeviceListUsages(DateTime.UtcNow, Scale.Day, Unit.KilowattHours, devices).Result;
         var endTime = DateTime.UtcNow;
         var startTime = endTime.AddHours(-12);
         var channel = devices.First().Devices.First().Channels.ElementAt(0);
         var chartUsage = vue.GetChartUsage(channel, startTime, endTime, Scale.Minute, Unit.KilowattHours).Result;
      }
   }
}
