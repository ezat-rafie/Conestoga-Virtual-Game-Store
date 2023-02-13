<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dbTest.aspx.cs" Inherits="Chocolist.dbTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td>
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Get Orlando" OnClick="TestDB" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
