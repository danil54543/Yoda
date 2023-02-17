using Microsoft.EntityFrameworkCore;
using Yoda.Domain.Enum;
using Yoda.Domain.Helper;
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("Users").HasKey(x => x.Id);

                builder.HasData(new User[]
                {
                    new User()
                    {
                        Id = 1,
                        Email = "Admin@gg.c",
                        Password = HashPasswordHelper.HashPassowrd("123456"),
                        Role = Role.Admin
                    },
                    new User()
                    {
                        Id = 2,
                        Email = "Moderator",
                        Password = HashPasswordHelper.HashPassowrd("654321"),
                        Role = Role.Moderator
                    }
                });
                builder.Property(x => x.Id).ValueGeneratedOnAdd();

                builder.Property(x => x.Password).IsRequired();
                builder.Property(x => x.Email).HasMaxLength(100).IsRequired();

                builder.HasOne(x => x.Profile)
                    .WithOne(x => x.User)
                    .HasPrincipalKey<User>(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);
                
            });
            modelBuilder.Entity<Todo>(builder =>
            {
                builder.ToTable("Todos").HasKey(x => x.Id);

                builder.HasOne(r => r.User).WithMany(t => t.TodoItems)
                    .HasForeignKey(r => r.UserId);
            });

        }
    }
}
