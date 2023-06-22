namespace Insurance.Domain.Exceptions
{
    /// <summary>
    /// HttpResponseException.
    /// </summary>
    public class HttpResponseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseException"/> class.
        /// </summary>
        /// <param name="statusCode">statusCode</param>
        public HttpResponseException(int statusCode) =>
            StatusCode = statusCode;

        /// <summary>
        /// Gets statusCode.
        /// </summary>
        public int StatusCode { get; }
    }
}
