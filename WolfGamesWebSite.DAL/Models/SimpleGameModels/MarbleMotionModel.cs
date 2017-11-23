using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WolfGamesWebSite.Models.SimpleGameModels
{
    /// <summary>
    /// Base model for the Marble Motion game
    /// </summary>
    public class MarbleMotionModel
    {
        /// <summary>
        /// Database generated record key for this table
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// The player's score
        /// </summary>
        public long Score { get; set; }

        /// <summary>
        /// The payer's character X position
        /// </summary>
        public int XPosition { get; set; }

        /// <summary>
        /// The payer's character Z position
        /// </summary>
        public int ZPosition { get; set; }
    }
}
