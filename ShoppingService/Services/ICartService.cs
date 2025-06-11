using ShoppingService.DTOs;

namespace ShoppingService.Services
{
    public interface ICartService
    {
        Task<IEnumerable<CartDTO>> GetAllCartsCartService();
        Task<CartDTO> GetCartByEmailCartService(string userEmail);
        Task<IEnumerable<CartDTO>> GetDisabledCartsCartService();
        Task<CartStatusDTO> GetCartStatusCartService(string email);
        Task<CartDTO> CreateCartForUserService(string email, bool v);
        Task UpdateCartTotalPriceCartService(int cartId, decimal priceToAdd);  
        Task<CartDTO> DisableCartCartService(string userEmail);
        Task<CartDTO> EnableCartCartService(string userEmail);
    }
}
