using HP.API.Data;
using HP.API.Models.Domain;
using HP.API.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HP.API.Repositories
{
    public class SQLPetRepository : IPetRepository
    {
        private readonly HPDbContext dbContext;
        private readonly UserManager<User> userManager;

        public SQLPetRepository(HPDbContext dbContext, UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<Pet> CreateAsync(AddPetRequestDto addPetRequestDto)
        {
            var owner = await userManager.FindByIdAsync(addPetRequestDto.Owner_Id);

            if(owner == null)
            {
                return null;
            }

            var pet = new Pet() { 
                Name = addPetRequestDto.Name,
                Breed = addPetRequestDto.Breed,
                Color = addPetRequestDto.Color,
                Age = addPetRequestDto.Age,
                MedHistory = addPetRequestDto.MedHistory,
                Animal = addPetRequestDto.Animal,
                Owner_Id = addPetRequestDto.Owner_Id,
                Owner_Name = owner.UserName
            };
            await dbContext.Pets.AddAsync(pet);
            await dbContext.SaveChangesAsync();

            return pet;
        }

        public async Task<Pet> DeleteAsync(Guid id)
        {
            var pet = await dbContext.Pets.FindAsync(id);

            if(pet == null)
            {
                return null;
            }

            dbContext.Pets.Remove(pet);
            await dbContext.SaveChangesAsync();

            return pet;
        }

        public async Task<List<Pet>> GetAllAsync()
        {
            return await dbContext.Pets.ToListAsync();

            
        }

        public async Task<Pet> GetByIdAsync(Guid id)
        {
            var pet = await dbContext.Pets.FindAsync(id);

            if(pet == null)
            {
                return null;
            }

            return pet;
        }

        public async Task<List<Pet>> GetByName(string? petName, string? petAnimal)
        {
            var pets = dbContext.Pets.AsQueryable();

            if (!string.IsNullOrWhiteSpace(petAnimal))
            {
                if (!petAnimal.Equals("Site", StringComparison.OrdinalIgnoreCase))
                {
                    if (petAnimal.Equals("Ostanato", StringComparison.OrdinalIgnoreCase))
                    {
                        pets = pets.Where(pet => pet.Animal.ToLower() != "kuce" &&
                                    pet.Animal.ToLower() != "macka" &&
                                    pet.Animal.ToLower() != "zajak" &&
                                    pet.Animal.ToLower() != "papagal");
                    }
                    else
                    {
                        pets = pets.Where(pet => pet.Animal.ToLower() == petAnimal.ToLower());
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(petName))
            {
                pets = pets.Where(pet => pet.Name.Contains(petName) || pet.Owner_Name.Contains(petName));
            }

            return await pets.ToListAsync();
        }

        public async Task<List<Pet>> GetByOwnerAsync(string id)
        {
            var owner = await userManager.FindByIdAsync(id);

            if (owner == null)
            {
                throw new Exception("Owner not found");
            }

            var pets = await dbContext.Pets.Where(p=>p.Owner_Id == owner.Id).ToListAsync();

            return pets;
        }
    }
}
