using Insurance.Domain.Models;

namespace Insurance.Domain.Helpers
{
    /// <summary>
    /// Utils.
    /// </summary>
    public static class Utils
    {
        public static string GetProductsTypeConst = "{0}/product_types";
        public static string GetProductTypeConst = "{0}/product_types/{1:G}";
        public static string GetProductConst = "{0}/products/{1:G}";
        public static string GetProductsConst = "{0}/products/";

        /// <summary>
        /// GetInsuranceValue.
        /// </summary>
        /// <returns>double.</returns>
        public static double GetInsuranceValue(Product product, double? surcharge)
        {
            double insuranceValue = 0;

            if (!product.ProductType.CanBeInsured)
            {
                return insuranceValue;
            }

            if (product.SalesPrice >= 500)
            {
                switch (product.SalesPrice)
                {
                    case < 2000:
                        insuranceValue += 1000;
                        break;
                    case >= 2000:
                        insuranceValue += 2000;
                        break;
                }
            }

            insuranceValue += CalculateExtraForLaptopSmartphone(product.ProductTypeId.Value);

            insuranceValue += surcharge.HasValue ? CalculateSurcharge(product.SalesPrice.Value, surcharge.Value) : 0;
            return insuranceValue;
        }

        /// <summary>
        /// CalculateDigitalCamera.
        /// </summary>
        /// <returns>double.</returns>
        public static double CalculateDigitalCamera(Product product)
        {
            bool hasCamera = product.ProductTypeId == 33 && product.ProductType.CanBeInsured;
            return hasCamera ? 500 : 0;
        }

        /// <summary>
        /// CalculateDigitalCamera.
        /// </summary>
        /// <returns>double.</returns>
        public static double CalculateDigitalCamera(List<Product> products)
        {
            bool hasCamera = products.Any(p => p.ProductTypeId == 33 && p.ProductType.CanBeInsured);
            return hasCamera ? 500 : 0;
        }

        /// <summary>
        /// CalculateSurcharge.
        /// </summary>
        /// <returns>double.</returns>
        public static double CalculateSurcharge(double salesPrice, double surcharge)
        {
            return salesPrice * (surcharge / 100);
        }

        /// <summary>
        /// CalculateExtraForLaptopSmartphone.
        /// </summary>
        /// <returns>double.</returns>
        public static double CalculateExtraForLaptopSmartphone(int productTypeId)
        {
            // 21 id is for laptops insured and 32 id is for smartphones insured
            return productTypeId == 21 || productTypeId == 32 ? 500 : 0;
        }
    }
}
