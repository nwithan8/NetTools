using System.Linq;
using Xunit;

namespace NetTools.Tools;

public class UnitTests
{
    public class TestEnum : NetTools.Enum
    {
        public static readonly TestEnum Value1 = new TestEnum(1);
        public static readonly TestEnum Value2 = new TestEnum(2);
        public static readonly TestEnum Value3 = new TestEnum(3);

        private TestEnum(int id) : base(id)
        {
        }
    }

    public class TestValueEnum : NetTools.ValueEnum
    {
        public static readonly TestValueEnum Value1 = new TestValueEnum(1, 1);
        public static readonly TestValueEnum Value2 = new TestValueEnum(2, "string");
        public static readonly TestValueEnum Value3 = new TestValueEnum(3, false);

        private TestValueEnum(int id, object value) : base(id, value)
        {
        }
    }

    public class TestMultiValueEnum : NetTools.MultiValueEnum
    {
        public static readonly TestMultiValueEnum Value1 = new TestMultiValueEnum(1, "string", 100.00, false, new object());
        public static readonly TestMultiValueEnum Value2 = new TestMultiValueEnum(2, 1, 3, 4);
        public static readonly TestMultiValueEnum Value3 = new TestMultiValueEnum(3, true, false);
        public static readonly TestMultiValueEnum Value4 = new TestMultiValueEnum(4, "string", 100.00, false, TestEnum.Value1);

        private TestMultiValueEnum(int id, params object[] values) : base(id, values)
        { 
        }
    }

    [Fact]
    public void TestSwitchCase()
    {
        var result = 0;
        var @switch = new SwitchCase
        {
            { true, () => { result = 1; } },
            { "string", () => { result = 2; } },
            { 100, () => { result = 3; } },
            { Scenario.Default, () => { result = 4; } }
        };
        @switch.MatchFirst(true); // evaluate switch case, checking which expression evaluates to "true"
        Assert.Equal(1, result);
    }

    [Fact]
    public void TestEnumAll()
    {
        var all = Enum.All<TestMultiValueEnum>();
        
        Assert.Equal(4, Enum.All<TestMultiValueEnum>().Count());
    }

    [Fact]
    public void TestEnumFromId()
    {
        const int id = 1;
        
        var value = Enum.FromId<TestEnum>(id);
        Assert.Equal(TestEnum.Value1, value);
        
        var value2 = Enum.FromId<TestValueEnum>(id);
        Assert.Equal(TestValueEnum.Value1, value2);
        
        var value3 = Enum.FromId<TestMultiValueEnum>(id);
        Assert.Equal(TestMultiValueEnum.Value1, value3);
    }

    [Fact]
    public void TestEnumFromValue()
    {
        const int value = 1;
        
        var value2 = ValueEnum.FromValue<TestValueEnum>(value);
        Assert.Equal(TestValueEnum.Value1, value2);
        
        var value3 = MultiValueEnum.FromValue<TestMultiValueEnum>(value);
        Assert.Equal(TestMultiValueEnum.Value2, value3);
    }
    
    [Fact]
    public void TestEnumFromValues()
    {
        const string value1 = "string";
        const double value2 = 100.00;
        const bool value3 = false;
        var value4 = TestEnum.Value1;

        // find a match if all values are provided
        var matchingEnum = MultiValueEnum.FromValues<TestMultiValueEnum>(value1, value2, value3, value4);
        Assert.Equal(TestMultiValueEnum.Value4, matchingEnum);
        
        // don't find a match if not all values are provided
        var matchingEnum2 = MultiValueEnum.FromValues<TestMultiValueEnum>(value1, value2, value3);
        Assert.Null(matchingEnum2);
    }
    
    [Fact]
    public void TestEnumFromValuesOrder()
    {
        const string value1 = "string";
        const double value2 = 100.00;
        const bool value3 = false;
        var value4 = TestEnum.Value1;
        
        // find a match if all values are present and provided in the right order
        var matchingEnum = MultiValueEnum.FromValuesOrder<TestMultiValueEnum>(value1, value2, value3, value4);
        Assert.Equal(TestMultiValueEnum.Value4, matchingEnum);
        
        // don't find a match if not all values are provided
        var matchingEnum2 = MultiValueEnum.FromValuesOrder<TestMultiValueEnum>(value1, value2, value3);
        Assert.Null(matchingEnum2);
        
        // don't find a match if all values are present, but not provided in the wrong order
        var matchingEnum3 = MultiValueEnum.FromValuesOrder<TestMultiValueEnum>(value4, value1, value3, value2);
        Assert.Null(matchingEnum3);
    }
    
}