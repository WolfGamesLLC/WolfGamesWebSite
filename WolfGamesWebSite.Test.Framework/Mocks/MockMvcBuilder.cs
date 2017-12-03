using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace WolfGamesWebSite.Test.Framework.Mocks
{
    public class MockMvcBuilder : IMvcBuilder
    {
        public IServiceCollection Services => throw new NotImplementedException();

        public ApplicationPartManager PartManager => throw new NotImplementedException();
    }
}
