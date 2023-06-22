using System.Net.Http;
using System.Net;
using Insurance.Api.Services;
using Insurance.Domain.Test;
using Moq;
using Xunit;
using System.Threading;
using Moq.Protected;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Generic;
using Insurance.Domain.Models;
using Insurance.Domain.Exceptions;

namespace Insurance.Tests
{
    public class ProductAPIServiceMoqTests
    {
        private readonly HttpClient _httpClientWrapperMock;
        private readonly Mock<HttpMessageHandler> _messageHandlerMock;
        private readonly ProductAPIService _productAPIServiceMock;
        private readonly string baseUrl;

        public ProductAPIServiceMoqTests()
        {
            baseUrl = "http://localhost:5002";
            _messageHandlerMock = new Mock<HttpMessageHandler>();
            _httpClientWrapperMock = new HttpClient(_messageHandlerMock.Object)
            {
                BaseAddress = new Uri(baseUrl),
            };
            _productAPIServiceMock = new ProductAPIService(_httpClientWrapperMock);

        }

        [Fact]
        public async void GetProducts()
        {
            //Arrange
            _messageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(InsuranceDomainHelpers.CreateProductList())),
               })
               .Verifiable();

            // Act
            var products = await _productAPIServiceMock.GetProducts(baseUrl);

            // Assert
            Assert.Equal(InsuranceDomainHelpers.CreateProductList().Count, products.Count);

        }

        [Fact]
        public async void GetProducts_Return_EmptyList()
        {
            //Arrange
            _messageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(new List<ProductType>())),
               })
               .Verifiable();

            // Act
            var products = await _productAPIServiceMock.GetProducts(baseUrl);

            // Assert
            Assert.NotNull(products);
            Assert.Empty(products);

        }

        [Fact]
        public async void GetProductTypes_TotalCount_ShouldBeSame()
        {
            //Arrange
            _messageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(InsuranceDomainHelpers.CreateProductTypeList())),
               })
               .Verifiable();

            // Act
            var productTypes = await _productAPIServiceMock.GetProductTypes(baseUrl);

            // Assert
            Assert.Equal(InsuranceDomainHelpers.CreateProductTypeList().Count, productTypes.Count);

        }


        [Fact]
        public async void GetProductTypes_EmptyList()
        {
            //Arrange
            _messageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(new List<Product>())),
               })
               .Verifiable();

            // Act
            var productTypes = await _productAPIServiceMock.GetProductTypes(baseUrl);

            // Assert
            Assert.NotNull(productTypes);
            Assert.Empty(productTypes);

        }


        [Fact]
        public async void GetProductTypesById()
        {
            //Arrange
            var listOfProductTypes = InsuranceDomainHelpers.CreateProductTypeList();
            var productType = listOfProductTypes.FirstOrDefault();

            _messageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(productType)),
               })
               .Verifiable();

            // Act
            var result = await _productAPIServiceMock.GetProductTypeById(productType.Id, baseUrl);

            // Assert
            Assert.Equal(productType.Id, result.Id);

        }
     
        [Fact]
        public async void GetProductTypesById_ThrowHttpResponseException()
        {
            //Arrange
            var listOfProductTypes = InsuranceDomainHelpers.CreateProductTypeList();
            var productType = listOfProductTypes.FirstOrDefault();

            _messageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.NotFound,
                   Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(productType)),
               })
               .Verifiable();

            // Assert
            await Assert.ThrowsAsync<HttpResponseException>(() => _productAPIServiceMock.GetProductTypeById(productType.Id, baseUrl));

        }
      
        [Fact]
        public async void GetProductById()
        {
            //Arrange
            var listOfProducts = InsuranceDomainHelpers.CreateProductList();
            var product = listOfProducts.FirstOrDefault();

            _messageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(product)),
               })
               .Verifiable();

            // Act
            var result = await _productAPIServiceMock.GetProductById(product.Id, baseUrl);

            // Assert
            Assert.Equal(product.Id, result.Id);

        }

        [Fact]
        public async void GetProductById_ShouldReturnProductType()
        {
            //Arrange
            var listOfProducts = InsuranceDomainHelpers.CreateProductList();
            var product = listOfProducts.FirstOrDefault();

            _messageHandlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(product)),
               })
               .Verifiable();

            // Act
            var result = await _productAPIServiceMock.GetProductById(product.Id, baseUrl);

            // Assert
            Assert.IsType<Product>(result);

        }
    }

}