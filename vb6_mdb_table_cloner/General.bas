Attribute VB_Name = "General"
Option Explicit

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

Public Function CloneTableByTBL1toTBL2(ByVal sourceDbFilepath$, ByVal destDbFilepath$, ByVal deleteIfExists As Boolean, tables() As Variant)
  Dim conn As ADODB.Connection
  Dim tableName As String
  Dim sqlCreate As String
  Dim sqlDelete As String
  Dim sourceTableName, destTableName As String
  Dim i%

  Set conn = New ADODB.Connection
  conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & destDbFilepath & ";"
  conn.Open

  For i = LBound(tables) To UBound(tables)
    Debug.Print tables(i, 0)

    sourceTableName = tables(i, 0)
    destTableName = tables(i, 0)

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
      destTableName = tables(i, 0) & GetToday
    End If

    On Error Resume Next

    ' COPY ROWS FROM SOURCE DBASE
    sqlCreate = "SELECT * INTO " & destTableName & " FROM [" & sourceTableName & "] IN '" & sourceDbFilepath & "';"
    conn.Execute sqlCreate

    ' ADD PK
    If (IsNullOrEmpty(tables(i, 2)) = False) Then
      sqlCreate = "ALTER TABLE [" & destTableName & "] ADD CONSTRAINT [PrimaryKey] PRIMARY KEY ([" & tables(i, 2) & "]);"

      conn.Execute sqlCreate
    End If

    If Err.Number <> 0 Then
      MsgBox Err.Description, vbExclamation
      Err.Clear  ' Clear the error
    End If

    On Error GoTo 0  ' Resume normal error handling
  Next i

  conn.Close
  Set conn = Nothing
  MsgBox "Tables cloned!", vbInformation
End Function

Public Sub AddTablesToListview(ByRef lstv As ListView, ByVal filepath$)
  Dim tableCounts As Variant
  Dim i As Long

  lstv.ListItems.Clear

  tableCounts = GetTableNamesAndCounts(filepath)

  ' Print out the table names and row counts
  For i = LBound(tableCounts, 1) To UBound(tableCounts, 1)
    'If (tableCounts(i, 1) > 0) Then
    lstv.ListItems.Add , , tableCounts(i, 0)
    lstv.ListItems(lstv.ListItems.count).ListSubItems.Add , , tableCounts(i, 1)
    lstv.ListItems(lstv.ListItems.count).ListSubItems.Add , , tableCounts(i, 2)
    'End If
  Next i
End Sub

Private Function GetTableNamesAndCounts(ByVal dbPath As String) As Variant
  Dim conn As ADODB.Connection
  Dim rs, tempRS As ADODB.Recordset
  Dim tableInfo() As Variant
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
  ReDim tableInfo(0 To tableCount - 1, 0 To 2)

  ' Reset the recordset to the beginning
  rs.MoveFirst
  tableCount = 0

  ' Retrieve table names and row counts
  Do While Not rs.EOF
    If rs.Fields("TABLE_TYPE").Value = "TABLE" Then
      tableInfo(tableCount, 0) = rs.Fields("TABLE_NAME").Value

      ' Count the number of rows in the table
      query = "SELECT COUNT(*) AS TotalRows FROM [" & tableInfo(tableCount, 0) & "]"
      Set tempRS = conn.Execute(query)
      tableInfo(tableCount, 1) = tempRS(0).Value
      tempRS.Close

      ' Get the primary key name
      pkName = GetPrimaryKeyName(dbPath, tableInfo(tableCount, 0))
      tableInfo(tableCount, 2) = pkName

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
  GetTableNamesAndCounts = tableInfo
End Function

Private Function GetPrimaryKeyName(ByVal dbPath As String, ByVal tableName As String) As String
  Dim conn As ADODB.Connection
  Dim rs As ADODB.Recordset
  Dim keyName As String

  Set conn = New ADODB.Connection
  conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & dbPath & ";"
  conn.Open

  Set rs = conn.OpenSchema(adSchemaPrimaryKeys, Array(Empty, Empty, tableName))

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

