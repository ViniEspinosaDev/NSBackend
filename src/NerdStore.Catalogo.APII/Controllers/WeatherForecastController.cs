using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NerdStore.Catalogo.APII.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : MainController
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

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("hello-world/{id}")]
        public ActionResult<string> HelloWorld(int id, Produto produto, [FromForm] Produto produto1)
        {
            return $"Hello world, {id}";
        }


    }

    [ApiController]
    public abstract class MainController : Controller
    {
        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new 
            {
                success = false,
                errors = ObterErros()
            });
        }

        protected bool OperacaoValida()
        {
            // Implementar validações
            return true;
        }

        protected IEnumerable<string> ObterErros()
        {
            return new List<string>() { "Erro 1", "Erro 2"};
        }
    }

    public class Produto
    {
        Guid Id { get; set; }
        string Nome { get; set; }
    }
}
