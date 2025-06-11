namespace CdService.DTOs
{
    public class GroupItemDTO
    {
        public int IdGroup { get; set; }
        public string NameGroup { get; set; } = null!;
        public string? ImageGroup { get; set; }
        public int MusicGenreId { get; set; }
    }
}
