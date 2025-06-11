using CdService.DTOs;

namespace CdService.Services
{
    public interface IGroupService : IeCommerceDsService<GroupDTO, GroupInsertDTO, GroupUpdateDTO>
    {
        Task<IEnumerable<GroupRecordsDTO>> GetGroupsRecordsGroupService();
        Task<IEnumerable<GroupDTO>> SearchByNameGroupService(string text);
        Task<IEnumerable<GroupDTO>> GetSortedByNameGroupService(bool ascending);
        Task<bool> GroupHasRecordsGroupService(int id);
        Task<GroupRecordsDTO> GetRecordsByGroupGroupService(int idGroup);
    }
}
