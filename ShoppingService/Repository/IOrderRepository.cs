using ShoppingService.Models;

namespace ShoppingService.Repository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersOrderRepository();
        Task<IEnumerable<Order>> GetOrdersByUserEmailOrderRepository(string userEmail);
        Task<IEnumerable<Order>> GetOrdersByCartIdOrderRepository(int cartId);
        Task<Order> CreateOrderOrderRepository(Order order);
        Task DeleteOrderOrderRepository(Order order);
    }
}
