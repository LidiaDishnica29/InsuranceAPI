using Insurance.Domain.Models;

namespace Insurance.Domain.Interfaces
{
    /// <summary>
    /// IProductAPIService.
    /// </summary>
    public interface IProductAPIService
    {
        /// <summary>
        /// GetProductTypes.
        /// </summary>
        /// <param name="baseUrl">baseUrl.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ProductType>> GetProductTypes(string baseUrl);

        /// <summary>
        /// GetProductById.
        /// </summary>
        /// <param name="productId">productId.</param>
        /// <param name="baseUrl">baseUrl.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<Product> GetProductById(int? productId, string baseUrl);

        /// <summary>
        /// GetProducts.
        /// </summary>
        /// <param name="baseUrl">ids.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<Product>> GetProducts(string baseUrl);

        /// <summary>
        /// GetProductTypeById.
        /// </summary>
        /// <param name="productTypeId">productTypeId.</param>
        /// <param name="baseUrl">baseUrl.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ProductType> GetProductTypeById(int? productTypeId, string baseUrl);
    }
}
