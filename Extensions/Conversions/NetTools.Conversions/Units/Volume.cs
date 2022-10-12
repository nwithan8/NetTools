namespace NetTools.Conversions.Units;

internal class MetricVolumeUnitInfo : BaseMetricUnitInfo, IVolumeUnitInfo
{
    internal MetricVolumeUnitInfo(string name, string symbol, double base10Power) : base(name, symbol, base10Power)
    {
    }
}

internal interface IVolumeUnitInfo : IConvertableUnitInfo
{
}