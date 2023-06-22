using System.Text.Json.Serialization;

namespace Insurance.Domain.DTOs
{
    /// <summary>
    /// ProductTypeWithSurchargeDTO.
    /// </summary>
    public class ProductTypeWithSurchargeDTO
    {
        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        [JsonIgnore]
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets ProductTypeId.
        /// </summary>
        public int ProductTypeId { get; set; }

        /// <summary>
        /// Gets or sets Surcharge.
        /// </summary>
        public double Surcharge { get; set; }
    }
}
