Imports System
Imports System.Data

Partial Class CommitteePage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("AuthUser") Is Nothing Then
            Response.Redirect("Login.aspx")
            Return
        End If

        SqlDataHelper.EnsureSchema()

        If Not IsPostBack Then
            LoadMembers()
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim fullName As String = txtFullName.Text.Trim()
        Dim position As String = txtPosition.Text.Trim()
        If String.IsNullOrWhiteSpace(fullName) OrElse String.IsNullOrWhiteSpace(position) Then
            Return
        End If

        Dim year As String = SqlDataHelper.GetActiveYear()
        Dim sql As String = String.Format(
            "INSERT INTO dbo.CommitteeMembers (FullName, Position, Phone, Email, FestivalYear) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')",
            fullName.Replace("'", "''"),
            position.Replace("'", "''"),
            txtPhone.Text.Trim().Replace("'", "''"),
            txtEmail.Text.Trim().Replace("'", "''"),
            year)

        SqlDataHelper.ExecuteNonQuery(sql)
        SqlDataHelper.LogAudit(Session("AuthUser").ToString(), "Added committee member", "CommitteeMember", "", "", fullName)

        txtFullName.Text = ""
        txtPosition.Text = ""
        txtPhone.Text = ""
        txtEmail.Text = ""
        LoadMembers()
    End Sub

    Private Sub LoadMembers()
        Dim year As String = SqlDataHelper.GetActiveYear()
        Dim data As DataTable = SqlDataHelper.GetDataTable("SELECT * FROM dbo.CommitteeMembers WHERE FestivalYear = '" & year & "' ORDER BY MemberId DESC")
        rptMembers.DataSource = data
        rptMembers.DataBind()
    End Sub
End Class
