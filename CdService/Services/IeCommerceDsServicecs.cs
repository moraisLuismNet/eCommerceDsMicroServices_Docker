namespace CdService.Services
{
    public interface IeCommerceDsService<T, TI, TU>
    {
        public List<string> Errors { get; }
        Task<IEnumerable<T>> GetService();
        Task<T> GetByIdService(int id);
        Task<T> AddService(TI tInsertDTO);
        Task<T> UpdateService(int id, TU tUpdateDTO);
        Task<T> DeleteService(int id);
    }
}
