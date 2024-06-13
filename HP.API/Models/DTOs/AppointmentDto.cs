using HP.API.Models.Domain;

namespace HP.API.Models.DTOs
{
    public class AppointmentDto
    {
        public Guid Id { get; set; }
        public Guid Category_Id { get; set; }
        public string Pet_Name { get; set; }
        public string Category_Name { get; set; }
        public Guid Pet_Id { get; set; }
        public string Owner_Id { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime Created { get; set; }
    }
}
