Imports System
Imports System.Data

Partial Class ExpensesPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("AuthUser") Is Nothing Then
            Response.Redirect("Login.aspx")
            Return
        End If

        SqlDataHelper.EnsureSchema()

        If Not IsPostBack Then
            txtExpenseDate.Text = Date.Today.ToString("yyyy-MM-dd")
            LoadExpenses()
        End If
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim description As String = txtDescription.Text.Trim()
        Dim amount As String = txtAmount.Text.Trim()
        If String.IsNullOrWhiteSpace(description) OrElse String.IsNullOrWhiteSpace(amount) Then
            Return
        End If

        Dim year As String = SqlDataHelper.GetActiveYear()
        Dim sql As String = String.Format(
            "INSERT INTO dbo.Expenses (ExpenseDate, FestivalYear, Category, Description, Amount, PaymentMethod, Vendor, Status) VALUES ('{0}', '{1}', '{2}', '{3}', {4}, 'Cash', '{4}', 'Approved')",
            txtExpenseDate.Text,
            year,
            ddlCategory.SelectedValue.Replace("'", "''"),
            description.Replace("'", "''"),
            amount,
            txtVendor.Text.Trim().Replace("'", "''"))

        SqlDataHelper.ExecuteNonQuery(sql)
        SqlDataHelper.LogAudit(Session("AuthUser").ToString(), "Added expense", "Expense", "", "", description)

        txtDescription.Text = ""
        txtAmount.Text = ""
        txtVendor.Text = ""
        LoadExpenses()
    End Sub

    Private Sub LoadExpenses()
        Dim year As String = SqlDataHelper.GetActiveYear()
        Dim data As DataTable = SqlDataHelper.GetDataTable("SELECT * FROM dbo.Expenses WHERE FestivalYear = '" & year & "' ORDER BY ExpenseDate DESC")
        rptExpenses.DataSource = data
        rptExpenses.DataBind()
    End Sub
End Class
