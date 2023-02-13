<%@ Page Title="Game Preview" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Preview.aspx.cs" Inherits="Chocolist.Pages.Products.Preview" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Preview.css" rel="stylesheet" />
    <!-- TODO:Title should change dynamically -->
    <div>
        <div class="mb-4">
            <h1 class="mx-auto" style="width: fit-content;" runat="server" id="lblTitle"></h1>
        </div>
        <div>
            <div class="row">
                <div class="col-9">
                    <div class="jumbotron" style="padding: 0; margin: 0; background-color: transparent; margin-top: -105px;">
                        <script type="text/javascript">
                            jQuery(document).ready(function ($) {
                                if (!$('#<%= reviewTextBox.ClientID %>').length)
                                    $('#ratingButtons').hide();
                                var jssor_1_options = {
                                    $AutoPlay: 1,
                                    $ArrowNavigatorOptions: {
                                        $Class: $JssorArrowNavigator$
                                    },
                                    $ThumbnailNavigatorOptions: {
                                        $Class: $JssorThumbnailNavigator$,
                                        $SpacingX: 5,
                                        $SpacingY: 5
                                    }
                                };

                                var jssor_1_slider = new $JssorSlider$("jssor_1", jssor_1_options);

                                //make sure to clear margin of the slider container element
                                jssor_1_slider.$Elmt.style.margin = "";

                                /*#region responsive code begin*/

                                /*
                                    parameters to scale jssor slider to fill parent container
                    
                                    MAX_WIDTH
                                        prevent slider from scaling too wide
                                    MAX_HEIGHT
                                        prevent slider from scaling too high, default value is original height
                                    MAX_BLEEDING
                                        prevent slider from bleeding outside too much, default value is 1
                                        0: contain mode, allow up to 0% to bleed outside, the slider will be all inside parent container
                                        1: cover mode, allow up to 100% to bleed outside, the slider will cover full area of parent container
                                        0.1: flex mode, allow up to 10% to bleed outside, this is better way to make full window slider, especially for mobile devices
                                */

                                var MAX_WIDTH = 3000;
                                var MAX_HEIGHT = 800;
                                var MAX_BLEEDING = 0;

                                function ScaleSlider() {
                                    var containerElement = jssor_1_slider.$Elmt.parentNode;
                                    var containerWidth = containerElement.clientWidth;

                                    if (containerWidth) {
                                        var originalWidth = jssor_1_slider.$OriginalWidth();
                                        var originalHeight = jssor_1_slider.$OriginalHeight();

                                        var containerHeight = containerElement.clientHeight || originalHeight;

                                        var expectedWidth = Math.min(MAX_WIDTH || containerWidth, containerWidth);
                                        var expectedHeight = Math.min(MAX_HEIGHT || containerHeight, containerHeight);

                                        //scale the slider to expected size
                                        jssor_1_slider.$ScaleSize(expectedWidth, expectedHeight, MAX_BLEEDING);

                                        //position slider at center in vertical orientation
                                        jssor_1_slider.$Elmt.style.top = ((containerHeight - expectedHeight) / 2) + "px";

                                        //position slider at center in horizontal orientation
                                        jssor_1_slider.$Elmt.style.left = ((containerWidth - expectedWidth) / 2) + "px";
                                    }
                                    else {
                                        window.setTimeout(ScaleSlider, 30);
                                    }
                                }

                                function OnOrientationChange() {
                                    ScaleSlider();
                                    window.setTimeout(ScaleSlider, 800);
                                }

                                ScaleSlider();

                                $(window).bind("load", ScaleSlider);
                                $(window).bind("resize", ScaleSlider);
                                $(window).bind("orientationchange", OnOrientationChange);
                                /*#endregion responsive code end*/
                            });
                        </script>
                        <style>
                            /*jssor slider arrow skin 053 css*/
                            .jssora053 {
                                display: block;
                                position: absolute;
                                cursor: pointer;
                            }

                                .jssora053 .a {
                                    fill: none;
                                    stroke: #fff;
                                    stroke-width: 640;
                                    stroke-miterlimit: 10;
                                }

                                .jssora053:hover {
                                    opacity: .8;
                                }

                                .jssora053.jssora053dn {
                                    opacity: .5;
                                }

                                .jssora053.jssora053ds {
                                    opacity: .3;
                                    pointer-events: none;
                                }

                            /*jssor slider thumbnail skin 061 css*/
                            .jssort061 .p {
                                position: absolute;
                                top: 0;
                                left: 0;
                                box-sizing: border-box;
                            }

                            .jssort061 .t {
                                position: absolute;
                                top: 0;
                                left: 0;
                                width: 100%;
                                height: 100%;
                                border: none;
                                opacity: .6;
                                border-radius: 5px;
                            }

                            .jssort061 .p:hover {
                                border-color: rgba(0,0,0,.6);
                            }

                                .jssort061 .pav, .jssort061 .p:hover.pdn {
                                    border-color: #000;
                                }

                                    .jssort061 .pav .t, .jssort061 .p:hover.pdn .t {
                                        opacity: 1;
                                    }
                        </style>
                        <div style="position: relative; top: 0; left: 0; width: 100%; height: 100%; overflow: hidden;">
                            <div id="jssor_1" style="position: relative; margin: 0 auto; top: 0px; left: 0px; width: 1300px; height: 900px; overflow: hidden; visibility: hidden; background-color: rgba(0,0,0,0);">
                                <div data-u="slides" style="cursor: default; position: relative; top: 0px; left: 0px; width: 1300px; height: 900px; overflow: hidden; border-radius: 10px; box-shadow: rgb(0 0 0 / 40%) 5px 5px 10px;">

                                    <div data-idle="5000">
                                        <img data-u="image" id="coverImage" runat="server" src="/images/placeholder.jpg" style="object-fit: cover;" />
                                        <img data-u="thumb" id="thumbImage" runat="server" src="/images/placeholder.jpg" />
                                    </div>
                                </div>
                                <a data-scale="0" href="https://www.jssor.com" style="display: none; position: absolute;">html slider</a>
                                <!-- Thumbnail Navigator -->
                                <div data-u="thumbnavigator" class="jssort061" style="position: absolute; left: 0px; bottom: 0px; width: 1024px; height: 100px;" data-autocenter="1" data-scale-bottom="0.75">
                                    <div data-u="slides">
                                        <div data-u="prototype" class="p" style="width: 190px; height: 90px;">
                                            <div data-u="thumbnailtemplate" class="t"></div>
                                        </div>
                                    </div>
                                </div>
                                <!-- Arrow Navigator -->
                                <div data-u="arrowleft" class="jssora053" style="width: 45px; height: 45px; top: 0px; left: 25px;" data-autocenter="2" data-scale="0.75" data-scale-left="0.75">
                                    <svg viewbox="0 0 16000 16000" style="position: absolute; top: 0; left: 0; width: 100%; height: 100%;">
                                        <polyline class="a" points="11040,1920 4960,8000 11040,14080 "></polyline>
                                    </svg>
                                </div>
                                <div data-u="arrowright" class="jssora053" style="width: 45px; height: 45px; top: 0px; right: 25px;" data-autocenter="2" data-scale="0.75" data-scale-right="0.75">
                                    <svg viewbox="0 0 16000 16000" style="position: absolute; top: 0; left: 0; width: 100%; height: 100%;">
                                        <polyline class="a" points="4960,1920 11040,8000 4960,14080 "></polyline>
                                    </svg>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-3">
                    <div class="row">
                        <div class="col p-0">
                            <div class="card p-1 preview-card">
                                <img class="card-img-top preview-img" id="posterImage" runat="server" src="/images/placeholder.jpg" alt="Poster">
                                <div class="card-body p-0">
                                    <p class="my-2" runat="server" id="lblDescription"></p>
                                    <div>
                                        <div id="divPlatform" runat="server">
                                            <span style="opacity: 0.8;">Platforms: </span>
                                            <span class="preview-platform" runat="server" id="lblPlatforms"></span>
                                        </div>
                                        <div id="divMerchandise" runat="server">
                                            <span style="opacity: 0.8;">Colour: </span>
                                            <span class="preview-platform" runat="server" id="lblColour"></span>
                                            <canvas id="canvasColour" width="16" height="16" style="border:4px solid #000000;" runat="server"></canvas><br />
                                            <span style="opacity: 0.8;">Size: </span>
                                            <span class="preview-platform" runat="server" id="lblSize"></span>
                                        </div>

                                        <div class="mt-4 mb-3" runat="server">
                                            <span style="opacity: 0.8;">Reviews: </span><span class="preview-rating"><%= averageRating %> <i class="fa-solid fa-star"></i>(<%= totalRating.ToString() %>)</span>
                                            <div style="display: inline-block; float: right; margin-bottom: -10px;">
                                                <asp:Button ID="btnAddReview" class="btn btn-secondary btn-sm float-left preview-add-to-wishlist ml-3" Text="Write Review" OnClientClick="$('#ratingButtons').show();" OnClick="ButtonAddReview_Click" runat="server" />
                                            </div>
                                        </div>
                                        <div class="overflow-auto" style="max-height: 180px;padding: 5px;box-shadow: 0px 0px 12px 0px #000000ba;">
                                            <asp:Repeater ID="rptReview" runat="server">
                                                <ItemTemplate>
                                                    <!-- use this card (div) as template for insertion -->
                                                    <div class="card p-1 mb-2 game-card">
                                                        <div class="card-body p-0 w-100">
                                                            <span class="card-title my-1 w-100"><%#Eval("UserName") %></span> <span class="review-date"><%# Eval("ApprovalDate").ToString().Substring(0, 10)%></span> </span><span class="preview-rating review-date"><%#Eval("Rating") %> <i class="fa-solid fa-star"></i></span>
                                                            <div><%#Eval("WrittenReview") %></div>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <style>
                                            .rating {
                                                display: flex;
                                                flex-direction: row-reverse;
                                                justify-content: center;
                                            }

                                                .rating > input {
                                                    display: none;
                                                }

                                                .rating > label {
                                                    position: relative;
                                                    width: 1.1em;
                                                    font-size: 27px;
                                                    color: #ca5500;
                                                    cursor: pointer;
                                                }

                                                    .rating > label::before {
                                                        content: "\2605";
                                                        position: absolute;
                                                        opacity: 0;
                                                    }

                                                    .rating > label:hover:before,
                                                    .rating > label:hover ~ label:before {
                                                        opacity: 1 !important;
                                                    }

                                                .rating > input:checked ~ label:before {
                                                    opacity: 1;
                                                }

                                                .rating:hover > input:checked ~ label:before {
                                                    opacity: 0.4;
                                                }
                                        </style>
                                        <div class="rating" id="ratingButtons">
                                            <asp:RadioButton ID="fiveStar" GroupName="gameRating" runat="server" ClientIDMode="Static"/><label for="fiveStar"onclick="$('<%= fiveStar.ClientID %>').click();">☆</label>
                                            <asp:RadioButton ID="fourStar" GroupName="gameRating" runat="server" ClientIDMode="Static"/><label for="fourStar" onclick="$('<%= fourStar.ClientID %>').click();">☆</label>
                                            <asp:RadioButton ID="threeStar" GroupName="gameRating" runat="server" ClientIDMode="Static"/><label for="threeStar"onclick="$('<%= threeStar.ClientID %>').click();">☆</label>
                                            <asp:RadioButton ID="twoStar" GroupName="gameRating" runat="server" ClientIDMode="Static"/><label for="twoStar" onclick="$('<%= twoStar.ClientID %>').click();">☆</label>
                                            <asp:RadioButton ID="oneStar" GroupName="gameRating" runat="server" ClientIDMode="Static"/><label for="oneStar" onclick="$('<%= oneStar.ClientID %>').click();">☆</label>
                                        </div>
                                        <asp:TextBox ID="reviewTextBox" ClientIDMode="Static" Visible="false" class="w-100 review-input mt-2" Style="width: 100%; margin-top: 16px;" TextMode="MultiLine" runat="server" Columns="37"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqReviewText" runat="server" ControlToValidate="reviewTextBox"
                                            Display="Dynamic" Style="color: red; padding-left: 10px" ErrorMessage="Review content is required." />
                                        <div style="display: inline-block; float: right;">
                                            <asp:Button ID="btnReviewItem" class="btn btn-secondary btn-sm float-left preview-add-to-wishlist ml-3" Text="Add Review" OnClick="ButtonReview_Click" runat="server" />
                                        </div>
                                        <div id="div1" runat="server">
                                            <div>
                                                <asp:Label ID="messageLabel" class="float-right" ForeColor="#FF6B00" runat="server"></asp:Label>
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
                                <div class="card-body p-0">
                                    <div class="preview-price" runat="server" id="lblPrice"></div>
                                    <div>
                                        <asp:Button ID="ButtonWishList" class="btn btn-secondary btn-sm float-left preview-add-to-wishlist ml-3" Text="Add to Wishlist" OnClick="ButtonWishList_Click" runat="server" />
                                        <asp:Button ID="ButtonBack" class="btn btn-secondary btn-sm float-right mr-3" runat="server" Text="Back" OnClick="ButtonBack_Click" CausesValidation="False" />
                                        <asp:Button ID="ButtonCart" class="btn btn-secondary btn-sm float-right preview-add-to-cart mr-3" Text="Add to Cart" OnClick="ButtonCart_Click" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div id="divResult" runat="server">
                            <div>
                                <asp:Label ID="ResultMessage" class="float-right" ForeColor="Green" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
