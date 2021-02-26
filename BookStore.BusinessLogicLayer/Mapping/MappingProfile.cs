using AutoMapper;
using BookStore.BusinessLogicLayer.Models.Responses;
using BookStore.BusinessLogicLayer.Models.User;
using BookStore.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace BookStore.BusinessLogicLayer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserLoginModel>().ReverseMap();
            CreateMap<User, UserRegistrationModel>().ReverseMap();
            CreateMap<User, UserResponseModel>().ReverseMap();
            CreateMap<User, UserUpdateModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
        }
    }
}
