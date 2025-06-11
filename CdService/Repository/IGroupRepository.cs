using CdService.DTOs;

namespace CdService.Repository
{
    public interface IGroupRepository<TEntity> : IeCommerceDsRepository<TEntity>
    {
        Task<IEnumerable<GroupRecordsDTO>> GetGroupsRecordsGroupRepository();
        Task<IEnumerable<TEntity>> SearchByNameGroupRepository(string text);
        Task<IEnumerable<TEntity>> GetSortedByNameGroupRepository(bool ascending);
        Task<bool> MusicGenreExistsGroupRepository(int id);
        Task<bool> GroupHasRecordsGroupRepository(int id);
        Task<IEnumerable<GroupDTO>> GetGroupRepository();
        Task<GroupRecordsDTO> GetRecordsByGroupGroupRepository(int id);
    }
}
