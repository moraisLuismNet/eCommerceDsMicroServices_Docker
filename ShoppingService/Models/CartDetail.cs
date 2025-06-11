namespace ShoppingService.Models
{
    public class CartDetail
    {
        public int IdCartDetail { get; set; }

        // Relationship with Cart
        public int CartId { get; set; }
        public Cart Cart { get; set; } = null!;

        public int RecordId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public decimal Total => Amount * Price;
    }
}