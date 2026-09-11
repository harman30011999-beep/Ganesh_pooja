Imports System
Imports System.Data
Imports System.Globalization
Imports System.IO
Imports Microsoft.VisualBasic.FileIO

Partial Class EventsPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("AuthUser") Is Nothing Then
            Response.Redirect("Login.aspx")
            Return
        End If

        SqlDataHelper.EnsureSchema()
        If Not IsPostBack Then
            txtEventDate.Text = Date.Today.ToString("yyyy-MM-dd")
            LoadEvents()
        End If
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim eventDate As DateTime
        If String.IsNullOrWhiteSpace(txtEventName.Text) OrElse Not DateTime.TryParse(txtEventDate.Text, eventDate) Then
            ShowMessage("Enter an event name and a valid date.")
            Return
        End If

        InsertEvent(txtEventName.Text, eventDate, txtEventTime.Text, txtVenue.Text, txtDescription.Text, txtPosterUrl.Text)
        ShowMessage("Event added to the public schedule.")
        ClearForm()
        LoadEvents()
    End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As EventArgs)
        If Not fileSchedule.HasFile OrElse Path.GetExtension(fileSchedule.FileName).ToLowerInvariant() <> ".csv" Then
            ShowMessage("Choose a CSV file with the columns shown above.")
            Return
        End If

        Dim imported As Integer = 0
        Using reader As New StreamReader(fileSchedule.PostedFile.InputStream)
            Using parser As New TextFieldParser(reader)
                parser.TextFieldType = FieldType.Delimited
                parser.SetDelimiters(",")
                parser.HasFieldsEnclosedInQuotes = True
                If Not parser.EndOfData Then parser.ReadFields()
                While Not parser.EndOfData
                    Dim fields() As String = parser.ReadFields()
                    If fields.Length >= 2 Then
                        Dim eventDate As DateTime
                        If DateTime.TryParse(fields(1), eventDate) AndAlso Not String.IsNullOrWhiteSpace(fields(0)) Then
                            InsertEvent(fields(0), eventDate, GetField(fields, 2), GetField(fields, 3), GetField(fields, 4), GetField(fields, 5))
                            imported += 1
                        End If
                    End If
                End While
            End Using
        End Using

        ShowMessage(imported.ToString() & " event(s) imported.")
        LoadEvents()
    End Sub

    Private Sub InsertEvent(ByVal eventName As String, ByVal eventDate As DateTime, ByVal eventTime As String, ByVal venue As String, ByVal description As String, ByVal posterUrl As String)
        Dim year As String = SqlDataHelper.GetActiveYear()
        Dim sql As String = String.Format(CultureInfo.InvariantCulture,
            "INSERT INTO dbo.Events (EventName, EventDate, EventTime, Venue, Description, FestivalYear, PosterUrl) VALUES ('{0}', '{1:yyyy-MM-dd}', '{2}', '{3}', '{4}', '{5}', '{6}')",
            Escape(eventName), eventDate, Escape(eventTime), Escape(venue), Escape(description), Escape(year), Escape(posterUrl))
        SqlDataHelper.ExecuteNonQuery(sql)
        SqlDataHelper.LogAudit(Session("AuthUser").ToString(), "Added event", "Event", "", "", eventName)
    End Sub

    Private Sub LoadEvents()
        Dim year As String = SqlDataHelper.GetActiveYear()
        rptEvents.DataSource = SqlDataHelper.GetDataTable("SELECT * FROM dbo.Events WHERE FestivalYear = '" & Escape(year) & "' ORDER BY EventDate, EventTime")
        rptEvents.DataBind()
    End Sub

    Private Sub ClearForm()
        txtEventName.Text = ""
        txtEventTime.Text = ""
        txtVenue.Text = ""
        txtDescription.Text = ""
        txtPosterUrl.Text = ""
    End Sub

    Private Sub ShowMessage(ByVal message As String)
        lblMessage.Text = Server.HtmlEncode(message)
    End Sub

    Private Shared Function GetField(ByVal fields() As String, ByVal index As Integer) As String
        If index < fields.Length Then Return fields(index)
        Return String.Empty
    End Function

    Private Shared Function Escape(ByVal value As String) As String
        Return If(value, String.Empty).Replace("'", "''")
    End Function
End Class
