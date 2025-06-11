namespace CdService.Models
{
    public class MusicGenre
    {
        public int IdMusicGenre { get; set; }
        public string NameMusicGenre { get; set; } = null!;
        public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
    }
}
