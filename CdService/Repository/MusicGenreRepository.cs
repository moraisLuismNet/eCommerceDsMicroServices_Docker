using CdService.DTOs;
using CdService.Models;
using Microsoft.EntityFrameworkCore;

namespace CdService.Repository
{
    public class MusicGenreRepository : IMusicGenreRepository<MusicGenre>
    {
        private readonly CdContext _context;

        public MusicGenreRepository(CdContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<MusicGenreDTO>> GetMusicGenreRepository()
        {
            var musicGenres = await (from x in _context.MusicGenres
                                     select new MusicGenreDTO
                                     {
                                         IdMusicGenre = x.IdMusicGenre,
                                         NameMusicGenre = x.NameMusicGenre,
                                         TotalGroups = x.Groups.Count()
                                     })
                                 .AsNoTracking()  // Disable entity tracking
                                 .ToListAsync();

            return musicGenres ?? new List<MusicGenreDTO>();
        }


        public async Task<MusicGenre> GetByIdRepository(int id)
        {
            return await _context.MusicGenres.FindAsync(id);
        }


        public async Task<IEnumerable<MusicGenre>> GetSortedByNameMusicGenreRepository(bool ascending)
        {
            return ascending
                ? await _context.MusicGenres
                    .AsNoTracking()  // Disable entity tracking
                    .OrderBy(x => x.NameMusicGenre)
                    .ToListAsync()
                : await _context.MusicGenres
                    .AsNoTracking()  // Disable entity tracking
                    .OrderByDescending(x => x.NameMusicGenre)
                    .ToListAsync();
        }


        public async Task<IEnumerable<MusicGenre>> GetMusicGenresWithTotalGroupsMusicGenreRepository()
        {
            return await _context.MusicGenres
                 .AsNoTracking()  // Disable entity tracking
                .Include(mg => mg.Groups)
                .ToListAsync();
        }


        public async Task<IEnumerable<MusicGenre>> SearchByNameMusicGenreRepository(string text)
        {
            return await _context.MusicGenres
                .AsNoTracking()  // Disable entity tracking
                .Where(x => x.NameMusicGenre.Contains(text))
                .ToListAsync();
        }


        public async Task AddRepository(MusicGenre entity)
        {
            await _context.MusicGenres.AddAsync(entity);
        }


        public async Task<bool> MusicGenreHasGroupsMusicGenreRepository(int id)
        {
            return await _context.Groups.AnyAsync(b => b.MusicGenreId == id);
        }


        public async Task SaveRepository()
        {
            await _context.SaveChangesAsync();
        }


        public void UpdateRepository(MusicGenre entity)
        {
            _context.MusicGenres.Update(entity);
        }


        public void DeleteRepository(MusicGenre entity)
        {
            _context.MusicGenres.Remove(entity);
        }

    }
}
