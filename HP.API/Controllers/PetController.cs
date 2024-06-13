using AutoMapper;
using HP.API.Models.Domain;
using HP.API.Models.DTOs;
using HP.API.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : Controller
    {
        private readonly IPetRepository petRepository;
        private readonly IMapper mapper;

        public PetController(IPetRepository petRepository,IMapper mapper)
        {
            this.petRepository = petRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]AddPetRequestDto addPetRequestDto) {

            var petResult = await petRepository.CreateAsync(addPetRequestDto);

            if (petResult == null)
            {
                return NotFound();
            }

            var petDto = mapper.Map<PetDto>(petResult);

            return Ok(petDto);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var pets = await petRepository.GetAllAsync();
            var petDto = mapper.Map<List<PetDto>>(pets);

            return Ok(petDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) { 
            
            var petResult = await petRepository.GetByIdAsync(id);

            if(petResult == null)
            {
                return NotFound(id);
            }

            var petDto = mapper.Map<PetDto>(petResult);

            return Ok(petDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var deleteResult = await petRepository.DeleteAsync(id);

            if(deleteResult == null)
            {
                return NotFound();
            }

            return Ok("Deleted");
        }

        [HttpGet]
        [Route("by-name")]
        public async Task<IActionResult> GetByName([FromQuery]string? petName, [FromQuery] string? petAnimal)
        {
            var pets = await petRepository.GetByName(petName, petAnimal);

            var petsDto = mapper.Map<List<PetDto>>(pets);

            return Ok(petsDto);
        }

        [HttpGet]
        [Route("by-owner/{id}")]
        public async Task<IActionResult> GetByOwner([FromRoute] string id)
        {
            var pets = await petRepository.GetByOwnerAsync(id);

            var petsDto = mapper.Map<List<PetDto>>(pets);
            return Ok(petsDto);
        }
    }
}
