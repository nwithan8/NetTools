// ReSharper disable InconsistentNaming

using RestSharp;

namespace NetTools.HTTP.RestSharp;

public static class StatusCodes
{
    /// <summary>
    ///     Return whether the given response has a status code in the given range.
    /// </summary>
    /// <param name="response">Response to check.</param>
    /// <param name="min">Minimum valid status code.</param>
    /// <param name="max">Maximum valid status code.</param>
    /// <returns>Whether the given response has a status code in the given range.</returns>
    public static bool HasStatusCodeBetween(this RestResponseBase response, int min, int max)
    {
        return StatusCodeBetween(response, min, max);
    }

    /// <summary>
    ///     Return whether the given response has an error status code.
    /// </summary>
    /// <param name="response">Response to check.</param>
    /// <returns>True if the response code is not in the 200-299 range, false otherwise.</returns>
    public static bool ReturnedError(this RestResponseBase response)
    {
        return !ReturnedNoError(response);
    }

    /// <summary>
    ///     Return whether the given response has a successful status code.
    /// </summary>
    /// <param name="response">Response to check.</param>
    /// <returns>True if the response code is in the 200-299 range, false otherwise.</returns>
    public static bool ReturnedNoError(this RestResponseBase response)
    {
        return response.StatusCode.Is2xx();
    }

    /// <summary>
    ///     Return whether the given response has a status code in the given range.
    /// </summary>
    /// <param name="response">Response to check.</param>
    /// <param name="min">Minimum valid status code.</param>
    /// <param name="max">Maximum valid status code.</param>
    /// <returns>Whether the given response has a status code in the given range.</returns>
    public static bool StatusCodeBetween(RestResponseBase response, int min, int max)
    {
        return NetTools.HTTP.StatusCodes.StatusCodeBetween(response.StatusCode, min, max);
    }

    /// <summary>
    ///     Return whether the given response has a status code that is a 1xx code.
    /// </summary>
    /// <param name="response">Response to check.</param>
    /// <returns>True if the status code is a 1xx code, False otherwise.</returns>
    public static bool StatusCodeIs1xx(RestResponseBase response)
    {
        return StatusCodeBetween(response, 100, 199);
    }

    /// <summary>
    ///     Return whether the given response has a status code that is a 2xx code.
    /// </summary>
    /// <param name="response">Response to check.</param>
    /// <returns>True if the status code is a 2xx code, False otherwise.</returns>
    public static bool StatusCodeIs2xx(RestResponseBase response)
    {
        return StatusCodeBetween(response, 200, 299);
    }

    /// <summary>
    ///     Return whether the given response has a status code that is a 3xx code.
    /// </summary>
    /// <param name="response">Response to check.</param>
    /// <returns>True if the status code is a 3xx code, False otherwise.</returns>
    public static bool StatusCodeIs3xx(RestResponseBase response)
    {
        return StatusCodeBetween(response, 300, 399);
    }

    /// <summary>
    ///     Return whether the given response has a status code that is a 4xx code.
    /// </summary>
    /// <param name="response">Response to check.</param>
    /// <returns>True if the status code is a 4xx code, False otherwise.</returns>
    public static bool StatusCodeIs4xx(RestResponseBase response)
    {
        return StatusCodeBetween(response, 400, 499);
    }

    /// <summary>
    ///     Return whether the given response has a status code that is a 5xx code.
    /// </summary>
    /// <param name="response">Response to check.</param>
    /// <returns>True if the status code is a 5xx code, False otherwise.</returns>
    public static bool StatusCodeIs5xx(RestResponseBase response)
    {
        return StatusCodeBetween(response, 500, 599);
    }
}
