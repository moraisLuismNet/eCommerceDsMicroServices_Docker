using UserService.Models;

namespace UserService.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUserRepository();
        Task<User?> GetByEmailUserRepository(string email);
        Task<bool> UserExistsUserRepository(string email);
        Task AddUserRepository(User entity);
        void UpdateUserRepository(User entity);
        void DeleteUserRepository(User entity);
        Task SaveUserRepository();
    }
}
