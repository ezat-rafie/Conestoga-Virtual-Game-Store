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
    public class AddressService : IAddressService
    {
        /// <summary>
        /// Create an address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public int CreateAddress(Address address)
        {

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO dbo.Address (UserId, FullName, AddressLine1, AddressLine2, AddressLine3, City, Province, Postal) OUTPUT INSERTED.AddressId VALUES (@userId, @fullName, @addressLine1, @addressLine2, @addressLine3, @city, @province, @postal)";
                    command.Parameters.AddWithValue("@userId", address.UserId);
                    command.Parameters.AddWithValue("@fullName", address.FullName);
                    command.Parameters.AddWithValue("@addressLine1", address.AddressLine1);
                    command.Parameters.AddWithValue("@addressLine2", address.AddressLine2);
                    command.Parameters.AddWithValue("@addressLine3", address.AddressLine3);
                    command.Parameters.AddWithValue("@city", address.City);
                    command.Parameters.AddWithValue("@province", address.Province);
                    command.Parameters.AddWithValue("@postal", address.PostalCode);
                    connection.Open();

                    int createdId = (int)command.ExecuteScalar();
                    return createdId;
                }
            }
        }

        public Address GetAddress(int userId, int addressId)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand sqlCmd = connection.CreateCommand())
                {
                    sqlCmd.CommandText = $"SELECT * FROM dbo.[Address] WHERE UserId={userId} and AddressId={addressId};";
                    connection.Open();

                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    Address address = new Address();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            address = new Address()
                            {
                                AddressId = Int32.Parse(reader["AddressId"].ToString()),
                                FullName = reader["FullName"]?.ToString() ?? "",
                                AddressLine1 = reader["AddressLine1"]?.ToString() ?? "",
                                AddressLine2 = reader["AddressLine2"]?.ToString() ?? "",
                                AddressLine3 = reader["AddressLine3"]?.ToString() ?? "",
                                City = reader["City"]?.ToString() ?? "",
                                Province = reader["Province"]?.ToString() ?? "",
                                PostalCode = reader["Postal"]?.ToString() ?? ""
                            };
                        }
                    }
                    return address;
                }
            }
        }

        public List<Address> GetAddresses(int userId)
        {
            List<Address> addresses = new List<Address>();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "  SELECT * from dbo.Address WHERE UserId = @userId;";
                    command.Parameters.AddWithValue("@userId", userId);
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            addresses.Add(new Address
                            {
                                UserId = Int32.Parse(reader["UserId"].ToString()),
                                AddressId = Int32.Parse(reader["AddressId"].ToString()),
                                FullName = reader["FullName"]?.ToString() ?? "",
                                AddressLine1 = reader["AddressLine1"]?.ToString() ?? "",
                                AddressLine2 = reader["AddressLine2"]?.ToString() ?? "",
                                AddressLine3 = reader["AddressLine3"]?.ToString() ?? "",
                                City = reader["City"]?.ToString() ?? "",
                                Province = reader["Province"]?.ToString() ?? "",
                                PostalCode = reader["Postal"]?.ToString() ?? ""
                            });
                        }
                    }
                }
            }

            return addresses;
        }



        public bool UpdateAddress(Address address)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"UPDATE dbo.Address SET FullName = @fullName, AddressLine1 = @addressLine1, AddressLine2 = @addressLine2, AddressLine3 = @addressLine3, City = @city, Province = @province, Postal = @postal WHERE AddressId = @addressId;";
                    cmd.Parameters.AddWithValue("@fullName", address.FullName);
                    cmd.Parameters.AddWithValue("@addressLine1", address.AddressLine1);
                    cmd.Parameters.AddWithValue("@addressLine2", address.AddressLine2);
                    cmd.Parameters.AddWithValue("@addressLine3", address.AddressLine3);
                    cmd.Parameters.AddWithValue("@city", address.City);
                    cmd.Parameters.AddWithValue("@province", address.Province);
                    cmd.Parameters.AddWithValue("@postal", address.PostalCode);
                    cmd.Parameters.AddWithValue("@addressId", address.AddressId);
                    connection.Open();
                    int id = (int)cmd.ExecuteNonQuery();
                    if (id > 0)
                        return true;
                    else
                        return false;
                }
            }
        }

        public bool RemoveAddress(int userId, int addressId)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "Delete FROM dbo.Address WHERE UserId = @UserId AND AddressId = @AddressId;";
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@AddressId", addressId);
                    connection.Open();
                    int createdId = (int)command.ExecuteNonQuery();

                    if (createdId > 0)
                        return true;
                    else
                        return false;
                }

            }
        }


    }
}
