using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Yoda.DAL.Interface;
using Yoda.Domain.BaseResponse;
using Yoda.Domain.Enum;
using Yoda.Domain.Extension;
using Yoda.Domain.Helper;
using Yoda.Domain.Model;
using Yoda.Domain.ViewModel.AdminAccount;
using Yoda.Domain.ViewModel.User;
using Yoda.Service.Interface;

namespace Yoda.Service.Implementation
{
    public class UserService : IUserService
	{
		private readonly ILogger<UserService> logger;
		private readonly IUserRepository userRepository;
		private readonly IProfileRepository profileRepository;

		public UserService(ILogger<UserService> logger, IUserRepository userRepository, IProfileRepository profileRepository)
		{
			this.logger = logger;
			this.userRepository = userRepository;
			this.profileRepository = profileRepository;
		}

		public async Task<IBaseResponse<User>> Create(UserViewModelAdmin model)
		{
			try
			{
				var user = await userRepository.GetAll().FirstOrDefaultAsync(x => x.Email == model.Login);
				if (user != null)
				{
					return new BaseResponse<User>()
					{
						Description = "There is already a user with this login.",
						StatusCode = StatusCode.UserAlreadyExists
					};
				}
				user = new User()
				{
					Email = model.Login,
					Role = Enum.Parse<Role>(model.Role),
					Password = HashPasswordHelper.HashPassowrd(model.Password),
					IsVerified = true,
					TimeRegistration = DateTime.Now,
				};
                await userRepository.Create(user);
                logger.LogInformation($"[UserService.Create]: {DateTime.Now} New account {user.Email} created." +
                    $"\n-------------------------------------------------------------------------");
                var profile = new Profile()
				{
					FirstName = model.FirstName,
					LastName = model.LastName,
					BirdDate = model.BirdDate,
					Age = (byte)AgeHelper.GetAge(DateTime.Now),
					UserId = user.Id,
				};
				
				await profileRepository.Create(profile);
                logger.LogInformation($"[UserService.Create]: {DateTime.Now} Create profile {user.Email}." +
                    $"\n-------------------------------------------------------------------------");
                return new BaseResponse<User>()
				{
					Data = user,
					Description = "User added.",
					StatusCode = StatusCode.OK
				};
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"[UserService.Create]: {DateTime.Now} error {ex.Message}." +
					$"\n------------------------------------------------------");
				return new BaseResponse<User>()
				{
					StatusCode = StatusCode.InternalServerError,
					Description = $"Internal error: {ex.Message}."
				};
			}
		}

		public async Task<IBaseResponse<bool>> Delete(long id)
		{
			try
			{
				var user = userRepository.GetAll().FirstOrDefault(x => x.Id == id);
				if (user == null)
				{
					return new BaseResponse<bool>()
					{
						Description = "User not found.",
						StatusCode = StatusCode.UserNotFound,
						Data = false,
					};
				}
				await userRepository.Delete(user);
				logger.LogInformation($"[UserService.Delete]: {DateTime.Now} Account {user.Email} deleted." +
					$"\n-------------------------------------------------------------------");
				return new BaseResponse<bool>()
				{
					Data = true,
					StatusCode = StatusCode.OK,
				};
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"[UserService.Delete]: {DateTime.Now} error {ex.Message}" +
					$"\n--------------------------------------------------------");
				return new BaseResponse<bool>()
				{
					StatusCode = StatusCode.InternalServerError,
					Description = $"Internal error: {ex.Message}.",
					Data = false,
				};
			}
		}

		public BaseResponse<Dictionary<int, string>> GetRoles()
		{
			try
			{
				var roles = ((Role[])Enum.GetValues(typeof(Role)))
				   .ToDictionary(k => (int)k, t => t.GetDisplayName());
				logger.LogInformation($"[UserService.GetRoles]: {DateTime.Now}" +
					"\n--------------------------------------");
				return new BaseResponse<Dictionary<int, string>>()
				{
					Data = roles,
					StatusCode = StatusCode.OK
				};
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"[UserService.GetRoles]: {DateTime.Now} error {ex.Message}" +
					$"\n--------------------------------------------------------");
				return new BaseResponse<Dictionary<int, string>>()
				{
					Description = ex.Message,
					StatusCode = StatusCode.InternalServerError
				};
			}
		}

		public async Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsers()
		{
			try
			{
				var users = await userRepository.GetAll()
				   .Select(x => new UserViewModel()
				   {
					   Id = x.Id,
					   Login = x.Email,
					   Role = x.Role.GetDisplayName()
				   })
				   .ToListAsync();
				logger.LogInformation($"[UserService.GetUsers]: {DateTime.Now} items received {users.Count}." +
					$"\n--------------------------------------------------------------------");
				return new BaseResponse<IEnumerable<UserViewModel>>()
				{
					Data = users,
					StatusCode = StatusCode.OK
				};

			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"[UserSerivce.GetUsers]: {DateTime.Now} error {ex.Message}." +
					$"\n---------------------------------------------------------");
				return new BaseResponse<IEnumerable<UserViewModel>>()
				{
					StatusCode = StatusCode.InternalServerError,
					Description = $"Internal error: {ex.Message}."
				};
			}
		}
	}
}
