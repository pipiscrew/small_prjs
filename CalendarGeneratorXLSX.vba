Option Explicit

'/**
'* @link https://pipiscrew.com
'* @copyright 2021 PipisCrew
'*/

Public Sub DrawSheets()

    Dim i%
    
    For i = 1 To GetWorksheetCount()
        Call DrawCalendar(GetWorksheetName(i))
    Next i

End Sub

Sub DrawCalendar(sheetName As String)

    Dim i, k%
    Dim yr$
    
    yr = CInt(GetValue(sheetName, "A1"))
    
    If (yr = "0") Then
        yr = Format(Now, "yyyy")
        Call SetValue(sheetName, "A1", yr)
    End If
    
    'draw header
    Dim monthNameIndex%
    monthNameIndex = 0
    
    For i = 1 To 24
        If (i Mod 2 = 1) Then
            monthNameIndex = monthNameIndex + 1
            
            Call SetValue(sheetName, Number2Letter(i) & "2", UCase(MonthName(Convert2Date(yr, monthNameIndex, 1))))
            Call SetHeaderStyle(sheetName, Number2Letter(i) & "2")
        Else 'header empty cells
            Call SetBGColor(sheetName, Number2Letter(i) & "2", RGB(0, 112, 192))
        End If
    Next i
    'draw header
    
    Dim noDays%
    Dim dayNameVar$
    
    Dim monthStep%
    monthStep = 1
    
    'for all months
    For i = 0 To 11
        
        noDays = GetMonthDays(yr, i + 1)
    
        'month days
        For k = 1 To noDays
            dayNameVar = DayName(Convert2Date(yr, i + 1, k))
            
            'colorize weekend days
            If (LCase(dayNameVar) = "sat" Or LCase(dayNameVar) = "sun") Then
               Call SetBGColor(sheetName, Number2Letter(monthStep) & CStr(2 + k), RGB(173, 212, 242)) 'RGB(255, 199, 206))
            End If
            
            Call SetValue(sheetName, Number2Letter(monthStep) & CStr(2 + k), Format(k, "00") & "  " & dayNameVar)
        Next k
        
        'set comments columns width
        Call SetColWidth(sheetName, Number2Letter(monthStep + 1), 13)
        
        monthStep = monthStep + 2
    Next i

End Sub


' string to date with custom format
' https://www.excelanytime.com/excel/index.php?option=com_content&view=article&id=316:excel-vba-date-time-functions-year-month-week-day-functions&catid=79&Itemid=475#VBA%20DateSerial%20Function
Function GetMonthDays(ByVal yr As Integer, ByVal mn As Integer) As Integer

    Dim nb%
    Dim dt As Date
    dt = DateSerial(yr, mn, 1)
    
    nb = Day(DateSerial(Year(dt), Month(dt) + 1, 1) - 1)
    
    GetMonthDays = nb
    
End Function

' string to date with custom format - we dont use CDATE as is using system locale date format
Function Convert2Date(ByVal yr As Integer, ByVal mn As Integer, ByVal d As Integer) As Date
    Convert2Date = DateSerial(yr, mn, d)
End Function

Function DayName(ByVal d As Date) As String
' https://stackoverflow.com/a/48156068
    DayName = Application.WorksheetFunction.Text(d, "[$-409]" & "ddd")
End Function

Function MonthName(ByVal d As Date) As String
    MonthName = Application.WorksheetFunction.Text(d, "[$-409]" & "mmmm")
End Function

Function Number2Letter(ByVal ColumnNumber As Long) As String
' https://www.thespreadsheetguru.com/the-code-vault/vba-code-to-convert-column-number-to-letter-or-letter-to-number
    Dim ColumnLetter As String
    
    ColumnLetter = Split(Cells(1, ColumnNumber).Address, "$")(1)
      
    Number2Letter = ColumnLetter
End Function

Function GetValue(ByVal ws As String, ByVal xy As String)
    GetValue = ThisWorkbook.Sheets(ws).Range(xy).Value
End Function

Function GetWorksheetCount() As Integer
    GetWorksheetCount = ThisWorkbook.Sheets.Count
End Function

Function GetWorksheetName(ByVal wIndex As Integer) As String
    GetWorksheetName = ThisWorkbook.Sheets(wIndex).Name
End Function

Sub SetValue(ByVal ws As String, ByVal xy As String, ByVal val As String)
     ThisWorkbook.Sheets(ws).Range(xy).Value = val
End Sub

Sub SetCellStyle(ByVal ws As String, ByVal xy As String, ByVal bold As Boolean)
' https://www.automateexcel.com/vba/cell-font-color-size/
     ThisWorkbook.Sheets(ws).Range(xy).Font.bold = bold
End Sub

Sub SetFGColor(ByVal ws As String, ByVal xy As String)
' https://www.automateexcel.com/vba/cell-font-color-size/
     ThisWorkbook.Sheets(ws).Range(xy).Font.color = vbWhite
End Sub

Sub SetBGColor(ByVal ws As String, ByVal xy As String, ByVal color As Long)
' https://www.excel-easy.com/vba/examples/background-colors.html
     ThisWorkbook.Sheets(ws).Range(xy).Interior.color = color
End Sub

Sub SetColWidth(ByVal ws As String, ByVal col As String, ByVal width As Long)
' https://docs.microsoft.com/en-us/office/vba/api/excel.range.columnwidth
     ThisWorkbook.Sheets(ws).Columns(col).ColumnWidth = width
End Sub

Sub SetHeaderStyle(ByVal ws As String, ByVal xy As String)
    Call SetCellStyle(ws, xy, True)
    Call SetFGColor(ws, xy)
    Call SetBGColor(ws, xy, RGB(0, 112, 192))
End Sub




