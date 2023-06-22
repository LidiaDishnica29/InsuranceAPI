using AutoMapper;
using Insurance.Api.Services;
using Insurance.Domain.DTOs;
using Insurance.Domain.Interfaces;
using Insurance.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Domain.Services
{
    /// <summary>
    /// ProductTypeWithSurchargeService.
    /// </summary>
    public class ProductTypeWithSurchargeService : IProductTypeWithSurchargeService
    {
        private readonly IProductTypeWithSurchargeRepository _productTypeWithSurchargeRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductTypeWithSurchargeService"/> class.
        /// </summary>
        /// <param name="productTypeWithSurcharge">productTypeWithSurcharge.</param>
        public ProductTypeWithSurchargeService(IProductTypeWithSurchargeRepository productTypeWithSurcharge, IMapper mapper)
        {
            _productTypeWithSurchargeRepository = productTypeWithSurcharge ?? throw new ArgumentNullException(nameof(productTypeWithSurcharge));
            _mapper = mapper;
        }

        /// <summary>
        /// GetProductTypeWithSurcharge.
        /// </summary>
        /// <param name="productTypeId">productId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<ProductTypeWithSurchargeDTO> GetProductTypeWithSurcharge(int productTypeId)
        {
            ProductTypeWithSurcharge productTypeWithSurcharge = await _productTypeWithSurchargeRepository.GetByProductTypeId(productTypeId);
            ProductTypeWithSurchargeDTO productTypeWithSurchargeDTO = _mapper.Map<ProductTypeWithSurchargeDTO>(productTypeWithSurcharge);
            return productTypeWithSurchargeDTO;
        }

        /// <summary>
        /// Add ProductTypeWithSurcharge.
        /// </summary>
        /// <param name="productTypeId">productId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<int> AddProductTypeWithSurcharge(ProductTypeWithSurchargeDTO productTypeWithSurcharge)
        {
            if (productTypeWithSurcharge == null)
            {
                throw new ArgumentNullException(nameof(productTypeWithSurcharge));
            }

            ProductTypeWithSurcharge productTypeWithSurchargeModel = _mapper.Map<ProductTypeWithSurcharge>(productTypeWithSurcharge);

            ProductTypeWithSurcharge productTypeWithSurchExist = await _productTypeWithSurchargeRepository.GetByProductTypeId(productTypeWithSurcharge.ProductTypeId);

            if (productTypeWithSurchExist != null)
            {
                productTypeWithSurchExist.Surcharge = productTypeWithSurchargeModel.Surcharge;
                return await _productTypeWithSurchargeRepository.Update(productTypeWithSurchExist);
            }

            return await _productTypeWithSurchargeRepository.Add(productTypeWithSurchargeModel);
        }
    }
}
