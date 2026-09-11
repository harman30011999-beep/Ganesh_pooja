<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PublicDonation.aspx.vb" Inherits="PublicDonationPage" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Support the Festival | Ganesh Utsav</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/css/admin-dashboard.css" rel="stylesheet" />
</head>
<body class="public-page">
    <form id="form1" runat="server">
        <header class="public-header"><div class="container dashboard-width"><a class="brand" href="Default.aspx"><span class="brand-mark">ॐ</span><span><strong>Ganesh Utsav</strong><small>Community celebration</small></span></a></div></header>
        <main class="public-content">
                    <div class="donation-card">
                            <p class="kicker">Make a difference</p><h1>Support the festival</h1>
                            <p class="text-muted">Make a public donation to help organize the annual Ganesh Utsav.</p>

                            <div class="row g-3">
                                <div class="col-md-6">
                                    <label class="form-label">Name</label>
                                    <asp:TextBox ID="txtDonorName" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">Phone</label>
                                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">Amount</label>
                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" TextMode="Number" />
                                </div>
                                <div class="col-md-6">
                                    <label class="form-label">Payment Method</label>
                                    <asp:DropDownList ID="ddlPaymentMethod" runat="server" CssClass="form-select">
                                        <asp:ListItem Text="UPI" Value="UPI" />
                                        <asp:ListItem Text="Cash" Value="Cash" />
                                        <asp:ListItem Text="Bank" Value="Bank" />
                                    </asp:DropDownList>
                                </div>
                                <div class="col-12">
                                    <label class="form-label">UPI / Donation Reference</label>
                                    <asp:TextBox ID="txtReference" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-12 mt-4">
                                    <asp:Button ID="btnDonate" runat="server" CssClass="btn btn-primary btn-lg w-100" Text="Submit Donation" OnClick="btnDonate_Click" />
                                </div>
                            </div>
                    </div>
        </main>
    </form>
</body>
</html>
