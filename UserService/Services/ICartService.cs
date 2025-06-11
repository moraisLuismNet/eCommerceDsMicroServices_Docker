using UserService.DTOs;

namespace UserService.Services
{
    public interface ICartService
    {
        Task<CartDTO> CreateCartForUserService(string email, bool v);
    }
}
