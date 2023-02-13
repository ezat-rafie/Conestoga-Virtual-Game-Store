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
using System.Web.UI;

namespace Chocolist.Pages.User
{
    /// <summary>
    /// Login page code-hehind partial class
    /// </summary>
    public partial class Login : System.Web.UI.Page
    {
        #region Fields

        /// <summary>
        /// User service for data access
        /// </summary>
        IUserService userService = new UserService();

        #endregion Fields 

        #region Handlers

        /// <summary>
        /// Get user typed login values and login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Validations.Login login = new Validations.Login();
                var email = emailLogin.Value;
                var password = passwordLogin.Value;

                login.email = email;
                login.password = password;

                var context = new ValidationContext(login, serviceProvider: null, items: null);
                var validationResults = new List<ValidationResult>();
                Boolean isValid = Validator.TryValidateObject(login, context, validationResults, true);

                int attempts = userService.GetLoginAttempts(email);
                if (isValid)
                {
                    if (!userService.IsExisting(email))
                    {
                        lblLoginError.Text = $"Can not find account.";
                        lblLoginError.Visible = true;
                        return;
                    }
                    int userId = userService.Login(email, password);
                    if (login.DatabaseLoginValid(userId) && attempts < 3) 
                    {
                        if (userService.IsVerified(userId))
                        {
                        Session["LoggedIn"] = userId;
                        userService.ResetLoginAttempts(userId);

                        var previewItem = Session["PreviewItem"];
                        if (previewItem != null)
                        {
                            int previewItemId = Convert.ToInt32(previewItem);
                            Session["PreviewItem"] = null;
                            Response.Redirect("~/pages/Products/Preview.aspx?itemId="+previewItemId);
                        }

                        var eventDetailItem = Session["EventDetailItem"];
                        if (eventDetailItem != null)
                        {
                            int eventId = Convert.ToInt32(eventDetailItem);
                            Session["EventDetailItem"] = null;
                            Response.Redirect("~/pages/Products/EventDetail?eventId=" + eventId);
                        }

                        Response.Redirect("~/");

                        }
                        else
                        {
                            lblLoginError.Text = $"Please verify your account first.";
                            lblLoginError.Visible = true;
                        }
                    }
                    else
                    {
                        userService.UpdateLoginAttempts(email);
                        if (attempts < 2)
                        {
                            lblLoginError.Text = $"Log in failed. Please try again! (Attempts: {attempts + 1}/3)";
                        }
                        else
                        {
                            lblLoginError.Text = $"You have exceeded 3 login attempts.<br/>Please reset your password.<br/>";
                            btnReset.Visible = true;
                        }
                        Session["LoggedIn"] = 0;
                        lblLoginError.Visible = true;
                    }
                } else
                {
                    String errorOutput = "<ul>";
                    foreach (var validationResult in validationResults)
                    {
                        errorOutput += "<li>" + validationResult.ErrorMessage.ToString() + "</li>";
                    }
                    errorOutput += "</ul>";
                    lblLoginError.Text = errorOutput;
                    lblLoginError.Visible = true;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Server.Transfer("~/ErrorPages/ErrorPage.aspx", true);
            }

        }

        protected void btnSendResetLink_Click(object sender, EventArgs e)
        {
            var email = emailLogin.Value;
            if (userService.UpdateTokenForResetPW(email, Register.Verify(email, true)))
            {
                lblLoginError.Text = "Reset link has been sent.";
                btnReset.Visible = false;                
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "redirectJS",
                        "setTimeout(function() { window.location.replace('/') }, 5000);", true);
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