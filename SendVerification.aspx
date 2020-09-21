<%@ Page Language="C#" AutoEventWireup="false" CodeFile="SendVerification.aspx.cs" Inherits="SendVerification" ErrorPage="~/error.aspx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Send Verification</title>

    <link href="Scripts/css/bootstrap.min.css" rel="stylesheet" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

    <script src="Scripts/js/jquery-3.3.1.min.js"></script>
    <script src="Scripts/js/popper.min.js"></script>
    <script src="Scripts/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label runat="server">Enter your registered email</asp:Label>
            <asp:TextBox runat="server" ID="EmailTextBox" ValidationGroup="vg" required></asp:TextBox>
            <br />
            <asp:RegularExpressionValidator runat="server" ValidationGroup="vg" ErrorMessage="*Enter a valid email" ControlToValidate="EmailTextBox" ValidationExpression="[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?" Display="Dynamic"></asp:RegularExpressionValidator>
            
            <asp:Label runat="server" ID="EmailLabel"></asp:Label>
            <br />
            <asp:Button runat="server" ValidationGroup="vg" Text="Send Email" OnClick="SendVerification_Click" />
        </div>
    </form>
</body>
</html>
