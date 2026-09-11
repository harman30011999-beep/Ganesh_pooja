Imports System
Imports System.Data
Imports System.Globalization

Partial Class DonationsPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("AuthUser") Is Nothing Then
            Response.Redirect("Login.aspx")
            Return
        End If

        SqlDataHelper.EnsureSchema()

        If Not IsPostBack Then
            txtDonationDate.Text = Date.Today.ToString("yyyy-MM-dd")
            LoadDonations()
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim donorName As String = txtDonorName.Text.Trim()
        Dim amount As String = txtAmount.Text.Trim()
        If String.IsNullOrWhiteSpace(donorName) OrElse String.IsNullOrWhiteSpace(amount) Then
            Return
        End If

        Dim year As String = SqlDataHelper.GetActiveYear()
        Dim sql As String = String.Format(
            "INSERT INTO dbo.Donations (DonorName, Amount, DonationDate, FestivalYear, PaymentMethod, Status, IsPublic) VALUES ('{0}', {1}, '{2}', '{3}', '{4}', '{5}', 1)",
            donorName.Replace("'", "''"),
            amount,
            txtDonationDate.Text,
            year,
            ddlPaymentMethod.SelectedValue.Replace("'", "''"),
            ddlStatus.SelectedValue.Replace("'", "''"))

        SqlDataHelper.ExecuteNonQuery(sql)
        SqlDataHelper.LogAudit(Session("AuthUser").ToString(), "Added donation", "Donation", "", "", donorName)

        txtDonorName.Text = ""
        txtAmount.Text = ""
        ddlStatus.SelectedIndex = 0
        LoadDonations()
    End Sub

    Private Sub LoadDonations()
        Dim year As String = SqlDataHelper.GetActiveYear()
        Dim data As DataTable = SqlDataHelper.GetDataTable("SELECT * FROM dbo.Donations WHERE FestivalYear = '" & year & "' ORDER BY DonationDate DESC")
        rptDonations.DataSource = data
        rptDonations.DataBind()
    End Sub
End Class
