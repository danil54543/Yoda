﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<IBaseResponse<Project>> Create(ProjectViewModel model)
        {
            try
            {
                var user = await userRepository.GetAll().FirstOrDefaultAsync(x => x.Email == model.Login);
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
                    DateCreated = DateTime.Now,
                    Category = model.Category,
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
                logger.LogInformation($"[ProjectService.Delete]: {DateTime.Now} User {project.User.Email} deleted project {project.Title}." +
                    $"\n---------------------------------------------------------------------------------------");
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
        //TODO:
        public async Task<IBaseResponse<Project>> Update(ProjectViewModel model)
        {
            try
            {
                var todo = await projectRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.Id);
                if (todo == null)
                {
                    return new BaseResponse<Project>()
                    {
                        Description = "Todo not found.",
                        StatusCode = StatusCode.ProjectNotFound
                    };
                }
                todo.Title = model.Title;

                await projectRepository.Update(todo);
                logger.LogInformation($"[ProjectService.Edit]: {DateTime.Now} User {todo.User.Email} edit todo {todo.Title}." +
                    $"\n----------------------------------------------------------------------------------");
                return new BaseResponse<Project>()
                {
                    Data = todo,
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


        public async Task<BaseResponse<IEnumerable<ProjectViewModel>>> GetProjects(string login)
        {
            try
            {
                var user = await userRepository.GetAll().FirstOrDefaultAsync(x => x.Email == login);
                if (user == null)
                {
                    logger.LogError($"[ProjectService.GetProjects]: {DateTime.Now} error User not found." +
                   $"\n----------------------------------------------");
                    return new BaseResponse<IEnumerable<ProjectViewModel>>()
                    {
                        Description = "User not found.",
                        StatusCode = StatusCode.UserNotFound
                    };
                }
                var projects = await projectRepository.GetAll().Where(p => p.UserId == user.Id).ToListAsync();
                var response = from t in projects
                               select new ProjectViewModel()
                               {
                                   Id = t.Id,
                                   Title = t.Title,
                                   DateCreated = t.DateCreated,
                                   Category = t.Category,
                               };
                logger.LogInformation($"[ProjectService.GetProjects]: {DateTime.Now} User {login} request projects." +
                    $"\n----------------------------------------------");
                return new BaseResponse<IEnumerable<ProjectViewModel>>()
                {
                    Data = response,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"[ProjectService.GetProjects]: {DateTime.Now} error {ex.Message}" +
                    $"\n----------------------------------------------");
                return new BaseResponse<IEnumerable<ProjectViewModel>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<ProjectViewModel>> GetProject(long id)
        {
            try
            {
                var project = await projectRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (project == null)
                {
                    logger.LogError($"[ProjectService.GetProject]: {DateTime.Now} error Todo not found." +
                   $"\n----------------------------------------------");
                    return new BaseResponse<ProjectViewModel>()
                    {
                        Description = "Project not found.",
                        StatusCode = StatusCode.ProjectNotFound
                    };
                }

                var data = new ProjectViewModel()
                {
                    Title = project.Title,
                    DateCreated = project.DateCreated,
                    Category = project.Category
                };
                logger.LogInformation($"[ProjectService.GetProject]: {DateTime.Now} User {project.UserId} request project." +
                    $"\n----------------------------------------------");
                return new BaseResponse<ProjectViewModel>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data,
                };
            }
            catch (Exception ex)
            {
                logger.LogError($"[ProjectService.GetProject]: {DateTime.Now} error Project internal server error." +
                   $"\n----------------------------------------------");
                return new BaseResponse<ProjectViewModel>()
                {
                    Description = $"[GetProject] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public BaseResponse<Dictionary<int, string>> GetTypes()
        {
            try
            {
                var types = ((ProjectCategory[])Enum.GetValues(typeof(ProjectCategory)))
                    .ToDictionary(k => (int)k, t => t.GetDisplayName());

                return new BaseResponse<Dictionary<int, string>>()
                {
                    Data = types,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}