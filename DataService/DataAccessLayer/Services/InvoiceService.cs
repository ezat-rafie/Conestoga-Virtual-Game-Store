using DataService.DataAccessLayer.IServices;
using DataService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.DataAccessLayer.Services
{
    public class InvoiceService : IInvoiceService
    {

        public bool CreateInvoice(int userId, int creditCardId, int addressId, int billingAddressId, List<InvoiceItem> cartItems)
        {
            bool result = false;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    int newID = 0;
                    try
                    {
                        Debug.WriteLine(userId + " : " + creditCardId + " : " + addressId + " : " + billingAddressId);
                        Debug.WriteLine(cartItems);
                        cmd.CommandText = $"INSERT INTO dbo.[Invoice] (UserId, StatusId, CreditCardId, AddressId, BillingAddressId, Date, Paid)  OUTPUT INSERTED.InvoiceId VALUES(@field1, 4, @field2, @field3, @field4, @field5, 1);";
                        cmd.Parameters.AddWithValue("@field1", userId);
                        cmd.Parameters.AddWithValue("@field2", creditCardId);
                        cmd.Parameters.AddWithValue("@field3", addressId);
                        cmd.Parameters.AddWithValue("@field4", billingAddressId);
                        cmd.Parameters.AddWithValue("@field5", DateTime.Now);
                        connection.Open();

                        newID = (int)cmd.ExecuteScalar();

                        result = true;
                    }
                    catch (Exception e)
                    {
                        Debug.Print(e.Message);
                        Debug.Print("Failed in Saving into DB");
                    }
                    if (result)
                    {
                        result = false;
                        try
                        {
                            int lineCount = 0;
                            foreach (InvoiceItem cartItem in cartItems)
                            {
                                lineCount++;
                                cmd.CommandText = $"INSERT INTO dbo.[InvoiceItem] (InvoiceId, ItemId, InvoiceLine, InvoicePrice, Quantity) VALUES(@field11, @field12, @field13, @field14, @field15);";
                                cmd.Parameters.AddWithValue("@field11", newID);
                                cmd.Parameters.AddWithValue("@field12", cartItem.itemId);
                                cmd.Parameters.AddWithValue("@field13", lineCount);
                                cmd.Parameters.AddWithValue("@field14", cartItem.invoicePrice);
                                cmd.Parameters.AddWithValue("@field15", cartItem.quantity);
                                cmd.ExecuteNonQuery();
                            }
                            result = true;
                        }
                        catch (Exception e)
                        {
                            Debug.Print(e.Message);
                            Debug.Print("Failed in Saving items into DB");
                        }
                    }
                }
            }
            return (result);
        }

        public List<InvoiceItem> GetInvoiceItems(int invoiceId)
        {
            List<InvoiceItem> output = new List<InvoiceItem>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT * FROM INVOICEITEM WHERE InvoiceId = {invoiceId}";
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            output.Add(new InvoiceItem
                            {
                                invoiceId = Convert.ToInt32(rdr["InvoiceId"]),
                                itemId = Convert.ToInt32(rdr["ItemId"])
                            });
                        }
                    }
                }
            }
            return (output);
        }

        public string GetGameNameByInvoiceId(int invoiceId)
        {
            string gameName = string.Empty;

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT TOP 1 IE.Name FROM [Invoice] I 
                                            INNER JOIN[InvoiceItem] II on II.InvoiceId = I.InvoiceId
                                            INNER JOIN[Item] IE on IE.ItemId = II.ItemID
                                            where I.InvoiceId = " + invoiceId.ToString();
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {


                            gameName = Convert.ToString(rdr["Name"]);

                        }
                    }
                }
            }
            return gameName;
        }

        public List<DisplayInvoice> GetDisplayInvoices()
        {
            List<DisplayInvoice> output = new List<DisplayInvoice>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT i.InvoiceId, u.EmailAddress, i.StatusId, i.Date, (SELECT STUFF((SELECT ' <li>' + SPACE(1) + 'ID:' + CAST(invi.ItemId AS VARCHAR(10)) + ' - ' + [Name], ' x' + SPACE(1) + CAST([Quantity] AS VARCHAR(10)) + '</li>' FROM dbo.[InvoiceItem] invi LEFT JOIN dbo.[Item] item ON invi.ItemId = item.ItemId WHERE invi.InvoiceId = i.InvoiceId FOR XML PATH(''), TYPE).value('.', 'VARCHAR(MAX)'), 1, 1, '') AS [ItemList]) AS ItemList, (SELECT SUM(ii.Quantity * ii.InvoicePrice * 1.13) FROM dbo.[InvoiceItem] ii WHERE ii.InvoiceId = i.InvoiceId) AS Total FROM dbo.[Invoice] i LEFT JOIN dbo.[User] u on i.UserId = u.UserId WHERE i.StatusId != 6;";
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            output.Add(new DisplayInvoice
                            {
                                invoiceId = Convert.ToInt32(rdr["InvoiceId"]),
                                statusId = Convert.ToInt32(rdr["StatusId"]),
                                date = Convert.ToDateTime(rdr["Date"]),
                                total = Convert.ToDouble(rdr["Total"]),
                                userEmail = rdr["EmailAddress"].ToString(),
                                items = rdr["ItemList"].ToString()
                            });
                        }
                    }
                }
            }
            return (output);
        }

        public List<DisplayInvoice> GetDisplayInvoicesByUserId(int UserId)
        {
            List<DisplayInvoice> output = new List<DisplayInvoice>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT i.InvoiceId, u.EmailAddress, 
                                            i.StatusId, i.Date, 
                                            (SELECT STUFF((SELECT ' <li>' + SPACE(1) + 'ID:' + CAST(invi.ItemId AS VARCHAR(10)) + ' - ' + [Name], ' x' + SPACE(1) + CAST([Quantity] AS VARCHAR(10)) + '</li>'
                                            FROM dbo.[InvoiceItem]
                                                                invi
                                            LEFT JOIN dbo.[Item] item ON invi.ItemId = item.ItemId
                                            INNER JOIN dbo.[Game] G on G.ItemId = item.ItemId
                                            WHERE G.PlatformId in (1, 2) AND invi.InvoiceId = i.InvoiceId FOR XML PATH(''), TYPE).value('.', 'VARCHAR(MAX)'), 1, 1, '') AS[ItemList]) AS ItemList, (SELECT SUM(ii.Quantity * ii.InvoicePrice * 1.13)
                                            FROM dbo.[InvoiceItem] ii
                                            WHERE ii.InvoiceId = i.InvoiceId) AS Total FROM dbo.[Invoice] i LEFT JOIN dbo.[User] u on i.UserId = u.UserId WHERE i.StatusId != 6 AND u.UserId = @field2; ";
                    command.Parameters.AddWithValue("@field2", UserId);
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            output.Add(new DisplayInvoice
                            {
                                invoiceId = Convert.ToInt32(rdr["InvoiceId"]),
                                statusId = Convert.ToInt32(rdr["StatusId"]),
                                date = Convert.ToDateTime(rdr["Date"]),
                                total = Convert.ToDouble(rdr["Total"]),
                                userEmail = rdr["EmailAddress"].ToString(),
                                items = rdr["ItemList"].ToString()
                            });
                        }
                    }
                }
            }
            return (output);
        }

        public bool UpdateInvoiceStatus(int invoiceId, int statusId)
        {
            bool result = false;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    try
                    {
                        cmd.CommandText = $"UPDATE dbo.[Invoice] SET StatusId = @field1 WHERE InvoiceId = @field2;";
                        cmd.Parameters.AddWithValue("@field1", statusId);
                        cmd.Parameters.AddWithValue("@field2", invoiceId);
                        connection.Open();

                        cmd.ExecuteNonQuery();
                        result = true;
                    }
                    catch (Exception e)
                    {
                        Debug.Print(e.Message);
                        Debug.Print("Failed in Saving into DB");
                    }

                }
            }
            return (result);
        }
    }
}
