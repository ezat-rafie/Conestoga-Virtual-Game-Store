/*
 * Subject      : PROG3050-Sec2: Microsoft Enterprise Application
 * Title        : Iteration#1 Source Code
 * Team         : #5 Chocolist
 * Team Members : Trevor White, Keum Ji Kim, Ilshin Ji, Ezatullah Rafie, Asraa Aleem-Uddin  
 * Created      : October 2022 
 */

using DataService.DataAccessLayer.IServices;
using DataService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Net.Mail;

namespace DataService.DataAccessLayer.Services
{
    /// <summary>
    /// User service class
    /// </summary>
    public class UserService : IUserService
    {
        #region Fields
        #endregion Fields 

        #region Methods
        /// <summary>
        /// Create a user to DB
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="verificationToken"></param>
        /// <returns></returns>
        public int CreateMember(string email, string password, string verificationToken)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    int newID = 0;
                    try
                    {
                        cmd.CommandText = $"INSERT INTO dbo.[USER] (EmailAddress,Password,UserTypeId,EmailValidationToken,PlatformId, GenreId)  OUTPUT INSERTED.UserId VALUES(@field1,@field2,2,'{verificationToken}',@field4,@field5);";
                        cmd.Parameters.AddWithValue("@field1", email);
                        cmd.Parameters.AddWithValue("@field2", password);
                        cmd.Parameters.AddWithValue("@field3", verificationToken);
                        cmd.Parameters.AddWithValue("@field4", 0);
                        cmd.Parameters.AddWithValue("@field5", 0);
                        connection.Open();

                        newID = (int)cmd.ExecuteScalar();
                    }
                    catch (Exception e)
                    {
                        Debug.Print(e.Message);
                        Debug.Print("Failed in Saving into DB");
                    }

                    return newID;
                }
            }
        }

        /// <summary>
        /// Get all customers from DB
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllMembers()
        {
            List<User> memberList = new List<User>();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM dbo.[USER] WHERE [UserTypeId]=2;";
                    connection.Open();

                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            memberList.Add(new User
                            {
                                UserId = Convert.ToInt32(rdr["UserId"]),
                                EmailAddress = rdr["EmailAddress"].ToString()
                            });
                        }
                    }
                }
            }
            return memberList;
        }

        /// <summary>
        /// Get all employees from DB
        /// </summary>
        /// <returns></returns>
        public List<User> GetAllEmployees()
        {
            List<User> employeeList = new List<User>();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM dbo.[USER] WHERE [UserTypeId]=1;";
                    connection.Open();

                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            employeeList.Add(new User
                            {
                                UserId = Convert.ToInt32(rdr["UserId"]),
                                EmailAddress = rdr["EmailAddress"].ToString(),
                                UserTypeId = Convert.ToInt32(rdr["UserTypeId"])
                            });
                        }
                    }
                }
            }
            return employeeList;
        }


        /// <summary>
        /// Get all profiles from DB
        /// </summary>
        /// <returns></returns>
        public List<Profile> GetAllProfiles()
        {
            List<Profile> profileList = new List<Profile>();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM dbo.[USER];";
                    connection.Open();

                    SqlDataReader rdr = command.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            profileList.Add(new Profile
                            {
                                UserId= Convert.ToInt32(rdr["UserId"]),
                                FirstName = rdr["FirstName"].ToString(),
                                LastName = rdr["LastName"].ToString(),
                                DisplayName = rdr["DisplayName"].ToString(),
                                EmailAddress = rdr["EmailAddress"].ToString(),
                                BirthDate = String.IsNullOrWhiteSpace(rdr["BirthDate"].ToString()) ? DateTime.MinValue : DateTime.Parse(rdr["BirthDate"].ToString()),
                                Gender = String.IsNullOrWhiteSpace(rdr["GenderId"].ToString()) ? "0" : rdr["GenderId"].ToString(),
                                ReceivePromotional = Boolean.Parse(rdr["ReceivePromotional"].ToString())
                            });
                        }
                    }
                }
            }
            return profileList;
        }

        /// <summary>
        /// Get profile info of a user from DB
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Profile GetProfile(int userId)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand sqlCmd = connection.CreateCommand())
                {
                    sqlCmd.CommandText = $"SELECT FirstName, LastName, DisplayName, EmailAddress, BirthDate, GenderId, PlatformId, GenreId, ReceivePromotional, Password FROM dbo.[User] WHERE UserId={userId};";
                    connection.Open();

                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    Profile profile = new Profile();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            profile = new Profile()
                            {
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                DisplayName = reader["DisplayName"].ToString(),
                                EmailAddress = reader["EmailAddress"].ToString(),
                                PlatformId = String.IsNullOrWhiteSpace(reader["PlatformId"].ToString()) ? 0 : Convert.ToInt32(reader["PlatformId"]),
                                GenreId = String.IsNullOrWhiteSpace(reader["GenreId"].ToString()) ? 0 : Convert.ToInt32(reader["GenreId"]),
                                BirthDate = String.IsNullOrWhiteSpace(reader["BirthDate"].ToString()) ? DateTime.MinValue : DateTime.Parse(reader["BirthDate"].ToString()),
                                Gender = String.IsNullOrWhiteSpace(reader["GenderId"].ToString()) ? "15" : reader["GenderId"].ToString(),
                                ReceivePromotional = Boolean.Parse(reader["ReceivePromotional"].ToString()),
                                Password = reader["Password"].ToString()
                            };
                        }
                    }
                    return profile;
                }
            }
        }


       

        /// <summary>
        /// Check if a user is existing
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsExisting(string email)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM dbo.[User] WHERE [EmailAddress]='{email}';";
                    connection.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();
                    return rdr.HasRows;
                }
            }
        }


        /// <summary>
        /// Check if a user is existing
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsVerified(int userId)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM dbo.[User] WHERE [UserId]=@userId;";
                    cmd.Parameters.AddWithValue("@userId", userId);
                    connection.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    bool verified = false;
                    if (reader.HasRows)
                    {
                        reader.Read();
                        verified = (bool)reader["EmailValid"];
                    }
                    return verified;
                }
            }
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int Login(string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand sqlCmd = connection.CreateCommand())
                {
                    sqlCmd.CommandText = $"SELECT * FROM dbo.[User] WHERE EmailAddress='{email}' AND Password='{password}';";
                    connection.Open();

                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    int userId = 0;
                    if (reader.HasRows)
                    {
                        reader.Read();
                        userId = Convert.ToInt32(reader["UserId"].ToString());
                        Debug.WriteLine("SSUUCCEESS - " + userId);
                    }
                    return userId;
                }
            }
        }

        /// <summary>
        /// Update profile info to DB
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        public bool UpdateProfile(Profile profile)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"UPDATE dbo.[USER] SET FirstName = @firstName, LastName = @lastName, DisplayName = @displayName, BirthDate = @birthDate, GenderId = @gender, ReceivePromotional = @receivePromo,PlatformId=@platformId, GenreId=@genreId WHERE UserId = @userId;";

                    cmd.Parameters.AddWithValue("@firstName", profile.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", profile.LastName);
                    cmd.Parameters.AddWithValue("@displayName", profile.DisplayName);
                    cmd.Parameters.AddWithValue("@birthDate", profile.BirthDate==DateTime.MinValue? DateTime.Now : profile.BirthDate);
                    cmd.Parameters.AddWithValue("@gender", profile.Gender);
                    cmd.Parameters.AddWithValue("@receivePromo", profile.ReceivePromotional);
                    cmd.Parameters.AddWithValue("@userId", profile.UserId);
                    cmd.Parameters.AddWithValue("@platformId", profile.PlatformId);
                    cmd.Parameters.AddWithValue("@genreId", profile.GenreId);
                    connection.Open();
                    int id = (int)cmd.ExecuteNonQuery();
                    if (id > 0)
                        return true;
                    else
                        return false;
                }
            }
        }

        /// <summary>
        /// Verify Account
        /// </summary>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public User VerifyAccount(string email, string token)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand sqlCmd = connection.CreateCommand())
                {
                    sqlCmd.CommandText = $"SELECT * FROM dbo.[User] WHERE EmailAddress='{email}' AND EmailValidationToken='{token}';";
                    connection.Open();

                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows && ValidateEmail(email, token))
                    {
                        reader.Read();
                        return new User()
                        {
                            UserId = Convert.ToInt32(reader["UserId"].ToString()),
                            Password = reader["Password"].ToString(),
                            EmailValid = Boolean.Parse(reader["EmailValid"].ToString())
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Send an email and validate the address
        /// </summary>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private bool ValidateEmail(string email, string token)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand sqlCmd = connection.CreateCommand())
                {
                    sqlCmd.CommandText = $"UPDATE dbo.[USER] SET EmailValid = 1 WHERE EmailAddress = @field1 AND EmailValidationToken = @field2;";
                    sqlCmd.Parameters.AddWithValue("@field1", email);
                    sqlCmd.Parameters.AddWithValue("@field2", token);
                    connection.Open();
                    int id = (int)sqlCmd.ExecuteNonQuery();
                    if (id > 0)
                        return true;
                    else
                        return false;
                }
            }
        }

        public int GetLoginAttempts(string email)
        {
            int attempts = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand sqlCmd = connection.CreateCommand())
                {
                    sqlCmd.CommandText = $"SELECT UserId, UserTypeId, EmailAddress, LoginAttempts FROM dbo.[USER] WHERE EmailAddress='{email}';";
                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            attempts = Convert.ToInt32(reader["LoginAttempts"]);
                            // What if the user is Employee ???
                        }
                    }
                }
            }
            return attempts;
        }

        public bool UpdateLoginAttempts(string email)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand sqlCmd = connection.CreateCommand())
                {
                    sqlCmd.CommandText = $"UPDATE dbo.[USER] SET LoginAttempts=LoginAttempts+1 WHERE EmailAddress='{email}';";
                    return sqlCmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool ResetLoginAttempts(int userId)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand sqlCmd = connection.CreateCommand())
                {
                    sqlCmd.CommandText = $"UPDATE dbo.[USER] SET LoginAttempts=0 WHERE UserId={userId};";
                    connection.Open();
                    return sqlCmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateTokenForResetPW(string email, string newToken)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand cmd = connection.CreateCommand())
                {
                    try
                    {
                        connection.Close();
                        var tempPassword = "TEMPORARY";
                        cmd.CommandText = $"UPDATE dbo.[USER] SET Password='temporary',EmailValidationToken='{newToken}' WHERE EmailAddress='{email}';";
                        connection.Open();

                        return (int)cmd.ExecuteNonQuery() > 0;
                    }
                    catch (Exception e)
                    {
                        Debug.Print(e.Message);
                        Debug.Print("Failed in Saving into DB");
                        return false;
                    }
                }
            }
        }

        public bool UpdatePassword(int userId, string newPassword)
        {
            int updatedCount = 0;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["dbCon"].ConnectionString))
            {
                using (SqlCommand sqlCmd = connection.CreateCommand())
                {
                    sqlCmd.CommandText = $"UPDATE dbo.[USER] SET Password='{newPassword}' WHERE UserId={userId};";
                    connection.Open();
                    updatedCount = sqlCmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return updatedCount > 0;
        }
        #endregion Methods 

    }
}
