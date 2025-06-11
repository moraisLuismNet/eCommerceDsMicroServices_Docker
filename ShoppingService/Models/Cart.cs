namespace ShoppingService.Models
{
    public class Cart
    {
        public int IdCart { get; set; }
        public string UserEmail { get; set; } = null!;
        public decimal TotalPrice { get; set; }
        public bool Enabled { get; set; } = true;

        // Relationship with CartDetails
        public ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();
    }
}
