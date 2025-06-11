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
    public class RecordsController : ControllerBase
    {
        private readonly IValidator<RecordInsertDTO> _recordInsertValidator;
        private readonly IValidator<RecordUpdateDTO> _recordUpdateValidator;
        private readonly IRecordService _recordService;
        private IMapper _mapper;
        public RecordsController(IValidator<RecordInsertDTO> recordInsertValidator,
            IValidator<RecordUpdateDTO> recordUpdateValidator, IRecordService recordService,
            IMapper mapper)
        {
            _recordInsertValidator = recordInsertValidator;
            _recordUpdateValidator = recordUpdateValidator;
            _recordService = recordService;
            _mapper = mapper;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<RecordDTO>> Get() =>
            await _recordService.GetService();



        [HttpGet("{IdRecord:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<RecordItemExtDTO>> GetById(int IdRecord)
        {
            var recordDTO = await _recordService.GetByIdService(IdRecord);
            var recordItemExtDTO = _mapper.Map<RecordItemExtDTO>(recordDTO);

            return recordItemExtDTO == null ? NotFound($"Record with ID {IdRecord} not found") : Ok(recordItemExtDTO);
        }


        [HttpGet("sortedByTitle/{ascen}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RecordItemExtDTO>>> GetSortedByTitle(bool ascen)
        {
            var records = await _recordService.GetSortedByTitleRecordService(ascen);
            var recordsItemExt = _mapper.Map<IEnumerable<RecordItemExtDTO>>(records);

            return Ok(recordsItemExt);
        }


        [HttpGet("SearchByTitle/{text}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RecordItemExtDTO>>> SearchByTitle(string text)
        {
            var records = await _recordService.SearchByTitleRecordService(text);

            if (records == null || !records.Any())
            {
                return NotFound($"No records found matching the text '{text}'");
            }

            var recordsItemExt = _mapper.Map<IEnumerable<RecordItemExtDTO>>(records);

            return Ok(recordsItemExt);
        }


        [HttpGet("byPriceRange")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RecordItemExtDTO>>> GetByPriceRange(decimal min, decimal max)
        {
            var records = await _recordService.GetByPriceRangeRecordService(min, max);
            var recordsItemExt = _mapper.Map<IEnumerable<RecordItemExtDTO>>(records);

            return Ok(recordsItemExt);
        }


        [HttpPost]
        public async Task<ActionResult<RecordItemExtDTO>> Add(RecordInsertDTO recordInsertDTO)
        {
            var validationResult = await _recordInsertValidator.ValidateAsync(recordInsertDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var recordDTO = await _recordService.AddService(recordInsertDTO);
            var recordItemExtDTO = _mapper.Map<RecordItemExtDTO>(recordDTO);

            return CreatedAtAction(nameof(GetById), new { recordItemExtDTO.IdRecord }, recordItemExtDTO);
        }


        [HttpPut("{IdRecord:int}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<RecordItemExtDTO>> Update(int IdRecord, RecordUpdateDTO recordUpdateDTO)
        {
            var validationResult = await _recordUpdateValidator.ValidateAsync(recordUpdateDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var recordDTO = await _recordService.UpdateService(IdRecord, recordUpdateDTO);
            var recordItemExtDTO = _mapper.Map<RecordItemExtDTO>(recordDTO);

            return recordItemExtDTO == null ? NotFound($"Record with ID {IdRecord} not found") : Ok(recordItemExtDTO);
        }


        [HttpPut("{id:int}/updateStock/{amount:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateStock(int id, int amount)
        {
            var record = await _recordService.GetByIdService(id);

            if (record == null)
            {
                return NotFound(new { message = $"Record with ID {id} not found" });
            }

            try
            {
                await _recordService.UpdateStockRecordService(id, amount);
                var updatedRecord = await _recordService.GetByIdService(id);
                return Ok(new
                {
                    message = $"The stock of the record with ID {id} has been updated in {amount} units",
                    newStock = updatedRecord.Stock
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpDelete("{IdRecord:int}")]
        public async Task<ActionResult<RecordItemExtDTO>> Delete(int IdRecord)
        {
            var recordDTO = await _recordService.DeleteService(IdRecord);
            var recordItemExtDTO = _mapper.Map<RecordItemExtDTO>(recordDTO);

            return recordItemExtDTO == null ? NotFound($"Record with ID {IdRecord} not found") : Ok(recordItemExtDTO);
        }

    }
}
