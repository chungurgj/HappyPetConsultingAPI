using System.ComponentModel.DataAnnotations;

namespace HP.API.Models.DTOs
{
    public class RegisterRequestDto
    {
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string[] Roles { get; set; } = {"User" };
    }
}
