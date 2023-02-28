using Microsoft.AspNetCore.Mvc;
using Yoda.Domain.ViewModel.Project;
using Yoda.Service.Interface;

namespace Yoda.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService projectService;

        public ProjectController(IProjectService projectService)
        {
            this.projectService = projectService;
        }
        [HttpGet]
        public IActionResult Create()
        {
            var login = User.Identity.Name;
            //if (login == string.Empty)
            //{
            //    return RedirectToAction("Error", "Home");
            //}

            var model = new ProjectViewModel()
            {
                Login = login,
                DateCreated = DateTime.Now,
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProjectViewModel model)
        {

            if(ModelState.IsValid)
            {
                var response = await projectService.Create(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return Json(new { description = response.Description });
                }
            }      
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        public async Task<IActionResult> Delete(long id)
        {
            var response = await projectService.Delete(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Projects", "Project");
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
            var response = await projectService.GetProject(id);
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
                var response = await projectService.Update(model);
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
            var response = await projectService.GetProjects(User.Identity.Name);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data.ToList());
            }
            return RedirectToAction("Error", "Home");
        }
        //TODO: Make method "GetProject".
        [HttpPost]
        public JsonResult GetCategories()
        {
            var types = projectService.GetCategories();
            return Json(types.Data);
        }
    }
}
