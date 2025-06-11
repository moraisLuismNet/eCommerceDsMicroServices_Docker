using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingService.DTOs;
using ShoppingService.Services;

namespace ShoppingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<CartDTO>>> GetAllCarts()
        {
            var carts = await _cartService.GetAllCartsCartService();

            return Ok(carts);
        }


        [HttpGet("{email}")]
        public async Task<ActionResult<CartDTO>> GetCartByEmail(string email)
        {
            try
            {
                var cartSumary = await _cartService.GetCartByEmailCartService(email);

                return Ok(cartSumary);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error", details = ex.Message });
            }
        }


        [HttpGet("GetCartStatus/{email}")]
        public async Task<IActionResult> GetCartStatus(string email)
        {
            try
            {
                var status = await _cartService.GetCartStatusCartService(email);

                return Ok(status);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }


        [HttpGet("disabledCarts")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<CartDTO>>> GetDisabledCarts()
        {
            try
            {
                var disabledCarts = await _cartService.GetDisabledCartsCartService();
                return Ok(disabledCarts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error", details = ex.Message });
            }
        }


        [HttpPost("Disable/{email}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DisableCart(string email)
        {
            try
            {
                var disabledCart = await _cartService.DisableCartCartService(email);
                return Ok(new
                {
                    message = $"Cart with UserEmail {email} has been disabled",
                    cart = disabledCart
                });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error", details = ex.Message });
            }
        }


        [HttpPost("Enable/{email}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EnableCart(string email)
        {
            try
            {
                var enabledCart = await _cartService.EnableCartCartService(email);

                return Ok(new
                {
                    message = $"Cart with UserEmail {email} has been enabled",
                    cart = enabledCart
                });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error", details = ex.Message });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<CartDTO>> CreateCart([FromBody] CartDTO cartDTO)
        {
            try
            {
                var cart = await _cartService.CreateCartForUserService(cartDTO.UserEmail, cartDTO.Enabled);
                
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal Server Error", details = ex.Message });
            }
        }

    }
}
