namespace HP.API.Models.Domain
{
    public class Slot
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Time { get; set; }
        public bool isAvailable { get; set; }
        public SlotType SlotType { get; set; }
        public bool Possible { get; set; }
    }
}
