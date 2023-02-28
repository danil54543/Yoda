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
        Task<IBaseResponse<Project>> Create(ProjectCreateViewModel model, string login);
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
        Task<IBaseResponse<Project>> Update(ProjectCreateViewModel model);
        Task<BaseResponse<IEnumerable<ProjectInfoViewModel>>> GetProjects(string login);

        Task<IBaseResponse<ProjectCreateViewModel>> GetProject(long id);
        //BaseResponse<Dictionary<int, string>> GetCategories();
    }
}
