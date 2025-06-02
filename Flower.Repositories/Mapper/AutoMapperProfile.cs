using AutoMapper;
using Flower.Contract.Repositories.Entity;
using Flower.ModelViews.CategoryModelViews;
using Flower.ModelViews.FlowerTypeModelViews;
using Flower.ModelViews.RoleModelViews;
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
            /*CreateMap<UserMessage, ChatMessageModelView>().ReverseMap();
            CreateMap<ApplicationUser, UserResponseModel>().ReverseMap();

            //User
            CreateMap<ApplicationUser, UserLoginResponseModel>().ReverseMap();
            CreateMap<ApplicationUser, UserResponseModel>().ReverseMap();
            CreateMap<ApplicationUser, UpdateUserProfileRequest>().ReverseMap();*/

            //Employee
            /*CreateMap<EmployeeLoginResponseModel, ApplicationUser>().ReverseMap();
            CreateMap<CreateEmployeeRequest, ApplicationUser>().ReverseMap();
            CreateMap<UpdateEmployeeProfileRequest, ApplicationUser>().ReverseMap();
            CreateMap<ApplicationUser, EmployeeResponseModel>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.UserRoles.FirstOrDefault().Role))
                .ReverseMap()
                .ForMember(dest => dest.UserRoles, opt => opt.Ignore());*/


        }
    }
}
