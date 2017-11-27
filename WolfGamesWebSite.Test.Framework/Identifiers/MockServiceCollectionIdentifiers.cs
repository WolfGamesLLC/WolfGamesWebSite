using System;
using System.Collections.Generic;
using System.Text;

namespace WolfGamesWebSite.Test.Framework.Identifiers
{
    /// <summary>
    /// The group of constants used to contain mock service collection related
    /// identifiers. Hopefully this will prevent the primitive obsession 
    /// smell, at least in this case
    /// </summary>
    public class MockServiceCollectionIdentifiers
    {
        private string _identifier;

        /// <summary>
        /// The Identifier accessor
        /// </summary>
        public string Identifier
        {
            get
            {
                return _identifier;
            }
        }

        /// <summary>
        /// The default constructor
        /// </summary>
        /// <param name="identifier">The text the message will be set to</param>
        public MockServiceCollectionIdentifiers(string identifier)
        {
            this._identifier = identifier;
        }

        /// <summary>
        /// The add mvc accessor
        /// </summary>
        /// <returns>The add mvc identifier</returns>
        public static string MVCAdded()
        {
            return new MockServiceCollectionIdentifiers("MVC Added").Identifier;
        }
    }
}
