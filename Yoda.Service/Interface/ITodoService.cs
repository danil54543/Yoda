using Yoda.Domain.BaseResponse;
using Yoda.Domain.Model;
using Yoda.Domain.ViewModel.Todo;

namespace Yoda.Service.Interface
{
	/// <summary>
	/// Todo service: Create, Delete, Edit.
	/// </summary>
	public interface ITodoService
	{
		/// <summary>
		/// Adding new todo.
		/// </summary>
		/// <param name="model"></param>
		/// <returns>Todo data.</returns>
		Task<IBaseResponse<Todo>> Create(TodoViewModel model);
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
		Task<IBaseResponse<Todo>> Update(TodoViewModel model);
		Task<BaseResponse<IEnumerable<TodoViewModel>>> GetTodos(string login);

        Task<IBaseResponse<TodoViewModel>> GetTodo(long id);
		BaseResponse<Dictionary<int, string>> GetTypes();
    }
}
