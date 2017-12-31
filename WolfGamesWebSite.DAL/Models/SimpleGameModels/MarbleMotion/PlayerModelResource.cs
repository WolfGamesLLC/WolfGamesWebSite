using System;
using System.Collections.Generic;
using System.Text;

namespace WolfGamesWebSite.DAL.Models.SimpleGameModels.MarbleMotion
{
    /// <summary>
    /// Player data model for the Marble Motion game
    /// </summary>
    public class PlayerModelResource : ApiResource
    {
        /// <summary>
        /// The player's most recent recorded score
        /// </summary>
        public long Score { get; set; }
    }
}
