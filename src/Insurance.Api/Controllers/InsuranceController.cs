using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Insurance.Api.DTOs;
using Insurance.Api.Helpers;
using Insurance.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        /// <summary>
        /// Initializes a new instance of the <see cref="InsuranceController"/> class.
        /// </summary>
        /// <param name="logger">logger.</param>
        /// <param name="iInsuranceService">iInsuranceService.</param>
        public InsuranceController(IInsuranceService iInsuranceService)
        {
            _iInsuranceService = iInsuranceService;
        }

        /// <summary>
        /// CalculateInsurance.
        /// </summary>
        /// <param name="productId">productId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet("/product")]
        public async Task<ActionResult<InsuranceDTO>> CalculateInsurance(int? productId)
        {
            if (productId == null)
            {
                throw new ApplicationException(ErrorMessages.ID_NULL);
            }

            InsuranceDTO toInsure = await _iInsuranceService.GetInsuranceByProduct(productId.Value);

            return Ok(toInsure);
        }

        /// <summary>
        /// CalculateOrderInsurance.
        /// </summary>
        /// <param name="productIds">productIds.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost("/products")]
        public async Task<ActionResult<TotalInsuranceCostOrderDTO>> CalculateOrderInsurance(List<int> productIds)
        {
            if (productIds == null || productIds?.Count == 0)
            {
                throw new ApplicationException(ErrorMessages.ID_NULL);
            }

            TotalInsuranceCostOrderDTO toInsure = await _iInsuranceService.GetInsuranceForProductsInShoppingCart(productIds);

            return Ok(toInsure);
        }

        /// <summary>
        /// CalculateSurcharge.
        /// </summary>
        /// <param name="productTypeId">productTypeId.</param>
        /// <param name="surcharge">surcharge.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpGet("/surcharge")]
        public async Task<ActionResult<InsuranceDTO>> CalculateSurcharge(int? productTypeId, int? surcharge)
        {
            if (productTypeId == null)
            {
                throw new ApplicationException(ErrorMessages.ID_NULL);
            }

            if (surcharge == null)
            {
                throw new ApplicationException(ErrorMessages.SURCHARGE_NULL);
            }

            return Ok();
        }
    }
}