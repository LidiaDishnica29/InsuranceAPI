using System.ComponentModel.DataAnnotations;

namespace Insurance.Domain.Models
{
    /// <summary>
    /// ProductTypeWithSurcharge.
    /// </summary>
    public class ProductTypeWithSurcharge
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets productTypeId.
        /// </summary>
        public int ProductTypeId { get; set; }

        /// <summary>
        /// Gets or sets Surcharge.
        /// </summary>
        public double Surcharge { get; set; }
    }
}
