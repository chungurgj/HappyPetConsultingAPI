using HP.API.Data;
using HP.API.Models.Domain;
using HP.API.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HP.API.Repositories
{
    public class SQLProductRepository : IProductRepository
    {
        private readonly HPDbContext dbContext;

        public SQLProductRepository(HPDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Product> CreateAsync(Product product)
        {
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteAsync(Guid id)
        {
            var product = await dbContext.Products.FindAsync(id);

            if(product== null)
            {
                return null;
            }

            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();

            return product;
        }

        public async Task<List<Product>> GetAsync(FilterRequestDto filterRequestDto)
        {

            var products = dbContext.Products.AsQueryable();
            if (string.IsNullOrWhiteSpace(filterRequestDto.filterOn) == false &&
               string.IsNullOrWhiteSpace(filterRequestDto.filterQuery) == false)
            {
                if (filterRequestDto.filterOn.Equals("Title", StringComparison.OrdinalIgnoreCase))
                {
                    products = products.Where(product => product.Title.Contains(filterRequestDto.filterQuery));
                }
            }

            if (!string.IsNullOrWhiteSpace(filterRequestDto.filterAnimal))
            {
                if(filterRequestDto.filterAnimal.Equals("Kuce", StringComparison.OrdinalIgnoreCase))
                {
                    products = products.Where(product => product.Animal.ToLower() == filterRequestDto.filterAnimal.ToLower());
                }
                else if (filterRequestDto.filterAnimal.Equals("Macka", StringComparison.OrdinalIgnoreCase))
                {
                    products = products.Where(product => product.Animal.ToLower() == filterRequestDto.filterAnimal.ToLower());
                }
                else if (filterRequestDto.filterAnimal.Equals("Zajak", StringComparison.OrdinalIgnoreCase))
                {
                    products = products.Where(product => product.Animal.ToLower() == filterRequestDto.filterAnimal.ToLower());
                }
                else if (filterRequestDto.filterAnimal.Equals("Ribi", StringComparison.OrdinalIgnoreCase))
                {
                    products = products.Where(product => product.Animal.ToLower() == filterRequestDto.filterAnimal.ToLower());
                }
                else if (filterRequestDto.filterAnimal.Equals("Papagal", StringComparison.OrdinalIgnoreCase))
                {
                    products = products.Where(product => product.Animal.ToLower() == filterRequestDto.filterAnimal.ToLower());
                }
                else if (filterRequestDto.filterAnimal.Equals("Ostanato", StringComparison.OrdinalIgnoreCase))
                {
                    products = products.Where(product => product.Animal.ToLower() == filterRequestDto.filterAnimal.ToLower());
                }

               
            }

            if (filterRequestDto.filterCategory.Equals("Site",StringComparison.OrdinalIgnoreCase))
            {
                return await products.ToListAsync();
            }
            else if (filterRequestDto.filterCategory.Equals("Galanterija", StringComparison.OrdinalIgnoreCase))
            {
                products = products.Where(product=>product.Category.ToLower() == filterRequestDto.filterCategory.ToLower());
            }
            else if (filterRequestDto.filterCategory.Equals("Hrana", StringComparison.OrdinalIgnoreCase))
            {
                products = products.Where(product => product.Category.ToLower() == filterRequestDto.filterCategory.ToLower());
            }
            else if (filterRequestDto.filterCategory.Equals("Lekovi", StringComparison.OrdinalIgnoreCase))
            {
                products = products.Where(product => product.Category.ToLower() == filterRequestDto.filterCategory.ToLower());
            }
            else if (filterRequestDto.filterCategory.Equals("Odrzuvanje", StringComparison.OrdinalIgnoreCase))
            {
                products = products.Where(product => product.Category.ToLower() == filterRequestDto.filterCategory.ToLower());
            }

            


            return await products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var product = await dbContext.Products.FindAsync(id);

            if(product == null)
            {
                return null;
            }

            return product;
        }
    }
}
