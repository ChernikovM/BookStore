using AutoMapper;
using BookStore.BusinessLogicLayer.Models.Base;
using BookStore.BusinessLogicLayer.Models.RequestModels.Author;
using BookStore.BusinessLogicLayer.Models.RequestModels.Order;
using BookStore.BusinessLogicLayer.Models.RequestModels.OrderItem;
using BookStore.BusinessLogicLayer.Models.RequestModels.Payment;
using BookStore.BusinessLogicLayer.Models.RequestModels.PrintingEdition;
using BookStore.BusinessLogicLayer.Models.RequestModels.User;
using BookStore.BusinessLogicLayer.Models.ResponseModel.PrintingEdition;
using BookStore.BusinessLogicLayer.Models.ResponseModels.Author;
using BookStore.BusinessLogicLayer.Models.ResponseModels.Order;
using BookStore.BusinessLogicLayer.Models.ResponseModels.OrderItem.cs;
using BookStore.BusinessLogicLayer.Models.ResponseModels.Payment;
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
            CreateMap<User, UserResponseModel>().ReverseMap().ForMember(dest => dest.LockoutEnd, opt => opt.UseDestinationValue());
            CreateMap<User, UserUpdateModel>().ReverseMap();

            CreateMap<Author, AuthorModel>().ReverseMap();
            CreateMap<Author, AuthorCreateModel>().ReverseMap();
            CreateMap<Author, BaseModel>().ReverseMap();

            CreateMap<PrintingEdition, PrintingEditionModel>().ReverseMap();
            CreateMap<PrintingEdition, PrintingEditionCreateModel>().ReverseMap();
            CreateMap<PrintingEdition, BaseModel>().ReverseMap();

            CreateMap<Order, OrderModel>().ReverseMap();
            CreateMap<Order, OrderCreateModel>().ReverseMap();
            CreateMap<Order, BaseModel>().ReverseMap();

            CreateMap<OrderItem, OrderItemCreateModel>().ReverseMap();
            CreateMap<OrderItem, OrderItemModel>().ReverseMap();
            CreateMap<OrderItem, BaseModel>().ReverseMap();

            CreateMap<Payment, PaymentCreationModel>().ReverseMap();
            CreateMap<Payment, PaymentModel>().ReverseMap();
            CreateMap<Payment, BaseModel>().ReverseMap();
        }
    }
}
