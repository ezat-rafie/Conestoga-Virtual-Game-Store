<%@ Page Title="Item Review" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItemReviews.aspx.cs" Inherits="Chocolist.Pages.Products.ItemReviews" %>

<asp:Content ID="reviewBody" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        ul.share-buttons {
            list-style: none;
            padding: 0;
        }

            ul.share-buttons li {
                display: inline;
            }

            ul.share-buttons .sr-only {
                position: absolute;
                clip: rect(1px 1px 1px 1px);
                clip: rect(1px, 1px, 1px, 1px);
                padding: 0;
                border: 0;
                height: 1px;
                width: 1px;
                overflow: hidden;
            }
    </style>
    <link href="Preview.css" rel="stylesheet" />
    <div class="ContentHead row">
        <asp:Label runat="server" ID="reviewListTitle" class="float-left" Text="Review List" Font-Size="XX-Large" />
    </div>

    <div class="row">
        <asp:Repeater ID="rptPendingReview" runat="server">
            <ItemTemplate>
                <div class="col-12">
                    <!-- use this card (div) as template for insertion -->
                    <div class="card p-1 mb-2 game-card">
                        <div class="row mr-3">
                            <div class="col-3">
                                <asp:HyperLink class="btn btn-secondary btn-sm float-right add-to-cart mr-1" ID="HyperLink2" runat="server" Text="Link" NavigateUrl='<%# "~/pages/Products/Preview.aspx?itemId=" + Eval("game.item.itemId") %>'>
                                        <img class="card-img-top game-img" src="/images/1.jpg" alt="">
                                </asp:HyperLink>
                            </div>
                            <div class="col-9">
                                <div class="card-body p-0">
                                    <h5 class="card-title my-1 font-primary"><%#Eval("ReviewItemTitle") %></h5>
                                    <span class="game-rating"><%#Eval("Rating") %> <i class="fa-solid fa-star"></i></span>
                                    <!-- use this card (div) as template for insertion -->
                                    <div class="game-price">
                                        <span>User: </span><span class="card-title my-1 w-100"><%#Eval("UserName") %></span>
                                        <div><span>Comments: </span><%#Eval("WrittenReview") %></div>
                                    </div>
                                    <div>
                                        <asp:LinkButton class="btn btn-secondary btn-sm float-right preview-add-to-cart mb-1" CommandArgument='<%#Eval("reviewid") %>' OnClientClick="if (!confirm('Are you sure you want to reject it?')) return false;" runat="server" OnCommand="ButtonRejectReview_Click" Text="Reject" ID="btnReject" />
                                        <asp:LinkButton CommandArgument='<%#Eval("reviewid") %>' class="btn btn-secondary float-right btn-sm preview-add-to-cart mr-3" Text="Publish" OnCommand="ButtonPublish_Click" runat="server" ID="btnCart" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>

</asp:Content>
