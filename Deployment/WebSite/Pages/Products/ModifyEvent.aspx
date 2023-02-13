<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModifyEvent.aspx.cs" Inherits="Chocolist.Pages.Products.ModifyEvent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="ModifyItem.css" rel="stylesheet" />

    <form class="container p-2" id="frmCreateEvent" style="background-color: #282B2E; border-radius: 5px; box-shadow: rgb(0 0 0 / 0.40) 5px 5px 10px;">
        <div class="row m-1">
            <div class="mb-4">
                <h3 class="mx-auto" style="width: fit-content; float: left; color: darkgrey; padding-left: 20px;">Edit Event</h3>
            </div>
        </div>
        <div class="row m-1">
            <div class="col-4">
                <div id="divEventInput" runat="server">
                    <div class="row m-1 mb-3">
                        <asp:Label Text="Event Title" runat="server" Style="color: darkgrey; font-style: italic; margin-left: 5px; margin-bottom: 2px;" />
                        <asp:TextBox type="text" class="w-100 text-input" ID="txtEventTitle" placeholder="Event Title" runat="server" />
                    </div>
                    <div class="row m-1 mb-3">
                        <asp:Label Text="Event Description" runat="server" Style="color: darkgrey; font-style: italic; margin-left: 5px; margin-bottom: 2px;" />
                        <asp:TextBox TextMode="MultiLine" Rows="5" class="w-100 text-input" ID="txtEventDescription" placeholder="Event Description" runat="server"></asp:TextBox>
                    </div>
                    <div class="row m-1 mb-3">
                        <asp:Label Text="Event Price" runat="server" Style="color: darkgrey; font-style: italic; margin-left: 5px; margin-bottom: 2px;" />
                        <asp:TextBox type="text" class="w-100 text-input" ID="txtEventPrice" placeholder="$0.00" runat="server"></asp:TextBox>
                    </div>
                    <div class="row m-1 mb-4">
                        <asp:Label Text="Start Date" runat="server" Style="color: darkgrey; font-style: italic; margin-left: 5px; margin-bottom: 2px;" />
                        <asp:TextBox class="w-100 text-input" ID="txtStartDate" runat="server" TextMode="Date" />
                        <asp:TextBox class="w-100 text-input" ID="txtStartTime" runat="server" TextMode="Time" />
                    </div>
                    <div class="row m-1 mb-4">
                        <asp:Label Text="End Date" runat="server" Style="color: darkgrey; font-style: italic; margin-left: 5px; margin-bottom: 2px;" />
                        <asp:TextBox class="w-100 text-input" ID="txtEndDate" runat="server" TextMode="Date" />
                        <asp:TextBox class="w-100 text-input" ID="txtEndTime" runat="server" TextMode="Time" />
                    </div>
                    <div class="row m-1 mb-4">
                        <asp:Label Text="Location" runat="server" Style="color: darkgrey; font-style: italic; margin-left: 5px; margin-bottom: 2px;" />
                        <asp:TextBox type="text" class="w-100 text-input" ID="txtEventLocation" placeholder="Online" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-8">
                <div class="row m-1 mb-2 cover-card">
                    <a href="#" class="btn btn-secondary mx-auto mb-2 buttons">Add Cover</a>
                    <img src="/images/13.jpg" class="game-images" />
                </div>
                <div class="row m-1 mb-4 cover-card">
                    <a href="#" class="btn btn-secondary mx-auto mb-2 buttons">Add Screenshot</a>
                    <div class="row m-1">
                        <div class="col-4">
                            <img src="/images/13.jpg" class="game-images" />
                        </div>
                        <div class="col-4">
                            <img src="/images/13.jpg" class="game-images" />
                        </div>
                        <div class="col-4">
                            <img src="/images/13.jpg" class="game-images" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-8">
                        <div id="divResult" runat="server">
                            <div>
                                <asp:Label ID="ResultMessage" class="float-right" ForeColor="Green" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-4">
                        <div id="divButtonArea" class="row m-1 mb-4">
                            <asp:Button ID="btnEventSave" class="btn btn-secondary mx-auto mb-2 buttons" runat="server" Text="Save" OnClick="btnEventSave_Click" />
                            <asp:Button ID="btnEventBack" class="btn btn-secondary mx-auto mb-2 buttons" runat="server" Text="Back" OnClick="btnEventBack_Click" CausesValidation="False" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
