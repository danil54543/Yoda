using Microsoft.EntityFrameworkCore;
using Yoda.DAL.Interface;
using Yoda.Domain.Model;

namespace Yoda.DAL.Repository
{
	/// <summary>
	/// TodoItem repository.
	/// </summary>
	public class ProjectRepository : IProjectRepository
    {
		/// <summary>
		/// Database context instance.
		/// </summary>
		private readonly AppDbContext db;

		public ProjectRepository(AppDbContext dbContext)
		{
			db = dbContext;
		}

		/// <summary>
		/// Adding todo to database.
		/// </summary>
		/// <param name="entity">User.</param>
		public async Task Create(Project entity)
		{
			await db.Projects.AddAsync(entity);
			await db.SaveChangesAsync();
		}
		/// <summary>
		/// Delete todo from database.
		/// </summary>
		/// <param name="entity">User.</param>
		public async Task Delete(Project entity)
		{
			db.Projects.Remove(entity);
			await db.SaveChangesAsync();
		}

		/// <summary>
		/// Get all todos from database.
		/// </summary>
		public IQueryable<Project> GetAll()
		{
			return db.Projects;
		}

		/// <summary>
		/// Updating todo in database.
		/// </summary>
		/// <param name="entity">User.</param>
		public async Task<Project> Update(Project entity)
		{
			db.Projects.Update(entity);
			await db.SaveChangesAsync();
			return entity;
		}
	}
}
