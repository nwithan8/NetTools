namespace NetTools.Common.Conversions.Units;

internal class MetricWeightUnitInfo : BaseMetricUnitInfo, IWeightUnitInfo
{
    internal MetricWeightUnitInfo(string name, string symbol, double base10Power) : base(name, symbol, base10Power)
    {
    }
}

internal class ImperialWeightUnitInfo : BaseImperialUnitInfo, IWeightUnitInfo
{
    internal ImperialWeightUnitInfo(string name, string symbol, double inOunces) : base(name, symbol, inOunces)
    {
    }
}

internal interface IWeightUnitInfo : IConvertableUnitInfo
{
}
