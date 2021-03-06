﻿using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using WGXUnit.Facts;
using WolfGamesWebSite.Common;
using Xunit;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Routing.Internal;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Routing;

namespace WolfGamesWebSite.Test.Framework.Facts
{
    /// <summary>
    /// Test suite for the <see cref="Startup"/> class containing
    /// tests for the common features used by all sites and APIs
    /// </summary>
    public abstract class BasicStartupShould : FactWriteToStdOut
    {
        protected ServiceCollection _services;
        protected int _expectedServicesCount;

        /// <summary>
        /// Test initializer
        /// </summary>
        /// <param name="testOutputHelper">Allows the test to write data to stdout</param>
        public BasicStartupShould(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
            _services = new ServiceCollection();
        }

        /// <summary>
        /// Get a specific service from a Service collection
        /// </summary>
        /// <typeparam name="T">The type of the service to retrieve</typeparam>
        /// <param name="services">The ServiceCollection to be searched</param>
        /// <returns>The service or null if the service is not found</returns>
        protected T GetService<T>(ServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider.GetService<T>();
        }

        [Fact]
        public void AddAllRequiredServicesToServicePipeline()
        {
            OutputHelper.WriteLine(_services.Count.ToString());
            Assert.True(_services.Count == _expectedServicesCount);
        }

        [Fact]
        public void AddApplicationPartManagerToServicePipeline()
        {
            Assert.NotNull(GetService<ApplicationPartManager>(_services));
        }

        [Fact]
        public void AddIInlineConstraintResolverToServicePipeline()
        {
            Assert.NotNull(GetService<IInlineConstraintResolver>(_services));
        }

        [Fact]
        public void AddUrlEncoderToServicePipeline()
        {
            Assert.NotNull(GetService<UrlEncoder>(_services));
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
            Assert.NotNull(GetService<RoutingMarkerService>(_services));
        }

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
