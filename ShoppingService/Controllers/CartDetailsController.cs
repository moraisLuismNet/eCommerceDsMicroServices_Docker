using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingService.Models;
using ShoppingService.Services;

namespace ShoppingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class CartDetailsController : ControllerBase
    {
        private readonly ICartDetailService _cartDetailService;
        private readonly ICartService _cartService;
        private readonly IRecordService _recordService;
        private readonly IUserService _userService;

        public CartDetailsController(ICartDetailService cartDetailService, ICartService cartService, 
            IRecordService recordService, IUserService userService
            )
        {
            _cartDetailService = cartDetailService;
            _cartService = cartService;
            _recordService = recordService;
            _userService = userService;
        }


        [HttpGet("getCartDetails/{email}")]
        public async Task<IActionResult> GetCartDetails([FromRoute] string email)
        {
            try
            {
                var cart = await _cartService.GetCartByEmailCartService(email);
                if (cart == null)
                {
                    return NotFound(new { message = "Cart not found" });
                }

                var cartDetails = await _cartDetailService.GetCartDetailsByCartIdCartDetailService(cart.IdCart);
                
                return Ok(cartDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while processing your request",
                    code = "SERVER_ERROR"
                });
            }
        }


        [Authorize]
        [HttpGet("getCartItemCount/{email}")]
        public async Task<IActionResult> GetCartItemCount([FromRoute] string email)
        {
            try
            {
                var cart = await _cartService.GetCartByEmailCartService(email);
                if (cart == null)
                {
                    return NotFound(new { message = "Cart not found" });
                }

                try
                {
                    var cartDetails = await _cartDetailService.GetCartDetailsByCartIdCartDetailService(cart.IdCart);
                    int totalItems = cartDetails.Sum(detail => detail.Amount);
                    return Ok(new { totalItems });
                }
                catch (InvalidOperationException)
                {
                    // Return 0 if there are no cart details
                    return Ok(new { totalItems = 0 });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }
        }


        [Authorize]
        [HttpPost("addToCartDetailAndCart/{email}")]
        public async Task<IActionResult> AddToCartDetail([FromRoute] string email, [FromQuery] int recordId, [FromQuery] int amount)
        {
            if (amount <= 0)
            {
                return BadRequest(new { message = "Amount must be greater than zero" });
            }

            var record = await _recordService.GetRecordByIdRecordService(recordId);
            if (record == null)
            {
                return NotFound(new { message = "Record not found" });
            }

            // Check available stock
            if (record.Stock < amount)
            {
                return BadRequest(new { message = $"Not enough stock. Available: {record.Stock}, Requested: {amount}" });
            }

            var cart = await _cartService.GetCartByEmailCartService(email);
            if (cart == null)
            {
                return NotFound(new { message = "Cart not found" });
            }

            if (!cart.Enabled)
            {
                return BadRequest(new { message = "Cannot add items to a disabled cart" });
            }

            // Update stock before adding to cart
            var recordToUpdate = await _recordService.GetRecordByIdRecordService(recordId);
            if (recordToUpdate == null)
            {
                return NotFound(new { message = "Record not found" });
            }

            // Update stock using the specific endpoint
            await _recordService.UpdateStockRecordService(recordId, -amount);

            var cartDetail = await _cartDetailService.GetCartDetailByCartIdAndRecordIdCartDetailService(cart.IdCart, recordId);

            if (cartDetail == null)
            {
                cartDetail = new CartDetail
                {
                    CartId = cart.IdCart,
                    RecordId = recordId,
                    Amount = amount,
                    Price = record.Price
                };

                await _cartDetailService.AddCartDetailCartDetailService(cartDetail);
            }
            else
            {
                cartDetail.Amount += amount;
                await _cartDetailService.UpdateCartDetailCartDetailService(cartDetail);
            }

            decimal itemTotalPrice = record.Price * amount;
            await _cartService.UpdateCartTotalPriceCartService(cart.IdCart, itemTotalPrice);

            // Get the updated record after modifying the stock
            var updatedRecord = await _recordService.GetRecordByIdRecordService(recordId);

            return Ok(new
            {
                message = "Item added to cart successfully",
                updatedStock = updatedRecord.Stock
            });
        }

        
        [Authorize]
        [HttpPost("removeFromCartDetailAndCart/{email}")]
        public async Task<IActionResult> RemoveFromCartDetail([FromRoute] string email, [FromQuery] int recordId, [FromQuery] int amount)
        {
            if (amount <= 0)
            {
                return BadRequest(new { message = "Amount must be greater than zero" });
            }

            var cart = await _cartService.GetCartByEmailCartService(email);
            if (cart == null)
            {
                return NotFound(new { message = "Cart not found" });
            }

            var cartDetail = await _cartDetailService.GetCartDetailByCartIdAndRecordIdCartDetailService(cart.IdCart, recordId);
            if (cartDetail == null)
            {
                return NotFound(new { message = "Item not found in cart" });
            }

            if (cartDetail.Amount < amount)
            {
                return BadRequest(new { message = "Not enough units in cart to remove" });
            }

            try
            {
                await _recordService.UpdateStockRecordService(recordId, amount); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Failed to update record stock: {ex.Message}" });
            }

            cartDetail.Amount -= amount;
            decimal priceToSubtract = cartDetail.Price * amount;

            if (cartDetail.Amount == 0)
            {
                await _cartDetailService.RemoveFromCartDetailCartDetailService(cartDetail);
            }
            else
            {
                await _cartDetailService.UpdateCartDetailCartDetailService(cartDetail);
            }

            await _cartService.UpdateCartTotalPriceCartService(cart.IdCart, -priceToSubtract);

            var updatedRecord = await _recordService.GetRecordByIdRecordService(recordId);

            return Ok(new
            {
                message = "Item removed from cart successfully",
                updatedStock = updatedRecord?.Stock
            });
        }

    }
}
