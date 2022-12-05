using Xunit;

namespace NetTools.Common.Conversions.Test;

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

    [Fact]
    public void TestVolumeConversion()
    {
        const int gallons = 100;

        var liters = Conversions.Volume.Convert(gallons, Volume.Units.Gallons, Volume.Units.Liters);

        Assert.Equal(378.5411, liters, 3); // 3 decimal places tolerance
    }

    [Fact]
    public void TestWeightConversion()
    {
        const int pounds = 100;

        var kilograms = Conversions.Weight.Convert(pounds, Weight.Units.Pounds, Weight.Units.Kilograms);

        Assert.Equal(45.3592, kilograms, 3); // 3 decimal places tolerance
    }
}
