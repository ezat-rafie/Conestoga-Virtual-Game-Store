<%@ Page Title="Friends" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Friends.aspx.cs" Inherits="Chocolist.Pages.User.Friends" ViewStateMode="Enabled" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .friend-title {
            color: #fff;
            font-size: 24px;
            padding: 5px;
            margin-left: 5px;
        }
        .friend-info {
            color: #fff;
            opacity: 0.6;
            font-size: 18px;
            padding: 5px;
            margin-left: 5px;
        }
        .result-title {
            color: #282B2E;
            font-size: 24px;
            padding: 5px;
            margin-left: 5px;
        }
        .result-info {
            color: #282B2E;
            opacity: 0.8;
            font-size: 18px;
            padding: 5px;
        }
        .friend-card {
            width: 100%;
            background-color: #d5e3e6;
            box-shadow: rgb(0 0 0 / 0.40) 5px 5px 10px;
        }
        .sent-request-card {
            width: 100%;
            background-color: #2b4561;
            box-shadow: rgb(0 0 0 / 0.40) 5px 5px 10px;
        }
        .request-card {
            width: 100%;
            background-color: #8a5b2b;
            box-shadow: rgb(0 0 0 / 0.40) 5px 5px 10px;
        }
    </style>

    <div class="row m-1">
        <div class="mb-4">
            <h3 class="mx-auto" style="width: fit-content; float: left; color: darkgrey; padding-left: 20px;">Friends & Family</h3>
        </div>
    </div>
    <div class="container-fluid">
        <div class="row content">
            <!-- Friend & Requests Section -->
            <div class="col-sm-4 sidenav">
                <asp:Repeater ID="rptFriend" runat="server"><%-- OnItemDataBound="rptFriend_ItemDataBound">--%>
                    <ItemTemplate>
                        <div class="card p-1 mb-2 game-card">
                            <div class="card-body p-0">
                                <div class="row m-1">
                                    <div class="col">
                                        <h5 class="friend-title "><%#Eval("displayName") %></h5>
                                    </div>
                                    <div class="col">
                                        <asp:LinkButton runat="server" ID="btnViewWishList" Text="View Wish List"
                                            CommandArgument='<%#Eval("UserId") %>' OnCommand="btnViewWishList_Click"
                                            CssClass="btn btn-secondary float-right add-to-cart" />
                                    </div>
                                </div>
                                <span class="friend-info"><%#Eval("firstName")%> <%#Eval("lastName")%></span> <br />
                                <span class="friend-info"><%#Eval("emailAddress") %></span>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <br /><br />
                <h4 class="mx-auto" style="width: fit-content; float: left; color: darkgrey; padding:0px 0px 10px 20px;">Sent Requests</h4> <br />
                <div class="row float-left">
                        <asp:Label Text="" ID="sentRequestMsg" runat="server" />
                    </div>
                <asp:Repeater ID="rptSentRequest" runat="server">
                    <ItemTemplate>
                        <div class="card p-1 mb-2 sent-request-card">
                            <div class="card-body p-0">
                                <div class="row m-1">
                                    <div class="col">
                                        <h5 class="friend-title "><%#Eval("displayName") %></h5>
                                    </div>
                                    <div class="col">
                                        <asp:LinkButton runat="server" ID="btnCancel" Text="Cancel"
                                            CommandArgument='<%#Eval("UserId") %>' OnCommand="btnCancel_Command"
                                            CssClass="btn btn-secondary float-right add-to-cart" />
                                    </div>
                                </div>
                                <span class="friend-info"><%#Eval("firstName")%> <%#Eval("lastName")%></span> <br />
                                <span class="friend-info"><%#Eval("emailAddress") %></span>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>  
                <br /><br />
                <h4 class="mx-auto" style="width: fit-content; float: left; color: darkgrey; padding:0px 0px 10px 20px;">Requests</h4> <br />
                <asp:Label runat="server" ID="requestMsg" class="float-left" Text="" />
                <asp:Repeater ID="rptRequest" runat="server">
                    <ItemTemplate>
                        <div class="card p-1 mb-2 request-card">
                            <div class="card-body p-0">
                                <div class="row m-1">
                                    <div class="col">
                                        <h5 class="friend-title "><%#Eval("displayName") %></h5>
                                    </div>
                                    <div class="col">
                                        <div class="row float-right">
                                            <asp:LinkButton runat="server" ID="btnAccept" Text="Accept"
                                                CommandArgument='<%#Eval("UserId") %>' OnCommand="btnAccept_Command"
                                                CssClass="btn btn-secondary float-right add-to-cart" />
                                            <asp:LinkButton runat="server" ID="btnIgnore" Text="Ignore"
                                                CommandArgument='<%#Eval("UserId") %>' OnCommand="btnIgnore_Command"
                                                CssClass="btn btn-secondary float-right add-to-cart ml-2" />
                                        </div>                                        
                                    </div>
                                </div>
                                <span class="friend-info"><%#Eval("firstName")%> <%#Eval("lastName")%></span> <br />
                                <span class="friend-info"><%#Eval("emailAddress") %></span>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>               
            </div>

            <!-- Search Section -->
            <div class="col-sm-8" >
                <center>
                    <asp:TextBox runat="server" ID="txtSearch" type="text" class="w-50 text-input" placeholder="Search User" />
                    <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" /><br /><br />
                    <asp:Label runat="server" ID="searchMsg" Text="" />
                </center><br />
                <div class="panel">
                    <asp:Repeater ID="rptResult" runat="server">
                        <ItemTemplate>
                            <div class="card p-1 mb-2 friend-card">
                                <div class="card-body p-0">
                                    <div class="row m-1">
                                        <div class="col-sm-3 align-self-center">
                                            <span class="result-title"><%#Eval("displayName") %></span>
                                        </div>
                                        <div class="col-sm-3 align-self-center">
                                            <span class="result-info"><%#Eval("firstName")%> <%#Eval("lastName")%></span> 
                                        </div>
                                        <div class="col-sm-4 align-self-center">
                                            <span class="result-info"><%#Eval("emailAddress") %></span>
                                        </div>
                                        <asp:LinkButton runat="server" ID="btnRequest" Text="Request"
                                            CommandArgument='<%#Eval("UserId") %>' OnCommand="btnRequest_Click"
                                            CssClass="btn btn-secondary float-right add-to-cart align-self-center"/>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
</asp:Content>