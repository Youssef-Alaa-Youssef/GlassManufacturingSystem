using Microsoft.Extensions.Localization;

namespace Factory.PL.Helper.Lang
{
    public class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly string _resourcesPath;

        public JsonStringLocalizerFactory(string resourcesPath)
        {
            _resourcesPath = resourcesPath ?? throw new ArgumentNullException(nameof(resourcesPath));
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            return new JsonStringLocalizer(_resourcesPath);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new JsonStringLocalizer(_resourcesPath);
        }
    }
}