using System.Resources;
using System.Reflection;

namespace WeatherTracker.Resources
{
    public static class ResourceHelper
    {
        private static readonly ResourceManager _resourceManager =
            new ResourceManager("WeatherTracker.Resources.Messages", Assembly.GetExecutingAssembly());

        public static string GetString(string key)
        {
            return _resourceManager.GetString(key) ?? $"Missing resource: {key}";
        }
    }
}
