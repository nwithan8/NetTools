namespace NetTools.Conversions.Units;

internal class BaseMetricUnitInfo : BaseUnitInfo, IConvertableUnitInfo
{
    internal BaseMetricUnitInfo(string name, string symbol, double base10Power) : base(name, symbol)
    {
        Base10Power = base10Power;
    }

    internal double Base10Power { get; }

    public Func<double, double> FromBase => x => x / Math.Pow(10, Base10Power);
    public Func<double, double> ToBase => x => x * Math.Pow(10, Base10Power);
}