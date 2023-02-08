using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Yoda.DAL.Interface;
using Yoda.Domain.BaseResponse;
using Yoda.Domain.Enum;
using Yoda.Domain.Model;
using Yoda.Domain.ViewModel.Todo;
using Yoda.Service.Interface;

namespace Yoda.Service.Implementation
{
	public class TodoService : ITodoService
	{
		private readonly IUserRepository userRepository;
		private readonly ITodoRepository todoRepository;
		private readonly ILogger<TodoService> logger;

		public TodoService(IUserRepository userRepository, ITodoRepository todoRepository, ILogger<TodoService> logger)
		{
			this.userRepository = userRepository;
			this.todoRepository = todoRepository;
			this.logger = logger;
		}

		public async Task<IBaseResponse<Todo>> Create(CreateTodoViewModel model)
		{
			try
			{
				var user = await userRepository.GetAll().FirstOrDefaultAsync(x => x.Email == model.Login);
				if (user == null)
				{
					return new BaseResponse<Todo>()
					{
						Description = "User is not found.",
						StatusCode = StatusCode.UserNotFound
					};
				}
				var todo = new Todo
				{
					Title = model.Title,
					Item = model.Item,
					DateCreate = DateTime.Now,
					Priority = model.Priority,
					Marker = model.Marker,
					UserId = user.Id,
				};
				await todoRepository.Create(todo);
				return new BaseResponse<Todo>()
				{
					Description = "Note created.",
					StatusCode = StatusCode.OK
				};
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"[CreateTodo]: {ex.Message}" +
					$"-----------------------------------");
				return new BaseResponse<Todo>()
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
				var todo = todoRepository.GetAll().FirstOrDefault(x => x.Id == id);
				if (todo == null)
				{
					return new BaseResponse<bool>()
					{
						StatusCode = StatusCode.TodoNotFound,
						Description = "Todo not found."
					};
				}
				await todoRepository.Delete(todo);
				return new BaseResponse<bool>()
				{
					StatusCode = StatusCode.OK,
					Description = "Todo deleted."
				};
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"[DeleteTodo]: {ex.Message}" +
					$"-----------------------------------");
				return new BaseResponse<bool>()
				{
					Description = ex.Message,
					StatusCode = StatusCode.InternalServerError
				};
			}
		}

		public async Task<IBaseResponse<Todo>> Edit(long id, EditTodoViewModel model)
		{
			try
			{
				var todo = await todoRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
				if (todo == null)
				{
					return new BaseResponse<Todo>()
					{
						Description = "Todo not found.",
						StatusCode = StatusCode.TodoNotFound
					};
				}
				todo.Title = model.Title;
				todo.Priority = model.Priority;
				todo.Marker = model.Marker;
				todo.Item = model.Item;
				await todoRepository.Update(todo);
				return new BaseResponse<Todo>()
				{
					Data = todo,
				    StatusCode = StatusCode.OK,
				};
			}
			catch (Exception ex)
			{
				logger.LogError(ex, $"[EditTodo]: {ex.Message}" +
					$"-----------------------------------");
				return new BaseResponse<Todo>()
				{
					Description = $"[EditTodo] : {ex.Message}",
					StatusCode = StatusCode.InternalServerError
				};
			}
		}
	}
}
