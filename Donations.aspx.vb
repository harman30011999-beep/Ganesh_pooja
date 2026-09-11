Imports System
Imports System.Data
Imports System.Globalization
Imports System.Web.UI.WebControls

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

    Protected Sub rptDonations_ItemCommand(ByVal source As Object, ByVal e As RepeaterCommandEventArgs)
        If e.CommandName <> "UpdateStatus" Then
            Return
        End If

        Dim statusList As DropDownList = CType(e.Item.FindControl("ddlRowStatus"), DropDownList)
        Dim donationId As Integer
        If statusList Is Nothing OrElse Not Integer.TryParse(e.CommandArgument.ToString(), donationId) Then
            Return
        End If

        Dim newStatus As String = statusList.SelectedValue
        If newStatus <> "Pending" AndAlso newStatus <> "Confirmed" AndAlso newStatus <> "Rejected" Then
            Return
        End If

        Dim oldStatus As Object = SqlDataHelper.ExecuteScalar("SELECT Status FROM dbo.Donations WHERE DonationId = " & donationId & " AND StatusChangeCount = 0")
        If oldStatus Is Nothing Then
            LoadDonations()
            Return
        End If

        SqlDataHelper.ExecuteNonQuery("UPDATE dbo.Donations SET Status = '" & newStatus & "', StatusChangeCount = StatusChangeCount + 1 WHERE DonationId = " & donationId & " AND StatusChangeCount = 0")
        SqlDataHelper.LogAudit(Session("AuthUser").ToString(), "Updated donation status", "Donation", donationId.ToString(), If(oldStatus Is Nothing, "", oldStatus.ToString()), newStatus)
        LoadDonations()
    End Sub

    Private Sub LoadDonations()
        Dim year As String = SqlDataHelper.GetActiveYear()
        Dim data As DataTable = SqlDataHelper.GetDataTable("SELECT * FROM dbo.Donations WHERE FestivalYear = '" & year & "' ORDER BY DonationDate DESC")
        rptDonations.DataSource = data
        rptDonations.DataBind()
    End Sub
End Class
