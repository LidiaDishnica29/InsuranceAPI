using Insurance.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Infrastructure.Context
{
    /// <summary>
    /// InsuranceDbContext.
    /// </summary>
    public class InsuranceDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InsuranceDbContext"/> class.
        /// </summary>
        /// <param name="options">options.</param>
        public InsuranceDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<ProductTypeWithSurcharge> ProductTypeWithSurcharge { get; set; }
    }
}
