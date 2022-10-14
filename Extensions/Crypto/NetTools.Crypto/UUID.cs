// ReSharper disable InconsistentNaming
namespace NetTools.Crypto;

public static class UUID
{
    public static Guid GenerateUUID()
    {
        return System.Guid.NewGuid();
    }
}
