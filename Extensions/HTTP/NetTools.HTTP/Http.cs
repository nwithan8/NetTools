// ReSharper disable InconsistentNaming

namespace NetTools.HTTP;

public static class Http
{
    /// <summary>
    ///     Return whether the given response has a status code in the given range.
    /// </summary>
    /// <param name="response">Response to check.</param>
    /// <param name="min">Minimum valid status code.</param>
    /// <param name="max">Maximum valid status code.</param>
    /// <returns>Whether the given response has a status code in the given range.</returns>
    public static bool HasStatusCodeBetween(this RestSharp.RestResponseBase response, int min, int max)
    {
        return StatusCodeBetween(response, min, max);
    }

    /// <summary>
    ///     Return whether the given status code is a 1xx error.
    /// </summary>
    /// <param name="statusCode">Status code to check.</param>
    /// <returns>True if the status code is a 1xx error, False otherwise.</returns>
    public static bool Is1xx(this System.Net.HttpStatusCode statusCode)
    {
        return StatusCodeIs1xx(statusCode);
    }

    /// <summary>
    ///     Return whether the given status code is a 2xx error.
    /// </summary>
    /// <param name="statusCode">Status code to check.</param>
    /// <returns>True if the status code is a 2xx error, False otherwise.</returns>
    public static bool Is2xx(this System.Net.HttpStatusCode statusCode)
    {
        return StatusCodeIs2xx(statusCode);
    }

    /// <summary>
    ///     Return whether the given status code is a 3xx error.
    /// </summary>
    /// <param name="statusCode">Status code to check.</param>
    /// <returns>True if the status code is a 3xx error, False otherwise.</returns>
    public static bool Is3xx(this System.Net.HttpStatusCode statusCode)
    {
        return StatusCodeIs3xx(statusCode);
    }

    /// <summary>
    ///     Return whether the given status code is a 4xx error.
    /// </summary>
    /// <param name="statusCode">Status code to check.</param>
    /// <returns>True if the status code is a 4xx error, False otherwise.</returns>
    public static bool Is4xx(this System.Net.HttpStatusCode statusCode)
    {
        return StatusCodeIs4xx(statusCode);
    }

    /// <summary>
    ///     Return whether the given status code is a 5xx error.
    /// </summary>
    /// <param name="statusCode">Status code to check.</param>
    /// <returns>True if the status code is a 5xx error, False otherwise.</returns>
    public static bool Is5xx(this System.Net.HttpStatusCode statusCode)
    {
        return StatusCodeIs5xx(statusCode);
    }

    /// <summary>
    ///     Return whether the given status code is in the given range.
    /// </summary>
    /// <param name="statusCode">Status code to check.</param>
    /// <param name="min">Minimum valid status code.</param>
    /// <param name="max">Maximum valid status code.</param>
    /// <returns>Whether the given status code is in the given range.</returns>
    public static bool IsBetween(this System.Net.HttpStatusCode statusCode, int min, int max)
    {
        return StatusCodeBetween(statusCode, min, max);
    }

    /// <summary>
    ///     Return whether the given response has an error status code.
    /// </summary>
    /// <param name="response">Response to check.</param>
    /// <returns>True if the response code is not in the 200-299 range, false otherwise.</returns>
    public static bool ReturnedError(this RestSharp.RestResponse response)
    {
        return !ReturnedNoError(response);
    }

    /// <summary>
    ///     Return whether the given response has a successful status code.
    /// </summary>
    /// <param name="response">Response to check.</param>
    /// <returns>True if the response code is in the 200-299 range, false otherwise.</returns>
    public static bool ReturnedNoError(this RestSharp.RestResponse response)
    {
        return response.StatusCode.Is2xx();
    }

    /// <summary>
    ///     Return whether the given status code is in the given range.
    /// </summary>
    /// <param name="statusCode">Status code to check.</param>
    /// <param name="min">Minimum valid status code.</param>
    /// <param name="max">Maximum valid status code.</param>
    /// <returns>Whether the given status code is in the given range.</returns>
    public static bool StatusCodeBetween(int statusCode, int min, int max)
    {
        return statusCode >= min && statusCode <= max;
    }

    /// <summary>
    ///     Return whether the given status code is in the given range.
    /// </summary>
    /// <param name="statusCode">Status code to check.</param>
    /// <param name="min">Minimum valid status code.</param>
    /// <param name="max">Maximum valid status code.</param>
    /// <returns>Whether the given status code is in the given range.</returns>
    public static bool StatusCodeBetween(System.Net.HttpStatusCode statusCode, int min, int max)
    {
        return StatusCodeBetween((int)statusCode, min, max);
    }

    /// <summary>
    ///     Return whether the given response has a status code in the given range.
    /// </summary>
    /// <param name="response">Response to check.</param>
    /// <param name="min">Minimum valid status code.</param>
    /// <param name="max">Maximum valid status code.</param>
    /// <returns>Whether the given response has a status code in the given range.</returns>
    public static bool StatusCodeBetween(RestSharp.RestResponseBase response, int min, int max)
    {
        return StatusCodeBetween(response.StatusCode, min, max);
    }

    /// <summary>
    ///     Return whether the given status code is a 1xx error.
    /// </summary>
    /// <param name="statusCode">Status code to check.</param>
    /// <returns>True if the status code is a 1xx error, False otherwise.</returns>
    public static bool StatusCodeIs1xx(int statusCode)
    {
        return StatusCodeBetween(statusCode, 100, 199);
    }

    /// <summary>
    ///     Return whether the given status code is a 1xx error.
    /// </summary>
    /// <param name="statusCode">Status code to check.</param>
    /// <returns>True if the status code is a 1xx error, False otherwise.</returns>
    public static bool StatusCodeIs1xx(System.Net.HttpStatusCode statusCode)
    {
        return StatusCodeBetween(statusCode, 100, 199);
    }

    /// <summary>
    ///     Return whether the given status code is a 2xx error.
    /// </summary>
    /// <param name="statusCode">Status code to check.</param>
    /// <returns>True if the status code is a 2xx error, False otherwise.</returns>
    public static bool StatusCodeIs2xx(int statusCode)
    {
        return StatusCodeBetween(statusCode, 200, 299);
    }

    /// <summary>
    ///     Return whether the given status code is a 2xx error.
    /// </summary>
    /// <param name="statusCode">Status code to check.</param>
    /// <returns>True if the status code is a 2xx error, False otherwise.</returns>
    public static bool StatusCodeIs2xx(System.Net.HttpStatusCode statusCode)
    {
        return StatusCodeBetween(statusCode, 200, 299);
    }

    /// <summary>
    ///     Return whether the given status code is a 3xx error.
    /// </summary>
    /// <param name="statusCode">Status code to check.</param>
    /// <returns>True if the status code is a 3xx error, False otherwise.</returns>
    public static bool StatusCodeIs3xx(int statusCode)
    {
        return StatusCodeBetween(statusCode, 300, 399);
    }

    /// <summary>
    ///     Return whether the given status code is a 3xx error.
    /// </summary>
    /// <param name="statusCode">Status code to check.</param>
    /// <returns>True if the status code is a 3xx error, False otherwise.</returns>
    public static bool StatusCodeIs3xx(System.Net.HttpStatusCode statusCode)
    {
        return StatusCodeBetween(statusCode, 300, 399);
    }

    /// <summary>
    ///     Return whether the given status code is a 4xx error.
    /// </summary>
    /// <param name="statusCode">Status code to check.</param>
    /// <returns>True if the status code is a 4xx error, False otherwise.</returns>
    public static bool StatusCodeIs4xx(int statusCode)
    {
        return StatusCodeBetween(statusCode, 400, 499);
    }

    /// <summary>
    ///     Return whether the given status code is a 4xx error.
    /// </summary>
    /// <param name="statusCode">Status code to check.</param>
    /// <returns>True if the status code is a 4xx error, False otherwise.</returns>
    public static bool StatusCodeIs4xx(System.Net.HttpStatusCode statusCode)
    {
        return StatusCodeBetween(statusCode, 400, 499);
    }

    /// <summary>
    ///     Return whether the given status code is a 5xx error.
    /// </summary>
    /// <param name="statusCode">Status code to check.</param>
    /// <returns>True if the status code is a 5xx error, False otherwise.</returns>
    public static bool StatusCodeIs5xx(int statusCode)
    {
        return StatusCodeBetween(statusCode, 500, 599);
    }

    /// <summary>
    ///     Return whether the given status code is a 5xx error.
    /// </summary>
    /// <param name="statusCode">Status code to check.</param>
    /// <returns>True if the status code is a 5xx error, False otherwise.</returns>
    public static bool StatusCodeIs5xx(System.Net.HttpStatusCode statusCode)
    {
        return StatusCodeBetween(statusCode, 500, 599);
    }
}
