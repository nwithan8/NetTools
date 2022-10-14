using NetTools.Conversions.Units;

namespace NetTools.Conversions;

public static class Storage
{
    public class Units : Enum
    {
        public static readonly Units Bit = new Units(0, new StorageUnitInfo("Bit", "b", 0, false));
        public static readonly Units Byte = new Units(1, new StorageUnitInfo("Byte", "B", 0, true));
        public static readonly Units Kilobit = new Units(2, new StorageUnitInfo("Kilobit", "kb", 1, false));
        public static readonly Units Kilobyte = new Units(3, new StorageUnitInfo("Kilobyte", "kB", 1, true));
        public static readonly Units Megabit = new Units(4, new StorageUnitInfo("Megabit", "Mb", 2, false));
        public static readonly Units Megabyte = new Units(5, new StorageUnitInfo("Megabyte", "MB", 2, true));
        public static readonly Units Gigabit = new Units(6, new StorageUnitInfo("Gigabit", "Gb", 3, false));
        public static readonly Units Gigabyte = new Units(7, new StorageUnitInfo("Gigabyte", "GB", 3, true));
        public static readonly Units Terabit = new Units(8, new StorageUnitInfo("Terabit", "Tb", 4, false));
        public static readonly Units Terabyte = new Units(9, new StorageUnitInfo("Terabyte", "TB", 4, true));
        public static readonly Units Petabit = new Units(10, new StorageUnitInfo("Petabit", "Pb", 5, false));
        public static readonly Units Petabyte = new Units(11, new StorageUnitInfo("Petabyte", "PB", 5, true));
        public static readonly Units Exabit = new Units(12, new StorageUnitInfo("Exabit", "Eb", 6, false));
        public static readonly Units Exabyte = new Units(13, new StorageUnitInfo("Exabyte", "EB", 6, true));
        public static readonly Units Zettabit = new Units(14, new StorageUnitInfo("Zettabit", "Zb", 7, false));
        public static readonly Units Zettabyte = new Units(15, new StorageUnitInfo("Zettabyte", "ZB", 7, true));
        public static readonly Units Yottabit = new Units(16, new StorageUnitInfo("Yottabit", "Yb", 8, false));
        public static readonly Units Yottabyte = new Units(17, new StorageUnitInfo("Yottabyte", "YB", 8, true));

        private Units(int id, IStorageUnitInfo convertableUnitInfo) : base(id)
        {
            ConvertableUnitInfo = convertableUnitInfo;
        }

        internal IConvertableUnitInfo ConvertableUnitInfo { get; }
    }

    public static double Convert(double value, Units from, Units to)
    {
        var currentUnitInfo = from.ConvertableUnitInfo;
        var finalUnitInfo = to.ConvertableUnitInfo;

        var asBits = currentUnitInfo.ToBase(value);
        var asFinal = finalUnitInfo.FromBase(asBits);

        return asFinal;
    }
}
