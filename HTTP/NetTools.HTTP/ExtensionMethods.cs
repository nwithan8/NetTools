using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace NetTools.HTTP;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public static class HttpStatusCodeExtensions
{
    /// <summary>
    ///     Return whether the given <see cref="HttpStatusCode"/> is a 1xx error.
    /// </summary>
    /// <param name="statusCode"><see cref="HttpStatusCode"/> to check.</param>
    /// <returns><c>true</c> if the <see cref="HttpStatusCode"/> is a 1xx error, <c>false</c> otherwise.</returns>
    public static bool Is1xx(this HttpStatusCode statusCode) => IsBetween(statusCode, 100, 199);

    /// <summary>
    ///     Return whether the given status code is a 1xx error.
    /// </summary>
    /// <param name="statusCode">Status code to check.</param>
    /// <returns><c>true</c> if the status code is a 1xx error, <c>false</c> otherwise.</returns>
    public static bool Is1xx(int statusCode) => IsBetween(statusCode, 100, 199);

    /// <summary>
    ///     Return whether the given <see cref="HttpStatusCode"/> is a 2xx error.
    /// </summary>
    /// <param name="statusCode"><see cref="HttpStatusCode"/> to check.</param>
    /// <returns><c>true</c> if the <see cref="HttpStatusCode"/> is a 2xx error, <c>false</c> otherwise.</returns>
    public static bool Is2xx(this HttpStatusCode statusCode) => IsBetween(statusCode, 200, 299);

    /// <summary>
    ///     Return whether the given status code is a 2xx error.
    /// </summary>
    /// <param name="statusCode">Status code to check.</param>
    /// <returns><c>true</c> if the status code is a 2xx error, <c>false</c> otherwise.</returns>
    public static bool Is2xx(int statusCode) => IsBetween(statusCode, 200, 299);

    /// <summary>
    ///     Return whether the given <see cref="HttpStatusCode"/> is a 3xx error.
    /// </summary>
    /// <param name="statusCode"><see cref="HttpStatusCode"/> to check.</param>
    /// <returns><c>true</c> if the <see cref="HttpStatusCode"/> is a 3xx error, <c>false</c> otherwise.</returns>
    public static bool Is3xx(this HttpStatusCode statusCode) => IsBetween(statusCode, 300, 399);

    /// <summary>
    ///     Return whether the given status code is a 3xx error.
    /// </summary>
    /// <param name="statusCode">Status code to check.</param>
    /// <returns><c>true</c> if the status code is a 3xx error, <c>false</c> otherwise.</returns>
    public static bool Is3xx(int statusCode) => IsBetween(statusCode, 300, 399);

    /// <summary>
    ///     Return whether the given <see cref="HttpStatusCode"/> is a 4xx error.
    /// </summary>
    /// <param name="statusCode"><see cref="HttpStatusCode"/> to check.</param>
    /// <returns><c>true</c> if the <see cref="HttpStatusCode"/> is a 4xx error, <c>false</c> otherwise.</returns>
    public static bool Is4xx(this HttpStatusCode statusCode) => IsBetween(statusCode, 400, 499);

    /// <summary>
    ///     Return whether the given status code is a 4xx error.
    /// </summary>
    /// <param name="statusCode">Status code to check.</param>
    /// <returns><c>true</c> if the status code is a 4xx error, <c>false</c> otherwise.</returns>
    public static bool Is4xx(int statusCode) => IsBetween(statusCode, 400, 499);

    /// <summary>
    ///     Return whether the given <see cref="HttpStatusCode"/> is a 5xx error.
    /// </summary>
    /// <param name="statusCode"><see cref="HttpStatusCode"/> to check.</param>
    /// <returns><c>true</c> if the <see cref="HttpStatusCode"/> is a 5xx error, <c>false</c> otherwise.</returns>
    public static bool Is5xx(this HttpStatusCode statusCode) => IsBetween(statusCode, 500, 599);

    /// <summary>
    ///     Return whether the given status code is a 5xx error.
    /// </summary>
    /// <param name="statusCode">Status code to check.</param>
    /// <returns><c>true</c> if the status code is a 5xx error, <c>false</c> otherwise.</returns>
    public static bool Is5xx(int statusCode) => IsBetween(statusCode, 500, 599);

    /// <summary>
    ///     Return whether the given <see cref="HttpStatusCode"/> is in the given range.
    /// </summary>
    /// <param name="statusCode"><see cref="HttpStatusCode"/> to check.</param>
    /// <param name="min">Minimum valid status code.</param>
    /// <param name="max">Maximum valid status code.</param>
    /// <returns>Whether the given <see cref="HttpStatusCode"/> is in the given range.</returns>
    public static bool IsBetween(this HttpStatusCode statusCode, int min, int max) =>
        IsBetween((int)statusCode, min, max);

    /// <summary>
    ///     Return whether the given status code is in the given range.
    /// </summary>
    /// <param name="statusCode">Status code to check.</param>
    /// <param name="min">Minimum valid status code.</param>
    /// <param name="max">Maximum valid status code.</param>
    /// <returns>Whether the given status code is in the given range.</returns>
    public static bool IsBetween(int statusCode, int min, int max) => statusCode >= min && statusCode <= max;
}

public static class HttpResponseMessageExtensions
{
    /// <summary>
    ///     Return whether the given <see cref="HttpResponseMessage"/> has a status code in the given range.
    /// </summary>
    /// <param name="response"><see cref="HttpResponseMessage"/> to check.</param>
    /// <param name="min">Minimum valid status code.</param>
    /// <param name="max">Maximum valid status code.</param>
    /// <returns>Whether the given <see cref="HttpResponseMessage"/> has a status code in the given range.</returns>
    public static bool HasStatusCodeBetween(this HttpResponseMessage response, int min, int max) =>
        response.StatusCode.IsBetween(min, max);

    /// <summary>
    ///     Return whether the given <see cref="HttpResponseMessage"/> has a successful status code.
    /// </summary>
    /// <param name="response"><see cref="HttpResponseMessage"/> to check.</param>
    /// <returns><c>true</c> if the <see cref="HttpResponseMessage"/> has a status code in the 200-299 range, <c>false</c> otherwise.</returns>
    public static bool ReturnedNoError(this HttpResponseMessage response) => response.StatusCode.Is2xx();

    /// <summary>
    ///     Return whether the given <see cref="HttpResponseMessage"/> has an error status code.
    /// </summary>
    /// <param name="response"><see cref="HttpResponseMessage"/> to check.</param>
    /// <returns><c>true</c> if the <see cref="HttpResponseMessage"/> has a status code not in the 200-299 range, <c>false</c> otherwise.</returns>
    public static bool ReturnedError(this HttpResponseMessage response) => !ReturnedNoError(response);
}