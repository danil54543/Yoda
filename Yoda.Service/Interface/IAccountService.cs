using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Yoda.Domain.BaseResponse;
using Yoda.Domain.ViewModel.Account;

namespace Yoda.Service.Interface
{
	public interface IAccountService
	{
		Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);

		Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);

		//TODO: Смена пароля.
		//Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel model);
	}
}
