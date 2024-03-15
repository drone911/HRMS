<%@ Page Language="C#" AutoEventWireup="false" CodeFile="SendVerification.aspx.cs" Inherits="SendVerification" ErrorPage="~/error.aspx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Send Verification</title>

    <link href="Scripts/css/bootstrap.min.css" rel="stylesheet" />
    
    <script src="Scripts/js/jquery-3.3.1.min.js"></script>
    <script src="Scripts/js/popper.min.js"></script>
    <script src="Scripts/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="w-100" style="height:95vh;">
            <div class="jumbotron col-8 mx-auto" style="margin-top: 20vh">
                <h1 class="display-4">Didn't receive an Email?</h1>
                <hr class="my-4" />
                <div class="form-group">
                    <asp:Label  runat="server" CssClass="pr-2">Enter your registered email</asp:Label>
                    <asp:TextBox TextMode="Email" CssClass="form-control" runat="server" ID="EmailTextBox" ValidationGroup="vg" required></asp:TextBox>
                    <asp:Label runat="server" ID="EmailLabel"></asp:Label>
                </div>
                <asp:Button runat="server" CssClass="btn btn-primary btn-lg" ValidationGroup="vg" Text="Send Mail" OnClick="SendVerification_Click" />
                <asp:HyperLink runat="server" ID="RegisterButton" CssClass="btn btn-outline-success btn-lg" Text="Register Here" NavigateUrl="~/UserRegistration.aspx" Visible="false"></asp:HyperLink>
                <br />
                <br />
            </div>
        </div>
    </form>
</body>
</html>
