/*
 * Subject      : PROG3050-Sec2: Microsoft Enterprise Application
 * Title        : Iteration#1 Source Code
 * Team         : #5 Chocolist
 * Team Members : Trevor White, Keum Ji Kim, Ilshin Ji, Ezatullah Rafie, Asraa Aleem-Uddin  
 * Created      : October 2022 
 */

namespace Chocolist.Enums
{
    /// <summary>
    /// Enum class to be used in the app
    /// </summary>
    public static class Enum
    {
        /// <summary>
        /// Platform enum for game
        /// </summary>
        public enum PlatformEnum
        {
            Macintosh = 1,
            Windows = 2,
            Nintendo = 3,
            Playstation = 4,
            XBox = 5
        }


        public enum GenreEnum
        {
            Action = 1,
            Adventure = 2,
            Strategy = 3,
            Family = 4,
            Puzzle = 5,
            Sports = 6
        }

        public enum StatusEnum
        {
            Received = 3,
            PaymentProcessed = 4,
            PaymentError = 5,
            Processed = 6,
            ReviewApprovalPending = 7,
            ReviewApproved = 8,
            ReviewRejected = 15,
        }
    }
}