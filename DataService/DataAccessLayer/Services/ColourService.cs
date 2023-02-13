using DataService.DataAccessLayer.IServices;
using DataService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.DataAccessLayer.Services
{
    public class ColourService : IColourService
    {
        public List<Colour> GetAllColours()
        {
            List<Colour> colourList = new List<Colour>();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM dbo.Colour;";
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            colourList.Add(new Colour
                            {
                                ColourId = Convert.ToInt32(rdr["ColourId"]),
                                Name = rdr["Name"].ToString(),
                                Value = rdr["Value"].ToString()
                            });
                        }
                    }
                }
            }

            return colourList;
        }

        public Colour GetColour(int colourId)
        {
            Colour theColour = new Colour();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Name, Value FROM dbo.Colour WHERE ColourId = @field1;";
                    command.Parameters.AddWithValue("@field1", colourId);
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        rdr.Read();
                        theColour.ColourId = colourId;
                        theColour.Name = rdr["Name"].ToString();
                        theColour.Value = rdr["Value"].ToString();
                    }
                }
            }
            return theColour;
        }
    }
}
