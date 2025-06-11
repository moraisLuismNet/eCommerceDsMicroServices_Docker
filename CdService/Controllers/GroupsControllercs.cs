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
    public class GroupsController : ControllerBase
    {
        private readonly IValidator<GroupInsertDTO> _groupInsertValidator;
        private readonly IValidator<GroupUpdateDTO> _groupUpdateValidator;
        private readonly IGroupService _groupService;
        private IMapper _mapper;

        public GroupsController(IValidator<GroupInsertDTO> groupInsertValidator,
            IValidator<GroupUpdateDTO> groupUpdateValidator,
            IGroupService groupService, IMapper mapper)
        {
            _groupInsertValidator = groupInsertValidator;
            _groupUpdateValidator = groupUpdateValidator;
            _groupService = groupService;
            _mapper = mapper;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<GroupDTO>> Get() =>
            await _groupService.GetService();



        [HttpGet("{IdGroup:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<GroupItemDTO>> GetById(int IdGroup)
        {
            var groupDTO = await _groupService.GetByIdService(IdGroup);
            var groupItemDTO = _mapper.Map<GroupItemDTO>(groupDTO);

            return groupItemDTO == null ? NotFound($"Group with ID {IdGroup} not found") : Ok(groupItemDTO);
        }


        [HttpGet("groupsRecords")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<GroupRecordsDTO>>> GetGroupsRecords()
        {
            var groupRecords = await _groupService.GetGroupsRecordsGroupService();

            return Ok(groupRecords);
        }


        [HttpGet("recordsByGroup/{idGroup}")]
        [AllowAnonymous]
        public async Task<ActionResult<GroupRecordsDTO>> GetRecordsByGroup(int idGroup)
        {
            var groupDTO = await _groupService.GetRecordsByGroupGroupService(idGroup);

            if (groupDTO == null)
            {
                return NotFound($"Group with ID {idGroup} not found");
            }

            return Ok(groupDTO);
        }


        [HttpGet("SearchByName/{text}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<GroupItemDTO>>> SearchByName(string text)
        {
            var groups = await _groupService.SearchByNameGroupService(text);

            if (groups == null || !groups.Any())
            {
                return NotFound($"No groups found matching the text '{text}'");
            }

            var groupsItem = _mapper.Map<IEnumerable<GroupItemDTO>>(groups);

            return Ok(groupsItem);
        }


        [HttpGet("sortedByName/{ascen}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<GroupItemDTO>>> GetSortedByName(bool ascen)
        {
            var groups = await _groupService.GetSortedByNameGroupService(ascen);
            var groupsItem = _mapper.Map<IEnumerable<GroupItemDTO>>(groups);

            return Ok(groupsItem);
        }


        [HttpPost]
        public async Task<ActionResult<GroupItemDTO>> Add(GroupInsertDTO groupInsertDTO)
        {
            var validationResult = await _groupInsertValidator.ValidateAsync(groupInsertDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var groupDTO = await _groupService.AddService(groupInsertDTO);
            var groupItemDTO = _mapper.Map<GroupItemDTO>(groupDTO);

            return CreatedAtAction(nameof(GetById), new { groupItemDTO.IdGroup }, groupItemDTO);
        }


        [HttpPut("{IdGroup:int}")]
        public async Task<ActionResult<GroupItemDTO>> Update(int IdGroup, GroupUpdateDTO groupUpdateDTO)
        {
            var validationResult = await _groupUpdateValidator.ValidateAsync(groupUpdateDTO);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var groupDTO = await _groupService.UpdateService(IdGroup, groupUpdateDTO);
            var groupItemDTO = _mapper.Map<GroupItemDTO>(groupDTO);

            return groupItemDTO == null ? NotFound($"Group with ID {IdGroup} not found") : Ok(groupItemDTO);
        }


        [HttpDelete("{IdGroup:int}")]
        public async Task<ActionResult<GroupItemDTO>> Delete(int IdGroup)
        {
            bool hasGroups = await _groupService.GroupHasRecordsGroupService(IdGroup);
            if (hasGroups)
            {
                return BadRequest($"The Group with ID {IdGroup} cannot be deleted because it has associated Records");
            }
            var groupDTO = await _groupService.DeleteService(IdGroup);
            var groupItemDTO = _mapper.Map<GroupItemDTO>(groupDTO);

            return groupItemDTO == null ? NotFound($"Group with ID {IdGroup} not found") : Ok(groupItemDTO);
        }
    }
}
