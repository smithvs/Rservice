using Microsoft.AspNetCore.Mvc;
using RService.Models;
using RService.Repositories.Interfaces;
using System.Xml.Linq;

namespace RService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OfficeController : ControllerBase
    {
        private readonly ILogger<OfficeController> _logger;
        private readonly IOfficeRepository _repository;
        public OfficeController(ILogger<OfficeController> logger, IOfficeRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }



        [HttpGet("Office")]
        public ActionResult<Office> Office(int id)
        {
            var office = _repository.GetOffice(id);
            if (office != null)
            {
                return Ok(office);
            }
            else
            {
                return NotFound($"Оффис с id={id} не найден");
            }
        }

        [HttpGet("Offices")]
        public ActionResult<IEnumerable<Office>> Offices()
        {
            return Ok(_repository.GetOffices());
        }

        [HttpPost("Office")]
        public ActionResult<Office> AddOffice([FromBody] Office office)
        {
            return Ok(_repository.AddOffice(office));
        }

        [HttpDelete("Office")]
        public ActionResult DeleteOffice(int id)
        {
            try
            {
                _repository.DeleteOffice(id);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("Office")]
        public ActionResult<Office> PutOffice([FromBody] Office office)
        {
            try
            {
                return Ok(_repository.PutOffice(office));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

    }
}


//using Microsoft.AspNetCore.Mvc;

//namespace RService.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class WeatherForecastController : ControllerBase
//    {
//        private static readonly string[] Summaries = new[]
//        {
//        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//    };

//        private readonly ILogger<WeatherForecastController> _logger;

//        public WeatherForecastController(ILogger<WeatherForecastController> logger)
//        {
//            _logger = logger;
//        }

//        [HttpGet(Name = "GetWeatherForecast")]
//        public IEnumerable<WeatherForecast> Get()
//        {
//            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
//            {
//                Date = DateTime.Now.AddDays(index),
//                TemperatureC = Random.Shared.Next(-20, 55),
//                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
//            })
//            .ToArray();
//        }
//    }
//}