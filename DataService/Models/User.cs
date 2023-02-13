/*
 * Subject      : PROG3050-Sec2: Microsoft Enterprise Application
 * Title        : Iteration#1 Source Code
 * Team         : #5 Chocolist
 * Team Members : Trevor White, Keum Ji Kim, Ilshin Ji, Ezatullah Rafie, Asraa Aleem-Uddin  
 * Created      : October 2022 
 */

using System;

namespace DataService.Models
{
    /// <summary>
    /// User model class
    /// </summary>
    public class User
    {
        /// <summary>
        /// Identifier of a user
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// User's email address
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// User's password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// If user's email is valid
        /// </summary>
        public bool EmailValid { get; set; }

        /// <summary>
        /// 1 is employee 2 is customer
        /// </summary>
        public int UserTypeId { get; set; }
    }

    public class Profile
    {
        /// <summary>
        /// Identifier of a user
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// User's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User's display name
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// User's email
        /// </summary>
        public string EmailAddress { get; set; }

        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public bool ReceivePromotional { get; set; }
        public int PlatformId { get; set; }
        public int GenreId { get; set; }
        public string Password { get; set; }
    }

    public class FriendUser
    {
        /// <summary>
        /// Identifier of a user
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// User's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User's last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User's display name
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// User's email
        /// </summary>
        public string EmailAddress { get; set; }
        public bool Approved { get; set; }
    }
}
