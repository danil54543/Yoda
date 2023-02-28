using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Yoda.DAL.Interface;
using Yoda.Domain.BaseResponse;
using Yoda.Domain.Enum;
using Yoda.Domain.Extension;
using Yoda.Domain.Model;
using Yoda.Domain.ViewModel.Project;
using Yoda.Service.Interface;

namespace Yoda.Service.Implementation
{
    public class ProjectService : IProjectService
    {
        private readonly IUserRepository userRepository;
        private readonly IProjectRepository projectRepository;
        private readonly ILogger<ProjectService> logger;


        public ProjectService(IUserRepository userRepository, IProjectRepository projectRepository, ILogger<ProjectService> logger)
        {
            this.userRepository = userRepository;
            this.projectRepository = projectRepository;
            this.logger = logger;
        }

        public async Task<IBaseResponse<Project>> Create(ProjectCreateViewModel model,string login)
        {
            try
            {
                var user = await userRepository.GetAll().FirstOrDefaultAsync(x => x.Email == login);
                if (user == null)
                {
                    return new BaseResponse<Project>()
                    {
                        Description = "User is not found.",
                        StatusCode = StatusCode.UserNotFound
                    };
                }
                var project = new Project()
                {
                    Title = model.Title,
                    DateCreated = model.DateCreated,
                    ConpanyType = Enum.Parse<ConpanyType>(model.ConpanyType),
                    City= model.City,
                    Country = model.Country,
                    UserId = user.Id,
                };
                await projectRepository.Create(project);
                logger.LogInformation($"[ProjectService.Create]: {DateTime.Now} {user.Email} created new project {project.Title}." +
                    $"\n------------------------------------------------------------------------------");
                return new BaseResponse<Project>()
                {
                    Description = "Project created.",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"[ProjectService.Create]: {DateTime.Now} error {ex.Message}." +
                    $"\n----------------------------------------------------------------------");
                return new BaseResponse<Project>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> Delete(long id)
        {
            try
            {
                var project = projectRepository.GetAll().FirstOrDefault(x => x.Id == id);
                if (project == null)
                {
                    return new BaseResponse<bool>()
                    {
                        StatusCode = StatusCode.ProjectNotFound,
                        Description = "Project not found.",
                        Data = false,
                    };
                }
                await projectRepository.Delete(project);
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.OK,
                    Description = "Project deleted.",
                    Data = true,
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"[ProjectService.Delete]: {DateTime.Now} error {ex.Message}." +
                    $"\n-----------------------------------------");
                return new BaseResponse<bool>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError,
                    Data = false,
                };
            }
        }
        //TODO: Update project service.
        public async Task<IBaseResponse<Project>> Update(ProjectCreateViewModel model)
        {
            try
            {
                var project = await projectRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.Id);
                if (project == null)
                {
                    return new BaseResponse<Project>()
                    {
                        Description = "Todo not found.",
                        StatusCode = StatusCode.ProjectNotFound
                    };
                }
                //project.Title = model.Title;
                //project.Logo = model.Logo;
                //project.Street = model.Street;
                //project.Email = model.Email;
                //project.Build = model.Build;
                //project.City = model.City;
                //project.Country = model.Country;
                //project.PhoneNum = model.PhoneNum;
                //project.ConpanyType = Enum.Parse<ProjectCategory>(model.ConpanyType);

                await projectRepository.Update(project);
                logger.LogInformation($"[ProjectService.Edit]: {DateTime.Now} User {project.User.Email} edit todo {project.Title}." +
                    $"\n----------------------------------------------------------------------------------");
                return new BaseResponse<Project>()
                {
                    Data = project,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"[ProjectService.Edit]: {DateTime.Now} error {ex.Message}" +
                    $"\n----------------------------------------------");
                return new BaseResponse<Project>()
                {
                    Description = $"[ProjectService.Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }


        public async Task<BaseResponse<IEnumerable<ProjectInfoViewModel>>> GetProjects(string login)
        {
            try
            {
                var user = await userRepository.GetAll().Include(x=>x.Projects).FirstOrDefaultAsync(x=>x.Email==login);
                if (user == null)
                {
                    logger.LogError($"[ProjectService.GetProjects]: {DateTime.Now} error User not found." +
                   $"\n----------------------------------------------");
                    return new BaseResponse<IEnumerable<ProjectInfoViewModel>>()
                    {
                        Description = "User not found.",
                        StatusCode = StatusCode.UserNotFound
                    };
                }
                var projects = user.Projects;
                var response = from t in projects
                               select new ProjectInfoViewModel()
                               {
                                   Id = t.Id,
                                   Title = t.Title,
                                   DateCreated = t.DateCreated,
                                   ConpanyType = t.ConpanyType.GetDisplayName(),
                                   Country= t.Country,
                                   City = t.City,
                               };
                logger.LogInformation($"[ProjectService.GetProjects]: {DateTime.Now} User {login} request projects." +
                    $"\n----------------------------------------------");
                return new BaseResponse<IEnumerable<ProjectInfoViewModel>>()
                {
                    Data = response,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"[ProjectService.GetProjects]: {DateTime.Now} error {ex.Message}" +
                    $"\n----------------------------------------------");
                return new BaseResponse<IEnumerable<ProjectInfoViewModel>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<ProjectCreateViewModel>> GetProject(long id)
        {
            try
            {
                var project = await projectRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (project == null)
                {
                    logger.LogError($"[ProjectService.GetProject]: {DateTime.Now} error Project not found." +
                   $"\n----------------------------------------------");
                    return new BaseResponse<ProjectCreateViewModel>()
                    {
                        Description = "Project not found.",
                        StatusCode = StatusCode.ProjectNotFound
                    };
                }

                var data = new ProjectCreateViewModel()   
                {
                    Title = project.Title,
                    DateCreated = project.DateCreated,
                    //TODO:
                    //Category = project.Category.GetDisplayName()
                };
                logger.LogInformation($"[ProjectService.GetProject]: {DateTime.Now} User {project.UserId} request project." +
                    $"\n----------------------------------------------");
                return new BaseResponse<ProjectCreateViewModel>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data,
                };
            }
            catch (Exception ex)
            {
                logger.LogError($"[ProjectService.GetProject]: {DateTime.Now} error Project internal server error." +
                   $"\n----------------------------------------------");
                return new BaseResponse<ProjectCreateViewModel>()
                {
                    Description = $"[GetProject] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        //public BaseResponse<Dictionary<int, string>> GetCategories()
        //{
        //    try
        //    {
        //        var types = ((ProjectCategory[])Enum.GetValues(typeof(ProjectCategory)))
        //            .ToDictionary(k => (int)k, t => t.GetDisplayName());

        //        return new BaseResponse<Dictionary<int, string>>()
        //        {
        //            Data = types,
        //            StatusCode = StatusCode.OK
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new BaseResponse<Dictionary<int, string>>()
        //        {
        //            Description = ex.Message,
        //            StatusCode = StatusCode.InternalServerError
        //        };
        //    }
        //}
    }
}
