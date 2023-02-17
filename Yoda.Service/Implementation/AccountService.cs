using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Yoda.DAL.Interface;
using Yoda.Domain.BaseResponse;
using Yoda.Domain.Enum;
using Yoda.Domain.Helper;
using Yoda.Domain.Model;
using Yoda.Domain.ViewModel.Account;
using Yoda.Service.Interface;

namespace Yoda.Service.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository userRepository;
        private readonly ILogger<AccountService> logger;
        private readonly IProfileRepository profileRepository;

        public AccountService(IUserRepository userRepository, ILogger<AccountService> logger, IProfileRepository profileRepository)
        {
            this.userRepository = userRepository;
            this.logger = logger;
            this.profileRepository= profileRepository;
        }
       

        public async Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model)
        {
            try
            {
                var user = await userRepository.GetAll().FirstOrDefaultAsync(x => x.Email == model.Login);
                if (user != null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "There is already a user with this email.",
                    };
                }
                user = new User()
                {
                    Email = model.Login,
                    Role = Role.User,
                    Password = HashPasswordHelper.HashPassowrd(model.Password),                    
                    TimeRegistration = DateTime.Now,
                };
                
                await userRepository.Create(user);
                logger.LogInformation($"[AccountService.Register]: {DateTime.Now} Register new user {user.Email}" +
                    $"\n-------------------------------------------------------------------------");
                var profile = new Profile()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    BirdDate = model.BirdDate,
                    Age = (byte)AgeHelper.GetAge(model.BirdDate),
                    User = user,
                    UserId = user.Id,
                };
                await profileRepository.Create(profile);
                logger.LogInformation($"[AccountService.Register]: {DateTime.Now} Created new profile. {user.Email}" +
                   $"\n-------------------------------------------------------------------------");
                var result = Authenticate(user);
                logger.LogInformation($"[AccountService.Register]: {DateTime.Now} User {user.Email} authenticate" +
                    $"\n----------------------------------------------------------------------------------------");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    Description = "Object added.",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"[AccountService.Register]: {DateTime.Now} error {ex.Message}" +
                    $"\n-----------------------------------------------------------");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                var user = await userRepository.GetAll().FirstOrDefaultAsync(x => x.Email == model.Login);
                if (user == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "User is not found."
                    };
                }
                if (user.Password != HashPasswordHelper.HashPassowrd(model.Password))
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Invalid password or login!"
                    };
                }
                var result = Authenticate(user);
                logger.LogInformation($"[AccountService.Login]: {DateTime.Now} User {user.Email} authenticate" +
                    $"\n----------------------------------------------------------------------------------------");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"[AccountService.Login]: {DateTime.Now} error {ex.Message}" +
                    $"\n----------------------------------------------------------");
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                var user = await userRepository.GetAll().FirstOrDefaultAsync(x => x.Email == model.UserLogin);
                if (user == null)
                {
                    return new BaseResponse<bool>()
                    {
                        StatusCode = StatusCode.UserNotFound,
                        Description = "User is not found."
                    };
                }
                user.Password = HashPasswordHelper.HashPassowrd(model.NewPassword);
                await userRepository.Update(user);
                logger.LogInformation($"[AccountService.ChangePassword]: {DateTime.Now} User {user.Email} change password" +
                    $"\n-------------------------------------------------------------------------------------------------");
                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK,
                    Description = "Password updated."
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"[ChangePassword]: {DateTime.Now} error {ex.Message}" +
                    $"\n--------------------------------------------------");
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        /// <summary>
        /// New authenticate.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private static ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
