using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private static readonly string GET_PLAYER_LIST_URL = "http://GetPlayers";
        private static readonly string GET_PLAYER_URL = "https://players/";
        private static readonly string CREATE_PLAYER_URL = "https://players/";
        private static readonly string UPDATE_PLAYER_URL = "https://players/";
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
            mockUrl.Setup(r => r.Link("GetPlayers", null)).Returns(GET_PLAYER_LIST_URL);
            mockUrl.Setup(r => r.Link("GetPlayerAsync", It.IsAny<object>())).Returns(GET_PLAYER_URL + PLAYER_ID + 1);
            mockUrl.Setup(r => r.Link("CreatePlayerAsync", It.IsAny<object>())).Returns(CREATE_PLAYER_URL + PLAYER_ID + 1);
            mockUrl.Setup(r => r.Link("UpdatePlayerAsync", It.IsAny<object>())).Returns(UPDATE_PLAYER_URL + PLAYER_ID + 1);
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
        /// The <see cref="PlayersController.GetPlayersAsync"/> action should return a OkObjectResult
        /// </summary>
        [Fact]
        public void GetPlayersReturnsOkObjectResult()
        {
            Assert.IsType<OkObjectResult>(Controller.GetPlayersAsync());
        }

        /// <summary>
        /// The <see cref="PlayersController.GetPlayersAsync"/> action should return a OkObjectResult
        /// containing a self reference and a list of players when called with no args
        /// </summary>
        [Fact]
        public void GetPlayersAsyncReturnsOkObjectResultWithPlayerLIst()
        {
            var res = Controller.GetPlayersAsync() as OkObjectResult;
            Assert.IsType<Collection<PlayerModelResource>>(res.Value);
            Assert.Equal(3, ((Collection<PlayerModelResource>)res.Value).Count);
        }

        /// <summary>
        /// The <see cref="PlayersController.GetPlayerAsync"/> action should return
        /// the player data for a specified player
        /// </summary>
        [Fact]
        public async void GetPlayerAsyncReturnsPlayerData()
        {
            // arrange
            var expPlayer = new PlayerModelResource()
            {
                Href = GET_PLAYER_URL + PLAYER_ID + 1,
                Score = 1,
                XPosition = 10,
                ZPosition = 100
            };

            // apply
            var res = await Controller.GetPlayerAsync(Guid.Parse(PLAYER_ID + 1), new CancellationToken());

            // assert
            Assert.IsType<OkObjectResult>(res);

            PlayerModelResource player = ((OkObjectResult)res).Value as PlayerModelResource;

            Assert.Equal(expPlayer.Href, player.Href);
            Assert.Equal(expPlayer.Score, player.Score);
            Assert.Equal(expPlayer.XPosition, player.XPosition);
            Assert.Equal(expPlayer.ZPosition, player.ZPosition);
        }


        /// <summary>
        /// Create a player's data in the database
        /// </summary>
        [Fact]
        public async Task CreatePlayerDataInContextAsync()
        {
            var expectedPlayer = new PlayerModel()
            {
                Score = 6,
                XPosition = 60,
                ZPosition = 600
            };
            var putResult = await Controller.CreatePlayerAsync(new PlayerModelResource()
            {
                Score = expectedPlayer.Score,
                XPosition = expectedPlayer.XPosition,
                ZPosition = expectedPlayer.ZPosition
            }, new CancellationToken());

            Assert.IsType<CreatedResult>(putResult);
            expectedPlayer.Id = (Guid)(((CreatedResult)putResult).Value);

            var res = await _context.PlayerModel.SingleOrDefaultAsync<PlayerModel>(r => r.Id == expectedPlayer.Id);
            Assert.Equal(expectedPlayer.Id, res.Id);
            Assert.Equal(expectedPlayer.Score, res.Score);
            Assert.Equal(expectedPlayer.XPosition, res.XPosition);
            Assert.Equal(expectedPlayer.ZPosition, res.ZPosition);
        }
        
        /// <summary>
        /// Update a player's data in the database
        /// </summary>
        [Fact]
        public async Task UpdatePlayerDataInContextAsync()
        {
            Guid id = new Guid(PLAYER_ID + 2);
            var expectedPlayer = new PlayerModel()
            {
                Id = id,
                Score = 6,
                XPosition = 60,
                ZPosition = 600
            };
            var putResult = await Controller.UpdatePlayerAsync(id, new PlayerModelResource()
            {
                Score = expectedPlayer.Score,
                XPosition = expectedPlayer.XPosition,
                ZPosition = expectedPlayer.ZPosition
            }, new CancellationToken());

            Assert.IsType<CreatedResult>(putResult);

            var res = await _context.PlayerModel.SingleOrDefaultAsync<PlayerModel>(r => r.Id == id);
            Assert.Equal(expectedPlayer.Id, res.Id);
            Assert.Equal(expectedPlayer.Score, res.Score);
            Assert.Equal(expectedPlayer.XPosition, res.XPosition);
            Assert.Equal(expectedPlayer.ZPosition, res.ZPosition);
        }
    }
}
