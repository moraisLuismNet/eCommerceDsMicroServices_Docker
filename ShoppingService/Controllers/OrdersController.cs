using Microsoft.AspNetCore.Mvc;
using ShoppingService.DTOs;
using ShoppingService.Services;

namespace ShoppingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            var orders = await _orderService.GetOrdersOrderService();

            return Ok(orders);
        }


        [HttpGet("{userEmail}")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersByUserEmail(string userEmail)
        {
            var orders = await _orderService.GetOrdersByUserEmailOrderService(userEmail);
            
            return Ok(orders);
        }


        [HttpPost("from-cart/{userEmail}")]
        public async Task<ActionResult<OrderDTO>> CreateOrderFromCart(
        string userEmail, string paymentMethod = "Credit Card")
        {
            try
            {
                var decodedEmail = Uri.UnescapeDataString(userEmail);

                var order = await _orderService.CreateOrderFromCartOrderService(
                decodedEmail, paymentMethod
            );

                return CreatedAtAction(
                    nameof(GetOrdersByUserEmail),
                    new { userEmail = decodedEmail },
                    order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
