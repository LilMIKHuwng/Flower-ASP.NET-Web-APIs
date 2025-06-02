using AutoMapper;
using Azure.Core;
using Flower.Contract.Repositories.Interface;
using Flower.Contract.Services.Interface;
using Flower.Core.APIResponse;
using Flower.Core.Utils;
using Flower.ModelViews.UserModelViews;
using Flower.Repositories.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PusherServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Flower.Services.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration _configuration;

        public UserService(
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
             IUnitOfWork _unitOfWork,
             IConfiguration configuration
            )
        {
            _configuration = configuration;
            _userManager = userManager;
            _mapper = mapper;
            unitOfWork = _unitOfWork;
        }


        public async Task<ApiResult<bool>> UpdateUserProfile(UserUpdateProfileModelView updateDTO)
        {
            var user = await _userManager.FindByIdAsync(updateDTO.Id.ToString());
            if (user == null)
            {
                return new ApiErrorResult<bool>("Không tìm thấy tài khoản");
            }


            // Update user profile
            user.FullName = updateDTO.FullName;

            user.Age = updateDTO.Age;
            user.Email = updateDTO.Email;


            if (updateDTO.AvatarUrl != null)
            {

                // Upload new avatar
                var uploadResult = await Flower.Core.Firebase.ImageHelper.Upload(updateDTO.AvatarUrl);
                user.AvatarUrl = uploadResult?.ToString() ?? user.AvatarUrl;
            }


            // Calculate age if date of birth is provided
            user.LastUpdatedTime = DateTime.Now;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new ApiErrorResult<bool>(errors);
            }

            return new ApiSuccessResult<bool>(true, "Cập nhật thành công");
        }

        public async Task<ApiResult<object>> ConfirmUserRegister(UserConfirmRegisterModelView userConfirmRegisterDTO)
        {
            // Check existed email
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == userConfirmRegisterDTO.Email);
            if (existingUser == null)
            {
                return new ApiErrorResult<object>("Email is not existed.", System.Net.HttpStatusCode.NotFound);
            }

            // Confirm code 
            var result = await _userManager.ConfirmEmailAsync(existingUser, userConfirmRegisterDTO.Code);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<object>("Confirm email unsuccessfully", result.Errors.Select(x => x.Description).ToList(), System.Net.HttpStatusCode.BadRequest);
            }
            existingUser.Status = 1;
            var rs = await _userManager.UpdateAsync(existingUser);

            if (!rs.Succeeded)
            {
                return new ApiErrorResult<object>("Update unsuccessfully", result.Errors.Select(x => x.Description).ToList(), System.Net.HttpStatusCode.BadRequest);
            }
            return new ApiSuccessResult<object>("Register successfully.");
        }

        public async Task<ApiResult<object>> ForgotPassword(UserForgotPasswordModelView userForgotPasswordDTO)
        {
            var email = userForgotPasswordDTO.Email;
            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser == null)
            {
                return new ApiErrorResult<object>("Email is not existed.", System.Net.HttpStatusCode.NotFound);
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(existingUser);
            var encodedToken = Uri.EscapeDataString(token);

            // Correct relative path from current directory to the HTML file
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FormSendEmail", "SendCode.html");
            path = Path.GetFullPath(path);
            if (!File.Exists(path))
            {
                return new ApiErrorResult<object>("Không tìm thấy file gửi mail");
            }

            var frontEndUrl = _configuration["URL:FrontEnd"];
            var fullForgotPasswordUrl = frontEndUrl + "auth/new-password?email=" + email + "&token=" + encodedToken;
            string contentCustomer = System.IO.File.ReadAllText(path);
            contentCustomer = contentCustomer.Replace("{{VerifyCode}}", fullForgotPasswordUrl);
            var sendMailResult = DoingMail.SendMail("Flower Shop", "Yêu cầu thay đổi mật khẩu", contentCustomer, email);
            if (!sendMailResult)
            {
                return new ApiErrorResult<object>("Lỗi hệ thống. Vui lòng thử lại sau", System.Net.HttpStatusCode.NotFound);
            }
            return new ApiSuccessResult<object>("Please check your mail to reset password.");
        }

        public async Task<ApiResult<object>> RegisterUser(UserRegisterModelView registerDTO)
        {
            // Check existed email
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == registerDTO.Username);
            if (existingUser != null)
            {
                return new ApiErrorResult<object>("Username is existed.", System.Net.HttpStatusCode.BadRequest);
            }
            var existingUserEmail = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == registerDTO.Email);
            if (existingUserEmail != null)
            {
                return new ApiErrorResult<object>("Email is existed.", System.Net.HttpStatusCode.BadRequest);
            }
            var user = new ApplicationUser
            {
                UserName = registerDTO.Username,
                FullName = registerDTO.FullName,
                Age = registerDTO.Age,
                Email = registerDTO.Email
            };
            // Save user
            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<object>("Register unsuccessfully.", result.Errors.Select(x => x.Description).ToList(), System.Net.HttpStatusCode.BadRequest);
            }
            // Add role
            var rs = await _userManager.AddToRoleAsync(user, "User");
            if (!rs.Succeeded)
            {
                return new ApiErrorResult<object>("Register unsuccessfully.", rs.Errors.Select(x => x.Description).ToList(), System.Net.HttpStatusCode.BadRequest);
            }
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            // Create OTP
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FormSendEmail", "Welcome.html");
            path = Path.GetFullPath(path);
            if (!File.Exists(path))
            {
                return new ApiErrorResult<object>("Không tìm thấy file gửi mail");
            }
            var content = File.ReadAllText(path);
            content = content.Replace("{{OTP}}", Uri.EscapeDataString(token));
            content = content.Replace("{{Name}}", user.FullName);
            var resultSendMail = DoingMail.SendMail("Flower Shop", "Confirm Email", content, user.Email);
            if (!resultSendMail)
            {
                return new ApiErrorResult<object>("Cannot send email to " + registerDTO.Email);
            }

            return new ApiSuccessResult<object>("Please check your gmail to confirm");
        }


        public async Task<ApiResult<UserModelView>> UserLogin(UserLoginModelView loginDTO)
        {
            // Tìm user theo email
            var user = await _userManager.FindByNameAsync(loginDTO.Username);
            if (user == null)
            {
                return new ApiErrorResult<UserModelView>("Tài khoản hoặc mật khẩu không đúng");
            }

            // Kiểm tra password
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
            if (!isPasswordValid)
            {
                return new ApiErrorResult<UserModelView>("Tài khoản hoặc mật khẩu không đúng");
            }
            var isConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            if (!isConfirmed)
            {
                return new ApiErrorResult<UserModelView>("Tài khoản của bạn chưa được xác thực");

            }


            // Kiểm tra trạng thái tài khoản
            if (!(user.Status == 1))
            {
                return new ApiErrorResult<UserModelView>("Tài khoản hoặc mật khẩu không đúng");

            }


            var userDto = _mapper.Map<UserModelView>(user);




            return new ApiSuccessResult<UserModelView>(userDto, "Đăng nhập thành công");
        }


        
        public async Task<ApiResult<object>> ResetPassword(UserResetPasswordModelView userResetPasswordDTO)
        {
            // Check existed email
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == userResetPasswordDTO.Email);
            if (existingUser == null)
            {
                return new ApiErrorResult<object>("Email is not existed.");
            }
            string decodedToken = Uri.UnescapeDataString(userResetPasswordDTO.Token);
            // Valid token
            var result = await _userManager.ResetPasswordAsync(existingUser, decodedToken, userResetPasswordDTO.Password);
            if (!result.Succeeded)
            {
                return new ApiErrorResult<object>("Reset password unsuccesfully", result.Errors.Select(x => x.Description).ToList(), System.Net.HttpStatusCode.BadRequest);
            }
            return new ApiSuccessResult<object>("Reset password successfully.");

        }

        
    }
}
