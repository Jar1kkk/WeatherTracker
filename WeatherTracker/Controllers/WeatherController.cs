using Microsoft.AspNetCore.Mvc;
using System.Net;
using WeatherTracker.Data;
using WeatherTracker.Exceptions;
using WeatherTracker.Models;
using WeatherTracker.Resources;

namespace WeatherTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly WeatherRepository _repo;

        public WeatherController(WeatherRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_repo.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var forecast = _repo.GetById(id);
            if (forecast == null)
                throw new HttpStatusCodeException(HttpStatusCode.NotFound, ResourceHelper.GetString("WeatherNotFound"));

            return Ok(forecast);
        }

        [HttpPost]
        public IActionResult Add([FromBody] WeatherForecast forecast)
        {
            var newForecast = _repo.Add(forecast);
            return CreatedAtAction(nameof(GetById), new { id = newForecast.Id }, newForecast);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_repo.Delete(id)) return NotFound();
            return NoContent();
        }
    }
}
