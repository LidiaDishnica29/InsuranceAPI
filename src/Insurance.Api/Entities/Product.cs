namespace Insurance.Api.Entities
{
    /// <summary>
    /// Product.
    /// </summary>
    public class Product
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
        /// Gets or sets SalesPrice.
        /// </summary>
        public float? SalesPrice { get; set; }

        /// <summary>
        /// Gets or sets ProductTypeId.
        /// </summary>
        public int? ProductTypeId { get; set; }

        /// <summary>
        /// Gets or sets ProductType.
        /// </summary>
        public ProductType ProductType { get; set; }
    }
}
