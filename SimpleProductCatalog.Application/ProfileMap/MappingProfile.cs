using AutoMapper;
using SimpleProductCatalog.Abstraction.DTO;
using SimpleProductCatalog.Domain.Entities;

namespace SimpleProductCatalog.Application.ProfileMap
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
            CreateMap<ProductDTO, Product>()
                .ForMember(dest => dest.Category, opt => opt.Ignore()) // don't map entire Category entity
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category!.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Category!.Name));
        }
    }
}
