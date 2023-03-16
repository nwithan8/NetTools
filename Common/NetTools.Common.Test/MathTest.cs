using System.Collections.Generic;
using NetTools.Common;
using Xunit;

namespace NetTools.Tools;

public class MathTest
{
    [Fact]
    public void TestAddInt()
    {
        var sum = Math.Sum(1, 2, 3, 4);
        
        Assert.Equal(10, sum);
        
        
        var sum2 = Math.Sum(new List<int> { 1, 2, 3, 4 });
        
        Assert.Equal(10, sum2);
    }
    
    [Fact]
    public void TestAddDouble()
    {
        var sum = Math.Sum(1.0, 2.0, 3.0, 4.0);
        
        Assert.Equal(10.0, sum);
        
        
        var sum2 = Math.Sum(new List<double> { 1.0, 2.0, 3.0, 4.0 });
        
        Assert.Equal(10.0, sum2);
    }
    
    [Fact]
    public void TestAddDecimal()
    {
        var sum = Math.Sum(1.0m, 2.0m, 3.0m, 4.0m);
        
        Assert.Equal(10.0m, sum);
        
        
        var sum2 = Math.Sum(new List<decimal> { 1.0m, 2.0m, 3.0m, 4.0m });
        
        Assert.Equal(10.0m, sum2);
    }
    
    [Fact]
    public void TestAddFloat()
    {
        var sum = Math.Sum(1.0f, 2.0f, 3.0f, 4.0f);
        
        Assert.Equal(10.0f, sum);
        
        
        var sum2 = Math.Sum(new List<float> { 1.0f, 2.0f, 3.0f, 4.0f });
        
        Assert.Equal(10.0f, sum2);
    }
    
    [Fact]
    public void TestAddLong()
    {
        var sum = Math.Sum(1L, 2L, 3L, 4L);
        
        Assert.Equal(10L, sum);
        
        
        var sum2 = Math.Sum(new List<long> { 1L, 2L, 3L, 4L });
        
        Assert.Equal(10L, sum2);
    }

    [Fact]
    public void TestProductInt()
    {
        var product = Math.Product(1, 2, 3, 4);
        
        Assert.Equal(24, product);
        
        var product2 = Math.Product(new List<int> { 1, 2, 3, 4 });
        
        Assert.Equal(24, product2);
    }
    
    [Fact]
    public void TestProductDouble()
    {
        var product = Math.Product(1.0, 2.0, 3.0, 4.0);
        
        Assert.Equal(24.0, product);
        
        var product2 = Math.Product(new List<double> { 1.0, 2.0, 3.0, 4.0 });
        
        Assert.Equal(24.0, product2);
    }
    
    [Fact]
    public void TestProductDecimal()
    {
        var product = Math.Product(1.0m, 2.0m, 3.0m, 4.0m);
        
        Assert.Equal(24.0m, product);
        
        var product2 = Math.Product(new List<decimal> { 1.0m, 2.0m, 3.0m, 4.0m });
        
        Assert.Equal(24.0m, product2);
    }
    
    [Fact]
    public void TestProductFloat()
    {
        var product = Math.Product(1.0f, 2.0f, 3.0f, 4.0f);
        
        Assert.Equal(24.0f, product);
        
        var product2 = Math.Product(new List<float> { 1.0f, 2.0f, 3.0f, 4.0f });
        
        Assert.Equal(24.0f, product2);
    }
    
    [Fact]
    public void TestProductLong()
    {
        var product = Math.Product(1L, 2L, 3L, 4L);
        
        Assert.Equal(24L, product);
        
        var product2 = Math.Product(new List<long> { 1L, 2L, 3L, 4L });
        
        Assert.Equal(24L, product2);
    }
    
    [Fact]
    public void TestAverageInt()
    {
        var average = Math.Average(1, 2, 3, 4);
        
        Assert.Equal(2, average);
        
        var average2 = Math.Average(new List<int> { 1, 2, 3, 4 });
        
        Assert.Equal(2, average2);
    }
    
    [Fact]
    public void TestAverageDouble()
    {
        var average = Math.Average(1.0, 2.0, 3.0, 4.0);
        
        Assert.Equal(2.5, average);
        
        var average2 = Math.Average(new List<double> { 1.0, 2.0, 3.0, 4.0 });
        
        Assert.Equal(2.5, average2);
    }
    
    [Fact]
    public void TestAverageDecimal()
    {
        var average = Math.Average(1.0m, 2.0m, 3.0m, 4.0m);
        
        Assert.Equal(2.5m, average);
        
        var average2 = Math.Average(new List<decimal> { 1.0m, 2.0m, 3.0m, 4.0m });
        
        Assert.Equal(2.5m, average2);
    }
    
    [Fact]
    public void TestAverageFloat()
    {
        var average = Math.Average(1.0f, 2.0f, 3.0f, 4.0f);
        
        Assert.Equal(2.5f, average);
        
        var average2 = Math.Average(new List<float> { 1.0f, 2.0f, 3.0f, 4.0f });
        
        Assert.Equal(2.5f, average2);
    }
    
    [Fact]
    public void TestAverageLong()
    {
        var average = Math.Average(1L, 2L, 3L, 4L);
        
        Assert.Equal(2, average);
        
        var average2 = Math.Average(new List<long> { 1L, 2L, 3L, 4L });
        
        Assert.Equal(2, average2);
    }
    
    [Fact]
    public void TestMinInt()
    {
        var min = Math.Min(1, 2, 3, 4);
        
        Assert.Equal(1, min);
        
        var min2 = Math.Min(new List<int> { 1, 2, 3, 4 });
        
        Assert.Equal(1, min2);
    }
    
    [Fact]
    public void TestMinDouble()
    {
        var min = Math.Min(1.0, 2.0, 3.0, 4.0);
        
        Assert.Equal(1.0, min);
        
        var min2 = Math.Min(new List<double> { 1.0, 2.0, 3.0, 4.0 });
        
        Assert.Equal(1.0, min2);
    }
    
    [Fact]
    public void TestMinDecimal()
    {
        var min = Math.Min(1.0m, 2.0m, 3.0m, 4.0m);
        
        Assert.Equal(1.0m, min);
        
        var min2 = Math.Min(new List<decimal> { 1.0m, 2.0m, 3.0m, 4.0m });
        
        Assert.Equal(1.0m, min2);
    }
    
    [Fact]
    public void TestMinFloat()
    {
        var min = Math.Min(1.0f, 2.0f, 3.0f, 4.0f);
        
        Assert.Equal(1.0f, min);
        
        var min2 = Math.Min(new List<float> { 1.0f, 2.0f, 3.0f, 4.0f });
        
        Assert.Equal(1.0f, min2);
    }
    
    [Fact]
    public void TestMinLong()
    {
        var min = Math.Min(1L, 2L, 3L, 4L);
        
        Assert.Equal(1L, min);
        
        var min2 = Math.Min(new List<long> { 1L, 2L, 3L, 4L });
        
        Assert.Equal(1L, min2);
    }
    
    [Fact]
    public void TestMaxInt()
    {
        var max = Math.Max(1, 2, 3, 4);
        
        Assert.Equal(4, max);
        
        var max2 = Math.Max(new List<int> { 1, 2, 3, 4 });
        
        Assert.Equal(4, max2);
    }
    
    [Fact]
    public void TestMaxDouble()
    {
        var max = Math.Max(1.0, 2.0, 3.0, 4.0);
        
        Assert.Equal(4.0, max);
        
        var max2 = Math.Max(new List<double> { 1.0, 2.0, 3.0, 4.0 });
        
        Assert.Equal(4.0, max2);
    }
    
    [Fact]
    public void TestMaxDecimal()
    {
        var max = Math.Max(1.0m, 2.0m, 3.0m, 4.0m);
        
        Assert.Equal(4.0m, max);
        
        var max2 = Math.Max(new List<decimal> { 1.0m, 2.0m, 3.0m, 4.0m });
        
        Assert.Equal(4.0m, max2);
    }
    
    [Fact]
    public void TestMaxFloat()
    {
        var max = Math.Max(1.0f, 2.0f, 3.0f, 4.0f);
        
        Assert.Equal(4.0f, max);
        
        var max2 = Math.Max(new List<float> { 1.0f, 2.0f, 3.0f, 4.0f });
        
        Assert.Equal(4.0f, max2);
    }
    
    [Fact]
    public void TestMaxLong()
    {
        var max = Math.Max(1L, 2L, 3L, 4L);
        
        Assert.Equal(4L, max);
        
        var max2 = Math.Max(new List<long> { 1L, 2L, 3L, 4L });
        
        Assert.Equal(4L, max2);
    }

    [Fact]
    public void TestModeInt()
    {
        var mode = Math.Mode(1, 2, 3, 4);
        
        Assert.Equal(1, mode);
        
        var mode2 = Math.Mode(new List<int> { 1, 2, 3, 4 });
        
        Assert.Equal(1, mode2);

        var mode3 = Math.Mode(1, 2, 3, 4, 4);
        
        Assert.Equal(4, mode3);
        
        var mode4 = Math.Mode(new List<int> { 1, 2, 3, 4, 4 });
        
        Assert.Equal(4, mode4);
    }
    
    [Fact]
    public void TestModeDouble()
    {
        var mode = Math.Mode(1.0, 2.0, 3.0, 4.0);
        
        Assert.Equal(1.0, mode);
        
        var mode2 = Math.Mode(new List<double> { 1.0, 2.0, 3.0, 4.0 });
        
        Assert.Equal(1.0, mode2);

        var mode3 = Math.Mode(1.0, 2.0, 3.0, 4.0, 4.0);
        
        Assert.Equal(4.0, mode3);
        
        var mode4 = Math.Mode(new List<double> { 1.0, 2.0, 3.0, 4.0, 4.0 });
        
        Assert.Equal(4.0, mode4);
    }
    
    [Fact]
    public void TestModeDecimal()
    {
        var mode = Math.Mode(1.0m, 2.0m, 3.0m, 4.0m);
        
        Assert.Equal(1.0m, mode);
        
        var mode2 = Math.Mode(new List<decimal> { 1.0m, 2.0m, 3.0m, 4.0m });
        
        Assert.Equal(1.0m, mode2);

        var mode3 = Math.Mode(1.0m, 2.0m, 3.0m, 4.0m, 4.0m);
        
        Assert.Equal(4.0m, mode3);
        
        var mode4 = Math.Mode(new List<decimal> { 1.0m, 2.0m, 3.0m, 4.0m, 4.0m });
        
        Assert.Equal(4.0m, mode4);
    }
    
    [Fact]
    public void TestModeFloat()
    {
        var mode = Math.Mode(1.0f, 2.0f, 3.0f, 4.0f);
        
        Assert.Equal(1.0f, mode);
        
        var mode2 = Math.Mode(new List<float> { 1.0f, 2.0f, 3.0f, 4.0f });
        
        Assert.Equal(1.0f, mode2);

        var mode3 = Math.Mode(1.0f, 2.0f, 3.0f, 4.0f, 4.0f);
        
        Assert.Equal(4.0f, mode3);
        
        var mode4 = Math.Mode(new List<float> { 1.0f, 2.0f, 3.0f, 4.0f, 4.0f });
        
        Assert.Equal(4.0f, mode4);
    }
    
    [Fact]
    public void TestModeLong()
    {
        var mode = Math.Mode(1L, 2L, 3L, 4L);
        
        Assert.Equal(1L, mode);
        
        var mode2 = Math.Mode(new List<long> { 1L, 2L, 3L, 4L });
        
        Assert.Equal(1L, mode2);

        var mode3 = Math.Mode(1L, 2L, 3L, 4L, 4L);
        
        Assert.Equal(4L, mode3);
        
        var mode4 = Math.Mode(new List<long> { 1L, 2L, 3L, 4L, 4L });
        
        Assert.Equal(4L, mode4);
    }
    
    [Fact]
    public void TestMedianInt()
    {
        var median = Math.Median(1, 2, 3, 4);
        
        Assert.Equal(2, median);
        
        var median2 = Math.Median(new List<int> { 1, 2, 3, 4 });
        
        Assert.Equal(2, median2);

        var median3 = Math.Median(1, 2, 3, 4, 5);
        
        Assert.Equal(3, median3);
        
        var median4 = Math.Median(new List<int> { 1, 2, 3, 4, 5 });
        
        Assert.Equal(3, median4);
    }
    
    [Fact]
    public void TestMedianDouble()
    {
        var median = Math.Median(1.0, 2.0, 3.0, 4.0);
        
        Assert.Equal(2.0, median);
        
        var median2 = Math.Median(new List<double> { 1.0, 2.0, 3.0, 4.0 });
        
        Assert.Equal(2.0, median2);

        var median3 = Math.Median(1.0, 2.0, 3.0, 4.0, 5.0);
        
        Assert.Equal(3.0, median3);
        
        var median4 = Math.Median(new List<double> { 1.0, 2.0, 3.0, 4.0, 5.0 });
        
        Assert.Equal(3.0, median4);
    }
    
    [Fact]
    public void TestMedianDecimal()
    {
        var median = Math.Median(1.0m, 2.0m, 3.0m, 4.0m);
        
        Assert.Equal(2.0m, median);
        
        var median2 = Math.Median(new List<decimal> { 1.0m, 2.0m, 3.0m, 4.0m });
        
        Assert.Equal(2.0m, median2);

        var median3 = Math.Median(1.0m, 2.0m, 3.0m, 4.0m, 5.0m);
        
        Assert.Equal(3.0m, median3);
        
        var median4 = Math.Median(new List<decimal> { 1.0m, 2.0m, 3.0m, 4.0m, 5.0m });
        
        Assert.Equal(3.0m, median4);
    }
    
    [Fact]
    public void TestMedianFloat()
    {
        var median = Math.Median(1.0f, 2.0f, 3.0f, 4.0f);
        
        Assert.Equal(2.0f, median);
        
        var median2 = Math.Median(new List<float> { 1.0f, 2.0f, 3.0f, 4.0f });
        
        Assert.Equal(2.0f, median2);

        var median3 = Math.Median(1.0f, 2.0f, 3.0f, 4.0f, 5.0f);
        
        Assert.Equal(3.0f, median3);
        
        var median4 = Math.Median(new List<float> { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f });
        
        Assert.Equal(3.0f, median4);
    }
    
    [Fact]
    public void TestMedianLong()
    {
        var median = Math.Median(1L, 2L, 3L, 4L);
        
        Assert.Equal(2L, median);
        
        var median2 = Math.Median(new List<long> { 1L, 2L, 3L, 4L });
        
        Assert.Equal(2L, median2);

        var median3 = Math.Median(1L, 2L, 3L, 4L, 5L);
        
        Assert.Equal(3L, median3);
        
        var median4 = Math.Median(new List<long> { 1L, 2L, 3L, 4L, 5L });
        
        Assert.Equal(3L, median4);
    }

    [Fact]
    public void TestIsEven()
    {
        var isEven = 2.IsEven();
        
        Assert.True(isEven);
        
        var isEven2 = 3.IsEven();
        
        Assert.False(isEven2);
    }
    
    [Fact]
    public void TestIsOdd()
    {
        var isOdd = 3.IsOdd();
        
        Assert.True(isOdd);
        
        var isOdd2 = 2.IsOdd();
        
        Assert.False(isOdd2);
    }
}
