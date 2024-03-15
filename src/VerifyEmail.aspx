<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VerifyEmail.aspx.cs" Inherits="VerifyEmail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Verify Email</title>

    <link href="Scripts/css/bootstrap.min.css" rel="stylesheet" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

    <script src="Scripts/js/jquery-3.3.1.min.js"></script>
    <script src="Scripts/js/popper.min.js"></script>
    <script src="Scripts/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container col-6" style="margin-top: 20vh;">
            <div class="card align-items-center">
                <h2 class="card-header" style="width: 100%; align-content: center">Email Verification</h2>
                <asp:Label ID="DisplayLabel" Style="width: 100%; align-content: center; padding-top: 10px; margin-top: 15px; margin-bottom: 15px" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
