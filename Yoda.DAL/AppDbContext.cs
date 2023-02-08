using Microsoft.EntityFrameworkCore;
using Yoda.Domain.Model;

namespace Yoda.DAL
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> optionsBuilder) : base(optionsBuilder)
		{
			Database.EnsureCreated();
		}
		public DbSet<User> Users { get; set; } = null!;
		public DbSet<Todo> Todos { get; set; } = null!;
	}
}
