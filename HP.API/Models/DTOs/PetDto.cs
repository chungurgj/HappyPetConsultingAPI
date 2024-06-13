using HP.API.Models.Domain;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HP.API.Models.DTOs
{
    public class PetDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Animal { get; set; }
        [Required]
        public string Breed { get; set; }
        [Required]
        public int Age { get; set; }
        public string Owner_Name { get; set; }
        public string? MedHistory { get; set; }
        public string? Color { get; set; }
        public string Owner_Id { get; set; }
    }
}
