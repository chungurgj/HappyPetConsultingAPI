using HP.API.Models.Domain;

namespace HP.API.Models.DTOs
{
    public class ConsultationDto
    {
        public Guid Id { get; set; }
        public Guid Pet_Id { get; set; }
        public string Vet_Id { get; set; }
        public string Owner_Id { get; set; }
        public int Type_Id { get; set; }
        public ConsultationType ConsultationType { get; set; }
        public DateTime? ConsultationStart { get; set; }
        public string Pet_Name { get; set; }
        public string Pet_Breed { get; set; }
        public int Pet_Age { get; set; }
        public string Vet_Name { get; set; }
        public string Owner_Name { get; set; }
        public string Owner_Email { get; set; }
        public DateTime Created { get; set; }
        public int Price { get; set; }
        public bool Done { get; set; }
        public string Des { get; set; }
        public bool? Started { get; set; }
        public string? StartedBy { get; set; }

    }
}
