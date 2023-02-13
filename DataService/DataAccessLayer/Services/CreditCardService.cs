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
    public class CreditCardService : ICreditCardService
    {
        public bool AddCreditCard(int userId, string DisplayName, long CardNumber, DateTime Expiry, int CVV)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO dbo.CreditCard (DisplayName, CardName, BillingAddressId, CardNumber, Expiry, CVV) OUTPUT INSERTED.CreditCardId VALUES (@DisplayName, '',0, @CardNumber, @Expiry, @CVV);";

                    command.Parameters.AddWithValue("@DisplayName", DisplayName);
                    command.Parameters.AddWithValue("@CardNumber", CardNumber);
                    command.Parameters.AddWithValue("@Expiry", Expiry);
                    command.Parameters.AddWithValue("@CVV", CVV);
                    connection.Open();

                    int createdId = (int)command.ExecuteScalar();

                    command.CommandText = "INSERT INTO dbo.UserCreditCard (UserId, CreditCardId) OUTPUT INSERTED.CreditCardId VALUES (@UserId, @CreditCardId);";
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@CreditCardId", createdId);

                    int createdCardId = (int)command.ExecuteScalar();
                    if (createdCardId > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
        }

        public CreditCard GetCreditCard(int userId, int cardId)
        {
            CreditCard creditCard = new CreditCard();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "  SELECT c.CreditCardId, c.DisplayName, c.CardNumber, c.Expiry, c.CVV FROM dbo.CreditCard c  JOIN dbo.UserCreditCard u ON c.CreditCardId = u.CreditCardId  WHERE u.UserId = @userId AND c.CreditCardId = @CreditCardId;";
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@CreditCardId", cardId);
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            creditCard = new CreditCard()
                            {
                                CreditCardId = Convert.ToInt32(rdr["CreditCardId"]),
                                DisplayName = rdr["DisplayName"].ToString(),
                                CardNumber = long.Parse(rdr["CardNumber"].ToString()),
                                Expiry = Convert.ToDateTime(rdr["Expiry"]),
                                CVV = Convert.ToInt32(rdr["CVV"])
                            };
                        }
                    }
                }
            }
            return creditCard;
        }

        public List<CreditCard> GetCreditCards(int userId)
        {
            List<CreditCard> creditCards = new List<CreditCard>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "  SELECT c.CreditCardId, c.DisplayName, c.CardNumber, c.Expiry, c.CVV FROM dbo.CreditCard c  JOIN dbo.UserCreditCard u ON c.CreditCardId = u.CreditCardId  WHERE u.UserId = @userId;";
                    command.Parameters.AddWithValue("@userId", userId);
                    connection.Open();
                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            creditCards.Add(new CreditCard
                            {
                                CreditCardId = Convert.ToInt32(rdr["CreditCardId"]),
                                DisplayName = rdr["DisplayName"].ToString(),
                                CardNumber = long.Parse(rdr["CardNumber"].ToString()),
                                Expiry = Convert.ToDateTime(rdr["Expiry"]),
                                CVV = Convert.ToInt32(rdr["CVV"])
                            });
                        }
                    }
                }
            }

            return creditCards;
        }

        public bool RemoveCreditCard(int userId, int cardId)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Delete FROM dbo.UserCreditCard WHERE UserId = @UserId AND CreditCardId = @CreditId;";
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@CreditId", cardId);
                    connection.Open();
                    command.ExecuteNonQuery();

                    command.CommandText = "Delete FROM dbo.CreditCard WHERE CreditCardId = @CreditCardId;";
                    command.Parameters.AddWithValue("@CreditCardId", cardId);
                    int createdId = (int)command.ExecuteNonQuery();

                    if (createdId > 0)
                        return true;
                    else
                        return false;
                }

            }
        }

        public bool UpdateCreditCard(int userId, int CreditCardId, string DisplayName, long CardNumber, DateTime Expiry, int CVV)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE dbo.CreditCard SET DisplayName = @DisplayName, CardName = '', BillingAddressId = 0, CardNumber = @CardNumber, Expiry = @Expiry, CVV = @CVV OUTPUT INSERTED.CreditCardId WHERE CreditCardId = @CreditCardId;";

                    command.Parameters.AddWithValue("@DisplayName", DisplayName);
                    command.Parameters.AddWithValue("@CardNumber", CardNumber);
                    command.Parameters.AddWithValue("@Expiry", Expiry);
                    command.Parameters.AddWithValue("@CVV", CVV);
                    command.Parameters.AddWithValue("@CreditCardId", CreditCardId);
                    connection.Open();
                    int cardId = (int)command.ExecuteScalar();
                    if (cardId > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

            }
        }
    }
}
