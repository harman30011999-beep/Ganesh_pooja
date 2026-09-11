Imports System

Partial Class PublicDonationPage
    Inherits System.Web.UI.Page

    Protected Sub btnDonate_Click(ByVal sender As Object, ByVal e As EventArgs)
        SqlDataHelper.EnsureSchema()

        Dim donorName As String = txtDonorName.Text.Trim()
        Dim amount As String = txtAmount.Text.Trim()

        If String.IsNullOrWhiteSpace(donorName) OrElse String.IsNullOrWhiteSpace(amount) Then
            Return
        End If

        Dim year As String = SqlDataHelper.GetActiveYear()
        Dim sql As String = String.Format(
            "INSERT INTO dbo.Donations (DonorName, Phone, Amount, DonationDate, FestivalYear, PaymentMethod, TransactionReference, Status, IsPublic) VALUES ('{0}', '{1}', {2}, '{3}', '{4}', '{5}', '{6}', 'Pending', 1)",
            donorName.Replace("'", "''"),
            txtPhone.Text.Trim().Replace("'", "''"),
            amount,
            Date.Today.ToString("yyyy-MM-dd"),
            year,
            ddlPaymentMethod.SelectedValue.Replace("'", "''"),
            txtReference.Text.Trim().Replace("'", "''"))

        SqlDataHelper.ExecuteNonQuery(sql)
        Response.Redirect("Default.aspx")
    End Sub
End Class
