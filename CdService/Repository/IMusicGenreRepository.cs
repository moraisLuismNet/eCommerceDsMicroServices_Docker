using CdService.DTOs;

namespace CdService.Repository
{
    public interface IMusicGenreRepository<TEntity> : IeCommerceDsRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> SearchByNameMusicGenreRepository(string text);
        Task<IEnumerable<TEntity>> GetSortedByNameMusicGenreRepository(bool ascending);
        Task<IEnumerable<TEntity>> GetMusicGenresWithTotalGroupsMusicGenreRepository();
        Task<bool> MusicGenreHasGroupsMusicGenreRepository(int id);
        Task<IEnumerable<MusicGenreDTO>> GetMusicGenreRepository();
    }
}
