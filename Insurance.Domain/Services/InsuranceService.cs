using AutoMapper;
using Insurance.Domain.DTOs;
using Insurance.Domain.Helpers;
using Insurance.Domain.Interfaces;
using Insurance.Domain.Models;

namespace Insurance.Domain.Services
{
    /// <summary>
    /// InsuranceService.
    /// </summary>
    public class InsuranceService : IInsuranceService
    {
        private readonly IProductAPIService _productAPIService;
        private readonly IProductTypeWithSurchargeService _productTypeWithSurchargeService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="InsuranceService"/> class.
        /// </summary>
        /// <param name="mapper">mapper.</param>
        /// <param name="productAPIService">productAPIService.</param>
        public InsuranceService(IProductAPIService productAPIService, IMapper mapper, IProductTypeWithSurchargeService productTypeWithSurcharge)
        {
            _productAPIService = productAPIService ?? throw new ArgumentNullException(nameof(productAPIService));
            _mapper = mapper;
            _productTypeWithSurchargeService = productTypeWithSurcharge;
        }

        /// <summary>
        /// GetInsuranceByProduct.
        /// </summary>
        /// <param name="productId">productId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<InsuranceDTO> GetInsuranceByProduct(int productId, string baseUrl)
        {
            Product product = await _productAPIService.GetProductById(productId, baseUrl);
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            product.ProductType = await _productAPIService.GetProductTypeById(product.ProductTypeId, baseUrl);

            if (product.ProductType == null || product.ProductTypeId == null)
            {
                throw new ArgumentNullException(nameof(ProductType));
            }

            ProductTypeWithSurchargeDTO productTypeWithSurcharge = await _productTypeWithSurchargeService.GetProductTypeWithSurcharge(product.ProductTypeId.Value);

            InsuranceDTO toInsure = _mapper.Map<InsuranceDTO>(product);

            double insurance = Utils.GetInsuranceValue(product, productTypeWithSurcharge?.Surcharge);

            insurance += Utils.CalculateDigitalCamera(product);

            toInsure.InsuranceValue = insurance;

            return toInsure;
        }

        /// <summary>
        /// GetInsuranceForProductsInShoppingCart.
        /// </summary>
        /// <param name="productIds">productIds.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<double?> GetInsuranceForProductsInShoppingCart(List<int> productIds, string baseUrl)
        {
            List<ProductType> productTypes = await _productAPIService.GetProductTypes(baseUrl);

            List<Product> products = await _productAPIService.GetProducts(baseUrl);

            // Take products and repeated ones of the orders
            if (productIds != null)
            {
                products = productIds
                .Join(products, id => id, product => product.Id, (id, product) => product)
                .ToList();
            }

            if (products == null || products?.Count == 0)
            {
                throw new ArgumentNullException(nameof(products));
            }

            double? totalInsuranceCost = 0;

            foreach (Product product in products)
            {
                product.ProductType = productTypes?.FirstOrDefault(i => i.Id == product.ProductTypeId);
                ProductTypeWithSurchargeDTO productTypeWithSurcharge = await _productTypeWithSurchargeService.GetProductTypeWithSurcharge(product.ProductTypeId.Value);

                totalInsuranceCost += Utils.GetInsuranceValue(product, productTypeWithSurcharge?.Surcharge);
            }

            // if producttypeId==33 for digital camera
            totalInsuranceCost += Utils.CalculateDigitalCamera(products);

            return totalInsuranceCost.Value;
        }
    }
}