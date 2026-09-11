<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="LoginPage" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Committee Login | Ganesh Utsav</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/css/admin-dashboard.css" rel="stylesheet" />
</head>
<body class="auth-page">
    <form id="form1" runat="server">
        <div class="auth-card">
            <a class="brand" href="Default.aspx"><span class="brand-mark">ॐ</span><span><strong>Ganesh Utsav</strong><small>Committee console</small></span></a>
            <h1>Welcome back</h1>
            <div class="subtle mb-4">Sign in to manage the festival.</div>

            <div class="alert alert-danger" runat="server" id="messageBox">
                <%= LoginMessage %>
            </div>

            <div class="mb-3">
                <label class="form-label">Username</label>
                <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" />
            </div>
            <div class="mb-4">
                <label class="form-label">Password</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
            </div>
            <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary w-100" Text="Login" OnClick="btnLogin_Click" />
            <div class="mt-4 text-center">
                <a href="Default.aspx" class="text-decoration-none">Back to home</a>
            </div>
        </div>
    </form>
</body>
</html>
