using AutoMapper;
using CdService.DTOs;
using CdService.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CdService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class MusicGenresController : ControllerBase
    {
        private readonly IValidator<MusicGenreInsertDTO> _musicGenreInsertValidator;
        private readonly IValidator<MusicGenreUpdateDTO> _musicGenreUpdateValidator;
        private readonly IMusicGenreService _musicGenreService;
        private IMapper _mapper;

        public MusicGenresController(IValidator<MusicGenreInsertDTO> musicGenreInsertValidator,
            IValidator<MusicGenreUpdateDTO> musicGenreUpdateValidator,
            IMusicGenreService musicGenreService, IMapper mapper)
        {
            _musicGenreInsertValidator = musicGenreInsertValidator;
            _musicGenreUpdateValidator = musicGenreUpdateValidator;
            _musicGenreService = musicGenreService;
            _mapper = mapper;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<MusicGenreDTO>> Get() =>
            await _musicGenreService.GetService();



        [HttpGet("{IdMusicGenre:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<MusicGenreItemDTO>> GetById(int IdMusicGenre)
        {
            var musicGenreDTO = await _musicGenreService.GetByIdService(IdMusicGenre);
            var musicGenreItemDTO = _mapper.Map<MusicGenreItemDTO>(musicGenreDTO);

            return musicGenreItemDTO == null ? NotFound($"MusicGenre with ID {IdMusicGenre} not found") : Ok(musicGenreItemDTO);
        }


        [HttpGet("SearchByName/{text}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<MusicGenreItemDTO>>> SearchByName(string text)
        {
            var musicGenres = await _musicGenreService.SearchByNameMusicGenreService(text);

            if (musicGenres == null || !musicGenres.Any())
            {
                return NotFound($"No musical genres found matching the text '{text}'");
            }

            var musicGenresItemDTO = _mapper.Map<IEnumerable<MusicGenreItemDTO>>(musicGenres);

            return Ok(musicGenresItemDTO);
        }


        [HttpGet("sortedByName/{ascen}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<MusicGenreItemDTO>>> GetSortedByName(bool ascen)
        {
            var musicGenres = await _musicGenreService.GetSortedByNameMusicGenreService(ascen);
            if (musicGenres == null || !musicGenres.Any())
            {
                return NotFound("No musical genres found");
            }

            var musicGenresItemDTO = _mapper.Map<IEnumerable<MusicGenreItemDTO>>(musicGenres);

            return Ok(musicGenresItemDTO);
        }


        [HttpGet("GetMusicGenresWithTotalGroups")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<MusicGenreTotalGroupsDTO>>> GetMusicGenresWithTotalGroups()
        {
            var genres = await _musicGenreService.GetMusicGenresWithTotalGroupsMusicGenreService();

            return Ok(genres);
        }


        [HttpPost]
        public async Task<ActionResult<MusicGenreItemDTO>> Add(MusicGenreInsertDTO musicGenreInsertDTO)
        {
            var validationResult = await _musicGenreInsertValidator.ValidateAsync(musicGenreInsertDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var musicGenreDTO = await _musicGenreService.AddService(musicGenreInsertDTO);
            var musicGenreItemDTO = _mapper.Map<MusicGenreItemDTO>(musicGenreDTO);

            return CreatedAtAction(nameof(GetById), new { musicGenreItemDTO.IdMusicGenre }, musicGenreItemDTO);
        }


        [HttpPut("{IdMusicGenre:int}")]
        public async Task<ActionResult<MusicGenreItemDTO>> Update(int IdMusicGenre, MusicGenreUpdateDTO musicGenreUpdateDTO)
        {
            var validationResult = await _musicGenreUpdateValidator.ValidateAsync(musicGenreUpdateDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var musicGenreDTO = await _musicGenreService.UpdateService(IdMusicGenre, musicGenreUpdateDTO);
            var musicGenreItemDTO = _mapper.Map<MusicGenreItemDTO>(musicGenreDTO);

            return musicGenreItemDTO == null ? NotFound($"MusicGenre with ID {IdMusicGenre} not found") : Ok(musicGenreItemDTO);
        }


        [HttpDelete("{IdMusicGenre:int}")]
        public async Task<ActionResult<MusicGenreItemDTO>> Delete(int IdMusicGenre)
        {
            bool hasGroups = await _musicGenreService.MusicGenreHasGroupsMusicGenreService(IdMusicGenre);
            if (hasGroups)
            {
                return BadRequest($"The Music Genre with ID {IdMusicGenre} cannot be deleted because it has associated Groups");
            }
            var musicGenreDTO = await _musicGenreService.DeleteService(IdMusicGenre);
            var musicGenreItemDTO = _mapper.Map<MusicGenreItemDTO>(musicGenreDTO);

            return musicGenreItemDTO == null ? NotFound($"MusicalGenre with ID {IdMusicGenre} not found") : Ok(musicGenreItemDTO);
        }

    }
}
