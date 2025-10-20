namespace WeatherTracker.Models
{
    public class WeatherForecast
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public DateTime Date { get; set; }
        public double TemperatureC { get; set; }
        public string Summary { get; set; }
    }
}
