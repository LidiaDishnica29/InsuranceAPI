
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
        

        public InsuranceControllerMoqTests()
        {
            _iInsuranceServiceMock = new Mock<IInsuranceService>();
            _configurationMock = new Mock<IConfiguration>();
            _configurationSection = new Mock<IConfigurationSection>();
            _insuranceController = new InsuranceController(_iInsuranceServiceMock.Object, _configurationMock.Object);
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

        [Fact]
        public async void CalculateOrderInsurance_ShouldReturnDouble()
        {
            var products = CreateProductList();
            string baseUrl = "http://localhost:5002";
            List<int> ids = new List<int>() { 715990, 715990, 572770 };
            var productTypes = CreateProductTypeList();
            var product = products.FirstOrDefault();
            var productType = productTypes.FirstOrDefault(i => i.Id == product.ProductTypeId);
            var dtoExpected = MapModelToInsuranceDTO(product);

            _configurationSection.Setup(a => a.Value).Returns(baseUrl);
            _configurationMock.Setup(c => c.GetSection(It.IsAny<String>())).Returns(new Mock<IConfigurationSection>().Object);
            _configurationMock.Setup(a => a.GetSection("ProductApi")).Returns(_configurationSection.Object);

            _iInsuranceServiceMock.Setup(c => c.GetInsuranceForProductsInShoppingCart(ids, baseUrl)).ReturnsAsync(500);

            var result = await _insuranceController.CalculateOrderInsurance(ids);

            Assert.IsType<ActionResult<double?>>(result);
        }

        [Fact]
        public async void CalculateOrderInsurance_ShouldThrowError_WhenListIds_IsEmpty()
        {
            string baseUrl = "http://localhost:5002";
            List<int> ids = new List<int>() {};
            
            _configurationSection.Setup(a => a.Value).Returns(baseUrl);
            _configurationMock.Setup(c => c.GetSection(It.IsAny<String>())).Returns(new Mock<IConfigurationSection>().Object);
            _configurationMock.Setup(a => a.GetSection("ProductApi")).Returns(_configurationSection.Object);

            _iInsuranceServiceMock.Setup(c => c.GetInsuranceForProductsInShoppingCart(ids, baseUrl)).ReturnsAsync(500);

            await Assert.ThrowsAsync<ApplicationException>(() => _insuranceController.CalculateOrderInsurance(ids));
        }

        
        private List<Product> CreateProductList()
        {
            return new List<Product>()
            {
               new Product()
              {
                Id= 572770,
                Name= "Samsung WW80J6400CW EcoBubble",
                SalesPrice= 475,
                ProductTypeId= 124
              },
               new Product()
              {
                Id= 715990,
                Name= "Canon Powershot SX620 HS Black",
                SalesPrice= 195,
                ProductTypeId= 33
              },
               new Product()
              {
                Id= 725435,
                Name= "Cowon Plenue D Gold",
                SalesPrice= 299.99,
                ProductTypeId= 12
              },
               new Product()
              {
                Id= 735246,
                Name= "AEG L8FB86ES",
                SalesPrice= 699,
                ProductTypeId= 124
              },
               new Product()
              {
                Id= 735296,
                Name= "Canon EOS 5D Mark IV Body",
                SalesPrice= 2699,
                ProductTypeId= 35
              },
               new Product()
              {
                Id= 767490,
                Name= "Canon EOS 77D + 18-55mm IS STM",
                SalesPrice= 749,
                ProductTypeId= 35
              },
               new Product()
              {
                Id= 780829,
                Name= "Panasonic Lumix DC-TZ90 Silver",
                SalesPrice= 319,
                ProductTypeId= 33
              },
               new Product()
              {
                Id= 805073,
                Name= "Haier HW80-B14636",
                SalesPrice= 449,
                ProductTypeId= 124
              },
               new Product()
              {
                Id= 819148,
                Name= "Nikon D3500 + AF-P DX 18-55mm f/3.5-5.6G VR",
                SalesPrice= 469,
                ProductTypeId= 35
              },
               new Product()
              {
                Id= 827074,
                Name= "Samsung Galaxy S10 Plus 128 GB Black",
                SalesPrice= 699,
                ProductTypeId= 32
              },
               new Product()
              {
                Id= 859366,
                Name= "OnePlus 8 Pro 128GB Black 5G",
                SalesPrice= 886,
                ProductTypeId= 32
              },
               new Product()
              {
                Id= 861866,
                Name= "Apple MacBook Pro 13\" (2020) MXK52N/A Space Gray",
                SalesPrice= 1749,
                ProductTypeId= 21
              }
            };
        }
        private List<ProductType> CreateProductTypeList()
        {
            return new List<ProductType>()
            {
                  new ProductType()
                  {
                    Id= 21,
                    Name= "Laptops",
                    CanBeInsured= true
                  },
                  new ProductType()
                  {
                    Id= 32,
                    Name= "Smartphones",
                    CanBeInsured= true
                  },
                  new ProductType()
                  {
                    Id= 33,
                    Name= "Digital cameras",
                    CanBeInsured= true
                  },
                  new ProductType()
                  {
                    Id= 35,
                    Name= "SLR cameras",
                    CanBeInsured= false
                  },
                  new ProductType()
                  {
                    Id= 12,
                    Name= "MP3 players",
                    CanBeInsured= false
                  },
                  new ProductType()
                  {
                    Id= 124,
                    Name= "Washing machines",
                    CanBeInsured= true
                  },
                  new ProductType()
                  {
                    Id= 841,
                    Name= "Laptops",
                    CanBeInsured= false
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