using Microsoft.AspNetCore.Mvc;
using Yoda.Domain.ViewModel.Project;
using Yoda.Service.Interface;

namespace Yoda.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService todoService;

        public ProjectController(IProjectService todoService)
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

            var model = new ProjectViewModel()
            {
                Login = login,
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProjectViewModel model)
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
            var response = await todoService.GetProject(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return PartialView(response.Data);
            }
            ModelState.AddModelError("", response.Description);
            return PartialView();
        }
        [HttpPost]
        public async Task<IActionResult> Update(ProjectViewModel model)
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
        public async Task<IActionResult> Projects()
        {
            var response = await todoService.GetProjects(User.Identity.Name);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data.ToList());
            }

            return RedirectToAction("Error", "Shared");
        }
        //TODO: Make method "GetProject".
        [HttpPost]
        public JsonResult GetTypes()
        {
            var types = todoService.GetTypes();
            return Json(types.Data);
        }
    }
}
