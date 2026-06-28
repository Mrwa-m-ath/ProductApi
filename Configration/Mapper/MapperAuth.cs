using AutoMapper;
using ProductApi.Dto.DtoCategores;
using ProductApi.Dto.DtoProduct;
using ProductApi.Dto.DtoUser;
using ProductApi.Model;

namespace ProductApi.Configration.Mapper
{
    public class MapperAuth : Profile
    {


        public MapperAuth()
        {

            CreateMap<User, AddNewUserDto>().ReverseMap();
            CreateMap<Product, AddNewProductDto>().ReverseMap();
            CreateMap<Categorise, AddNewCategoriseDto>().ReverseMap();

        }
    }
}
