namespace UserService.DTOs
{
    public class CartDTO
    {
        public int IdCart { get; set; }
        public string UserEmail { get; set; }
        public decimal TotalPrice { get; set; }
        public bool Enabled { get; set; }
    }
}
