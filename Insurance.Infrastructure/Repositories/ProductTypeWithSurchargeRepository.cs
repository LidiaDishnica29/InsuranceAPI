using Insurance.Domain.Interfaces;
using Insurance.Domain.Models;
using Insurance.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Infrastructure.Repositories
{
    /// <summary>
    /// ProductTypeWithSurchargeRepository.
    /// </summary>
    public class ProductTypeWithSurchargeRepository : IProductTypeWithSurchargeRepository
    {
        private readonly InsuranceDbContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductTypeWithSurchargeRepository"/> class.
        /// </summary>
        /// <param name="context">context.</param>
        public ProductTypeWithSurchargeRepository(InsuranceDbContext context)
        {
            _dbContext = context;
        }

        /// <inheritdoc/>
        public async Task<List<ProductTypeWithSurcharge>> GetAllProductTypeWithSurcharge()
        {
            return await _dbContext.ProductTypeWithSurcharge.AsNoTracking().ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<ProductTypeWithSurcharge> GetByProductTypeId(int poductTypeId)
        {
            return await _dbContext.ProductTypeWithSurcharge.FirstOrDefaultAsync(b => b.ProductTypeId == poductTypeId);
        }

        /// <inheritdoc/>
        public async Task<int> Add(ProductTypeWithSurcharge productTypeWithSurcharge)
        {
            _dbContext.Add(productTypeWithSurcharge);
            return await SaveChanges();
        }

        /// <summary>
        /// Update.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<int> Update(ProductTypeWithSurcharge productTypeWithSurcharge)
        {
            _dbContext.Update(productTypeWithSurcharge);
            return await SaveChanges();
        }

        /// <summary>
        /// SaveChanges.
        /// </summary>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public async Task<int> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync();
        }

    }
}
