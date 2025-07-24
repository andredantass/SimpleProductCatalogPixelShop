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
            CreateMap<Product, ProductDTO>();
        }
    }
}
