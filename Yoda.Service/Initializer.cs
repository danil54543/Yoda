using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Yoda.DAL.Interface;
using Yoda.DAL.Repository;
using Yoda.Service.Implementation;
using Yoda.Service.Interface;

namespace Yoda.Service
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
        }
        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProfileService, ProfileService>();
        }
        
    }
}
