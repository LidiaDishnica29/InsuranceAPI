using Insurance.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Insurance.Api.Interfaces
{
    /// <summary>
    /// IProductAPIService.
    /// </summary>
    public interface IProductAPIService
    {
        /// <summary>
        /// GetProductTypes.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ProductType>> GetProductTypes();

        /// <summary>
        /// GetProductById.
        /// </summary>
        /// <param name="productId">productId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<Product> GetProductById(int? productId);

        /// <summary>
        /// GetProducts.
        /// </summary>
        /// <param name="ids">ids.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<Product>> GetProducts(List<int> ids = null);

        /// <summary>
        /// GetProductTypeById
        /// </summary>
        /// <param name="productTypeId">productTypeId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ProductType> GetProductTypeById(int? productTypeId);
    }
}
