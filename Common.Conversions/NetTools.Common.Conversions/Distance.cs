using NetTools.Common.Conversions.Units;

namespace NetTools.Common.Conversions;

public static class Distance
{
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

    private static double ConvertImperialToMetricUnits(double value, ImperialDistanceUnitInfo currentConvertableConvertableUnitInfo, MetricDistanceUnitInfo finalConvertableConvertableUnitInfo)
    {
        var inInches = currentConvertableConvertableUnitInfo.ToBase(value); // Convert initial unit to inches
        var inMeters = inInches / 39.3701; // Convert inches to meters
        return finalConvertableConvertableUnitInfo.FromBase(inMeters); // Convert meters to final unit
    }

    private static double ConvertImperialUnits(double value, ImperialDistanceUnitInfo currentConvertableConvertableUnitInfo, ImperialDistanceUnitInfo finalConvertableConvertableUnitInfo)
    {
        var inInches = currentConvertableConvertableUnitInfo.ToBase(value); // Convert initial unit to inches
        return finalConvertableConvertableUnitInfo.FromBase(inInches); // Convert inches to final unit
    }

    private static double ConvertMetricToImperialUnits(double value, MetricDistanceUnitInfo currentConvertableConvertableUnitInfo, ImperialDistanceUnitInfo finalConvertableConvertableUnitInfo)
    {
        var inMeters = currentConvertableConvertableUnitInfo.ToBase(value); // Convert initial unit to meters
        var inInches = inMeters * 39.3701; // Convert meters to inches
        return finalConvertableConvertableUnitInfo.FromBase(inInches); // Convert inches to final unit
    }

    private static double ConvertMetricUnits(double value, MetricDistanceUnitInfo currentConvertableConvertableUnitInfo, MetricDistanceUnitInfo finalConvertableConvertableUnitInfo)
    {
        var inMeters = currentConvertableConvertableUnitInfo.ToBase(value); // Convert initial unit to meters
        return finalConvertableConvertableUnitInfo.FromBase(inMeters); // Convert meters to final unit
    }

    public class Units : Enum
    {
        public static readonly Units Centimeters = new(12, new MetricDistanceUnitInfo("Centimeters", "cm", -2));
        public static readonly Units Decameters = new(9, new MetricDistanceUnitInfo("Decameters", "dam", 1));
        public static readonly Units Decimeters = new(11, new MetricDistanceUnitInfo("Decimeters", "dm", -1));
        public static readonly Units Exameters = new(3, new MetricDistanceUnitInfo("Exameters", "Em", 18));
        public static readonly Units Feet = new(18, new ImperialDistanceUnitInfo("Feet", "ft", 12));
        public static readonly Units Gigameters = new(6, new MetricDistanceUnitInfo("Gigameters", "Gm", 9));
        public static readonly Units Hectometers = new(8, new MetricDistanceUnitInfo("Hectometers", "hm", 2));
        public static readonly Units Inches = new(17, new ImperialDistanceUnitInfo("Inches", "in", 1));
        public static readonly Units Kilometers = new(7, new MetricDistanceUnitInfo("Kilometers", "km", 3));
        public static readonly Units Meters = new(10, new MetricDistanceUnitInfo("Meters", "m", 0));
        public static readonly Units Micrometers = new(14, new MetricDistanceUnitInfo("Micrometers", "um", -6));
        public static readonly Units Miles = new(20, new ImperialDistanceUnitInfo("Miles", "mi", 63360));
        public static readonly Units Millimeters = new(13, new MetricDistanceUnitInfo("Millimeters", "mm", -3));
        public static readonly Units Nanometers = new(15, new MetricDistanceUnitInfo("Nanometers", "nm", -9));
        public static readonly Units Petameters = new(4, new MetricDistanceUnitInfo("Petameters", "Pm", 15));
        public static readonly Units Picometers = new(16, new MetricDistanceUnitInfo("Picometers", "pm", -12));
        public static readonly Units Terameters = new(5, new MetricDistanceUnitInfo("Terameters", "Tm", 12));
        public static readonly Units Yards = new(19, new ImperialDistanceUnitInfo("Yards", "yd", 36));
        public static readonly Units Yottameters = new(1, new MetricDistanceUnitInfo("Yottameters", "Ym", 24));
        public static readonly Units Zettameters = new(2, new MetricDistanceUnitInfo("Zettameters", "Zm", 21));

        internal IConvertableUnitInfo ConvertableUnitInfo { get; }

        private Units(int id, IDistanceUnitInfo convertableUnitInfo) : base(id)
        {
            ConvertableUnitInfo = convertableUnitInfo;
        }
    }

    public static class Common
    {
        public static class Miles
        {
            public static double ToFeet(double miles)
            {
                return Convert(miles, Units.Miles, Units.Feet);
            }

            public static double ToInches(double miles)
            {
                return Convert(miles, Units.Miles, Units.Inches);
            }

            public static double ToKilometers(double miles)
            {
                return Convert(miles, Units.Miles, Units.Kilometers);
            }

            public static double ToMeters(double miles)
            {
                return Convert(miles, Units.Miles, Units.Meters);
            }

            public static double ToYards(double miles)
            {
                return Convert(miles, Units.Miles, Units.Yards);
            }
        }

        public static class Yards
        {
            public static double ToFeet(double yards)
            {
                return Convert(yards, Units.Yards, Units.Feet);
            }

            public static double ToInches(double yards)
            {
                return Convert(yards, Units.Yards, Units.Inches);
            }

            public static double ToKilometers(double yards)
            {
                return Convert(yards, Units.Yards, Units.Kilometers);
            }

            public static double ToMeters(double yards)
            {
                return Convert(yards, Units.Yards, Units.Meters);
            }

            public static double ToMiles(double yards)
            {
                return Convert(yards, Units.Yards, Units.Miles);
            }
        }

        public static class Feet
        {
            public static double ToInches(double feet)
            {
                return Convert(feet, Units.Feet, Units.Inches);
            }


            public static double ToKilometers(double feet)
            {
                return Convert(feet, Units.Feet, Units.Kilometers);
            }


            public static double ToMeters(double feet)
            {
                return Convert(feet, Units.Feet, Units.Meters);
            }

            public static double ToMiles(double feet)
            {
                return Convert(feet, Units.Feet, Units.Miles);
            }


            public static double ToYards(double feet)
            {
                return Convert(feet, Units.Feet, Units.Yards);
            }
        }

        public static class Inches
        {
            public static double ToFeet(double inches)
            {
                return Convert(inches, Units.Inches, Units.Feet);
            }


            public static double ToKilometers(double inches)
            {
                return Convert(inches, Units.Inches, Units.Kilometers);
            }


            public static double ToMeters(double inches)
            {
                return Convert(inches, Units.Inches, Units.Meters);
            }


            public static double ToMiles(double inches)
            {
                return Convert(inches, Units.Inches, Units.Miles);
            }


            public static double ToYards(double inches)
            {
                return Convert(inches, Units.Inches, Units.Yards);
            }
        }

        public static class Kilometers
        {
            public static double ToFeet(double kilometers)
            {
                return Convert(kilometers, Units.Kilometers, Units.Feet);
            }


            public static double ToInches(double kilometers)
            {
                return Convert(kilometers, Units.Kilometers, Units.Inches);
            }

            public static double ToMeters(double kilometers)
            {
                return Convert(kilometers, Units.Kilometers, Units.Meters);
            }


            public static double ToMiles(double kilometers)
            {
                return Convert(kilometers, Units.Kilometers, Units.Miles);
            }


            public static double ToYards(double kilometers)
            {
                return Convert(kilometers, Units.Kilometers, Units.Yards);
            }
        }

        public static class Meters
        {
            public static double ToFeet(double meters)
            {
                return Convert(meters, Units.Meters, Units.Feet);
            }

            public static double ToInches(double meters)
            {
                return Convert(meters, Units.Meters, Units.Inches);
            }

            public static double ToKilometers(double meters)
            {
                return Convert(meters, Units.Meters, Units.Kilometers);
            }


            public static double ToMiles(double meters)
            {
                return Convert(meters, Units.Meters, Units.Miles);
            }


            public static double ToYards(double meters)
            {
                return Convert(meters, Units.Meters, Units.Yards);
            }
        }
    }
}
