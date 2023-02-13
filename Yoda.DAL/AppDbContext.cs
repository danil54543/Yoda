using Microsoft.EntityFrameworkCore;
using Yoda.Domain.Model;

namespace Yoda.DAL
{
	/// <summary>
	/// Database context. 
	/// </summary>
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> optionsBuilder) : base(optionsBuilder)
		{
			Database.EnsureCreated();
		}
		public DbSet<User> Users { get; set; } = null!;
		public DbSet<Todo> Todos { get; set; } = null!;
		public DbSet<Profile> Profiles { get; set; } = null!;
	}
}
