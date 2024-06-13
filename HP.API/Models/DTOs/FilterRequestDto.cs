namespace HP.API.Models.DTOs
{
    public class FilterRequestDto
    {
        public string? filterOn { get; set; }
        public string? filterQuery { get; set; }
        public string filterCategory { get; set; } = "Site";
        public string? filterAnimal {  get; set; }

    }
}
