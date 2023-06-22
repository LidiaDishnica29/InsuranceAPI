using Insurance.Domain.DTOs;

namespace Insurance.Domain.Interfaces
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
        Task<InsuranceDTO> GetInsuranceByProduct(int productId, string baseUrl);

        /// <summary>
        /// GetInsuranceForProductsInShoppingCart
        /// </summary>
        /// <param name="productIds">productIds</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<double?> GetInsuranceForProductsInShoppingCart(List<int> productIds, string baseUrl);
    }
}
