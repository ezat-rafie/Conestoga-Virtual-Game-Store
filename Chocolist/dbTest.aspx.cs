/*
 * Subject      : PROG3050-Sec2: Microsoft Enterprise Application
 * Title        : Iteration#1 Source Code
 * Team         : #5 Chocolist
 * Team Members : Trevor White, Keum Ji Kim, Ilshin Ji, Ezatullah Rafie, Asraa Aleem-Uddin  
 * Created      : October 2022 
 */

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Chocolist
{
    /// <summary>
    /// dbTest page code-hehind partial class
    /// </summary>
    public partial class dbTest : System.Web.UI.Page
    {
        #region Fields
        #endregion Fields 

        #region Handlers

        /// <summary>
        /// Test DB connection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TestDB(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                SqlCommand command = new SqlCommand(
                  "Select FirstName from SalesLT.Customer where CustomerID = 1;",
                  connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Literal1.Text = reader.GetString(0);
                    }
                }
                else
                {
                    Literal1.Text = "No rows found.";
                }
                reader.Close();
            }
        }

        #endregion Handlers

        #region Methods

        /// <summary>
        /// Initial page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #endregion Methods 


       
    }
}