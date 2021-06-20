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
    public class GetDevicesUsageResponse
    {
        [DataMember(Name = "channelUsages")]
        public ChannelUsage[] ChannelUsages { get; set; }
    }
}
