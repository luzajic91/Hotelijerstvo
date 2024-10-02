using HotelMan2.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelMan2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly HotelContext _dbContext;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, HotelContext dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        //[HttpGet(Name = "GetPeople")]
        public IEnumerable<dynamic> Get()
        {
            return _dbContext.People.ToList();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //})
            //.ToArray();
        }
    }
}
