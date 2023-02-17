using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yoda.DAL.Interface;
using Yoda.DAL.Repository;
using Yoda.Domain.BaseResponse;
using Yoda.Domain.Enum;
using Yoda.Domain.Helper;
using Yoda.Domain.Model;
using Yoda.Domain.ViewModel.Profile;
using Yoda.Service.Interface;

namespace Yoda.Service.Implementation
{
    public class ProfileService : IProfileService
    {
        private readonly ILogger<ProfileService> logger;
        private readonly IProfileRepository profileRepository;
        private readonly IUserRepository userRepository;

        public ProfileService(IProfileRepository profileRepository,ILogger<ProfileService> logger, IUserRepository userRepository)
        {
            this.profileRepository = profileRepository;
            this.logger = logger;
            this.userRepository = userRepository;
        }

        public async Task<BaseResponse<ProfileViewModel>> GetProfile(string login)
        {
            try
            {
                var profile = await profileRepository.GetAll()
                    .Select(x => new ProfileViewModel()
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        BirdDate = x.BirdDate,
                        Age = (byte)AgeHelper.GetAge(x.BirdDate),
                        Login = x.User.Email
                    })
                    .FirstOrDefaultAsync(x => x.Login == login);
                if (profile == null)
                {
                    logger.LogError($"[ProfileService.GetProfile] error: Profile not found.");
                    return new BaseResponse<ProfileViewModel>()
                    {
                        StatusCode = StatusCode.ProfileNotFound,
                        Description = $"Internal error: Profile not found."
                    };
                }
               
                logger.LogInformation($"[ProfileService.GetProfile] Getting profile.");

                return new BaseResponse<ProfileViewModel>()
                {
                    Data = profile,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"[ProfileService.GetProfile] error: {ex.Message}");
                return new BaseResponse<ProfileViewModel>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Internal error: {ex.Message}"
                };
            }
        }

        public async Task<BaseResponse<Profile>> Update(ProfileViewModel model)
        {
            try
            {
                var profile = await profileRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.Id == model.Id);
                if(profile == null)
                {
                    logger.LogError($"[ProfileService.Save] error: Internal error.");
                    return new BaseResponse<Profile>
                    {
                        Description = "Internal error",
                        StatusCode = StatusCode.InternalServerError,
                    };
                }
                profile.FirstName = model.FirstName;
                profile.LastName = model.LastName;
                profile.BirdDate = model.BirdDate;
                //TODO: Update image.

                await profileRepository.Update(profile);
                logger.LogInformation($"[ProfileService.Save] Update data.");
                return new BaseResponse<Profile>()
                {
                    Data = profile,
                    Description = "Update data.",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"[ProfileService.Save] error: {ex.Message}");
                return new BaseResponse<Profile>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Internal error: {ex.Message}"
                };
            }
        }
    }
}
