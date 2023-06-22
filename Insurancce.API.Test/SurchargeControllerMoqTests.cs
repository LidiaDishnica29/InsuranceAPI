using Insurance.Api.Controllers;
using Insurance.Domain.DTOs;
using Insurance.Domain.Interfaces;
using Insurance.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Insurancce.API.Test
{
    public class SurchargeControllerMoqTests
    {
        private readonly Mock<IProductTypeWithSurchargeService> _productTypeWithSurchargeServiceMock;
        private readonly SurchargeController _surchargeController;

        public SurchargeControllerMoqTests()
        {
            _productTypeWithSurchargeServiceMock = new Mock<IProductTypeWithSurchargeService>();
            _surchargeController = new SurchargeController(_productTypeWithSurchargeServiceMock.Object);
        }

        [Fact]
        public async void UploadSurcharge_ShouldReturnBadRequest_WhenIsNotAddedSuccessfully()
        {
            ProductTypeWithSurchargeDTO productTypeWithSurchargeDTO = new ProductTypeWithSurchargeDTO()
            {
                Id = 1,
                ProductTypeId = 21,
                Surcharge = 10
            };

            _productTypeWithSurchargeServiceMock.Setup(c => c.AddProductTypeWithSurcharge(productTypeWithSurchargeDTO)).ReturnsAsync(0);

            var result = await _surchargeController.UploadSurcharge(productTypeWithSurchargeDTO);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async void UploadSurcharge_ShouldReturnOk_WhenIsAddedSuccessfully()
        {
            ProductTypeWithSurchargeDTO productTypeWithSurchargeDTO = new ProductTypeWithSurchargeDTO()
            {
                Id = 1,
                ProductTypeId = 21,
                Surcharge = 10
            };

            _productTypeWithSurchargeServiceMock.Setup(c => c.AddProductTypeWithSurcharge(productTypeWithSurchargeDTO)).ReturnsAsync(1);

            var result = await _surchargeController.UploadSurcharge(productTypeWithSurchargeDTO);
          
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
