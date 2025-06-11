namespace CdService.DTOs
{
    public class GroupRecordsDTO
    {
        public int IdGroup { get; set; }
        public string NameGroup { get; set; }
        public int TotalRecords { get; set; }
        public List<RecordItemDTO> Records { get; set; }
    }
}
