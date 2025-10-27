using WeatherTracker.Models;
using System.Collections.Generic;
using System.Linq;

namespace WeatherTracker.Data
{
    public class WeatherRepository
    {
        private readonly List<WeatherForecast> _forecasts = new()
        {
            new WeatherForecast { Id = 1, CityName = "Kyiv", Date = new DateOnly(2025, 10, 21), TemperatureC = 15, Summary = "Cloudy" },
            new WeatherForecast { Id = 2, CityName = "Lviv", Date = new DateOnly(2025, 10, 21), TemperatureC = 12, Summary = "Rain" },
            new WeatherForecast { Id = 3, CityName = "Warsaw", Date = new DateOnly(2025, 10, 21), TemperatureC = 10, Summary = "Clear" },
            new WeatherForecast { Id = 4, CityName = "Berlin", Date = new DateOnly(2025, 10, 21), TemperatureC = 13, Summary = "Sunny" },
            new WeatherForecast { Id = 5, CityName = "Paris", Date = new DateOnly(2025, 10, 21), TemperatureC = 17, Summary = "Warm" }
        };

        public IEnumerable<WeatherForecast> FilterAndSort(
            string? cityName = null,
            string? summary = null,
            int? minTemp = null,
            int? maxTemp = null,
            bool? sortByCityAsc = null,
            bool? sortByTempAsc = null,
            bool? sortByDateAsc = null,
            PaginationParams? pagination = null)
        {
            var query = _forecasts.AsQueryable();

            if (!string.IsNullOrEmpty(cityName))
                query = query.Where(f => f.CityName.Contains(cityName, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(summary))
                query = query.Where(f => f.Summary.Contains(summary, StringComparison.OrdinalIgnoreCase));

            if (minTemp.HasValue)
                query = query.Where(f => f.TemperatureC >= minTemp.Value);

            if (maxTemp.HasValue)
                query = query.Where(f => f.TemperatureC <= maxTemp.Value);

            if (sortByCityAsc.HasValue)
                query = sortByCityAsc.Value
                    ? query.OrderBy(f => f.CityName)
                    : query.OrderByDescending(f => f.CityName);

            if (sortByTempAsc.HasValue)
                query = sortByTempAsc.Value
                    ? query.OrderBy(f => f.TemperatureC)
                    : query.OrderByDescending(f => f.TemperatureC);

            if (sortByDateAsc.HasValue)
                query = sortByDateAsc.Value
                    ? query.OrderBy(f => f.Date)
                    : query.OrderByDescending(f => f.Date);

            if (pagination != null)
            {
                query = query
                    .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                    .Take(pagination.PageSize);
            }

            return query.ToList();
        }

        public WeatherForecast? GetById(int id)
        {
            return _forecasts.FirstOrDefault(f => f.Id == id);
        }

        public WeatherForecast Add(WeatherForecast forecast)
        {
            forecast.Id = _forecasts.Any() ? _forecasts.Max(f => f.Id) + 1 : 1;
            _forecasts.Add(forecast);
            return forecast;
        }

        public bool Delete(int id)
        {
            var existing = _forecasts.FirstOrDefault(f => f.Id == id);
            if (existing == null) return false;
            _forecasts.Remove(existing);
            return true;
        }
    }
}
