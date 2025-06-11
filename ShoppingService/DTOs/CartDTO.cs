namespace ShoppingService.DTOs
{
    public class CartDTO
    {
        public int IdCart { get; set; }
        public string UserEmail { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public bool Enabled { get; set; } = true;
    }
}
