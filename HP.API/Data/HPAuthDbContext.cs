using HP.API.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HP.API.Data
{
    public class HPAuthDbContext : IdentityDbContext<User>
    {
        public HPAuthDbContext(DbContextOptions<HPAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var roles = new List<IdentityRole> { 
                new IdentityRole{
                    Id = "df9baffb-b241-4da7-ad0a-41fc757f77fd",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "df9baffb-b241-4da7-ad0a-41fc757f77fd"
                },
                new IdentityRole{
                    Id = "3402fb71-2223-4b8d-9002-e9c7fd38efc1",
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "3402fb71-2223-4b8d-9002-e9c7fd38efc1"
                },
                new IdentityRole{
                    Id = "bdbe51a9-4aad-4869-815d-ced982f1b00c",
                    Name = "Vet",
                    NormalizedName = "VET",
                    ConcurrencyStamp = "bdbe51a9-4aad-4869-815d-ced982f1b00c"
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
