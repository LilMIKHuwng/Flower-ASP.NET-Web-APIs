using AutoMapper;
using Flower.Contract.Repositories.Entity;
using Flower.ModelViews.CartItemModelView;
using Flower.ModelViews.CategoryModelViews;
using Flower.ModelViews.FlowerTypeModelViews;
using Flower.ModelViews.OrderDetailModelViews;
using Flower.ModelViews.OrderModelViews;
using Flower.ModelViews.PaymentModelViews;
using Flower.ModelViews.RoleModelViews;
using Flower.ModelViews.UserMessage;
using Flower.ModelViews.UserModelViews;
using Flower.Repositories.Entity;


namespace Flower.Repositories.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Role
            CreateMap<ApplicationRole, RoleModelView>().ReverseMap();
            CreateMap<ApplicationRole, CreateRoleModelView>().ReverseMap();
            CreateMap<ApplicationRole, UpdatedRoleModelView>().ReverseMap();
            // user
            CreateMap<ApplicationUser, UserModelView>().ReverseMap();


            //Category
            CreateMap<Category, CategoryCreateModelView>().ReverseMap();
            CreateMap<Category, CategoryUpdateModelView>().ReverseMap();
            CreateMap<Category, CategoryModelView>().ReverseMap();

            //FlowerType
            CreateMap<FlowerType, FlowerTypeModelView>().ReverseMap();
            CreateMap<FlowerType, FlowerTypeCreateModelView>().ReverseMap();
            CreateMap<FlowerType, FlowerTypeUpdateModelView>().ReverseMap();

            //Message
            CreateMap<UserMessage, ChatMessageModelView>().ReverseMap();

            //FlowerType
            CreateMap<CartItem, CartItemModelView>().ReverseMap();
            CreateMap<CartItem, CreateCartItemModelView>().ReverseMap();
            CreateMap<CartItem, UpdateCartItemModelView>().ReverseMap();

            //Order
            CreateMap<Order, OrderModelView>().ReverseMap();
            CreateMap<Order, CreateOrderModelView>().ReverseMap();

            //OrderDetail
            CreateMap<OrderDetail, OrderDetailModelView>().ReverseMap();

            //Payment
            CreateMap<Payment, PaymentModelView>().ReverseMap();
            CreateMap<Payment, CreatePaymentModelView>().ReverseMap();

        }
    }
}
