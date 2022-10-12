namespace NetTools.Conversions
{
    public static class Distance
    {
        public class Units : Enum, IDistanceUnits
        {
            public static readonly Units Yottameters = new Units(1, new MetricUnitInfo("Yottameters", "Ym", 1e+24));
            public static readonly Units Zettameters = new Units(2, new MetricUnitInfo("Zettameters", "Zm", 1e+21));
            public static readonly Units Exameters = new Units(3, new MetricUnitInfo("Exameters", "Em", 1e+18));
            public static readonly Units Petameters = new Units(4, new MetricUnitInfo("Petameters", "Pm", 1e+15));
            public static readonly Units Terameters = new Units(5, new MetricUnitInfo("Terameters", "Tm", 1e+12));
            public static readonly Units Gigameters = new Units(6, new MetricUnitInfo("Gigameters", "Gm", 1e+09));
            public static readonly Units Kilometers = new Units(7, new MetricUnitInfo("Kilometers", "km", 1e+03));
            public static readonly Units Hectometers = new Units(8, new MetricUnitInfo("Hectometers", "hm", 1e+02));
            public static readonly Units Decameters = new Units(9, new MetricUnitInfo("Decameters", "dam", 1e+01));
            public static readonly Units Meters = new Units(10, new MetricUnitInfo("Meters", "m", 1e+00));
            public static readonly Units Decimeters = new Units(11, new MetricUnitInfo("Decimeters", "dm", 1e-01));
            public static readonly Units Centimeters = new Units(12, new MetricUnitInfo("Centimeters", "cm", 1e-02));
            public static readonly Units Millimeters = new Units(13, new MetricUnitInfo("Millimeters", "mm", 1e-03));
            public static readonly Units Micrometers = new Units(14, new MetricUnitInfo("Micrometers", "um", 1e-06));
            public static readonly Units Nanometers = new Units(15, new MetricUnitInfo("Nanometers", "nm", 1e-09));
            public static readonly Units Picometers = new Units(16, new MetricUnitInfo("Picometers", "pm", 1e-12));
            public static readonly Units Inches = new Units(17, new ImperialDistanceUnitInfo("Inches", "in", 1));
            public static readonly Units Feet = new Units(18, new ImperialDistanceUnitInfo("Feet", "ft", 12));
            public static readonly Units Yards = new Units(19, new ImperialDistanceUnitInfo("Yards", "yd", 36));
            public static readonly Units Miles = new Units(20, new ImperialDistanceUnitInfo("Miles", "mi", 63360));

            private Units(int id, IUnitInfo unitInfo) : base(id)
            {
                UnitInfo = unitInfo;
            }

            public IUnitInfo UnitInfo { get; }
        }

        public static double ConvertUnits(double value, Units currentUnits,
            Units finalUnits)
        {
            var currentUnitInfo = currentUnits.UnitInfo;
            var finalUnitInfo = finalUnits.UnitInfo;

            return currentUnitInfo switch
            {
                ImperialDistanceUnitInfo currentImperialUnitInfo when finalUnitInfo is ImperialDistanceUnitInfo finalImperialUnitInfo => ConvertImperialUnits(value, currentImperialUnitInfo, finalImperialUnitInfo),
                ImperialDistanceUnitInfo currentImperialUnitInfo when finalUnitInfo is MetricUnitInfo finalMetricUnitInfo => ConvertImperialToMetricUnits(value, currentImperialUnitInfo, finalMetricUnitInfo),
                MetricUnitInfo currentMetricUnitInfo when finalUnitInfo is ImperialDistanceUnitInfo finalImperialUnitInfo => ConvertMetricToImperialUnits(value, currentMetricUnitInfo, finalImperialUnitInfo),
                MetricUnitInfo currentMetricUnitInfo when finalUnitInfo is MetricUnitInfo finalMetricUnitInfo => ConvertMetricUnits(value, currentMetricUnitInfo, finalMetricUnitInfo),
                var _ => throw new ArgumentException("Invalid unit type")
            };
        }

        private static double ConvertMetricUnits(double value, MetricUnitInfo currentUnitInfo, MetricUnitInfo finalUnitInfo)
        {
            var toBase = currentUnitInfo.ToBase(value); // Convert initial unit to meters
            return finalUnitInfo.FromBase(toBase); // Convert meters to final unit
        }

        private static double ConvertImperialUnits(double value, ImperialDistanceUnitInfo currentUnitInfo, ImperialDistanceUnitInfo finalUnitInfo)
        {
            var toBase = currentUnitInfo.ToBase(value); // Convert initial unit to inches
            return finalUnitInfo.FromBase(toBase); // Convert inches to final unit
        }

        private static double ConvertMetricToImperialUnits(double value, MetricUnitInfo currentUnitInfo, ImperialDistanceUnitInfo finalUnitInfo)
        {
            var toBase = currentUnitInfo.ToBase(value); // Convert initial unit to meters
            var toInches = toBase * 39.3701; // Convert meters to inches
            return finalUnitInfo.FromBase(toInches); // Convert inches to final unit
        }

        private static double ConvertImperialToMetricUnits(double value, ImperialDistanceUnitInfo currentUnitInfo, MetricUnitInfo finalUnitInfo)
        {
            var toBase = currentUnitInfo.ToBase(value); // Convert initial unit to inches
            var toMeters = toBase / 39.3701; // Convert inches to meters
            return finalUnitInfo.FromBase(toMeters); // Convert meters to final unit
        }

        public static class Common
        {
            public static class Miles
            {
                public static double ToFeet(double miles)
                {
                    return ConvertUnits(miles, Units.Miles, Units.Feet);
                }

                public static double ToYards(double miles)
                {
                    return ConvertUnits(miles, Units.Miles, Units.Yards);
                }

                public static double ToInches(double miles)
                {
                    return ConvertUnits(miles, Units.Miles, Units.Inches);
                }

                public static double ToKilometers(double miles)
                {
                    return ConvertUnits(miles, Units.Miles, Units.Kilometers);
                }

                public static double ToMeters(double miles)
                {
                    return ConvertUnits(miles, Units.Miles, Units.Meters);
                }
            }

            public static class Yards
            {
                public static double ToFeet(double yards)
                {
                    return ConvertUnits(yards, Units.Yards, Units.Feet);
                }

                public static double ToMiles(double yards)
                {
                    return ConvertUnits(yards, Units.Yards, Units.Miles);
                }

                public static double ToInches(double yards)
                {
                    return ConvertUnits(yards, Units.Yards, Units.Inches);
                }

                public static double ToKilometers(double yards)
                {
                    return ConvertUnits(yards, Units.Yards, Units.Kilometers);
                }

                public static double ToMeters(double yards)
                {
                    return ConvertUnits(yards, Units.Yards, Units.Meters);
                }
            }

            public static class Feet
            {
                public static double ToMiles(double feet)
                {
                    return ConvertUnits(feet, Units.Feet, Units.Miles);
                }

                public static double ToYards(double feet)
                {
                    return ConvertUnits(feet, Units.Feet, Units.Yards);
                }

                public static double ToInches(double feet)
                {
                    return ConvertUnits(feet, Units.Feet, Units.Inches);
                }

                public static double ToMeters(double feet)
                {
                    return ConvertUnits(feet, Units.Feet, Units.Meters);
                }

                public static double ToKilometers(double feet)
                {
                    return ConvertUnits(feet, Units.Feet, Units.Kilometers);
                }
            }

            public static class Inches
            {
                public static double ToFeet(double inches)
                {
                    return ConvertUnits(inches, Units.Inches, Units.Feet);
                }

                public static double ToMiles(double inches)
                {
                    return ConvertUnits(inches, Units.Inches, Units.Miles);
                }

                public static double ToYards(double inches)
                {
                    return ConvertUnits(inches, Units.Inches, Units.Yards);
                }

                public static double ToMeters(double inches)
                {
                    return ConvertUnits(inches, Units.Inches, Units.Meters);
                }

                public static double ToKilometers(double inches)
                {
                    return ConvertUnits(inches, Units.Inches, Units.Kilometers);
                }
            }

            public static class Kilometers
            {
                public static double ToMeters(double kilometers)
                {
                    return ConvertUnits(kilometers, Units.Kilometers, Units.Meters);
                }

                public static double ToMiles(double kilometers)
                {
                    return ConvertUnits(kilometers, Units.Kilometers, Units.Miles);
                }

                public static double ToFeet(double kilometers)
                {
                    return ConvertUnits(kilometers, Units.Kilometers, Units.Feet);
                }

                public static double ToYards(double kilometers)
                {
                    return ConvertUnits(kilometers, Units.Kilometers, Units.Yards);
                }

                public static double ToInches(double kilometers)
                {
                    return ConvertUnits(kilometers, Units.Kilometers, Units.Inches);
                }
            }

            public static class Meters
            {
                public static double ToKilometers(double meters)
                {
                    return ConvertUnits(meters, Units.Meters, Units.Kilometers);
                }

                public static double ToFeet(double meters)
                {
                    return ConvertUnits(meters, Units.Meters, Units.Feet);
                }

                public static double ToYards(double meters)
                {
                    return ConvertUnits(meters, Units.Meters, Units.Yards);
                }

                public static double ToMiles(double meters)
                {
                    return ConvertUnits(meters, Units.Meters, Units.Miles);
                }

                public static double ToInches(double meters)
                {
                    return ConvertUnits(meters, Units.Meters, Units.Inches);
                }
            }
        }
    }

    public interface IDistanceUnits : IUnit
    {
    }
}
