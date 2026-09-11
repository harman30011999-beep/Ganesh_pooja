Imports System
Imports System.Data
Imports System.Data.SqlClient

Partial Class LoginPage
    Inherits System.Web.UI.Page

    Public Property LoginMessage As String
    Public Property MessageVisible As Boolean

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        SqlDataHelper.EnsureSchema()
    End Sub

    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim username As String = txtUserName.Text.Trim()
        Dim password As String = txtPassword.Text.Trim()

        If String.IsNullOrWhiteSpace(username) OrElse String.IsNullOrWhiteSpace(password) Then
            LoginMessage = "Please enter username and password."
            MessageVisible = True
            Return
        End If

        Dim dt As DataTable = SqlDataHelper.GetDataTable("SELECT TOP 1 * FROM dbo.Users WHERE UserName = '" & username.Replace("'", "''") & "' AND Password = '" & password.Replace("'", "''") & "'")

        If dt.Rows.Count = 0 Then
            LoginMessage = "Invalid username or password."
            MessageVisible = True
            Return
        End If

        Dim userRow As DataRow = dt.Rows(0)
        Session("AuthUser") = userRow("UserName").ToString()
        Session("AuthRole") = userRow("Role").ToString()
        Session("AuthFullName") = userRow("FullName").ToString()

        Response.Redirect("Dashboard.aspx")
    End Sub
End Class
