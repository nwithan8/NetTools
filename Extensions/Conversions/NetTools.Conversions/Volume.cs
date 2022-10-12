using NetTools.Conversions.Units;

namespace NetTools.Conversions
{
    public static class Volume
    {
        public class Units : Enum
        {
            public static readonly Units Yottaliters = new Units(1, new MetricDistanceUnitInfo("Yottaliters", "Ym", 1e+24));
            public static readonly Units Zettaliters = new Units(2, new MetricDistanceUnitInfo("Zettaliters", "Zm", 1e+21));
            public static readonly Units Exaliters = new Units(3, new MetricDistanceUnitInfo("Exaliters", "Em", 1e+18));
            public static readonly Units Petaliters = new Units(4, new MetricDistanceUnitInfo("Petaliters", "Pm", 1e+15));
            public static readonly Units Teraliters = new Units(5, new MetricDistanceUnitInfo("Teraliters", "Tm", 1e+12));
            public static readonly Units Gigaliters = new Units(6, new MetricDistanceUnitInfo("Gigaliters", "Gm", 1e+09));
            public static readonly Units Kiloliters = new Units(7, new MetricDistanceUnitInfo("Kiloliters", "km", 1e+03));
            public static readonly Units Hectoliters = new Units(8, new MetricDistanceUnitInfo("Hectoliters", "hm", 1e+02));
            public static readonly Units Decaliters = new Units(9, new MetricDistanceUnitInfo("Decaliters", "dam", 1e+01));
            public static readonly Units Liters = new Units(10, new MetricDistanceUnitInfo("Liters", "m", 1e+00));
            public static readonly Units Deciliters = new Units(11, new MetricDistanceUnitInfo("Deciliters", "dm", 1e-01));
            public static readonly Units Centiliters = new Units(12, new MetricDistanceUnitInfo("Centiliters", "cm", 1e-02));
            public static readonly Units Milliliters = new Units(13, new MetricDistanceUnitInfo("Milliliters", "mm", 1e-03));
            public static readonly Units Microliters = new Units(14, new MetricDistanceUnitInfo("Microliters", "um", 1e-06));
            public static readonly Units Nanoliters = new Units(15, new MetricDistanceUnitInfo("Nanoliters", "nm", 1e-09));
            public static readonly Units Picoliters = new Units(16, new MetricDistanceUnitInfo("Picoliters", "pm", 1e-12));

            private Units(int id, IConvertableUnitInfo convertableUnitInfo) : base(id)
            {
                ConvertableUnitInfo = convertableUnitInfo;
            }

            internal IConvertableUnitInfo ConvertableUnitInfo { get; }
        }

        public static double Convert(double value, Units from, Units to)
        {
            var currentUnitInfo = from.ConvertableUnitInfo;
            var finalUnitInfo = to.ConvertableUnitInfo;

            return currentUnitInfo switch
            {
                ImperialDistanceUnitInfo currentImperialUnitInfo when finalUnitInfo is ImperialDistanceUnitInfo finalImperialUnitInfo => ConvertImperialUnits(value, currentImperialUnitInfo, finalImperialUnitInfo),
                ImperialDistanceUnitInfo currentImperialUnitInfo when finalUnitInfo is MetricDistanceUnitInfo finalMetricUnitInfo => ConvertImperialToMetricUnits(value, currentImperialUnitInfo, finalMetricUnitInfo),
                MetricDistanceUnitInfo currentMetricUnitInfo when finalUnitInfo is ImperialDistanceUnitInfo finalImperialUnitInfo => ConvertMetricToImperialUnits(value, currentMetricUnitInfo, finalImperialUnitInfo),
                MetricDistanceUnitInfo currentMetricUnitInfo when finalUnitInfo is MetricDistanceUnitInfo finalMetricUnitInfo => ConvertMetricUnits(value, currentMetricUnitInfo, finalMetricUnitInfo),
                var _ => throw new ArgumentException("Invalid unit type")
            };
        }

        private static double ConvertMetricUnits(double value, MetricDistanceUnitInfo currentConvertableConvertableUnitInfo, MetricDistanceUnitInfo finalConvertableConvertableUnitInfo)
        {
            var toBase = currentConvertableConvertableUnitInfo.ToBase(value); // Convert initial unit to meters
            return finalConvertableConvertableUnitInfo.FromBase(toBase); // Convert meters to final unit
        }

        private static double ConvertImperialUnits(double value, ImperialDistanceUnitInfo currentConvertableConvertableUnitInfo, ImperialDistanceUnitInfo finalConvertableConvertableUnitInfo)
        {
            var toBase = currentConvertableConvertableUnitInfo.ToBase(value); // Convert initial unit to inches
            return finalConvertableConvertableUnitInfo.FromBase(toBase); // Convert inches to final unit
        }

        private static double ConvertMetricToImperialUnits(double value, MetricDistanceUnitInfo currentConvertableConvertableUnitInfo, ImperialDistanceUnitInfo finalConvertableConvertableUnitInfo)
        {
            var toBase = currentConvertableConvertableUnitInfo.ToBase(value); // Convert initial unit to meters
            var toInches = toBase * 39.3701; // Convert meters to inches
            return finalConvertableConvertableUnitInfo.FromBase(toInches); // Convert inches to final unit
        }

        private static double ConvertImperialToMetricUnits(double value, ImperialDistanceUnitInfo currentConvertableConvertableUnitInfo, MetricDistanceUnitInfo finalConvertableConvertableUnitInfo)
        {
            var toBase = currentConvertableConvertableUnitInfo.ToBase(value); // Convert initial unit to inches
            var toMeters = toBase / 39.3701; // Convert inches to meters
            return finalConvertableConvertableUnitInfo.FromBase(toMeters); // Convert meters to final unit
        }

        public static class Common
        {
        }
    }
}
