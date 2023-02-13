<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EventDetail.aspx.cs" Inherits="Chocolist.Pages.Products.EventDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link href="Preview.css" rel="stylesheet" />
    <!-- TODO:Title should change dynamically -->
    <div>
        <div class="mb-4">
            <h1 class="mx-auto" style="width: fit-content;" runat="server" id="lblTitle"></h1>
        </div>
        <div>
            <div class="row">
                <div class="col-12">
                    <div class="row">
                        <div class="col-12 p-0">
                            <div class="card p-1 preview-card">
                                <%--<img class="card-img-top preview-img" src="/images/1.jpg" alt="Poster">--%>
                                <div class="card-body p-0">
                                    <p class="my-2" runat="server" id="lblDescription"></p>
                                    <div class="event-detail">
                                        <div id="divEvent" runat="server">
                                            <div class="row m-1 mb-3">
                                                <asp:Label Text="Event Title" runat="server" Style="color: darkgrey; font-style: italic; margin-left: 5px; margin-bottom: 2px;" />
                                                <asp:Label class="w-100" ID="txtEventTitle" Style="color: white; margin-left: 10px; margin-bottom: 2px;" runat="server" />
                                            </div>
                                            <div class="row m-1 mb-3">
                                                <asp:Label Text="Event Description" runat="server" Style="color: darkgrey; font-style: italic; margin-left: 5px; margin-bottom: 2px;" />
                                                <asp:Label class="w-100" ID="txtEventDescription" Style="color: white; margin-left: 10px; margin-bottom: 2px;" runat="server" />
                                            </div>
                                            <div class="row m-1 mb-3">
                                                <asp:Label Text="Event Price" runat="server" Style="color: darkgrey; font-style: italic; margin-left: 5px; margin-bottom: 2px;" />
                                                <asp:Label class="w-100" ID="txtEventPrice" Style="color: white; margin-left: 10px; margin-bottom: 2px;" runat="server" />
                                            </div>
                                            <div class="row m-1 mb-4">
                                                <asp:Label class="w-100" Text="Start Date" runat="server" Style="color: darkgrey; font-style: italic; margin-left: 5px; margin-bottom: 2px;" />
                                                <asp:Label ID="txtStartDate" Style="color: white; margin-left: 10px; margin-bottom: 2px; display: inline-block;" runat="server" />
                                                <asp:Label ID="txtStartTime" Style="color: white; margin-left: 10px; margin-bottom: 2px; display: inline-block;" runat="server" />
                                            </div>
                                            <div class="row m-1 mb-4">
                                                <asp:Label class="w-100" Text="End Date" runat="server" Style="color: darkgrey; font-style: italic; margin-left: 5px; margin-bottom: 2px;" />
                                                <asp:Label ID="txtEndDate" Style="color: white; margin-left: 10px; margin-bottom: 2px; display: inline-block;" runat="server" />
                                                <asp:Label ID="txtEndTime" Style="color: white; margin-left: 10px; margin-bottom: 2px; display: inline-block;" runat="server" />
                                            </div>
                                            <div class="row m-1 mb-4">
                                                <asp:Label Text="Location" runat="server" Style="color: darkgrey; font-style: italic; margin-left: 5px; margin-bottom: 2px;" />
                                                <asp:Label class="w-100" ID="txtEventLocation" Style="color: white; margin-left: 10px; margin-bottom: 2px;" runat="server" />
                                            </div>
                                            <div class="row m-1 mb-4">
                                                <asp:Label Text="Attendees" runat="server" Style="color: darkgrey; font-style: italic; margin-left: 5px; margin-bottom: 2px;" />
                                                <asp:Label class="w-100" ID="txtAttendance" Style="color: white; font-weight: bold; margin-left: 10px; margin-bottom: 2px;" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row mt-3">
                        <div class="col p-0">
                            <div class="card p-1 preview-price-card">
                                <div class="row">
                                    <div class="col-8">
                                        <div id="div1" runat="server">
                                            <div>
                                                <asp:Label ID="ResultMessage" class="float-right" ForeColor="Green" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-4">
                                        <div>
                                            <asp:Button ID="btnRSVP" class="btn btn-info mx-auto mb-2 buttons" runat="server" Text="RSVP" OnClick="ButtonRSVP_Click" CausesValidation="False" />
                                            <asp:Button ID="btnCancelRSVP" class="btn btn-info mx-auto mb-2 buttons" runat="server" Text="Cancel RSVP" OnClick="ButtonCancelRSVP_Click" CausesValidation="False" />
                                            <asp:Button ID="btnBack" class="btn btn-secondary mx-auto mb-2 buttons" runat="server" Text="Back" OnClick="btnEventBack_Click" CausesValidation="False" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
