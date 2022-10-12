namespace NetTools.Conversions;

internal class MeasurementSystem : Enum
{
    internal static readonly MeasurementSystem Metric = new MeasurementSystem(0);
    internal static readonly MeasurementSystem Imperial = new MeasurementSystem(1);

    private MeasurementSystem(int id) : base(id)
    {
    }
}
