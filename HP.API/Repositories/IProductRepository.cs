using HP.API.Models.Domain;
using HP.API.Models.DTOs;

namespace HP.API.Repositories
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(Product product);
        Task<List<Product>> GetAsync(FilterRequestDto filterRequestDto);
        Task<Product> GetByIdAsync(Guid id);

        Task<Product> DeleteAsync(Guid id);
    }
}
