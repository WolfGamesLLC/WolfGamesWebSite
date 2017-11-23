using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WolfGamesWebSite.Models;
using WolfGamesWebSite.Models.SimpleGameModels;

namespace WolfGamesWebSite.DAL.Data
{
    /// <summary>
    /// The main application db context
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// The default constructor for the main application db
        /// context
        /// </summary>
        /// <param name="options">The db context options</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// The operation to perform when a model is created for the 
        /// first time
        /// </summary>
        /// <param name="builder">The ModelBuilder object to use </param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        /// <summary>
        /// The MarbleMotionModel db set
        /// </summary>
        public DbSet<WolfGamesWebSite.Models.SimpleGameModels.MarbleMotionModel> MarbleMotionModel { get; set; }
    }
}
