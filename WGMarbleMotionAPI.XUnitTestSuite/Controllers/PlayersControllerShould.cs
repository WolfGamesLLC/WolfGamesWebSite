using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WGMarbleMotionAPI.Controllers;
using WGXUnit.Facts;
using WolfGamesWebSite.Common.XUnitTest.Controllers;
using WolfGamesWebSite.DAL.Data;
using WolfGamesWebSite.DAL.Models.SimpleGameModels.MarbleMotion;
using Xunit;
using Xunit.Abstractions;

namespace WGMarbleMotionAPI.XUnitTestSuite.Controllers
{
    /// <summary>
    /// Test suite for the <see cref="PlayersController"/>
    /// </summary>
    public class PlayersControllerShould : BaseControllerShould<PlayersController>, IDisposable
    {
        private Mock<IUrlHelper> mockUrl;
        private static readonly string PLAYER_ID = "11111111-1111-1111-1111-11111111111";
        private ApplicationDbContext _context;

        /// <summary>
        /// The test initializer for the suite
        /// </summary>
        public PlayersControllerShould(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
            DbContextOptionsBuilder<ApplicationDbContext> builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase("player model");

            _context = new ApplicationDbContext(builder.Options);
            AddTestUserToContext(_context, "1", 1, 10, 100);
            AddTestUserToContext(_context, "2", 5, 50, 500);
            AddTestUserToContext(_context, "3", 0, 0, 0);
            _context.SaveChanges();

            Controller = new PlayersController(_context);
            mockUrl = new Mock<IUrlHelper>();
            mockUrl.Setup(r => r.Link("GetPlayers", null)).Returns("http://GetPlayers");
            Guid playerId;
            mockUrl.Setup(r => r.Link("GetPlayerAsync", It.IsAny<object>())).Returns("https://players/" + PLAYER_ID + 1);
            Controller.Url = mockUrl.Object;
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
        }

        private static void AddTestUserToContext(ApplicationDbContext context, string id, int score, int xpos, int ypos)
        {
            context.PlayerModel.Add(new PlayerModel()
            {
                Id = Guid.Parse(PLAYER_ID + id),
                Score = score,
                XPosition = xpos,
                ZPosition = ypos
            });
        }

        /// <summary>
        /// The <see cref="PlayersController.GetPlayers"/> action should return a OkObjectResult
        /// </summary>
        [Fact]
        public void GetPlayersReturnsOkObjectResult()
        {
            Assert.IsType<OkObjectResult>(Controller.GetPlayers(null));
        }

        /// <summary>
        /// The <see cref="PlayersController.GetPlayers"/> action should return a OkObjectResult
        /// containing a self reference and a list of players when called with no args
        /// </summary>
        [Fact]
        public void GetPlayersReturnsOkObjectResultWithPlayerLIst()
        {
            OkObjectResult response = Controller.GetPlayers(null) as OkObjectResult;
            var exp = new { href = "http://GetPlayers\nPlayers" };
            Assert.Equal(exp.ToString(), response.Value.ToString());
        }

        /// <summary>
        /// The <see cref="PlayersController.GetPlayer"/> action should throw a
        /// not found exception when a non-existing player is requested
        /// </summary>
        [Fact]
        public void GetPlayerThrowsNotFound()
        {
            Assert.Throws<ArgumentException>(() => Controller.GetPlayers(1000));
        }

        /// <summary>
        /// The <see cref="PlayersController.GetPlayerAsync"/> action should return
        /// the player data for a specified player
        /// </summary>
        [Fact]
        public async void GetPlayerAsyncReturnsPlayerData()
        {
            var res = await Controller.GetPlayerAsync(Guid.Parse(PLAYER_ID + 1), new CancellationToken());

            Assert.IsType<OkObjectResult>(res);

            PlayerModelResource player = ((OkObjectResult)res).Value as PlayerModelResource;

            var expPlayer = new PlayerModelResource()
            {
                Href = "https://players/" + PLAYER_ID + 1,
                Score = 1
            };
            Assert.Equal(expPlayer.ToString(), player.ToString());
        }
    }
}
