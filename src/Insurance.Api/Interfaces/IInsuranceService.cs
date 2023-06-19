using Insurance.Api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Api.Interfaces
{
    /// <summary>
    /// IInsuranceService.
    /// </summary>
    public interface IInsuranceService
    {
        /// <summary>
        /// GetInsuranceByProduct.
        /// </summary>
        /// <param name="productId">productId</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<InsuranceDTO> GetInsuranceByProduct(int productId);

        /// <summary>
        /// GetInsuranceForProductsInShoppingCart
        /// </summary>
        /// <param name="productIds">productIds</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<TotalInsuranceCostOrderDTO> GetInsuranceForProductsInShoppingCart(List<int> productIds);
    }
}
