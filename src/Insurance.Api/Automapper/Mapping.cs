using AutoMapper;
using Insurance.Api.DTOs;
using Insurance.Api.Entities;

namespace Insurance.Api.Automapper
{
    /// <summary>
    /// Mapping.
    /// </summary>
    public class Mapping : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Mapping"/> class.
        /// </summary>
        public Mapping()
        {
            CreateMap<ProductType, InsuranceDTO>()
               .ForMember(dto => dto.ProductTypeName, opt => opt.MapFrom(y => y.Name))
               .ForMember(dto => dto.ProductTypeId, opt => opt.MapFrom(y => y.Id)).ReverseMap();

            CreateMap<Product, InsuranceDTO>()
            .ForMember(dto => dto.SalesPrice, opt => opt.MapFrom(y => y.SalesPrice))
            .ForMember(dto => dto.ProductName, opt => opt.MapFrom(y => y.Name))
            .ForMember(dto => dto.ProductTypeHasInsurance, opt => opt.MapFrom(y => y.ProductType.CanBeInsured))
            .ForMember(dto => dto.ProductId, opt => opt.MapFrom(y => y.Id))
            .IncludeAllDerived();
        }
    }
}
