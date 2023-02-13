<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Chocolist.Pages.User.Register" ViewStateMode="Enabled" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Login.css" rel="stylesheet" />
    <div>
        <img src="/images/10.jpg" alt="Background" class="background-image" />

        <div class="mx-auto" id="register-form">
            <div style="margin-bottom: 3em;">
                <h1 class="mx-auto w-fit">Register</h1>
                <p class="mx-auto w-fit">Already have an account? <a href="Login.aspx" style="color: #008A7A;" id="logIntoAccount">Log In</a></p>
            </div>
            <div class="form-group">
                <input type="email" class="form-control mx-auto w-100 text-input" id="emailRegister" placeholder="Email" clientidmode="static" runat="server">
                <asp:RequiredFieldValidator ID="reqEmail" runat="server" ControlToValidate="emailRegister" Display="Dynamic" CssClass="ml-4 text-danger"
                    ErrorMessage="Please enter email address." />
                <asp:Label ID="errMessage" runat="server" ClientIDMode="Static" Visible="false" />
            </div>
            <div class="form-group">
                <input type="password" class="form-control mx-auto w-100 text-input" id="passwordRegister" placeholder="Password" clientidmode="static" runat="server">
                <asp:RequiredFieldValidator ID="reqPassword" runat="server" ControlToValidate="passwordRegister" Display="Dynamic" CssClass="ml-4 text-danger"
                    ErrorMessage="Please enter password." />
                <div class="ml-4">
                <asp:RegularExpressionValidator ID="regPassword" runat="server" ControlToValidate="passwordRegister" Display="Dynamic" CssClass="text-danger"
                    ErrorMessage="Password must be minimum eight characters, at least one letter, one number and one special character."
                    ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$" />
                </div>
            </div>
            <div class="form-group">
                <cc1:CaptchaControl ID="cptCaptcha" runat="server"
                    CssClass="captcha w-100 mx-auto"
                    CaptchaBackgroundNoise="Low" CaptchaLength="5"
                    CaptchaHeight="80" CaptchaWidth="250"
                    CaptchaLineNoise="None" CaptchaMinTimeout="5"
                    CaptchaMaxTimeout="240" FontColor="#529E00" />
            </div>
            <div class="form-group">
                <div>
                    <asp:TextBox ID="txtCaptcha" class="form-control mx-auto w-100 text-input" runat="server" placeholder="Please enter code"></asp:TextBox>
                <asp:Label ID="lblErrorMessage" runat="server" Text="" CssClass="ml-4 text-danger"></asp:Label>
                </div>
            </div>

            <div class="mx-auto w-fit mt-4">
                <asp:Button ID="btnRegister" runat="server" ClientIDMode="static" Text="Register" OnClick="btnRegister_Click" class="btn btn-primary mt-4" />
            </div>
        </div>
    </div>
<%--    <script>
        $(document).ready(function () {
            $('#register-form').hide();
            //if ($('[id*=txtName]').val.length > 0) {
            //    $('#createAccount').click();
            //}
        });
        $('#createAccount').on('click', function () {
            $('#login-form').hide("slow");
            $('#register-form').show("slow");
        });
        $('#logIntoAccount').on('click', function () {
            $('#register-form').hide("slow");
            $('#login-form').show("slow");
        });

    </script>--%>
</asp:Content>
