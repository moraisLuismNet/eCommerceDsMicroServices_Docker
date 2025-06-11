using AutoMapper;
using CdService.DTOs;
using CdService.Models;

namespace CdService.AutoMappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Group, GroupDTO>().ReverseMap();
            CreateMap<Group, GroupInsertDTO>().ReverseMap();
            CreateMap<GroupDTO, GroupItemDTO>().ReverseMap();
            CreateMap<Group, GroupUpdateDTO>().ReverseMap();
            CreateMap<Group, GroupRecordsDTO>().ReverseMap();
            CreateMap<MusicGenre, MusicGenreDTO>().ReverseMap();
            CreateMap<MusicGenre, MusicGenreItemDTO>().ReverseMap();
            CreateMap<MusicGenreDTO, MusicGenreItemDTO>().ReverseMap();
            CreateMap<MusicGenre, MusicGenreInsertDTO>().ReverseMap();
            CreateMap<MusicGenre, MusicGenreUpdateDTO>().ReverseMap();
            CreateMap<MusicGenre, MusicGenreTotalGroupsDTO>().ReverseMap();
            CreateMap<Record, RecordDTO>().ReverseMap();
            CreateMap<Record, RecordInsertDTO>().ReverseMap();
            CreateMap<Record, RecordUpdateDTO>().ReverseMap();
            CreateMap<Record, RecordItemDTO>().ReverseMap();
            CreateMap<Record, RecordItemExtDTO>().ReverseMap();
            CreateMap<RecordDTO, RecordItemExtDTO>().ReverseMap();
        }
    }
}
