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
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace WolfGamesWebSite.Test.Framework.Facts
{
    /// <summary>
    /// Test suite for the <see cref="Startup"/> class containing
    /// tests for the common features used by all sites and APIs
    /// </summary>
    public class BasicStartupShould
    {
//        [Fact]    
//        public void AddApplicationPartManagerToServicePipeline()
//        {
//            var mockConfig = new Mock<IConfiguration>();
//            var mockServices = new ServiceCollection();
//            var startUp = new Startup(mockConfig.Object);
//
//            startUp.ConfigureServices(mockServices);
//
//            var serviceProvider = mockServices.BuildServiceProvider();
//            
//            var controller = serviceProvider.GetService<ApplicationPartManager>();
//            Assert.NotNull(controller);
//        }

        //  Arrange

        //  Setting up the stuff required for Configuration.GetConnectionString("DefaultConnection")
        //        Mock<IConfigurationSection> configurationSectionStub = new Mock<IConfigurationSection>();
        //        configurationSectionStub.Setup(x => x["DefaultConnection"]).Returns("TestConnectionString");
        //        Mock<Microsoft.Extensions.Configuration.IConfiguration> configurationStub = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
        //        configurationStub.Setup(x => x.GetSection("ConnectionStrings")).Returns(configurationSectionStub.Object);
        //
        //        IServiceCollection services = new ServiceCollection();
        //        var target = new Startup(configurationStub.Object);
        //
        //        //  Act
        //
        //        target.ConfigureServices(services);
        //    //  Mimic internal asp.net core logic.
        //    services.AddTransient<TestController>();
        //
        //    //  Assert
        //
        //    var serviceProvider = services.BuildServiceProvider();
        //
        //        var controller = serviceProvider.GetService<TestController>();
        //        Assert.IsNotNull(controller);
    }
}
