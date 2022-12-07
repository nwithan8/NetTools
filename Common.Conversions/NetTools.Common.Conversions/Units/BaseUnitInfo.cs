namespace NetTools.Common.Conversions.Units;

internal class BaseUnitInfo
{
    internal string Name { get; }
    internal string Symbol { get; }

    internal BaseUnitInfo(string name, string symbol)
    {
        Name = name;
        Symbol = symbol;
    }
}

internal interface IConvertableUnitInfo
{
    /// <summary>
    ///     The conversion function to convert the base unit to the current unit.
    /// </summary>
    public Func<double, double> FromBase { get; }

    /// <summary>
    ///     The conversion function to convert the current unit to the base unit.
    /// </summary>
    public Func<double, double> ToBase { get; }
}
