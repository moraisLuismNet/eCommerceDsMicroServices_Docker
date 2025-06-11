using ShoppingService.Models;
using Microsoft.EntityFrameworkCore;

namespace ShoppingService.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShoppingContext _context;

        public OrderRepository(ShoppingContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Order>> GetOrdersOrderRepository()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderDetails) 
                .AsNoTracking() // Disable entity tracking
                .ToListAsync();

            return orders;
        }


        public async Task<IEnumerable<Order>> GetOrdersByUserEmailOrderRepository(string userEmail)
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)
                .Where(o => o.UserEmail == userEmail) 
                .AsNoTracking() // Disable entity tracking
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByCartIdOrderRepository(int cartId)
        {
            return await _context.Orders
                .Where(o => o.CartId == cartId)
                .ToListAsync();
        }

        public async Task<Order> CreateOrderOrderRepository(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task DeleteOrderOrderRepository(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

    }
}
