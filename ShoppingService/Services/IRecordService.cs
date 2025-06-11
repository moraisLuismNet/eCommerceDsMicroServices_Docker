using ShoppingService.DTOs;

namespace ShoppingService.Services
{
    public interface IRecordService
    {
        Task<RecordDTO> GetRecordByIdRecordService(int id);
        Task UpdateRecordRecordService(RecordDTO record);
        Task UpdateStockRecordService(int id, int amount);
    }
}
