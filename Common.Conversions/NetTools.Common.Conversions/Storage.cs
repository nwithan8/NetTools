using NetTools.Common.Conversions.Units;

namespace NetTools.Common.Conversions;

public static class Storage
{
    public static double Convert(double value, Units from, Units to)
    {
        var currentUnitInfo = from.ConvertableUnitInfo;
        var finalUnitInfo = to.ConvertableUnitInfo;

        var asBits = currentUnitInfo.ToBase(value);
        var asFinal = finalUnitInfo.FromBase(asBits);

        return asFinal;
    }

    public class Units : Enum
    {
        public static readonly Units Bit = new(0, new StorageUnitInfo("Bit", "b", 0, false));
        public static readonly Units Byte = new(1, new StorageUnitInfo("Byte", "B", 0, true));
        public static readonly Units Exabit = new(12, new StorageUnitInfo("Exabit", "Eb", 6, false));
        public static readonly Units Exabyte = new(13, new StorageUnitInfo("Exabyte", "EB", 6, true));
        public static readonly Units Gigabit = new(6, new StorageUnitInfo("Gigabit", "Gb", 3, false));
        public static readonly Units Gigabyte = new(7, new StorageUnitInfo("Gigabyte", "GB", 3, true));
        public static readonly Units Kilobit = new(2, new StorageUnitInfo("Kilobit", "kb", 1, false));
        public static readonly Units Kilobyte = new(3, new StorageUnitInfo("Kilobyte", "kB", 1, true));
        public static readonly Units Megabit = new(4, new StorageUnitInfo("Megabit", "Mb", 2, false));
        public static readonly Units Megabyte = new(5, new StorageUnitInfo("Megabyte", "MB", 2, true));
        public static readonly Units Petabit = new(10, new StorageUnitInfo("Petabit", "Pb", 5, false));
        public static readonly Units Petabyte = new(11, new StorageUnitInfo("Petabyte", "PB", 5, true));
        public static readonly Units Terabit = new(8, new StorageUnitInfo("Terabit", "Tb", 4, false));
        public static readonly Units Terabyte = new(9, new StorageUnitInfo("Terabyte", "TB", 4, true));
        public static readonly Units Yottabit = new(16, new StorageUnitInfo("Yottabit", "Yb", 8, false));
        public static readonly Units Yottabyte = new(17, new StorageUnitInfo("Yottabyte", "YB", 8, true));
        public static readonly Units Zettabit = new(14, new StorageUnitInfo("Zettabit", "Zb", 7, false));
        public static readonly Units Zettabyte = new(15, new StorageUnitInfo("Zettabyte", "ZB", 7, true));

        internal IConvertableUnitInfo ConvertableUnitInfo { get; }

        private Units(int id, IStorageUnitInfo convertableUnitInfo) : base(id)
        {
            ConvertableUnitInfo = convertableUnitInfo;
        }
    }
}
