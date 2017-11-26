using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WolfGamesWebSite.Test.Framework.Mocks;
using WolfGamesWebSite.Test.Framework.Identifiers;
using Xunit;
using WGSystem.Collections.Generic;

namespace WGMarbleMotionAPI.XUnitTestSuite
{
    /// <summary>
    /// Test suite for the <see cref="Startup"/> class
    /// </summary>
    public class StartupShould
    {
        [Fact]    
        public void AddMVCToServicePipeline()
        {
            var list = new WGGenericCollectionsFactory().CreateList<string>();
            var mockConfig = new Mock<IConfiguration>();
            var mockServices = new MockServiceCollection(list);
            var startUp = new Startup(mockConfig.Object);

            startUp.ConfigureServices(mockServices);

            Assert.Contains<string>(MockServiceCollectionIdentifiers.MVCAdded(), list);
        }
    }
}
