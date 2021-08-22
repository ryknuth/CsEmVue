using System;
using System.Runtime.Serialization;

namespace CsEmVue
{
    [DataContract]
    public class GetDevicesResponse : Customer
    {
        [DataMember(Name = "devices")]
        public Device[] Devices { get; set; }
    }

    [DataContract]
    public class GetDeviceListUsagesResponse
    {
        [DataMember(Name = "deviceListUsages")]
        public DeviceListUsages DeviceListUsages { get; set; }
    }
}
