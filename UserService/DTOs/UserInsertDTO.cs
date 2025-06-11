namespace UserService.DTOs
{
    public class UserInsertDTO
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Role { get; set; } = "User";
    }
}
