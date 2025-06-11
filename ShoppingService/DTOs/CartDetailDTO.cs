namespace ShoppingService.DTOs
{
    public class CartDetailDTO
    {
        public int IdCartDetail { get; set; }
        public int CartId { get; set; }
        public int RecordId { get; set; }
        public int Amount { get; set; } 
        public decimal Price { get; set; } 
        public decimal Total => Amount * Price;
    }

}
