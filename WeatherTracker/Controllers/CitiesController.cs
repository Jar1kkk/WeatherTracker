using Microsoft.AspNetCore.Mvc;
using WeatherTracker.Data;
using WeatherTracker.Models;

namespace WeatherTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly CityRepository _repo;

        public CitiesController(CityRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_repo.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var city = _repo.GetById(id);
            if (city == null) return NotFound();
            return Ok(city);
        }

        [HttpPost]
        public IActionResult Add([FromBody] City city)
        {
            var newCity = _repo.Add(city);
            return CreatedAtAction(nameof(GetById), new { id = newCity.Id }, newCity);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] City city)
        {
            if (!_repo.Update(id, city)) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_repo.Delete(id)) return NotFound();
            return NoContent();
        }
    }
}
