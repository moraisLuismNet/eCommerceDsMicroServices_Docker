using CdService.DTOs;

namespace CdService.Services
{
    public interface IMusicGenreService : IeCommerceDsService<MusicGenreDTO, MusicGenreInsertDTO, MusicGenreUpdateDTO>
    {
        Task<IEnumerable<MusicGenreDTO>> SearchByNameMusicGenreService(string text);
        Task<IEnumerable<MusicGenreDTO>> GetSortedByNameMusicGenreService(bool ascending);
        Task<IEnumerable<MusicGenreTotalGroupsDTO>> GetMusicGenresWithTotalGroupsMusicGenreService();
        Task<bool> MusicGenreHasGroupsMusicGenreService(int id);
    }
}
