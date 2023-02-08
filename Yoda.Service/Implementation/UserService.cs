using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Yoda.DAL.Interface;
using Yoda.DAL.Repository;
using Yoda.Domain.BaseResponse;
using Yoda.Domain.Enum;
using Yoda.Domain.Helper;
using Yoda.Domain.Model;
using Yoda.Domain.ViewModel.Account;
using Yoda.Domain.ViewModel.Profile;
using Yoda.Service.Interface;

namespace Yoda.Service.Implementation
{
	public class UserService : IUserService
	{
		private readonly ILogger<UserService> logger;
		private readonly IUserRepository userRepository;

		public UserService(ILogger<UserService> logger, IUserRepository userRepository)
		{
			this.logger = logger;
			this.userRepository = userRepository;
		}

		public async Task<IBaseResponse<User>> Create(UserViewModel model)
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
					FirstName ="null",
					LastName ="null",
					BirdDate= DateTime.Now,
					Age = (byte)AgeHelper.GetAge(DateTime.Now),
					IsVerified = true,
					TimeRegistration = DateTime.Now,
				};
				await userRepository.Create(user);
				return new BaseResponse<User>()
				{
					Data = user,
					Description = "User added.",
					StatusCode = StatusCode.OK
				};
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"[UserService.Create] error: {ex.Message}" +
					$"------------------------------------------------------");
				return new BaseResponse<User>()
				{
					StatusCode = StatusCode.InternalServerError,
					Description = $"Internal error: {ex.Message}"
				};
			}
		}

		public async Task<IBaseResponse<bool>> Delete(long id)
		{
			try
			{
				var user = userRepository.GetAll().FirstOrDefault(x=>x.Id==id);
				if (user == null)
				{
					return new BaseResponse<bool>()
					{
						Description = "User not found.",
						StatusCode = StatusCode.UserNotFound,
					};
				}
				await userRepository.Delete(user);
				return new BaseResponse<bool>()
				{
					Data = true,
					StatusCode = StatusCode.OK,
				};
			}
			catch(Exception ex)
			{
				logger.LogError(ex, $"[UserService.Delete] error: {ex.Message}" +
					$"--------------------------------------------------------");
				return new BaseResponse<bool>()
				{
					StatusCode = StatusCode.InternalServerError,
					Description = $"Internal error: {ex.Message}"
				};
			}
		}

		public BaseResponse<Dictionary<int, string>> GetRoles()
		{
			throw new NotImplementedException();
		}

		public Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsers()
		{
			throw new NotImplementedException();
		}
	}
}
