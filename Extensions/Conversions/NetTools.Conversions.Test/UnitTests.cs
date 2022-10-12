using Xunit;

namespace NetTools.Conversions.Test;

public class UnitTests
{
    [Fact]
    public void TestStorageConversion()
    {
        const int mb = 2;

        var kb = Conversions.Storage.Convert(mb, Storage.Units.Megabyte, Storage.Units.Bit);

        Assert.Equal(mb * 1024 * 1024 * 8, kb);
    }

    [Fact]
    public void TestTemperatureConversion()
    {
        const int celsius = 100;

        var fahrenheit = Conversions.Temperature.Convert(celsius, Temperature.Units.Celsius, Temperature.Units.Fahrenheit);

        Assert.Equal(212, fahrenheit);
    }

    [Fact]
    public void TestDistanceConversion()
    {
        const int miles = 100;

        var kilometers = Conversions.Distance.Convert(miles, Distance.Units.Miles, Distance.Units.Kilometers);

        Assert.Equal(160.9344, kilometers, 3); // 3 decimal places tolerance
    }
}
