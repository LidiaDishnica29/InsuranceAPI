using Insurance.Domain.DTOs;
using Insurance.Domain.Models;

namespace Insurance.Domain.Interfaces
{
    /// <summary>
    /// IProductTypeWithSurchargeService.
    /// </summary>
    public interface IProductTypeWithSurchargeService
    {
        /// <summary>
        /// GetProductTypeWithSurcharge.
        /// </summary>
        /// <param name="productTypeId">productId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ProductTypeWithSurchargeDTO> GetProductTypeWithSurcharge(int productTypeId);

        /// <summary>
        /// Add ProductTypeWithSurcharge.
        /// </summary>
        /// <param name="productTypeWithSurcharge">productTypeWithSurcharge.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<int> AddProductTypeWithSurcharge(ProductTypeWithSurchargeDTO productTypeWithSurcharge);
    }
}
