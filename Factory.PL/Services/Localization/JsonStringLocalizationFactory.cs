﻿using Microsoft.Extensions.Localization;

namespace Factory.PL.Services.Localization
{
    public class JsonStringLocalizationFactory : IStringLocalizerFactory
    {
        public IStringLocalizer Create(Type resourceSource)
        {
            return new JsonStringLocalization();
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new JsonStringLocalization();
        }
    }
}
