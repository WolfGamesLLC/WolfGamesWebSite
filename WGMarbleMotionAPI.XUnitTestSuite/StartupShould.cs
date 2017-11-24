using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

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
            var mockConfig = new Mock<IConfiguration>();
            var mockServices = new Mock<IServiceCollection>();
            var startUp = new Startup(mockConfig.Object);

            startUp.ConfigureServices(mockServices.Object);

            Action<ServiceDescriptor>[] elementInspectors = null;
            Assert.Collection<ServiceDescriptor>(mockServices.Object, elementInspectors);
        }
    }
}
