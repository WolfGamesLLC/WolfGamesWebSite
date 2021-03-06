﻿using System;
using System.Collections.Generic;
using System.Text;
using WolfGamesWebSite.DAL.Models;
using Xunit;

namespace WolfGamesWebSite.XUnitTestSuite.Models
{
    /// <summary>
    /// Test suite for the standard application user model
    /// </summary>
    public class ApplicationUserModelShould
    {
        /// <summary>
        /// Verify the model can be created
        /// </summary>
        [Fact]
        public void CreateAnApplicationUser()
        {
            Assert.NotNull(new ApplicationUser());
        }
    }
}
