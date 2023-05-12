using Microsoft.AspNetCore.Mvc;
using RService.Models;
using RService.Repositories.Interfaces;
using System.Xml.Linq;

namespace RService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SpecialistController : ControllerBase
    {
        private readonly ILogger<SpecialistController> _logger;
        private readonly ISpecialistRepository _repository;
        public SpecialistController(ILogger<SpecialistController> logger, ISpecialistRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("Specialist")]
        public ActionResult<Specialist> Specialist(int id)
        {
            var specialist = _repository.GetSpecialist(id);
            if (specialist != null)
            {
                return Ok(specialist);
            }
            else
            {
                return NotFound($"Специалист с id={id} не найден");
            }
        }

        [HttpGet("Specialists")]
        public ActionResult<IEnumerable<Specialist>> Specialists()
        {
            return Ok(_repository.GetSpecialists());
        }

        [HttpPost("Specialist")]
        public ActionResult<Specialist> AddSpecialist([FromBody] Specialist specialist)
        {
            return Ok(_repository.AddSpecialist(specialist));
        }

        [HttpDelete("Specialist")]
        public ActionResult DeleteSpecialist(int id)
        {
            try
            {
                _repository.DeleteSpecialist(id);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("Specialist")]
        public ActionResult<Specialist> PutSpecialist([FromBody] Specialist specialist)
        {
            try
            {
                return Ok(_repository.PutSpecialist(specialist));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

    }
}



