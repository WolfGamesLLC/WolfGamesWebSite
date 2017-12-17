using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using WolfGamesWebSite.Test.Framework.Facts;
using Xunit.Abstractions;
using Microsoft.AspNetCore.Routing;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Routing.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Formatters;
using WGMarbleMotionAPI.Infrastructure;

namespace WGMarbleMotionAPI.XUnitTestSuite
{
    /// <summary>
    /// Test suite for the <see cref="Startup"/> class
    /// </summary>
    public class StartupShould : BasicStartupShould
    {
        /// <summary>
        /// Test initializer
        /// </summary>
        /// <param name="testOutputHelper">Allows the test to write data to stdout</param>
        public StartupShould(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
            var startup = new Startup((new Mock<IConfiguration>()).Object);
            startup.ConfigureServices(_mockServices);

            _expectedServicesCount = 178;

            foreach (ServiceDescriptor serv in _mockServices)
            {
                OutputHelper.WriteLine(serv.ServiceType.FullName);
            }
        }

        [Fact]
        public void UseOnlyLowerCaseRouting()
        {
            Assert.NotNull(GetService<IConfigureOptions<RouteOptions>>(_mockServices));
        }

        [Fact]
        public void UseIonOutputFormatter()
        {
            Assert.NotNull(GetService<JsonOutputFormatter>(_mockServices));
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
