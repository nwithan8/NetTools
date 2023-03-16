using NetTools.Common;
using Xunit;

namespace NetTools.Tools;

public class ValidationExtensionMethodsTest
{
    [Fact]
    public void AtLeastOneExistsTest()
    {
        var result = new object?[] { null, null, null }.AtLeastOneExists();
        
        Assert.False(result);
        
        result = new object?[] { null, null, new object() }.AtLeastOneExists();
        
        Assert.True(result);
        
        result = new object?[] { null, new object(), new object() }.AtLeastOneExists();
        
        Assert.True(result);
    }

    [Fact]
    public void AtLeastOneDoesNotExistTest()
    {
        var result = new object?[] { null, null, null }.AtLeastOneDoesNotExist();
        
        Assert.True(result);
        
        result = new object?[] { null, null, new object() }.AtLeastOneDoesNotExist();
        
        Assert.True(result);
        
        result = new object?[] { new object(), new object(), new object() }.AtLeastOneDoesNotExist();
        
        Assert.False(result);
    }

    [Fact]
    public void AtMostOneExistsTest()
    {
        var result = new object?[] { null, null, null }.AtMostOneExists();
        
        Assert.True(result);
        
        result = new object?[] { null, null, new object() }.AtMostOneExists();
        
        Assert.True(result);
        
        result = new object?[] { null, new object(), new object() }.AtMostOneExists();
        
        Assert.False(result);
    }
    
    [Fact]
    public void AtMostOneDoesNotExistTest()
    {
        var result = new object?[] { null, null, null }.AtMostOneDoesNotExist();
        
        Assert.False(result);
        
        result = new object?[] { null, null, new object() }.AtMostOneDoesNotExist();
        
        Assert.False(result);
        
        result = new object?[] { null, new object(), new object() }.AtMostOneDoesNotExist();
        
        Assert.True(result);
    }

    [Fact]
    public void ExactlyOneExistsTest()
    {
        var result = new object?[] { null, null, null }.ExactlyOneExists();
        
        Assert.False(result);
        
        result = new object?[] { null, null, new object() }.ExactlyOneExists();
        
        Assert.True(result);
        
        result = new object?[] { null, new object(), new object() }.ExactlyOneExists();
        
        Assert.False(result);
    }
    
    [Fact]
    public void ExactlyOneDoesNotExistTest()
    {
        var result = new object?[] { null, null, null }.ExactlyOneDoesNotExist();
        
        Assert.False(result);
        
        result = new object?[] { new object(), new object(), new object() }.ExactlyOneDoesNotExist();
        
        Assert.False(result);
        
        result = new object?[] { null, new object(), new object() }.ExactlyOneDoesNotExist();
        
        Assert.True(result);
    }

    [Fact]
    public void AnyExistTest()
    {
        var result = new object?[] { null, null, null }.AnyExist();
        
        Assert.False(result);
        
        result = new object?[] { null, null, new object() }.AnyExist();
        
        Assert.True(result);
        
        result = new object?[] { new object(), new object(), new object() }.AnyExist();
        
        Assert.True(result);
    }
    
    [Fact]
    public void AnyDoNotExistTest()
    {
        var result = new object?[] { null, null, null }.AnyDoNotExist();
        
        Assert.True(result);
        
        result = new object?[] { null, null, new object() }.AnyDoNotExist();
        
        Assert.True(result);
        
        result = new object?[] { new object(), new object(), new object() }.AnyDoNotExist();
        
        Assert.False(result);
    }
    
    [Fact]
    public void AllExistTest()
    {
        var result = new object?[] { null, null, null }.AllExist();
        
        Assert.False(result);
        
        result = new object?[] { null, null, new object() }.AllExist();
        
        Assert.False(result);
        
        result = new object?[] { new object(), new object(), new object() }.AllExist();
        
        Assert.True(result);
    }
    
    [Fact]
    public void NoneExistTest()
    {
        var result = new object?[] { null, null, null }.NoneExist();
        
        Assert.True(result);
        
        result = new object?[] { null, null, new object() }.NoneExist();
        
        Assert.False(result);
        
        result = new object?[] { new object(), new object(), new object() }.NoneExist();
        
        Assert.False(result);
    }
}
