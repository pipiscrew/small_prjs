Attribute VB_Name = "ExportDDL"
Option Explicit


Public Sub ExportDatabaseSchema(ByVal dbPath As String, ByVal outputFile As String)
    Dim cat As New ADOX.Catalog
    Dim tbl As ADOX.Table
    Dim col As ADOX.Column
    Dim ky As ADOX.Key
    Dim idx As ADOX.Index
    
    Dim fNum As Integer
    Dim ddlScript As String
    Dim colDetails As String
    
    On Error GoTo ErrorHandler
    cat.ActiveConnection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & dbPath & ";"
    
    ddlScript = "-- ==============================================" & vbCrLf
    ddlScript = ddlScript & "-- DATABASE SCHEMA DDL EXPORT (With Unique/PK Support)" & vbCrLf
    ddlScript = ddlScript & "-- Generated programmatically via VB6" & vbCrLf
    ddlScript = ddlScript & "-- ==============================================" & vbCrLf & vbCrLf

    ' 1. Loop through Tables to generate CREATE TABLE statements
    For Each tbl In cat.tables
        If UCase(tbl.Type) = "TABLE" Then
            ddlScript = ddlScript & "CREATE TABLE [" & tbl.Name & "] (" & vbCrLf
            
            Dim isFirstCol As Boolean
            isFirstCol = True
            
            For Each col In tbl.Columns
                If Not isFirstCol Then ddlScript = ddlScript & "," & vbCrLf
                
                colDetails = "  [" & col.Name & "] " & GetDataTypeString(col.Type, col.DefinedSize)
                
                If col.Attributes And adColNullable Then
                    colDetails = colDetails & " NULL"
                Else
                    colDetails = colDetails & " NOT NULL"
                End If
                
                ddlScript = ddlScript & colDetails
                isFirstCol = False
            Next col
            
            ddlScript = ddlScript & vbCrLf & ");" & vbCrLf & vbCrLf
        End If
    Next tbl
    
    ' 2. Loop through INDEXES for Primary Keys and Unique constraints
    ' Placing these in a second pass avoids constraint sequencing issues
    For Each tbl In cat.tables
        If UCase(tbl.Type) = "TABLE" Then
            For Each idx In tbl.Indexes
                Dim idxCols As String
                Dim i As Integer
                
                ' Build comma-separated list of columns in the index
                idxCols = ""
                For i = 0 To idx.Columns.count - 1
                    If i > 0 Then idxCols = idxCols & ", "
                    idxCols = idxCols & "[" & idx.Columns(i).Name & "]"
                Next i
                
                ' Case A: Primary Key
                If idx.PrimaryKey Then
                    ddlScript = ddlScript & "ALTER TABLE [" & tbl.Name & "] " & vbCrLf
                    ddlScript = ddlScript & "ADD CONSTRAINT [" & idx.Name & "] PRIMARY KEY (" & idxCols & ");" & vbCrLf & vbCrLf
                
                ' Case B: Unique Index (but not the primary key)
                ElseIf idx.Unique Then
                    ddlScript = ddlScript & "ALTER TABLE [" & tbl.Name & "] " & vbCrLf
                    ddlScript = ddlScript & "ADD CONSTRAINT [" & idx.Name & "] UNIQUE (" & idxCols & ");" & vbCrLf & vbCrLf
                End If
            Next idx
        End If
    Next tbl
    
    ' 3. Loop through KEYS for Foreign Keys exclusively
    For Each tbl In cat.tables
        If UCase(tbl.Type) = "TABLE" Then
            For Each ky In tbl.Keys
                If ky.Type = adKeyForeign Then
                    ddlScript = ddlScript & "ALTER TABLE [" & tbl.Name & "] " & vbCrLf
                    ddlScript = ddlScript & "ADD CONSTRAINT [" & ky.Name & "] " & vbCrLf
                    ddlScript = ddlScript & "FOREIGN KEY ([" & ky.Columns(0).Name & "]) " & vbCrLf
                    ddlScript = ddlScript & "REFERENCES [" & ky.RelatedTable & "] ([" & ky.Columns(0).RelatedColumn & "]);" & vbCrLf & vbCrLf
                End If
            Next ky
        End If
    Next tbl

    ' Write out file
    fNum = FreeFile
    Open outputFile For Output As #fNum
    Print #fNum, ddlScript
    Close #fNum
    
    MsgBox "Database schema exported successfully!", vbInformation, "Success"
    Exit Sub

ErrorHandler:
    MsgBox "An error occurred: " & Err.Description, vbCritical, "Error"
    If fNum > 0 Then Close #fNum
End Sub


' Helper function to map ADOX DataTypeEnum values to SQL string syntax
Private Function GetDataTypeString(ByVal dataTypeId As Long, ByVal size As Long) As String
    Select Case dataTypeId
        Case adVarWChar, adVarChar:
            GetDataTypeString = "VARCHAR(" & size & ")"
        Case adLongVarWChar, adLongVarChar:
            GetDataTypeString = "TEXT"
        Case adInteger:
            GetDataTypeString = "INT"
        Case adSmallInt:
            GetDataTypeString = "SMALLINT"
        Case adDouble:
            GetDataTypeString = "DOUBLE"
        Case adSingle:
            GetDataTypeString = "REAL"
        Case adBoolean:
            GetDataTypeString = "BIT"
        Case adDate, adDBTimeStamp:
            GetDataTypeString = "DATETIME"
        Case Else:
            GetDataTypeString = "VARCHAR(255)" ' Fallback
    End Select
End Function
