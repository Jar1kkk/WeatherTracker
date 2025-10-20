using Microsoft.AspNetCore.Mvc;
using WeatherTracker.Data;
using WeatherTracker.Models;

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
            var f = _repo.GetById(id);
            if (f == null) return NotFound();
            return Ok(f);
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
