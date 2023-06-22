using Insurance.Domain.Models;
using Insurance.Infrastructure.Context;
using Insurance.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Infrastrructure.Test
{
    public class ProductTypeWithSurchargeRepositoryTests
    {

        private readonly DbContextOptions<InsuranceDbContext> _options;

        public ProductTypeWithSurchargeRepositoryTests()
        {
            _options = InsuranceHelper.InsuranceDbContextOptions();
            InsuranceHelper.CreateInMemoryDb(_options);
        }

        [Fact]
        public async void GetAllProductTypeWithSurcharge_ShouldReturnEmptyList_WhenEmptyDatabase()
        {
            await InsuranceHelper.RemoveData(_options);

            await using var context = new InsuranceDbContext(_options);
            var repository = new ProductTypeWithSurchargeRepository(context);
            var productTypeWithSurcharges = await repository.GetAllProductTypeWithSurcharge();

            Assert.NotNull(productTypeWithSurcharges);
            Assert.Empty(productTypeWithSurcharges);
        }
        [Fact]
        public async void GetAllProductTypeWithSurcharge_ShouldReturnList()
        {
            await using var context = new InsuranceDbContext(_options);
            var repository = new ProductTypeWithSurchargeRepository(context);
            var productTypeWithSurcharges = await repository.GetAllProductTypeWithSurcharge();

            Assert.NotNull(productTypeWithSurcharges);
            Assert.NotEmpty(productTypeWithSurcharges);
            Assert.IsType<List<ProductTypeWithSurcharge>>(productTypeWithSurcharges);
        }

        [Fact]
        public async void GetProductTypeWithSurchargeByProductTypeId_ShouldReturSurcharge_Equal()
        {
            await using var context = new InsuranceDbContext(_options);
            var repository = new ProductTypeWithSurchargeRepository(context);
            ProductTypeWithSurcharge typeWithSurcharge = CreateProductTypeWithSurcharge();
            var productTypeWithSurcharges = await repository.GetByProductTypeId(typeWithSurcharge.ProductTypeId);

            Assert.NotNull(productTypeWithSurcharges);
            Assert.Equal(typeWithSurcharge.Surcharge, productTypeWithSurcharges.Surcharge);
        }

        [Fact]
        public async void GetProductTypeWithSurcharge_ShouldReturnNull_WhenByProductTypeId_NotExist()
        {
            int productTypeId = 49;
            await using var context = new InsuranceDbContext(_options);
            var repository = new ProductTypeWithSurchargeRepository(context);
            var productTypeWithSurcharges = await repository.GetByProductTypeId(productTypeId);

            Assert.Null(productTypeWithSurcharges);
        }
      
        [Fact]
        public async void AddSurcharge_ShouldAddSurcharge_WhenNotProductTypeIdAddedBefore()
        {
            ProductTypeWithSurcharge productTypeWithSurcharge = new();

            await using (InsuranceDbContext context = new InsuranceDbContext(_options))
            {
                productTypeWithSurcharge = new ProductTypeWithSurcharge()
                {
                    Id = 29,
                    ProductTypeId = 55,
                    Surcharge = 23
                };

                var repository = new ProductTypeWithSurchargeRepository(context);
                await repository.Add(productTypeWithSurcharge);
            }
            await using (var context = new InsuranceDbContext(_options))
            {
                var result = await context.ProductTypeWithSurcharge.Where(b => b.Id == productTypeWithSurcharge.Id).FirstOrDefaultAsync();

                Assert.NotNull(result);
                Assert.IsType<ProductTypeWithSurcharge>(productTypeWithSurcharge);
                Assert.Equal(productTypeWithSurcharge.Id, result.Id);
                Assert.Equal(productTypeWithSurcharge.Surcharge, result.Surcharge);
            }
        }
      
        [Fact]
        public async void UpdateSurcharge_ShouldAddSurcharge_WhenProductTypeIdAddedBefore()
        {
            ProductTypeWithSurcharge productTypeWithSurcharge = new();
            await using (var context = new InsuranceDbContext(_options))
            {
                productTypeWithSurcharge = await context.ProductTypeWithSurcharge.FirstOrDefaultAsync(b => b.ProductTypeId == 22);
                productTypeWithSurcharge.Surcharge =98;
            }

            await using (InsuranceDbContext context = new InsuranceDbContext(_options))
            {
                var repository = new ProductTypeWithSurchargeRepository(context);
                await repository.Update(productTypeWithSurcharge);
            }
            await using (var context = new InsuranceDbContext(_options))
            {
                var result = await context.ProductTypeWithSurcharge.FirstOrDefaultAsync(b => b.ProductTypeId == productTypeWithSurcharge.ProductTypeId);

                Assert.NotNull(result);
                Assert.IsType<ProductTypeWithSurcharge>(productTypeWithSurcharge);
                Assert.Equal(productTypeWithSurcharge.ProductTypeId, result.ProductTypeId);
                Assert.Equal(productTypeWithSurcharge.Id, result.Id);
                Assert.Equal(productTypeWithSurcharge.Surcharge, result.Surcharge);
            }
        }

        private ProductTypeWithSurcharge CreateProductTypeWithSurcharge()
        {
            return new ProductTypeWithSurcharge()
            {
                Id = 2,
                Surcharge = 40,
                ProductTypeId = 22
            };
        }
    }
}