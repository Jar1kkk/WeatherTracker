using FluentValidation;
using WeatherTracker.Models;

namespace WeatherTracker.Validators
{
    public class WeatherForecastValidator : AbstractValidator<WeatherForecast>
    {
        public WeatherForecastValidator()
        {
            RuleFor(f => f.CityName).NotEmpty().Length(2, 100);
            RuleFor(f => f.TemperatureC).InclusiveBetween(-100, 100);
            //object DateRule = RuleFor(static f => f.Date).LessThanOrEqualTo(DateTime.Now).WithMessage("Date cannot be in the future");
            RuleFor(f => f.Summary).MaximumLength(250);
        }
    }
}
