Imports System
Imports System.Data
Imports System.Globalization

Partial Class DashboardPage
    Inherits System.Web.UI.Page

    Public Property CurrentBalance As Decimal
    Public Property ConfirmedDonationTotal As Decimal
    Public Property ApprovedExpenseTotal As Decimal
    Public Property CommitteeCount As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("AuthUser") Is Nothing Then
            Response.Redirect("Login.aspx")
            Return
        End If

        SqlDataHelper.EnsureSchema()

        Dim currentYear As String = SqlDataHelper.GetActiveYear()
        Dim settings As DataRow = SqlDataHelper.GetCurrentSettings()
        Dim openBalance As Decimal = 0
        If settings IsNot Nothing Then
            openBalance = Convert.ToDecimal(If(String.IsNullOrWhiteSpace(settings("OpeningBalance").ToString()), "0", settings("OpeningBalance")))
        End If

        Dim donationsData As DataTable = SqlDataHelper.GetDataTable("SELECT * FROM dbo.Donations WHERE FestivalYear = '" & currentYear & "'")
        Dim expensesData As DataTable = SqlDataHelper.GetDataTable("SELECT * FROM dbo.Expenses WHERE FestivalYear = '" & currentYear & "'")
        Dim committeeData As DataTable = SqlDataHelper.GetDataTable("SELECT * FROM dbo.CommitteeMembers WHERE FestivalYear = '" & currentYear & "'")

        ConfirmedDonationTotal = donationsData.AsEnumerable() _
            .Where(Function(row) row("Status").ToString() = "Confirmed") _
            .Sum(Function(row) Convert.ToDecimal(row("Amount")))

        ApprovedExpenseTotal = expensesData.AsEnumerable() _
            .Where(Function(row) row("Status").ToString() = "Approved") _
            .Sum(Function(row) Convert.ToDecimal(row("Amount")))

        CommitteeCount = committeeData.Rows.Count
        CurrentBalance = openBalance + ConfirmedDonationTotal - ApprovedExpenseTotal

        Dim recentDonations As DataTable = donationsData.Clone()
        For Each row As DataRow In donationsData.Rows
            recentDonations.ImportRow(row)
        Next

        If recentDonations.Rows.Count > 0 Then
            recentDonations.DefaultView.Sort = "DonationDate DESC"
            rptDonations.DataSource = recentDonations.DefaultView
            rptDonations.DataBind()
        Else
            rptDonations.DataSource = Nothing
            rptDonations.DataBind()
        End If
    End Sub

    Public Shared Function FormatCurrency(value As Decimal) As String
        Return String.Format(CultureInfo.GetCultureInfo("en-IN"), "₹{0:n0}", value)
    End Function
End Class
