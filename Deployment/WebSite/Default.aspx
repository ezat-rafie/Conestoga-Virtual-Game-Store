<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Chocolist._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron" style="padding: 0; margin: 0;">

        <script type="text/javascript">
            window.jssor_1_slider_init = function () {

                var jssor_1_options = {
                    $AutoPlay: 1,
                    $ArrowNavigatorOptions: {
                        $Class: $JssorArrowNavigator$
                    },
                    $BulletNavigatorOptions: {
                        $Class: $JssorBulletNavigator$,
                        $SpacingX: 20,
                        $SpacingY: 20
                    }
                };

                var jssor_1_slider = new $JssorSlider$("jssor_1", jssor_1_options);

                /*#region responsive code begin*/

                var MAX_WIDTH = 3000;

                function ScaleSlider() {
                    var containerElement = jssor_1_slider.$Elmt.parentNode;
                    var containerWidth = containerElement.clientWidth;

                    if (containerWidth) {

                        var expectedWidth = Math.min(MAX_WIDTH || containerWidth, containerWidth);

                        jssor_1_slider.$ScaleWidth(expectedWidth);
                    }
                    else {
                        window.setTimeout(ScaleSlider, 30);
                    }
                }

                ScaleSlider();

                $Jssor$.$AddEvent(window, "load", ScaleSlider);
                $Jssor$.$AddEvent(window, "resize", ScaleSlider);
                $Jssor$.$AddEvent(window, "orientationchange", ScaleSlider);
                /*#endregion responsive code end*/
            };
        </script>
        <style>
            /* jssor slider loading skin spin css */
            .jssorl-009-spin img {
                animation-name: jssorl-009-spin;
                animation-duration: 1.6s;
                animation-iteration-count: infinite;
                animation-timing-function: linear;
            }

            @keyframes jssorl-009-spin {
                from {
                    transform: rotate(0deg);
                }

                to {
                    transform: rotate(360deg);
                }
            }


            /*jssor slider bullet skin 132 css*/
            .jssorb132 {
                position: absolute;
            }

                .jssorb132 .i {
                    position: absolute;
                    cursor: pointer;
                }

                    .jssorb132 .i .b {
                        fill: #fff;
                        fill-opacity: 0.8;
                        stroke: #000;
                        stroke-width: 1600;
                        stroke-miterlimit: 10;
                        stroke-opacity: 0.7;
                    }

                    .jssorb132 .i:hover .b {
                        fill: #000;
                        fill-opacity: .7;
                        stroke: #fff;
                        stroke-width: 2000;
                        stroke-opacity: 0.8;
                    }

                .jssorb132 .iav .b {
                    fill: #000;
                    stroke: #fff;
                    stroke-width: 2400;
                    fill-opacity: 0.8;
                    stroke-opacity: 1;
                }

                .jssorb132 .i.idn {
                    opacity: 0.3;
                }

            .jssora051 {
                display: block;
                position: absolute;
                cursor: pointer;
            }

                .jssora051 .a {
                    fill: none;
                    stroke: #fff;
                    stroke-width: 360;
                    stroke-miterlimit: 10;
                }

                .jssora051:hover {
                    opacity: .8;
                }

                .jssora051.jssora051dn {
                    opacity: .5;
                }

                .jssora051.jssora051ds {
                    opacity: .3;
                    pointer-events: none;
                }


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
                float: none !important;
            }

            .tab {
                padding: 5px;
                color: gray;
            }

            .selectedTab {
                border-top: solid 1px white;
            }

            .tabContents {
                padding: 0px 10px;
            }

            .panel a:hover {
                color: white;
            }

            .panel a.static.selected {
                color: #FF6B00 !important;
            }

            .searchButton {
                border: 1px solid #6464647d;
                background-color: #2e2e2e;
            }
        </style>

        <div id="jssor_1"
            style="position: relative; margin: 0 auto; top: 0px; left: 0px; width: 1383px; height: 400px; overflow: hidden; visibility: hidden; border-radius: 5px; box-shadow: rgb(0 0 0 / 0.40) 5px 5px 10px; border: 1px solid #62626273">
            <div data-u="slides"
                style="cursor: default; position: relative; top: 0px; left: 0px; width: 1383px; height: 400px; overflow: hidden;">
                <div data-idle="5000">
                    <img data-u="image" src="images/banner/1.jpg" style="object-fit: cover;" />
                </div>
                <div data-idle="5000">
                    <img data-u="image" src="images/banner/2.jpg" style="object-fit: cover;" />
                </div>
                <div data-idle="5000">
                    <img data-u="image" src="images/banner/3.jpg" style="object-fit: cover;" />
                </div>
                <div data-idle="5000">
                    <img data-u="image" src="images/banner/4.jpg" style="object-fit: cover;" />
                </div>
                <div data-idle="5000">
                    <img data-u="image" src="images/banner/5.jpg" style="object-fit: cover;" />
                </div>
                <div data-idle="5000">
                    <img data-u="image" src="images/banner/6.jpg" style="object-fit: cover;" />
                </div>
                <div data-idle="5000">
                    <img data-u="image" src="images/banner/7.jpg" style="object-fit: cover;" />
                </div>
            </div>
            <!-- Bullet Navigator -->
            <div data-u="navigator" class="jssorb132" style="position: absolute; bottom: 24px; right: 16px;" data-autocenter="1"
                data-scale="0.5" data-scale-bottom="0.75">
                <div data-u="prototype" class="i" style="width: 12px; height: 12px;">
                    <svg viewbox="0 0 16000 16000" style="position: absolute; top: 0; left: 0; width: 100%; height: 100%;">
                        <circle class="b" cx="8000" cy="8000" r="5800"></circle>
                    </svg>
                </div>
            </div>
            <!-- Arrow Navigator -->
            <div data-u="arrowleft" class="jssora051" style="width: 55px; height: 55px; top: 0px; left: 25px;" data-autocenter="2"
                data-scale="0.75" data-scale-left="0.75">
                <svg viewbox="0 0 16000 16000" style="position: absolute; top: 0; left: 0; width: 100%; height: 100%;">
                    <polyline class="a" points="11040,1920 4960,8000 11040,14080 "></polyline>
                </svg>
            </div>
            <div data-u="arrowright" class="jssora051" style="width: 55px; height: 55px; top: 0px; right: 25px;"
                data-autocenter="2" data-scale="0.75" data-scale-right="0.75">
                <svg viewbox="0 0 16000 16000" style="position: absolute; top: 0; left: 0; width: 100%; height: 100%;">
                    <polyline class="a" points="4960,1920 11040,8000 4960,14080 "></polyline>
                </svg>
            </div>
        </div>
        <script type="text/javascript">jssor_1_slider_init();
        </script>
    </div>

    <div class="container filter-bar">
        <div class="row" style="margin: 0px 11%;">
            <div class="col">
                <div class="dropdown show">
                    <asp:DropDownList class="w-100 text-input" CssClass="btn btn-secondary btn-sm dropdown-toggle filter-buttons" ID="ddlPrice" runat="server" AutoPostBack="true">
                        <asp:ListItem Text="Sort by price" Value="All" />
                        <asp:ListItem Text="Low to high" Value="low" />
                        <asp:ListItem Text="High to low" Value="high" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col">
                <div class="dropdown show">
                    <asp:DropDownList class="w-100 text-input" CssClass="btn btn-secondary btn-sm dropdown-toggle filter-buttons" ID="ddlGenre" runat="server" AutoPostBack="true">
                        <asp:ListItem Text="Genre" Value="All" />
                        <asp:ListItem Text="Action" Value="1" />
                        <asp:ListItem Text="Adventure" Value="2" />
                        <asp:ListItem Text="Strategy" Value="3" />
                        <asp:ListItem Text="Family" Value="4" />
                        <asp:ListItem Text="Puzzle" Value="5" />
                        <asp:ListItem Text="Sports" Value="6" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col">
                <div class="dropdown show">
                    <asp:DropDownList class="w-100 text-input" CssClass="btn btn-secondary btn-sm dropdown-toggle filter-buttons" ID="ddlPlatform" runat="server" AutoPostBack="true">
                        <asp:ListItem Text="Platform" Value="All" />
                        <asp:ListItem Text="Macintosh" Value="1" />
                        <asp:ListItem Text="Windows" Value="2" />
                        <asp:ListItem Text="Nintendo" Value="3" />
                        <asp:ListItem Text="Playstation" Value="4" />
                        <asp:ListItem Text="XBox" Value="5" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col">
                <div class="dropdown show">
                    <asp:DropDownList class="w-100 text-input" CssClass="btn btn-secondary btn-sm dropdown-toggle filter-buttons" ID="ddlPriceRange" runat="server" AutoPostBack="true">
                        <asp:ListItem Text="Price Range" Value="All" />
                        <asp:ListItem Text="0 to 50" Value="50" />
                        <asp:ListItem Text="51 to 100" Value="100" />
                        <asp:ListItem Text="100+" Value="100+" />
                    </asp:DropDownList>
                </div>
            </div>
        </div>
    </div>

    <div class="panel">
        <asp:Menu ID="DefaultMain" Orientation="Horizontal" StaticMenuItemStyle-CssClass="tab" Font-Size="X-Large"
            StaticSelectedStyle-CssClass="selectedTab" StaticSelectedStyle-ForeColor="White" StaticSelectedStyle-Font-Bold="true"
            StaticMenuItemStyle-HorizontalPadding="20px"
            StaticMenuItemStyle-VerticalPadding="10px"
            CssClass="tabs d-flex justify-content-center" runat="server" OnMenuItemClick="DefaultMain_MenuItemClick">
            <Items>
                <asp:MenuItem Text="Games" Value="0" Selected="true"></asp:MenuItem>
                <asp:MenuItem Text="Merch" Value="1"></asp:MenuItem>
                <asp:MenuItem Text="Events" Value="2"></asp:MenuItem>
            </Items>
        </asp:Menu>
        <div class="input-group" id="searchBar" style="position: absolute; top: 2%; right: calc(50% - 150px); width: 300px;">
            <asp:TextBox CssClass="text-input form-control" ID="txtSearch" runat="server" placeholder="Search"></asp:TextBox>
            <div class="input-group-append">
                <asp:LinkButton class="btn btn-outline-secondary text-white searchButton" runat="server" ID="btnSearch" CausesValidation="False" UseSubmitBehavior="false" OnCommand="SearchPage"><i class="fa-solid fa-magnifying-glass" style="margin-top: 6px;"></i></asp:LinkButton>
            </div>
        </div>
        <div class="tabContents">
            <asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">
                <!-- Profile Info -->
                <asp:View ID="tabGame" runat="server">
                    <div class="container game-list">
                        <div class="row">
                            <div class="mb-4">
                                <asp:Label ID="gameSearchResult" runat="server" class="mx-auto" Style="width: fit-content; float: left; color: darkgray; font-size: large; padding-left: 20px; font-weight: bold;"></asp:Label>
                            </div>
                            <asp:Repeater ID="rptGame" runat="server">
                                <ItemTemplate>
                                    <!-- use this card (div) as template for insertion -->
                                    <div class="col-3">
                                        <div class="card p-1 mb-2 game-card">
                                            <asp:HyperLink class="btn btn-secondary btn-sm float-right add-to-cart mr-1" ID="HyperLink2" runat="server" Text="Link" NavigateUrl='<%# "~/pages/Products/Preview.aspx?itemId=" + Eval("itemId") %>'>
                                                <img class="card-img-top game-img" src="/images/1.jpg" alt=""/>
                                            </asp:HyperLink>
                                            <div class="card-body p-0">
                                                <h5 class="card-title my-1"><%#Eval("item.name") %></h5>
                                                <span class="game-rating"><%#Eval("Rating").ToString() %> <i class="fa-solid fa-star"></i></span>
                                                <div class="game-category">"<%#Eval("GenreName") %></div>
                                                <div class="game-platform"><%#Eval("PlatformName").ToString()%></div>
                                                <span class="game-price">$<%#Eval("item.price") %></span>
                                                <asp:LinkButton class="btn btn-secondary btn-sm float-right add-to-cart" runat="server" ID="btnAddToCart" OnClick='btnAddToCart_Click' CommandArgument='<%# Eval("itemId") %>'>Add To Cart</asp:LinkButton>
                                                <!--<a href="#" class="btn btn-secondary btn-sm float-right add-to-cart">Add to Cart</a> -->
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </asp:View>

                <!-- Address -->
                <asp:View ID="tabMerch" runat="server">
                    <div class="container game-list">
                        <div class="row">
                            <div class="mb-4">
                                <asp:Label ID="merchandiseSearchResult" runat="server" class="mx-auto" Style="width: fit-content; float: left; color: darkgray; font-size: large; padding-left: 20px; font-weight: bold;"></asp:Label>
                            </div>
                            <asp:Repeater ID="rptMerchandise" runat="server">
                                <ItemTemplate>
                                    <!-- use this card (div) as template for insertion -->
                                    <div class="col-3">
                                        <div class="card p-1 mb-2 game-card">
                                            <asp:HyperLink class="btn btn-secondary btn-sm float-right add-to-cart mr-1" ID="HyperLink2" runat="server" Text="Link" NavigateUrl='<%# "~/pages/Products/Preview.aspx?itemId=" + Eval("itemId") %>'>
                                                <img class="card-img-top game-img" src="/images/1.jpg" alt=""/>
                                            </asp:HyperLink>
                                            <div class="card-body p-0">
                                                <h5 class="card-title my-1"><%#Eval("item.name") %></h5>
                                                <span class="game-rating">4.8 <i class="fa-solid fa-star"></i></span>
                                                <div class="game-platform">Colour:<canvas width="16" height="16" style='<%# "border:2px solid #000000; background:" + Eval("CanvasStyle") + ";" %>>'</canvas></div>
                                                <div class="game-platform">Size:<%#Eval("Size").ToString()%></div>
                                                <span class="game-price">$<%#Eval("item.price") %></span>
                                                <asp:LinkButton class="btn btn-secondary btn-sm float-right add-to-cart" runat="server" ID="btnAddToCart" OnClick='btnAddToCart_Click' CommandArgument='<%# Eval("itemId") %>'>Add To Cart</asp:LinkButton>
                                                <!--<a href="#" class="btn btn-secondary btn-sm float-right add-to-cart">Add to Cart</a> -->
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </asp:View>

                <!-- Payment -->
                <asp:View ID="tabEvent" runat="server">
                    <div class="row">
                        <asp:Repeater ID="rptEvent" runat="server">
                            <ItemTemplate>
                                <!-- use this card (div) as template for insertion -->
                                <div class="col-3">
                                    <div class="card p-1 mb-2 game-card">
                                        <asp:HyperLink class="btn btn-secondary btn-sm float-right add-to-cart mr-1" ID="hLinkEvent2" runat="server" Text="Link" NavigateUrl='<%# "~/pages/Products/EventDetail.aspx?eventId=" + Eval("eventId") %>'>
                                            <img class="card-img-top game-img" src="/images/1.jpg" alt="">
                                        </asp:HyperLink>
                                        <div class="card-body p-0">
                                            <h5 class="card-title my-1"><%#Eval("title") %></h5>
                                            <span class="game-rating">4.8 <i class="fa-solid fa-star"></i></span>
                                            <!--<div class="game-category">Shooter/First Person</div> -->
                                            <span class="game-price">$<%#Eval("price", "{0:0.00}") %></span>
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
    <script type="text/javascript">
        function clickButton(e, buttonid) {
            var evt = e ? e : window.event;
            var bt = document.getElementById(buttonid);
            if (bt) {
                if (evt.keyCode == 13) {
                    bt.click();
                    return false;
                }
            }
        }
    </script>

</asp:Content>
