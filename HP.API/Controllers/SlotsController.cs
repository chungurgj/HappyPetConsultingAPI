using HP.API.Models.Domain;
using HP.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SlotsController : Controller
    {
        private readonly IVetVisitRepository vetVisitRepository;
        private readonly IConsultationRepository consultationRepository;

        public SlotsController(IVetVisitRepository vetVisitRepository,IConsultationRepository consultationRepository)
        {
            this.vetVisitRepository = vetVisitRepository;
            this.consultationRepository = consultationRepository;
        }

        [HttpGet]
        [Route("get-all-visit-slots")]
        public async Task<IActionResult> GetSlots([FromQuery]DateTime date)
        {
            
            var slots = await vetVisitRepository.GetAllSlotsAsync(date);

            return Ok(slots);
        }

        [HttpGet]
        [Route("get-available-visit-slots")]
        public async Task<IActionResult> GetAvailableSlots([FromQuery]DateTime date) { 
            var slots = await vetVisitRepository.GetAvailableSlotsAsync(date);

            return Ok(slots);
        }

        [HttpGet]
        [Route("get-all-cons-slots")]
        public async Task<IActionResult> GetConsSlots([FromQuery]DateTime date) {
            var slots = await consultationRepository.GetAllSlotsAsync(date);

            return Ok(slots);
        }

        

        
        
        
    }
}
