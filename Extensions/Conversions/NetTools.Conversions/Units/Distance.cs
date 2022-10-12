namespace NetTools.Conversions.Units;

internal class MetricDistanceUnitInfo : BaseMetricUnitInfo, IDistanceUnitInfo
{
    internal MetricDistanceUnitInfo(string name, string symbol, double base10Power) : base(name, symbol, base10Power)
    {
    }
}

internal class ImperialDistanceUnitInfo : BaseUnitInfo, IDistanceUnitInfo
{
    internal ImperialDistanceUnitInfo(string name, string symbol, double inInches) : base(name, symbol)
    {
        InInches = inInches;
    }

    internal double InInches { get; }

    public Func<double, double> FromBase => x => x / InInches;
    public Func<double, double> ToBase => x => x * InInches;
}

internal interface IDistanceUnitInfo : IConvertableUnitInfo
{
}
