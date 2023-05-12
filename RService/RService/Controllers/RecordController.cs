using Microsoft.AspNetCore.Mvc;
using RService.Models;
using RService.Repositories.Interfaces;
using System.Xml.Linq;

namespace RService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RecordController : ControllerBase
    {
        private readonly ILogger<RecordController> _logger;
        private readonly IRecordRepository _repository;
        public RecordController(ILogger<RecordController> logger, IRecordRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }



        [HttpGet("Record")]
        public ActionResult<Record> Record(int id)
        {
            var record = _repository.GetRecord(id);
            if (record != null)
            {
                return Ok(record);
            }
            else
            {
                return NotFound($"Запись с id={id} не найден");
            }
        }

        [HttpGet("Records")]
        public ActionResult<IEnumerable<Record>> Records()
        {
            return Ok(_repository.GetRecords());
        }

        [HttpPost("Record")]
        public ActionResult<Record> AddRecord([FromBody] Record record)
        {
            return Ok(_repository.AddRecord(record));
        }

        [HttpDelete("Record")]
        public ActionResult DeleteRecord(int id)
        {
            try
            {
                _repository.DeleteRecord(id);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("Record")]
        public ActionResult<Record> PutRecord([FromBody] Record record)
        {
            try
            {
                return Ok(_repository.PutRecord(record));
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