using AutoMapper;
using ShoppingService.DTOs;
using ShoppingService.Models;
using Microsoft.EntityFrameworkCore;

namespace ShoppingService.Repository
{
    public class CartDetailRepository : ICartDetailRepository
    {
        private readonly ShoppingContext _context;
        private readonly IMapper _mapper;

        public CartDetailRepository(ShoppingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<List<CartDetailInfoDTO>> GetCartDetailsInfoCartDetailRepository(int cartId)
        {
            return await _context.CartDetails
                .Where(cd => cd.CartId == cartId)
                .Select(cd => new CartDetailInfoDTO
                {
                    RecordId = cd.RecordId,
                    Amount = cd.Amount
                })
                .ToListAsync();
        }


        public async Task<CartDetail> GetCartDetailByCartIdAndRecordIdCartDetailRepository(int cartId, int recordId)
        {
            return await _context.CartDetails
                .FirstOrDefaultAsync(cd => cd.CartId == cartId && cd.RecordId == recordId);
        }


        public async Task UpdateCartDetailCartDetailRepository(CartDetail cartDetail)
        {
            var existingCartDetail = await _context.CartDetails
                .FirstOrDefaultAsync(cd => cd.IdCartDetail == cartDetail.IdCartDetail);

            if (existingCartDetail != null)
            {
                _context.Entry(existingCartDetail).CurrentValues.SetValues(cartDetail);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException("CartDetail not found");
            }
        }


        public async Task RemoveFromCartDetailCartDetailRepository(CartDetail cartDetail)
        {
            _context.CartDetails.Remove(cartDetail);
        }


        public async Task SaveCartDetailRepository()
        {
            await _context.SaveChangesAsync();
        }


        public async Task RemoveAllCartDetailsCartDetailRepository(int cartId)
        {
            var details = await _context.CartDetails
                .Where(cd => cd.CartId == cartId)
                .ToListAsync();

            _context.CartDetails.RemoveRange(details);
            await _context.SaveChangesAsync();
        }


        public async Task RemoveAllDetailsFromCartCartDetailRepository(int cartId)
        {
            var details = await _context.CartDetails
                .Where(cd => cd.CartId == cartId)
                .ToListAsync();

            _context.CartDetails.RemoveRange(details);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CartDetailDTO>> GetCartDetailsByCartIdCartDetailRepository(int cartId)
        {
            return await _context.CartDetails
                .Where(cd => cd.CartId == cartId)
                .Select(cd => new CartDetailDTO
                {
                    IdCartDetail = cd.IdCartDetail,
                    CartId = cd.CartId,
                    RecordId = cd.RecordId,
                    Amount = cd.Amount,
                    Price = cd.Price
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<CartDetail>> GetCartDetailByCartIdCartDetailRepository(int cartId)
        {
            return await _context.CartDetails
                .Where(cd => cd.CartId == cartId)
                .ToListAsync();
        }
    }
}
