namespace ShoppingService.Models
{
    public class Order
    {
        public int IdOrder { get; set; }
        public DateTime OrderDate { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public decimal Total { get; set; }

        public string UserEmail { get; set; } = null!;

        // Optional relationship with Cart (if the order comes from a cart)
        public int CartId { get; set; }
        public Cart Cart { get; set; } = null!;

        // Relationship with OrderDetails
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
