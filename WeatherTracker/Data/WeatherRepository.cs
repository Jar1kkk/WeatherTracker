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

        public IEnumerable<WeatherForecast> GetAll()
        {
            return _forecasts;
        }

        public IEnumerable<WeatherForecast> FilterAndSort(
            string? cityName = null,
            string? summary = null,
            int? minTemp = null,
            int? maxTemp = null,
            bool? sortByCityAsc = null,
            bool? sortByTempAsc = null,
            bool? sortByDateAsc = null)
        {
            var query = _forecasts.AsQueryable();

            
            if (!string.IsNullOrWhiteSpace(cityName))
                query = query.Where(f => f.CityName.ToLower().Contains(cityName.ToLower()));

            if (!string.IsNullOrWhiteSpace(summary))
                query = query.Where(f => f.Summary.ToLower().Contains(summary.ToLower()));

            if (minTemp.HasValue)
                query = query.Where(f => f.TemperatureC >= minTemp.Value);

            if (maxTemp.HasValue)
                query = query.Where(f => f.TemperatureC <= maxTemp.Value);

            
            if (sortByCityAsc.HasValue)
                query = sortByCityAsc.Value ? query.OrderBy(f => f.CityName) : query.OrderByDescending(f => f.CityName);

            if (sortByTempAsc.HasValue)
                query = sortByTempAsc.Value ? query.OrderBy(f => f.TemperatureC) : query.OrderByDescending(f => f.TemperatureC);

            if (sortByDateAsc.HasValue)
                query = sortByDateAsc.Value ? query.OrderBy(f => f.Date) : query.OrderByDescending(f => f.Date);

            return query.ToList();
        }





        //private static List<WeatherForecast> _forecasts = new List<WeatherForecast>();

        //public List<WeatherForecast> GetAll() => _forecasts;

        public WeatherForecast? GetById(int id) => _forecasts.FirstOrDefault(f => f.Id == id);

        public WeatherForecast Add(WeatherForecast forecast)
        {
            forecast.Id = _forecasts.Count > 0 ? _forecasts.Max(f => f.Id) + 1 : 1;
            _forecasts.Add(forecast);
            return forecast;
        }

        public bool Delete(int id)
        {
            var f = GetById(id);
            if (f == null) return false;
            _forecasts.Remove(f);
            return true;
        }


    }
}
