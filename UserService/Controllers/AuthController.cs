using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.DTOs;
using UserService.Services;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly ICartService _cartService;

        public AuthController(IUserService userService, ITokenService tokenService,
        ICartService cartService)
        {
            _userService = userService;
            _tokenService = tokenService;
            _cartService = cartService;
        }


        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register([FromBody] UserInsertDTO userInsertDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_userService.ValidateUserService(userInsertDTO))
                {
                    return BadRequest(_userService.Errors);
                }

                userInsertDTO.Role = (userInsertDTO.Role?.Trim().ToLower() == "admin")
                    ? "Admin"
                    : "User";

                var allowedRoles = new List<string> { "User", "Admin" };
                if (!allowedRoles.Contains(userInsertDTO.Role))
                {
                    return BadRequest("Invalid role");
                }

                var userDTO = await _userService.AddUserService(userInsertDTO);
                if (userDTO == null)
                {
                    if (_userService.Errors.Any())
                    {
                        return BadRequest(_userService.Errors);
                    }
                    return BadRequest("Failed to create user");
                }

                // Create cart disabled if admin, enabled if normal user
                var isAdmin = userInsertDTO.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase);
                var cart = await _cartService.CreateCartForUserService(userDTO.Email, !isAdmin);

                if (cart == null)
                {
                    return BadRequest("Failed to create cart for the user");
                }

                return Ok(userDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserLoginDTO user)
        {
            var userDB = await _userService.GetByEmailUserService(user.Email);

            if (userDB == null)
            {
                return Unauthorized("User not found");
            }

            bool isValidPassword = _userService.VerifyPasswordUserService(user.Password, userDB);
            if (!isValidPassword)
            {
                return Unauthorized("Invalid credentials");
            }

            var tokenResponse = _tokenService.GenerateTokenService(user);

            return Ok(tokenResponse);
        }


        [HttpPost("changePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool result = await _userService.ChangePasswordUserService(changePasswordDTO.Email, changePasswordDTO.OldPassword, changePasswordDTO.NewPassword);
            if (!result)
            {
                return BadRequest("Invalid credentials or user not found");
            }

            return Ok("Password changed successfully");
        }

    }
}
