namespace UserService.DTOs
{
    public class UserLoginDTO
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Role { get; set; }
    }
}
