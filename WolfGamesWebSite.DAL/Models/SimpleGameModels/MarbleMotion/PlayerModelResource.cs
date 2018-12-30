using System;
using System.Collections.Generic;
using System.Text;

namespace WolfGamesWebSite.DAL.Models.SimpleGameModels.MarbleMotion
{
    /// <summary>
    /// Player data model for the Marble Motion game
    /// </summary>
    public class PlayerModelResource : ApiResource, IEquatable<PlayerModelResource>
    {
        /// <summary>
        /// The player's most recent recorded score
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

        public override bool Equals(object obj)
        {
            return Equals(obj as PlayerModelResource);
        }

        public bool Equals(PlayerModelResource other)
        {
            return other != null &&
                   Href == other.Href &&
                   Score == other.Score &&
                   XPosition == other.XPosition &&
                   ZPosition == other.ZPosition;
        }

        public override int GetHashCode()
        {
            var hashCode = 1574511355;
            hashCode = hashCode * -1521134295 + Score.GetHashCode();
            hashCode = hashCode * -1521134295 + XPosition.GetHashCode();
            hashCode = hashCode * -1521134295 + ZPosition.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(PlayerModelResource resource1, PlayerModelResource resource2)
        {
            return EqualityComparer<PlayerModelResource>.Default.Equals(resource1, resource2);
        }

        public static bool operator !=(PlayerModelResource resource1, PlayerModelResource resource2)
        {
            return !(resource1 == resource2);
        }
    }
}
