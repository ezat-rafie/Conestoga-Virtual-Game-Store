/*
 * Subject      : PROG3050-Sec2: Microsoft Enterprise Application
 * Title        : Iteration#1 Source Code
 * Team         : #5 Chocolist
 * Team Members : Trevor White, Keum Ji Kim, Ilshin Ji, Ezatullah Rafie, Asraa Aleem-Uddin  
 * Created      : October 2022 
 */

namespace DataService.Models
{
    /// <summary>
    /// Item model class
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Identifier of an item
        /// </summary>
        public int ItemId {get;set;}

        /// <summary>
        /// Item name
        /// </summary>
        public string Name {get;set;}   

        /// <summary>
        /// Item Description
        /// </summary>
        public string Description {get;set;}

        /// <summary>
        /// Item price
        /// </summary>
        public double Price { get;set;}

        /// <summary>
        /// Item quantity
        /// </summary>
        public int QuantityInStock {get;set;}

        public string GameTag { get; set; }

    }
}
