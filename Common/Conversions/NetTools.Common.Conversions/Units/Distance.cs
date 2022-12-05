namespace NetTools.Common.Conversions.Units;

internal class MetricDistanceUnitInfo : BaseMetricUnitInfo, IDistanceUnitInfo
{
    internal MetricDistanceUnitInfo(string name, string symbol, double base10Power) : base(name, symbol, base10Power)
    {
    }
}

internal class ImperialDistanceUnitInfo : BaseImperialUnitInfo, IDistanceUnitInfo
{
    internal ImperialDistanceUnitInfo(string name, string symbol, double inInches) : base(name, symbol, inInches)
    {
    }
}

internal interface IDistanceUnitInfo : IConvertableUnitInfo
{
}
