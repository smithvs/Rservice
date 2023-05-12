using Microsoft.AspNetCore.Mvc;
using RService.Models;
using RService.Repositories.Interfaces;
using System.Xml.Linq;

namespace RService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly ILogger<ServiceController> _logger;
        private readonly IServiceRepository _repository;
        public ServiceController(ILogger<ServiceController> logger, IServiceRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("Service")]
        public ActionResult<Service> Service(int id)
        {
            var service = _repository.GetService(id);
            if (service != null)
            {
                return Ok(service);
            }
            else
            {
                return NotFound($"Клиент с id={id} не найден");
            }
        }

        [HttpGet("Services")]
        public ActionResult<IEnumerable<Service>> Services()
        {
            return Ok(_repository.GetServices());
        }

        [HttpPost("Service")]
        public ActionResult<Service> AddService([FromBody] Service service)
        {
            return Ok(_repository.AddService(service));
        }

        [HttpDelete("Service")]
        public ActionResult DeleteService(int id)
        {
            try
            {
                _repository.DeleteService(id);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("Service")]
        public ActionResult<Service> PutService([FromBody] Service service)
        {
            try
            {
                return Ok(_repository.PutService(service));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

    }
}


