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
  ddlScript = ddlScript & "-- DATABASE SCHEMA DDL EXPORT (With Clean Index Support)" & vbCrLf
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

  ' 2. Loop through INDEXES (Cleans up PKs, explicit Unique constraints, and Regular Indexes)
  For Each tbl In cat.tables
    If UCase(tbl.Type) = "TABLE" Then
      For Each idx In tbl.Indexes

        ' Skip hidden system-generated indexes for Foreign Keys to avoid messy duplication
        If Left(idx.Name, 11) <> "Reference" And Left(idx.Name, 5) <> "aaaaa" Then

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

            ' Case B: Explicit Unique Index (Custom-made, not matching system PK names)
          ElseIf idx.Unique Then
            ' Filter out Microsoft Access internal primary key tracking names
            If UCase(idx.Name) <> "PRIMARYKEY" Then
              ddlScript = ddlScript & "ALTER TABLE [" & tbl.Name & "] " & vbCrLf
              ddlScript = ddlScript & "ADD CONSTRAINT [" & idx.Name & "] UNIQUE (" & idxCols & ");" & vbCrLf & vbCrLf
            End If

            ' Case C: Regular Index (Non-Unique performance indexes)
          Else
            ddlScript = ddlScript & "CREATE INDEX [" & idx.Name & "] ON [" & tbl.Name & "] (" & idxCols & ");" & vbCrLf & vbCrLf
          End If

        End If
      Next idx
    End If
  Next tbl

  ' 3. Loop through KEYS for Foreign Keys exclusively (Multi-column relationship safe)
  For Each tbl In cat.tables
    If UCase(tbl.Type) = "TABLE" Then
      For Each ky In tbl.Keys
        If ky.Type = adKeyForeign Then
          Dim fkCols As String
          Dim refCols As String
          Dim k As Integer

          fkCols = ""
          refCols = ""

          ' Dynamically stitch together multi-column foreign key combinations if they exist
          For k = 0 To ky.Columns.count - 1
            If k > 0 Then
              fkCols = fkCols & ", "
              refCols = refCols & ", "
            End If
            fkCols = fkCols & "[" & ky.Columns(k).Name & "]"
            refCols = refCols & "[" & ky.Columns(k).RelatedColumn & "]"
          Next k

          ddlScript = ddlScript & "ALTER TABLE [" & tbl.Name & "] " & vbCrLf
          ddlScript = ddlScript & "ADD CONSTRAINT [" & ky.Name & "] " & vbCrLf
          ddlScript = ddlScript & "FOREIGN KEY (" & fkCols & ") " & vbCrLf
          ddlScript = ddlScript & "REFERENCES [" & ky.RelatedTable & "] (" & refCols & ");" & vbCrLf & vbCrLf
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
    GetDataTypeString = "VARCHAR(255)"  ' Fallback
  End Select
End Function



Public Function GetFullCreateTableScript(dbPath As String, tables() As String, ByVal outputFile As String) As String
  Dim cat As New ADOX.Catalog
  Dim tbl As ADOX.Table
  Dim col As ADOX.Column
  Dim idx As ADOX.Index
  Dim ky As ADOX.Key
  Dim sql As String
  Dim colSql As String
  Dim constraintSql As String
  Dim i, w As Integer
  Dim fNum As Integer
  Dim outputSQL$

  ' Open connection to the Jet .mdb file
  cat.ActiveConnection = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & dbPath

  For w = 0 To UBound(tables)
    Dim tableName$
    tableName = tables(w)

    Set tbl = cat.tables(tableName)

    ' 1. START TABLE INITIALIZATION
    sql = "CREATE TABLE [" & tableName & "] (" & vbCrLf

    ' 2. PROCESS ALL COLUMNS (Fields and basic types)
    For Each col In tbl.Columns
      colSql = "  [" & col.Name & "] "

      Select Case col.Type
      Case adVarWChar, adWChar: colSql = colSql & "TEXT(" & col.DefinedSize & ")"
      Case adInteger: colSql = colSql & "LONG"  ' Access 'Integer' is 16-bit, 'Long' is 32-bit standard int
      Case adSmallInt: colSql = colSql & "INTEGER"
      Case adDouble: colSql = colSql & "DOUBLE"
      Case adSingle: colSql = colSql & "SINGLE"
      Case adDate: colSql = colSql & "DATETIME"
      Case adBoolean: colSql = colSql & "YESNO"
      Case adCurrency: colSql = colSql & "CURRENCY"
      Case adLongVarWChar: colSql = colSql & "MEMO"
      Case adBinary, adVarBinary: colSql = colSql & "BINARY"
      Case Else: colSql = colSql & "TEXT(255)"
      End Select

      ' Handle Identity / AutoIncrement columns
      If col.Properties("AutoIncrement").Value = True Then
        ' Replace the type with COUNTER for Access AutoNumber
        colSql = "  [" & col.Name & "] COUNTER"
      End If

      ' Handle Nullability
      If col.Attributes And adColNullable Then
        colSql = colSql & " NULL"
      Else
        colSql = colSql & " NOT NULL"
      End If

      sql = sql & colSql & "," & vbCrLf
    Next col

    ' 3. PROCESS PRIMARY KEYS & FOREIGN KEYS (via tbl.Keys)
    constraintSql = ""
    For Each ky In tbl.Keys
      If ky.Type = adKeyPrimary Then
        ' Primary Key constraint
        constraintSql = constraintSql & "  CONSTRAINT [" & ky.Name & "] PRIMARY KEY ("
        For i = 0 To ky.Columns.count - 1
          constraintSql = constraintSql & "[" & ky.Columns(i).Name & "], "
        Next i
        If Right(constraintSql, 2) = ", " Then constraintSql = Left(constraintSql, Len(constraintSql) - 2)
        constraintSql = constraintSql & ")," & vbCrLf

      ElseIf ky.Type = adKeyForeign Then
        ' Foreign Key constraint
        constraintSql = constraintSql & "  CONSTRAINT [" & ky.Name & "] FOREIGN KEY ("
        For i = 0 To ky.Columns.count - 1
          constraintSql = constraintSql & "[" & ky.Columns(i).Name & "], "
        Next i
        If Right(constraintSql, 2) = ", " Then constraintSql = Left(constraintSql, Len(constraintSql) - 2)

        ' Reference Table and Columns
        constraintSql = constraintSql & ") REFERENCES [" & ky.RelatedTable & "] ("
        For i = 0 To ky.Columns.count - 1
          ' ADOX Foreign Keys use RelatedColumn properties inside the key column definition
          constraintSql = constraintSql & "[" & ky.Columns(i).RelatedColumn & "], "
        Next i
        If Right(constraintSql, 2) = ", " Then constraintSql = Left(constraintSql, Len(constraintSql) - 2)
        constraintSql = constraintSql & ")," & vbCrLf
      End If
    Next ky

    ' Append Constraints if they exist
    If constraintSql <> "" Then
      sql = sql & constraintSql
    End If

    ' Clean up final trailing commas inside the CREATE TABLE statement
    If Right(sql, 3) = "," & vbCrLf Then
      sql = Left(sql, Len(sql) - 3) & vbCrLf
    End If

    sql = sql & ");" & vbCrLf & vbCrLf

    ' 4. PROCESS UNIQUE & SECONDARY INDEXES (via tbl.Indexes)
    ' In Jet/Access SQL, standard and unique indexes are created outside the CREATE TABLE block
    For Each idx In tbl.Indexes
      ' Skip the Primary Key index as it is already handled inside CREATE TABLE
      If idx.PrimaryKey = False Then
        Dim idxType As String
        idxType = "CREATE "

        If idx.Unique = True Then
          idxType = "CREATE UNIQUE "
        End If

        sql = sql & idxType & "INDEX [" & idx.Name & "] ON [" & tableName & "] ("
        For i = 0 To idx.Columns.count - 1
          sql = sql & "[" & idx.Columns(i).Name & "]"
          ' Handle Sort Order (Ascending/Descending)
          If idx.Columns(i).SortOrder = adSortDescending Then
            sql = sql & " DESC"
          End If
          sql = sql & ", "
        Next i
        If Right(sql, 2) = ", " Then sql = Left(sql, Len(sql) - 2)
        sql = sql & ");" & vbCrLf
      End If
    Next idx

    outputSQL = outputSQL & sql & vbCrLf
    ' Clean up objects
    Set tbl = Nothing

  Next w
  Set cat = Nothing
  
  'export
  fNum = FreeFile
  Open outputFile For Output As #fNum
  Print #fNum, outputSQL
  Close #fNum

  GetFullCreateTableScript = sql
End Function


