using System.Security.Claims;
using Yoda.Domain.BaseResponse;
using Yoda.Domain.ViewModel.Account;

namespace Yoda.Service.Interface
{
	/// <summary>
	/// Account service: Register, Login, ChangePassword.
	/// </summary>
	public interface IAccountService
	{
		/// <summary>
		/// New user registration.
		/// </summary>
		Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);
		/// <summary>
		/// Account login.
		/// </summary>
		Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
		/// <summary>
		/// Changing account password.
		/// </summary>
		Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel model);
	}
}
