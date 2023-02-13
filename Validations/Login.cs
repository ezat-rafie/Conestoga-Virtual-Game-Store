/*
 * Subject      : PROG3050-Sec2: Microsoft Enterprise Application
 * Title        : Iteration#1 Source Code
 * Team         : #5 Chocolist
 * Team Members : Trevor White, Keum Ji Kim, Ilshin Ji, Ezatullah Rafie, Asraa Aleem-Uddin  
 * Created      : October 2022 
 */
using System;
using System.ComponentModel.DataAnnotations;

namespace Validations
{
    /// <summary>
    /// Login class
    /// </summary>
    public class Login
    {
        [Required(ErrorMessage = "Username is required")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
            ErrorMessage = "Please enter proper email as username")]
        public string email
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Password is required")]
        public string password
        {
            get;
            set;
        }

        /// <summary>
        /// Return validation result
        /// </summary>
        /// <param name="returnedUserId"></param>
        /// <returns></returns>
        public Boolean DatabaseLoginValid(int returnedUserId)
        {
            Boolean result = false;

            if (returnedUserId > 0)
            {
                result = true;
            }

            return (result);
        }
    }
}
