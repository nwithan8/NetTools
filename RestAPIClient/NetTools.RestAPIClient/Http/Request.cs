using System.Collections.Specialized;
using System.Text;
using System.Web;
using NetTools.Common;
using NetTools.HTTP;
using NetTools.JSON;

namespace NetTools.RestAPIClient.Http
{
#pragma warning disable CA1001
#pragma warning disable CA1852 // Cannot be sealed because need virtual Dispose(bool)
    /// <summary>
    ///     Represents an HTTP request being sent to the EasyPost API.
    /// </summary>
    public class Request : IDisposable
#pragma warning restore CA1001
#pragma warning restore CA1852
    {
        /// <summary>
        ///     The <see cref="HttpRequestMessage"/> being executed by an <see cref="HttpClient"/>.
        /// </summary>
        private readonly HttpRequestMessage _requestMessage;

        /// <summary>
        ///     A <see cref="Dictionary{TKey,TValue}"/> of parameters to include in the HTTP request, either in the body or query.
        /// </summary>
        private readonly Dictionary<string, object> _parameters;

        /// <summary>
        ///     The <see cref="Method"/> being used for the HTTP request.
        /// </summary>
        private readonly Method _method;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Request"/> class.
        /// </summary>
        /// <param name="domain">The API domain for the HTTP request.</param>
        /// <param name="endpoint">The API endpoint for the HTTP request.</param>
        /// <param name="method">The <see cref="Method"/> for the HTTP request.</param>
        /// <param name="parameters">Optional parameters to include in the HTTP request.</param>
        /// <param name="headers">Optional headers to include in the HTTP request.</param>
        // ReSharper disable once SuggestBaseTypeForParameterInConstructor
        internal Request(string domain, string endpoint, Method method, Dictionary<string, object>? parameters = null, Dictionary<string, string>? headers = null)
        {
            // store method for later use
            _method = method;

            // build the request URL, then request message
            Uri url = new($"{domain.TrimEnd('/')}/{endpoint.TrimStart('/')}");
            _requestMessage = new HttpRequestMessage(_method.ToHttpMethod(), url);

            // store parameters for later use
            _parameters = parameters ?? new Dictionary<string, object>();

            // set the headers (user-agent, auth, and content-type should have been included in the headers dictionary)
            if (headers == null) return;
            foreach (KeyValuePair<string, string> header in headers)
            {
                _requestMessage.Headers.Add(header.Key, header.Value);
            }
        }

        /// <summary>
        ///     Construct an <see cref="HttpRequestMessage"/> from this <see cref="Request"/>'s configuration.
        /// </summary>
        /// <returns>A <see cref="HttpRequestMessage"/> with prepared parameters and headers.</returns>
        internal HttpRequestMessage AsHttpRequestMessage()
        {
            // wait until the last possible moment to set the content
            BuildParameters();

            return _requestMessage;
        }

        /// <summary>
        ///     Build the parameters, either as body parameters or query parameters depending on the <see cref="Method"/>.
        /// </summary>
        private void BuildParameters()
        {
            if (_parameters.Count == 0)
            {
                return;
            }

            var @switch = new SwitchCase
            {
                // equality of two HttpMethod objects falls back to their inner strings, so we can compare these objects directly
                { Method.Get.ToHttpMethod(), BuildQueryParameters },
                { Method.Delete.ToHttpMethod(), BuildQueryParameters },
                { Method.Post.ToHttpMethod(), BuildBodyParameters },
                { Method.Put.ToHttpMethod(), BuildBodyParameters },
                { Method.Patch.ToHttpMethod(), BuildBodyParameters },
            };

            @switch.MatchFirst(_method.ToHttpMethod());
        }

        /// <summary>
        ///     Build a request body and assign it to the <see cref="_requestMessage"/>.
        /// </summary>
        private void BuildBodyParameters()
        {
            var body = JsonSerialization.ConvertObjectToJson(_parameters);
            _requestMessage.Content = new StringContent(body, Encoding.UTF8, "application/json");
        }

        /// <summary>
        ///     Build a request query string and assign it to the <see cref="_requestMessage"/>.
        /// </summary>
        private void BuildQueryParameters()
        {
            // add query parameters
            NameValueCollection query = HttpUtility.ParseQueryString(string.Empty);

            // build query string from parameters
            foreach (KeyValuePair<string, object> param in _parameters)
            {
                // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
                if (param.Value == null)
                {
                    continue;
                }

                query[param.Key] = param.Value switch
                {
                    // TODO: Handle special conversions for other types
                    // DateTime dateTime => dateTime.ToString("o", CultureInfo.InvariantCulture),
                    var _ => param.Value.ToString(),
                };
            }

            // short circuit if no query parameters
            if (query.Count == 0)
            {
                return;
            }

            // rebuild the request URL with the query string appended
            var uriBuilder = new UriBuilder(_requestMessage.RequestUri!)
            {
                Query = query.ToString(),
            };
            _requestMessage.RequestUri = new Uri(uriBuilder.ToString());
        }

        /// <summary>
        ///     Whether this object has been disposed or not.
        /// </summary>
        private bool _isDisposed;

        /// <summary>
        ///     Dispose of this object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Dispose of this object.
        /// </summary>
        /// <param name="disposing">Whether this object is being disposed.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed) return;
            if (disposing)
            {
                // Dispose managed state (managed objects).

                // Dispose the request message
                _requestMessage.Dispose();
            }

            // Free native resources (unmanaged objects) and override a finalizer below.
            _isDisposed = true;
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="Request"/> class.
        /// </summary>
        ~Request()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(disposing: false);
        }
    }
}
