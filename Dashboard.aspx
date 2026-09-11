<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Dashboard.aspx.vb" Inherits="DashboardPage" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Dashboard | Ganesh Utsav</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link href="https://fonts.googleapis.com/css2?family=DM+Sans:wght@400;500;600;700&family=Space+Grotesk:wght@500;600;700&display=swap" rel="stylesheet" />
    <link href="assets/css/admin-dashboard.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <nav class="topbar">
            <div class="container-fluid dashboard-width d-flex align-items-center justify-content-between">
                <a class="brand" href="Default.aspx">
                    <span class="brand-mark"></span>
                    <span><strong>Ganesh Utsav</strong><small>Committee console</small></span>
                </a>
                <div class="d-flex align-items-center gap-3">
                    <span class="live-status"><span></span> Live workspace</span>
                    <a class="logout-link" href="Login.aspx">Log out</a>
                </div>
            </div>
        </nav>

        <main class="dashboard-width dashboard-content">
            <div class="welcome-row">
                <div>
                    <p class="kicker">Festival year <span class="year-chip"><%= SqlDataHelper.GetActiveYear() %></span></p>
                    <h1>Good morning, committee.</h1>
                    <p class="welcome-copy">A clear view of the money, people, and activity keeping this celebration moving.</p>
                </div>
                <div class="d-flex flex-wrap gap-2"><a class="primary-action" href="Events.aspx">Upload schedule</a><a class="primary-action" href="Donations.aspx"><span>+</span> Record donation</a></div>
            </div>

            <section class="stats-grid" aria-label="Financial summary">
                <div class="col-md-6 col-xl-3">
                    <div class="metric-card balance-card">
                        <div class="metric-top"><span class="metric-label">Current balance</span><span class="metric-icon">↗</span></div>
                        <h2>Rs. <%=(CurrentBalance) %></h2>
                        <p>Available after approved expenses</p>
                    </div>
                </div>
                <div class="col-md-6 col-xl-3">
                    <div class="metric-card">
                        <div class="metric-top"><span class="metric-label">Confirmed donations</span><span class="metric-icon amber">₹</span></div>
                        <h2>Rs. <%=(ConfirmedDonationTotal) %></h2>
                        <p>Received and reconciled</p>
                    </div>
                </div>
                <div class="col-md-6 col-xl-3">
                    <div class="metric-card">
                        <div class="metric-top"><span class="metric-label">Approved expenses</span><span class="metric-icon coral">−</span></div>
                        <h2>Rs. <%=(ApprovedExpenseTotal) %></h2>
                        <p>Committed from this year's fund</p>
                    </div>
                </div>
                <div class="col-md-6 col-xl-3">
                    <div class="metric-card">
                        <div class="metric-top"><span class="metric-label">Committee members</span><span class="metric-icon violet">✦</span></div>
                        <h2><%= CommitteeCount %></h2>
                        <p>Registered for this festival year</p>
                    </div>
                </div>
            </section>

            <section class="dashboard-lower">
                <div class="recent-panel">
                    <div class="section-heading">
                        <div><p class="kicker">Latest activity</p><h3>Recent donations</h3></div>
                        <a href="Donations.aspx" class="text-link">View all <span>→</span></a>
                    </div>
                    <div class="table-responsive">
                    <table class="donation-table">
                    <thead>
                        <tr>
                            <th>Donor</th><th>Amount</th><th>Status</th><th>Date</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptDonations" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><span class="donor-avatar"><%# Left(Eval("DonorName").ToString(), 1).ToUpper() %></span><strong><%# Eval("DonorName") %></strong></td>
                                    <td class="amount"><%# Eval("Amount", "₹{0:n0}") %></td>
                                    <td><span class="status-pill"><%# Eval("Status") %></span></td>
                                    <td><%# Eval("DonationDate", "{0:dd MMM yyyy}") %></td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                    </div>
                </div>
                <aside class="quick-panel">
                    <p class="kicker">Quick actions</p>
                    <h3>Keep things moving</h3>
                    <a href="Expenses.aspx"><span class="quick-icon coral">−</span><span><strong>Review expenses</strong><small>Check pending approvals</small></span><b>→</b></a>
                    <a href="Committee.aspx"><span class="quick-icon violet">✦</span><span><strong>Manage committee</strong><small>Update member records</small></span><b>→</b></a>
                    <a href="Settings.aspx"><span class="quick-icon amber">⚙</span><span><strong>Festival settings</strong><small>Keep public details current</small></span><b>→</b></a>
                </aside>
            </section>
        </main>
    </form>
</body>
</html>
