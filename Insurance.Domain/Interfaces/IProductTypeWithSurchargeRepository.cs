using Insurance.Domain.Models;

namespace Insurance.Domain.Interfaces
{
    /// <summary>
    /// IProductTypeWithSurchargeRepository.
    /// </summary>
    public interface IProductTypeWithSurchargeRepository
    {
        /// <summary>
        /// GetAllroductTypeWithSurcharge.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<List<ProductTypeWithSurcharge>> GetAllProductTypeWithSurcharge();

        /// <summary>
        /// GetByProductTypeId.
        /// </summary>
        /// <param name="poductTypeId"></param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        Task<ProductTypeWithSurcharge> GetByProductTypeId(int poductTypeId);

        /// <summary>
        /// Add.
        /// </summary>
        /// <param name="productTypeWithSurcharge">productTypeWithSurcharge.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task<int> Add(ProductTypeWithSurcharge productTypeWithSurcharge);

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="productTypeWithSurcharge">productTypeWithSurcharge.</param>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        Task<int> Update(ProductTypeWithSurcharge productTypeWithSurcharge);
    }
}
