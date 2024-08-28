using AutoMapper;
using Speridian.CMS.Entities.DTO;
using Speridian.CMS.PL.Models;

namespace Speridian.CMS.PL.Config
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Menu, MenuDto>();

            CreateMap<MenuDto, Menu>();

            CreateMap<CustomerMaster, CustomerDto>();

            CreateMap<CustomerDto, CustomerMaster>();

            CreateMap<Feedback, FeedbackDto>();

            CreateMap<FeedbackDto, Feedback>();

            CreateMap<Payment, PaymentDTO>();

            CreateMap<PaymentDTO, Payment>();

            CreateMap<OrderItemDto, OrderItem>();

            CreateMap<OrderItem, OrderItemDto>();
            
            CreateMap<OrderInvoiceDto, OrderInvoice>();

            CreateMap<OrderInvoice, OrderInvoiceDto>();
            
            CreateMap<User, UserDto>();

            CreateMap<UserDto, User>();
            
            CreateMap<Role, RoleDto>();

            CreateMap<RoleDto, Role>();
        }
    }
}
