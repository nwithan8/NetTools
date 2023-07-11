using System;
// using NetTools.Common.RegEx;
using Xunit;

namespace NetTools.Tools;

public class RegularExpressionsTest
{
    /*
    [Fact]
    public void TestRegularExpressionsMatches()
    {
        const string input = "This is a test string";
        const string pattern1 = "This is a test string";
        const string pattern2 = "is";

        var matches = RegularExpressions.Matches(input, pattern1);
        Assert.True(matches);

        matches = RegularExpressions.Matches(input, pattern2);
        Assert.True(matches);
    }

    [Fact]
    public void TestRegularExpressionsSubstrings()
    {
        const string input = "This is a test string";
        const string pattern = "is";

        var substrings = RegularExpressions.Substrings(input, pattern);
        Assert.Equal(2, substrings.Count);
    }

    [Fact]
    public void TestRegularExpressionsSubstringExists()
    {
        const string input = "This is a test string";
        const string pattern1 = "is";
        const string pattern2 = "absent";

        var substringExists = RegularExpressions.SubstringExists(input, pattern1);
        Assert.True(substringExists);

        substringExists = RegularExpressions.SubstringExists(input, pattern2);
        Assert.False(substringExists);
    }

    [Fact]
    public void TestRegularExpressionsReplace()
    {
        const string input = "This is a test string";
        const string pattern1 = "is";
        const string pattern2 = "IS";
        const string expectedOutput = "Thwas was a test string";

        var output = RegularExpressions.Replace(input, pattern1, "was");
        Assert.Equal(expectedOutput, output);

        output = RegularExpressions.Replace(input, pattern2, "was", false);
        Assert.Equal(input, output); // Should not replace anything

        output = RegularExpressions.Replace(input, pattern2, "was", true);
        Assert.Equal(expectedOutput, output);

        const string base64Pattern = "^(data:)([a-zA-Z0-9]+/[a-zA-Z0-9]+;)(base64,)";
        const string base64Portion = "dGVzdCBzdHJpbmc=";
        const string base64Input = $"data:image/png;base64,{base64Portion}";
        
        output = RegularExpressions.Replace(base64Input, base64Pattern, string.Empty, true);
        Assert.Equal(base64Portion, output);
    }
    */
}
