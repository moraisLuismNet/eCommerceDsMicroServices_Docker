namespace CdService.Repository
{
    public interface IeCommerceDsRepository<TEntity>
    {
        Task<TEntity> GetByIdRepository(int id);
        Task AddRepository(TEntity entity);
        void UpdateRepository(TEntity entity);
        void DeleteRepository(TEntity entity);
        Task SaveRepository();
    }
}
