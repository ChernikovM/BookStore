using AutoMapper;
using BookStore.BusinessLogicLayer.Models.RequestModels.User;
using BookStore.BusinessLogicLayer.Models.ResponseModel.PrintingEdition;
using BookStore.BusinessLogicLayer.Models.ResponseModels.Author;
using BookStore.BusinessLogicLayer.Models.ResponseModels.User;
using BookStore.DataAccessLayer.Entities;

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
            CreateMap<User, UserResponseModelForAdmin>().ReverseMap();
            CreateMap<Author, AuthorModel>().ReverseMap();
            CreateMap<PrintingEdition, PrintingEditionModel>().ReverseMap();
        }
    }
}
