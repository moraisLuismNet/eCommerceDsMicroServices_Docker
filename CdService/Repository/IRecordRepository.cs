using CdService.DTOs;
using CdService.Models;

namespace CdService.Repository
{
    public interface IRecordRepository<TEntity> : IeCommerceDsRepository<TEntity> where TEntity : class
    {
        Task<bool> GroupExistsRecordRepository(int id);
        Task<IEnumerable<TEntity>> GetSortedByTitleRecordRepository(bool ascending);
        Task<IEnumerable<TEntity>> SearchByTitleRecordRepository(string text);
        Task<IEnumerable<TEntity>> GetByPriceRangeRecordRepository(decimal min, decimal max);
        Task<IEnumerable<RecordDTO>> GetRecordRepository();
        Task<TEntity> GetByIdAsyncRecordRepository(int id);
        Task UpdateStockRecordRepository(Record record);
        Task UpdateAsyncRecordRepository(TEntity entity);
    }
}
