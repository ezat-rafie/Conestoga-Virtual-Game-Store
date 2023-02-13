/*
 * Subject      : PROG3050-Sec2: Microsoft Enterprise Application
 * Title        : Iteration#1 Source Code
 * Team         : #5 Chocolist
 * Team Members : Trevor White, Keum Ji Kim, Ilshin Ji, Ezatullah Rafie, Asraa Aleem-Uddin  
 * Created      : October 2022 
 */

using System.ComponentModel.DataAnnotations;

namespace Validations
{
    /// <summary>
    /// VerifyAccount class 
    /// </summary>
    public class VerifyAccount
    {
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
            ErrorMessage = "Email is invalid")]
        public string email
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Token is required")]
        public string token
        {
            get;
            set;
        }
    }
}
