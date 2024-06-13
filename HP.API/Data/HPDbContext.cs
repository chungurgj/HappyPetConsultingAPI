using HP.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace HP.API.Data
{
    public class HPDbContext : DbContext
    {
        public HPDbContext(DbContextOptions<HPDbContext> options) : base(options)
        {
        }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<Product> Products { get; set; }
  

        public DbSet<VetVisit> VetVisits { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<ConsultationType> ConsultationTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var consultationTypes = new List<ConsultationType>()
            {
                new ConsultationType
                {
                    Id = 1,
                    Name = "text",
                    Price = 300
                },
                new ConsultationType
                {
                    Id = 2,
                    Name = "video",
                    Price = 600
                },
                new ConsultationType
                {
                    Id = 3,
                    Name = "emergency",
                    Price = 900
                }
            };

            modelBuilder.Entity<ConsultationType>().HasData(consultationTypes);
        }

    }
}
