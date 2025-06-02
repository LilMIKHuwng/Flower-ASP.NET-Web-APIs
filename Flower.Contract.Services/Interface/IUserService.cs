using Flower.Core.APIResponse;
using Flower.ModelViews.UserModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower.Contract.Services.Interface
{
    public interface IUserService
    {
        Task<ApiResult<UserModelView>> UserLogin(UserLoginModelView loginDTO);
        Task<ApiResult<object>> RegisterUser(UserRegisterModelView registerDTO);
        
        Task<ApiResult<object>> ConfirmUserRegister(UserConfirmRegisterModelView userConfirmRegisterDTO);
        Task<ApiResult<object>> ForgotPassword(UserForgotPasswordModelView userForgotPasswordDTO);
        Task<ApiResult<object>> ResetPassword(UserResetPasswordModelView userResetPasswordDTO);
        Task<ApiResult<bool>> UpdateUserProfile(UserUpdateProfileModelView updateDTO);

    }
}
