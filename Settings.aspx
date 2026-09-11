<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Settings.aspx.vb" Inherits="SettingsPage" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" /><meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Settings | Ganesh Utsav</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/css/admin-dashboard.css" rel="stylesheet" />
</head>
<body class="admin-page">
<form id="form1" runat="server" enctype="multipart/form-data">
<nav class="topbar"><div class="container-fluid dashboard-width d-flex align-items-center justify-content-between"><a class="brand" href="Dashboard.aspx"><span class="brand-mark">ॐ</span><span><strong>Ganesh Utsav</strong><small>Committee console</small></span></a><div class="admin-nav"><a href="Donations.aspx">Donations</a><a href="Events.aspx">Schedule</a><a href="Expenses.aspx">Expenses</a><a href="Committee.aspx">Committee</a><a class="active" href="Settings.aspx">Settings</a><a href="Login.aspx">Log out</a></div></div></nav>
<main class="dashboard-width page-content">
<div class="page-intro"><div><p class="kicker">Configuration</p><h1>Festival settings</h1><p>Shape the public experience and keep the festival information current.</p></div><a class="primary-action" href="Dashboard.aspx">Back to dashboard</a></div>
<div class="admin-panel"><div class="panel-title"><h2>Public festival details</h2></div><div class="row g-3">
<div class="col-md-4"><label class="form-label">Festival Name</label><asp:TextBox ID="txtFestivalName" runat="server" CssClass="form-control" /></div>
<div class="col-md-4"><label class="form-label">Committee Name</label><asp:TextBox ID="txtCommitteeName" runat="server" CssClass="form-control" /></div>
<div class="col-md-4"><label class="form-label">Public UPI</label><asp:TextBox ID="txtPublicUpiId" runat="server" CssClass="form-control" /></div>
<div class="col-md-4"><label class="form-label">Opening Balance</label><asp:TextBox ID="txtOpeningBalance" runat="server" CssClass="form-control" TextMode="Number" /></div>
<div class="col-md-4"><label class="form-label">Landing Headline</label><asp:TextBox ID="txtLandingHeadline" runat="server" CssClass="form-control" /></div>
<div class="col-md-4"><label class="form-label">Landing Subheadline</label><asp:TextBox ID="txtLandingSubheadline" runat="server" CssClass="form-control" /></div>
<div class="col-md-6"><label class="form-label">Banner Image URL</label><asp:TextBox ID="txtBannerImageUrl" runat="server" CssClass="form-control" /></div>
<div class="col-md-6"><label class="form-label">Ganesh Idol Image (PNG/JPG)</label><asp:FileUpload ID="fileIdolImage" runat="server" CssClass="form-control" /><small class="text-muted">Upload a new image to replace the current idol image.</small></div>
<div class="col-md-4"><label class="form-label">Festival Start Date</label><asp:TextBox ID="txtFestivalStartDate" runat="server" CssClass="form-control" TextMode="Date" /></div>
<div class="col-md-4"><label class="form-label">Festival End Date</label><asp:TextBox ID="txtFestivalEndDate" runat="server" CssClass="form-control" TextMode="Date" /></div>
<div class="col-md-4"><label class="form-label">About Title</label><asp:TextBox ID="txtAboutTitle" runat="server" CssClass="form-control" /></div>
<div class="col-12"><label class="form-label">About Description</label><asp:TextBox ID="txtAboutDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" /></div>
<div class="col-md-4 form-check ms-2"><asp:CheckBox ID="chkPublicDonationEnabled" runat="server" Text="Enable public donations" /></div>
<div class="col-12"><asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="Save settings" OnClick="btnSave_Click" /><asp:Label ID="lblMessage" runat="server" CssClass="ms-3 text-muted" /></div>
</div></div></main></form></body></html>
