namespace NetTools.Common.Conversions.Units;

internal class BaseImperialUnitInfo : BaseUnitInfo, IConvertableUnitInfo
{
    internal BaseImperialUnitInfo(string name, string symbol, double inBaseUnits) : base(name, symbol)
    {
        InBaseUnits = inBaseUnits;
    }

    internal double InBaseUnits { get; }

    public Func<double, double> FromBase => x => x / InBaseUnits;
    public Func<double, double> ToBase => x => x * InBaseUnits;
}
