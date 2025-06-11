using CdService.Validators;

namespace CdService.DTOs
{
    public class GroupUpdateDTO
    {
        public int IdGroup { get; set; }
        public string NameGroup { get; set; } = null!;

        [WeightFileValidator(MaximumWeightInMegaBytes: 4)]
        [FileTypeValidator(GroupFileType.Image)]
        public IFormFile? Photo { get; set; }

        public int MusicGenreId { get; set; }
    }
}
