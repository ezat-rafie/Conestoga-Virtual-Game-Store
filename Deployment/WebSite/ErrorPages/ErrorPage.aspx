<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="Chocolist.Pages.ErrorPage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="ModifyItem.css" rel="stylesheet" />
    <div class="container p-2">
        <div class="mb-4">
            <h1 class="mx-auto" style="width: fit-content;">Something Went Wrong. Please click "Home" button below.</h1>
        </div>
        <div class="mb-4">
            <div class="row">
                <asp:HyperLink ID="lnkHome" class="mx-auto mb-4" runat="server" NavigateUrl="/">Return to the main page.</asp:HyperLink>
                <!--<asp:Button ID="ButtonBack" class="btn btn-secondary mx-auto mb-4 buttons" runat="server" Text="Home" OnClick="ButtonBack_Click" /> -->
            </div>

        </div>
    </div>
</asp:Content>
