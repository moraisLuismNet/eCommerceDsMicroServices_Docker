using UserService.DTOs;

namespace UserService.Services
{
    public interface ITokenService
    {
        LoginResponseDTO GenerateTokenService(UserLoginDTO user);
    }
}
