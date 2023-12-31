﻿
namespace Insurance.Domain.DTOs
{
    /// <summary>
    /// TotalInsuranceCostOrderDTO.
    /// </summary>
    public class TotalInsuranceCostOrderDTO
    {
        /// <summary>
        /// Gets or sets TotalInsuranceCostOrder.
        /// </summary>
        public double TotalInsuranceCostOrder { get; set; }

        /// <summary>
        /// Gets or sets InsuranceDTO.
        /// </summary>
        public IEnumerable<InsuranceDTO> InsuranceDTO { get; set; }
    }
}
