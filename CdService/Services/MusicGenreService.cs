using AutoMapper;
using CdService.DTOs;
using CdService.Models;
using CdService.Repository;

namespace CdService.Services
{
    public class MusicGenreService : IMusicGenreService
    {
        private readonly IMusicGenreRepository<MusicGenre> _musicGenreRepository;
        private IMapper _mapper;
        public List<string> Errors { get; }

        public MusicGenreService(IMusicGenreRepository<MusicGenre> musicGenreRepository,
            IMapper mapper)
        {
            _musicGenreRepository = musicGenreRepository;
            _mapper = mapper;
            Errors = new List<string>();

        }


        public async Task<IEnumerable<MusicGenreDTO>> GetService()
        {
            var musicGenres = await _musicGenreRepository.GetMusicGenreRepository();

            return musicGenres?.Select(musicGenre => _mapper.Map<MusicGenreDTO>(musicGenre)) ?? new List<MusicGenreDTO>();
        }


        public async Task<MusicGenreDTO> GetByIdService(int id)
        {
            var musicGenres = await _musicGenreRepository.GetByIdRepository(id);

            if (musicGenres != null)
            {
                var musicGenreDTO = _mapper.Map<MusicGenreDTO>(musicGenres);
                return musicGenreDTO;
            }

            return null;
        }


        public async Task<IEnumerable<MusicGenreDTO>> SearchByNameMusicGenreService(string text)
        {
            var musicGenres = await _musicGenreRepository.SearchByNameMusicGenreRepository(text);

            return musicGenres.Select(musicGenre => _mapper.Map<MusicGenreDTO>(musicGenre));
        }


        public async Task<IEnumerable<MusicGenreDTO>> GetSortedByNameMusicGenreService(bool ascending)
        {
            var musicGenres = await _musicGenreRepository.GetSortedByNameMusicGenreRepository(ascending);
            
            return musicGenres.Select(musicGenre => _mapper.Map<MusicGenreDTO>(musicGenre));
        }


        public async Task<IEnumerable<MusicGenreTotalGroupsDTO>> GetMusicGenresWithTotalGroupsMusicGenreService()
        {
            var musicGenres = await _musicGenreRepository.GetMusicGenresWithTotalGroupsMusicGenreRepository();

            return musicGenres.Select(mg => new MusicGenreTotalGroupsDTO
            {
                IdMusicGenre = mg.IdMusicGenre,
                NameMusicGenre = mg.NameMusicGenre,
                TotalGroups = mg.Groups.Count()
            }).ToList();
        }


        public async Task<MusicGenreDTO> AddService(MusicGenreInsertDTO musicGenreInsertDTO)
        {
            var musicGenre = _mapper.Map<MusicGenre>(musicGenreInsertDTO);
            await _musicGenreRepository.AddRepository(musicGenre);
            await _musicGenreRepository.SaveRepository();
            var musicGenreDTO = _mapper.Map<MusicGenreDTO>(musicGenre);

            return musicGenreDTO;
        }


        public async Task<MusicGenreDTO> UpdateService(int id, MusicGenreUpdateDTO musicGenreUpdateDTO)
        {
            var musicGenre = await _musicGenreRepository.GetByIdRepository(id);

            if (musicGenre != null)
            {
                musicGenre = _mapper.Map<MusicGenreUpdateDTO, MusicGenre>(musicGenreUpdateDTO, musicGenre);

                _musicGenreRepository.UpdateRepository(musicGenre);
                await _musicGenreRepository.SaveRepository();

                var musicGenreDTO = _mapper.Map<MusicGenreDTO>(musicGenre);

                return musicGenreDTO;
            }

            return null;
        }


        public async Task<MusicGenreDTO> DeleteService(int id)
        {
            var musicGenre = await _musicGenreRepository.GetByIdRepository(id);

            if (musicGenre != null)
            {
                var musicGenreDTO = _mapper.Map<MusicGenreDTO>(musicGenre);

                _musicGenreRepository.DeleteRepository(musicGenre);
                await _musicGenreRepository.SaveRepository();

                return musicGenreDTO;
            }

            return null;
        }


        public async Task<bool> MusicGenreHasGroupsMusicGenreService(int id)
        {
            return await _musicGenreRepository.MusicGenreHasGroupsMusicGenreRepository(id);
        }

    }
}
