Imports System
Imports System.Data
Imports System.Linq
Imports System.Globalization

Partial Class _Default
    Inherits System.Web.UI.Page

    Public Property FestivalName As String
    Public Property CommitteeName As String
    Public Property LandingHeadline As String
    Public Property LandingSubheadline As String
    Public Property LandingLogo As String
    Public Property LandingBannerImage As String
    Public Property AboutTitle As String
    Public Property AboutDescription As String
    Public Property FestivalStartDate As String
    Public Property FestivalEndDate As String
    Public Property PublicUpiId As String
    Public Property PublicDonationEnabled As Boolean
    Public Property OpenBalance As Decimal
    Public Property ConfirmedDonationTotal As Decimal
    Public Property ApprovedExpenseTotal As Decimal
    Public Property CurrentBalance As Decimal
    Public Property DonationCount As Integer
    Public Property EventCount As Integer
    Public Property Year As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        SqlDataHelper.EnsureSchema()

        Dim settings As DataRow = SqlDataHelper.GetCurrentSettings()
        FestivalName = settings("FestivalName").ToString()
        CommitteeName = settings("CommitteeName").ToString()
        LandingHeadline = settings("LandingHeadline").ToString()
        LandingSubheadline = settings("LandingSubheadline").ToString()
        LandingLogo = settings("LogoUrl").ToString()
        LandingBannerImage = settings("LandingBannerImage").ToString()
        AboutTitle = settings("AboutTitle").ToString()
        AboutDescription = settings("AboutDescription").ToString()
        FestivalStartDate = settings("FestivalStartDate").ToString()
        FestivalEndDate = settings("FestivalEndDate").ToString()
        PublicUpiId = settings("PublicUpiId").ToString()
        PublicDonationEnabled = Convert.ToBoolean(settings("PublicDonationEnabled"))

        Year = SqlDataHelper.GetActiveYear()

        Dim currentYear = Year
        OpenBalance = Convert.ToDecimal(If(String.IsNullOrWhiteSpace(settings("OpeningBalance").ToString()), "0", settings("OpeningBalance")))

        Dim donationsData As DataTable = SqlDataHelper.GetDataTable("SELECT * FROM dbo.Donations WHERE FestivalYear = '" & currentYear & "'")
        Dim expensesData As DataTable = SqlDataHelper.GetDataTable("SELECT * FROM dbo.Expenses WHERE FestivalYear = '" & currentYear & "'")
        Dim eventsData As DataTable = SqlDataHelper.GetDataTable("SELECT * FROM dbo.Events WHERE FestivalYear = '" & currentYear & "'")

        ConfirmedDonationTotal = donationsData.AsEnumerable() _
            .Where(Function(row) row("Status").ToString() = "Confirmed") _
            .Sum(Function(row) Convert.ToDecimal(row("Amount")))

        ApprovedExpenseTotal = expensesData.AsEnumerable() _
            .Where(Function(row) row("Status").ToString() = "Approved") _
            .Sum(Function(row) Convert.ToDecimal(row("Amount")))

        CurrentBalance = OpenBalance + ConfirmedDonationTotal - ApprovedExpenseTotal
        DonationCount = donationsData.Rows.Count
        EventCount = eventsData.Rows.Count

        Dim galleryData As DataTable = SqlDataHelper.GetDataTable("SELECT * FROM dbo.Gallery WHERE FestivalYear = '" & currentYear & "' ORDER BY CreatedAt DESC")
        Dim committeeData As DataTable = SqlDataHelper.GetDataTable("SELECT * FROM dbo.CommitteeMembers WHERE FestivalYear = '" & currentYear & "' ORDER BY MemberId DESC")

        rptGallery.DataSource = galleryData
        rptGallery.DataBind()

        rptEvents.DataSource = eventsData
        rptEvents.DataBind()

        rptCommittee.DataSource = committeeData
        rptCommittee.DataBind()
    End Sub

    Public Shared Function FormatCurrency(value As Decimal) As String
        Return String.Format(CultureInfo.GetCultureInfo("en-IN"), "₹{0:n0}", value)
    End Function
End Class
