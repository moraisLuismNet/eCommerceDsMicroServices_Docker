namespace CdService.Models
{
    public class Group
    {
        public int IdGroup { get; set; }
        public string NameGroup { get; set; } = null!;
        public string? ImageGroup { get; set; }
        public int MusicGenreId { get; set; }
        public virtual MusicGenre MusicGenre { get; set; } = null!;
        public virtual ICollection<Record> Records { get; set; } = new List<Record>();
    }
}
