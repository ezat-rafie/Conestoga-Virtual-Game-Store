<%@ Page Language="C#" Debug="true" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="CreateItem.aspx.cs" Inherits="Chocolist.Pages.Products.CreateItem" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="ModifyItem.css" rel="stylesheet" />
    <!-- TODO:Title should change dynamically -->
    <form class="container p-2" id="frmCreateItem" style="background-color: #282B2E; border-radius: 5px; box-shadow: rgb(0 0 0 / 0.40) 5px 5px 10px;">
        <div class="row m-1">
            <div class="mb-4">
                <h3 class="mx-auto" style="width: fit-content; float: left; color:darkgrey; padding-left: 20px;">Item Creation</h3>
            </div>
        </div>
        <div class="row m-1">
            <div class="col-4">
                <div id="divItemInput" runat="server">
                    <div class="row m-1 mb-3">
                        <asp:Label Text="Title" runat="server" style="color:darkgrey; font-style:italic; margin-left:5px; margin-bottom:2px;"/>
                        <asp:TextBox type="text" class="w-100 text-input" ID="txtTitle" placeholder="Item Title" runat="server" />
                         <asp:RequiredFieldValidator ID="reqTitle" runat="server" ControlToValidate="txtTitle" Display="Dynamic"
                            ErrorMessage="Title is required." />
                    </div>
                    <div class="row m-1 mb-3">
                        <asp:Label Text="Description" runat="server" style="color:darkgrey; font-style:italic; margin-left:5px; margin-bottom:2px;"/>
                        <asp:TextBox Rows="4" class="w-100 text-input" ID="txtDescription" placeholder="ItemGame Description" runat="server"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqDescription" runat="server" ControlToValidate="txtDescription" Display="Dynamic"
                            ErrorMessage="Description is required." />
                    </div>
                    <div class="row m-1 mb-3">
                        <asp:Label Text="Price" runat="server" style="color:darkgrey; font-style:italic; margin-left:5px; margin-bottom:2px;"/>
                        <asp:TextBox type="text" class="w-100 text-input" ID="txtPrice" placeholder="$0.00" runat="server"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqPrice" runat="server" ControlToValidate="txtPrice" Display="Dynamic"
                            ErrorMessage="Price is required." />
                         <asp:RegularExpressionValidator ID="regexPrice" runat="server" ControlToValidate="txtPrice"
                             ErrorMessage="Can only be numbers and 2 after a decimal" ValidationExpression="^\d*\.?\d{0,2}$" Display="Dynamic"></asp:RegularExpressionValidator>
                    </div>
                    <div class="row m-1 mb-4">                        
                        <asp:Label Text="Quantity" runat="server" style="color:darkgrey; font-style:italic; margin-left:5px; margin-bottom:2px;"/>
                        <asp:TextBox type="text" class="w-100 text-input" ID="txtQuantity" placeholder="0" runat="server"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqQuantity" runat="server" ControlToValidate="txtQuantity" Display="Dynamic"
                            ErrorMessage="Quantity is required." />
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtQuantity" Display="Dynamic"
                             ErrorMessage="Must be a number >= 0" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                    </div>
                    <div class="row m-1 mb-3">
                        <asp:Label Text="GameTag" runat="server" style="color:darkgrey; font-style:italic; margin-left:5px; margin-bottom:2px;"/>
                        <asp:TextBox type="text" class="w-100 text-input" ID="txtGameTag" placeholder="GameTags(separate with ,)" runat="server" />
                    </div>
                </div>
                <!-- TODO Hiding for now
                <div class="row m-1 mb-4">
                    <input type="text" class="w-100 text-input" id="releaseData" placeholder="Release Date">
                </div>
                <div class="row m-1 mb-4">
                    <input type="text" class="w-100 text-input" id="developer" placeholder="Developer">
                </div>
                -->
                <asp:Label class="row m-1 mb-1" Text="Type" runat="server" style="color:darkgrey; font-style:italic; margin-left:5px; margin-bottom:2px;"/>
                <div class="row m-1 mb-4 itemttype">
                    <div class="col">
                        <div class="form-check mb-1 row">
                            <asp:RadioButton class="form-check-label" ID="radTypeGame" Text="Game" Checked="True" GroupName="ItemTypeRadio" runat="server" oncheckedchanged="GameType_Changed" AutoPostBack="true"/><br />
                        </div>
                        <div class="form-check row mb-1">
                            <asp:RadioButton class="form-check-label" ID="radTypeMerch" Text="Merchandise" GroupName="ItemTypeRadio" runat="server" oncheckedchanged="GameType_Changed" AutoPostBack="true"/><br />
                        </div>
                    </div>
                </div>  
                <div id="divGame" runat="server">
                    <asp:Label class="row m-1 mb-1" Text="Platform" runat="server" style="color:darkgrey; font-style:italic; margin-left:5px; margin-bottom:2px;"/>
                    <div class="row m-1 mb-4 platforms">
                        <div class="col">
                            <div class="form-check mb-1 row">
                                <asp:RadioButton class="form-check-label" ID="radWindows" Text="Windows" Checked="True" GroupName="PlatformRadio" runat="server" /><br />
                            </div>
                            <div class="form-check row mb-1">
                                <asp:RadioButton class="form-check-label" ID="radMacintosh" Text="Macintosh" GroupName="PlatformRadio" runat="server" /><br />
                            </div>
                            <div class="form-check row mb-1">
                                <asp:RadioButton class="form-check-label" ID="radNintendo" Text="Nintendo" GroupName="PlatformRadio" runat="server" /><br />
                            </div>
                            <div class="form-check row mb-1">
                                <asp:RadioButton class="form-check-label" ID="radXbox" Text="Xbox" GroupName="PlatformRadio" runat="server" /><br />
                            </div>
                            <div class="form-check row mb-1">
                                <asp:RadioButton class="form-check-label" ID="radPlayStation" Text="PlayStation" GroupName="PlatformRadio" runat="server" /><br />
                            </div>
                        </div>
                    </div>  
                
                    <asp:Label class="row m-1 mb-1" Text="Genre" runat="server" Style="color: darkgrey; font-style: italic; margin-left: 5px; margin-bottom: 2px;" />
                    <div class="row m-1 mb-4 platforms">
                        <div class="col">
                            <div class="form-check mb-1 row">
                                <asp:RadioButton class="form-check-label" ID="radAction" Text="Action" Checked="True" GroupName="GenreRadio" runat="server" /><br />
                            </div>
                            <div class="form-check row mb-1">
                                <asp:RadioButton class="form-check-label" ID="radAdventure" Text="Adventure" GroupName="GenreRadio" runat="server" /><br />
                            </div>
                            <div class="form-check row mb-1">
                                <asp:RadioButton class="form-check-label" ID="radStrategy" Text="Strategy" GroupName="GenreRadio" runat="server" /><br />
                            </div>
                            <div class="form-check row mb-1">
                                <asp:RadioButton class="form-check-label" ID="radFamily" Text="Family" GroupName="GenreRadio" runat="server" /><br />
                            </div>
                            <div class="form-check row mb-1">
                                <asp:RadioButton class="form-check-label" ID="radPuzzle" Text="Puzzle" GroupName="GenreRadio" runat="server" /><br />
                            </div>
                            <div class="form-check row mb-1">
                                <asp:RadioButton class="form-check-label" ID="radSports" Text="Sports" GroupName="GenreRadio" runat="server" /><br />
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divMerchandise" runat="server">
                    <div class="row m-1 mb-3">
                        <asp:Label class="row m-1 mb-1" Text="Colour" runat="server" style="color:darkgrey; font-style:italic; margin-left:5px; margin-bottom:2px;"/>
                        <asp:DropDownList CssClass="w-100 text-input btn btn-secondary btn-sm dropdown-toggle" ID="ddlSelectColour" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Colour_Changed">
                        </asp:DropDownList>                    
                        <canvas id="canvasColour" width="50" height="50" style="border:4px solid #000000;" runat="server"></canvas>
                    </div>
                    <div class="row m-1 mb-3">
                        <asp:Label class="row m-1 mb-1" Text="Size" runat="server" style="color:darkgrey; font-style:italic; margin-left:5px; margin-bottom:2px;"/>
                        <asp:TextBox type="text" class="w-100 text-input" ID="txtSize" placeholder="Size" runat="server" />
                    </div>
                </div>
<%--                <div class="row m-1 mb-4">
                    <div>
                         TODO Later 
                        <asp:Label ID="LabelCategory" runat="server" Text="Category"></asp:Label>
                        <asp:DropDownList ID="DropDownCategory" runat="server"></asp:DropDownList>
                    </div>
                     TODO Hiding for now
                    <input type="text" class="w-100 text-input" id="category" placeholder="Category">
                    
                </div>--%>
            </div>
            <div class="col-8">
                <div class="row m-1 mb-2 cover-card">
                    <a href="#" class="btn btn-secondary mx-auto mb-2 buttons">Add Cover</a>
                    <img src="/images/1.jpg" class="game-images" id="coverImage" runat="server"/>
                </div>
                <div class="row m-1 mb-4 cover-card">
                    <a href="#" class="btn btn-secondary mx-auto mb-2 buttons">Add Screenshot</a>
                    <div class="row m-1">
                        <div class="col-4">
                            <img src="/images/1.jpg" class="game-images" id="itemImage1" runat="server"/>
                        </div>
                        <div class="col-4">
                            <img src="/images/2.jpg" class="game-images" id="itemImage2" runat="server"/>
                        </div>
                        <div class="col-4">
                            <img src="/images/3.jpg" class="game-images" id="itemImage3" runat="server"/>
                        </div>
                    </div>
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
                    <asp:Button ID="ButtonSave" class="btn btn-secondary mx-auto mb-2 buttons" runat="server" Text="Save" OnClick="ButtonSave_Click" />
                    <asp:Button ID="ButtonBack" class="btn btn-secondary mx-auto mb-2 buttons" runat="server" Text="Back" OnClick="ButtonBack_Click" CausesValidation="False"/>
                </div>
            </div>
        </div>
    </form>
</asp:Content>

