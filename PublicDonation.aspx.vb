Imports System
Imports System.Data
Imports System.Globalization
Imports System.Web

Partial Class PublicDonationPage
    Inherits System.Web.UI.Page

    Public Property FestivalName As String = "Ganesh Utsav"
    Public Property CommitteeName As String = "Puja Committee"
    Public Property PublicUpiId As String = ""
    Public Property QrUpiId As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        SqlDataHelper.EnsureSchema()
        Dim settings As DataRow = SqlDataHelper.GetCurrentSettings()
        If settings IsNot Nothing Then
            FestivalName = settings("FestivalName").ToString()
            CommitteeName = settings("CommitteeName").ToString()
            PublicUpiId = settings("PublicUpiId").ToString()
            QrUpiId = HttpUtility.JavaScriptStringEncode(PublicUpiId)
        End If
    End Sub

    Protected Sub btnDonate_Click(ByVal sender As Object, ByVal e As EventArgs)
        SqlDataHelper.EnsureSchema()

        Dim donorName As String = txtDonorName.Text.Trim()
        Dim amountValue As Decimal

        If String.IsNullOrWhiteSpace(donorName) OrElse Not Decimal.TryParse(txtAmount.Text.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, amountValue) OrElse amountValue <= 0 Then
            Return
        End If

        Dim amount As String = amountValue.ToString(CultureInfo.InvariantCulture)

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
        pnlDonation.Visible = False
        pnlReceipt.Visible = True
        imgReceiptLogo.ImageUrl = If(String.IsNullOrWhiteSpace(SqlDataHelper.GetCurrentSettings()("LogoUrl").ToString()), "assets/pngtree-ganesha-the-embodiment-of-prosperity-and-joy-png-image_20323342.png", SqlDataHelper.GetCurrentSettings()("LogoUrl").ToString())
        litFestivalName.Text = Server.HtmlEncode(FestivalName)
        litCommitteeName.Text = Server.HtmlEncode(CommitteeName)
        litDonorName.Text = Server.HtmlEncode(donorName)
        litAmount.Text = amountValue.ToString("N2", CultureInfo.GetCultureInfo("en-IN"))
        litPaymentMethod.Text = Server.HtmlEncode(ddlPaymentMethod.SelectedValue)
        litDonationDate.Text = Date.Today.ToString("dd MMM yyyy")
    End Sub
End Class
