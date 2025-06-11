namespace CdService.DTOs
{
    public class RecordItemDTO
    {
        public int IdRecord { get; set; }
        public string TitleRecord { get; set; }
        public int? YearOfPublication { get; set; }
        public string? ImageRecord { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
    }
}
