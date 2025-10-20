using FluentValidation;
using WeatherTracker.Models;

namespace WeatherTracker.Validators
{
    public class CityValidator : AbstractValidator<City>
    {
        public CityValidator()
        {
            RuleFor(c => c.Name).NotEmpty().Length(2, 100);
            RuleFor(c => c.Country).NotEmpty().Length(2, 100);
            RuleFor(c => c.Temperature).InclusiveBetween(-100, 100);
        }
    }
}
