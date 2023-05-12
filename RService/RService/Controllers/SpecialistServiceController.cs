using Microsoft.AspNetCore.Mvc;
using RService.Models;
using RService.Repositories.Interfaces;
using System.Xml.Linq;

namespace RService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SpecialistServiceController : ControllerBase
    {
        private readonly ILogger<SpecialistServiceController> _logger;
        private readonly ISpecialistServiceRepository _repository;
        public SpecialistServiceController(ILogger<SpecialistServiceController> logger, ISpecialistServiceRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }



        [HttpGet("SpecialistService")]
        public ActionResult<SpecialistService> SpecialistService(int id)
        {
            var specialistservice = _repository.GetSpecialistService(id);
            if (specialistservice != null)
            {
                return Ok(specialistservice);
            }
            else
            {
                return NotFound($"СпециалистСервис с id={id} не найден");
            }
        }

        [HttpGet("SpecialistServices")]
        public ActionResult<IEnumerable<SpecialistService>> SpecialistServices()
        {
            return Ok(_repository.GetSpecialistServices());
        }

        [HttpPost("SpecialistService")]
        public ActionResult<SpecialistService> AddSpecialistService([FromBody] SpecialistService specialistservice)
        {
            return Ok(_repository.AddSpecialistService(specialistservice));
        }

        [HttpDelete("SpecialistService")]
        public ActionResult DeleteSpecialistService(int id)
        {
            try
            {
                _repository.DeleteSpecialistService(id);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("SpecialistService")]
        public ActionResult<SpecialistService> PutSpecialistService([FromBody] SpecialistService specialistservice)
        {
            try
            {
                return Ok(_repository.PutSpecialistService(specialistservice));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

    }
}
