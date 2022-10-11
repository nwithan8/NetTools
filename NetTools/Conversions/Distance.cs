namespace NetTools.Conversions;

public static class Distance
{
    public enum Units
    {
        Kilometers,
        Miles,
        Meters,
        Feet,
        Yards
    }

    public static double ConvertUnits(double size, Units currentUnits,
        Units finalUnits)
    {
        return currentUnits switch
        {
            Units.Miles => finalUnits switch
            {
                Units.Feet => Miles.ToFeet(size),
                Units.Kilometers => Miles.ToKilometers(size),
                Units.Meters => Miles.ToMeters(size),
                var _ => size
            },
            Units.Yards => finalUnits switch
            {
                Units.Feet => Yards.ToFeet(size),
                Units.Kilometers => Yards.ToKilometers(size),
                Units.Meters => Yards.ToMeters(size),
                var _ => size
            },
            Units.Feet => finalUnits switch
            {
                Units.Miles => Feet.ToMiles(size),
                Units.Meters => Feet.ToMeters(size),
                Units.Kilometers => Feet.ToKilometers(size),
                var _ => size
            },
            Units.Kilometers => finalUnits switch
            {
                Units.Feet => Kilometers.ToFeet(size),
                Units.Meters => Kilometers.ToMeters(size),
                Units.Miles => Kilometers.ToMiles(size),
                var _ => size
            },
            Units.Meters => finalUnits switch
            {
                Units.Feet => Meters.ToFeet(size),
                Units.Kilometers => Meters.ToKilometers(size),
                Units.Miles => Meters.ToMiles(size),
                var _ => size
            },
            var _ => size
        };
    }

    public static class Miles
    {
        public static double ToFeet(double miles)
        {
            return miles * 5280;
        }

        public static double ToYards(double miles)
        {
            var feet = ToFeet(miles);
            return Feet.ToYards(feet);
        }

        public static double ToKilometers(double miles)
        {
            return miles * 1.60934;
        }

        public static double ToMeters(double miles)
        {
            var kilometers = ToKilometers(miles);
            return Kilometers.ToMeters(kilometers);
        }
    }

    public static class Yards
    {
        public static double ToFeet(double yards)
        {
            return yards * 3;
        }

        public static double ToMiles(double yards)
        {
            var feet = ToFeet(yards);
            return Feet.ToMiles(feet);
        }

        public static double ToKilometers(double yards)
        {
            var feet = ToFeet(yards);
            return Feet.ToKilometers(feet);
        }

        public static double ToMeters(double yards)
        {
            var kilometers = ToKilometers(yards);
            return Kilometers.ToMeters(kilometers);
        }
    }

    public static class Feet
    {
        public static double ToMiles(double feet)
        {
            return feet / 5280;
        }

        public static double ToYards(double feet)
        {
            return feet / 3;
        }

        public static double ToMeters(double feet)
        {
            return feet * 0.3048;
        }

        public static double ToKilometers(double feet)
        {
            var meters = ToMeters(feet);
            return Meters.ToKilometers(meters);
        }
    }

    public static class Kilometers
    {
        public static double ToMeters(double kilometers)
        {
            return kilometers * 1000;
        }

        public static double ToMiles(double kilometers)
        {
            return kilometers * 0.621371;
        }

        public static double ToFeet(double kilometers)
        {
            var miles = ToMiles(kilometers);
            return Miles.ToFeet(miles);
        }

        public static double ToYards(double kilometers)
        {
            var miles = ToMiles(kilometers);
            return Miles.ToYards(miles);
        }
    }

    public static class Meters
    {
        public static double ToKilometers(double meters)
        {
            return meters / 1000;
        }

        public static double ToFeet(double meters)
        {
            return meters * 3.28084;
        }

        public static double ToYards(double meters)
        {
            var feet = ToFeet(meters);
            return Feet.ToYards(feet);
        }

        public static double ToMiles(double meters)
        {
            var feet = ToFeet(meters);
            return Feet.ToMiles(feet);
        }
    }
}
