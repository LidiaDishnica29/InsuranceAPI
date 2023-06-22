namespace Insurance.Domain.DTOs
{
    /// <summary>
    /// InsuranceDTO.
    /// </summary>
    public class InsuranceDTO
    {
        /// <summary>
        /// Gets or sets productId.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets ProductTypeName.
        /// </summary>
        public string ProductTypeName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets ProductTypeHasInsurance.
        /// </summary>
        public bool ProductTypeHasInsurance { get; set; }

        /// <summary>
        /// Gets or sets ProductName.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets ProductTypeId.
        /// </summary>
        public int? ProductTypeId { get; set; }

        /// <summary>
        /// Gets or sets InsuranceValue.
        /// </summary>
        public double? InsuranceValue { get; set; }

        /// <summary>
        /// Gets or sets SalesPrice.
        /// </summary>
        public double? SalesPrice { get; set; }
    }
}
