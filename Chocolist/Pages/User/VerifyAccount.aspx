<%@ Page Title="Verify" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerifyAccount.aspx.cs" Inherits="Chocolist.Pages.User.VerifyAccount" ViewStateMode="Enabled" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="w-100">
        <h3 runat="server" class="text-danger mx-auto mt-4" style="width:fit-content;" id="message"></h3>
    </div>
        <div class="mt-4">
            <div class="row">
                <a href="~/" id="redirectMessage" class="mx-auto" runat="server">Return to the main page.</a>
            </div>
        </div>
    </asp:Content>