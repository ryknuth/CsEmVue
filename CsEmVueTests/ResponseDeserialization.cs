using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using CsEmVue;

namespace CsEmVueTests
{
    [TestClass]
    public class ResponseDeserialization
    {
        [TestMethod]
        public void GetCustomerResponse()
        {
            var json = "{\"customerGid\":1,\"email\":\"test@test.com\",\"firstName\":\"Hi\",\"lastName\":\"Beau\",\"createdAt\":\"2020 - 05 - 04T20: 34:42.902Z\"}";
            var customer = JsonConvert.DeserializeObject<Customer>(json);
        }

        [TestMethod]
        public void GetDevicesResponse()
        {
            var json = "{\"customerGid\":1,\"email\":\"test@test.com\",\"firstName\":\"Hi\",\"lastName\":\"Beau\",\"createdAt\":\"2020 - 05 - 04T20: 34:42.902Z\",\"devices\":[{\"deviceGid\":10353,\"manufacturerDeviceId\":\"00013c71bf04fc28\",\"model\":\"VUE001\",\"firmware\":\"Vue - 1587661391\",\"parentDeviceGid\":null,\"parentChannelNum\":null,\"locationProperties\":{\"deviceGid\":10353,\"deviceName\":\"House\",\"zipCode\":\"99208\",\"timeZone\":\"America / Los_Angeles\",\"billingCycleStartDay\":1,\"usageCentPerKwHour\":0.07,\"peakDemandDollarPerKw\":0.0,\"locationInformation\":{\"airConditioning\":\"true\",\"heatSource\":\"naturalGasFurnace\",\"locationSqFt\":\"5500\",\"numElectricCars\":\"0\",\"locationType\":\"houseMultiLevel\",\"numPeople\":\"7\"},\"latitudeLongitude\":null,\"utilityRateGid\":null},\"outlet\":null,\"evCharger\":null,\"devices\":[{\"deviceGid\":10353,\"manufacturerDeviceId\":\"SX00013c71bf04fc28\",\"model\":\"WAT001\",\"firmware\":null,\"channels\":[{\"deviceGid\":10353,\"name\":\"AC Main\",\"channelNum\":\"1\",\"channelMultiplier\":2.0,\"channelTypeGid\":1},{\"deviceGid\":10353,\"name\":\"Refrigerator \",\"channelNum\":\"2\",\"channelMultiplier\":1.0,\"channelTypeGid\":10},{\"deviceGid\":10353,\"name\":\"Dryer\",\"channelNum\":\"3\",\"channelMultiplier\":2.0,\"channelTypeGid\":4},{\"deviceGid\":10353,\"name\":\"Oven\",\"channelNum\":\"4\",\"channelMultiplier\":2.0,\"channelTypeGid\":7},{\"deviceGid\":10353,\"name\":\"Freezer\",\"channelNum\":\"5\",\"channelMultiplier\":1.0,\"channelTypeGid\":10},{\"deviceGid\":10353,\"name\":\"AC Up\",\"channelNum\":\"6\",\"channelMultiplier\":1.0,\"channelTypeGid\":1},{\"deviceGid\":10353,\"name\":\"Furnace Up\",\"channelNum\":\"7\",\"channelMultiplier\":1.0,\"channelTypeGid\":11},{\"deviceGid\":10353,\"name\":\"Office\",\"channelNum\":\"8\",\"channelMultiplier\":1.0,\"channelTypeGid\":6}]}],\"channels\":[{\"deviceGid\":10353,\"name\":null,\"channelNum\":\"1,2,3\",\"channelMultiplier\":1.0,\"channelTypeGid\":null}]},{\"deviceGid\":10366,\"manufacturerDeviceId\":\"00013c71bf04fc20\",\"model\":\"VUE001\",\"firmware\":\"Vue - 1587661391\",\"parentDeviceGid\":null,\"parentChannelNum\":null,\"locationProperties\":{\"deviceGid\":10366,\"deviceName\":\"Shop\",\"zipCode\":null,\"timeZone\":\"America / Los_Angeles\",\"billingCycleStartDay\":1,\"usageCentPerKwHour\":13.2,\"peakDemandDollarPerKw\":0.0,\"locationInformation\":null,\"latitudeLongitude\":null,\"utilityRateGid\":null},\"outlet\":null,\"evCharger\":null,\"devices\":[{\"deviceGid\":10366,\"manufacturerDeviceId\":\"SX00013c71bf04fc20\",\"model\":\"WAT001\",\"firmware\":null,\"channels\":[{\"deviceGid\":10366,\"name\":\"Bath Heater\",\"channelNum\":\"1\",\"channelMultiplier\":2.0,\"channelTypeGid\":9},{\"deviceGid\":10366,\"name\":\"Lights\",\"channelNum\":\"2\",\"channelMultiplier\":1.0,\"channelTypeGid\":12},{\"deviceGid\":10366,\"name\":\"Refrigerator\",\"channelNum\":\"3\",\"channelMultiplier\":1.0,\"channelTypeGid\":10},{\"deviceGid\":10366,\"name\":\"Lean - to RV\",\"channelNum\":\"4\",\"channelMultiplier\":1.0,\"channelTypeGid\":9},{\"deviceGid\":10366,\"name\":\"Well Pump\",\"channelNum\":\"5\",\"channelMultiplier\":2.0,\"channelTypeGid\":19},{\"deviceGid\":10366,\"name\":\"Reservoir Pump\",\"channelNum\":\"6\",\"channelMultiplier\":1.0,\"channelTypeGid\":19},{\"deviceGid\":10366,\"name\":\"Water Heater\",\"channelNum\":\"7\",\"channelMultiplier\":1.0,\"channelTypeGid\":22},{\"deviceGid\":10366,\"name\":\"Main Heater\",\"channelNum\":\"8\",\"channelMultiplier\":2.0,\"channelTypeGid\":11}]}],\"channels\":[{\"deviceGid\":10366,\"name\":null,\"channelNum\":\"1,2,3\",\"channelMultiplier\":1.0,\"channelTypeGid\":null}]}]}";
            var devices = JsonConvert.DeserializeObject<GetDevicesResponse>(json);
        }

        [TestMethod]
        public void GetDevicesUsageResponse()
        {
            var json = "{\"channelUsages\":[{\"deviceGid\":10353,\"channelNum\":\"1,2,3\",\"usage\":{\"Timestamp\":{\"nano\":0,\"epochSecond\":1623913200},\"Value\":31.863154451887343,\"value\":31.863154451887343}},{\"deviceGid\":10353,\"channelNum\":\"1\",\"usage\":{\"Timestamp\":{\"nano\":0,\"epochSecond\":1623913200},\"Value\":4.775967052665816,\"value\":4.775967052665816}},{\"deviceGid\":10353,\"channelNum\":\"2\",\"usage\":{\"Timestamp\":{\"nano\":0,\"epochSecond\":1623913200},\"Value\":2.427270969308747,\"value\":2.427270969308747}},{\"deviceGid\":10353,\"channelNum\":\"3\",\"usage\":{\"Timestamp\":{\"nano\":0,\"epochSecond\":1623913200},\"Value\":0.0,\"value\":0.0}},{\"deviceGid\":10353,\"channelNum\":\"4\",\"usage\":{\"Timestamp\":{\"nano\":0,\"epochSecond\":1623913200},\"Value\":1.3534664807067978,\"value\":1.3534664807067978}},{\"deviceGid\":10353,\"channelNum\":\"5\",\"usage\":{\"Timestamp\":{\"nano\":0,\"epochSecond\":1623913200},\"Value\":3.1771748607097727,\"value\":3.1771748607097727}},{\"deviceGid\":10353,\"channelNum\":\"6\",\"usage\":{\"Timestamp\":{\"nano\":0,\"epochSecond\":1623913200},\"Value\":0.0,\"value\":0.0}},{\"deviceGid\":10353,\"channelNum\":\"7\",\"usage\":{\"Timestamp\":{\"nano\":0,\"epochSecond\":1623913200},\"Value\":0.0,\"value\":0.0}},{\"deviceGid\":10353,\"channelNum\":\"8\",\"usage\":{\"Timestamp\":{\"nano\":0,\"epochSecond\":1623913200},\"Value\":4.610175760895412,\"value\":4.610175760895412}},{\"deviceGid\":10366,\"channelNum\":\"1,2,3\",\"usage\":{\"Timestamp\":{\"nano\":0,\"epochSecond\":1623913200},\"Value\":-45.571534151539936,\"value\":-45.571534151539936}},{\"deviceGid\":10366,\"channelNum\":\"MainsFromGrid\",\"usage\":{\"Timestamp\":{\"nano\":0,\"epochSecond\":1623913200},\"Value\":3.9233516733647718,\"value\":3.9233516733647718}},{\"deviceGid\":10366,\"channelNum\":\"MainsToGrid\",\"usage\":{\"Timestamp\":{\"nano\":0,\"epochSecond\":1623913200},\"Value\":-49.494885824904706,\"value\":-49.494885824904706}},{\"deviceGid\":10366,\"channelNum\":\"1\",\"usage\":{\"Timestamp\":{\"nano\":0,\"epochSecond\":1623913200},\"Value\":0.0,\"value\":0.0}},{\"deviceGid\":10366,\"channelNum\":\"2\",\"usage\":{\"Timestamp\":{\"nano\":0,\"epochSecond\":1623913200},\"Value\":0.8430876767506864,\"value\":0.8430876767506864}},{\"deviceGid\":10366,\"channelNum\":\"3\",\"usage\":{\"Timestamp\":{\"nano\":0,\"epochSecond\":1623913200},\"Value\":0.3919688938197825,\"value\":0.3919688938197825}},{\"deviceGid\":10366,\"channelNum\":\"4\",\"usage\":{\"Timestamp\":{\"nano\":0,\"epochSecond\":1623913200},\"Value\":0.8489747851709526,\"value\":0.8489747851709526}},{\"deviceGid\":10366,\"channelNum\":\"5\",\"usage\":{\"Timestamp\":{\"nano\":0,\"epochSecond\":1623913200},\"Value\":6.8844338570662345,\"value\":6.8844338570662345}},{\"deviceGid\":10366,\"channelNum\":\"6\",\"usage\":{\"Timestamp\":{\"nano\":0,\"epochSecond\":1623913200},\"Value\":1.3820422099502219,\"value\":1.3820422099502219}},{\"deviceGid\":10366,\"channelNum\":\"7\",\"usage\":{\"Timestamp\":{\"nano\":0,\"epochSecond\":1623913200},\"Value\":0.6886588813539346,\"value\":0.6886588813539346}},{\"deviceGid\":10366,\"channelNum\":\"8\",\"usage\":{\"Timestamp\":{\"nano\":0,\"epochSecond\":1623913200},\"Value\":0.0,\"value\":0.0}}]}";
            var devicesUsage = JsonConvert.DeserializeObject<GetDevicesUsageResponse>(json);
        }
    }
}