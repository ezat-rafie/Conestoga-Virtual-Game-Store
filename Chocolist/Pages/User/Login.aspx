<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Chocolist.Pages.User.Login" ViewStateMode="Enabled" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Login.css" rel="stylesheet" />
    <div>
        <img src="/images/10.jpg" alt="Background" class="background-image" />

        <div class="mx-auto" id="login-form">
            <div style="margin-bottom: 3em;">
                <h1 class="mx-auto w-fit">Sign In</h1>
                <p class="mx-auto w-fit">Not registered? <a href="Register.aspx" style="color: #008A7A;" id="createAccount">Create an account</a></p>
            </div>
            <div class="form-group">
                <input type="text" class="form-control mx-auto w-100 text-input" id="emailLogin" placeholder="Email" clientidmode="static" runat="server">
                <asp:RequiredFieldValidator ID="RequiredEmail" runat="server" ControlToValidate="emailLogin" Display="Dynamic" CssClass="ml-4 text-danger"
                    ErrorMessage="Please enter email address." />
                <asp:Label ID="lblEmailError" runat="server" ClientIDMode="Static" Visible="false" />
            </div>
            <div class="form-group">
                <input type="password" class="form-control mx-auto w-100 text-input" id="passwordLogin" placeholder="Password" clientidmode="static" runat="server">
                <asp:RequiredFieldValidator ID="RequiredPassword" runat="server" ControlToValidate="passwordLogin" Display="Dynamic" CssClass="ml-4  text-danger"
                    ErrorMessage="Please enter password." />
                <asp:Label ID="lblPasswordError" runat="server" ClientIDMode="Static" Visible="false" />
            </div>
            <asp:Label ID="lblLoginError" runat="server" ClientIDMode="Static" CssClass="ml-4 text-danger align-center" Visible="false" 
                style="display:inline-block;"/>
            <asp:Button runat="server" ID="btnReset" CssClass="btn btn-warning mt-4 centerButton" Text="Send me reset link" Visible="false"
                OnClick="btnSendResetLink_Click" CausesValidation="false"/>
            <div class="mx-auto w-fit mt-4">                
                <asp:Button ID="btnLogin" runat="server" ClientIDMode="static" Text="Log In" OnClick="btnLogin_Click" class="btn btn-primary mt-4"  />
            </div>
        </div>
    </div>

</asp:Content>
