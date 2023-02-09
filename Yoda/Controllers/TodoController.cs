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
			var model = new CreateTodoViewModel()
			{
				Login = User.Identity.Name,
			};
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> Create(CreateTodoViewModel model)
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
				return RedirectToAction("Todo", "GetTodos");
			}
			return View("Error", $"{response.Description}");
		}

		//[HttpGet]//TODO: Возможно неправильно.
		//public async Task<IActionResult> Edit(long id)
		//{
		//	var response = await todoService.GetItem(id);
		//	if (response.StatusCode != Domain.Enum.StatusCode.OK)
		//	{
		//		return View("Error", $"{response.Description}");
		//	}
		//	var model = new EditTodoViewModel()
		//	{
		//		Title = response.Data.Title,
		//		Item = response.Data.Item,
		//		Marker = response.Data.Marker,
		//		Priority = response.Data.Priority,
		//	};
		//	return View(model);
		//}
		[HttpPost]
		public async Task<IActionResult> Todos() 
		{
            var response = await todoService.GetItems(User.Identity.Name);         
            return View(response.Data.ToList());       
        }
	}
}
