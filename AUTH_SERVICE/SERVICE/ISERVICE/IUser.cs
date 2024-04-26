using AUTH_SERVICE.Models.DTOS;

namespace AUTH_SERVICE.SERVICE.ISERVICE
{
    public interface IUser
    {
        Task<string> RegisterUser(RegisterUserDto registerUserDto);

        Task<LoginResponseDto> loginUser(LoginRequestDto loginRequestDto);

        Task<bool> AssignUserRoles(string Email, string RoleName);
    }
}
