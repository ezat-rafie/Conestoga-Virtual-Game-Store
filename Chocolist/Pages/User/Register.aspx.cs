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
using System.Net.Mail;
using System.Web;
using System.Web.UI;

namespace Chocolist.Pages.User
{
    /// <summary>
    /// Register page code-hehind partial class
    /// </summary>
    public partial class Register : System.Web.UI.Page
    {
        IUserService userService = new UserService();

        /// <summary>
        /// Initial page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                lblErrorMessage.ForeColor = System.Drawing.Color.Green;
                lblErrorMessage.Text = "";
            }
        }

        /// <summary>
        /// Validate chaptha
        /// </summary>
        /// <returns></returns>
        private bool IsCaptchaValid(String captchaText)
        {
            cptCaptcha.ValidateCaptcha(captchaText);
            return (cptCaptcha.UserValidated);
        }

        /// <summary>
        /// Validate captcha and create user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                Validations.Register register = new Validations.Register();
                var email = emailRegister.Value;
                var password = passwordRegister.Value;
                var captcha = txtCaptcha.Text.Trim();

                register.email = email;
                register.password = password;
                register.captcha = captcha;

                var context = new ValidationContext(register, serviceProvider: null, items: null);
                var validationResults = new List<ValidationResult>();
                Boolean isValid = Validator.TryValidateObject(register, context, validationResults, true);

                lblErrorMessage.ForeColor = System.Drawing.Color.Green;
                lblErrorMessage.Text = "";

                if (isValid)
                {
                    if (!IsCaptchaValid(captcha))
                    {
                        lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                        lblErrorMessage.Text = "Incorrect CAPTCHA. Please try again!";
                        // Should stop reloading or rendering
                        return;
                    }


                    // email is already in DB --> display error message       
                    if (userService.IsExisting(email))
                    {
                        errMessage.Text = "Email address is already registered.";
                        errMessage.Visible = true;
                    }
                    else
                    {
                        if (userService.CreateMember(email, password, Verify(email)) != 0)
                        {
                            Response.Redirect("~/Default");
                        }
                    }
                }
                else
                {
                    String errorOutput = "<ul>";
                    foreach (var validationResult in validationResults)
                    {
                        errorOutput += "<li>" + validationResult.ErrorMessage.ToString() + "</li>";
                    }
                    errorOutput += "</ul>";
                    lblErrorMessage.Text = errorOutput;
                    lblErrorMessage.Visible = true;
                }
            }
            catch (Exception)
            {
                Server.Transfer("~/ErrorPages/ErrorPage.aspx", true);
            }

        }

        /// <summary>
        /// Send email to verify a user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static string Verify(string email, bool forPassword = false)
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());

            SmtpClient smtpClient = new SmtpClient("mail.twistcomputers.ca", 587);

            smtpClient.Credentials = new System.Net.NetworkCredential("####", "###");
            // smtpClient.UseDefaultCredentials = true; // uncomment if you don't want to use the network credentials
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = false;
            MailMessage mail = new MailMessage();

            //Setting From , To and CC
            mail.From = new MailAddress("Chocolist.cvgs@twistcomputers.ca", "Chocolist");
            mail.To.Add(new MailAddress($"{email}"));

            mail.Subject = forPassword ? "Reset Password" : "Account Verification";
            mail.Body = forPassword ? $"Click on the link to reset password: " : $"Click on the link to verify: ";
            mail.Body += $"https://localhost:44343/Pages/User/VerifyAccount?email={email}&token={GuidString}";
            //DisableCertificateValidation();
            smtpClient.Send(mail);
            
            return GuidString;
        }

        //// Should not disable certificates but since this is not in production it's fine
        //static void DisableCertificateValidation()
        //{
        //    ServicePointManager.ServerCertificateValidationCallback =
        //        delegate (
        //            object s,
        //            X509Certificate certificate,
        //            X509Chain chain,
        //            SslPolicyErrors sslPolicyErrors
        //        ) {
        //            return true;
        //        };
        //}
    }
}