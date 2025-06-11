namespace CdService.DTOs
{
    public class GroupDTO
    {
        public int IdGroup { get; set; }
        public string NameGroup { get; set; } = null!;
        public string? ImageGroup { get; set; }
        public int MusicGenreId { get; set; }
        public string? NameMusicGenre { get; set; } = null!;
        public int? TotalRecords { get; set; }
    }
}
