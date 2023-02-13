/*
 * Subject      : PROG3050-Sec2: Microsoft Enterprise Application
 * Title        : Iteration#1 Source Code
 * Team         : #5 Chocolist
 * Team Members : Trevor White, Keum Ji Kim, Ilshin Ji, Ezatullah Rafie, Asraa Aleem-Uddin  
 * Created      : October 2022 
 */

using System.Collections.Generic;

namespace DataService.Models
{
    /// <summary>
    /// Game model class
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Identifier of a game
        /// </summary>
        public int ItemId { get; set; } 

        /// <summary>
        /// Platform id of a game
        /// </summary>
        public int PlatformId { get; set; }
        public int GenreId { get; set; }

        /// <summary>
        /// Platform name of a game
        /// </summary>
        public string PlatformName { get; set; }
        public string GenreName { get; set; }

        /// <summary>
        /// Review rate of a game
        /// </summary>
        public string ESRBRating { get; set; }

        /// <summary>
        /// Item class info of a game
        /// </summary>
        public Item Item { get; set; }

        public decimal Rating { get; set; }
    }
}
