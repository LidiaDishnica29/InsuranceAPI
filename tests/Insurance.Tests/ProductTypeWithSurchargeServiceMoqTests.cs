using AutoMapper;
using Insurance.Domain.DTOs;
using Insurance.Domain.Interfaces;
using Insurance.Domain.Models;
using Insurance.Domain.Services;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace Insurance.Domain.Test
{
    public class ProductTypeWithSurchargeServiceMoqTests
    {
        private readonly Mock<IProductTypeWithSurchargeRepository> _productTypeWithSurchargeRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly ProductTypeWithSurchargeService _productTypeWithSurchargeService;

        public ProductTypeWithSurchargeServiceMoqTests()
        {
            _mapper = new Mock<IMapper>();
            _productTypeWithSurchargeRepository = new Mock<IProductTypeWithSurchargeRepository>();
            _productTypeWithSurchargeService = new ProductTypeWithSurchargeService(_productTypeWithSurchargeRepository.Object, _mapper.Object);
        }

        [Fact]
        public async void GetProductTypeWithSurcharge_WhenProductTypeIdExists()
        {
            //Arrange
            int productTypeId = 21;
            var productTypeWithSurcharge = InsuranceDomainHelpers.CreateProductTypeWithSurchargeList().FirstOrDefault(i => i.ProductTypeId == productTypeId);

            _productTypeWithSurchargeRepository.Setup(c => c.GetByProductTypeId(productTypeId)).ReturnsAsync(productTypeWithSurcharge);
            _mapper.Setup(m => m.Map<ProductTypeWithSurchargeDTO>(
                        It.IsAny<ProductTypeWithSurcharge>())).Returns(InsuranceDomainHelpers.MapModelToProductTypeWithSurchargeDTO(productTypeWithSurcharge));
            var result = await _productTypeWithSurchargeService.GetProductTypeWithSurcharge(productTypeId);

            //Assets
            Assert.NotNull(result);
            Assert.IsType<ProductTypeWithSurchargeDTO>(result);
        }

        [Fact]
        public async void GetProductTypeWithSurcharge_WhenProductTypeIdDontExists()
        {
            //Arrange
            int productTypeId = 99;
            var productTypeWithSurcharge = InsuranceDomainHelpers.CreateProductTypeWithSurchargeList().FirstOrDefault(i => i.ProductTypeId == productTypeId);

            _productTypeWithSurchargeRepository.Setup(c => c.GetByProductTypeId(productTypeId)).ReturnsAsync(productTypeWithSurcharge);
            _mapper.Setup(m => m.Map<ProductTypeWithSurchargeDTO>(
                        It.IsAny<ProductTypeWithSurcharge>())).Returns(InsuranceDomainHelpers.MapModelToProductTypeWithSurchargeDTO(productTypeWithSurcharge));
            var result = await _productTypeWithSurchargeService.GetProductTypeWithSurcharge(productTypeId);

            //Assets
            Assert.Null(result);
        }

        [Fact]
        public async void GetProductTypeWithSurchargeTypeDouble_WhenProductTypeIdExists()
        {
            //Arrange
            int productTypeId = 21;
            var productTypeWithSurcharge = InsuranceDomainHelpers.CreateProductTypeWithSurchargeList().FirstOrDefault(i => i.ProductTypeId == productTypeId);

            _productTypeWithSurchargeRepository.Setup(c => c.GetByProductTypeId(productTypeId)).ReturnsAsync(productTypeWithSurcharge);
            _mapper.Setup(m => m.Map<ProductTypeWithSurchargeDTO>(
                        It.IsAny<ProductTypeWithSurcharge>())).Returns(InsuranceDomainHelpers.MapModelToProductTypeWithSurchargeDTO(productTypeWithSurcharge));
            var result = await _productTypeWithSurchargeService.GetProductTypeWithSurcharge(productTypeId);

            //Assets
            Assert.NotNull(result);
            Assert.IsType<double>(result.Surcharge);
        }

        [Fact]
        public async void AddProductTypeWithSurcharge()
        {
            //Arrange
            var productTypeWithSurchargeModel = InsuranceDomainHelpers.CreateProductTypeWithSurcharge();
            var dto = new ProductTypeWithSurchargeDTO()
            {
                Id = productTypeWithSurchargeModel.Id,
                ProductTypeId = productTypeWithSurchargeModel.ProductTypeId,
                Surcharge = productTypeWithSurchargeModel.Surcharge
            };

            ProductTypeWithSurcharge productTypeWithSurcharge = null;
            _productTypeWithSurchargeRepository.Setup(c => c.GetByProductTypeId(dto.ProductTypeId)).ReturnsAsync(productTypeWithSurcharge);

            _mapper.Setup(m => m.Map<ProductTypeWithSurcharge>(
                        It.IsAny<ProductTypeWithSurchargeDTO>())).Returns(productTypeWithSurchargeModel);

            _productTypeWithSurchargeRepository.Setup(c => c.Add(productTypeWithSurchargeModel)).ReturnsAsync(1);

            var result = await _productTypeWithSurchargeService.AddProductTypeWithSurcharge(dto);

            //Assets
            Assert.IsType<int>(result);
            Assert.Equal(1, result);
        }

        [Fact]
        public async void AddProductTypeWithSurcharge_NotExist()
        {
            //Assets
            await Assert.ThrowsAsync<ArgumentNullException>(() => _productTypeWithSurchargeService.AddProductTypeWithSurcharge(null));
        }

        [Fact]
        public async void AddProductTypeWithSurcharge_WhenProductTypeIdExists()
        {
            //Arrange

            var productTypeWithSurcharge = InsuranceDomainHelpers.CreateProductTypeWithSurchargeList().FirstOrDefault(i => i.ProductTypeId == 21);

            var dto = new ProductTypeWithSurchargeDTO()
            {
                Id = productTypeWithSurcharge.Id,
                ProductTypeId = productTypeWithSurcharge.ProductTypeId,
                Surcharge = productTypeWithSurcharge.Surcharge + 10
            };
            _productTypeWithSurchargeRepository.Setup(c => c.GetByProductTypeId(dto.ProductTypeId)).ReturnsAsync(productTypeWithSurcharge);

            _mapper.Setup(m => m.Map<ProductTypeWithSurcharge>(
                        It.IsAny<ProductTypeWithSurchargeDTO>())).Returns(InsuranceDomainHelpers.MapModelToProductTypeWithSurcharge(dto));

            _productTypeWithSurchargeRepository.Setup(c => c.Update(productTypeWithSurcharge)).ReturnsAsync(1);

            var result = await _productTypeWithSurchargeService.AddProductTypeWithSurcharge(dto);

            //Assets
            Assert.IsType<int>(result);
            Assert.NotEqual(0, result);
        }
    }
}
