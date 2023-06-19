namespace Insurance.Api.DTOs
{
    public class ProductTypeWithSurchargeDTO
    {
        public float Surcharge { get; set; }
        public int ProductTypeId { get; set; }
        public int ProductId { get; set; }

    }
}
