using System.ComponentModel.DataAnnotations;

namespace HP.API.Models.DTOs
{
    public class AddPetRequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Animal { get; set; }
        [Required]
        public string Breed { get; set; }
        [Required]
        public int Age { get; set; }
        public string? MedHistory { get; set; }
        public string? Color { get; set; }
        [Required]
        public string Owner_Id { get; set; }
    }
}
