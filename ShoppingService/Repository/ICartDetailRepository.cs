using ShoppingService.DTOs;
using ShoppingService.Models;

namespace ShoppingService.Repository
{
    public interface ICartDetailRepository
    {
        Task<IEnumerable<CartDetailDTO>> GetCartDetailsByCartIdCartDetailRepository(int cartId);
        Task<IEnumerable<CartDetail>> GetCartDetailByCartIdCartDetailRepository(int cartId);
        Task<List<CartDetailInfoDTO>> GetCartDetailsInfoCartDetailRepository(int cartId);
        Task<CartDetail> GetCartDetailByCartIdAndRecordIdCartDetailRepository(int cartId, int recordId);
        Task UpdateCartDetailCartDetailRepository(CartDetail cartDetail);
        Task RemoveFromCartDetailCartDetailRepository(CartDetail cartDetail);
        Task RemoveAllCartDetailsCartDetailRepository(int cartId);
        Task RemoveAllDetailsFromCartCartDetailRepository(int cartId);
        Task SaveCartDetailRepository();
    }
}
