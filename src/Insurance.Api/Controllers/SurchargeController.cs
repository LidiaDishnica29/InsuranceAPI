using Insurance.Domain.DTOs;
using Insurance.Domain.Helpers;
using Insurance.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Insurance.Api.Controllers
{
    /// <summary>
    /// InsuranceController.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SurchargeController : ControllerBase
    {
        private readonly IProductTypeWithSurchargeService _productTypeWithSurchargeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SurchargeController"/> class.
        /// </summary>
        /// <param name="productTypeWithSurchargeService">productTypeWithSurchargeService.</param>
        public SurchargeController(IProductTypeWithSurchargeService productTypeWithSurchargeService)
        {
            _productTypeWithSurchargeService = productTypeWithSurchargeService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        /// <summary>
        /// CalculateSurcharge.
        /// </summary>
        /// <param name="productTypeWithSurcharge">productTypeWithSurcharge.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost("/surcharge")]
        public async Task<ActionResult> UploadSurcharge(ProductTypeWithSurchargeDTO productTypeWithSurcharge)
        {
            var isSaved = await _productTypeWithSurchargeService.AddProductTypeWithSurcharge(productTypeWithSurcharge);
            if (isSaved <= 0)
            {
                return BadRequest(ErrorMessages.CANNOT_CREATE);
            }

            return Ok(ErrorMessages.CREATE_SURCHARGE);
        }
    }
}
