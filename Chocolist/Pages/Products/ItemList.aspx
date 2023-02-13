<%@ Page Title="Item List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItemList.aspx.cs" Inherits="Chocolist.Pages.Products.ItemList" %>

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
    </style>

    <div class="row m-1">
        <div class="mb-4">
            <h3 class="mx-auto" style="width: fit-content; float: left; color: darkgrey;">Admin Panel Item List</h3>
        </div>
    </div>
    <div class="d-flex justify-content-end">
        <div class="dropdown">
            <button class="btn btn-primary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" style="background-color: #CA5500; border: none; box-shadow: rgb(0 0 0 / 0.40) 5px 5px 10px;"><i class="fa-solid fa-plus"></i></button>
            <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                <a class="dropdown-item" href="CreateItem.aspx">Item</a>
                <a class="dropdown-item" href="CreateEvent.aspx">Event</a>
            </div>
        </div>
    </div>
    <div class="panel">
        <asp:Menu ID="ItemListMenu" Orientation="Horizontal" StaticMenuItemStyle-CssClass="tab" Font-Size="X-Large"
            StaticSelectedStyle-CssClass="selectedTab" StaticSelectedStyle-ForeColor="White" StaticSelectedStyle-Font-Bold="true"
            StaticMenuItemStyle-HorizontalPadding="20px"
            CssClass="tabs" runat="server" OnMenuItemClick="ItemListMenuClick">
            <Items>
                <asp:MenuItem Text="Games" Value="0" Selected="true"></asp:MenuItem>
                <asp:MenuItem Text="Merchandise" Value="1"></asp:MenuItem>
                <asp:MenuItem Text="Events" Value="2"></asp:MenuItem>
            </Items>
        </asp:Menu>

        <div class="tabContents">
            <asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">
                <!-- Game info -->
                <asp:View ID="tabGame" runat="server">
                    <div class="row">
                        <asp:Repeater ID="rptGame" runat="server">
                            <ItemTemplate>
                                <!-- use this card (div) as template for insertion -->
                                <div class="col-3">
                                    <div class="card p-1 mb-2 game-card">
                                        <asp:HyperLink class="btn btn-secondary btn-sm float-right add-to-cart mr-1" ID="HyperLink2" runat="server" Text="Link" NavigateUrl='<%# "~/pages/Products/ModifyItem.aspx?itemId=" + Eval("itemId") %>'>
                                <img class="card-img-top game-img" src="/images/1.jpg" alt="">
                                        </asp:HyperLink>
                                        <div class="card-body p-0">
                                            <h5 class="card-title my-1"><%#Eval("item.name") %></h5>
                                            <div class="game-category"><%#Eval("GenreName") %></div>
                                            <div class="game-platform"><%#Eval("PlatformName").ToString()%></div>
                                            <span class="game-price">$<%#Eval("item.price", "{0:0.00}") %></span>
                                            <asp:LinkButton class="btn btn-secondary btn-sm float-right add-to-cart" CommandArgument='<%#Eval("itemId") %>' OnClientClick="if (!confirm('Are you sure you want delete?')) return false;" runat="server" OnCommand="RemoveGame" Text="Remove" />
                                            <asp:HyperLink class="btn btn-secondary btn-sm float-right add-to-cart mr-1" ID="HyperLink1" runat="server" Text="Link" NavigateUrl='<%# "~/pages/Products/ModifyItem.aspx?itemId=" + Eval("itemId") %>'>Edit</asp:HyperLink>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </asp:View>

                <!-- Merchandise Info -->
                <asp:View ID="tabMerch" runat="server">
                    <div class="row">
                        <asp:Repeater ID="rptMerchandise" runat="server">
                            <ItemTemplate>
                                <!-- use this card (div) as template for insertion -->
                                <div class="col-3">
                                    <div class="card p-1 mb-2 game-card">
                                        <asp:HyperLink class="btn btn-secondary btn-sm float-right add-to-cart mr-1" ID="HyperLink3" runat="server" Text="Link" NavigateUrl='<%# "~/pages/Products/ModifyItem.aspx?itemId=" + Eval("itemId") %>'>
                                            <img class="card-img-top game-img" src="/images/merchandise2.jpg" alt="">
                                        </asp:HyperLink>
                                        <div class="card-body p-0">
                                            <h5 class="card-title my-1"><%#Eval("item.name") %></h5>
                                            <div class="game-platform">Colour:<canvas width="16" height="16" style='<%# "border:2px solid #000000; background:" + Eval("CanvasStyle") + ";" %>>'</canvas></div>
                                            <div class="game-platform">Size:<%#Eval("Size").ToString()%></div>
                                            <span class="game-price">$<%#Eval("item.price", "{0:0.00}") %></span>
                                            <asp:LinkButton class="btn btn-secondary btn-sm float-right add-to-cart" CommandArgument='<%#Eval("itemId") %>' OnClientClick="if (!confirm('Are you sure you want delete?')) return false;" runat="server" OnCommand="RemoveMerchandise" Text="Remove" />
                                            <asp:HyperLink class="btn btn-secondary btn-sm float-right add-to-cart mr-1" ID="HyperLink4" runat="server" Text="Link" NavigateUrl='<%# "~/pages/Products/ModifyItem.aspx?itemId=" + Eval("itemId") %>'>Edit</asp:HyperLink>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </asp:View>

                <!-- Event Info -->
                <asp:View ID="tabEvent" runat="server">
                    <div class="row">
                        <asp:Repeater ID="rptEvent" runat="server">
                            <ItemTemplate>
                                <!-- use this card (div) as template for insertion -->
                                <div class="col-3">
                                    <div class="card p-1 mb-2 game-card">
                                        <asp:HyperLink class="btn btn-secondary btn-sm float-right add-to-cart mr-1" ID="hLinkEvent2" runat="server" Text="Link" NavigateUrl='<%# "~/pages/Products/ModifyEvent.aspx?eventId=" + Eval("eventId") %>'>
                                <img class="card-img-top game-img" src="/images/1.jpg" alt="">
                                        </asp:HyperLink>
                                        <div class="card-body p-0">
                                            <h5 class="card-title my-1"><%#Eval("title") %></h5>
                                            <div class="game-category">Shooter/First Person</div>
                                            <span class="game-price">$<%#Eval("price", "{0:0.00}") %></span>
                                            <asp:LinkButton class="btn btn-secondary btn-sm float-right add-to-cart" CommandArgument='<%#Eval("eventId") %>' OnClientClick="if (!confirm('Are you sure you want delete?')) return false;" runat="server" OnCommand="RemoveEvent" Text="Remove" />
                                            <asp:HyperLink class="btn btn-secondary btn-sm float-right add-to-cart mr-1" ID="hLinkEvent1" runat="server" Text="Link" NavigateUrl='<%# "~/pages/Products/ModifyEvent.aspx?eventId=" + Eval("eventId") %>'>Edit</asp:HyperLink>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </asp:View>
            </asp:MultiView>
        </div>
    </div>
</asp:Content>


