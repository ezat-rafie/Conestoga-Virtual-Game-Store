<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewWishList.aspx.cs" Inherits="Chocolist.Pages.Cart.ViewWishList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        ul.share-buttons{
          list-style: none;
          padding: 0;
        }

        ul.share-buttons li{
          display: inline;
        }

        ul.share-buttons .sr-only{
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
    <link href="../Products/Preview.css" rel="stylesheet" />
    <link href="Cart.css" rel="stylesheet" />
    <div class="ContentHead row">
        <asp:Label runat="server" ID="wishListTitle" class="float-left" Text="Wish List" Font-Size="XX-Large" />
    </div>
    <div class="share-btn-container" runat="server" id="shareButtons">
        <ul class="share-buttons" data-source="simplesharingbuttons.com">
          <li><a href="https://www.facebook.com/sharer/sharer.php?u=https%3A%2F%2Fchocolist.twistcomputers.ca%2F&quote=Chocolist%20Wish%20List" title="Share on Facebook" target="_blank"
              class="btn btn-primary" style="background-color: #3b5998;" role="button"> <!-- class="facebook-btn" -->
              <i class="fab fa-facebook-f"></i></a>
              </li>
          <li><a href="https://twitter.com/intent/tweet?source=https%3A%2F%2Fchocolist.twistcomputers.ca%2F&text=Chocolist%20Wish%20List:%20https%3A%2F%2Fchocolist.twistcomputers.ca%2F" target="_blank" title="Tweet"
              class="btn btn-primary" style="background-color: #55acee;" role="button"> <!-- class="twitter-btn" -->
              <i class="fab fa-twitter"></i></a></li>
          <%--<li><a href="http://www.linkedin.com/shareArticle?mini=true&url=https%3A%2F%2Fchocolist.twistcomputers.ca%2F&title=Chocolist%20Wish%20List&summary=this%20is%20my%20wish%20list&source=https%3A%2F%2Fchocolist.twistcomputers.ca%2F" target="_blank" title="Share on LinkedIn"
              class="btn btn-primary" style="background-color: #0082ca;" role="button"> <!-- class="linkedin-btn" -->
              <i class="fab fa-linkedin-in"></i>
              </a></li>--%>
        </ul>
    </div>
    <div class="row">
        <asp:Repeater ID="rptWishListGame" runat="server" OnItemDataBound="rptWishListGame_ItemDataBound">
            <ItemTemplate>
                <!-- use this card (div) as template for insertion -->
                <div class="col-12">
                    <div class="card p-1 mb-2 game-card">
                        <div class="row mr-3">
                            <div class="col-3">
                                <asp:HyperLink class="btn btn-secondary btn-sm float-right add-to-cart mr-1" ID="HyperLink2" runat="server" Text="Link" NavigateUrl='<%# "~/pages/Products/Preview.aspx?itemId=" + Eval("itemId") %>'>
                                        <img class="card-img-top game-img" src="/images/1.jpg" alt="">
                                </asp:HyperLink>
                            </div>
                            <div class="col-9">
                                <div class="card-body p-0">
                                    <h5 class="card-title my-1"><%#Eval("item.name") %></h5>
                                    <span class="game-rating">4.8 <i class="fa-solid fa-star"></i></span>
                                    <div class="game-category">
                                        <span>Genre: </span>
                                        <span><%#Eval("GenreName") %></span>
                                    </div>
                                    <div class="game-platform">
                                        <span>Platform: </span>
                                        <span><%#Eval("PlatformName").ToString()%></span>
                                    </div>
                                    <div class="game-price">
                                        <span >Price: </span>
                                        <span >$<%#Eval("item.price", "{0:0.00}") %></span>
                                    </div>              
                                    <div>
                                        <asp:LinkButton class="btn btn-secondary btn-sm float-right preview-add-to-cart mb-1" CommandArgument='<%#Eval("itemId") %>' OnClientClick="if (!confirm('Are you sure you want to remove it?')) return false;" runat="server" OnCommand="RemoveWishList" Text="Remove" ID="btnRemove"/>
                                        <asp:LinkButton CommandArgument='<%#Eval("itemId") %>' class="btn btn-secondary float-right btn-sm preview-add-to-cart mr-3" Text="Add to Cart" OnCommand="ButtonWishListCart_Click" runat="server" ID="btnCart"/>
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
