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
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Chocolist
{
    public partial class SiteMaster : MasterPage
    {
        #region Fields

        /// <summary>
        /// User service for data access
        /// </summary>
        IUserService userService = new UserService();

        /// <summary>
        /// User list from DB
        /// </summary>
        List<User> employeeList = new List<User>();

        /// <summary>
        /// Loged in user id
        /// </summary>
        int loggedInUserId = 0;

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
            employeeList = userService.GetAllEmployees();

            var loggedIn = Session["LoggedIn"];
            if (loggedIn != null)
            {
                loggedInUserId = Convert.ToInt32(loggedIn);
                if (loggedInUserId > 0)
                {
                    var profile = userService.GetProfile(loggedInUserId);
                    userProfile.Title = !string.IsNullOrEmpty(profile.DisplayName) ? profile.DisplayName : profile.EmailAddress;
                    UpdateCart(ShopCarts.ShoppingCart.Instance.GetItemCount());
                }
            }
            ShowAdminOption();
        }

        public void UpdateCart(int value)
        {
            if (loggedInUserId > 0)
            {
                if (value > 0)
                {
                    cartCount.Text = "(" + value.ToString() + ")";
                }
            }
        }

        protected void ShowAdminOption()
        {

            if (loggedInUserId > 0 && employeeList.Any(u => u.UserId == loggedInUserId))
            {
                btnItemList.Visible = true;
                btnOrders.Visible = true;
                btnReviews.Visible = true;
                btnReports.Visible = true;
                return;
            }
            else if (loggedInUserId > 0 && !employeeList.Any(u => u.UserId == loggedInUserId))
            {
                btnWish.Visible = true;
                btnCart.Visible = true;
                btnFriendList.Visible = true;
                btnUserOrder.Visible = true;
            }

            btnItemList.Visible = false;
            
            if(loggedInUserId < 1)
            {
                string path = Request.Path.ToLower();
                switch (path)
                {
                    case "/pages/products/itemlist":
                    case "/pages/products/createitem":
                    case "/pages/products/createevent":
                    case "/pages/products/itemreviews":
                    case "/pages/cart/orders":
                        Response.Redirect("~/Default.aspx");
                        break;
                    case "/pages/products/eventdetail":
                        Response.Redirect("~/Pages/User/Login.aspx");
                        break;
                }

                if (path.Contains("modifyitem"))
                    Response.Redirect("~/Default.aspx");
                if (path.Contains("reports"))
                    Response.Redirect("~/Default.aspx");
            }
        }

        public string SetProfileDisplay
        {
            set
            {
                userProfile.Title = value;
            }
        }


        /// <summary>
        /// Check if a user is logged in and show pages according to the status
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CheckUserLoggedIn(object sender, EventArgs e)
        {
            if (loggedInUserId > 0)
            {
                Response.Redirect("~/Pages/User/Profile.aspx");
            }
            else
            {
                Response.Redirect("~/Pages/User/Login.aspx");
            }
        }

        #endregion Methods 



    }
}