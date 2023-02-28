using Yoda.Domain.BaseResponse;
using Yoda.Domain.Model;
using Yoda.Domain.ViewModel.Account;
using Yoda.Domain.ViewModel.AdminAccount;
using Yoda.Domain.ViewModel.User;

namespace Yoda.Service.Interface
{
    /// <summary>
    /// Admin panel interface.
    /// </summary>
    public interface IUserService
	{
		/// <summary>
		/// Creating new user.
		/// </summary>
		Task<IBaseResponse<User>> Create(UserViewModelAdmin model);
		/// <summary>
		/// Get dictionary roles.
		/// </summary>
		/// <returns>Dictionary roles.</returns>
		BaseResponse<Dictionary<int, string>> GetRoles();
		/// <summary>
		/// Get list users.
		/// </summary>
		/// <returns>List users.</returns>
		Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsers();
		/// <summary>
		/// Deleting user.
		/// </summary>
		/// <param name="id">User id.</param>
		/// <returns>bool</returns>
		Task<IBaseResponse<bool>> Delete(long id);
	}
}
