using Microsoft.AspNetCore.Mvc;

namespace VanThiel.Infrastructure.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

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
        public async ValueTask<IActionResult> TestCancellationTokenAsync(CancellationToken cancellationToken = default(CancellationToken)) {
            try
            {
                await Task.Run(async () => {
                    while (!cancellationToken.IsCancellationRequested) {
                        await Task.Delay(500);
                        Console.WriteLine("0");

                    }
                }, cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();
            } catch (TaskCanceledException ex)
            {

                Console.WriteLine($"{ex}");
                return Ok($"{ex}");
            }
            
            catch (Exception ex)
            {

                Console.WriteLine($"{ex}");
                return Ok($"{ex}");
            }

            return Ok();
        }

        //private Task TestAsync(CancellationToken cancellationToken) { 
            
        //}
    }
}
