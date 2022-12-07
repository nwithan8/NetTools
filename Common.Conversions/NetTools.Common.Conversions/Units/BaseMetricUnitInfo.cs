namespace NetTools.Common.Conversions.Units;

internal class BaseMetricUnitInfo : BaseUnitInfo, IConvertableUnitInfo
{
    public Func<double, double> FromBase => x => x / System.Math.Pow(10, Base10Power);
    public Func<double, double> ToBase => x => x * System.Math.Pow(10, Base10Power);

    internal double Base10Power { get; }

    internal BaseMetricUnitInfo(string name, string symbol, double base10Power) : base(name, symbol)
    {
        Base10Power = base10Power;
    }
}
