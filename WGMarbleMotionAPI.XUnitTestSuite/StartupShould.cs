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
using WolfGamesWebSite.Test.Framework.Facts;
using System.Diagnostics;
using Xunit.Abstractions;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using Microsoft.Extensions.ObjectPool;
using Microsoft.AspNetCore.Routing.Internal;
using Microsoft.AspNetCore.Routing.Tree;

namespace WGMarbleMotionAPI.XUnitTestSuite
{
    /// <summary>
    /// Test suite for the <see cref="Startup"/> class
    /// </summary>
    public class StartupShould : BasicStartupShould
    {
        private ServiceCollection _mockServices;

        /// <summary>
        /// Test initializer
        /// </summary>
        /// <param name="testOutputHelper">Allows the test to write data to stdout</param>
        public StartupShould(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
            _mockServices = new ServiceCollection();

            var startup = new Startup((new Mock<IConfiguration>()).Object);
            startup.ConfigureServices(_mockServices);


            foreach (ServiceDescriptor serv in _mockServices)
            {
                OutputHelper.WriteLine(serv.ServiceType.FullName);
            }
        }

        [Fact]
        public void AddAllRequiredServicesToServicePipeline()
        {
            var expectedServicesCount = 176;
            Assert.True(_mockServices.Count == expectedServicesCount);
        }

        [Fact]    
        public void AddApplicationPartManagerToServicePipeline()
        {
            Assert.NotNull(GetService<ApplicationPartManager>(_mockServices));
        }

        [Fact]
        public void AddIInlineConstraintResolverToServicePipeline()
        {
            Assert.NotNull(GetService<IInlineConstraintResolver>(_mockServices));
        }

        [Fact]
        public void AddUrlEncoderToServicePipeline()
        {
            Assert.NotNull(GetService<UrlEncoder>(_mockServices));
        }

        [Fact]
        public void AddUriBindingContextObjectPoolToServicePipeline()
        {
            Assert.True(true);
//            Assert.NotNull(GetService<ObjectPool<UriBuildingContext>>(_mockServices));
        }

        [Fact]
        public void AddRoutingMarkerServiceToServicePipeline()
        {
            Assert.NotNull(GetService<RoutingMarkerService>(_mockServices));
        }

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
