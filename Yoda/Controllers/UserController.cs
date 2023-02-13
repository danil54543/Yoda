using Microsoft.AspNetCore.Mvc;
using Yoda.Domain.Extension;
using Yoda.Domain.ViewModel.User;
using Yoda.Service.Interface;

namespace Yoda.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task<IActionResult> GetUsers()
        {
            var response = await userService.GetUsers();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> DeleteUser(long id)
        {
            var response = await userService.Delete(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetUsers");
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Save() => PartialView();

        [HttpPost]
        public async Task<IActionResult> Save(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await userService.Create(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return Json(new { description = response.Description });
                }
                return BadRequest(new { errorMessage = response.Description });
            }
            var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList().Join();
            return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage });
        }

        [HttpPost]
        public JsonResult GetRoles()
        {
            var types = userService.GetRoles();
            return Json(types.Data);
        }
    }
}
