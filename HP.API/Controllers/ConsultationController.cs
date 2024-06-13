using AutoMapper;
using HP.API.Models.DTOs;
using HP.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationController : Controller
    {
        private readonly IConsultationRepository consultationRepository;
        private readonly IMapper mapper;

        public ConsultationController(IConsultationRepository consultationRepository,IMapper mapper)
        {
            this.consultationRepository = consultationRepository;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddConsultationRequestDto addConsultationRequestDto)
        {
            
            try
            {
                var textCons = await consultationRepository.CreateAsync(addConsultationRequestDto);
                var textConsDto = mapper.Map<ConsultationDto>(textCons);
                return Ok(textConsDto);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var textCons = await consultationRepository.GetAllAsync();

            var textConsDto = mapper.Map<List<ConsultationDto>>(textCons);

            return Ok(textConsDto);
        }

        [HttpPut]
        [Route("update-status")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateTextConsStatusRequestDto model) {

            try
            {
                var textCons = await consultationRepository.UpdateStatusAsync(model);

                var textConsDto = mapper.Map<ConsultationDto>(textCons);

                return Ok(textConsDto);
            }
            catch(Exception ex) {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("validate-consultation")]
        public async Task<IActionResult> IsValidCons(string userId,DateTime date,int typeId)
        {
            bool isValid = await consultationRepository.CheckValidConsAsync(userId, date, typeId);

            return Ok(isValid);
        }

        [HttpGet]
        [Route("today-consultation")]
        public async Task<IActionResult> GetTodaysCons(DateTime date, string? userId)
        {
            var textCons = await consultationRepository.GetTodaysAsync(date,userId);

            var textConsDto = mapper.Map<List<ConsultationDto>>(textCons);

            return Ok(textConsDto);
        }

        [HttpGet]
        [Route("consultation-by-user/{id}")]
        public async Task<IActionResult> GetAllByUser([FromRoute]string id)
        {
            var textCons = await consultationRepository.GetAllByUserAsync(id);

            var textConsDto = mapper.Map<List<ConsultationDto>>(textCons);
            return Ok(textConsDto);
        }

   

        [HttpGet]
        [Route("consultation/{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) {
            try
            {
                var textCons = await consultationRepository.GetByIdAsync(id);

                var textConsDto = mapper.Map<ConsultationDto>(textCons);

                return Ok(textConsDto);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet]
        [Route("vet/{id}")]
        public async Task<IActionResult> GetByVet([FromRoute]string id,DateTime date)
        {
            try
            {
                var textCons = await consultationRepository.GetByVetAsync(id,date);

                var textConsDto = mapper.Map<List<ConsultationDto>>(textCons);
                return Ok(textConsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("video")]
        public async Task<IActionResult> GetVideoCons([FromQuery] string? id, [FromQuery] DateTime? date, [FromQuery] bool? done)
        {
            try
            {
                var videoCons = await consultationRepository.GetVideoAsync(id,date,done);

                var videoConsDto = mapper.Map<List<ConsultationDto>>(videoCons);

                return Ok(videoConsDto);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("text")]
        public async Task<IActionResult> GetTextCons([FromQuery] string? id, [FromQuery] DateTime? date, 
            [FromQuery] bool? today, [FromQuery]bool? done, [FromQuery]Guid? consId)
        {
            try
            {
                var textCons = await consultationRepository.GetTextAsync(id,date,today,done,consId);

                var textConsDto = mapper.Map<List<ConsultationDto>>(textCons);

                return Ok(textConsDto);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("emergency")]
        public async Task<IActionResult> GetEmergencyCons([FromQuery] string? id, [FromQuery] DateTime? date, [FromQuery]bool? done)
        {
            try
            {
                var emCons = await consultationRepository.GetEmergencyAsync(id,date,done);

                var emConsDto = mapper.Map<List<ConsultationDto>>(emCons);

                return Ok(emConsDto);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("start-consultation")]
        public async Task<IActionResult> StartConsultation([FromQuery]Guid consId, [FromQuery] string userId)
        {
            try
            {
                var cons = await consultationRepository.StartConsultationAsync(consId, userId);

                var consDto = mapper.Map<ConsultationDto>(cons);

                return Ok(consDto);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
