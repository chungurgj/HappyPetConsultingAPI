using AutoMapper;
using HP.API.Models.Domain;
using HP.API.Models.DTOs;
using HP.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   
    public class VetVisitController : Controller
    {
        private readonly IVetVisitRepository vetVisitRepository;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly IEmailSender emailSender;

        public VetVisitController(IVetVisitRepository vetVisitRepository,IMapper mapper,
            UserManager<User> userManager,IEmailSender emailSender)
        {
            this.vetVisitRepository = vetVisitRepository;
            this.mapper = mapper;
            this.userManager = userManager;
            this.emailSender = emailSender;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddVetVisitRequestDto addVetVisitRequestDto) {

            try
            {
                var vetVisit = await vetVisitRepository.CreateAsync(addVetVisitRequestDto);
                var vetVisitDto = mapper.Map<VetVisitDto>(vetVisit);

                return Ok(vetVisitDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] FilterVetVisitDto filterVetVisitDto)
        {
            var vetVisits = await vetVisitRepository.GetAllAsync(filterVetVisitDto);

            var vetVisitsDto = mapper.Map<List<VetVisitDto>>(vetVisits);

            return Ok(vetVisitsDto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByUser([FromRoute] string id)
        {
            var vetVisits = await vetVisitRepository.GetByUserAsync(id);

            var vetVisitsDto = mapper.Map <List<VetVisitDto>>(vetVisits);

            return Ok(vetVisitsDto);
        }

        [HttpPut]
        [Route("update/vet-visit")]
        public async Task<IActionResult> Update([FromBody] UpdateVetVisitRequestDto updateVetVisitRequestDto) { 
            
            var vetVisit = await vetVisitRepository.UpdateAsync(updateVetVisitRequestDto);

            if(vetVisit == null) {
                return NotFound();
            }

            var vetVisitDto = mapper.Map<VetVisitDto>(vetVisit);

            return Ok(vetVisitDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id,string message) {

            try
            {
                var vetVisit = await vetVisitRepository.DeleteAsync(id, message);

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
