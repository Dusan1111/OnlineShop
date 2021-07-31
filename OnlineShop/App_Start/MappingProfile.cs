using AutoMapper;
using OnlineShop.Dtos;
using OnlineShop.Models;

namespace OnlineShop.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domain to DTO 
            Mapper.CreateMap<Product, ProductDto>();
            Mapper.CreateMap<ProductCategory, ProductCategoryDto>();

            // DTO to Domain
            //Mapper.CreateMap<CustomerDTO, Customer>()
            //    .ForMember(x => x.Id, opt => opt.Ignore());

            //Mapper.CreateMap<MovieDTO, Movie>()
            //    .ForMember(x => x.Id, opt => opt.Ignore());

            //Mapper.CreateMap<MembershipType, MembershipTypeDTO>()
            //    .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}