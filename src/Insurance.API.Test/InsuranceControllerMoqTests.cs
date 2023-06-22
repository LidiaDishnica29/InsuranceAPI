using Insurance.Api.Controllers;
using Insurance.Domain.DTOs;
using Insurance.Domain.Interfaces;
using Insurance.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Insurance.API.Test
{
    public class InsuranceControllerMoqTests
    {
        private readonly InsuranceController _insuranceController;
        private readonly Mock<IInsuranceService> _iInsuranceServiceMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<IConfigurationSection> _configurationSection;
        private readonly Mock<IProductTypeWithSurchargeService> _productTypeWithSurchargeService;

        public InsuranceControllerMoqTests()
        {
            _iInsuranceServiceMock = new Mock<IInsuranceService>();
            _configurationMock = new Mock<IConfiguration>();
            _configurationSection = new Mock<IConfigurationSection>();
            _productTypeWithSurchargeService = new Mock<IProductTypeWithSurchargeService>();
            _insuranceController = new InsuranceController(_iInsuranceServiceMock.Object, _configurationMock.Object,_productTypeWithSurchargeService.Object);
        }

        [Fact]
        public async void CalculateInsuranceById_ShouldReturnObject_InsuranceDTO()
        {
            var product = CreateProductList().FirstOrDefault();
            string baseUrl = "http://localhost:5002";
            product.ProductType = CreateProductTypeList()?.FirstOrDefault(i => i.Id == product?.ProductTypeId);
            var dtoExpected = MapModelToInsuranceDTO(product);

            _configurationSection.Setup(a => a.Value).Returns(baseUrl);
            _configurationMock.Setup(c => c.GetSection(It.IsAny<String>())).Returns(new Mock<IConfigurationSection>().Object);
            _configurationMock.Setup(a => a.GetSection("ProductApi")).Returns(_configurationSection.Object);

            _iInsuranceServiceMock.Setup(c => c.GetInsuranceByProduct(product.Id, baseUrl)).ReturnsAsync(dtoExpected);

            var result = await _insuranceController.CalculateInsuranceById(product.Id);

            Assert.IsType<ActionResult<InsuranceDTO>>(result);
        }

        [Fact]
        public async void CalculateInsuranceById_ShouldReturnNull()
        {
            var product = new Product();
            string baseUrl = "http://localhost:5002";
            product.ProductType = new ProductType();
            var dtoExpected = new InsuranceDTO();

            _configurationSection.Setup(a => a.Value).Returns(baseUrl);
            _configurationMock.Setup(c => c.GetSection(It.IsAny<String>())).Returns(new Mock<IConfigurationSection>().Object);
            _configurationMock.Setup(a => a.GetSection("ProductApi")).Returns(_configurationSection.Object);

            _iInsuranceServiceMock.Setup(c => c.GetInsuranceByProduct(product.Id, baseUrl)).ReturnsAsync(dtoExpected);

            var result = await _insuranceController.CalculateInsuranceById(product.Id);

            Assert.Null(result.Value);
        }

        [Fact]
        public async void CalculateInsuranceById_ShouldReturnArgumentError_WhereProductId_IsNull()
        {
                string baseUrl = "http://localhost:5002";
                InsuranceDTO dtoExpected = new InsuranceDTO();

                _configurationSection.Setup(a => a.Value).Returns(baseUrl);
                _configurationMock.Setup(c => c.GetSection(It.IsAny<String>())).Returns(new Mock<IConfigurationSection>().Object);
                _configurationMock.Setup(a => a.GetSection("ProductApi")).Returns(_configurationSection.Object);

                _iInsuranceServiceMock.Setup(c => c.GetInsuranceByProduct(0, baseUrl)).ReturnsAsync(dtoExpected);

                await Assert.ThrowsAsync<ApplicationException>(() => _insuranceController.CalculateInsuranceById(null));

        }
        private static List<Product> CreateProductList()
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
                  ProductTypeId=21,
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
        private static List<ProductType> CreateProductTypeList()
        {
            return new List<ProductType>()
            {
                new ProductType()
                {
                    Id = 33,
                    Name = "Digital Camera",
                    CanBeInsured = true,

                },
              new ProductType()
                {
                    Id = 32,
                    Name = "Tesst",
                    CanBeInsured = true,

                },
              new ProductType()
                {
                    Id = 22,
                    Name = "TV",
                    CanBeInsured = true,

                },
                new ProductType()
                {
                    Id = 21,
                    Name = "Laptops",
                    CanBeInsured = true,

                }
            };
        }
        private static InsuranceDTO MapModelToInsuranceDTO(Product product)
        {
            var insuranceDTO = new InsuranceDTO()
            {
                ProductId = product.Id,
                ProductName = product.Name,
                ProductTypeHasInsurance = true,
                SalesPrice = product.SalesPrice,
                ProductTypeId = product.ProductTypeId,
                ProductTypeName = product.ProductType?.Name,
                InsuranceValue = 1000
            };

            return insuranceDTO;
        }
    }
}