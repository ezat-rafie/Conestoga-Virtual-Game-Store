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
    /// Inferface of User Service 
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Create a user to DB
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="verificationToken"></param>
        /// <returns></returns>
        int CreateMember(string email, string password, string verificationToken);

        /// <summary>
        /// Get all users from DB
        /// </summary>
        /// <returns></returns>
        List<User> GetAllMembers();

        /// <summary>
        /// Get all employees from DB
        /// </summary>
        /// <returns></returns>
        List<User> GetAllEmployees();

        /// <summary>
        /// Get all profile from DB
        /// </summary>
        /// <returns></returns>
        List<Profile> GetAllProfiles();

        /// <summary>
        /// Check if a user is existing
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        bool IsExisting(string email);
        
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        int Login(string username, string password);

        /// <summary>
        /// Verify Account
        /// </summary>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        User VerifyAccount(string email, string token);
        
        /// <summary>
        /// Get profile info of a user from DB
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Profile GetProfile(int userId);

        /// <summary>
        /// Update profile info to DB
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        bool UpdateProfile(Profile profile);

        int GetLoginAttempts(string email);
        bool UpdateLoginAttempts(string email);
        bool ResetLoginAttempts(int userId);
        bool UpdateTokenForResetPW(string email, string newToken);
        bool UpdatePassword(int userId, string newPassword);

        bool IsVerified(int userId);
    }
}
