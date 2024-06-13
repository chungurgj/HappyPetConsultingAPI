namespace HP.API.Models.DTOs
{
    public class AddAppointmentRequestDto
    {
        public Guid Category_Id { get; set; }
        public Guid Pet_Id { get; set; }
        public string Owner_Id { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime Created { get; set; }

    }
}
