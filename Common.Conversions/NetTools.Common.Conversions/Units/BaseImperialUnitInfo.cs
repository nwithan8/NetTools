namespace NetTools.Common.Conversions.Units;

internal class BaseImperialUnitInfo : BaseUnitInfo, IConvertableUnitInfo
{
    public Func<double, double> FromBase => x => x / InBaseUnits;
    public Func<double, double> ToBase => x => x * InBaseUnits;

    internal double InBaseUnits { get; }

    internal BaseImperialUnitInfo(string name, string symbol, double inBaseUnits) : base(name, symbol)
    {
        InBaseUnits = inBaseUnits;
    }
}
