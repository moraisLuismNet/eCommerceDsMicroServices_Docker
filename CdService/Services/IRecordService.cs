using CdService.DTOs;
using CdService.Models;

namespace CdService.Services
{
    public interface IRecordService : IeCommerceDsService<RecordDTO, RecordInsertDTO, RecordUpdateDTO>
    {
        Task<IEnumerable<RecordDTO>> GetSortedByTitleRecordService(bool ascending);
        Task<IEnumerable<RecordDTO>> SearchByTitleRecordService(string text);
        Task<IEnumerable<RecordDTO>> GetByPriceRangeRecordService(decimal min, decimal max);
        Task UpdateStockRecordService(int id, int amount);
        Task<Record> GetRecordByIdRecordService(int id);
        Task UpdateRecordRecordService(Record record);
    }
}
