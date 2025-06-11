using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.DTOs;
using UserService.Services;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<ActionResult<List<UserInsertDTO>>> GetUsers()
        {
            var users = await _userService.GetUserService();
            return Ok(users);
        }


        [HttpDelete("{email}")]
        public async Task<ActionResult> Delete(string email)
        {
            var result = await _userService.DeleteUserService(email);
            if (result == null)
            {
                return NotFound("User not found");
            }

            return NoContent();
        }
    }
}
