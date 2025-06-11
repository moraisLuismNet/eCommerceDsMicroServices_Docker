using UserService.DTOs;
using UserService.Models;

namespace UserService.Services
{
    public interface IUserService
    {
        public List<string> Errors { get; }
        Task<IEnumerable<UserInsertDTO>> GetUserService();
        Task<User?> GetByEmailUserService(string email);
        bool ValidateUserService(UserInsertDTO dto);
        bool VerifyPasswordUserService(string password, User user);
        Task<UserDTO> AddUserService(UserInsertDTO userInsertDTO);
        Task<bool> ChangePasswordUserService(string email, string oldPassword, string newPassword);
        Task<UserDTO> DeleteUserService(string email);
    }
}
