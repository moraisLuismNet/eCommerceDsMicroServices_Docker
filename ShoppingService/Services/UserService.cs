using ShoppingService.DTOs;

namespace ShoppingService.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDTO?> GetByEmailUserService(string email)
        {
            try
            {
                var user = await _httpClient.GetFromJsonAsync<UserDTO>($"api/Users/{email}");
                return user;
            }
            catch
            {
                return null;
            }
        }
    }    
}
