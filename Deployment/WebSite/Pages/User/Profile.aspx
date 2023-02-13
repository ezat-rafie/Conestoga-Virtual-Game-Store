<%@ Page Title="Profile" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Chocolist.Pages.User.Profile" ViewStateMode="Enabled" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .text-input {
            background-color: #d9d9d912;
            border: none;
            color: #fff;
            font-size: 20px;
            border-radius: 5px;
            padding: 5px;
            box-shadow: rgb(0 0 0 / 0.40) 2px 2px 10px;
        }

        .tabs {
            position: relative;
            top: 1px;
            left: 10px;
        }

        .tab {
            padding: 5px;
            color: gray;
        }

        .selectedTab {
            border-top: solid 1px white;
        }

        .tabContents {
            padding: 20px 10px;
        }

        .panel a:hover {
            color: white;
        }

        .panel a.static.selected {
            color: #FF6B00 !important;
        }

        .text-size-25 {
            font-size: 20px;
        }

        .w-45 {
            width: 45% !important;
        }

        .disabledInput {
            background-color: #444444bf;
        }

        .gender-dropdown {
            width: 100%;
            box-shadow: none !important;
            text-align: left !important;
        }
    </style>

    <div class="panel">
        <asp:Menu ID="Menu1" Orientation="Horizontal" StaticMenuItemStyle-CssClass="tab" Font-Size="X-Large"
            StaticSelectedStyle-CssClass="selectedTab" StaticSelectedStyle-ForeColor="White" StaticSelectedStyle-Font-Bold="true"
            StaticMenuItemStyle-HorizontalPadding="20px"
            StaticMenuItemStyle-VerticalPadding="10px"
            CssClass="tabs" runat="server" OnMenuItemClick="Menu1_MenuItemClick">
            <Items>
                <asp:MenuItem Text="Profile Info" Value="0" Selected="true"></asp:MenuItem>
                <asp:MenuItem Text="Address" Value="1"></asp:MenuItem>
                <asp:MenuItem Text="Payment" Value="2"></asp:MenuItem>
            </Items>
        </asp:Menu>
        <div class="tabContents">
            <asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">
                <!-- Profile Info -->
                <asp:View ID="tabProfile" runat="server">
                    <div class="row">
                        <div class="col-6">
                            <asp:Label
                                Text="Display name"
                                AssociatedControlID="txtDisplayName"
                                runat="server"
                                CssClass="w-100">
                                <asp:TextBox type="text" class="w-100 text-input" ID="txtDisplayName" placeholder="Display name" runat="server" />
                            </asp:Label>
                        </div>
                        <div class="col-6">
                            <asp:Label
                                Text="Email"
                                AssociatedControlID="txtEmail"
                                runat="server"
                                CssClass="w-100">
                                <asp:TextBox type="text" class="w-100 text-input" ID="txtEmail" placeholder="Email" runat="server" ReadOnly="true" ForeColor="gray" />
                            </asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-6">
                            <asp:Label
                                Text="First name"
                                AssociatedControlID="txtFirstName"
                                runat="server"
                                CssClass="w-100">
                                <asp:TextBox type="text" class="w-100 text-input" ID="txtFirstName" placeholder="First name" runat="server" />
                                <asp:RequiredFieldValidator ID="reqFirstName" runat="server" ControlToValidate="txtFirstName" Display="Dynamic"
                                    ErrorMessage="First name is required." ForeColor="orange" />
                            </asp:Label>
                        </div>
                        <div class="col-6">
                            <asp:Label
                                Text="Birth Date"
                                AssociatedControlID="txtBirthDate"
                                runat="server"
                                CssClass="w-100">
                                <asp:TextBox class="w-100 text-input" ID="txtBirthDate" runat="server" TextMode="Date" />
                                <asp:RangeValidator 
                                    runat="server" 
                                    ID="RangeValidator1" 
                                    Type="Date" 
                                    ControlToValidate="txtBirthdate" 
                                    MinimumValue="1800-01-01"
                                    MaximumValue="3999-12-31" 
                                    ErrorMessage="Birth Date can not be in the future" 
                                    ForeColor ="orange"
                                    Display="Dynamic"
                                />
                            </asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-6">
                            <asp:Label
                                Text="Last name"
                                AssociatedControlID="txtLastName"
                                runat="server"
                                CssClass="w-100">
                                <asp:TextBox type="text" class="w-100 text-input" ID="txtLastName" placeholder="Last name" runat="server" />
                                <asp:RequiredFieldValidator ID="reqLastName" runat="server" ControlToValidate="txtLastName" Display="Dynamic"
                                    ErrorMessage="Last name is required." ForeColor="orange" />
                            </asp:Label>
                        </div>
                        <div class="col-6">
                            <asp:Label
                                Text="Gender"
                                AssociatedControlID="ddlGender"
                                runat="server"
                                CssClass="w-100">
                                <asp:DropDownList class="w-100 text-input btn btn-secondary btn-sm dropdown-toggle gender-dropdown" ID="ddlGender" runat="server">
                                    <asp:ListItem Text="Male" Value="7" Selected="True" />
                                    <asp:ListItem Text="Female" Value="8" />
                                    <asp:ListItem Text="Prefer not to answer" Value="15" />
                                </asp:DropDownList>
                            </asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-6">
                            <asp:Label
                                Text="Platform"
                                AssociatedControlID="ddlPlatform"
                                runat="server"
                                CssClass="w-100">
                                <asp:DropDownList class="w-100 text-input btn btn-secondary btn-sm dropdown-toggle gender-dropdown" ID="ddlPlatform" runat="server">
                                    <asp:ListItem Text="(None)" Value="0" Selected="True" />
                                    <asp:ListItem Text="Macintosh" Value="1" />
                                    <asp:ListItem Text="Windows" Value="2" />
                                    <asp:ListItem Text="Nintendo" Value="3" />
                                    <asp:ListItem Text="Playstation" Value="4" />
                                    <asp:ListItem Text="XBox" Value="5" />
                                </asp:DropDownList>
                            </asp:Label>
                        </div>
                        <div class="col-6">
                            <asp:Label
                                Text="Genre"
                                AssociatedControlID="ddlGenre"
                                runat="server"
                                CssClass="w-100">
                                <asp:DropDownList class="w-100 text-input btn btn-secondary btn-sm dropdown-toggle gender-dropdown" ID="ddlGenre" runat="server">
                                    <asp:ListItem Text="(None)" Value="0" Selected="True" />
                                    <asp:ListItem Text="Action" Value="1" />
                                    <asp:ListItem Text="Adventure" Value="2" />
                                    <asp:ListItem Text="Strategy" Value="3" />
                                    <asp:ListItem Text="Family" Value="4" />
                                    <asp:ListItem Text="Puzzle" Value="5" />
                                    <asp:ListItem Text="Sports" Value="6" />
                                </asp:DropDownList>
                            </asp:Label>
                        </div>
                    </div>
                    <div class="row mt-3">
                        <div class="col-6 border border-secondary rounded p-3" id="passwordControl" runat="server">
                            <asp:Label
                                Text="Current Password"
                                AssociatedControlID="txtOldPassword"
                                runat="server" ID="ctlOldPassword"
                                CssClass="w-100">
                                <asp:TextBox TextMode="Password" class="w-100 text-input" ID="txtOldPassword" placeholder="Current Password" runat="server" />
                                <asp:Label ID="errOldPassword" runat="server" ClientIDMode="Static" Visible="false" ForeColor="Orange" />
                            </asp:Label>
                            <asp:Label
                                Text="New Password"
                                AssociatedControlID="txtNewPassword"
                                runat="server"
                                CssClass="w-100">
                                <asp:TextBox TextMode="Password" class="w-100 text-input" ID="txtNewPassword" placeholder="New Password" runat="server" />
                                <asp:RegularExpressionValidator ID="regPassword" runat="server" ControlToValidate="txtNewPassword" Display="Dynamic" CssClass="text-danger"
                                    ErrorMessage="Password must be minimum eight characters, at least one letter, one number and one special character."
                                    ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$" />
                            </asp:Label>
                            <asp:Label
                                Text="Confirm New Password"
                                AssociatedControlID="txtNewPassword2"
                                runat="server"
                                CssClass="w-100">
                                <asp:TextBox TextMode="Password" class="w-100 text-input" ID="txtNewPassword2" placeholder="Confirm Password" runat="server" />
                                <asp:Label ID="errPassword" runat="server" ClientIDMode="Static" Visible="false" ForeColor="Orange" />
                                <asp:CompareValidator runat="server" ID="newPasswordValidator" ControlToValidate="txtNewPassword2" ControlToCompare="txtNewPassword" Display="Dynamic" CssClass="text-danger"
                                    ErrorMessage="Please enter the same password." />
                            </asp:Label>
                        </div>
                        <div class="col-6">
                            <div class="row ml-2">
                                <asp:CheckBox ID="chkPromo" runat="server" Text="Receive Promotional Emails" ForeColor="White" />
                            </div>
                            <div class="row mt-3">
                                <h3 runat="server" class="text-warning mx-auto mt-2" style="width: fit-content; float: left;" id="msgProfile"></h3>
                                <div style="float: right;">
                                    <asp:Button ID="ButtonSave" class="btn btn-secondary mx-auto mb-2 buttons" runat="server" Text="Save" OnClick="ButtonSave_Click" />
                                    <asp:Button ID="ButtonLogout" class="btn btn-secondary mx-auto mb-2 buttons" runat="server" Text="Log Out" OnClick="ButtonLogout_Click" CausesValidation="false" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                    </div>
                </asp:View>

                <!-- Address -->
                <asp:View ID="tabAddress" runat="server">
                    <div class="container border border-secondary rounded p-3">
                        <div class="row">
                            <div class="col-6">
                                <asp:Label
                                    Text="Address Name"
                                    AssociatedControlID="txtAddAddressName"
                                    runat="server"
                                    CssClass="w-100">
                                    <asp:TextBox type="text" class="w-100 text-input" ID="txtAddAddressName" placeholder="Address Full Name" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtAddAddressName" Display="Dynamic"
                                            ErrorMessage="Address Full Name is required." ForeColor="orange" ValidationGroup="EditAddressTemplate" />
                                </asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6">
                                <asp:Label
                                    Text="Address Line 1"
                                    AssociatedControlID="txtAddAddress1"
                                    runat="server"
                                    CssClass="w-100">
                                    <asp:TextBox type="text" class="w-100 text-input" ID="txtAddAddress1" placeholder="Address Line 1" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAddAddress1" Display="Dynamic"
                                        ErrorMessage="Address Line 1 is required." ForeColor="orange" ValidationGroup="EditAddressTemplate" />
                                </asp:Label>
                            </div>
                            <div class="col-6">
                                <asp:Label
                                    Text="City"
                                    AssociatedControlID="txtAddCity"
                                    runat="server"
                                    CssClass="w-100">
                                    <asp:TextBox type="text" class="w-100 text-input" ID="txtAddCity" placeholder="City" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtAddCity" Display="Dynamic"
                                        ErrorMessage="City is required." ForeColor="orange" ValidationGroup="EditAddressTemplate" />
                                </asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6">
                                <asp:Label
                                    Text="Address Line 2"
                                    AssociatedControlID="txtAddAddress2"
                                    runat="server"
                                    CssClass="w-100">
                                    <asp:TextBox type="text" class="w-100 text-input" ID="txtAddAddress2" placeholder="Address Line 2" runat="server" />
                                </asp:Label>
                            </div>
                            <div class="col-6">
                                <asp:Label
                                    Text="Province"
                                    AssociatedControlID="ddlProvince"
                                    runat="server"
                                    CssClass="w-100">
                                    <asp:DropDownList class="w-100 text-input btn btn-secondary btn-sm dropdown-toggle gender-dropdown" ID="ddlProvince" runat="server">
                                        <asp:ListItem Text="ON" Value="ON" Selected="True" />
                                        <asp:ListItem Text="QC" Value="QC" />
                                        <asp:ListItem Text="BC" Value="BC" />
                                        <asp:ListItem Text="AB" Value="AB" />
                                        <asp:ListItem Text="MB" Value="MB" />
                                        <asp:ListItem Text="NB" Value="NB" />
                                        <asp:ListItem Text="NL" Value="NL" />
                                        <asp:ListItem Text="NT" Value="NT" />
                                        <asp:ListItem Text="NS" Value="NS" />
                                        <asp:ListItem Text="NU" Value="NU" />
                                        <asp:ListItem Text="PE" Value="PE" />
                                        <asp:ListItem Text="SK" Value="SK" />
                                        <asp:ListItem Text="YT" Value="YT" />
                                    </asp:DropDownList>
                                </asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-6">
                                <asp:Label
                                    Text="Address Line 3"
                                    AssociatedControlID="txtAddAddress3"
                                    runat="server"
                                    CssClass="w-100">
                                    <asp:TextBox type="text" class="w-100 text-input" ID="txtAddAddress3" placeholder="Address Line 3" runat="server" />
                                </asp:Label>
                            </div>
                            <div class="col-6">
                                <asp:Label
                                    Text="Postal Code"
                                    AssociatedControlID="txtAddPostal"
                                    runat="server"
                                    CssClass="w-100">
                                    <asp:TextBox type="text" class="w-100 text-input" ID="txtAddPostal" placeholder="Postal Code" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtAddPostal" Display="Dynamic"
                                        ErrorMessage="Postal Code is required." ForeColor="orange" ValidationGroup="EditAddressTemplate" />
                                </asp:Label>
                            </div>
                        </div>
                        <div class="row p-2 mr-1">
                            <h3 runat="server" class="text-warning mx-auto mt-2" style="width: fit-content;" id="msgAddress"></h3>
                            <div>
                                <asp:Button ID="btnSaveAddress" CssClass="btn btn-secondary mx-auto buttons" runat="server" Text="Add" OnClick="btnSaveAddress_Click" ValidationGroup="EditAddressTemplate" />
                                <asp:Button ID="btnCancelAddress" CssClass="btn btn-secondary mx-auto buttons" runat="server" Text="Cancel" OnClick="btnCancelAddress_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="row mt-3 ml-3">
                        <asp:Repeater ID="rptAddress" runat="server" OnItemDataBound="rptAddress_ItemDataBound">
                            <ItemTemplate>
                                <div class="row mt-2">
                                    <div class="col-2">
                                        <asp:Label
                                            Text="Address Name"
                                            AssociatedControlID="txtFullName"
                                            runat="server">
                                            <asp:TextBox CssClass="text-input disabledInput w-75" ID="txtFullName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FullName") %>' Enabled="false" />
                                        </asp:Label>
                                    </div>
                                    <div class="col">
                                        <asp:Label
                                            Text="Address"
                                            AssociatedControlID="txtAddress"
                                            runat="server">
                                            <div class="row m-0" runat="server">
                                                <asp:TextBox CssClass="text-input disabledInput" Width="250px" ID="txtAddress" runat="server" Enabled="false" />
                                            </div>
                                        </asp:Label>                                        
                                    </div>
                                    <div class="col-2">
                                        <asp:Label
                                            Text="City"
                                            AssociatedControlID="txtCity"
                                            runat="server">
                                            <div class="row m-0" runat="server">
                                                <asp:TextBox CssClass="text-input disabledInput w-75" ID="txtCity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "City").ToString() %>' Enabled="false" />
                                            </div>                                            
                                        </asp:Label>
                                    </div>
                                    <div class="col-1">
                                        <asp:Label
                                            Text="Province"
                                            AssociatedControlID="txtProvince"
                                            runat="server">
                                            <div class="row m-0" runat="server">
                                                <asp:TextBox CssClass="text-input disabledInput w-75" ID="txtProvince" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Province").ToString() %>' Enabled="false" />
                                            </div>                                            
                                        </asp:Label>
                                    </div>
                                    <div class="col-2">
                                        <asp:Label
                                            Text="Postal"
                                            AssociatedControlID="txtPostal"
                                            runat="server">
                                            <div class="row m-0" runat="server">
                                                <asp:TextBox CssClass="text-input disabledInput w-75" ID="txtPostal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PostalCode").ToString() %>' Enabled="false" />
                                            </div>                                            
                                        </asp:Label>
                                    </div>
                                    <div class="col d-flex align-items-center">
                                        <asp:LinkButton class="btn btn-secondary btn-sm mr-2 text-size-18" runat="server" CommandArgument='<%#Eval("AddressId") %>' OnClientClick="ShowTab();" CausesValidation="False" UseSubmitBehavior="false" OnCommand="EditAddress" Text="Edit" />
                                        <asp:LinkButton class="btn btn-secondary btn-sm text-size-18" CommandArgument='<%#Eval("AddressId") %>' runat="server" OnClientClick="if (!confirm('Are you sure you want to remove?'));" OnCommand="RemoveAddress" Text="Remove" />
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </asp:View>

                <!-- Payment -->
                <asp:View ID="tabPayment" runat="server">
                    <div class="row">
                        <div class="row">
                            <div class="col-12 d-flex align-items-center">
                                <asp:LinkButton class="btn btn-secondary btn-sm text-size-25" runat="server" ID="btnAddCard" ClientIDMode="Static" OnClientClick="ShowNewTab();" CausesValidation="False" UseSubmitBehavior="false" OnCommand="OpenAddCard" Text="Add Credit Card" />
                            </div>
                        </div>
                    </div>
                    <div class="row d-flex align-items-center">
                        <asp:Label Text="" ID="cardMessage" CssClass="text-lg-center" runat="server" />
                    </div>
                    <div class="row my-4">
                        <div class="row border border-secondary rounded pt-2" id="newCardForm">
                            <div class="col">
                                <asp:Label
                                    Text="Display Name"
                                    AssociatedControlID="txtAddDisplayName"
                                    runat="server">
                                    <asp:TextBox class="text-input" ClientIDMode="Static" ID="txtAddDisplayName" runat="server" placeholder="Display name on card"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAddDisplayName" Display="Dynamic"
                                        ErrorMessage="Display name on card is required" ForeColor="red" ValidationGroup="EditCardTemplate" />
                                </asp:Label>
                            </div>
                            <div class="col">
                                <asp:Label
                                    Text="Card Number"
                                    AssociatedControlID="txtAddCardNumber"
                                    runat="server">
                                    <asp:TextBox class="text-input" ClientIDMode="Static" ID="txtAddCardNumber" runat="server" placeholder="Card number"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAddCardNumber" Display="Dynamic"
                                        ErrorMessage="Card number is required" ForeColor="red" ValidationGroup="EditCardTemplate" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        ErrorMessage="Valid card number is required"
                                        ValidationExpression="(^4[0-9]{12}(?:[0-9]{3})?$)|(^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$)|(3[47][0-9]{13})|(^3(?:0[0-5]|[68][0-9])[0-9]{11}$)|(^6(?:011|5[0-9]{2})[0-9]{12}$)|(^(?:2131|1800|35\d{3})\d{11}$)"
                                        ControlToValidate="txtAddCardNumber"
                                        ForeColor="red"
                                        ValidationGroup="EditCardTemplate" />
                                </asp:Label>
                            </div>
                            <div class="col">
                                <asp:Label
                                    Text="Expiry Date"
                                    AssociatedControlID="expireAddDateDiv"
                                    runat="server">
                                    <div class="row m-0" runat="server" id="expireAddDateDiv">
                                        <asp:DropDownList class="w-45 text-input btn btn-secondary btn-sm dropdown-toggle gender-dropdown" ID="addExpireMonth" runat="server">
                                            <asp:ListItem Text="1" Value="1" />
                                            <asp:ListItem Text="2" Value="2" />
                                            <asp:ListItem Text="3" Value="3" />
                                            <asp:ListItem Text="4" Value="4" />
                                            <asp:ListItem Text="5" Value="5" />
                                            <asp:ListItem Text="6" Value="6" />
                                            <asp:ListItem Text="7" Value="7" />
                                            <asp:ListItem Text="8" Value="8" />
                                            <asp:ListItem Text="9" Value="9" />
                                            <asp:ListItem Text="10" Value="10" />
                                            <asp:ListItem Text="11" Value="11" />
                                            <asp:ListItem Text="12" Value="12" />
                                        </asp:DropDownList>
                                        <span style="margin: 0 5px; font-size: 25px;">/</span>
                                        <asp:DropDownList class="w-45 text-input btn btn-secondary btn-sm dropdown-toggle gender-dropdown" ID="addExpireYear" runat="server">
                                            <asp:ListItem Text="22" Value="2022" />
                                            <asp:ListItem Text="23" Value="2023" />
                                            <asp:ListItem Text="24" Value="2024" />
                                            <asp:ListItem Text="25" Value="2025" />
                                            <asp:ListItem Text="26" Value="2026" />
                                            <asp:ListItem Text="27" Value="2027" />
                                            <asp:ListItem Text="28" Value="2028" />
                                            <asp:ListItem Text="29" Value="2029" />
                                            <asp:ListItem Text="30" Value="2030" />
                                            <asp:ListItem Text="31" Value="2031" />
                                            <asp:ListItem Text="32" Value="2032" />
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="addExpireMonth" Display="Dynamic"
                                            ErrorMessage="Month is required" ForeColor="red" ValidationGroup="EditCardTemplate" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                            ErrorMessage="Valid expire month is required"
                                            ValidationExpression="^(0?[1-9]|1[0-2])$"
                                            ControlToValidate="addExpireMonth"
                                            ForeColor="red"
                                            ValidationGroup="EditCardTemplate" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="addExpireYear" Display="Dynamic"
                                            ErrorMessage="Year is required" ForeColor="red" ValidationGroup="EditCardTemplate" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                            ErrorMessage="Valid expire year is required"
                                            ValidationExpression="^([0-9]{4}|[0-9]{2})$"
                                            ControlToValidate="addExpireYear"
                                            ForeColor="red"
                                            ValidationGroup="EditCardTemplate" />
                                    </div>
                                </asp:Label>
                            </div>
                            <div class="col">
                                <asp:Label
                                    Text="CVV"
                                    AssociatedControlID="txtAddCvv"
                                    runat="server">
                                    <asp:TextBox class="text-input" ClientIDMode="Static" ID="txtAddCvv" runat="server" placeholder="CVV"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtAddCvv" Display="Dynamic"
                                        ErrorMessage="CVV is required" ForeColor="red" ValidationGroup="EditCardTemplate" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                        ErrorMessage="Valid CVV is required"
                                        ValidationExpression="^[0-9]{3,4}$"
                                        ControlToValidate="txtAddCvv"
                                        ForeColor="red"
                                        ValidationGroup="EditCardTemplate" />
                                </asp:Label>
                            </div>
                            <div class="col d-flex align-items-center">
                                <asp:LinkButton class="btn btn-secondary btn-sm mr-2 text-size-25" runat="server" ID="btnAddNewCard" ValidationGroup="EditCardTemplate" ClientIDMode="Static" OnCommand="AddNewCard" Text="Add" />
                                <a href="#" class="btn btn-secondary btn-sm text-size-25" id="btnCancelAddingCard">Cancel</a>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <asp:Repeater ID="rptCreditCard" runat="server">
                            <ItemTemplate>
                                <div class="row">
                                    <div class="col">
                                        <asp:Label
                                            Text="Display Name"
                                            AssociatedControlID="txtDisplayName"
                                            runat="server">
                                            <asp:TextBox CssClass="text-input disabledInput" ID="txtDisplayName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DisplayName") %>' Enabled="false"></asp:TextBox>
                                        </asp:Label>
                                    </div>
                                    <div class="col">
                                        <asp:Label
                                            Text="Card Number"
                                            AssociatedControlID="txtCardNumber"
                                            runat="server">
                                            <asp:TextBox CssClass="text-input disabledInput" ID="txtCardNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CardNumber").ToString() %>' Enabled="false"></asp:TextBox>
                                        </asp:Label>
                                    </div>
                                    <div class="col">
                                        <asp:Label
                                            Text="Expiry Date"
                                            AssociatedControlID="expireDateDiv"
                                            runat="server">
                                            <div class="row m-0" runat="server" id="expireDateDiv">
                                                <asp:TextBox CssClass="text-input disabledInput w-45" ClientIDMode="Static" Enabled="false" ID="expireMonth" runat="server" placeholder="Month" Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "Expiry")).Month.ToString() %>' type="number"></asp:TextBox>
                                                <span style="margin: 0 5px; font-size: 25px;">/</span>
                                                <asp:TextBox CssClass="text-input disabledInput w-45" ClientIDMode="Static" Enabled="false" ID="expireYear" runat="server" placeholder="Year" Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "Expiry")).Year.ToString().Substring(2,2) %>' type="number"></asp:TextBox>
                                            </div>
                                        </asp:Label>
                                    </div>
                                    <div class="col">
                                        <asp:Label
                                            Text="CVV"
                                            AssociatedControlID="txtCvv"
                                            runat="server">
                                            <asp:TextBox CssClass="text-input disabledInput" ID="txtCvv" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CVV").ToString() %>' Enabled="false"></asp:TextBox>
                                        </asp:Label>
                                    </div>
                                    <div class="col d-flex align-items-center">
                                        <asp:LinkButton class="btn btn-secondary btn-sm mt-3 mr-2 text-size-25" runat="server" CommandArgument='<%#Eval("CreditCardId") %>' OnClientClick="ShowTab();" CausesValidation="False" UseSubmitBehavior="false" OnCommand="EditCard" Text="Edit" />
                                        <asp:LinkButton class="btn btn-secondary btn-sm mt-3 text-size-25" CommandArgument='<%#Eval("CreditCardId") %>' runat="server" OnClientClick="if (!confirm('Are you sure you want to remove?'));" OnCommand="RemoveCard" Text="Remove" />
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </asp:View>
            </asp:MultiView>
        </div>
    </div>

    <script>
        $('#btnCancelAddingCard').on('click', function () {
            $('#txtAddDisplayName').val("");
            $('#txtAddCardNumber').val("");
            $('#addExpireMonth').val("");
            $('#addExpireYear').val("");
            $('#txtAddCvv').val("");
            $('#newCardForm').hide();
            $('#btnAddNewCard').html('Add');
        });
        function ShowNewTab() {
            $('#txtAddDisplayName').val("");
            $('#txtAddCardNumber').val("");
            $('#addExpireMonth').val("");
            $('#addExpireYear').val("");
            $('#txtAddCvv').val("");
            $('#newCardForm').hide();
            ShowTab();
        }
        function ShowTab() {
            $('#newCardForm').show();
        }
    </script>
</asp:Content>
