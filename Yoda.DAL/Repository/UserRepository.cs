using Yoda.DAL.Interface;
using Yoda.Domain.Model;

namespace Yoda.DAL.Repository
{
	/// <summary>
	/// User repository.
	/// </summary>
	public class UserRepository : IUserRepository
	{
		/// <summary>
		/// Database context instance.
		/// </summary>
		private readonly AppDbContext db;

		public UserRepository(AppDbContext dbContext)
		{
			db = dbContext;
		}


		/// <summary>
		/// Get all users from database.
		/// </summary>
		public IQueryable<User> GetAll()
		{
			return db.Users;
		}


		/// <summary>
		/// Delete user from database.
		/// </summary>
		/// <param name="entity">User.</param>
		public async Task Delete(User entity)
		{
			db.Users.Remove(entity);
			await db.SaveChangesAsync();
		}


		/// <summary>
		/// Adding user to database.
		/// </summary>
		/// <param name="entity">User.</param>
		public async Task Create(User entity)
		{
			await db.Users.AddAsync(entity);
			await db.SaveChangesAsync();
		}


		/// <summary>
		/// Updating user in database.
		/// </summary>
		/// <param name="entity">User.</param>
		public async Task<User> Update(User entity)
		{
			db.Users.Update(entity);
			await db.SaveChangesAsync();
			return entity;
		}
	}
}
