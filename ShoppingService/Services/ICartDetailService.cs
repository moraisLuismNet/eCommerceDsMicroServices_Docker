using ShoppingService.DTOs;
using ShoppingService.Models;

namespace ShoppingService.Services
{
    public interface ICartDetailService
    {
        Task<IEnumerable<CartDetailDTO>> GetCartDetailsByCartIdCartDetailService(int cartId);
        Task<CartDetail> GetCartDetailByCartIdAndRecordIdCartDetailService(int cartId, int recordId);
        Task AddCartDetailCartDetailService(CartDetail cartDetail);
        Task UpdateCartDetailCartDetailService(CartDetail cartDetail);
        Task RemoveFromCartDetailCartDetailService(CartDetail cartDetail);
    }
}
