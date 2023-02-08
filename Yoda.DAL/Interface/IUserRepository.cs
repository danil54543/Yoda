using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoda.Domain.Model;

namespace Yoda.DAL.Interface
{
	/// <summary>
	/// User repository.
	/// </summary>
	public interface IUserRepository : IBaseRepository<User>
	{
		/// <summary>
		/// Get all users from database.
		/// </summary>
		IQueryable<User> GetAll();
	}
}
