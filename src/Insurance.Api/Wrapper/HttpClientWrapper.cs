using System.Net.Http;
using System.Threading.Tasks;
using System;
using Insurance.Api.Wrapper;

namespace Insurance.Api.API
{
    /// <summary>
    /// HttpClientWrapper.
    /// </summary>
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpClientWrapper"/> class.
        /// </summary>
        /// <param name="client">client.</param>
        public HttpClientWrapper(HttpClient client)
        {
            _httpClient = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <inheritdoc/>
        public async Task<HttpResponseMessage> GetAsync(string apiUrl)
        {
            return await _httpClient.GetAsync(apiUrl);
        }
    }
}
