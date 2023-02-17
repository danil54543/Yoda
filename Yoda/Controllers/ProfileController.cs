using Microsoft.AspNetCore.Mvc;
using Yoda.Domain.ViewModel.Profile;
using Yoda.Service.Interface;

namespace Yoda.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService profileService;

        public ProfileController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProfileViewModel model)
        {

            if (ModelState.IsValid)
            {
                var response = await profileService.Update(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return Json(new { description = response.Description });
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        public async Task<IActionResult> GetProfile()
        {
            var login = User.Identity.Name;
            if (login == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var response = await profileService.GetProfile(login);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View();
        }
    }
}
