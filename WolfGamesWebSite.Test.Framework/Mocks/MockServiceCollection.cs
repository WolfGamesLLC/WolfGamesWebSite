using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using WGSystem.Collections.Generic;
using WolfGamesWebSite.Test.Framework.Identifiers;

namespace WolfGamesWebSite.Test.Framework.Mocks
{
    /// <summary>
    /// A mock service collection
    /// </summary>
    public class MockServiceCollection : ServiceCollection
    {
        private static IList<string> _services { get; set; }

        /// <summary>
        /// Create a MockServiceCollection using the provided list of
        /// strings to hold the names of the services that will be added
        /// </summary>
        /// <param name="services">A list of strings, normally this would be empty</param>
        public MockServiceCollection(IList<string> services)
        {
            _services = services;
        }

        /// <summary>
        /// Overload the AddMvc operation
        /// </summary>
        /// <returns></returns>
        public static IMvcBuilder AddMvc()
        {
            _services.Add(MockServiceCollectionIdentifiers.MVCAdded());
            return null;
        }
    }
}
