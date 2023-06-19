using AutoMapper;
using Insurance.Api.DTOs;
using Insurance.Api.Entities;
using Insurance.Api.Exceptions;
using Insurance.Api.Helpers;
using Insurance.Api.Interfaces;
using Insurance.Api.Wrapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Threading.Tasks;

namespace Insurance.Api.Services
{
    /// <summary>
    /// InsuranceService.
    /// </summary>
    public class InsuranceService : IInsuranceService
    {
        private readonly ILogger<InsuranceService> _logger;
        private readonly IProductAPIService _productAPIService;

        // automapper
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="InsuranceService"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <param name="mapper">mapper.</param>
        /// <param name="productAPIService">productAPIService.</param>
        public InsuranceService(ILogger<InsuranceService> logger, IMapper mapper, IProductAPIService productAPIService)
        {
            _logger = logger;
            _productAPIService = productAPIService ?? throw new ArgumentNullException(nameof(productAPIService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// GetInsuranceByProduct.
        /// </summary>
        /// <param name="productId">productId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<InsuranceDTO> GetInsuranceByProduct(int productId)
        {
            Product product = await _productAPIService.GetProductById(productId);
            if (product == null)
            {
                _logger.LogError(ErrorMessages.NO_DATA_FOR_ID, productId);
                throw new ArgumentNullException(nameof(product));
            }

            if (product.ProductTypeId == null)
            {
                _logger.LogError(ErrorMessages.NO_DATA_FOR_ID, product.ProductTypeId);
                throw new ArgumentNullException(nameof(ProductType));
            }

            product.ProductType = await _productAPIService.GetProductTypeById(product.ProductTypeId);

            InsuranceDTO insurance = _mapper.Map<InsuranceDTO>(product);

            insurance = GetInsuranceValue(insurance);

            // to ask
            bool hasDigitalCamera = product.ProductTypeId == 33;
            insurance.InsuranceValue += hasDigitalCamera ? 500 : 0;

            return insurance;
        }

        /// <summary>
        /// GetInsuranceForProductsInShoppingCart.
        /// </summary>
        /// <param name="productIds">productIds.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<TotalInsuranceCostOrderDTO> GetInsuranceForProductsInShoppingCart(List<int> productIds)
        {
            List<InsuranceDTO> insuranceList = new();
            List<ProductType> productTypes = await _productAPIService.GetProductTypes();

            List<Product> products = await _productAPIService.GetProducts(productIds);
            if (products == null || products?.Count == 0)
            {
                _logger.LogError(ErrorMessages.NO_DATA);
                throw new ArgumentNullException(nameof(products));
            }

            float? totalInsuranceCost = 0;

            // ask foreach one will be addeed the insurance??? for example 2 laptops etc
            foreach (Product product in products)
            {
                product.ProductType = productTypes?.FirstOrDefault(i => i.Id == product.ProductTypeId);
                InsuranceDTO insurance = _mapper.Map<InsuranceDTO>(product);
                totalInsuranceCost += GetInsuranceValue(insurance).InsuranceValue;
                insuranceList.Add(insurance);
            }

            // if producttypeId==33 for digital camera
            bool hasAnyDigitalCamera = products.Any(p => p.ProductTypeId == 33);
            totalInsuranceCost = hasAnyDigitalCamera ? totalInsuranceCost + 500 : totalInsuranceCost;

            TotalInsuranceCostOrderDTO totalInsuranceOrder = new()
            {
                TotalInsuranceCostOrder = totalInsuranceCost.Value,
                InsuranceDTO = insuranceList,
            };

            return totalInsuranceOrder;
        }

        /// <summary>
        /// CalculateSurcharge.
        /// </summary>
        /// <param name="productTypeId">productTypeId.</param>
        /// <param name="surcharge">surcharge.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<float> CalculateSurcharge(int productTypeId, float surcharge)
        {
            float surchangeValue = 1;
            List<ProductTypeWithSurchargeDTO> productTypeWithSurchargeDTOs = new();
            List<Product> products = await _productAPIService.GetProducts();
            foreach (Product product in products.Where(i => i.ProductTypeId == productTypeId))
            {
                ProductTypeWithSurchargeDTO productTypeWithSurchargeDTO = new()
                {
                    Surcharge = (float)product.SalesPrice * surcharge,
                    ProductTypeId = productTypeId,
                    ProductId = product.Id,
                };
                productTypeWithSurchargeDTOs.Add(productTypeWithSurchargeDTO);
            }

            return surchangeValue;
        }

        private static InsuranceDTO GetInsuranceValue(InsuranceDTO insurance)
        {
            insurance.InsuranceValue = 0;

            if (!insurance.ProductTypeHasInsurance)
            {
                return insurance;
            }

            if (insurance.SalesPrice >= 500)
            {
                switch (insurance.SalesPrice)
                {
                    case < 2000:
                        insurance.InsuranceValue += 1000;
                        break;
                    case >= 2000:
                        insurance.InsuranceValue += 2000;
                        break;
                }
            }

            // 21 id is for laptops insured and 32 id is for smartphones insured
            if (insurance.ProductTypeId == 21 || insurance.ProductTypeId == 32)
            {
                insurance.InsuranceValue += 500;
            }

            return insurance;
        }
    }
}