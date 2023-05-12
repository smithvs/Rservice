using Microsoft.AspNetCore.Mvc;
using RService.Models;
using RService.Repositories.Interfaces;
using System.Xml.Linq;

namespace RService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ServiceOfficeController : ControllerBase
    {
        private readonly ILogger<ServiceOfficeController> _logger;
        private readonly IServiceOfficeRepository _repository;
        public ServiceOfficeController(ILogger<ServiceOfficeController> logger, IServiceOfficeRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("ServiceOffice")]
        public ActionResult<ServiceOffice> ServiceOffice(int id)
        {
            var serviceoffice = _repository.GetServiceOffice(id);
            if (serviceoffice != null)
            {
                return Ok(serviceoffice);
            }
            else
            {
                return NotFound($"СервисОффис с id={id} не найден");
            }
        }

        [HttpGet("ServiceOffices")]
        public ActionResult<IEnumerable<ServiceOffice>> ServiceOffices()
        {
            return Ok(_repository.GetServiceOffices());
        }

        [HttpPost("ServiceOffice")]
        public ActionResult<ServiceOffice> AddServiceOffice([FromBody] ServiceOffice serviceoffice)
        {
            return Ok(_repository.AddServiceOffice(serviceoffice));
        }

        [HttpDelete("ServiceOffice")]
        public ActionResult DeleteServiceOffice(int id)
        {
            try
            {
                _repository.DeleteServiceOffice(id);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("ServiceOffice")]
        public ActionResult<ServiceOffice> PutServiceOffice([FromBody] ServiceOffice serviceoffice)
        {
            try
            {
                return Ok(_repository.PutServiceOffice(serviceoffice));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

    }
}


