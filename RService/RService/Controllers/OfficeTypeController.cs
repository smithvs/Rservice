using Microsoft.AspNetCore.Mvc;
using RService.Models;
using RService.Repositories.Interfaces;
using System.Xml.Linq;

namespace RService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OfficeTypeController : ControllerBase
    {
        private readonly ILogger<OfficeTypeController> _logger;
        private readonly IOfficeTypeRepository _repository;
        public OfficeTypeController(ILogger<OfficeTypeController> logger, IOfficeTypeRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }



        [HttpGet("OfficeType")]
        public ActionResult<OfficeType> OfficeType(int id)
        {
            var officetype = _repository.GetOfficeType(id);
            if (officetype != null)
            {
                return Ok(officetype);
            }
            else
            {
                return NotFound($"Оффис с id={id} не найден");
            }
        }

        [HttpGet("OfficeTypes")]
        public ActionResult<IEnumerable<OfficeType>> OfficeTypes()
        {
            return Ok(_repository.GetOfficeTypes());
        }

        [HttpPost("OfficeType")]
        public ActionResult<OfficeType> AddOfficeType([FromBody] OfficeType officetype)
        {
            return Ok(_repository.AddOfficeType(officetype));
        }

        [HttpDelete("OfficeType")]
        public ActionResult DeleteOfficeType(int id)
        {
            try
            {
                _repository.DeleteOfficeType(id);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("OfficeType")]
        public ActionResult<OfficeType> PutOfficeType([FromBody] OfficeType officetype)
        {
            try
            {
                return Ok(_repository.PutOfficeType(officetype));
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