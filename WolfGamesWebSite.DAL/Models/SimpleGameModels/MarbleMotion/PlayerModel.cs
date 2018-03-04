using System;
using System.Collections.Generic;
using System.Text;

namespace WolfGamesWebSite.DAL.Models.SimpleGameModels.MarbleMotion
{
    /// <summary>
    /// The player database table model
    /// </summary>
    public class PlayerModel
    {
        /// <summary>
        /// Database generated record key for this table
        /// </summary>
        public Guid Id { get; set; }

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
