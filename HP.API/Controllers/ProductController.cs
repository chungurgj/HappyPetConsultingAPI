using AutoMapper;
using HP.API.Models.Domain;
using HP.API.Models.DTOs;
using HP.API.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace HP.API.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductController(IProductRepository productRepository,IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddProductRequestDto addProductRequestDto)
        {
            var product = mapper.Map<Product>(addProductRequestDto);

            var productResult = await productRepository.CreateAsync(product);

            var productDto = mapper.Map<ProductDto>(productResult);

            return Ok(productDto);

        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]FilterRequestDto filterRequestDto)
        {
            var products = await productRepository.GetAsync(filterRequestDto);

            var productsDto = mapper.Map<List<ProductDto>>(products);

            if(productsDto == null)
            {
                return Ok("No products in database");
            }
            return Ok(productsDto);
        }

        [HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{id:Guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var getProductResult = await productRepository.GetByIdAsync(id);

            var productDto = mapper.Map<ProductDto>(getProductResult);

            return Ok(productDto);
        }

        [HttpDelete]
        [Microsoft.AspNetCore.Mvc.Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleteResult = await productRepository.DeleteAsync(id);

            if(deleteResult == null)
            {
                return NotFound();
            }

            return Ok("Deleted");
        }
    }
}
