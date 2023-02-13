/*
 * Subject      : PROG3050-Sec2: Microsoft Enterprise Application
 * Title        : Iteration#1 Source Code
 * Team         : #5 Chocolist
 * Team Members : Trevor White, Keum Ji Kim, Ilshin Ji, Ezatullah Rafie, Asraa Aleem-Uddin  
 * Created      : October 2022 
 */

using DataService.DataAccessLayer.IServices;
using DataService.DataAccessLayer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Chocolist.Pages.User
{
    /// <summary>
    /// VerifyAccount page code-hehind partial class
    /// </summary>
    public partial class VerifyAccount : System.Web.UI.Page
    {
        #region Fields

        /// <summary>
        /// User service for data access
        /// </summary>
        IUserService userService = new UserService();

        #endregion Fields 

        #region Handlers
        #endregion Handlers

        #region Methods

        /// <summary>
        /// Initial page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void Page_Load(object sender, EventArgs e)
        {
            redirectMessage.Visible = false;
            Uri path = new Uri(HttpContext.Current.Request.Url.AbsoluteUri);
            string email = HttpUtility.ParseQueryString(path.Query).Get("email");
            string verificationToken = HttpUtility.ParseQueryString(path.Query).Get("token");

            Validations.VerifyAccount verifyAccount = new Validations.VerifyAccount();

            verifyAccount.email = email;
            verifyAccount.token = verificationToken;

            var context = new ValidationContext(verifyAccount, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();
            Boolean isValid = Validator.TryValidateObject(verifyAccount, context, validationResults, true);

            email = HttpUtility.UrlDecode(email);
            verificationToken = HttpUtility.UrlDecode(verificationToken);
            verificationToken = verificationToken.Replace(" ", "+");
            if (isValid)
            {
                DataService.Models.User user = userService.VerifyAccount(email, verificationToken);
                if (user!=null && user.UserId > 0)
                {
                    Session["LoggedIn"] = user.UserId;

                    if (user.Password == "temporary")
                    {
                        message.InnerText = "Password change is required. Redirecting to Profile page...";
                    }
                    else if (user.EmailValid)
                    {
                        message.InnerText = "The link is invalid. Please use new link.";
                        return;
                    }
                    else
                    {
                        message.InnerText = "Account verified successfully. Redirecting to Home...";
                    }
                    redirectMessage.Visible = true;
                    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirectJS",
                        "setTimeout(function() { window.location.replace('/Pages/User/Profile') }, 5000);", true);
                }
                else
                {
                    message.InnerText = "Verification failed. Please try again!";
                }
            }
            else
            {
                message.InnerText = "Verification failed. Please try again!";
            }
        }

        #endregion Methods 

    }
}