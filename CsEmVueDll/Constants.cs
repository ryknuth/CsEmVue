namespace CsEmVue
{
    public class Scale
    {
        Scale(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public override string ToString()
        {
            return Value;
        }

        const string SECOND = "1S";
        static public Scale Second => new Scale(SECOND);

        const string MINUTE = "1MIN";
        static public Scale Minute => new Scale(MINUTE);

        const string MINUTES_15 = "15MIN";
        static public Scale Minutes15 => new Scale(MINUTES_15);

        const string HOUR = "1H";
        static public Scale Hour => new Scale(HOUR);

        const string DAY = "1D";
        static public Scale Day => new Scale(DAY);

        const string WEEK = "1W";
        static public Scale Week => new Scale(WEEK);

        const string MONTH = "1MON";
        static public Scale Month => new Scale(MONTH);

        const string YEAR = "1Y";
        static public Scale Year => new Scale(YEAR);

        public override bool Equals(object obj)
        {
            if (obj is Scale)
                return (obj as Scale).Value == Value;
            else if (obj is string)
                return (obj as string) == Value;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }

    public class Unit
    {
        Unit(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public override string ToString()
        {
            return Value;
        }

        public override bool Equals(object obj)
        {
            if (obj is Unit)
                return (obj as Unit).Value == Value;
            else if (obj is string)
                return (obj as string) == Value;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        const string KWH = "KilowattHours";
        static public Unit KilowattHours => new Unit(KWH);

        const string USD = "Dollars";
        public Unit Dollars => new Unit(USD);

        const string AMPHOURS = "AmpHours";
        public Unit AmpHours => new Unit(AMPHOURS);

        const string TREES = "Trees";
        public Unit Trees => new Unit(TREES);

        const string GAS = "GallonsOfGas";
        public Unit GallonsOfGas => new Unit(GAS);

        const string DRIVEN = "MilesDriven";
        public Unit MilesDriven => new Unit(DRIVEN);

        const string CARBON = "Carbon";
        public Unit Carbon => new Unit(CARBON);
    }

    static public class Constants
    {
        public const string API_ROOT = "https://api.emporiaenergy.com";
        public const string API_CUSTOMER = "/customers?email={0}";
        public const string API_CUSTOMER_DEVICES = "/customers/{0}/devices?detailed=true&hierarchy=true";
        public const string API_DEVICES_USAGE = "/AppAPI?apiMethod=getDevicesUsage&deviceGids={0}&instant={1}&scale={2}&energyUnit={3}";
        public const string API_CHART_USAGE = "/AppAPI?apiMethod=getChartUsage&deviceGid={0}&channel={1}&start={2}&end={3}&scale={4}&energyUnit={5}";
        public const string API_DEVICE_PROPERTIES = "/devices/{0}/locationProperties";
        public const string API_OUTLET = "/devices/outlet";
        public const string API_GET_OUTLETS = "/customers/outlets?customerGid={0}";

        public const string CLIENT_ID = "4qte47jbstod8apnfic0bunmrq";
        public const string USER_POOL = "us-east-2_ghlOXVLi1";
    }
}
