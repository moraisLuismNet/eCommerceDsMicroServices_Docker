using ShoppingService.DTOs;
using ShoppingService.Models;

namespace ShoppingService.Repository
{
    public interface ICartRepository
    {
        Task<IEnumerable<Cart>> GetAllCartsCartRepository();
        Task<Cart> GetByIdCartRepository(int cartId);
        Task<Cart> GetCartByEmailCartRepository(string userEmail);
        Task<IEnumerable<Cart>> GetCartsByUserEmailCartRepository(string userEmail);
        Task<IEnumerable<CartDTO>> GetDisabledCartsCartRepository();
        Task<Cart?> GetActiveCartByUserEmailCartRepository(string userEmail);
        Task<CartStatusDTO> GetCartStatusCartRepository(string email);
        Task<Cart> AddCartCartRepository(CartDTO cartDTO);
        Task UpdateCartTotalPriceCartRepository(Cart cart);
        Task<CartDTO> EnableCartCartRepository(string userEmail);
        Task UpdateCartDisabledCartCartRepository(Cart cart);
        Task DeleteCartCartRepository(Cart cart);
    }
}
