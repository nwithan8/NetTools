namespace NetTools.Common.Conversions.Units;

internal class MetricVolumeUnitInfo : BaseMetricUnitInfo, IVolumeUnitInfo
{
    internal MetricVolumeUnitInfo(string name, string symbol, double base10Power) : base(name, symbol, base10Power)
    {
    }
}

internal class ImperialVolumeUnitInfo : BaseImperialUnitInfo, IVolumeUnitInfo
{
    internal ImperialVolumeUnitInfo(string name, string symbol, double inTeaspoons) : base(name, symbol, inTeaspoons)
    {
    }
}

internal interface IVolumeUnitInfo : IConvertableUnitInfo
{
}
