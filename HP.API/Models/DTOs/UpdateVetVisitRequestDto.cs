namespace HP.API.Models.DTOs
{
    public class UpdateVetVisitRequestDto
    {
        public Guid Id { get; set; }
        public DateTime? DateTimeStart { get; set; }
        public bool? IsApproved { get; set; }
        public string? Vet_Id { get; set; }
    }
}
