using NLog.Fluent;
using Yoda.Domain.BaseResponse;
using Yoda.Domain.Model;
using Yoda.Domain.ViewModel.Project;

namespace Yoda.Service.Interface
{
    /// <summary>
    /// Todo service: Create, Delete, Edit.
    /// </summary>
    public interface IProjectService
    {
        /// <summary>
        /// Adding new todo.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Todo data.</returns>
        Task<IBaseResponse<Project>> Create(ProjectViewModel model);
        /// <summary>
        /// Delete todo.
        /// </summary>
        /// <param name="id">Todo id.</param>
        Task<IBaseResponse<bool>> Delete(long id);
        /// <summary>
        /// Edit todo.
        /// </summary>
        /// <param name="id">Todo id.</param>
        /// <param name="model">Updating todo data.</param>
        Task<IBaseResponse<Project>> Update(ProjectViewModel model);
        Task<BaseResponse<IEnumerable<ProjectViewModel>>> GetProjects(string login);

        Task<IBaseResponse<ProjectViewModel>> GetProject(long id);
        BaseResponse<Dictionary<int, string>> GetCategories();
    }
}
