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
        public DbSet<Project> Projects { get; set; } = null!;
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
                        Email = "Admin@test.com",
                        Password = HashPasswordHelper.HashPassowrd("123456"),
                        Role = Role.Admin,
                        IsVerified = true,
                    },
                    new User()
                    {
                        Id = 2,
                        Email = "Moderator@test.com",
                        Password = HashPasswordHelper.HashPassowrd("654321"),
                        Role = Role.Moderator,
                        IsVerified = true,
                    }
                });
                builder.Property(x => x.Id).ValueGeneratedOnAdd();

                builder.Property(x => x.Password).IsRequired();
                builder.Property(x => x.Email).HasMaxLength(100).IsRequired();

                builder.HasOne(x => x.Profile)
                    .WithOne(x => x.User)
                    .HasPrincipalKey<User>(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasMany(x => x.Projects)
                    .WithOne(x => x.User)
                    .HasPrincipalKey(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);

                    
            });
            modelBuilder.Entity<Profile>(builder =>
            {
                builder.ToTable("UserProfiles").HasKey(x => x.Id);

                builder.HasData(new Profile[]
                {
                    new Profile()
                    {
                        Id = 1,
                        FirstName = "Admin",
                        LastName = "Admin",
                        BirdDate = DateTime.Today,
                        Age = 30,
                        UserId= 1,
                    },
                    new Profile()
                    {
                        Id = 2,
                        FirstName = "Moderator",
                        LastName = "Moderator",
                        BirdDate = DateTime.Today,
                        Age = 30,
                        UserId= 2,
                    }
                });
            });
            modelBuilder.Entity<Project>(builder => 
            {
                builder.ToTable("Project").HasKey(x=>x.Id);

                builder.HasData(new Project[]
                {
                    new Project()
                    {
                        Id = 1,
                        Title = "Test",
                        DateCreated = DateTime.Now,
                        Category = ProjectCategory.BaberShop,
                        Country = "Ukraine",
                        City = "Kyiv",
                        Street = "Soborny",
                        Build = 12,
                        PhoneNum = "+380990763546",
                        Email = "danil54543@gmail.com",
                        UserId = 1,
                    }
                });
            });
        }
    }
}
