using WeatherTracker.Models;

namespace WeatherTracker.Data
{
    public class CityRepository
    {
        private static List<City> _cities = new List<City>
        {
            new City { Id = 1, Name = "Kyiv", Country = "Ukraine", Temperature = 15.3 },
            new City { Id = 2, Name = "London", Country = "UK", Temperature = 10.1 }
        };

        public List<City> GetAll() => _cities;

        public City? GetById(int id) => _cities.FirstOrDefault(c => c.Id == id);

        public City Add(City city)
        {
            city.Id = _cities.Count > 0 ? _cities.Max(c => c.Id) + 1 : 1;
            _cities.Add(city);
            return city;
        }

        public bool Update(int id, City updated)
        {
            var city = GetById(id);
            if (city == null) return false;

            city.Name = updated.Name;
            city.Country = updated.Country;
            city.Temperature = updated.Temperature;
            return true;
        }

        public bool Delete(int id)
        {
            var city = GetById(id);
            if (city == null) return false;

            _cities.Remove(city);
            return true;
        }
    }
}
