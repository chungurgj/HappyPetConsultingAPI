using HP.API.Models.Domain;
using HP.API.Models.DTOs;

namespace HP.API.Repositories
{
    public interface IPetRepository
    {
        Task<Pet> CreateAsync(AddPetRequestDto addPetRequestDto);
        Task<List<Pet>> GetAllAsync();

        Task<Pet> GetByIdAsync(Guid id);

        Task<Pet> DeleteAsync(Guid id);
        Task<List<Pet>> GetByName(string? petName,string? petAnimal);
        Task<List<Pet>> GetByOwnerAsync(string id);
    }
}
