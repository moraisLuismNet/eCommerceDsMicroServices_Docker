namespace ShoppingService.DTOs
{
    public class OrderDetailDTO
    {
        public int IdOrderDetail { get; set; }
        public int OrderId { get; set; }
        public int RecordId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public decimal Total => Amount * Price;
    }
}
