Imports System
Imports System.Data
Imports System.IO

Partial Class SettingsPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("AuthUser") Is Nothing Then
            Response.Redirect("Login.aspx")
            Return
        End If

        SqlDataHelper.EnsureSchema()

        If Not IsPostBack Then
            Dim settings As DataRow = SqlDataHelper.GetCurrentSettings()
            If settings IsNot Nothing Then
                txtFestivalName.Text = settings("FestivalName").ToString()
                txtCommitteeName.Text = settings("CommitteeName").ToString()
                txtPublicUpiId.Text = settings("PublicUpiId").ToString()
                txtOpeningBalance.Text = settings("OpeningBalance").ToString()
                txtLandingHeadline.Text = settings("LandingHeadline").ToString()
                txtLandingSubheadline.Text = settings("LandingSubheadline").ToString()
                txtBannerImageUrl.Text = settings("LandingBannerImage").ToString()
                txtAboutTitle.Text = settings("AboutTitle").ToString()
                txtAboutDescription.Text = settings("AboutDescription").ToString()
                txtFestivalStartDate.Text = settings("FestivalStartDate").ToString()
                txtFestivalEndDate.Text = settings("FestivalEndDate").ToString()
                chkPublicDonationEnabled.Checked = Convert.ToBoolean(settings("PublicDonationEnabled"))
            End If
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim festivalName As String = txtFestivalName.Text.Trim()
        Dim committeeName As String = txtCommitteeName.Text.Trim()

        Dim bannerImageUrl As String = txtBannerImageUrl.Text.Trim()
        Dim idolImageUrl As String = SaveIdolImage()
        Dim aboutTitle As String = txtAboutTitle.Text.Trim()
        Dim aboutDescription As String = txtAboutDescription.Text.Trim()
        Dim startDate As String = If(String.IsNullOrWhiteSpace(txtFestivalStartDate.Text.Trim()), "NULL", "'" & txtFestivalStartDate.Text.Trim() & "'")
        Dim endDate As String = If(String.IsNullOrWhiteSpace(txtFestivalEndDate.Text.Trim()), "NULL", "'" & txtFestivalEndDate.Text.Trim() & "'")

        Dim sql As String = String.Format(
            "UPDATE dbo.Settings SET FestivalName = '{0}', CommitteeName = '{1}', PublicUpiId = '{2}', OpeningBalance = {3}, LandingHeadline = '{4}', LandingSubheadline = '{5}', LandingBannerImage = '{6}', LogoUrl = ISNULL(NULLIF('{7}', ''), LogoUrl), PublicDonationEnabled = {8}, AboutTitle = '{9}', AboutDescription = '{10}', FestivalStartDate = {11}, FestivalEndDate = {12} WHERE Id = (SELECT TOP 1 Id FROM dbo.Settings ORDER BY Id DESC)",
            festivalName.Replace("'", "''"),
            committeeName.Replace("'", "''"),
            txtPublicUpiId.Text.Trim().Replace("'", "''"),
            txtOpeningBalance.Text.Trim(),
            txtLandingHeadline.Text.Trim().Replace("'", "''"),
            txtLandingSubheadline.Text.Trim().Replace("'", "''"),
            bannerImageUrl.Replace("'", "''"),
            idolImageUrl.Replace("'", "''"),
            If(chkPublicDonationEnabled.Checked, 1, 0),
            aboutTitle.Replace("'", "''"),
            aboutDescription.Replace("'", "''"),
            startDate,
            endDate)

        SqlDataHelper.ExecuteNonQuery(sql)
        SqlDataHelper.LogAudit(Session("AuthUser").ToString(), "Updated settings", "Settings", "", "", festivalName)
        lblMessage.Text = "Settings saved."
    End Sub

    Private Function SaveIdolImage() As String
        If Not fileIdolImage.HasFile Then
            Return String.Empty
        End If

        Dim extension As String = Path.GetExtension(fileIdolImage.FileName).ToLowerInvariant()
        If extension <> ".png" AndAlso extension <> ".jpg" AndAlso extension <> ".jpeg" Then
            Return String.Empty
        End If

        Dim folder As String = Server.MapPath("~/assets/uploads")
        Directory.CreateDirectory(folder)
        Dim fileName As String = "ganesh-idol-" & DateTime.Now.ToString("yyyyMMddHHmmss") & extension
        fileIdolImage.SaveAs(Path.Combine(folder, fileName))
        Return "assets/uploads/" & fileName
    End Function
End Class
