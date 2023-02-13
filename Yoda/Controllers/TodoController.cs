using Microsoft.AspNetCore.Mvc;
using Yoda.Domain.ViewModel.Todo;
using Yoda.Service.Interface;

namespace Yoda.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoService todoService;

        public TodoController(ITodoService todoService)
        {
            this.todoService = todoService;
        }
        [HttpGet]
        public IActionResult Create()
        {
            var login = User.Identity.Name;
            if (login == string.Empty)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new TodoViewModel()
            {
                Login = login,
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(TodoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await todoService.Create(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return Json(new { description = response.Description });
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        public async Task<IActionResult> Delete(long id)
        {
            var response = await todoService.Delete(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Todos", "Todo");
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<IActionResult> Update(long id)
        {
            if (id == 0)
            {
                return PartialView();
            }
            var response = await todoService.GetTodo(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return PartialView(response.Data);
            }
            ModelState.AddModelError("", response.Description);
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> Update(TodoViewModel model)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                var response = await todoService.Update(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return Json(new { description = response.Description });
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        [HttpGet]
        public async Task<IActionResult> Todos()
        {
            var response = await todoService.GetTodos(User.Identity.Name);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data.ToList());
            }
            return RedirectToAction("Todos", "Todo");
        }
        //TODO: Make method "GetTodo".
    }
}
