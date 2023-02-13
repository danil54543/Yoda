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

        public async Task<IBaseResponse<Todo>> Create(TodoViewModel model)
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
                logger.LogInformation($"[TodoService.Create]: {DateTime.Now} {user.Email} created new todo {todo.Title}." +
                    $"\n------------------------------------------------------------------------------");
                return new BaseResponse<Todo>()
                {
                    Description = "Todo created.",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"[TodoService.Create]: {DateTime.Now} error {ex.Message}." +
                    $"\n----------------------------------------------------------------------");
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
                        Description = "Todo not found.",
                        Data = false,
                    };
                }
                await todoRepository.Delete(todo);
                logger.LogInformation($"[TodoService.Delete]: {DateTime.Now} User {todo.User.Email} deleted todo {todo.Title}." +
                    $"\n---------------------------------------------------------------------------------------");
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.OK,
                    Description = "Todo deleted.",
                    Data = true,
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"[TodoService.Delete]: {DateTime.Now} error {ex.Message}." +
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
        public async Task<IBaseResponse<Todo>> Update(TodoViewModel model)
        {
            try
            {
                var todo = await todoRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.Id);
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
                logger.LogInformation($"[TodoService.Edit]: {DateTime.Now} User {todo.User.Email} edit todo {todo.Title}." +
                    $"\n----------------------------------------------------------------------------------");
                return new BaseResponse<Todo>()
                {
                    Data = todo,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"[TodoService.Edit]: {DateTime.Now} error {ex.Message}" +
                    $"\n----------------------------------------------");
                return new BaseResponse<Todo>()
                {
                    Description = $"[EditTodo] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }


        public async Task<BaseResponse<IEnumerable<TodoViewModel>>> GetTodos(string login)
        {
            try
            {
                var user = await userRepository.GetAll().FirstOrDefaultAsync(x => x.Email == login);
                if (user == null)
                {
                    logger.LogError($"[TodoService.GetTodos]: {DateTime.Now} error User not found." +
                   $"\n----------------------------------------------");
                    return new BaseResponse<IEnumerable<TodoViewModel>>()
                    {
                        Description = "User not found.",
                        StatusCode = StatusCode.UserNotFound
                    };
                }
                var todo = user.TodoItems;
                var response = from t in todo
                               select new TodoViewModel()
                               {
                                   Id = t.Id,
                                   Title = t.Title,
                                   DateCreate = t.DateCreate,
                                   Priority = t.Priority,
                                   Marker = t.Marker,
                               };
                logger.LogInformation($"[TodoService.GetTodos]: {DateTime.Now} User {login} getting todos." +
                    $"\n----------------------------------------------");
                return new BaseResponse<IEnumerable<TodoViewModel>>()
                {
                    Data = response,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"[TodoService.GetTodos]: {DateTime.Now} error {ex.Message}" +
                    $"\n----------------------------------------------");
                return new BaseResponse<IEnumerable<TodoViewModel>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<TodoViewModel>> GetTodo(long id)
        {
            try
            {
                var todo = await todoRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (todo == null)
                {
                    logger.LogError($"[TodoService.GetTodo]: {DateTime.Now} error Todo not found." +
                   $"\n----------------------------------------------");
                    return new BaseResponse<TodoViewModel>()
                    {
                        Description = "Todo not found.",
                        StatusCode = StatusCode.TodoNotFound
                    };
                }

                var data = new TodoViewModel()
                {
                    Title = todo.Title,
                    DateCreate = todo.DateCreate,
                    Priority = todo.Priority,
                    Marker = todo.Marker,
                    Item = todo.Item,
                };
                logger.LogInformation($"[TodoService.GetTodo]: {DateTime.Now} User {todo.UserId} getting todo." +
                    $"\n----------------------------------------------");
                return new BaseResponse<TodoViewModel>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data,
                };
            }
            catch (Exception ex)
            {
                logger.LogError($"[TodoService.GetTodo]: {DateTime.Now} error Todo internal server error." +
                   $"\n----------------------------------------------");
                return new BaseResponse<TodoViewModel>()
                {
                    Description = $"[GetTodo] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
