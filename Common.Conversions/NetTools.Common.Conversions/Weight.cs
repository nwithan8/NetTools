using NetTools.Common.Conversions.Units;

namespace NetTools.Common.Conversions;

public static class Weight
{
    public static double Convert(double value, Units from, Units to)
    {
        var currentUnitInfo = from.ConvertableUnitInfo;
        var finalUnitInfo = to.ConvertableUnitInfo;

        return currentUnitInfo switch
        {
            ImperialWeightUnitInfo currentImperialUnitInfo when finalUnitInfo is ImperialWeightUnitInfo finalImperialUnitInfo => ConvertImperialUnits(value, currentImperialUnitInfo, finalImperialUnitInfo),
            ImperialWeightUnitInfo currentImperialUnitInfo when finalUnitInfo is MetricWeightUnitInfo finalMetricUnitInfo => ConvertImperialToMetricUnits(value, currentImperialUnitInfo, finalMetricUnitInfo),
            MetricWeightUnitInfo currentMetricUnitInfo when finalUnitInfo is ImperialWeightUnitInfo finalImperialUnitInfo => ConvertMetricToImperialUnits(value, currentMetricUnitInfo, finalImperialUnitInfo),
            MetricWeightUnitInfo currentMetricUnitInfo when finalUnitInfo is MetricWeightUnitInfo finalMetricUnitInfo => ConvertMetricUnits(value, currentMetricUnitInfo, finalMetricUnitInfo),
            var _ => throw new ArgumentException("Invalid unit type")
        };
    }

    private static double ConvertImperialToMetricUnits(double value, ImperialWeightUnitInfo currentConvertableConvertableUnitInfo, MetricWeightUnitInfo finalConvertableConvertableUnitInfo)
    {
        var inOunces = currentConvertableConvertableUnitInfo.ToBase(value); // Convert initial unit to ounces
        var inGrams = inOunces / 0.035274; // Convert ounces to grams
        return finalConvertableConvertableUnitInfo.FromBase(inGrams); // Convert grams to final unit
    }

    private static double ConvertImperialUnits(double value, ImperialWeightUnitInfo currentConvertableConvertableUnitInfo, ImperialWeightUnitInfo finalConvertableConvertableUnitInfo)
    {
        var inOunces = currentConvertableConvertableUnitInfo.ToBase(value); // Convert initial unit to ounces
        return finalConvertableConvertableUnitInfo.FromBase(inOunces); // Convert ounces to final unit
    }

    private static double ConvertMetricToImperialUnits(double value, MetricWeightUnitInfo currentConvertableConvertableUnitInfo, ImperialWeightUnitInfo finalConvertableConvertableUnitInfo)
    {
        var inGrams = currentConvertableConvertableUnitInfo.ToBase(value); // Convert initial unit to grams
        var inOunces = inGrams * 0.035274; // Convert grams to ounces
        return finalConvertableConvertableUnitInfo.FromBase(inOunces); // Convert ounces to final unit
    }

    private static double ConvertMetricUnits(double value, MetricWeightUnitInfo currentConvertableConvertableUnitInfo, MetricWeightUnitInfo finalConvertableConvertableUnitInfo)
    {
        var inGrams = currentConvertableConvertableUnitInfo.ToBase(value); // Convert initial unit to grams
        return finalConvertableConvertableUnitInfo.FromBase(inGrams); // Convert grams to final unit
    }

    public class Units : Enum
    {
        public static readonly Units Centigrams = new(12, new MetricWeightUnitInfo("Centigrams", "cg", -2));
        public static readonly Units Decagrams = new(9, new MetricWeightUnitInfo("Decagrams", "dag", 1));
        public static readonly Units Decigrams = new(11, new MetricWeightUnitInfo("Decigrams", "dg", -1));
        public static readonly Units Exagrams = new(3, new MetricWeightUnitInfo("Exagrams", "Eg", 18));
        public static readonly Units Gigagrams = new(6, new MetricWeightUnitInfo("Gigagrams", "Gg", 9));
        public static readonly Units Grams = new(10, new MetricWeightUnitInfo("Grams", "m", 0));
        public static readonly Units Hectograms = new(8, new MetricWeightUnitInfo("Hectograms", "hg", 2));
        public static readonly Units Kilograms = new(7, new MetricWeightUnitInfo("Kilograms", "kg", 3));
        public static readonly Units Micrograms = new(14, new MetricWeightUnitInfo("Micrograms", "ug", -6));
        public static readonly Units Milligrams = new(13, new MetricWeightUnitInfo("Milligrams", "mg", -3));
        public static readonly Units Nanograms = new(15, new MetricWeightUnitInfo("Nanograms", "ng", -9));
        public static readonly Units Ounces = new(17, new ImperialWeightUnitInfo("Ounces", "oz", 1));
        public static readonly Units Petagrams = new(4, new MetricWeightUnitInfo("Petagrams", "Pg", 15));
        public static readonly Units Picograms = new(16, new MetricWeightUnitInfo("Picograms", "pg", -12));
        public static readonly Units Pounds = new(18, new ImperialWeightUnitInfo("Pounds", "lb", 16));
        public static readonly Units Stones = new(19, new ImperialWeightUnitInfo("Stones", "st", 224));
        public static readonly Units Teragrams = new(5, new MetricWeightUnitInfo("Teragrams", "Tg", 12));
        public static readonly Units Tons = new(20, new ImperialWeightUnitInfo("Tons", "t", 32000));
        public static readonly Units Yottagrams = new(1, new MetricWeightUnitInfo("Yottagrams", "Yg", 24));
        public static readonly Units Zettagrams = new(2, new MetricWeightUnitInfo("Zettagrams", "Zg", 21));

        internal IConvertableUnitInfo ConvertableUnitInfo { get; }

        private Units(int id, IWeightUnitInfo convertableUnitInfo) : base(id)
        {
            ConvertableUnitInfo = convertableUnitInfo;
        }
    }

    public static class Common
    {
        public static class Pounds
        {
            public static double ToGrams(double value)
            {
                return Convert(value, Units.Pounds, Units.Grams);
            }

            public static double ToKilograms(double value)
            {
                return Convert(value, Units.Pounds, Units.Kilograms);
            }

            public static double ToOunces(double value)
            {
                return Convert(value, Units.Pounds, Units.Ounces);
            }

            public static double ToTons(double value)
            {
                return Convert(value, Units.Pounds, Units.Tons);
            }
        }

        public static class Ounces
        {
            public static double ToGrams(double value)
            {
                return Convert(value, Units.Ounces, Units.Grams);
            }

            public static double ToKilograms(double value)
            {
                return Convert(value, Units.Ounces, Units.Kilograms);
            }

            public static double ToPounds(double value)
            {
                return Convert(value, Units.Ounces, Units.Pounds);
            }

            public static double ToTons(double value)
            {
                return Convert(value, Units.Ounces, Units.Tons);
            }
        }

        public static class Tons
        {
            public static double ToGrams(double value)
            {
                return Convert(value, Units.Tons, Units.Grams);
            }

            public static double ToKilograms(double value)
            {
                return Convert(value, Units.Tons, Units.Kilograms);
            }

            public static double ToOunces(double value)
            {
                return Convert(value, Units.Tons, Units.Ounces);
            }

            public static double ToPounds(double value)
            {
                return Convert(value, Units.Tons, Units.Pounds);
            }
        }

        public static class Grams
        {
            public static double ToKilograms(double value)
            {
                return Convert(value, Units.Grams, Units.Kilograms);
            }

            public static double ToOunces(double value)
            {
                return Convert(value, Units.Grams, Units.Ounces);
            }

            public static double ToPounds(double value)
            {
                return Convert(value, Units.Grams, Units.Pounds);
            }

            public static double ToTons(double value)
            {
                return Convert(value, Units.Grams, Units.Tons);
            }
        }

        public static class Kilograms
        {
            public static double ToGrams(double value)
            {
                return Convert(value, Units.Kilograms, Units.Grams);
            }

            public static double ToOunces(double value)
            {
                return Convert(value, Units.Kilograms, Units.Ounces);
            }

            public static double ToPounds(double value)
            {
                return Convert(value, Units.Kilograms, Units.Pounds);
            }

            public static double ToTons(double value)
            {
                return Convert(value, Units.Kilograms, Units.Tons);
            }
        }
    }
}
