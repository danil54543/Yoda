using Microsoft.EntityFrameworkCore;
using Yoda.DAL.Interface;
using Yoda.Domain.Model;

namespace Yoda.DAL.Repository
{
	/// <summary>
	/// TodoItem repository.
	/// </summary>
	public class TodoRepository : ITodoRepository
	{
		/// <summary>
		/// Database context instance.
		/// </summary>

		private readonly AppDbContext db;

		public TodoRepository(AppDbContext dbContext)
		{
			db = dbContext;
		}


		/// <summary>
		/// Adding todo to database.
		/// </summary>
		/// <param name="entity">Todo</param>
		public async Task Create(Todo entity)
		{
			await db.Todos.AddAsync(entity);
			await db.SaveChangesAsync();
		}


		/// <summary>
		/// Delete todo from database.
		/// </summary>
		/// <param name="entity">Todo</param>
		public async Task Delete(Todo entity)
		{
			db.Todos.Remove(entity);
			await db.SaveChangesAsync();
		}

		public async Task<IEnumerable<Todo>> GetAll(long userId)
		{
			return await db.Todos.Where(x => x.UserId == userId).ToArrayAsync();
		}



		/// <summary>
		/// Updating todo data in database.
		/// </summary>
		/// <param name="entity">Todo</param>
		public async Task<Todo> Update(Todo entity)
		{
			db.Todos.Update(entity);
			await db.SaveChangesAsync();
			return entity;
		}
	}
}
