namespace NetTools.Conversions;

public class MetricUnitInfo : UnitInfo, IUnitInfo
{
    internal MetricUnitInfo(string name, string symbol, double base10Power) : base(name, symbol, MeasurementSystem.Metric)
    {
        Base10Power = base10Power;
    }

    internal double Base10Power { get; }

    public Func<double, double> FromBase => x => x / Math.Pow(10, Base10Power);
    public Func<double, double> ToBase => x => x * Math.Pow(10, Base10Power);
}

public class ImperialDistanceUnitInfo : UnitInfo, IUnitInfo
{
    internal ImperialDistanceUnitInfo(string name, string symbol, double inInches) : base(name, symbol, MeasurementSystem.Imperial)
    {
        InInches = inInches;
    }

    internal double InInches { get; }

    public Func<double, double> FromBase => x => x / InInches;
    public Func<double, double> ToBase => x => x * InInches;
}

public class UnitInfo
{
    internal UnitInfo(string name, string symbol, MeasurementSystem measurementSystem)
    {
        Name = name;
        Symbol = symbol;
        MeasurementSystem = measurementSystem;
    }

    public string Name { get; }
    public string Symbol { get; }

    internal MeasurementSystem MeasurementSystem { get; set; }
}

public interface IUnitInfo
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
