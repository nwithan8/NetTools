using NetTools.Common.Conversions.Units;

namespace NetTools.Common.Conversions;

public static class Volume
{
    public static double Convert(double value, Units from, Units to)
    {
        var currentUnitInfo = from.ConvertableUnitInfo;
        var finalUnitInfo = to.ConvertableUnitInfo;

        return currentUnitInfo switch
        {
            ImperialVolumeUnitInfo currentImperialUnitInfo when finalUnitInfo is ImperialVolumeUnitInfo finalImperialUnitInfo => ConvertImperialUnits(value, currentImperialUnitInfo, finalImperialUnitInfo),
            ImperialVolumeUnitInfo currentImperialUnitInfo when finalUnitInfo is MetricVolumeUnitInfo finalMetricUnitInfo => ConvertImperialToMetricUnits(value, currentImperialUnitInfo, finalMetricUnitInfo),
            MetricVolumeUnitInfo currentMetricUnitInfo when finalUnitInfo is ImperialVolumeUnitInfo finalImperialUnitInfo => ConvertMetricToImperialUnits(value, currentMetricUnitInfo, finalImperialUnitInfo),
            MetricVolumeUnitInfo currentMetricUnitInfo when finalUnitInfo is MetricVolumeUnitInfo finalMetricUnitInfo => ConvertMetricUnits(value, currentMetricUnitInfo, finalMetricUnitInfo),
            var _ => throw new ArgumentException("Invalid unit type")
        };
    }

    private static double ConvertImperialToMetricUnits(double value, ImperialVolumeUnitInfo currentConvertableConvertableUnitInfo, MetricVolumeUnitInfo finalConvertableConvertableUnitInfo)
    {
        var inTeaspoons = currentConvertableConvertableUnitInfo.ToBase(value); // Convert initial unit to teaspoons
        var inLiters = inTeaspoons / 202.884; // Convert teaspoons to liters
        return finalConvertableConvertableUnitInfo.FromBase(inLiters); // Convert liters to final unit
    }

    private static double ConvertImperialUnits(double value, ImperialVolumeUnitInfo currentConvertableConvertableUnitInfo, ImperialVolumeUnitInfo finalConvertableConvertableUnitInfo)
    {
        var inTeaspoons = currentConvertableConvertableUnitInfo.ToBase(value); // Convert initial unit to teaspoons
        return finalConvertableConvertableUnitInfo.FromBase(inTeaspoons); // Convert teaspoons to final unit
    }

    private static double ConvertMetricToImperialUnits(double value, MetricVolumeUnitInfo currentConvertableConvertableUnitInfo, ImperialVolumeUnitInfo finalConvertableConvertableUnitInfo)
    {
        var inLiters = currentConvertableConvertableUnitInfo.ToBase(value); // Convert initial unit to liters
        var inTeaspoons = inLiters * 202.884; // Convert liters to teaspoons
        return finalConvertableConvertableUnitInfo.FromBase(inTeaspoons); // Convert teaspoons to final unit
    }

    private static double ConvertMetricUnits(double value, MetricVolumeUnitInfo currentConvertableConvertableUnitInfo, MetricVolumeUnitInfo finalConvertableConvertableUnitInfo)
    {
        var inLiters = currentConvertableConvertableUnitInfo.ToBase(value); // Convert initial unit to liters
        return finalConvertableConvertableUnitInfo.FromBase(inLiters); // Convert liters to final unit
    }

    public class Units : Enum
    {
        public static readonly Units Centiliters = new(12, new MetricVolumeUnitInfo("Centiliters", "cL", -2));
        public static readonly Units Cups = new(20, new ImperialVolumeUnitInfo("Cups", "cup", 48));
        public static readonly Units Decaliters = new(9, new MetricVolumeUnitInfo("Decaliters", "daL", 1));
        public static readonly Units Deciliters = new(11, new MetricVolumeUnitInfo("Deciliters", "dL", -1));
        public static readonly Units Exaliters = new(3, new MetricVolumeUnitInfo("Exaliters", "EL", 18));
        public static readonly Units FluidOunces = new(19, new ImperialVolumeUnitInfo("Fluid Ounces", "fl oz", 6));
        public static readonly Units Gallons = new(23, new ImperialVolumeUnitInfo("Gallons", "gal", 768));
        public static readonly Units Gigaliters = new(6, new MetricVolumeUnitInfo("Gigaliters", "GL", 9));
        public static readonly Units Hectoliters = new(8, new MetricVolumeUnitInfo("Hectoliters", "hL", 2));
        public static readonly Units Kiloliters = new(7, new MetricVolumeUnitInfo("Kiloliters", "kL", 3));
        public static readonly Units Liters = new(10, new MetricVolumeUnitInfo("Liters", "L", 0));
        public static readonly Units Microliters = new(14, new MetricVolumeUnitInfo("Microliters", "uL", -6));
        public static readonly Units Milliliters = new(13, new MetricVolumeUnitInfo("Milliliters", "mL", -3));
        public static readonly Units Nanoliters = new(15, new MetricVolumeUnitInfo("Nanoliters", "nL", -9));
        public static readonly Units Petaliters = new(4, new MetricVolumeUnitInfo("Petaliters", "PL", 15));
        public static readonly Units Picoliters = new(16, new MetricVolumeUnitInfo("Picoliters", "pL", -12));
        public static readonly Units Pints = new(21, new ImperialVolumeUnitInfo("Pints", "pt", 96));
        public static readonly Units Quarts = new(22, new ImperialVolumeUnitInfo("Quarts", "qt", 192));
        public static readonly Units Tablespoons = new(18, new ImperialVolumeUnitInfo("Tablespoons", "tbsp", 3));
        public static readonly Units Teaspoons = new(17, new ImperialVolumeUnitInfo("Teaspoons", "tsp", 1));
        public static readonly Units Teraliters = new(5, new MetricVolumeUnitInfo("Teraliters", "TL", 12));
        public static readonly Units Yottaliters = new(1, new MetricVolumeUnitInfo("Yottaliters", "YL", 24));
        public static readonly Units Zettaliters = new(2, new MetricVolumeUnitInfo("Zettaliters", "ZL", 21));

        internal IConvertableUnitInfo ConvertableUnitInfo { get; }

        private Units(int id, IVolumeUnitInfo convertableUnitInfo) : base(id)
        {
            ConvertableUnitInfo = convertableUnitInfo;
        }
    }

    public static class Common
    {
    }
}
