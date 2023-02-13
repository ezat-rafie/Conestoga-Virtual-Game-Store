/*
 * Subject      : PROG3050-Sec2: Microsoft Enterprise Application
 * Title        : Iteration#1 Source Code
 * Team         : #5 Chocolist
 * Team Members : Trevor White, Keum Ji Kim, Ilshin Ji, Ezatullah Rafie, Asraa Aleem-Uddin  
 * Created      : October 2022 
 */

using DataService.DataAccessLayer.IServices;
using DataService.DataAccessLayer.Services;
using DataService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.UI;
using System.Web.UI.WebControls;
using Validations;
using System.Drawing;
using System.Text.RegularExpressions;

namespace Chocolist.Pages.User
{
    /// <summary>
    /// Profile page code-hehind partial class
    /// </summary>
    public partial class Profile : System.Web.UI.Page
    {
        #region Fields

        /// <summary>
        /// User service for data access
        /// </summary>
        IUserService userService = new UserService();
        IAddressService addressService = new AddressService();
        ICreditCardService creditCardService = new CreditCardService();
        public static int loggedInUserID;
        public static int userAddressID;
        public static bool isEditingCard;
        public static int cardId;
        public static bool isPwChangeRequired;

        #endregion Fields 

        #region 0. Common for Profile Page
        #region 0-1. Handlers
        /// <summary>
        /// Navigating the tabs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Menu1_MenuItemClick(object sender, System.Web.UI.WebControls.MenuEventArgs e)
        {
            int index = Int32.Parse(e.Item.Value);
            MultiView1.ActiveViewIndex = index;

            switch (index)
            {
                case 1: // Address
                    PopulateAddressPage();
                    break;
                case 2: // Payment
                    PopulatePurchasePage();
                    break;
            }
        }
        #endregion

        #region 0-2. Methods
        /// <summary>
        /// Initial page load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            RangeValidator1.MaximumValue = DateTime.Now.Date.ToString("yyyy-MM-dd");
            if (!IsPostBack)
            {
                if (Request.QueryString["tabIndex"] != null)
                {
                    int menuTabIndex = Convert.ToInt32(Request.QueryString["tabIndex"]);
                    if (menuTabIndex >=0 && menuTabIndex <=2)
                    {
                        MultiView1.ActiveViewIndex = menuTabIndex;
                    }
                }
                PopulateProfilePage();
            }
        }
        #endregion
        #endregion

        #region 1. Profile
        #region 1-1. Handlers
        /// <summary>
        /// Validate inputs and update profile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (isPwChangeRequired)
            {
                if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
                {
                    regPassword.Text = "New password is requred.";
                    return;
                }
                if (!UpdatePassword()) return;
            }
            else if (!string.IsNullOrWhiteSpace(txtNewPassword.Text))
            {
                if (string.IsNullOrWhiteSpace(txtOldPassword.Text))
                {
                    errOldPassword.Text = "Current password is required to change password.";
                    errOldPassword.Visible = true;
                }
                if (!UpdatePassword()) return;
            }

            DataService.Models.Profile updatedProfile = new DataService.Models.Profile
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                DisplayName = txtDisplayName.Text,
                BirthDate = DateTime.Parse(txtBirthDate.Text),
                Gender = ddlGender.SelectedValue,
                ReceivePromotional = chkPromo.Checked,
                UserId = loggedInUserID,
                GenreId = int.Parse(ddlGenre.SelectedValue),
                PlatformId = int.Parse(ddlPlatform.SelectedValue)
            };

            if (userService.UpdateProfile(updatedProfile))
            {
                userService.ResetLoginAttempts(loggedInUserID);
                msgProfile.InnerText = "Profile updated successfully";
                SiteMaster masterPage = Page.Master as SiteMaster;
                masterPage.SetProfileDisplay = txtDisplayName.Text;
            }
            else
            {
                msgProfile.InnerText = "Failed to update profile";
            }
        }

        protected void ButtonLogout_Click(object sender, EventArgs e)
        {
            Session.Remove("LoggedIn");
            ShopCarts.ShoppingCart.Instance.ClearCart();
            Response.Redirect("/Default.aspx");
        }
        #endregion

        #region 1-2. Methods
        private void PopulateProfilePage()
        {
            loggedInUserID = Session["LoggedIn"] != null ? (int)Session["LoggedIn"] : 0;
            if (loggedInUserID > 0)
            {
                var profile = userService.GetProfile(loggedInUserID);
                txtFirstName.Text = String.IsNullOrEmpty(profile.FirstName) ? "" : profile.FirstName;
                txtLastName.Text = String.IsNullOrEmpty(profile.LastName) ? "" : profile.LastName;
                txtDisplayName.Text = String.IsNullOrEmpty(profile.DisplayName) ? "" : profile.DisplayName;
                txtEmail.Text = String.IsNullOrEmpty(profile.EmailAddress) ? "" : profile.EmailAddress;
                txtBirthDate.Text = DateTime.MinValue == profile.BirthDate ? DateTime.Now.ToString("yyyy-MM-dd") : profile.BirthDate.Date.ToString("yyyy-MM-dd");
                ddlGender.SelectedValue = String.IsNullOrEmpty(profile.Gender) ? "15" : profile.Gender.ToString();
                chkPromo.Checked = profile.ReceivePromotional;
                ddlPlatform.SelectedValue = profile.PlatformId.ToString();
                ddlGenre.SelectedValue = profile.GenreId.ToString();
                if (profile.Password.ToString() == "temporary")
                {
                    passwordControl.Attributes.Remove("class");
                    passwordControl.Attributes.Add("class", "col-6 border border-warning rounded p-3");
                    isPwChangeRequired = true;
                    ctlOldPassword.Visible = false;
                    txtNewPassword.Focus();
                }
            }
            else
            {
                Response.Redirect("~/Pages/User/Login.aspx");
            }
        }

        private bool UpdatePassword()
        {
            string password = string.IsNullOrWhiteSpace(txtOldPassword.Text) ? "temporary" : txtOldPassword.Text;
            if (userService.Login(txtEmail.Text, password) > 0)
            {
                if (txtNewPassword.Text != txtNewPassword2.Text)
                {
                    errPassword.Text = "Please enter the same password.";
                    errPassword.Visible = true;
                    return false;
                }

                userService.UpdatePassword(loggedInUserID, txtNewPassword.Text);
                return true;
            }
            else
            {
                errOldPassword.Text = "You've entered wrong password.";
            }
            return false;
        }
        #endregion
        #endregion


        #region 2. Address
        #region 2-1. Handlers
        protected void btnSaveAddress_Click(object sender, EventArgs e)
        {
            try
            {
                Address newAddress = new Address
                {
                    FullName = txtAddAddressName.Text,
                    UserId = loggedInUserID,
                    AddressLine1 = txtAddAddress1.Text,
                    AddressLine2 = txtAddAddress2.Text,
                    AddressLine3 = txtAddAddress3.Text,
                    City = txtAddCity.Text,
                    Province = ddlProvince.SelectedValue,
                    PostalCode = txtAddPostal.Text,
                    AddressId = userAddressID
                };

                AddressValidation addressValidation = new AddressValidation();
                addressValidation.FullName = newAddress.FullName;
                addressValidation.AddressLine1 = newAddress.AddressLine1;
                addressValidation.City = newAddress.City;
                addressValidation.Province = newAddress.Province;
                addressValidation.PostalCode = newAddress.PostalCode;

                var context = new ValidationContext(addressValidation, serviceProvider: null, items: null);
                var validationResults = new List<ValidationResult>();
                Boolean isValid = Validator.TryValidateObject(addressValidation, context, validationResults, true);
                if (isValid)
                {
                    newAddress.PostalCode = Regex.Replace(newAddress.PostalCode, @"\s+", "");
                    if (userAddressID > 0)
                    {
                        bool result = addressService.UpdateAddress(newAddress);
                        if (result == true)
                        {
                            msgAddress.InnerText = "Address updated successfully";
                            SiteMaster masterPage = Page.Master as SiteMaster;
                            masterPage.SetProfileDisplay = txtDisplayName.Text;
                            userAddressID = 0;
                            PopulateAddressPage();
                        }
                        else
                        {
                            msgAddress.InnerText = "Failed to update address";
                        }
                    }
                    else
                    {
                        int result = addressService.CreateAddress(newAddress);
                        if (result > 0)
                        {
                            msgAddress.InnerText = "Address added successfully";
                            SiteMaster masterPage = Page.Master as SiteMaster;
                            masterPage.SetProfileDisplay = txtDisplayName.Text;
                            userAddressID = 0;
                            PopulateAddressPage();
                            ClearAddressForm();
                        }
                        else
                        {
                            msgAddress.InnerText = "Failed to add address";
                        }

                    }
                }
                else
                {
                    String errorMessage = "";
                    foreach (ValidationResult item in validationResults)
                    {
                        errorMessage += item.ErrorMessage;
                    }
                    msgAddress.InnerText = errorMessage;
                }
            }
            catch (Exception)
            {
                Server.Transfer("~/ErrorPages/ErrorPage.aspx", true);
            }

        }

        /// <summary>
        /// Remove Address from Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RemoveAddress(object sender, CommandEventArgs e)
        {
            msgAddress.InnerText = "";
            try
            {
                userAddressID = Convert.ToInt32(e.CommandArgument);
                bool result = addressService.RemoveAddress(loggedInUserID, userAddressID);

                if (result)
                {
                    userAddressID = 0;
                    PopulateAddressPage();
                    msgAddress.InnerText = "Address removed successfully.";
                    cardMessage.ForeColor = Color.Green;
                }
            }
            catch (Exception)
            {
                msgAddress.InnerText = "Failed to remove address. Address is in use.";
                //Server.Transfer("~/ErrorPages/ErrorPage.aspx", true);
            }

        }
        
        protected void EditAddress(object sender, CommandEventArgs e)
        {
            try
            {
                userAddressID = Convert.ToInt32(e.CommandArgument);
                Address address = addressService.GetAddress(loggedInUserID, userAddressID);

                if (address != null)
                {
                    txtAddAddressName.Text = address.FullName;
                    txtAddAddress1.Text = address.AddressLine1;
                    txtAddAddress2.Text = address.AddressLine2;
                    txtAddAddress3.Text = address.AddressLine3;
                    txtAddCity.Text = address.City;
                    ddlProvince.SelectedValue = address.Province;
                    txtAddPostal.Text = address.PostalCode;
                    btnSaveAddress.Text = "Save";
                }
            }
            catch (Exception)
            {
                Server.Transfer("~/ErrorPages/ErrorPage.aspx", true);
            }            
        }

        protected void btnCancelAddress_Click(object sender, EventArgs e)
        {
            userAddressID = 0;
            ClearAddressForm();
        }

        protected void rptAddress_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Address dtAddress = e.Item.DataItem as Address;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string fullAddress = dtAddress.GetType().GetProperty("AddressLine1").GetValue(dtAddress)?.ToString();
                string add2 = dtAddress.GetType().GetProperty("AddressLine2").GetValue(dtAddress)?.ToString();
                string add3 = dtAddress.GetType().GetProperty("AddressLine3").GetValue(dtAddress)?.ToString();

                fullAddress += !string.IsNullOrWhiteSpace(add2) ? $", {add2}" : "";
                fullAddress += !string.IsNullOrWhiteSpace(add3) ? $", {add3}" : "";

                TextBox txtAddress = e.Item.FindControl("txtAddress") as TextBox;
                txtAddress.Text = fullAddress;
            }
        }
        #endregion

        #region 2-2. Methods
        private void PopulateAddressPage()
        {
            if (loggedInUserID > 0)
            {
                List<Address> addresses = addressService.GetAddresses(loggedInUserID);
                rptAddress.DataSource = addresses;
                rptAddress.DataBind();
            }
            ClearAddressForm();
        }

        private void ClearAddressForm()
        {
            txtAddAddressName.Text = "";
            txtAddAddress1.Text = "";
            txtAddAddress2.Text = "";
            txtAddAddress3.Text = "";
            txtAddCity.Text = "";
            ddlProvince.SelectedValue = "ON";
            txtAddPostal.Text = "";
            btnSaveAddress.Text = "Add";
        }
        #endregion
        #endregion


        #region 3. Payment
        #region 3-1. Handlers
        /// <summary>
        /// Remove Card from Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RemoveCard(object sender, CommandEventArgs e)
        {
            cardMessage.Text = "";
            try
            {
                cardId = Convert.ToInt32(e.CommandArgument);
                bool result = creditCardService.RemoveCreditCard(loggedInUserID, cardId);

                if (result)
                {
                    PopulatePurchasePage();
                    cardMessage.Text = "Card removed successfully.";
                    cardMessage.ForeColor = Color.Green;
                }
            }
            catch (Exception)
            {
                Server.Transfer("~/ErrorPages/ErrorPage.aspx", true);
            }

        }

        /// <summary>
        /// Add new Card to Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AddNewCard(object sender, CommandEventArgs e)
        {
            cardMessage.Text = "";
            try
            {
                Validations.CreditCardValidation creditCard = new Validations.CreditCardValidation();
                string displayName = txtAddDisplayName.Text;
                string cardNumber = txtAddCardNumber.Text;
                string expiryMonth = addExpireMonth.Text;
                string expiryYear = addExpireYear.Text;
                string cvv = txtAddCvv.Text;

                creditCard.displayName = displayName;
                creditCard.cardNumber = cardNumber;
                creditCard.expireMonth = expiryMonth;
                creditCard.expireYear = expiryYear;
                creditCard.cvv = cvv;

                var context = new ValidationContext(creditCard, serviceProvider: null, items: null);
                var validationResults = new List<ValidationResult>();
                Boolean isValid = Validator.TryValidateObject(creditCard, context, validationResults, true);

                if (isValid)
                {
                    DateTime today = DateTime.Now;
                    if (expiryYear.Length == 2)
                        expiryYear = "20" + expiryYear;
                    int expireYear = int.Parse(expiryYear);
                    int expireMonth = int.Parse(expiryMonth);
                    if (expireYear < today.Year ||
                        (expireMonth < today.Month && expireYear <= today.Year))
                    {
                        cardMessage.Text = "Expiry date cannot be in past. Please enter valid date.";
                        cardMessage.ForeColor = Color.Red;
                    }
                    else
                    {
                        DateTime convertedDate = new DateTime(expireYear, expireMonth, 01, 00, 00, 01);
                        if (isEditingCard)
                        {
                            bool result = creditCardService.UpdateCreditCard(loggedInUserID, cardId, displayName, long.Parse(cardNumber), convertedDate, int.Parse(cvv));

                            if (result)
                            {
                                cardMessage.Text = "Card updated successfully.";
                                cardMessage.ForeColor = Color.Green;
                                txtAddDisplayName.Text = "";
                                txtAddCardNumber.Text = "";
                                addExpireMonth.SelectedIndex = 0;
                                addExpireYear.SelectedIndex = 0;
                                txtAddCvv.Text = "";
                                btnAddNewCard.Text = "Add";
                                isEditingCard = false;
                                PopulatePurchasePage();
                            }
                            else
                            {
                                cardMessage.Text = "Failed to update card. Please try again.";
                                cardMessage.ForeColor = Color.Red;
                            }
                        }
                        else
                        {
                            bool result = creditCardService.AddCreditCard(loggedInUserID, displayName, long.Parse(cardNumber), convertedDate, int.Parse(cvv));

                            if (result)
                            {
                                cardMessage.Text = "Card registered successfully.";
                                cardMessage.ForeColor = Color.Green;
                                txtAddDisplayName.Text = "";
                                txtAddCardNumber.Text = "";
                                addExpireMonth.Text = "";
                                addExpireYear.Text = "";
                                txtAddCvv.Text = "";
                                PopulatePurchasePage();
                            }
                            else
                            {
                                cardMessage.Text = "Failed to register card. Please try again.";
                                cardMessage.ForeColor = Color.Red;
                            }
                        }

                    }
                }
            }
            catch (Exception)
            {
                Server.Transfer("~/ErrorPages/ErrorPage.aspx", true);
            }

        }

        /// <summary>
        /// Edit card
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EditCard(object sender, CommandEventArgs e)
        {
            cardMessage.Text = "";
            try
            {
                btnAddNewCard.Text = "Save";
                isEditingCard = true;
                cardId = Convert.ToInt32(e.CommandArgument);
                CreditCard result = creditCardService.GetCreditCard(loggedInUserID, cardId);
                if (result != null)
                {
                    txtAddDisplayName.Text = result.DisplayName;
                    txtAddCardNumber.Text = result.CardNumber.ToString();
                    addExpireMonth.Text = result.Expiry.Month.ToString();
                    addExpireYear.Text = result.Expiry.Year.ToString();
                    txtAddCvv.Text = result.CVV.ToString();
                }
            }
            catch (Exception)
            {
                Server.Transfer("~/ErrorPages/ErrorPage.aspx", true);
            }
        }

        protected void OpenAddCard(object sender, CommandEventArgs e)
        {
            btnAddNewCard.Text = "Add";
            isEditingCard = false;
        }
        #endregion

        #region 3-2. Methods
        private void PopulatePurchasePage()
        {
            if (loggedInUserID > 0)
            {
                List<CreditCard> creditCards = creditCardService.GetCreditCards(loggedInUserID);
                rptCreditCard.DataSource = creditCards;
                rptCreditCard.DataBind();
            }
        }
        #endregion

        #endregion
    }
}