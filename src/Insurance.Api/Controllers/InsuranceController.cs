using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Insurance.Domain.DTOs;
using Insurance.Domain.Helpers;
using Insurance.Domain.Interfaces;
using Insurance.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Insurance.Api.Controllers
{
    /// <summary>
    /// InsuranceController.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class InsuranceController : ControllerBase
    {
        private readonly IInsuranceService _iInsuranceService;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="InsuranceController"/> class.
        /// </summary>
        /// <param name="configuration">configuration.</param>
        /// <param name="iInsuranceService">iInsuranceService.</param>
        public InsuranceController(IInsuranceService iInsuranceService, IConfiguration configuration)
        {
            _configuration = configuration;
            _iInsuranceService = iInsuranceService;
        }

        /// <summary>
        /// CalculateInsurance.
        /// </summary>
        /// <param name="productId">productId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet("/product")]
        public async Task<ActionResult<InsuranceDTO>> CalculateInsuranceById(int? productId)
        {
            if (productId == null)
            {
                throw new ApplicationException(ErrorMessages.ID_NULL);
            }

            InsuranceDTO toInsure = await _iInsuranceService.GetInsuranceByProduct(productId.Value, _configuration.GetValue<string>("ProductApi"));

            return Ok(toInsure);
        }

        /// <summary>
        /// CalculateOrderInsurance.
        /// </summary>
        /// <param name="productIds">productIds.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost("/products")]
        public async Task<ActionResult<double?>> CalculateOrderInsurance(List<int> productIds)
        {
            if (productIds == null || productIds?.Count == 0)
            {
                throw new ApplicationException(ErrorMessages.ID_NULL);
            }

            var toInsure = await _iInsuranceService.GetInsuranceForProductsInShoppingCart(productIds, _configuration.GetValue<string>("ProductApi"));

            return Ok(toInsure);
        }
    }
}