using CdService.DTOs;
using CdService.Models;
using Microsoft.EntityFrameworkCore;

namespace CdService.Repository
{
    public class GroupRepository : IGroupRepository<Group>
    {
        private readonly CdContext _context;

        public GroupRepository(CdContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<GroupDTO>> GetGroupRepository()
        {

            var groups = await (from x in _context.Groups
                                     select new GroupDTO
                                     {
                                         IdGroup = x.IdGroup,
                                         NameGroup = x.NameGroup,
                                         ImageGroup = x.ImageGroup,
                                         MusicGenreId = x.MusicGenreId,
                                         NameMusicGenre = x.MusicGenre.NameMusicGenre,
                                         TotalRecords = x.Records.Count()
                                     })
                                     .AsNoTracking() // Disable entity tracking
                                     .ToListAsync();

            return groups;
        }


        public async Task<Group> GetByIdRepository(int id)
        {
            return await _context.Groups.FindAsync(id);
        }


        public async Task<IEnumerable<GroupRecordsDTO>> GetGroupsRecordsGroupRepository()
        {
            return await _context.Groups
                .AsNoTracking() // Disable entity tracking
                .Include(a => a.Records)
                .Select(a => new GroupRecordsDTO
                {
                    IdGroup = a.IdGroup,
                    NameGroup = a.NameGroup,
                    TotalRecords = a.Records.Count,
                    Records = a.Records.Select(l => new RecordItemDTO
                    {
                        IdRecord = l.IdRecord,
                        TitleRecord = l.TitleRecord,
                        YearOfPublication = l.YearOfPublication == 0 ? null : l.YearOfPublication,
                        ImageRecord = l.ImageRecord,
                        Price = l.Price == 0 ? null : l.Price,
                        Stock = l.Stock == 0 ? null : l.Stock
                    }).ToList()
                }).ToListAsync();
        }


        public async Task<GroupRecordsDTO> GetRecordsByGroupGroupRepository(int id)
        {
            var group = await _context.Groups
                .AsNoTracking()  // Disable entity tracking
                .Where(g => g.IdGroup == id)
                .Include(g => g.Records)
                .SingleOrDefaultAsync();

            if (group == null)
            {
                return null;
            }

            return new GroupRecordsDTO
            {
                IdGroup = group.IdGroup,
                NameGroup = group.NameGroup,
                TotalRecords = group.Records.Count,
                Records = group.Records.Select(r => new RecordItemDTO
                {
                    IdRecord = r.IdRecord,
                    TitleRecord = r.TitleRecord,
                    YearOfPublication = r.YearOfPublication,
                    ImageRecord = r.ImageRecord,
                    Price = r.Price,
                    Stock = r.Stock
                }).ToList() ?? new List<RecordItemDTO>()
            };
        }


        public async Task<IEnumerable<Group>> SearchByNameGroupRepository(string text)
        {
            return await _context.Groups
                .AsNoTracking()  // Disable entity tracking
                .Where(x => x.NameGroup.Contains(text))
                .ToListAsync();
        }


        public async Task<IEnumerable<Group>> GetSortedByNameGroupRepository(bool ascending)
        {
            return ascending
                ? await _context.Groups
                    .AsNoTracking()  // Disable entity tracking
                    .OrderBy(x => x.NameGroup)
                    .ToListAsync()
                : await _context.Groups
                    .AsNoTracking()  // Disable entity tracking
                    .OrderByDescending(x => x.NameGroup)
                    .ToListAsync();
        }


        public async Task AddRepository(Group entity)
        {
            await _context.Groups.AddAsync(entity);
        }


        public async Task<bool> MusicGenreExistsGroupRepository(int musicGenreId)
        {
            return await _context.MusicGenres.AnyAsync(g => g.IdMusicGenre == musicGenreId);
        }


        public void UpdateRepository(Group entity)
        {
            _context.Groups.Update(entity);
        }


        public void DeleteRepository(Group entity)
        {
            _context.Groups.Remove(entity);
        }


        public async Task<bool> GroupHasRecordsGroupRepository(int id)
        {
            return await _context.Records.AnyAsync(b => b.GroupId == id);

        }


        public async Task SaveRepository()
        {
            await _context.SaveChangesAsync();
        }

    }
}
