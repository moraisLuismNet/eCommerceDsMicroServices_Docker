using AutoMapper;
using UserService.DTOs;
using UserService.Models;
using UserService.Repository;

namespace UserService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly HashService _hashService;
        private readonly IMapper _mapper;

        public List<string> Errors { get; } = new List<string>();

        public UserService(IUserRepository userRepository, 
            HashService hashService, IMapper mapper)
        {
            _userRepository = userRepository;
            _hashService = hashService;
            _mapper = mapper;
        }


        public async Task<IEnumerable<UserInsertDTO>> GetUserService()
        {
            var users = await _userRepository.GetUserRepository();

            return users.Select(user => _mapper.Map<UserInsertDTO>(user));
        }


        public async Task<User?> GetByEmailUserService(string email)
        {
            return await _userRepository.GetByEmailUserRepository(email);
        }


        public bool VerifyPasswordUserService(string password, User user)
        {
            var hashResult = _hashService.Hash(password, user.Salt);

            return user.Password == hashResult.Hash;
        }


        public bool ValidateUserService(UserInsertDTO userInsertDTO)
        {
            if (string.IsNullOrWhiteSpace(userInsertDTO.Email) || string.IsNullOrWhiteSpace(userInsertDTO.Password))
            {
                Errors.Add("Email and password required");
                return false;
            }

            return true;
        }


        public async Task<UserDTO> AddUserService(UserInsertDTO userInsertDTO)
        {
            var existingUser = await _userRepository.GetByEmailUserRepository(userInsertDTO.Email);
            if (existingUser != null)
            {
                Errors.Add("User with this email already exists");
                return null;
            }

            var resultHash = _hashService.Hash(userInsertDTO.Password);
            var user = new User
            {
                Email = userInsertDTO.Email,
                Password = resultHash.Hash,
                Salt = resultHash.Salt,
                Role = userInsertDTO.Role
            };

            await _userRepository.AddUserRepository(user);
            await _userRepository.SaveUserRepository();

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<bool> ChangePasswordUserService(string email, string oldPassword, string newPassword)
        {
            var user = await _userRepository.GetByEmailUserRepository(email);
            if (user == null)
            {
                return false;
            }

            bool isValidPassword = VerifyPasswordUserService(oldPassword, user);
            if (!isValidPassword)
            {
                return false;
            }

            var resultHash = _hashService.Hash(newPassword);
            user.Password = resultHash.Hash;
            user.Salt = resultHash.Salt;

            _userRepository.UpdateUserRepository(user);
            await _userRepository.SaveUserRepository();

            return true;
        }


        public async Task<UserDTO> DeleteUserService(string email)
        {
            var user = await _userRepository.GetByEmailUserRepository(email);
            if (user == null)
            {
                return null;
            }

            _userRepository.DeleteUserRepository(user);
            await _userRepository.SaveUserRepository();

            return _mapper.Map<UserDTO>(user);
        }

    }
}
