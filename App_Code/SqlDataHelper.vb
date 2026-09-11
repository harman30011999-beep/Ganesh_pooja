Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient

Public Class SqlDataHelper
    Public Shared Function GetConnection() As SqlConnection
        Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("BuildingPlanDB").ConnectionString)
        Return conn
    End Function

    Public Shared Sub EnsureSchema()
        Dim createSql As String = " IF OBJECT_ID('dbo.Settings', 'U') IS NULL " &
          "  BEGIN " &
          "      CREATE TABLE dbo.Settings ( " &
          "          Id INT IDENTITY(1,1) PRIMARY KEY, " &
          "          FestivalName NVARCHAR(200) NOT NULL DEFAULT 'Ganesh Utsav', " &
          "          CommitteeName NVARCHAR(200) NOT NULL DEFAULT 'Puja Committee', " &
          "          PublicUpiId NVARCHAR(100) NULL, " &
          "          PublicDonationEnabled BIT NOT NULL DEFAULT 1, " &
          "          OpeningBalance DECIMAL(18,2) NOT NULL DEFAULT 0, " &
          "          PublicTransparency NVARCHAR(500) NULL DEFAULT 'openingBalance=1|confirmedDonations=1|approvedExpenses=1|currentBalance=1', " &
          "          LandingHeadline NVARCHAR(200) NULL, " &
          "          LandingSubheadline NVARCHAR(400) NULL, " &
          "          LogoUrl NVARCHAR(500) NULL, " &
          "          CreatedAt DATETIME NOT NULL DEFAULT GETDATE() " &
          "      ) " &
          "  END " &
          " " &
          "  IF OBJECT_ID('dbo.Users', 'U') IS NULL " &
          "  BEGIN " &
          "      CREATE TABLE dbo.Users ( " &
          "          UserId INT IDENTITY(1,1) PRIMARY KEY, " &
          "          UserName NVARCHAR(100) NOT NULL, " &
          "          Password NVARCHAR(200) NOT NULL, " &
          "          FullName NVARCHAR(200) NOT NULL, " &
          "          Role NVARCHAR(100) NOT NULL DEFAULT 'Admin', " &
          "          CreatedAt DATETIME NOT NULL DEFAULT GETDATE() " &
          "      ) " &
          "  END " &
          " " &
          "  IF OBJECT_ID('dbo.Years', 'U') IS NULL " &
          "  BEGIN " &
          "      CREATE TABLE dbo.Years ( " &
          "          Id INT IDENTITY(1,1) PRIMARY KEY, " &
          "          FestivalYear NVARCHAR(50) NOT NULL, " &
          "          DisplayName NVARCHAR(200) NOT NULL, " &
          "          Status NVARCHAR(50) NOT NULL DEFAULT 'Planning', " &
          "          OpeningBalance DECIMAL(18,2) NOT NULL DEFAULT 0, " &
          "          StartDate DATE NULL, " &
          "          EndDate DATE NULL, " &
          "          CreatedAt DATETIME NOT NULL DEFAULT GETDATE() " &
          "      ) " &
          "  END " &
          " " &
          "  IF OBJECT_ID('dbo.Donations', 'U') IS NULL " &
          "  BEGIN " &
          "      CREATE TABLE dbo.Donations ( " &
          "          DonationId INT IDENTITY(1,1) PRIMARY KEY, " &
          "          DonorName NVARCHAR(200) NOT NULL, " &
          "          Phone NVARCHAR(50) NULL, " &
          "          Email NVARCHAR(200) NULL, " &
          "          Address NVARCHAR(500) NULL, " &
          "          Amount DECIMAL(18,2) NOT NULL DEFAULT 0, " &
          "          DonationDate DATE NOT NULL, " &
          "          FestivalYear NVARCHAR(50) NOT NULL, " &
          "          PaymentMethod NVARCHAR(50) NOT NULL, " &
          "          TransactionReference NVARCHAR(200) NULL, " &
          "          ReceiptNumber NVARCHAR(100) NULL, " &
          "          Notes NVARCHAR(MAX) NULL, " &
          "          Status NVARCHAR(50) NOT NULL DEFAULT 'Pending', " &
          "          IsPublic BIT NOT NULL DEFAULT 0, " &
          "          CreatedAt DATETIME NOT NULL DEFAULT GETDATE() " &
          "      ) " &
          "  END " &
          " " &
          "  IF OBJECT_ID('dbo.Expenses', 'U') IS NULL " &
          "  BEGIN " &
          "      CREATE TABLE dbo.Expenses ( " &
          "          ExpenseId INT IDENTITY(1,1) PRIMARY KEY, " &
          "          ExpenseDate DATE NOT NULL, " &
          "          FestivalYear NVARCHAR(50) NOT NULL, " &
          "          Category NVARCHAR(100) NOT NULL, " &
          "          Description NVARCHAR(300) NOT NULL, " &
          "          Amount DECIMAL(18,2) NOT NULL DEFAULT 0, " &
          "          PaymentMethod NVARCHAR(50) NOT NULL, " &
          "          Vendor NVARCHAR(200) NULL, " &
          "          BillReference NVARCHAR(200) NULL, " &
          "          Notes NVARCHAR(MAX) NULL, " &
          "          Status NVARCHAR(50) NOT NULL DEFAULT 'Draft', " &
          "          CreatedAt DATETIME NOT NULL DEFAULT GETDATE() " &
          "      ) " &
          "  END " &
          " " &
          "  IF OBJECT_ID('dbo.CommitteeMembers', 'U') IS NULL " &
          "  BEGIN " &
          "      CREATE TABLE dbo.CommitteeMembers ( " &
          "          MemberId INT IDENTITY(1,1) PRIMARY KEY, " &
          "          FullName NVARCHAR(200) NOT NULL, " &
          "          Phone NVARCHAR(50) NULL, " &
          "          Email NVARCHAR(200) NULL, " &
          "          Position NVARCHAR(100) NOT NULL, " &
          "          JoiningDate DATE NULL, " &
          "          Status NVARCHAR(50) NOT NULL DEFAULT 'Active', " &
          "          RolePermission NVARCHAR(100) NOT NULL DEFAULT 'Support', " &
          "          PhotoUrl NVARCHAR(500) NULL, " &
          "          FestivalYear NVARCHAR(50) NOT NULL, " &
          "          CreatedAt DATETIME NOT NULL DEFAULT GETDATE() " &
          "      ) " &
          "  END " &
          " " &
          "  IF OBJECT_ID('dbo.Events', 'U') IS NULL " &
          "  BEGIN " &
          "      CREATE TABLE dbo.Events ( " &
          "          EventId INT IDENTITY(1,1) PRIMARY KEY, " &
          "          EventName NVARCHAR(200) NOT NULL, " &
          "          EventDate DATE NOT NULL, " &
          "          EventTime NVARCHAR(50) NULL, " &
          "          Venue NVARCHAR(200) NULL, " &
          "          Description NVARCHAR(MAX) NULL, " &
          "          FestivalYear NVARCHAR(50) NOT NULL, " &
          "          PosterUrl NVARCHAR(500) NULL, " &
          "          CreatedAt DATETIME NOT NULL DEFAULT GETDATE() " &
          "      ) " &
          "  END " &
          " " &
          "  IF OBJECT_ID('dbo.Gallery', 'U') IS NULL " &
          "  BEGIN " &
          "      CREATE TABLE dbo.Gallery ( " &
          "          GalleryId INT IDENTITY(1,1) PRIMARY KEY, " &
          "          AlbumTitle NVARCHAR(200) NOT NULL, " &
          "          FestivalYear NVARCHAR(50) NOT NULL, " &
          "          Visibility NVARCHAR(50) NOT NULL DEFAULT 'Public', " &
          "          CoverImage NVARCHAR(500) NULL, " &
          "          Photos NVARCHAR(MAX) NULL, " &
          "          Description NVARCHAR(500) NULL, " &
          "          CreatedAt DATETIME NOT NULL DEFAULT GETDATE() " &
          "      ) " &
          "  END " &
          " " &
          "  IF OBJECT_ID('dbo.Documents', 'U') IS NULL " &
          "  BEGIN " &
          "      CREATE TABLE dbo.Documents ( " &
          "          DocumentId INT IDENTITY(1,1) PRIMARY KEY, " &
          "          DocumentTitle NVARCHAR(200) NOT NULL, " &
          "          DocumentType NVARCHAR(100) NOT NULL, " &
          "          FestivalYear NVARCHAR(50) NOT NULL, " &
          "          FileName NVARCHAR(200) NULL, " &
          "          FileUrl NVARCHAR(500) NULL, " &
          "          Notes NVARCHAR(500) NULL, " &
          "          CreatedAt DATETIME NOT NULL DEFAULT GETDATE() " &
          "      ) " &
          "  END " &
          " " &
          "  IF OBJECT_ID('dbo.AuditLog', 'U') IS NULL " &
          "  BEGIN " &
          "      CREATE TABLE dbo.AuditLog ( " &
          "          AuditId INT IDENTITY(1,1) PRIMARY KEY, " &
          "          UserName NVARCHAR(100) NOT NULL, " &
          "          ActionPerformed NVARCHAR(200) NOT NULL, " &
          "          RecordType NVARCHAR(100) NULL, " &
          "          RecordId NVARCHAR(100) NULL, " &
          "          PreviousValue NVARCHAR(MAX) NULL, " &
          "          NewValue NVARCHAR(MAX) NULL, " &
          "          CreatedAt DATETIME NOT NULL DEFAULT GETDATE() " &
          "      ) " &
          "  END "

        ExecuteNonQuery(createSql)

        Dim alterSql As String = " IF OBJECT_ID('dbo.Settings', 'U') IS NOT NULL " &
          "  BEGIN " &
          "      IF COL_LENGTH('dbo.Settings', 'LandingBannerImage') IS NULL ALTER TABLE dbo.Settings ADD LandingBannerImage NVARCHAR(500) NULL; " &
          "      IF COL_LENGTH('dbo.Settings', 'AboutTitle') IS NULL ALTER TABLE dbo.Settings ADD AboutTitle NVARCHAR(300) NULL; " &
          "      IF COL_LENGTH('dbo.Settings', 'AboutDescription') IS NULL ALTER TABLE dbo.Settings ADD AboutDescription NVARCHAR(MAX) NULL; " &
          "      IF COL_LENGTH('dbo.Settings', 'FestivalStartDate') IS NULL ALTER TABLE dbo.Settings ADD FestivalStartDate DATE NULL; " &
          "      IF COL_LENGTH('dbo.Settings', 'FestivalEndDate') IS NULL ALTER TABLE dbo.Settings ADD FestivalEndDate DATE NULL; " &
          "  END  "
        ExecuteNonQuery(alterSql)

        Dim settingsCount As Integer = CInt(ExecuteScalar("SELECT COUNT(*) FROM dbo.Settings"))
        If settingsCount = 0 Then
            ExecuteNonQuery("INSERT INTO dbo.Settings (FestivalName, CommitteeName, PublicUpiId, PublicDonationEnabled, OpeningBalance, PublicTransparency, LandingHeadline, LandingSubheadline, LogoUrl, LandingBannerImage, AboutTitle, AboutDescription, FestivalStartDate, FestivalEndDate) VALUES ('Ganesh Utsav', 'Puja Committee', 'committee@upi', 1, 200000, 'openingBalance=1|confirmedDonations=1|approvedExpenses=1|currentBalance=1', 'Celebrate devotion, community and generosity.', 'A modern committee management platform for Ganesh Utsav.', 'assets/pngtree-ganesha-the-embodiment-of-prosperity-and-joy-png-image_20323342.png', 'assets/pngtree-ganesha-the-embodiment-of-prosperity-and-joy-png-image_20323342.png', 'A vibrant celebration rooted in devotion and unity', 'Our committee brings together families, volunteers and devotees to organize a spiritually uplifting, transparent and community-centered Ganesh Utsav.', '2026-07-01', '2026-10-31')")
        Else
            ExecuteNonQuery("UPDATE dbo.Settings SET LandingBannerImage = ISNULL(LandingBannerImage, 'assets/pngtree-ganesha-the-embodiment-of-prosperity-and-joy-png-image_20323342.png'), AboutTitle = ISNULL(AboutTitle, 'A vibrant celebration rooted in devotion and unity'), AboutDescription = ISNULL(AboutDescription, 'Our committee brings together families, volunteers and devotees to organize a spiritually uplifting, transparent and community-centered Ganesh Utsav.'), FestivalStartDate = ISNULL(FestivalStartDate, '2026-07-01'), FestivalEndDate = ISNULL(FestivalEndDate, '2026-10-31') WHERE Id = (SELECT TOP 1 Id FROM dbo.Settings ORDER BY Id DESC)")
        End If

        Dim yearCount As Integer = CInt(ExecuteScalar("SELECT COUNT(*) FROM dbo.Years"))
        If yearCount = 0 Then
            ExecuteNonQuery("INSERT INTO dbo.Years (FestivalYear, DisplayName, Status, OpeningBalance, StartDate, EndDate) VALUES ('2026', 'Ganesh Utsav 2026', 'Active', 200000, '2026-07-01', '2026-10-31')")
            ExecuteNonQuery("INSERT INTO dbo.Years (FestivalYear, DisplayName, Status, OpeningBalance, StartDate, EndDate) VALUES ('2025', 'Ganesh Utsav 2025', 'Closed', 150000, '2025-07-01', '2025-10-31')")
        End If

        Dim userCount As Integer = CInt(ExecuteScalar("SELECT COUNT(*) FROM dbo.Users"))
        If userCount = 0 Then
            ExecuteNonQuery("INSERT INTO dbo.Users (UserName, Password, FullName, Role) VALUES ('admin', 'admin123', 'Festival Admin', 'Admin')")
        End If
    End Sub

    Public Shared Function GetDataTable(sql As String) As DataTable
        Dim dt As New DataTable()
        Dim conn As SqlConnection = GetConnection()
        Try
            conn.Open()
            Using cmd As New SqlCommand(sql, conn)
                Using reader As SqlDataReader = cmd.ExecuteReader()
                    dt.Load(reader)
                End Using
            End Using
        Finally
            conn.Close()
        End Try
        Return dt
    End Function

    Public Shared Function ExecuteScalar(sql As String) As Object
        Dim result As Object = Nothing
        Dim conn As SqlConnection = GetConnection()
        Try
            conn.Open()
            Using cmd As New SqlCommand(sql, conn)
                result = cmd.ExecuteScalar()
            End Using
        Catch
            result = Nothing
        Finally
            conn.Close()
        End Try
        Return result
    End Function

    Public Shared Sub ExecuteNonQuery(sql As String)
        Dim conn As SqlConnection = GetConnection()
        Try
            conn.Open()
            Using cmd As New SqlCommand(sql, conn)
                cmd.ExecuteNonQuery()
            End Using
        Finally
            conn.Close()
        End Try
    End Sub

    Public Shared Function GetCurrentSettings() As DataRow
        Dim dt As DataTable = GetDataTable("SELECT TOP 1 * FROM dbo.Settings ORDER BY Id DESC")
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)
        End If
        Return Nothing
    End Function

    Public Shared Function GetActiveYear() As String
        Dim activeYearDt As DataTable = GetDataTable("SELECT TOP 1 FestivalYear FROM dbo.Years WHERE Status = 'Active' ORDER BY Id DESC")
        If activeYearDt.Rows.Count > 0 Then
            Return activeYearDt.Rows(0)(0).ToString()
        End If

        Dim latestYearDt As DataTable = GetDataTable("SELECT TOP 1 FestivalYear FROM dbo.Years ORDER BY Id DESC")
        If latestYearDt.Rows.Count > 0 Then
            Return latestYearDt.Rows(0)(0).ToString()
        End If

        Return String.Empty
    End Function

    Public Shared Sub LogAudit(userName As String, actionPerformed As String, recordType As String, recordId As String, previousValue As String, newValue As String)
        Dim sql As String = String.Format("INSERT INTO dbo.AuditLog (UserName, ActionPerformed, RecordType, RecordId, PreviousValue, NewValue) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
            userName.Replace("'", "''"),
            actionPerformed.Replace("'", "''"),
            recordType.Replace("'", "''"),
            recordId.Replace("'", "''"),
            previousValue.Replace("'", "''"),
            newValue.Replace("'", "''"))
        ExecuteNonQuery(sql)
    End Sub
End Class
