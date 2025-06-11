using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace UserService.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context;
        }


        public async Task<User?> GetByEmailUserRepository(string email)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
        }


        public async Task<IEnumerable<User>> GetUserRepository()
        {
            return await _context.Users.ToListAsync();
        }


        public async Task<bool> UserExistsUserRepository(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }


        public async Task AddUserRepository(User entity)
        {
            await _context.Users.AddAsync(entity);
        }


        public void UpdateUserRepository(User entity)
        {
            _context.Users.Update(entity);
        }


        public void DeleteUserRepository(User entity)
        {
            _context.Users.Remove(entity);
        }


        public async Task SaveUserRepository()
        {
            await _context.SaveChangesAsync();
        }

    }
}
