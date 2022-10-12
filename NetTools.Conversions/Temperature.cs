namespace NetTools.Conversions;

public static class Temperature
{
    public static class Celsius
    {
        public static double ToFahrenheit(double celsius)
        {
            return (celsius * 9 / 5) + 32;
        }

        public static double ToKelvin(double celsius)
        {
            return celsius + 273.15;
        }
    }
    
    public static class Fahrenheit
    {
        public static double ToCelsius(double fahrenheit)
        {
            return (fahrenheit - 32) * 5 / 9;
        }

        public static double ToKelvin(double fahrenheit)
        {
            return (fahrenheit + 459.67) * 5 / 9;
        }
    }
    
    public static class Kelvin
    {
        public static double ToCelsius(double kelvin)
        {
            return kelvin - 273.15;
        }

        public static double ToFahrenheit(double kelvin)
        {
            return kelvin * 9 / 5 - 459.67;
        }
    }
}
