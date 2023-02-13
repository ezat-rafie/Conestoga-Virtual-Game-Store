using DataService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.DataAccessLayer.IServices
{
    public interface IColourService
    {
        List<Colour> GetAllColours();

        Colour GetColour(int colourId);
    }
}
