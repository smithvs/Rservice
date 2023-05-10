using Microsoft.AspNetCore.Mvc;
using RService.Models;
using RService.Repositories.Interfaces;
using System.Xml.Linq;

namespace RService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly IClientRepository _repository;
        public ClientController(ILogger<ClientController> logger, IClientRepository repository) {
            _logger = logger;
            _repository = repository;
        }



        [HttpGet("Client")]
        public ActionResult<Client> Client(int id)
        {
            var client = _repository.GetClient(id);
            if (client != null)
            {
                return Ok(client);
            }
            else
            {
                return NotFound($"Клиент с id={id} не найден");
            }
        }

        [HttpGet("Clients")]
        public ActionResult<IEnumerable<Client>> Clients() { 
            return  Ok(_repository.GetClients());
        }

        [HttpPost("Client")]
        public ActionResult<Client> AddClient([FromBody]Client client)
        {
            return Ok(_repository.AddClient(client));
        }

        [HttpDelete("Client")]
        public ActionResult DeleteClient(int id) {
            try
            {
                _repository.DeleteClient(id);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }            
        }

        [HttpPut("Client")]
        public ActionResult<Client> PutClient([FromBody] Client client)
        {
            try
            {
                return Ok(_repository.PutClient(client));
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