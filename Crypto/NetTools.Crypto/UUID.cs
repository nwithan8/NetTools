// ReSharper disable InconsistentNaming
namespace NetTools.Common.Crypto;

public static class UUID
{
    public static Guid GenerateUUID()
    {
        return System.Guid.NewGuid();
    }
}
