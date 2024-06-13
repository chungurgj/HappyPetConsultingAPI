namespace HP.API.Models.Domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ImgURL { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Animal { get; set; }
    }
}
