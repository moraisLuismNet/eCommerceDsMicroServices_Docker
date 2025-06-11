using UserService.DTOs;

namespace UserService.Services
{
    public class CartService : ICartService
    {
        private readonly HttpClient _httpClient;
        public CartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<CartDTO> CreateCartForUserService(string email, bool enabled)
        {
            var cartDto = new CartDTO
            {
                UserEmail = email,
                TotalPrice = 0,
                Enabled = enabled
            };

            var response = await _httpClient.PostAsJsonAsync("api/Carts", cartDto);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to create cart: {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<CartDTO>();
        }
    }
}
