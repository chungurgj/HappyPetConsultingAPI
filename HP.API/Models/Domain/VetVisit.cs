using Microsoft.AspNetCore.Identity;

namespace HP.API.Models.Domain
{
    public class VetVisit
    {
        public Guid Id { get; set; }
        public Guid Pet_Id { get; set; }
        public string Pet_Name { get; set; }
        public string Owner_Id { get; set; }
        public string Owner_Name { get; set; }
        public string Owner_Email { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }
        public DateTime Created { get; set; }
        public bool Approved { get; set; }
        public string Vet_Id { get; set; }
        public string Vet_Name { get;set; }
    
    }
}
