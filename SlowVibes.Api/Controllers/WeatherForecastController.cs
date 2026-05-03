using Microsoft.AspNetCore.Mvc;
using Persistence.UoW;
using Domain.Entities.User;

namespace SlowVibes.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        IUnitOfWork _unitOfWork;

        public WeatherForecastController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("test")]
        public async Task<IActionResult> GetWithDb()
        {
            try
            {
                var users = await _unitOfWork.Repository<Users, int>().GetAllAsync();
                return Ok(new
                {
                    Status = "Conexion exitosa",
                    DataBaseCount = users.Count(),
                    Source = "Unit of work"
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error de conexion {ex.Message}");
            }
        }
    }
}
