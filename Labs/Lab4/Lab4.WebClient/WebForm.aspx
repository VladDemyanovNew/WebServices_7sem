<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm.aspx.cs" Inherits="Lab4.WebClient.WebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="A" runat="server">
        A1:
        <div>
            <label>S</label>
            <asp:TextBox runat="server" ID="A1S" />
        </div>
        <div>
            <label>K</label>
            <asp:TextBox runat="server" ID="A1K" />
        </div>
        <div>
            <label>F</label>
            <asp:TextBox runat="server" ID="A1F" />
        </div>
        <hr />
        A2:
        <div>
            <label>S</label>
            <asp:TextBox runat="server" ID="A2S" />
        </div>
        <div>
            <label>K</label>
            <asp:TextBox runat="server" ID="A2K" />
        </div>
        <div>
            <label>F</label>
            <asp:TextBox runat="server" ID="A2F" />
        </div>
        <div>
            <asp:Button runat="server" ID="send_btn" OnClick="Send" Text="Send" />
        </div>
        <hr />
        Response
        <div>
            <label>S</label>
            <asp:TextBox runat="server" ID="A3S" readonly="true"/>
        </div>
        <div>
            <label>K</label>
            <asp:TextBox runat="server" ID="A3K" readonly="true"/>
        </div>
        <div>
            <label>F</label>
            <asp:TextBox runat="server" ID="A3F" readonly="true"/>
        </div>
    </form>
</body>
</html>
