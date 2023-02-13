using DataService.DataAccessLayer.IServices;
using DataService.DataAccessLayer.Services;
using DataService.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.UI.WebControls;

namespace Chocolist.Pages.User
{
    public partial class Friends : System.Web.UI.Page
    {
        IFriendService friendService = new FriendService();
        static List<FriendUser> friendList = new List<FriendUser>();
        static List<FriendUser> requestList = new List<FriendUser>();
        static List<FriendUser> sentRequestList = new List<FriendUser>();
        static List<FriendUser> resultList = new List<FriendUser>();
        static int loggedInUserID;

        protected void Page_Load(object sender, EventArgs e)
        {
            loggedInUserID = (int)Session["LoggedIn"];
            if (!IsPostBack)
            {
                DisplayFriends();
                DisplayRequests();
            }
        }

        private void ResetMessages()
        {
            searchMsg.Text = "";
            requestMsg.Text = "";
            sentRequestMsg.Text = "";
        }

        #region Friend Section
        protected void DisplayFriends()
        {
            friendList = friendService.GetFriends(loggedInUserID);
            rptFriend.DataSource = friendList;
            rptFriend.DataBind();
        }

        //protected void rptFriend_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    FriendUser dtitem = e.Item.DataItem as FriendUser;
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        if (!(bool)dtitem.GetType().GetProperty("Approved").GetValue(dtitem))
        //        {
        //            LinkButton btnViewWishList = e.Item.FindControl("btnViewWishList") as LinkButton;
                    
        //            btnViewWishList.Enabled = false;
        //            btnViewWishList.Text = "Requested";
        //            btnViewWishList.ForeColor = System.Drawing.Color.Gray;
        //        }

        //    }
        //}

        protected void btnViewWishList_Click(object sender, CommandEventArgs e)
        {
            Session["FriendId"] = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("~/Pages/Cart/ViewWishList.aspx");
        }
        #endregion

        #region Request Section
        protected void DisplayRequests()
        {
            sentRequestList = friendService.GetRequests(loggedInUserID, true);
            rptSentRequest.DataSource = sentRequestList;
            rptSentRequest.DataBind();

            requestList = friendService.GetRequests(loggedInUserID, false);
            rptRequest.DataSource = requestList;
            rptRequest.DataBind();
        }

        protected void btnCancel_Command(object sender, CommandEventArgs e)
        {
            ResetMessages();
            try
            {
                if (friendService.DeleteLinkedUser(loggedInUserID, Convert.ToInt32(e.CommandArgument)))
                {
                    sentRequestMsg.Text = "Request Canceled.";
                    sentRequestMsg.ForeColor = System.Drawing.Color.Green;
                    DisplayRequests();
                }
                else
                {
                    throw new Exception("Cancel failed...");
                }
            }
            catch (Exception err)
            {
                sentRequestMsg.Text = err.Message;
                sentRequestMsg.ForeColor = System.Drawing.Color.Red;
                Debug.Print(err.Message);
                throw;
            }
        }
        protected void btnAccept_Command(object sender, CommandEventArgs e)
        {
            ResetMessages();
            if (friendService.UpdateApproved(Convert.ToInt32(e.CommandArgument), loggedInUserID, true))
            {
                DisplayFriends();
                DisplayRequests();
            }
        }

        protected void btnIgnore_Command(object sender, CommandEventArgs e)
        {
            ResetMessages();
            if (friendService.UpdateApproved(Convert.ToInt32(e.CommandArgument), loggedInUserID, false))
            {
                DisplayRequests();
            }
        }
        #endregion

        #region Search Section
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ResetMessages();
            resultList = friendService.SearchUsers(loggedInUserID, txtSearch.Text);
            rptResult.DataSource = resultList;
            rptResult.DataBind();
        }

        protected void btnRequest_Click(object sender, CommandEventArgs e)
        {
            ResetMessages();
            if(friendList.Any(f => f.UserId == Convert.ToInt32(e.CommandArgument)))
            {
                searchMsg.Text = "You two are already friends!";
                searchMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }
            if(friendService.CheckDuplicate(loggedInUserID, Convert.ToInt32(e.CommandArgument)))
            {
                searchMsg.Text = "Request has been sent already.";
                searchMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }
            if (friendService.CreateLinkedUser(loggedInUserID, Convert.ToInt32(e.CommandArgument)))
            {
                searchMsg.Text = "Friend Request has been sent!";
                searchMsg.ForeColor = System.Drawing.Color.Green;
                resultList.Remove(resultList.FirstOrDefault(f => f.UserId == Convert.ToInt32(e.CommandArgument)));
                DisplayFriends();
                DisplayRequests();
                rptResult.DataSource = resultList;
                rptResult.DataBind();
            }
            else
            {
                searchMsg.Text = "Friend Request failed...";
                searchMsg.ForeColor = System.Drawing.Color.Red;
            }
        }


        #endregion

    }
}