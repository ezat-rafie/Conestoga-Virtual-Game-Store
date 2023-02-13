/*
 * Subject      : PROG3050-Sec2: Microsoft Enterprise Application
 * Title        : Iteration#1 Source Code
 * Team         : #5 Chocolist
 * Team Members : Trevor White, Keum Ji Kim, Ilshin Ji, Ezatullah Rafie, Asraa Aleem-Uddin  
 * Created      : October 2022 
 */

using DataService.Models;
using System.Collections.Generic;

namespace DataService.DataAccessLayer.IServices
{
    /// <summary>
    /// Inferface of Merchandise Service 
    /// </summary>
    public interface IMerchandiseService : IItemService
    {
        string CreateMerchandise(int itemId, int colourId, string size);

        bool UpdateMerchandise(int itemId, int colourId, string size);

        Merchandise GetMerchandise(int itemId);

        List<Merchandise> GetAllMerchandise();

        bool DeleteMerchandise(int itemId);

        List<Merchandise> SearchMerchandise(string search);

    }
}
