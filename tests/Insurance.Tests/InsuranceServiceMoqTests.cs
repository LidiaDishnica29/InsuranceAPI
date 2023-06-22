using AutoMapper;
using Insurance.Domain.DTOs;
using Insurance.Domain.Interfaces;
using Insurance.Domain.Models;
using Insurance.Domain.Services;
using Insurance.Domain.Test;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Insurance.Tests
{
    public class InsuranceServiceMoqTests
    {
        private readonly Mock<IProductAPIService> _productAPIServiceMock;
        private readonly Mock<IProductTypeWithSurchargeService> _productTypeWithSurchargeService;
        private readonly InsuranceService _insuranceService;
        private readonly Mock<IMapper> _mapper;

        public InsuranceServiceMoqTests()
        {
            _mapper = new Mock<IMapper>();
            _productAPIServiceMock = new Mock<IProductAPIService>();
            _productTypeWithSurchargeService = new Mock<IProductTypeWithSurchargeService>();
            _insuranceService = new InsuranceService(_productAPIServiceMock.Object, _mapper.Object, _productTypeWithSurchargeService.Object);
        }

        [Fact]
        public async void GetInsurance_ForOrderTypeDouble_WhenListProductIdsExist()
        {
            //Arrange
            string baseUrl = "http://localhost:5002";
            List<int> ids = new() { 725435, 780829, 715990, 725435 };
            _productAPIServiceMock.Setup(i => i.GetProductTypes(baseUrl)).ReturnsAsync(InsuranceDomainHelpers.CreateProductTypeList());
            _productAPIServiceMock.Setup(i => i.GetProducts(baseUrl)).ReturnsAsync(InsuranceDomainHelpers.CreateProductList());
            var product = InsuranceDomainHelpers.CreateProductList().FirstOrDefault();
            _mapper.Setup(m => m.Map<InsuranceDTO>(
             It.IsAny<Product>())).Returns(InsuranceDomainHelpers.MapModelToInsuranceDTO(product));
            var result = await _insuranceService.GetInsuranceForProductsInShoppingCart(ids, baseUrl);

            //Assets
            Assert.NotNull(result);
            Assert.IsType<double>(result);
        }

        [Fact]
        public async void GetInsuranceForOrder_ThrowsError_WhenProductIdsNotExist()
        {
            // Arrange
            string baseUrl = "http://localhost:5002";
            List<int> ids = new() { 3 };

            _productAPIServiceMock.Setup(i => i.GetProductTypes(baseUrl))
                .ReturnsAsync(InsuranceDomainHelpers.CreateProductTypeList());
            _productAPIServiceMock.Setup(i => i.GetProducts(baseUrl)).ReturnsAsync(InsuranceDomainHelpers.CreateProductList());
            var product = InsuranceDomainHelpers.CreateProductList().FirstOrDefault();
            _mapper.Setup(m => m.Map<InsuranceDTO>(
             It.IsAny<Product>())).Returns(InsuranceDomainHelpers.MapModelToInsuranceDTO(product));

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _insuranceService.GetInsuranceForProductsInShoppingCart(ids, baseUrl));

        }

        [Fact]
        public async void GetInsuranceForOrder_ShouldReturnInsurance_And_CameraIncluded()
        {
            //Arrange
            string baseUrl = "http://localhost:5002";

            // Product id 715990-> has digital camera; 725435 not to consider since CanBeInsured=false
            List<int> ids = new() { 715990, 725435 };

            _productAPIServiceMock.Setup(i => i.GetProductTypes(baseUrl)).ReturnsAsync(InsuranceDomainHelpers.CreateProductTypeList());
            _productAPIServiceMock.Setup(i => i.GetProducts(baseUrl)).ReturnsAsync(InsuranceDomainHelpers.CreateProductList());

            var product = InsuranceDomainHelpers.CreateProductList().FirstOrDefault();
            _mapper.Setup(m => m.Map<InsuranceDTO>(
             It.IsAny<Product>())).Returns(InsuranceDomainHelpers.MapModelToInsuranceDTO(product));

            var result = await _insuranceService.GetInsuranceForProductsInShoppingCart(ids, baseUrl);

            //Assets
            Assert.NotNull(result);
            Assert.Equal(500, result);
        }

        [Fact]
        public async void GetInsuranceForOrder_ShouldReturn0_WhenProductTypeCanBeInsuredIsFalse()
        {
            //Arrange
            string baseUrl = "http://localhost:5002";

            // Product id 715990-> has digital camera; 725435 not to consider since CanBeInsured=false
            List<int> ids = new() { 725435, 725435, 725435 };

            _productAPIServiceMock.Setup(i => i.GetProductTypes(baseUrl)).ReturnsAsync(InsuranceDomainHelpers.CreateProductTypeList());
            _productAPIServiceMock.Setup(i => i.GetProducts(baseUrl)).ReturnsAsync(InsuranceDomainHelpers.CreateProductList());

            var product = InsuranceDomainHelpers.CreateProductList().FirstOrDefault();
            _mapper.Setup(m => m.Map<InsuranceDTO>(
             It.IsAny<Product>())).Returns(InsuranceDomainHelpers.MapModelToInsuranceDTO(product));

            var result = await _insuranceService.GetInsuranceForProductsInShoppingCart(ids, baseUrl);

            //Assets
            Assert.NotNull(result);
            Assert.Equal(0, result);
        }

        [Fact]
        public async void GetInsuranceForOrder_ShouldReturn_WhenProductTypeLaptopAndSmartphone()
        {
            //Arrange
            string baseUrl = "http://localhost:5002";

            List<int> ids = new() { 859366, 861866, 292929 };

            _productAPIServiceMock.Setup(i => i.GetProductTypes(baseUrl)).ReturnsAsync(InsuranceDomainHelpers.CreateProductTypeList());
            _productAPIServiceMock.Setup(i => i.GetProducts(baseUrl)).ReturnsAsync(InsuranceDomainHelpers.CreateProductList());

            var product = InsuranceDomainHelpers.CreateProductList().FirstOrDefault();
            _mapper.Setup(m => m.Map<InsuranceDTO>(
             It.IsAny<Product>())).Returns(InsuranceDomainHelpers.MapModelToInsuranceDTO(product));

            var result = await _insuranceService.GetInsuranceForProductsInShoppingCart(ids, baseUrl);

            //Assets
            Assert.NotNull(result);
            Assert.Equal(3000, result);
        }

        [Fact]
        public async void GetInsuranceForOrder_ShouldReturn_WhenProductTypeLaptopAndSurcharge()
        {
            //Arrange
            string baseUrl = "http://localhost:5002";

            List<int> ids = new() { 859366, 861866, 292929 };

            _productAPIServiceMock.Setup(i => i.GetProductTypes(baseUrl)).ReturnsAsync(InsuranceDomainHelpers.CreateProductTypeList());
            _productAPIServiceMock.Setup(i => i.GetProducts(baseUrl)).ReturnsAsync(InsuranceDomainHelpers.CreateProductList());

            var product = InsuranceDomainHelpers.CreateProductList().FirstOrDefault();
            _mapper.Setup(m => m.Map<InsuranceDTO>(
             It.IsAny<Product>())).Returns(InsuranceDomainHelpers.MapModelToInsuranceDTO(product));

            ProductTypeWithSurchargeDTO productTypeWithSurchargeDTO = new ProductTypeWithSurchargeDTO()
            {
                ProductTypeId = 21,
                Surcharge = 20
            };

            _productTypeWithSurchargeService.Setup(i => i.GetProductTypeWithSurcharge(21)).ReturnsAsync(productTypeWithSurchargeDTO);

            var result = await _insuranceService.GetInsuranceForProductsInShoppingCart(ids, baseUrl);

            //Assets
            Assert.NotNull(result);
            Assert.Equal(3349.8, result);
        }
        [Fact]
        public async void GetById_ShouldReturnInsuranceValue_WhenSurchargeCalculated()
        {
            //Arrange
            string baseUrl = "http://localhost:5002";
            int prductId = 572770;
            int prductTypeId = 124;
            var product = InsuranceDomainHelpers.CreateProductList().FirstOrDefault(i => i.Id == prductId);
            var productType = InsuranceDomainHelpers.CreateProductTypeList().FirstOrDefault(i => i.Id == prductTypeId);

            _productAPIServiceMock.Setup(i => i.GetProductById(prductId, baseUrl)).ReturnsAsync(product);
            _productAPIServiceMock.Setup(i => i.GetProductTypeById(prductTypeId, baseUrl)).ReturnsAsync(productType);
            ProductTypeWithSurchargeDTO productTypeWithSurchargeDTO = new ProductTypeWithSurchargeDTO()
            {
                ProductTypeId = prductTypeId,
                Surcharge = 10
            };

            _productTypeWithSurchargeService.Setup(i => i.GetProductTypeWithSurcharge(productType.Id)).ReturnsAsync(productTypeWithSurchargeDTO);

            product.ProductType = productType;
            _mapper.Setup(m => m.Map<InsuranceDTO>(
             It.IsAny<Product>())).Returns(InsuranceDomainHelpers.MapModelToInsuranceDTO(product));
            var result = await _insuranceService.GetInsuranceByProduct(prductId, baseUrl);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<InsuranceDTO>(result);
            Assert.Equal(47.5, result.InsuranceValue);
        }

        [Fact]
        public async void GetById_ShouldReturnInsuranceValueNotEqualTo0_WhenDigitalCamera()
        {
            string baseUrl = "http://localhost:5002";
            int productId = 715990;
            int productTypeId = 33;

            var product = InsuranceDomainHelpers.CreateProductList().FirstOrDefault(i => i.Id == productId);
            var productType = InsuranceDomainHelpers.CreateProductTypeList().FirstOrDefault(i => i.Id == productTypeId);
            product.ProductType = productType;

            _productAPIServiceMock.Setup(i => i.GetProductById(productId, baseUrl)).ReturnsAsync(product);
            _productAPIServiceMock.Setup(i => i.GetProductTypeById(productTypeId, baseUrl)).ReturnsAsync(productType);

            _mapper.Setup(m => m.Map<InsuranceDTO>(
             It.IsAny<Product>())).Returns(InsuranceDomainHelpers.MapModelToInsuranceDTO(product));

            var result = await _insuranceService.GetInsuranceByProduct(productId, baseUrl);

            Assert.NotNull(result);
            Assert.IsType<InsuranceDTO>(result);
            Assert.NotEqual(0, result.InsuranceValue);
        }

        [Fact]
        public async void GetById_ShouldReturnInsuranceValue0_WhenSmallerThan500()
        {
            string baseUrl = "http://localhost:5002";
            int productId = 572770;
            int productTypeId = 124;

            var product = InsuranceDomainHelpers.CreateProductList().FirstOrDefault(i => i.Id == productId);
            var productType = InsuranceDomainHelpers.CreateProductTypeList().FirstOrDefault(i => i.Id == productTypeId);
            product.ProductType = productType;

            _productAPIServiceMock.Setup(i => i.GetProductById(productId, baseUrl)).ReturnsAsync(product);
            _productAPIServiceMock.Setup(i => i.GetProductTypeById(productTypeId, baseUrl)).ReturnsAsync(productType);

            _mapper.Setup(m => m.Map<InsuranceDTO>(
             It.IsAny<Product>())).Returns(InsuranceDomainHelpers.MapModelToInsuranceDTO(product));

            var result = await _insuranceService.GetInsuranceByProduct(productId, baseUrl);

            Assert.NotNull(result);
            Assert.IsType<InsuranceDTO>(result);
            Assert.Equal(0, result.InsuranceValue);
        }

        [Fact]
        public async void GetById_ShouldReturnInsuranceValue_WhenLaptopAndSalesSmallerThan500()
        {
            string baseUrl = "http://localhost:5002";
            int productId = 311311;
            int productTypeId = 21;

            var product = InsuranceDomainHelpers.CreateProductList().FirstOrDefault(i => i.Id == productId);
            var productType = InsuranceDomainHelpers.CreateProductTypeList().FirstOrDefault(i => i.Id == productTypeId);
            product.ProductType = productType;

            _productAPIServiceMock.Setup(i => i.GetProductById(productId, baseUrl)).ReturnsAsync(product);
            _productAPIServiceMock.Setup(i => i.GetProductTypeById(productTypeId, baseUrl)).ReturnsAsync(productType);

            _mapper.Setup(m => m.Map<InsuranceDTO>(
             It.IsAny<Product>())).Returns(InsuranceDomainHelpers.MapModelToInsuranceDTO(product));

            var result = await _insuranceService.GetInsuranceByProduct(productId, baseUrl);

            Assert.NotNull(result);
            Assert.IsType<InsuranceDTO>(result);
            Assert.Equal(500, result.InsuranceValue);
        }

        [Fact]
        public async void GetById_ShouldReturnInsuranceValue_WhenLaptopAndSalesBiggerThan500()
        {
            string baseUrl = "http://localhost:5002";
            int productId = 827074;
            int productTypeId = 32;

            var product = InsuranceDomainHelpers.CreateProductList().FirstOrDefault(i => i.Id == productId);
            var productType = InsuranceDomainHelpers.CreateProductTypeList().FirstOrDefault(i => i.Id == productTypeId);
            product.ProductType = productType;

            _productAPIServiceMock.Setup(i => i.GetProductById(productId, baseUrl)).ReturnsAsync(product);
            _productAPIServiceMock.Setup(i => i.GetProductTypeById(productTypeId, baseUrl)).ReturnsAsync(productType);

            _mapper.Setup(m => m.Map<InsuranceDTO>(
             It.IsAny<Product>())).Returns(InsuranceDomainHelpers.MapModelToInsuranceDTO(product));

            var result = await _insuranceService.GetInsuranceByProduct(productId, baseUrl);

            Assert.NotNull(result);
            Assert.IsType<InsuranceDTO>(result);
            Assert.Equal(1500, result.InsuranceValue);
        }

        [Fact]
        public async void GetById_ShouldReturnInsuranceValue0_WhenCanBeInsuredFalse()
        {
            string baseUrl = "http://localhost:5002";
            int productId = 725435;
            int productTypeId = 12;

            var product = InsuranceDomainHelpers.CreateProductList().FirstOrDefault(i => i.Id == productId);
            var productType = InsuranceDomainHelpers.CreateProductTypeList().FirstOrDefault(i => i.Id == productTypeId);
            product.ProductType = productType;

            _productAPIServiceMock.Setup(i => i.GetProductById(productId, baseUrl)).ReturnsAsync(product);
            _productAPIServiceMock.Setup(i => i.GetProductTypeById(productTypeId, baseUrl)).ReturnsAsync(productType);

            _mapper.Setup(m => m.Map<InsuranceDTO>(
             It.IsAny<Product>())).Returns(InsuranceDomainHelpers.MapModelToInsuranceDTO(product));

            var result = await _insuranceService.GetInsuranceByProduct(productId, baseUrl);

            Assert.NotNull(result);
            Assert.IsType<InsuranceDTO>(result);
            Assert.Equal(0, result.InsuranceValue);
        }

    }
}
