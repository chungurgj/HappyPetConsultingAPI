namespace HP.API.Models.DTOs
{
    public class FilterVetVisitDto
    {
        public DateTime? filterDate {  get; set; }
        public string? filterStatus { get; set; }
        public int? filterMonth { get; set;}
        public string? filterQuery { get; set;} 
    }
}
