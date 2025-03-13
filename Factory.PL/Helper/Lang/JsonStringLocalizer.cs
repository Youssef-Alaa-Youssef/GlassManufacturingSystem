using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Text.Json;

namespace Factory.PL.Helper.Lang
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        private readonly string _resourcesPath;

        public JsonStringLocalizer(string resourcesPath)
        {
            _resourcesPath = resourcesPath ?? throw new ArgumentNullException(nameof(resourcesPath));
        }

        public LocalizedString this[string name]
        {
            get
            {
                var value = GetString(name);
                return new LocalizedString(name, value ?? name, resourceNotFound: value == null);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var format = GetString(name);
                var value = string.Format(format ?? name, arguments);
                return new LocalizedString(name, value, resourceNotFound: format == null);
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var culture = CultureInfo.CurrentUICulture;
            var filePath = Path.Combine(_resourcesPath, $"{culture.Name}.json");

            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

                foreach (var keyValuePair in dictionary)
                {
                    yield return new LocalizedString(keyValuePair.Key, keyValuePair.Value, resourceNotFound: false);
                }
            }
        }

        private string GetString(string name)
        {
            var culture = CultureInfo.CurrentUICulture;
            var filePath = Path.Combine(_resourcesPath, $"{culture.Name}.json");
            Console.WriteLine($"Looking for resource file: {filePath}");

            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var dictionary = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

                if (dictionary.TryGetValue(name, out var value))
                {
                    return value;
                }
            }

            return null;
        }
    }
}