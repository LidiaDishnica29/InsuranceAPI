
using Insurance.Domain.Exceptions;
using Insurance.Domain.Helpers;
using Insurance.Domain.Interfaces;
using Insurance.Domain.Models;
using System.Net.Http.Json;

namespace Insurance.Api.Services
{
    /// <summary>
    /// ProductAPIService.
    /// </summary>
    public class ProductAPIService : IProductAPIService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductAPIService"/> class.
        /// </summary>
        /// <param name="httpClientWrapper">httpClientWrapper.</param>
        public ProductAPIService(HttpClient client)
        {
            _httpClient = client;
        }

        /// <summary>
        /// GetProductTypes.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<List<ProductType>> GetProductTypes(string baseUrl)
        {
            List<ProductType> productTypes = new();

            string apiUrl = string.Format(Utils.GetProductsTypeConst, baseUrl);

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                productTypes = await response.Content.ReadFromJsonAsync<List<ProductType>>();
            }
            else
            {
                throw new HttpResponseException((int)response.StatusCode);
            }

            return productTypes;
        }

        /// <summary>
        /// GetProductById.
        /// </summary>
        /// <param name="productId">productId.</param>
        /// <param name="baseUrl">baseUrl.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<Product> GetProductById(int? productId, string baseUrl)
        {
            Product product = new();

            string apiUrl = string.Format(Utils.GetProductConst, baseUrl, productId);

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadFromJsonAsync<Product>();
            }
            else
            {
                throw new HttpResponseException((int)response.StatusCode);
            }

            return product;
        }

        /// <summary>
        /// GetProducts.
        /// </summary>
        /// <param name="baseUrl">baseUrl.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<List<Product>> GetProducts(string baseUrl)
        {
            List<Product> products = new();

            string apiUrl = string.Format(Utils.GetProductsConst, baseUrl);

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                products = await response.Content.ReadFromJsonAsync<List<Product>>();
            }
            else
            {
                throw new HttpResponseException((int)response.StatusCode);
            }

            return products;
        }

        /// <summary>
        /// GetProductTypeById.
        /// </summary>
        /// <param name="productTypeId">productTypeId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<ProductType> GetProductTypeById(int? productTypeId, string baseUrl)
        {
            ProductType productType = new();

            string apiUrl = string.Format(Utils.GetProductTypeConst, baseUrl, productTypeId);

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                productType = await response.Content.ReadFromJsonAsync<ProductType>();
            }
            else
            {
                throw new HttpResponseException((int)response.StatusCode);
            }

            return productType;
        }
    }
}
