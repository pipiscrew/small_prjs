Attribute VB_Name = "General"
Option Explicit

' file & folder exists
Private Declare Function GetFileAttributes Lib "kernel32.dll" Alias "GetFileAttributesA" (ByVal lpFileName As String) As Long
Private Const INVALID_FILE_ATTRIBUTES As Long = -1
Private Const FILE_ATTRIBUTE_DIRECTORY As Long = &H10
' file & folder exists

Private masterSchema As Collection

Public Function FileExists(ByVal filepath As String) As Boolean
    Dim attr As Long
    attr = GetFileAttributes(filepath)
    If attr <> INVALID_FILE_ATTRIBUTES Then
        FileExists = ((attr And FILE_ATTRIBUTE_DIRECTORY) <> FILE_ATTRIBUTE_DIRECTORY)
    End If
End Function

Public Function GetRecordSet(ByVal filepath$, ByVal sql$) As ADODB.Recordset
  On Error GoTo ErrLoop

  Dim rs As ADODB.Recordset

  Set rs = New ADODB.Recordset

  rs.Open sql, "Data Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & filepath, adOpenStatic, adLockOptimistic

  Set GetRecordSet = rs

  Exit Function
ErrLoop:
  MsgBox Err.Description & vbCrLf & vbCrLf & "The program now exit....Sorry!", vbCritical, "": End
End Function

Public Function CloneTableByTBL1toTBL2(ByVal sourceDbFilepath$, ByVal destDbFilepath$, ByVal deleteIfExists As Boolean, tables As Collection)
  Dim conn As ADODB.Connection
  Dim tableName As String
  Dim sqlCreate As String
  Dim sqlDelete As String
  Dim sourceTableName, destTableName As String
  Dim i%
  Dim rsCheck As ADODB.Recordset
  Dim tableExists As Boolean

  Set conn = New ADODB.Connection
  conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & destDbFilepath & ";"
  conn.Open

  Dim tInfo As clsTableInfo
  For Each tInfo In tables
    Debug.Print tInfo.tableName
    sourceTableName = tInfo.tableName
    destTableName = tInfo.tableName

    If (deleteIfExists) Then
      On Error Resume Next

      '//
      Set rsCheck = conn.OpenSchema(adSchemaTables, Array(Empty, Empty, sourceTableName, "TABLE"))
      tableExists = Not rsCheck.EOF
      rsCheck.Close
      Set rsCheck = Nothing
      '//

      If tableExists Then
        sqlDelete = "DROP TABLE [" & sourceTableName & "]"
        conn.Execute sqlDelete
      End If

      If (Err.Number <> 0) Then
        Debug.Print "Error deleting table: " & sourceTableName & " - " & Err.Description
        MsgBox "Error deleting table: " & sourceTableName & " - " & Err.Description & vbCrLf & vbCrLf & "Possible other table reference this table." & vbCrLf & vbCrLf & "Operation aborted!", vbCritical
        Err.Clear  ' Clear the error
        conn.Close
        On Error GoTo 0
        Exit Function
      End If

      On Error GoTo 0  ' Resume normal error handling
    Else
      destTableName = tInfo.tableName & GetToday
    End If

    On Error Resume Next

    ' COPY ROWS FROM SOURCE DBASE
    sqlCreate = "SELECT * INTO [" & destTableName & "] FROM [" & sourceTableName & "] IN '" & sourceDbFilepath & "';"
    conn.Execute sqlCreate

    '.............

    '    Dim tInfo As clsTableInfo
    Dim item As Variant
    Dim parts() As String
    Dim sqlText As String

    ' ADD PK
    'If (IsNullOrEmpty(tInfo.PK) = False) Then
    If tInfo.PKCollection.count > 0 Then
      Dim pkFields As String: pkFields = ""
      For Each item In tInfo.PKCollection
        pkFields = pkFields & "[" & item & "], "
      Next
      pkFields = Left(pkFields, Len(pkFields) - 2)

      '      sqlText = sqlText & "ALTER TABLE [" & tInfo.tableName & "] ADD CONSTRAINT [PK_" & tInfo.tableName & "] PRIMARY KEY (" & pkFields & ");" & vbCrLf
      sqlText = "ALTER TABLE [" & tInfo.tableName & "] ADD CONSTRAINT [PrimaryKey] PRIMARY KEY (" & pkFields & ");"
      conn.Execute sqlText
    End If

    If Err.Number <> 0 Then
      MsgBox Err.Description, vbExclamation
      Err.Clear  ' Clear the error
    End If


    'ADD UNIQUE
    For Each item In tInfo.UniqueCollection
      parts = Split(item, "|")  ' parts(0) = Index Name, parts(1) = Column List
      '            sqlText = sqlText & "ALTER TABLE [" & tInfo.tableName & "] ADD CONSTRAINT [" & parts(0) & "] UNIQUE (" & parts(1) & ");" & vbCrLf
      sqlText = "ALTER TABLE [" & tInfo.tableName & "] ADD CONSTRAINT [" & parts(0) & "] UNIQUE (" & parts(1) & ");"
      conn.Execute sqlText
    Next

    If Err.Number <> 0 Then
      MsgBox Err.Description, vbExclamation
      Err.Clear  ' Clear the error
    End If

    ' C. Regular Performance Indexes DDL (Non-unique lookup keys)
    For Each item In tInfo.IndexCollection
      parts = Split(item, "|")  ' parts(0) = Index Name, parts(1) = Column List
      'sqlText = sqlText & "CREATE INDEX [" & parts(0) & "] ON [" & tInfo.tableName & "] (" & parts(1) & ");" & vbCrLf
      sqlText = "CREATE INDEX [" & parts(0) & "] ON [" & tInfo.tableName & "] (" & parts(1) & ");"
      conn.Execute sqlText
    Next

    If Err.Number <> 0 Then
      MsgBox Err.Description, vbExclamation
      Err.Clear  ' Clear the error
    End If

    'ADD FK
    ' D. Foreign Keys DDL
    For Each item In tInfo.FKCollection
      parts = Split(item, "|")  ' LocalField|RefTable|RefField
      '            sqlText = sqlText & "ALTER TABLE [" & tInfo.tableName & "] ADD CONSTRAINT [FK_" & tInfo.tableName & "_" & parts(0) & "] " & _
                   '                      "FOREIGN KEY ([" & parts(0) & "]) REFERENCES [" & parts(1) & "] ([" & parts(2) & "]);" & vbCrLf
      sqlText = "ALTER TABLE [" & tInfo.tableName & "] ADD CONSTRAINT [" & parts(0) & "] " & _
                "FOREIGN KEY ([" & parts(1) & "]) REFERENCES [" & parts(2) & "] ([" & parts(3) & "]);"

      conn.Execute sqlText
    Next

    If Err.Number <> 0 Then
      MsgBox Err.Description, vbExclamation
      Err.Clear  ' Clear the error
    End If

    On Error GoTo 0  ' Resume normal error handling

  Next tInfo

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

    Dim pk As String

    If tInfo.PKCollection.count > 0 Then
      pk = tInfo.PKCollection.item(1)
    End If

    lstv.ListItems.Add , , tInfo.tableName
    lstv.ListItems(lstv.ListItems.count).ListSubItems.Add , , tInfo.RecordCount
    lstv.ListItems(lstv.ListItems.count).ListSubItems.Add , , pk
    '
    '
  Next tInfo

  If (store2masterSchema) Then

    Set masterSchema = mySchema
  End If

End Sub

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

Public Function IsArrayEmpty(arr() As String) As Boolean
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
          checkedCollection.Add tInfo, tInfo.tableName
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

  ' Variables to group composite index/unique fields
  Dim lastIdxName As String
  Dim collectedFields As String
  Dim isLastUnique As Boolean
  Dim isLastPK As Boolean

  On Error GoTo ErrorHandler

  conn.Open "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & dbPath & ";"
  Set tblSchema = New Collection

  ' Step A: Gather User Tables and Record Counts
  Set rsTables = conn.OpenSchema(adSchemaTables, Array(Empty, Empty, Empty, "TABLE"))

  Do While Not rsTables.EOF
    currentTable = rsTables("TABLE_NAME").Value

    If Left(currentTable, 4) <> "MSys" Then
      Set tInfo = New clsTableInfo
      tInfo.tableName = currentTable

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


  ' Step B: Extract ALL Foreign Keys FIRST (So we map and blacklist their exact names)
  Set rsFKs = conn.OpenSchema(adSchemaForeignKeys, Array(Empty, Empty, Empty, Empty, Empty, Empty))

  Do While Not rsFKs.EOF
    Dim fkTable As String: fkTable = rsFKs("FK_TABLE_NAME").Value
    Dim fkName As String: fkName = NVL(rsFKs("FK_NAME").Value)

    Set tInfo = Nothing
    On Error Resume Next
    Set tInfo = tblSchema(fkTable)
    On Error GoTo ErrorHandler

    If Not tInfo Is Nothing Then
      ' Pass original db fkName to store and automatically register to blacklist
      tInfo.AddFK fkName, _
                  rsFKs("FK_COLUMN_NAME").Value, _
                  rsFKs("PK_TABLE_NAME").Value, _
                  rsFKs("PK_COLUMN_NAME").Value
    End If

    rsFKs.MoveNext
  Loop
  rsFKs.Close

  ' Step C: Extract Primary Keys, Unique Constraints, and Database Indexes
  For Each tInfo In tblSchema
    currentTable = tInfo.tableName
    Set rsIdx = conn.OpenSchema(adSchemaIndexes, Array(Empty, Empty, Empty, Empty, currentTable))

    lastIdxName = ""
    collectedFields = ""

    Do While Not rsIdx.EOF
      Dim idxName As String: idxName = NVL(rsIdx("INDEX_NAME").Value)
      Dim colName As String: colName = NVL(rsIdx("COLUMN_NAME").Value)
      Dim isPK As Boolean: isPK = CBool(rsIdx("PRIMARY_KEY").Value)
      Dim isUnique As Boolean: isUnique = CBool(rsIdx("UNIQUE").Value)

      ' Safely ignores implicit systems and matches against exact original FK constraint names
      If idxName <> "" And Left(idxName, 10) <> "Reference" And Not tInfo.IsAnFKName(idxName) Then

        If lastIdxName <> "" And lastIdxName <> idxName Then
          If isLastPK Then
            ' Handled inline below
          Else
            tInfo.AddIndexOrUnique lastIdxName, collectedFields, isLastUnique
          End If
          collectedFields = ""
        End If

        If collectedFields = "" Then
          collectedFields = "[" & colName & "]"
        Else
          collectedFields = collectedFields & ", [" & colName & "]"
        End If

        If isPK Then tInfo.AddPK colName

        lastIdxName = idxName
        isLastUnique = isUnique
        isLastPK = isPK
      End If

      rsIdx.MoveNext
    Loop

    If lastIdxName <> "" And Not tInfo.IsAnFKName(lastIdxName) Then
      If Not isLastPK Then
        tInfo.AddIndexOrUnique lastIdxName, collectedFields, isLastUnique
      End If
    End If

    rsIdx.Close
  Next tInfo

  conn.Close
  Set conn = Nothing
  Set GetDatabaseSchemaCollection = tblSchema
  Exit Function

ErrorHandler:
  MsgBox "Error reading schema: " & Err.Description, vbCritical, "Error"
  Set GetDatabaseSchemaCollection = Nothing
End Function

' Clean Null values helper function
Private Function NVL(ByVal val As Variant) As String
  If IsNull(val) Then NVL = "" Else NVL = CStr(val)
End Function


Public Function GetListviewCheckedItems(ByVal lstv As ListView) As String()
    Dim i As Long
    Dim count As Long
    Dim checkedArray() As String
    
    ' Initialize count
    count = 0
    
    ' Loop through all ListView items
    For i = 1 To lstv.ListItems.count
        If lstv.ListItems(i).Checked Then
            ' Expand the array while keeping existing data
            ReDim Preserve checkedArray(count) As String
            
            ' Store the item text
            checkedArray(count) = lstv.ListItems(i).Text
            
            ' Increment index for the next found item
            count = count + 1
        End If
    Next i
    
    ' Return the populated array
    GetListviewCheckedItems = checkedArray
End Function


Public Sub CheckUncheckLstvItems(ByVal lstv As ListView, isCheck As Boolean)
  Dim i As Integer

  ' Loop through every item in the ListView
  For i = 1 To lstv.ListItems.count
    lstv.ListItems(i).Checked = isCheck
  Next i
End Sub
