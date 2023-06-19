using AutoMapper;
using Insurance.Api.Entities;
using Insurance.Api.Exceptions;
using Insurance.Api.Helpers;
using Insurance.Api.Interfaces;
using Insurance.Api.Wrapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Insurance.Api.Services
{
    /// <summary>
    /// ProductAPIService.
    /// </summary>
    public class ProductAPIService : IProductAPIService
    {
        private readonly ILogger<InsuranceService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientWrapper _httpWrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductAPIService"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <param name="configuration">configuration.</param>
        /// <param name="mapper">mapper.</param>
        /// <param name="httpClientWrapper">httpClientWrapper.</param>
        public ProductAPIService(ILogger<InsuranceService> logger, IConfiguration configuration, IMapper mapper, IHttpClientWrapper httpClientWrapper)
        {
            _logger = logger;
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpWrapper = httpClientWrapper;
        }

        /// <summary>
        /// GetProductTypes.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<List<ProductType>> GetProductTypes()
        {
            List<ProductType> productTypes = new();

            string apiUrl = string.Format(Utils.GetProductsTypeConst, _configuration.GetValue<string>("ProductApi"));

            HttpResponseMessage response = await _httpWrapper.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                productTypes = await response.Content.ReadFromJsonAsync<List<ProductType>>();
            }
            else
            {
                _logger.LogError(ErrorMessages.CANNOT_FETCH_DATA);
                throw new HttpResponseException((int)response.StatusCode);
            }

            return productTypes;
        }

        /// <summary>
        /// GetProductById.
        /// </summary>
        /// <param name="productId">productId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<Product> GetProductById(int? productId)
        {
            Product product = new();

            string apiUrl = string.Format(Utils.GetProductConst, _configuration.GetValue<string>("ProductApi"), productId);

            HttpResponseMessage response = await _httpWrapper.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadFromJsonAsync<Product>();
            }
            else
            {
                _logger.LogError(ErrorMessages.CANNOT_FETCH_DATA);
                throw new HttpResponseException((int)response.StatusCode);
            }

            return product;
        }

        /// <summary>
        /// GetProducts.
        /// </summary>
        /// <param name="ids">ids.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<List<Product>> GetProducts(List<int> ids = null)
        {
            List<Product> products = new();

            string apiUrl = string.Format(Utils.GetProductsConst, _configuration.GetValue<string>("ProductApi"));

            HttpResponseMessage response = await _httpWrapper.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                products = await response.Content.ReadFromJsonAsync<List<Product>>();
                if (ids != null)
                {
                    products = ids
                    .Join(products, id => id, product => product.Id, (id, product) => product)
                    .ToList();
                }
            }
            else
            {
                _logger.LogError(ErrorMessages.CANNOT_FETCH_DATA);
                throw new HttpResponseException((int)response.StatusCode);
            }

            return products;
        }

        /// <summary>
        /// GetProductTypeById.
        /// </summary>
        /// <param name="productTypeId">productTypeId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<ProductType> GetProductTypeById(int? productTypeId)
        {
            ProductType productType = new();

            string apiUrl = string.Format(Utils.GetProductTypeConst, _configuration.GetValue<string>("ProductApi"), productTypeId);

            HttpResponseMessage response = await _httpWrapper.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                productType = await response.Content.ReadFromJsonAsync<ProductType>();
            }
            else
            {
                _logger.LogError(ErrorMessages.CANNOT_FETCH_DATA);
                throw new HttpResponseException((int)response.StatusCode);
            }

            return productType;
        }
    }
}
