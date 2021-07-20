Option Explicit

'/** REV2
'* @link https://pipiscrew.com
'* @copyright 2021 PipisCrew
'*/

Private Const headerBGColor As Long = 12611584 'RGB(0, 112, 192)
Private Const weekendBGColor As Long = 15914157 'RGB(173, 212, 242) alternative-RGB(255, 199, 206)

Public Sub DrawSheets()

' https://docs.microsoft.com/en-us/office/vba/api/excel.application.screenupdating
Application.ScreenUpdating = False

On Error GoTo ErrA

    Dim i%
    
    For i = 1 To GetWorksheetCount()
        Call DrawCalendar(GetWorksheetName(i))
    Next i

Application.ScreenUpdating = True

Exit Sub

ErrA:
    Call MsgBox(Err.Description & vbCrLf & vbCrLf & Err.Source & vbCrLf & vbCrLf & Err.Number, vbExclamation)
End Sub

Private Sub DrawCalendar(sheetName As String)

    Dim i%, k%, noDays%, monthStep%, yr$, colLetter$, dayNameVar$

    If (IsInt(GetValue(sheetName, "A1")) = False) Then
        yr = 0
    Else
        yr = CInt(GetValue(sheetName, "A1"))
    End If
    
    If (yr = 0) Then
        yr = Format(Now, "yyyy")
        Call SetValue(sheetName, "A1", yr)
    End If
    
    'draw header
    Call SetHeaderStyle(yr, sheetName)
    
    monthStep = 1
    
    'for all months
    For i = 1 To 12
        
        noDays = GetMonthDays(yr, i)
    
        'month days
        For k = 1 To noDays
        
            colLetter = Number2Letter(monthStep) & CStr(2 + k)
        
            dayNameVar = DayName(Convert2Date(yr, i, k))
            
            'colorize weekend days
            If (LCase(dayNameVar) = "sat" Or LCase(dayNameVar) = "sun") Then
               Call SetBGColor(sheetName, colLetter & ":" & Number2Letter(monthStep + 1) & CStr(2 + k), weekendBGColor)
            End If
            
            Call SetValue(sheetName, colLetter, Format(k, "00") & "  " & dayNameVar)
        Next k
        
        monthStep = monthStep + 2
    Next i

End Sub


' string to date with custom format
' https://www.excelanytime.com/excel/index.php?option=com_content&view=article&id=316:excel-vba-date-time-functions-year-month-week-day-functions&catid=79&Itemid=475#VBA%20DateSerial%20Function
Private Function GetMonthDays(ByVal yr As Integer, ByVal mn As Integer) As Integer

    Dim nb%
    Dim dt As Date
    dt = DateSerial(yr, mn, 1)
    
    nb = Day(DateSerial(Year(dt), Month(dt) + 1, 1) - 1)
    
    GetMonthDays = nb
    
End Function

' string to date with custom format - we dont use CDATE as is using system locale date format
Private Function Convert2Date(ByVal yr As Integer, ByVal mn As Integer, ByVal d As Integer) As Date
    Convert2Date = DateSerial(yr, mn, d)
End Function

Private Function DayName(ByVal d As Date) As String
' https://stackoverflow.com/a/48156068
    DayName = Application.WorksheetFunction.Text(d, "[$-409]" & "ddd")
End Function

Private Function MonthName(ByVal d As Date) As String
    MonthName = Application.WorksheetFunction.Text(d, "[$-409]" & "mmmm")
End Function

Private Function Number2Letter(ByVal ColumnNumber As Long) As String
' https://www.thespreadsheetguru.com/the-code-vault/vba-code-to-convert-column-number-to-letter-or-letter-to-number
    Number2Letter = Split(Cells(1, ColumnNumber).Address, "$")(1)
End Function

Private Function GetValue(ByVal ws As String, ByVal xy As String) As Variant
    GetValue = ThisWorkbook.Sheets(ws).Range(xy).Value
End Function

Private Function GetWorksheetCount() As Integer
    GetWorksheetCount = ThisWorkbook.Sheets.Count
End Function

Private Function GetWorksheetName(ByVal wIndex As Integer) As String
    GetWorksheetName = ThisWorkbook.Sheets(wIndex).Name
End Function

Private Sub SetValue(ByVal ws As String, ByVal xy As String, ByVal val As String)
     ThisWorkbook.Sheets(ws).Range(xy).Value = val
End Sub

Private Sub SetCellBold(ByVal ws As String, ByVal xy As String, ByVal bold As Boolean)
' https://www.automateexcel.com/vba/cell-font-color-size/
     ThisWorkbook.Sheets(ws).Range(xy).Font.bold = bold
End Sub

Private Sub SetFGColor(ByVal ws As String, ByVal xy As String)
     ThisWorkbook.Sheets(ws).Range(xy).Font.color = vbWhite
End Sub

Private Sub SetBGColor(ByVal ws As String, ByVal xy As String, ByVal color As Long)
' https://www.excel-easy.com/vba/examples/background-colors.html
     ThisWorkbook.Sheets(ws).Range(xy).Interior.color = color
End Sub

Private Sub SetColWidth(ByVal ws As String, ByVal col As String, ByVal width As Double)
' https://docs.microsoft.com/en-us/office/vba/api/excel.range.columnwidth
     ThisWorkbook.Sheets(ws).Columns(col).ColumnWidth = width
End Sub

Private Sub SetHeaderStyle(ByVal yr As String, ByVal ws As String)
    Call SetBGColor(ws, "A2:X2", headerBGColor)
    Call SetFGColor(ws, "A2:X2")
    Call SetCellBold(ws, "A2:X2", True)

    Dim colLetter$, i%, monthNameIndex%
    monthNameIndex = 0

    For i = 1 To 24
        colLetter = Number2Letter(i)

        If (i Mod 2 = 1) Then
            monthNameIndex = monthNameIndex + 1
            Call SetHeaderCaption(ws, colLetter, "2", UCase(MonthName(Convert2Date(yr, monthNameIndex, 1))))
        Else 'column empty cells
            Call SetColWidth(ws, colLetter, 13)
        End If
    Next i
End Sub

Private Sub SetHeaderCaption(ByVal ws As String, ByVal x As String, ByVal y As String, ByVal cellValue As String)
    Call SetColWidth(ws, x, 8)
    Call SetValue(ws, x & y, cellValue)
End Sub


Private Function IsInt(ByVal inVar As Variant) As Boolean
    On Error Resume Next
    
    IsInt = CInt(inVar)
End Function


'/**
'* use VBA.xx to access all the default functions, use WorksheetFunction.xx to access all worksheet functions
'* more https://docs.microsoft.com/en-us/office/vba/language/reference/functions-visual-basic-for-applications
'*/

'Private Function Letter2Number(ColumnLetter As String) As Long
'   Letter2Number = Range(ColumnLetter & 1).Column
'End Function

'Private Sub IterateAll()
'
'    Dim wb As Workbook
'    Dim ws As Worksheet
'
'    For Each wb In Application.Workbooks
'        Debug.Print wb.Name
'
'        For Each ws In wb.Worksheets
'            Debug.Print ws.Name
'        Next ws
'    Next wb
'
'End Sub

'** Transpose
' https://www.excel-easy.com/examples/transpose.html
'1 - horizontal range 1 row * 5 columns
'Dim myArray As Variant
'
'Range("A1:E1").Value = Array(1, 2, 3, 4, 5)
'
'2 - vertical range 5 rows * 1 column
'Range("A1:A5").Value = Application.Transpose(Array(1, 2, 3, 4, 5))

'** CMDialog
'Dim f As FileDialog
'Set f = Application.FileDialog(msoFileDialogFolderPicker)
'f.Show

