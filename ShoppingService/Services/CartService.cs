using AutoMapper;
using ShoppingService.DTOs;
using ShoppingService.Repository;
using ShoppingService.Services;


public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly ICartDetailRepository _cartDetailRepository;
    private readonly IMapper _mapper;
    private readonly IRecordService _recordService;


    public CartService(
        ICartRepository cartRepository, IMapper mapper, ICartDetailRepository cartDetailRepository,
        IRecordService recordService
        )
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
        _cartDetailRepository = cartDetailRepository;
        _recordService = recordService;

    }


    public async Task<IEnumerable<CartDTO>> GetAllCartsCartService()
    {
        var carts = await _cartRepository.GetAllCartsCartRepository();

        return carts.Select(x => new CartDTO
        {
            IdCart = x.IdCart,
            UserEmail = x.UserEmail,
            TotalPrice = x.TotalPrice,
            Enabled = x.Enabled
        }).ToList();
    }


    public async Task<CartDTO> GetCartByEmailCartService(string email)
    {
        var cart = await _cartRepository.GetCartByEmailCartRepository(email);
        if (cart == null)
        {
            throw new InvalidOperationException($"No cart found for user with email {email}");
        }

        return _mapper.Map<CartDTO>(cart);
    }


    public async Task<IEnumerable<CartDTO>> GetDisabledCartsCartService()
    {
        return await _cartRepository.GetDisabledCartsCartRepository();
    }


    public async Task<CartStatusDTO> GetCartStatusCartService(string email)
    {
        try
        {
            return await _cartRepository.GetCartStatusCartRepository(email);
        }
        catch (Exception ex)
        {
            throw; // Rethrow the exception
        }
    }


    public async Task<CartDTO> CreateCartForUserService(string userEmail, bool enabled = true)
    {
        var cart = new CartDTO
        {
            UserEmail = userEmail,
            TotalPrice = 0,
            Enabled = enabled
        };

        var createdCart = await _cartRepository.AddCartCartRepository(cart);
        return _mapper.Map<CartDTO>(createdCart);
    }


    public async Task UpdateCartTotalPriceCartService(int cartId, decimal priceToAdd)
    {
        var cart = await _cartRepository.GetByIdCartRepository(cartId);
        if (cart == null)
        {
            throw new InvalidOperationException("Cart not found");
        }

        cart.TotalPrice += priceToAdd;

        await _cartRepository.UpdateCartTotalPriceCartRepository(cart);
    }

    public async Task<CartDTO> DisableCartCartService(string userEmail)
    {
        var cart = await _cartRepository.GetCartByEmailCartRepository(userEmail);
        if (cart == null)
        {
            throw new InvalidOperationException("No active cart found for this user");
        }

        // Get cart details
        var cartDetailsInfo = await _cartDetailRepository.GetCartDetailsInfoCartDetailRepository(cart.IdCart);

        // Update stock using IRecordService
        foreach (var detail in cartDetailsInfo)
        {
            try
            {
                await _recordService.UpdateStockRecordService(
                    id: detail.RecordId,
                    amount: detail.Amount 
                );
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Failed to update stock for record {detail.RecordId}: {ex.Message}"
                );
            }
        }

        // Clear cart
        await _cartDetailRepository.RemoveAllDetailsFromCartCartDetailRepository(cart.IdCart);

        // Deactivate cart
        cart.Enabled = false;
        cart.TotalPrice = 0;
        await _cartRepository.UpdateCartDisabledCartCartRepository(cart);

        return _mapper.Map<CartDTO>(cart);
    }


    public async Task<CartDTO> EnableCartCartService(string userEmail)
    {
        var cartDTO = await _cartRepository.EnableCartCartRepository(userEmail);

        return cartDTO;
    }

}
