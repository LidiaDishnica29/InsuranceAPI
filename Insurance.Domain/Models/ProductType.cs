namespace Insurance.Domain.Models
{
    /// <summary>
    /// ProductType.
    /// </summary>
    public class ProductType
    {
        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets CanBeInsured.
        /// </summary>
        public bool CanBeInsured { get; set; }
    }
}
