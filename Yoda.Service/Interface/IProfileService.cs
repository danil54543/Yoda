using Yoda.Domain.BaseResponse;
using Yoda.Domain.Model;
using Yoda.Domain.ViewModel.Profile;

namespace Yoda.Service.Interface
{
    public interface IProfileService
    {
        Task<BaseResponse<ProfileViewModel>> GetProfile(string login);

        Task<BaseResponse<Profile>> Update(ProfileViewModel model, byte[] imageData);
    }
}
