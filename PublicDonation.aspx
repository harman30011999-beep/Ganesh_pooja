<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PublicDonation.aspx.vb" Inherits="PublicDonationPage" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Support the Festival | Ganesh Utsav</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/css/admin-dashboard.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/qrcodejs/1.0.0/qrcode.min.js"></script>
</head>
<body class="public-page">
    <form id="form1" runat="server">
        <header class="public-header"><div class="container dashboard-width"><a class="brand" href="Default.aspx"><span class="brand-mark">ॐ</span><span><strong><%= FestivalName %></strong><small>Community celebration</small></span></a></div></header>
        <main class="public-content">
            <asp:Panel ID="pnlDonation" runat="server" CssClass="donation-card">
                <p class="kicker">Make a difference</p><h1>Support the festival</h1>
                <p class="text-muted">Scan the QR to pay by UPI, or choose cash and submit your contribution details.</p>
                <div class="donation-layout row g-4 align-items-center">
                    <div class="col-md-5 text-center">
                        <div id="qrCode" class="qr-code" aria-label="UPI payment QR code"></div>
                        <strong id="upiLabel"><%= PublicUpiId %></strong>
                        <small class="d-block text-muted mt-2">Use any UPI app</small>
                    </div>
                    <div class="col-md-7">
                        <div class="row g-3">
                            <div class="col-sm-6"><label class="form-label">Name</label><asp:TextBox ID="txtDonorName" runat="server" CssClass="form-control" /></div>
                            <div class="col-sm-6"><label class="form-label">Phone</label><asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" /></div>
                            <div class="col-sm-6"><label class="form-label">Amount</label><asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" TextMode="Number" /></div>
                            <div class="col-sm-6"><label class="form-label">Payment Method</label><asp:DropDownList ID="ddlPaymentMethod" runat="server" CssClass="form-select"><asp:ListItem Text="UPI" Value="UPI" /><asp:ListItem Text="Cash" Value="Cash" /><asp:ListItem Text="Bank" Value="Bank" /></asp:DropDownList></div>
                            <div class="col-12"><label class="form-label">UPI / Donation Reference</label><asp:TextBox ID="txtReference" runat="server" CssClass="form-control" /></div>
                            <div class="col-12 mt-2"><asp:Button ID="btnDonate" runat="server" CssClass="btn btn-primary btn-lg w-100" Text="Submit Donation" OnClick="btnDonate_Click" /></div>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlReceipt" runat="server" CssClass="donation-receipt" Visible="false">
                <div class="receipt-actions"><button type="button" class="btn btn-outline-secondary" onclick="window.print()">Print receipt</button><a class="btn btn-primary" href="Default.aspx">Back to home</a></div>
                <div class="receipt-paper">
                    <div class="receipt-heading"><asp:Image ID="imgReceiptLogo" runat="server" CssClass="receipt-logo" /><div><p class="kicker">Donation receipt</p><h1><asp:Literal ID="litFestivalName" runat="server" /></h1><p><asp:Literal ID="litCommitteeName" runat="server" /></p></div></div>
                    <div class="receipt-status"><span>Payment status</span><strong>Pending verification</strong></div>
                    <div class="receipt-details"><div><span>Donor name</span><strong><asp:Literal ID="litDonorName" runat="server" /></strong></div><div><span>Amount</span><strong>₹<asp:Literal ID="litAmount" runat="server" /></strong></div><div><span>Payment method</span><strong><asp:Literal ID="litPaymentMethod" runat="server" /></strong></div><div><span>Receipt date</span><strong><asp:Literal ID="litDonationDate" runat="server" /></strong></div></div>
                    <div class="receipt-wish"><h2>With heartfelt thanks</h2><p>May Lord Ganesha bless you and your family with happiness, good health, prosperity and peace. Thank you for supporting our community celebration.</p></div>
                    <p class="receipt-note">Your contribution is pending committee verification. Please keep this receipt for your records.</p>
                </div>
            </asp:Panel>
        </main>
    </form>
    <script>
        (function () {
            var qr = document.getElementById('qrCode');
            var amount = document.getElementById('<%= txtAmount.ClientID %>');
            var method = document.getElementById('<%= ddlPaymentMethod.ClientID %>');
            var upiId = '<%= QrUpiId %>';
            function renderQr() {
                if (!qr || !upiId) return;
                qr.innerHTML = '';
                var value = 'upi://pay?pa=' + encodeURIComponent(upiId) + '&pn=' + encodeURIComponent('<%= FestivalName %>') + '&cu=INR';
                if (amount && amount.value) value += '&am=' + encodeURIComponent(amount.value);
                new QRCode(qr, { text: value, width: 190, height: 190, colorDark: '#173c35', colorLight: '#ffffff' });
            }
            if (amount) amount.addEventListener('input', renderQr);
            if (method) method.addEventListener('change', renderQr);
            renderQr();
        }());
    </script>
</body>
</html>
