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
    /// Item CRUD validation class
    /// </summary>
    public class ItemCRUD
    {
        [Required(ErrorMessage = "Title is required")]
        public string title
        {
            get;
            set;
        }

        [Required(ErrorMessage = "Description is required")]
        public string description
        {
            get;
            set;
        }

        [RegularExpression(@"^\d*\.?\d+$", ErrorMessage = "Price can only be numbers and 2 after a decimal")]
        [Required(ErrorMessage = "Price is required")]
        public string price
        {
            get;
            set;
        }

        [RegularExpression(@"^\d+$", ErrorMessage = "Quantity must be a number >= 0")]
        [Required(ErrorMessage = "Quantity is required")]
        public string quantity
        {
            get;
            set;
        }
    }
}
