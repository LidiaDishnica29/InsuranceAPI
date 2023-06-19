using System;
using System.Threading.Tasks;
using AutoMapper;
using Insurance.Api.Controllers;
using Insurance.Api.DTOs;
using Insurance.Api.Interfaces;
using Insurance.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace Insurance.Tests
{
    public class InsuranceTests: IClassFixture<ControllerTestFixture>
    {
        private readonly ControllerTestFixture _fixture;
        private readonly Mock<IProductAPIService> _productAPIServiceMock;
        private readonly InsuranceService _insuranceService;
        private readonly Mock<ILogger<InsuranceService>> _logger;
        private readonly Mock<IMapper> _mapper;

        public InsuranceTests(ControllerTestFixture fixture)
        {
            _fixture = fixture;
          
            _insuranceService = new InsuranceService(_logger.Object, _mapper.Object, _productAPIServiceMock.Object);
        }

        [Fact]
        public async Task CalculateInsurance_GivenSalesPriceBetween500And2000Euros_ShouldAddThousandEurosToInsuranceCost()
        {
            const float expectedInsuranceValue = 1000;

            var sut = new InsuranceController(_insuranceService);

            var result =await sut.CalculateInsurance(735246);

            Assert.Equal(
                expected: expectedInsuranceValue,
                actual: result.Value.InsuranceValue
            );
        }
    }

    public class ControllerTestFixture
        //: IDisposable
    {
        private readonly IHost _host;

        public ControllerTestFixture()
        {
            //_host = new HostBuilder()
            //       .ConfigureWebHostDefaults(
            //            b => b.UseUrls("http://localhost:5002")
            //                  .UseStartup<ControllerTestStartup>()
            //        )
            //       .Build();

            //_host.Start();
        }

        //public void Dispose() => _host.Dispose();
    }

    public class ControllerTestStartup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(
                ep =>
                {
                    ep.MapGet(
                        "products/{id:int}",
                        context =>
                        {
                            int productId = int.Parse((string) context.Request.RouteValues["id"]);
                            var product = new
                                          {
                                              id = productId,
                                              name = "Test Product",
                                              productTypeId = 1,
                                              salesPrice = 750
                                          };
                            return context.Response.WriteAsync(JsonConvert.SerializeObject(product));
                        }
                    );
                    ep.MapGet(
                        "product_types",
                        context =>
                        {
                            var productTypes = new[]
                                               {
                                                   new
                                                   {
                                                       id = 1,
                                                       name = "Test type",
                                                       canBeInsured = true
                                                   }
                                               };
                            return context.Response.WriteAsync(JsonConvert.SerializeObject(productTypes));
                        }
                    );
                }
            );
        }
    }
}