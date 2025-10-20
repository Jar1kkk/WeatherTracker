using WeatherTracker.Models;

namespace WeatherTracker.Data
{
    public class WeatherRepository
    {
        private static List<WeatherForecast> _forecasts = new List<WeatherForecast>();

        public List<WeatherForecast> GetAll() => _forecasts;

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
