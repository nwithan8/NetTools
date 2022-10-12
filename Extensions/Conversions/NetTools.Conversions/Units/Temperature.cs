namespace NetTools.Conversions.Units;

internal class TemperatureUnitInfo : BaseUnitInfo, ITemperatureUnitInfo
{
    internal TemperatureUnitInfo(string name, string symbol, Func<double, double> toCelsius, Func<double, double> fromCelsius) : base(name, symbol)
    {
        ToBase = toCelsius;
        FromBase = fromCelsius;
    }

    public Func<double, double> FromBase { get; }
    public Func<double, double> ToBase { get; }
}

internal interface ITemperatureUnitInfo : IConvertableUnitInfo
{
}
