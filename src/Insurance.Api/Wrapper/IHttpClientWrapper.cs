using System.Net.Http;
using System.Threading.Tasks;

namespace Insurance.Api.Wrapper
{
    /// <summary>
    /// IHttpClientWrapper.
    /// </summary>
    public interface IHttpClientWrapper
    {
        /// <summary>
        /// GetAsync.
        /// </summary>
        /// <param name="apiUrl">apiUrl.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<HttpResponseMessage> GetAsync(string apiUrl);
    }
}
