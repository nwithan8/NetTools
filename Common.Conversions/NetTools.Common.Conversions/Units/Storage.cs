namespace NetTools.Common.Conversions.Units;

internal class StorageUnitInfo : BaseUnitInfo, IStorageUnitInfo
{
    public Func<double, double> FromBase => x => x / System.Math.Pow(1024, Base1024) / (Bytes ? 8 : 1);
    public Func<double, double> ToBase => x => x * System.Math.Pow(1024, Base1024) * (Bytes ? 8 : 1);

    internal int Base1024 { get; }

    internal bool Bytes { get; }

    internal StorageUnitInfo(string name, string symbol, int base1024, bool bytes = false) : base(name, symbol)
    {
        Base1024 = base1024;
        Bytes = bytes;
    }
}

internal interface IStorageUnitInfo : IConvertableUnitInfo
{
}
