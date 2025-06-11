using CdService.Validators;

namespace CdService.DTOs
{
    public class GroupInsertDTO
    {
        public string NameGroup { get; set; } = null!;

        [WeightFileValidator(MaximumWeightInMegaBytes: 4)]
        [FileTypeValidator(GroupFileType.Image)]
        public IFormFile? Photo { get; set; }
        public int MusicGenreId { get; set; }
    }
}
