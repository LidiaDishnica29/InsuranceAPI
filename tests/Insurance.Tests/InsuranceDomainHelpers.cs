using Insurance.Domain.DTOs;
using Insurance.Domain.Models;
using Insurance.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Domain.Test
{
    public static class InsuranceDomainHelpers
    {
        public static List<Product> CreateProductList()
        {
            return new List<Product>()
            {
               new Product()
              {
                Id= 572770,
                Name= "Samsung WW80J6400CW EcoBubble",
                SalesPrice= 475,
                ProductTypeId= 124
              },
               new Product()
              {
                Id= 715990,
                Name= "Canon Powershot SX620 HS Black",
                SalesPrice= 195,
                ProductTypeId= 33
              },
               new Product()
              {
                Id= 725435,
                Name= "Cowon Plenue D Gold",
                SalesPrice= 299.99,
                ProductTypeId= 12
              },
               new Product()
              {
                Id= 735246,
                Name= "AEG L8FB86ES",
                SalesPrice= 699,
                ProductTypeId= 124
              },
               new Product()
              {
                Id= 735296,
                Name= "Canon EOS 5D Mark IV Body",
                SalesPrice= 2699,
                ProductTypeId= 35
              },
               new Product()
              {
                Id= 767490,
                Name= "Canon EOS 77D + 18-55mm IS STM",
                SalesPrice= 749,
                ProductTypeId= 35
              },
               new Product()
              {
                Id= 780829,
                Name= "Panasonic Lumix DC-TZ90 Silver",
                SalesPrice= 319,
                ProductTypeId= 33
              },
               new Product()
              {
                Id= 805073,
                Name= "Haier HW80-B14636",
                SalesPrice= 449,
                ProductTypeId= 124
              },
               new Product()
              {
                Id= 819148,
                Name= "Nikon D3500 + AF-P DX 18-55mm f/3.5-5.6G VR",
                SalesPrice= 469,
                ProductTypeId= 35
              },
               new Product()
              {
                Id= 827074,
                Name= "Samsung Galaxy S10 Plus 128 GB Black",
                SalesPrice= 699,
                ProductTypeId= 32
              },
               new Product()
              {
                Id= 859366,
                Name= "OnePlus 8 Pro 128GB Black 5G",
                SalesPrice= 886,
                ProductTypeId= 32
              },
               new Product()
              {
                Id= 861866,
                Name= "Apple MacBook Pro 13\" (2020) MXK52N/A Space Gray",
                SalesPrice= 1749,
                ProductTypeId= 21
              },
             new Product()
              {
                Id= 292929,
                Name= "Apple MacBook Pro 13\" (2022) MXK52N/A Space Gray",
                SalesPrice= 1749,
                ProductTypeId= 841
              },
                new Product()
              {
                Id= 311311,
                Name= "Apple MacBook Pro 13\" (2020) MXK52N/A Space Gray",
                SalesPrice= 450,
                ProductTypeId= 21
              },
            };
        }
        public static List<ProductType> CreateProductTypeList()
        {
            return new List<ProductType>()
            {
                  new ProductType()
                  {
                    Id= 21,
                    Name= "Laptops",
                    CanBeInsured= true
                  },
                  new ProductType()
                  {
                    Id= 32,
                    Name= "Smartphones",
                    CanBeInsured= true
                  },
                  new ProductType()
                  {
                    Id= 33,
                    Name= "Digital cameras",
                    CanBeInsured= true
                  },
                  new ProductType()
                  {
                    Id= 35,
                    Name= "SLR cameras",
                    CanBeInsured= false
                  },
                  new ProductType()
                  {
                    Id= 12,
                    Name= "MP3 players",
                    CanBeInsured= false
                  },
                  new ProductType()
                  {
                    Id= 124,
                    Name= "Washing machines",
                    CanBeInsured= true
                  },
                  new ProductType()
                  {
                    Id= 841,
                    Name= "Laptops",
                    CanBeInsured= false
                  }
            };
        }

        public static InsuranceDTO MapModelToInsuranceDTO(Product product)
        {
            var insuranceDTO = new InsuranceDTO()
            {
                ProductId = product.Id,
                ProductName = product.Name,
                ProductTypeHasInsurance = true,
                SalesPrice = product.SalesPrice,
                ProductTypeId = product.ProductTypeId,
                ProductTypeName = product.ProductType?.Name,
            };

            return insuranceDTO;
        }

        public static List<ProductTypeWithSurcharge> CreateProductTypeWithSurchargeList()
        {
            return new List<ProductTypeWithSurcharge>()
            {
                new ProductTypeWithSurcharge {Id=1, ProductTypeId = 21, Surcharge = 10 },
                new ProductTypeWithSurcharge {Id=2, ProductTypeId = 22, Surcharge = 40 },
                new ProductTypeWithSurcharge {Id=3, ProductTypeId = 33, Surcharge = 5 },
                new ProductTypeWithSurcharge {Id=4, ProductTypeId = 32, Surcharge = 21 }
            };
        }
        public static ProductTypeWithSurcharge CreateProductTypeWithSurcharge()
        {
            return new ProductTypeWithSurcharge()
            {
                Id = 88,
                ProductTypeId = 99,
                Surcharge = 10,
            };
        }
        public static ProductTypeWithSurchargeDTO MapModelToProductTypeWithSurchargeDTO(ProductTypeWithSurcharge productTypeWithSurcharge)
        {
            if (productTypeWithSurcharge == null)
            {
                return null;
            }
            var productTypeWithSurchargeDTO = new ProductTypeWithSurchargeDTO()
            {
                ProductTypeId = productTypeWithSurcharge.ProductTypeId,
                Surcharge = productTypeWithSurcharge.Surcharge,
            };

            return productTypeWithSurchargeDTO;
        }
        public static ProductTypeWithSurcharge MapModelToProductTypeWithSurcharge(ProductTypeWithSurchargeDTO productTypeWithSurchargeDTO)
        {
            if (productTypeWithSurchargeDTO == null)
            {
                return null;
            }
            var productTypeWithSurcharge = new ProductTypeWithSurcharge()
            {
                ProductTypeId = productTypeWithSurchargeDTO.ProductTypeId,
                Surcharge = productTypeWithSurchargeDTO.Surcharge,
                Id = productTypeWithSurchargeDTO.Id
            };

            return productTypeWithSurcharge;
        }

    }
}
