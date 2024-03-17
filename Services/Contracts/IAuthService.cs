using Entities.Dtos;
using Microsoft.AspNetCore.Identity;

namespace Services.Contracts
{
    public interface IAuthService
    {
        IEnumerable<IdentityRole> Roles { get; }

        IEnumerable<IdentityUser> GetAllUsers();
        Task<IdentityResult> CreateUser(UserDtoForCreation userDtoForCreation);

        Task<IdentityUser> GetOneUser(string userName);

        Task<UserDtoForUpdate> GetOneUserForUpdate(string userName);

        Task Update(UserDtoForUpdate userDto);

        Task<IdentityResult> ResetPassword (ResetPasswordDto resetPasswordDto);
        Task<IdentityResult> DeleteOneUser (string userName);

    }
}