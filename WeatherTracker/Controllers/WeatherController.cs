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
        //private readonly WeatherRepository _repo;

        //public WeatherController(WeatherRepository repo)
        //{
        //    _repo = repo;
        //}

        private readonly WeatherRepository _repo = new WeatherRepository();

        [HttpGet]
        public IActionResult GetAll(
                   string? city = null,
                   string? summary = null,
                   int? minTemp = null,
                   int? maxTemp = null,
                   bool? sortTempAsc = null,
                   int pageNumber = 1,
                   int pageSize = 10)
        {
            var pagination = new PaginationParams { PageNumber = pageNumber, PageSize = pageSize };

            var forecasts = _repo.FilterAndSort(
                cityName: city,
                summary: summary,
                minTemp: minTemp,
                maxTemp: maxTemp,
                sortByTempAsc: sortTempAsc,
                pagination: pagination);

            return Ok(forecasts);
        }

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

        [HttpGet("filter")]
        public IActionResult FilterWeather(
            [FromQuery] string? cityName,
            [FromQuery] string? summary,
            [FromQuery] int? minTemp,
            [FromQuery] int? maxTemp,
            [FromQuery] bool? sortByCityAsc,
            [FromQuery] bool? sortByTempAsc,
            [FromQuery] bool? sortByDateAsc)
        {
            var result = _repo.FilterAndSort(cityName, summary, minTemp, maxTemp, sortByCityAsc, sortByTempAsc, sortByDateAsc);
            return Ok(result);
        }
    }
}
