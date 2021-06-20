using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CsEmVue
{
    [DataContract]
    public class Customer
    {
        [DataMember(Name = "customerGid")]
        public string CustomerGid { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        [DataMember(Name = "createdAt")]
        public DateTime CreatedAt { get; set; }
    }

    [DataContract]
    public class Device
    {
        [DataMember(Name = "deviceGid")]
        public int DeviceGid { get; set; }

        [DataMember(Name = "manufacturerDeviceId")]
        public string ManufacturerId { get; set; }

        [DataMember(Name = "model")]
        public string Model { get; set; }

        [DataMember(Name = "firmware")]
        public string Firmware { get; set; }

        [DataMember(Name = "channels")]
        public Channel[] Channels { get; set; }

        [DataMember(Name = "outlet")]
        public Outlet Outlet { get; set; }

        [DataMember(Name = "locationProperties")]
        public LocationProperties LocationProperties { get; set; }

        [DataMember(Name = "devices")]
        public IEnumerable<Device> Devices { get; set; }
    }

    [DataContract]
    public class LocationProperties
    {
        [DataMember(Name = "deviceName")]
        public string DeviceName { get; set; }

        [DataMember(Name = "zipCode")]
        public string ZipCode { get; set; }

        [DataMember(Name = "timeZone")]
        public string TimeZone { get; set; }

        [DataMember(Name = "usageCentPerKwHour")]
        public double UsageCentPerKwHour { get; set; }

        [DataMember(Name = "peakDemandDollarPerKw")]
        public double PeakDemandDollarPerKw { get; set; }

        [DataMember(Name = "billingCycleStartDay")]
        public int BillingCycleStartDay { get; set; }

        [DataMember(Name = "solar")]
        public bool Solar { get; set; }

        [DataMember(Name = "locationInformation")]
        public LocationInfo LocationInfo { get; set; }
    }

    [DataContract]
    public class LocationInfo
    {
        [DataMember(Name = "airConditioning")]
        public string AirConditioning { get; set; }

        [DataMember(Name = "heatSource")]
        public string HeatSource { get; set; }

        [DataMember(Name = "locationSqFt")]
        public string LocationSqft { get; set; }

        [DataMember(Name = "numElectricCars")]
        public string NumberElectricCars { get; set; }

        [DataMember(Name = "locationType")]
        public string LocationType { get; set; }

        [DataMember(Name = "numPeople")]
        public string NumberPeople { get; set; }

        [DataMember(Name = "swimmingPool")]
        public string SwimmingPool { get; set; }

        [DataMember(Name = "hotTub")]
        public string HotTub { get; set; }
    }

    [DataContract]
    public class Channel
    {
        [DataMember(Name = "deviceGid")]
        public int DeviceGid { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "channelNum")]
        public string ChannelNum { get; set; }

        [DataMember(Name = "channelMultiplier")]
        public double ChannelMultiplier { get; set; }

        [DataMember(Name = "channelTypeGid")]
        public int? ChannelTypeGid { get; set; }
    }

    [DataContract]
    public class ChannelUsage
    {
        [DataMember(Name = "deviceGid")]
        public int DeviceGid { get; set; }

        [DataMember(Name = "channelNum")]
        public string ChannelNum { get; set; }

        [DataMember(Name = "usage")]
        public ChartUsage Usage { get; set; }
    }

    [DataContract]
    public class ChartUsage
    {
        [DataMember(Name = "Value")]
        public double ValueKWH { get; set; }

        [DataMember(Name = "Timestamp")]
        public Timestamp Timestamp { get; set; }

        public DateTime UtcTime
        {
            get
            {
                if (Timestamp.Nano != 0)
                    return DateTime.UnixEpoch.AddTicks(Timestamp.Nano / 100);
                else if (Timestamp.EpochSecond != 0)
                    return DateTimeOffset.FromUnixTimeSeconds(Timestamp.EpochSecond).DateTime;
                else
                    return DateTime.MinValue;
            }
        }

        public DateTime LocalTime => UtcTime.ToLocalTime();
    }

    [DataContract]
    public class Timestamp
    {
        [DataMember(Name = "nano")]
        public Int64 Nano { get; set; }

        [DataMember(Name = "epochSecond")]
        public Int64 EpochSecond { get; set; }
    }

    [DataContract]
    public class Outlet
    {
        [DataMember(Name = "deviceGid")]
        public int DeviceGid { get; set; }

        [DataMember(Name = "outletOn")]
        public bool OutletOn { get; set; }

        [DataMember(Name = "parentDeviceGid")]
        public int ParentDeviceGid { get; set; }

        [DataMember(Name = "parentChannelNum")]
        public bool ParentChannelNum { get; set; }
    }

    [DataContract]
    public class Usage
    {
        [DataMember(Name = "firstUsageInstant")]
        public DateTime Start { get; set; }

        [DataMember(Name = "usageList")]
        public IEnumerable<double> Usages { get; set; }
    }

    [DataContract]
    public class LoginInfo
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Access { get; set; }

        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Refresh { get; set; }

        [DataMember]
        public DateTime Issued { get; set; }

        [DataMember]
        public DateTime Expiration { get; set; }
    }
}
