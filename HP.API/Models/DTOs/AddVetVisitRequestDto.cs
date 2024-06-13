using HP.API.Models.Domain;

namespace HP.API.Models.DTOs
{
    public class AddVetVisitRequestDto
    {
        public Guid Pet_Id { get; set; }
        public string Owner_Id { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime Created { get; set; }
        public string Vet_Id { get; set; }
    }
}

