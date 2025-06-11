using ShoppingService.DTOs;

namespace ShoppingService.Services
{
    public interface IUserService
    {
        Task<UserDTO?> GetByEmailUserService(string email);
    }
}
