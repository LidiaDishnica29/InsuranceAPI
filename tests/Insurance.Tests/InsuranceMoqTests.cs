using AutoMapper;
using Insurance.Api.DTOs;
using Insurance.Api.Entities;
using Insurance.Api.Interfaces;
using Insurance.Api.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;

namespace Insurance.Tests
{
    public class InsuranceMoqTests
    {
        private readonly Mock<IProductAPIService> _productAPIServiceMock;
        private readonly InsuranceService _insuranceService;
        private readonly Mock<ILogger<InsuranceService>> _logger;
        private readonly Mock<IMapper> _mapper;

        public InsuranceMoqTests()
        {
            _mapper = new Mock<IMapper>();
            _logger = new Mock<ILogger<InsuranceService>>();
            _productAPIServiceMock = new Mock<IProductAPIService>();
            _insuranceService = new InsuranceService(_logger.Object, _mapper.Object, _productAPIServiceMock.Object);
        }
        [Fact]
        public async void GetAll_ShouldReturnNull_WhenProductDoesntExist()
        {
            List<int> ids = new List<int>() { 1111, 1112 };
            _productAPIServiceMock.Setup(i => i.GetProductTypes()).ReturnsAsync(CreateProductTypeList());
            _productAPIServiceMock.Setup(i => i.GetProducts(ids)).ReturnsAsync(CreateProductList());
            var product = CreateProductList().FirstOrDefault();
            _mapper.Setup(m => m.Map<InsuranceDTO>(
             It.IsAny<Product>())).Returns(MapModelToInsuranceDTO(product));
            var result = await _insuranceService.GetInsuranceForProductsInShoppingCart(ids);

            Assert.NotNull(result);
            Assert.IsType<TotalInsuranceCostOrderDTO>(result);
        }

        [Fact]
        public async void GetById_ShouldReturnInsurance()
        {

            var result = await _insuranceService.GetInsuranceByProduct(735246);

            Assert.NotNull(result);
            Assert.IsType<InsuranceDTO>(result);
        }
        private List<Product> CreateProductList()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = 321,
                    Name = "Iphone",
                 ProductTypeId=32,
                  SalesPrice=500,

                },
              new Product()
                {
                    Id = 3222,
                    Name = "Iphone",
                      ProductTypeId=22,
                  SalesPrice=500,
                },
              new Product()
                {
                  Id = 1111,
                  Name = "Laptops",
                  ProductTypeId=22,
                  SalesPrice=500,
                },
               new Product()

              {
                  Id = 1112,
                  Name = "Digital Camera",
                  ProductTypeId=33,
                  SalesPrice=5100,
                },
            };
        }
        private List<ProductType> CreateProductTypeList()
        {
            return new List<ProductType>()
            {
                new ProductType()
                {
                    Id = 33,
                    Name = "",
                    CanBeInsured = true,

                },
              new ProductType()
                {
                    Id = 32,
                    Name = "",
                    CanBeInsured = true,

                },
              new ProductType()
                {
                    Id = 22,
                    Name = "",
                    CanBeInsured = true,

                },
            };
        }

        private InsuranceDTO MapModelToInsuranceDTO(Product product)
        {
            var insuranceDTO = new InsuranceDTO()
            {
                ProductId = product.Id,
                ProductName = product.Name,
                ProductTypeHasInsurance = true,
                SalesPrice = product.SalesPrice
            };
            return insuranceDTO;
        }

    }
}
