using CdService.DTOs;
using CdService.Models;
using Microsoft.EntityFrameworkCore;

namespace CdService.Repository
{
    public class RecordRepository : IRecordRepository<Record>
    {
        private readonly CdContext _context;

        public RecordRepository(CdContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<RecordDTO>> GetRecordRepository()
        {

            var records = await (from x in _context.Records
                                 select new RecordDTO
                                 {
                                     IdRecord = x.IdRecord,
                                     TitleRecord = x.TitleRecord,
                                     ImageRecord = x.ImageRecord,
                                     GroupId = x.GroupId,
                                     NameGroup = x.Group.NameGroup,
                                     YearOfPublication = x.YearOfPublication,
                                     Price = x.Price,
                                     Stock = x.Stock,
                                     Discontinued = x.Discontinued
                                 })
                                .AsNoTracking()  // Disable entity tracking
                                .ToListAsync();

            return records;
        }


        public async Task<Record> GetByIdRepository(int id)
        {
            return await _context.Records.FindAsync(id);
        }


        public async Task<Record> GetByIdAsyncRecordRepository(int id)
        {
            return await _context.Records
                .Include(r => r.Group)
                .FirstOrDefaultAsync(r => r.IdRecord == id);
        }


        public async Task<IEnumerable<Record>> GetSortedByTitleRecordRepository(bool ascending)
        {
            return ascending
                ? await _context.Records
                    .AsNoTracking()  // Disable entity tracking
                    .OrderBy(x => x.TitleRecord)
                    .ToListAsync()
                : await _context.Records
                    .AsNoTracking()  // Disable entity tracking
                    .OrderByDescending(x => x.TitleRecord)
                    .ToListAsync();
        }


        public async Task<IEnumerable<Record>> SearchByTitleRecordRepository(string text)
        {
            return await _context.Records
                .AsNoTracking()  // Disable entity tracking
                .Where(x => x.TitleRecord.Contains(text))
                .ToListAsync();
        }


        public async Task<IEnumerable<Record>> GetByPriceRangeRecordRepository(decimal min, decimal max)
        {
            return await _context.Records
                .AsNoTracking()  // Disable entity tracking
                .Where(x => x.Price >= min && x.Price <= max)
                .ToListAsync();
        }


        public async Task AddRepository(Record entity)
        {
            await _context.Records.AddAsync(entity);
        }


        public async Task<bool> GroupExistsRecordRepository(int groupId)
        {
            return await _context.Groups.AnyAsync(g => g.IdGroup == groupId);
        }


        public async Task SaveRepository()
        {
            await _context.SaveChangesAsync();
        }


        public void UpdateRepository(Record entity)
        {
            _context.Records.Update(entity);
        }


        public async Task UpdateAsyncRecordRepository(Record record)
        {
            _context.Entry(record).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }


        public async Task UpdateStockRecordRepository(Record record)
        {
            _context.Records.Update(record);
            await _context.SaveChangesAsync();
        }


        public void DeleteRepository(Record entity)
        {
            _context.Records.Remove(entity);
        }

    }
}
