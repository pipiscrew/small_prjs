Attribute VB_Name = "General"
Option Explicit

Private masterSchema As Collection

Public Function GetRecordSet(ByVal filepath$, ByVal SQL$) As ADODB.Recordset
  On Error GoTo ErrLoop

  Dim rs As ADODB.Recordset

  Set rs = New ADODB.Recordset

  rs.Open SQL, "Data Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & filepath, adOpenStatic, adLockOptimistic

  Set GetRecordSet = rs

  Exit Function
ErrLoop:
  MsgBox Err.Description & vbCrLf & vbCrLf & "The program now exit....Sorry!", vbCritical, "": End
End Function

Public Function CloneTableByTBL1toTBL2(ByVal sourceDbFilepath$, ByVal destDbFilepath$, ByVal deleteIfExists As Boolean, tables As Collection)
  Dim conn As ADODB.Connection
  Dim TableName As String
  Dim sqlCreate As String
  Dim sqlDelete As String
  Dim sourceTableName, destTableName As String
  Dim i%

  Set conn = New ADODB.Connection
  conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & destDbFilepath & ";"
  conn.Open

  Dim tInfo As clsTableInfo
  For Each tInfo In tables
    Debug.Print tInfo.TableName
    sourceTableName = tInfo.TableName
    destTableName = tInfo.TableName

    If (deleteIfExists) Then
      On Error Resume Next

      sqlDelete = "DROP TABLE [" & sourceTableName & "]"
      conn.Execute sqlDelete

      If Err.Number <> 0 Then
        Debug.Print "Error deleting table: " & sourceTableName & " - " & Err.Description
        Err.Clear  ' Clear the error
      End If

      On Error GoTo 0  ' Resume normal error handling
    Else
      destTableName = tInfo.TableName & GetToday
    End If

    On Error Resume Next

    ' COPY ROWS FROM SOURCE DBASE
    sqlCreate = "SELECT * INTO " & destTableName & " FROM [" & sourceTableName & "] IN '" & sourceDbFilepath & "';"
    conn.Execute sqlCreate

    ' ADD PK
    If (IsNullOrEmpty(tInfo.PK) = False) Then
      conn.Execute tInfo.PK
    End If

    If Err.Number <> 0 Then
      MsgBox Err.Description, vbExclamation
      Err.Clear  ' Clear the error
    End If

    'ADD FK
    If (IsNullOrEmpty(tInfo.FK) = False) Then
      conn.Execute tInfo.FK
    End If

    If Err.Number <> 0 Then
      MsgBox Err.Description, vbExclamation
      Err.Clear  ' Clear the error
    End If

    'ADD UNIQUE
    If (IsNullOrEmpty(tInfo.Unique) = False) Then
      conn.Execute tInfo.Unique
    End If

    If Err.Number <> 0 Then
      MsgBox Err.Description, vbExclamation
      Err.Clear  ' Clear the error
    End If

    On Error GoTo 0  ' Resume normal error handling

  Next tInfo

  '  For i = LBound(tables) To UBound(tables)
  '    Debug.Print tables(i, 0)
  '
  '    sourceTableName = tables(i, 0)
  '    destTableName = tables(i, 0)
  '
  '    If (deleteIfExists) Then
  '      On Error Resume Next
  '
  '      sqlDelete = "DROP TABLE [" & sourceTableName & "]"
  '      conn.Execute sqlDelete
  '
  '      If Err.Number <> 0 Then
  '        Debug.Print "Error deleting table: " & sourceTableName & " - " & Err.Description
  '        Err.Clear  ' Clear the error
  '      End If
  '
  '      On Error GoTo 0  ' Resume normal error handling
  '    Else
  '      destTableName = tables(i, 0) & GetToday
  '    End If
  '
  '    On Error Resume Next
  '
  '    ' COPY ROWS FROM SOURCE DBASE
  '    sqlCreate = "SELECT * INTO " & destTableName & " FROM [" & sourceTableName & "] IN '" & sourceDbFilepath & "';"
  '    conn.Execute sqlCreate
  '
  '    ' ADD PK
  '    If (IsNullOrEmpty(tables(i, 2)) = False) Then
  '      sqlCreate = "ALTER TABLE [" & destTableName & "] ADD CONSTRAINT [PrimaryKey] PRIMARY KEY ([" & tables(i, 2) & "]);"
  '
  '      conn.Execute sqlCreate
  '    End If
  '
  '    If Err.Number <> 0 Then
  '      MsgBox Err.Description, vbExclamation
  '      Err.Clear  ' Clear the error
  '    End If
  '
  '    On Error GoTo 0  ' Resume normal error handling
  '  Next i

  conn.Close
  Set conn = Nothing
  MsgBox "Tables cloned!", vbInformation
End Function

Public Sub AddTablesToListview(ByRef lstv As ListView, ByVal filepath$, ByVal store2masterSchema As Boolean)
  Dim tInfo As clsTableInfo
  Dim mySchema As Collection

  lstv.ListItems.Clear

  Set mySchema = GetDatabaseSchemaCollection(filepath)

  If mySchema Is Nothing Then Exit Sub

  For Each tInfo In mySchema
    Debug.Print "========================================"
    Debug.Print "Table Name: " & tInfo.TableName
    Debug.Print "Row Count : " & tInfo.RecordCount
    Debug.Print "PK Field  : " & IIf(tInfo.PK = "", "[None]", tInfo.PK)
    Debug.Print "FK Field  : " & IIf(tInfo.FK = "", "[None]", tInfo.FK)
    Debug.Print "Unique    : " & IIf(tInfo.Unique = "", "[None]", tInfo.Unique)

    lstv.ListItems.Add , , tInfo.TableName
    lstv.ListItems(lstv.ListItems.count).ListSubItems.Add , , tInfo.RecordCount
    lstv.ListItems(lstv.ListItems.count).ListSubItems.Add , , tInfo.PK


  Next tInfo

  If (store2masterSchema) Then

    Set masterSchema = mySchema
  End If
  '  Dim tableCounts As Variant
  '  Dim i As Long
  '
  '  lstv.ListItems.Clear
  '
  '  tableCounts = GetTableNamesAndCounts(filepath)
  '
  '  ' Print out the table names and row counts
  '  For i = LBound(tableCounts, 1) To UBound(tableCounts, 1)
  '    'If (tableCounts(i, 1) > 0) Then
  '    lstv.ListItems.Add , , tableCounts(i, 0)
  '    lstv.ListItems(lstv.ListItems.count).ListSubItems.Add , , tableCounts(i, 1)
  '    lstv.ListItems(lstv.ListItems.count).ListSubItems.Add , , tableCounts(i, 2)
  '    'End If
  '  Next i
End Sub

Private Function GetTableNamesAndCounts(ByVal dbPath As String) As Variant
  Dim conn As ADODB.Connection
  Dim rs, tempRS As ADODB.Recordset
  Dim TableInfo() As Variant
  Dim tableCount As Long
  Dim query As String
  Dim i As Long
  Dim pkName As String

  ' Initialize the connection
  Set conn = New ADODB.Connection
  conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & dbPath & ";"
  conn.Open

  ' Open a schema recordset to get table names
  Set rs = conn.OpenSchema(adSchemaTables)

  ' First, count how many tables there are
  tableCount = 0
  Do While Not rs.EOF
    If rs.Fields("TABLE_TYPE").Value = "TABLE" Then
      tableCount = tableCount + 1
    End If
    rs.MoveNext
  Loop

  ' Redim the array to hold table names, counts, and PK names
  ReDim TableInfo(0 To tableCount - 1, 0 To 2)

  ' Reset the recordset to the beginning
  rs.MoveFirst
  tableCount = 0

  ' Retrieve table names and row counts
  Do While Not rs.EOF
    If rs.Fields("TABLE_TYPE").Value = "TABLE" Then
      TableInfo(tableCount, 0) = rs.Fields("TABLE_NAME").Value

      ' Count the number of rows in the table
      query = "SELECT COUNT(*) AS TotalRows FROM [" & TableInfo(tableCount, 0) & "]"
      Set tempRS = conn.Execute(query)
      TableInfo(tableCount, 1) = tempRS(0).Value
      tempRS.Close

      ' Get the primary key name
      pkName = GetPrimaryKeyName(dbPath, TableInfo(tableCount, 0))
      TableInfo(tableCount, 2) = pkName

      tableCount = tableCount + 1
    End If
    rs.MoveNext
  Loop

  ' Clean up
  rs.Close
  conn.Close
  Set rs = Nothing
  Set conn = Nothing
  Set tempRS = Nothing

  ' Return the 3D array of table names, counts, and PK names
  GetTableNamesAndCounts = TableInfo
End Function

Private Function GetPrimaryKeyName(ByVal dbPath As String, ByVal TableName As String) As String
  Dim conn As ADODB.Connection
  Dim rs As ADODB.Recordset
  Dim keyName As String

  Set conn = New ADODB.Connection
  conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & dbPath & ";"
  conn.Open

  Set rs = conn.OpenSchema(adSchemaPrimaryKeys, Array(Empty, Empty, TableName))

  If Not rs.EOF Then
    keyName = rs.Fields("COLUMN_NAME").Value
  End If

  rs.Close
  conn.Close

  GetPrimaryKeyName = keyName
End Function

Public Function GetCheckListivewItems(ByVal lst As ListView) As Variant
  Dim checkedItems() As Variant
  Dim count As Long
  Dim i As Long
  Dim subItemCount As Long

  ' Initialize counter
  count = 0

  ' First, count how many checked items there are
  For i = 1 To lst.ListItems.count
    If lst.ListItems(i).Checked Then
      count = count + 1
    End If
  Next i

  If (count = 0) Then
    GetCheckListivewItems = checkedItems
    Exit Function
  End If

  ' Get number of subitems
  If lst.ListItems.count > 0 Then
    subItemCount = lst.ListItems(1).ListSubItems.count
  End If

  ' Resize the 2D array to hold checked items and their subitems
  ReDim checkedItems(0 To count - 1, 0 To subItemCount)  ' First dimension for items, second for subitems

  ' Reset counter
  count = 0

  ' Now populate the array with the checked items' texts and their subitem values
  For i = 1 To lst.ListItems.count
    If lst.ListItems(i).Checked Then
      checkedItems(count, 0) = lst.ListItems(i).Text
      Dim j As Long
      For j = 1 To subItemCount
        checkedItems(count, j) = lst.ListItems(i).ListSubItems(j).Text
      Next j
      count = count + 1
    End If
  Next i

  ' Optional: Print the checked items to debug
  '  For i = LBound(checkedItems) To UBound(checkedItems, 1)
  '    Dim output As String
  '    output = "Item: " & checkedItems(i, 0) & " | Subitems: "
  '    For j = 1 To UBound(checkedItems, 2)
  '      output = output & checkedItems(i, j) & ", "
  '    Next j

  'Next i

  GetCheckListivewItems = checkedItems
End Function


Public Function GetToday() As String
  Dim today As Date
  Dim formattedDate As String

  today = Now()

  formattedDate = Year(today) & Format(Month(today), "00") & Format(Day(today), "00") & _
                  Format(Hour(today), "00") & Format(Minute(today), "00") & Format(Second(today), "00")

  GetToday = formattedDate
End Function

Public Function IsNullOrEmpty(ByVal str As Variant) As Boolean
  If IsNull(str) Or Trim(str) = "" Then
    IsNullOrEmpty = True
  Else
    IsNullOrEmpty = False
  End If
End Function

Public Function IsArrayEmpty(arr() As Variant) As Boolean
  IsArrayEmpty = ((Not arr) = -1)
End Function


Public Function GetCheckedTableInfo(ByVal lv As ListView) As Collection
  Dim checkedCollection As New Collection
  Dim itm As ListItem
  Dim tInfo As clsTableInfo
  Dim tblKey As String

  On Error GoTo ErrorHandler

  ' Loop through every row in the ListView
  For Each itm In lv.ListItems
    ' Check if the row's checkbox is ticked
    If itm.Checked Then
      tblKey = itm.Text

      If Len(tblKey) > 0 Then
        ' Safe lookup directly from the collection using the text key
        On Error Resume Next
        Set tInfo = masterSchema(tblKey)
        On Error GoTo ErrorHandler

        ' Add to our checked output collection if found
        If Not tInfo Is Nothing Then
          checkedCollection.Add tInfo, tInfo.TableName
          Set tInfo = Nothing  ' Reset for next iteration
        Else
          MsgBox tblKey & " not found in the collection MASTER, help!"
        End If
      End If
    End If
  Next itm

  Set GetCheckedTableInfo = checkedCollection
  Exit Function

ErrorHandler:
  MsgBox "Error processing selections: " & Err.Description, vbCritical
  Set GetCheckedTableInfo = New Collection
End Function



Public Function GetDatabaseSchemaCollection(ByVal dbPath As String) As Collection
  Dim conn As New ADODB.Connection
  Dim rsTables As ADODB.Recordset
  Dim rsIdx As ADODB.Recordset
  Dim rsFKs As ADODB.Recordset
  Dim rsCount As ADODB.Recordset

  Dim tblSchema As Collection
  Dim tInfo As clsTableInfo
  Dim currentTable As String

  On Error GoTo ErrorHandler

  conn.Open "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & dbPath & ";"
  Set tblSchema = New Collection

  ' 1. Step A: Gather all User Tables and calculate Record Counts
  Set rsTables = conn.OpenSchema(adSchemaTables, Array(Empty, Empty, Empty, "TABLE"))

  Do While Not rsTables.EOF
    currentTable = rsTables("TABLE_NAME").Value

    If Left(currentTable, 4) <> "MSys" Then
      Set tInfo = New clsTableInfo
      tInfo.TableName = currentTable

      Set rsCount = conn.Execute("SELECT COUNT(*) FROM [" & currentTable & "]")
      If Not rsCount.EOF Then
        tInfo.RecordCount = CLng(rsCount(0).Value)
      End If
      rsCount.Close

      tblSchema.Add tInfo, currentTable
    End If

    rsTables.MoveNext
  Loop
  rsTables.Close

  ' 2. Step B: Extract Primary Key and Unique fields (First single field only)
  For Each tInfo In tblSchema
    currentTable = tInfo.TableName
    Set rsIdx = conn.OpenSchema(adSchemaIndexes, Array(Empty, Empty, Empty, Empty, currentTable))

    Do While Not rsIdx.EOF
      ' Assigning to properties instantly generates the ALTER statement strings
      If rsIdx("PRIMARY_KEY").Value = True And tInfo.PK = "" Then
        tInfo.PK = rsIdx("COLUMN_NAME").Value

      ElseIf rsIdx("UNIQUE").Value = True And tInfo.Unique = "" And rsIdx("PRIMARY_KEY").Value = False Then
        If Left(rsIdx("INDEX_NAME").Value, 10) <> "Reference" Then
          tInfo.Unique = rsIdx("COLUMN_NAME").Value
        End If
      End If

      rsIdx.MoveNext
    Loop
    rsIdx.Close
  Next tInfo

  ' 3. Step C: Extract Foreign Keys (First single field only)
  Set rsFKs = conn.OpenSchema(adSchemaForeignKeys, Array(Empty, Empty, Empty, Empty, Empty, Empty))

  Do While Not rsFKs.EOF
    Dim fkTable As String: fkTable = rsFKs("FK_TABLE_NAME").Value

    On Error Resume Next
    Set tInfo = tblSchema(fkTable)
    On Error GoTo ErrorHandler

    If Not tInfo Is Nothing Then
      If tInfo.FK = "" Then
        ' Build a temporary piped string to pass the relationship details to the class property
        Dim pipeDetails As String
        pipeDetails = rsFKs("FK_COLUMN_NAME").Value & "|" & _
                      rsFKs("PK_TABLE_NAME").Value & "|" & _
                      rsFKs("PK_COLUMN_NAME").Value

        tInfo.FK = pipeDetails
      End If
    End If

    rsFKs.MoveNext
  Loop
  rsFKs.Close

  conn.Close
  Set conn = Nothing
  Set GetDatabaseSchemaCollection = tblSchema
  Exit Function

ErrorHandler:
  MsgBox "Error reading schema: " & Err.Description, vbCritical, "Error"
  Set GetDatabaseSchemaCollection = Nothing
End Function



