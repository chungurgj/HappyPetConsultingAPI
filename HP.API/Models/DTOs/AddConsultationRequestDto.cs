namespace HP.API.Models.DTOs
{
    public class AddConsultationRequestDto
    {
        public Guid Pet_Id { get; set; }
        public string Vet_Id { get; set; }
        public int Type_Id { get; set; }
        public DateTime? ConsultationStart { get; set; }
        public int? Price { get; set; }
        public string Des { get; set; }
    }
}
