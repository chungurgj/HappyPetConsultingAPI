namespace HP.API.Models.DTOs
{
    public class LoginResponseDto
    {
        public string Id { get; set; }
        public string JwtToken{ get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
    }
}
