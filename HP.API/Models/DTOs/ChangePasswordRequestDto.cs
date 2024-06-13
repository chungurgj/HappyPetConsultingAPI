using System.ComponentModel.DataAnnotations;

namespace HP.API.Models.DTOs
{
    public class ChangePasswordRequestDto
    {
        [Required]
        public string userId {  get; set; }
        [Required]
        public string currentPassword { get; set; }
        [Required]
        public string newPassword { get; set; }
    }
}
