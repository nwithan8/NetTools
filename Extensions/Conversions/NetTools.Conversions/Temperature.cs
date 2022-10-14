using NetTools.Conversions.Units;

namespace NetTools.Conversions;

public static class Temperature
{
    public class Units : Enum
    {
        public static readonly Units Celsius = new Units(0, new TemperatureUnitInfo("Celsius", "°C", d => d, d => d));
        public static readonly Units Fahrenheit = new Units(1, new TemperatureUnitInfo("Fahrenheit", "°F", d => (d - 32) * 5 / 9, d => (d * 9 / 5) + 32));
        public static readonly Units Kelvin = new Units(2, new TemperatureUnitInfo("Kelvin", "K", d => d - 273.15, d => d + 273.15));


        private Units(int id, ITemperatureUnitInfo convertableUnitInfo) : base(id)
        {
            ConvertableUnitInfo = convertableUnitInfo;
        }

        internal IConvertableUnitInfo ConvertableUnitInfo { get; }
    }

    public static double Convert(double value, Units from, Units to)
    {
        var currentUnitInfo = from.ConvertableUnitInfo;
        var finalUnitInfo = to.ConvertableUnitInfo;

        var asCelsius = currentUnitInfo.ToBase(value);
        var asFinal = finalUnitInfo.FromBase(asCelsius);

        return asFinal;
    }

    public static class Common
    {
        public static class Celsius
        {
            public static double ToFahrenheit(double celsius)
                => Convert(celsius, Units.Celsius, Units.Fahrenheit);

            public static double ToKelvin(double celsius)
                => Convert(celsius, Units.Celsius, Units.Kelvin);
        }

        public static class Fahrenheit
        {
            public static double ToCelsius(double fahrenheit)
                => Convert(fahrenheit, Units.Fahrenheit, Units.Celsius);

            public static double ToKelvin(double fahrenheit)
                => Convert(fahrenheit, Units.Fahrenheit, Units.Kelvin);
        }

        public static class Kelvin
        {
            public static double ToCelsius(double kelvin)
                => Convert(kelvin, Units.Kelvin, Units.Celsius);

            public static double ToFahrenheit(double kelvin)
                => Convert(kelvin, Units.Kelvin, Units.Fahrenheit);
        }
    }
}
