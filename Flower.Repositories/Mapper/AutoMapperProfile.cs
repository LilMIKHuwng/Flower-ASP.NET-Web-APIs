using AutoMapper;
using Flower.Contract.Repositories.Entity;
using Flower.ModelViews.RoleModelViews;


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
