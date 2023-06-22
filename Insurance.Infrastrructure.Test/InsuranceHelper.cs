using Insurance.Domain.Models;
using Insurance.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Infrastrructure.Test
{
    public static class InsuranceHelper
    {

        /// <summary>
        /// InsuranceDbContextOptionsEfCoreInMemory
        /// </summary>
        public static DbContextOptions<InsuranceDbContext> InsuranceDbContextOptions()
        {
            var options = new DbContextOptionsBuilder<InsuranceDbContext>()
                .UseInMemoryDatabase($"InsuranceDbContextDatabase{Guid.NewGuid()}")
                .Options;

            return options;
        }

        /// <summary>
        /// CreateInMemoryDb
        /// </summary>
        public static async void CreateInMemoryDb(DbContextOptions<InsuranceDbContext> options)
        {
            await using var context = new InsuranceDbContext(options);
            CreateInMemoryData(context);
        }

        public static async Task RemoveData(DbContextOptions<InsuranceDbContext> options)
        {
            await using var context = new InsuranceDbContext(options);
            foreach (var productTypeWithSurcharge in context.ProductTypeWithSurcharge)
            {
                context.ProductTypeWithSurcharge.Remove(productTypeWithSurcharge);
            }
            await context.SaveChangesAsync();
        }

        private static void CreateInMemoryData(InsuranceDbContext insuranceDb)
        {
            insuranceDb.ProductTypeWithSurcharge.Add(new ProductTypeWithSurcharge { ProductTypeId = 21, Surcharge = 10 });
            insuranceDb.ProductTypeWithSurcharge.Add(new ProductTypeWithSurcharge { ProductTypeId = 22, Surcharge = 40 });
            insuranceDb.ProductTypeWithSurcharge.Add(new ProductTypeWithSurcharge { ProductTypeId = 33, Surcharge = 5 });
            insuranceDb.ProductTypeWithSurcharge.Add(new ProductTypeWithSurcharge { ProductTypeId = 32, Surcharge = 21 });

            insuranceDb.SaveChangesAsync();
        }
    }
}
