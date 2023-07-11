using System.Collections.Generic;
using System.Linq;
using NetTools.Common;
using NetTools.JSON;
using Newtonsoft.Json;
using Xunit;

namespace NetTools.Tools;

public class EnumerationTest
{
    public enum LegacyEnum
    {
        Value1,
        Value2,
        Value3,
    }
    
    // decorated with JsonConverter attribute so that it can be de/serialized custom
    [JsonConverter(typeof(EnumJsonConverter<TestEnum>))]
    public class TestEnum : Enum
    {
        public static readonly TestEnum Value1 = new TestEnum(1);
        public static readonly TestEnum Value2 = new TestEnum(2);
        public static readonly TestEnum Value3 = new TestEnum(3);

        private TestEnum(int id) : base(id)
        {
        }
    }

    // decorated with JsonConverter attribute so that it can be de/serialized custom
    [JsonConverter(typeof(ValueEnumJsonConverter<TestValueEnum>))]
    public class TestValueEnum : ValueEnum
    {
        public static readonly TestValueEnum Value1 = new TestValueEnum(1, "string1");
        public static readonly TestValueEnum Value2 = new TestValueEnum(2, "string2");
        public static readonly TestValueEnum Value3 = new TestValueEnum(3, "string3");

        private TestValueEnum(int id, object value) : base(id, value)
        {
        }
    }

    // decorated with JsonConverter attribute so that it can be de/serialized custom
    [JsonConverter(typeof(MultiValueEnumJsonConverter<TestMultiValueEnum>))]
    public class TestMultiValueEnum : MultiValueEnum
    {
        public static readonly TestMultiValueEnum Value1 = new TestMultiValueEnum(1, "string1", 100.00, false, new object());
        public static readonly TestMultiValueEnum Value2 = new TestMultiValueEnum(2, 1, 3, 4);
        public static readonly TestMultiValueEnum Value3 = new TestMultiValueEnum(3, true, false);
        public static readonly TestMultiValueEnum Value4 = new TestMultiValueEnum(4, "string2", 100.00, false, TestEnum.Value1);

        private TestMultiValueEnum(int id, params object[] values) : base(id, values)
        {
        }

        public override int? SerializerValueIndex => 1;
    }
    
    internal sealed class TestClass
    {
        [JsonProperty("enum")]
        public TestEnum? Enum { get; set; }

        [JsonProperty("value_enum")]
        public TestValueEnum? ValueEnum { get; set; }
        
        [JsonProperty("multi_value_enum")]
        public TestMultiValueEnum? MultiValueEnum { get; set; }
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
        const string value = "string2";

        var @enum = ValueEnum.FromValue<TestValueEnum>(value);
        Assert.Equal(TestValueEnum.Value2, @enum);

        var @enum2 = MultiValueEnum.FromValue<TestMultiValueEnum>(value);
        Assert.Equal(TestMultiValueEnum.Value4, @enum2);
    }

    [Fact]
    public void TestEnumFromValues()
    {
        const string value1 = "string2";
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
        const string value1 = "string2";
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

    [Fact]
    public void TestGetAllCustomEnums()
    {
        // get a list of all the enum IDs
        var enumValues = new List<object>
        {
            "string1",
            "string2",
            "string3",
        };
        var enums = Enum.All<TestValueEnum>();
        foreach (var @enum in enums)
        {
            Assert.IsType<TestValueEnum>(@enum);
            Assert.Contains(enumValues, x => x == @enum.Value);
        }
    }

    [Fact]
    public void TestCompareCustomEnums()
    {
        // compare against same value
        Assert.True(TestValueEnum.Value1.Equals(TestValueEnum.Value1));
        Assert.True(TestValueEnum.Value1 == TestValueEnum.Value1);
        Assert.False(TestValueEnum.Value1 != TestValueEnum.Value1);
        // compareTo checks if x comes before y
        // since they are the same, it should return 0
        Assert.True(TestValueEnum.Value1.CompareTo(TestValueEnum.Value1) == 0);

        // compare against different value
        Assert.False(TestValueEnum.Value1.Equals(TestValueEnum.Value2));
        Assert.False(TestValueEnum.Value1 == TestValueEnum.Value2);
        Assert.True(TestValueEnum.Value1 != TestValueEnum.Value2);
        // compareTo checks if x comes before y
        // should return -1
        Assert.True(TestValueEnum.Value1.CompareTo(TestValueEnum.Value2) == -1);

        // compare against null
        Assert.False(TestValueEnum.Value1.Equals(null));
        Assert.False(TestValueEnum.Value1 == null);
        Assert.True(TestValueEnum.Value1 != null);
    }

    [Fact]
    public void TestEnumToString()
    {
        // normal enums print their ID as a string
        Assert.Equal("1", TestEnum.Value1.ToString());

        // value enums print their value as a string
        Assert.Equal("string2", TestValueEnum.Value2.ToString());
    }
    
    [Fact]
    public void TestSerialization()
    {
        var obj = new TestClass
        {
            Enum = TestEnum.Value1,
            ValueEnum = TestValueEnum.Value1,
            MultiValueEnum = TestMultiValueEnum.Value1,
        };

        // run JSON serialization process
        var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonSerialization.ConvertObjectToJson(obj));

        // check that the enum values are serialized correctly
        Assert.Equal(1, (int)(long)dictionary["enum"]); // need to cast to avoid int64 vs int32 issues (https://stackoverflow.com/a/31737978)
        Assert.Equal("string1", dictionary["value_enum"]);
        Assert.Equal(100.00, (double)dictionary["multi_value_enum"]);
    }

    [Fact]
    public void TestDeserialization()
    {
        var dictionary = new Dictionary<string, object>
        {
            { "enum", 1 },
            { "value_enum", "string1" },
            { "multi_value_enum", 100.00 },
        };

        // run JSON deserialization process
        var obj = JsonConvert.DeserializeObject<TestClass>(JsonConvert.SerializeObject(dictionary));

        // check that the enum values are deserialized correctly
        Assert.Equal(TestEnum.Value1, obj.Enum);
        Assert.Equal(TestValueEnum.Value1, obj.ValueEnum);
        Assert.Equal(TestMultiValueEnum.Value1, obj.MultiValueEnum);
    }
}
