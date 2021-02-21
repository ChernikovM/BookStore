using AutoMapper;
using BookStore.BusinessLogicLayer.Models.User;
using BookStore.DataAccessLayer.Entities;

namespace BookStore.BusinessLogicLayer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserLoginModel>().ReverseMap();
            CreateMap<User, UserRegistrationModel>().ReverseMap();
        }
    }
}
