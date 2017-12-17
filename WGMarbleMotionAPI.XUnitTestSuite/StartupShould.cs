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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.Routing;

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
            startup.ConfigureServices(_services);

            _expectedServicesCount = 186;

            foreach (ServiceDescriptor serv in _services)
            {
                OutputHelper.WriteLine(serv.ServiceType.FullName);
            }
        }

        /// <summary>
        /// Test that the routing lower case converter service is being used
        /// </summary>
        [Fact]
        public void UseOnlyLowerCaseRouting()
        {
            Assert.NotNull(GetService<IConfigureOptions<RouteOptions>>(_services));
        }

        /// <summary>
        /// Verify that we are using a json output formatter
        /// </summary>
        [Fact]
        public void UseIonOutputFormatter()
        {
            Assert.NotNull(GetService<JsonOutputFormatter>(_services));
        }

        /// <summary>
        /// Verify that an API Version reader service is present
        /// </summary>
        [Fact]
        public void UseApiVersionReader()
        {
            Assert.NotNull(GetService<IApiVersionReader>(_services));
        }

        /// <summary>
        /// Verify that an API Version selector service is present
        /// </summary>
        [Fact]
        public void UseApiVersionSelector()
        {
            Assert.NotNull(GetService<IApiVersionSelector>(_services));
        }

        /// <summary>
        /// Verify that the API assumes the default version is requested if
        /// none is specified
        /// </summary>
        [Fact]
        public void AssumeDefaultVersionWhenUnspecified()
        {
            var verOpt = GetService<IOptions<ApiVersioningOptions>>(_services);
            Assert.True(verOpt.Value.AssumeDefaultVersionWhenUnspecified);
        }

        /// <summary>
        /// Verify that API Versions are reported
        /// </summary>
        [Fact]
        public void ReportApiVersions()
        {
            var verOpt = GetService<IOptions<ApiVersioningOptions>>(_services);
            Assert.True(verOpt.Value.ReportApiVersions);
        }

        /// <summary>
        /// Verify the default API Version
        /// </summary>
        [Fact]
        public void DefaultApiVersion()
        {
            var verOpt = GetService<IOptions<ApiVersioningOptions>>(_services);
            Assert.Equal(new ApiVersion(1,0), verOpt.Value.DefaultApiVersion);
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
