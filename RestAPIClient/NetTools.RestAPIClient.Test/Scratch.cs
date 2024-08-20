using System;
using System.Threading.Tasks;
using NetTools.Common.Attributes;
using NetTools.HTTP;
using NetTools.RestAPIClient.Parameters;
using Newtonsoft.Json;
using Xunit;

namespace NetTools.RestAPIClient.Test;

public class Scratch
{
    public static int PreRequestCallbackCallCount = 0;
    public static int PostRequestCallbackCallCount = 0;
    public static Guid RequestGuid = Guid.Empty;
    
    public class MyApiResponseObject : BaseObject
    {
        [JsonProperty("id")] public string Id { get; set; }
    }

    public class MyApiRequestParameters : BaseParameters
    {
        [TopLevelRequestParameter(Necessity.Optional, "top")]
        public string Top { get; set; }
    }

    public class MyClientConfiguration : BaseClientConfiguration
    {
        public string ExtraVariable { get; set; }

        public MyClientConfiguration(string extraVariable, IAuthentication authentication) : base(authentication)
        {
            ExtraVariable = extraVariable;
            Hooks = new Hooks
            {
                OnRequestExecuting = (_, args) =>
                {
                    // Modifying the HttpRequestMessage in this action does not impact the HttpRequestMessage being executed (passed by value, not reference)
                    PreRequestCallbackCallCount++;
                    Assert.True(args.RequestTimestamp > 0);
                    RequestGuid = args.Id;
                },
                OnRequestResponseReceived = (_, args) =>
                {
                    PostRequestCallbackCallCount++;
                    Assert.True(args.RequestTimestamp > 0);
                    Assert.True(args.ResponseTimestamp > 0);
                    Assert.True(args.ResponseTimestamp >= args.RequestTimestamp);
                    Assert.Equal(RequestGuid, args.Id);
                },
            };
        }
    }

    public class MyClient : BaseClient
    {
        public MyClient(BaseClientConfiguration configuration) : base(configuration)
        {
        }

        [CrudOperations.Read]
        public async Task<MyApiResponseObject> GetMyObjectAsync(MyApiRequestParameters parameters)
        {
            return await RequestAsync<MyApiResponseObject>(NetTools.HTTP.Method.Get, "/my-object",
                parameters);
        }
    }

    [Fact]
    public async Task ScratchTest()
    {
        var config = new MyClientConfiguration("extra", new UsernamePasswordAuthentication("user", "pass"))
        {
            ApiBase = "https://google.com",
        };

        var client = new MyClient(config);

        var parameters = new MyApiRequestParameters
        {
            Top = "top",
        };

        var result = await client.GetMyObjectAsync(parameters);
    }
}