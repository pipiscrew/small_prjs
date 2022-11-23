Imports System
Imports System.Collections.Generic
Imports System.Text

Imports System.Drawing
Imports System.IO
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports System.Globalization

' src - https://forums.autodesk.com/autodesk/attachments/autodesk/152/24069/1/Excel%20late%20binding_vb.txt
'=============  Last edition date  06//22/11  ============='
'  mostly borrowed from:
'  http://www.sql.ru/forum/actualthread.aspx?tid=620401&pg=1

Namespace ExcelReflectVB

    Public Class Excel
        Implements IDisposable
        Public Const UID As String = "Excel.Application"
        Private oExcel As Object = Nothing
        Private WorkBooks As Object, WorkBook As Object, WorkSheets As Object, WorkSheet As Object, Range As Object, Interior As Object
        'Constructor
        Public Sub New()
            oExcel = Activator.CreateInstance(Type.GetTypeFromProgID(UID))
        End Sub


        'Set Visible property
        Public WriteOnly Property Visible() As Boolean
            Set(ByVal value As Boolean)
                If False = value Then
                    oExcel.GetType().InvokeMember("Visible", BindingFlags.SetProperty, Nothing, oExcel, New Object() {False})
                Else
                    oExcel.GetType().InvokeMember("Visible", BindingFlags.SetProperty, Nothing, oExcel, New Object() {True})
                End If
            End Set
        End Property

        ' Get WorkBook
        Public Function GetWorkBook(ByVal booknumber As Integer) As Object
            WorkBooks = oExcel.GetType().InvokeMember("Workbooks", BindingFlags.GetProperty, Nothing, oExcel, Nothing)
            Return WorkBooks.GetType().InvokeMember("Item", BindingFlags.GetProperty, Nothing, WorkBooks, New Object() {booknumber})
        End Function

        ' Get ActiveWorkBook
        Public Function ActiveDocument() As Object

            Return oExcel.GetType().InvokeMember("ActiveWorkBook", BindingFlags.GetProperty, Nothing, oExcel, Nothing)
        End Function

        ' Get Sheet By Number
        Public Function ActiveSheet(ByVal WorkBook As Object, ByVal sheetnumber As Integer) As Object
            WorkSheets = WorkBook.GetType().InvokeMember("Worksheets", BindingFlags.GetProperty, Nothing, WorkBook, Nothing)
            Return WorkSheets.GetType().InvokeMember("Item", BindingFlags.GetProperty, Nothing, WorkSheets, New Object() {sheetnumber})
        End Function

        ' Get UsedRange 
        Public Function GetUsedRange(ByVal WorkSheet As Object) As Object
            Return WorkSheet.GetType().InvokeMember("UsedRange", BindingFlags.GetProperty, Nothing, WorkSheet, Nothing)
        End Function

        ' Get Range By Address
        Public Function GetRange(ByVal WorkSheet As Object, ByVal address As Object()) As Object
            Return WorkSheet.GetType().InvokeMember("Range", BindingFlags.GetProperty, Nothing, WorkSheet, address)
        End Function

        'Get Range Value
        Public Function GetRangeValue(ByVal Range As Object) As Object
            Return Range.GetType().InvokeMember("Value", BindingFlags.GetProperty, Nothing, Range, Nothing)
        End Function

       
        ' Set Range Value
        Public Sub SetRangeValue(ByVal Range As Object, ByVal value As Object)
            Range.GetType().InvokeMember("Value", BindingFlags.SetProperty, Nothing, Range, New Object() {value})
        End Sub

        'Get Range Row
        Public Function GetRangeColumn(ByVal Range As Object) As Integer
            Return Convert.ToInt32(Range.GetType().InvokeMember("Row", BindingFlags.GetProperty, Nothing, Range, Nothing))
        End Function

        'Get Range Column
        Public Function GetRangeRow(ByVal Range As Object) As Integer
            Return Convert.ToInt32(Range.GetType().InvokeMember("Column", BindingFlags.GetProperty, Nothing, Range, Nothing))
        End Function

        'Get number of Columns in the Range 
        Public Function ColumnsCount(ByVal Range As Object) As Integer
            Dim RangeColumns As Object = Range.GetType().InvokeMember("Columns", BindingFlags.GetProperty, Nothing, Range, Nothing)
            Return Convert.ToInt32(RangeColumns.GetType().InvokeMember("Count", BindingFlags.GetProperty, Nothing, RangeColumns, Nothing))
        End Function

        'Get number of Rows in the Range 
        Public Function RowsCount(ByVal Range As Object) As Integer
            Dim RangeRows As Object = Range.GetType().InvokeMember("Rows", BindingFlags.GetProperty, Nothing, Range, Nothing)
            Return Convert.ToInt32(RangeRows.GetType().InvokeMember("Count", BindingFlags.GetProperty, Nothing, RangeRows, Nothing))
        End Function

        Public Sub SetText(ByVal Range As Object, ByVal text As String)
            Range.GetType().InvokeMember("Value2", BindingFlags.SetProperty, Nothing, Range, New Object() {text})
        End Sub

        'Open Document
        Public Sub OpenDocument(ByVal name As String)
            WorkBooks = oExcel.GetType().InvokeMember("Workbooks", BindingFlags.GetProperty, Nothing, oExcel, Nothing)
            WorkBook = WorkBooks.GetType().InvokeMember("Open", BindingFlags.InvokeMethod, Nothing, WorkBooks, New Object() {name, True})
            WorkSheets = WorkBook.GetType().InvokeMember("Worksheets", BindingFlags.GetProperty, Nothing, WorkBook, Nothing)
            WorkSheet = WorkSheets.GetType().InvokeMember("Item", BindingFlags.GetProperty, Nothing, WorkSheets, New Object() {1})
        End Sub

        'Create new document
        Public Sub NewDocument()
            WorkBooks = oExcel.GetType().InvokeMember("Workbooks", BindingFlags.GetProperty, Nothing, oExcel, Nothing)
            WorkBook = WorkBooks.GetType().InvokeMember("Add", BindingFlags.InvokeMethod, Nothing, WorkBooks, Nothing)
            WorkSheets = WorkBook.GetType().InvokeMember("Worksheets", BindingFlags.GetProperty, Nothing, WorkBook, Nothing)
            WorkSheet = WorkSheets.GetType().InvokeMember("Item", BindingFlags.GetProperty, Nothing, WorkSheets, New Object() {1})
            Range = WorkSheet.GetType().InvokeMember("Range", BindingFlags.GetProperty, Nothing, WorkSheet, New Object(0) {"A1"})
        End Sub

        'Close Document
        Public Sub CloseDocument()
            WorkBook.GetType().InvokeMember("Close", BindingFlags.InvokeMethod, Nothing, WorkBook, New Object() {True})
        End Sub

        'Save Document
        Public Sub SaveDocument(ByVal name As String)
            If File.Exists(name) Then
                WorkBook.GetType().InvokeMember("Save", BindingFlags.InvokeMethod, Nothing, WorkBook, Nothing)
            Else
                WorkBook.GetType().InvokeMember("SaveAs", BindingFlags.InvokeMethod, Nothing, WorkBook, New Object() {name})
            End If
        End Sub

        'Set cell background color
        Public Sub SetColor(ByVal range As String, ByVal r As Integer)
            'Range.Interior.ColorIndex - https://learn.microsoft.com/en-us/office/vba/api/excel.colorindex
            range = WorkSheet.GetType().InvokeMember("Range", BindingFlags.GetProperty, Nothing, WorkSheet, New Object() {range})
            Interior = range.GetType().InvokeMember("Interior", BindingFlags.GetProperty, Nothing, range, Nothing)
            range.GetType().InvokeMember("ColorIndex", BindingFlags.SetProperty, Nothing, Interior, New Object() {r})
        End Sub
        'Page orientation enums
        Public Enum XlPageOrientation
            xlPortrait = 1
            'portrait
            xlLandscape = 2
            ' landscape
        End Enum

        'Set page orientation
        Public Sub SetOrientation(ByVal Orientation As XlPageOrientation)
            Dim PageSetup As Object = WorkSheet.GetType().InvokeMember("PageSetup", BindingFlags.GetProperty, Nothing, WorkSheet, Nothing)
            PageSetup.GetType().InvokeMember("Orientation", BindingFlags.SetProperty, Nothing, PageSetup, New Object() {2})
        End Sub

        'Set page margins
        Public Sub SetMargin(ByVal Left As Double, ByVal Right As Double, ByVal Top As Double, ByVal Bottom As Double)
            Dim PageSetup As Object = WorkSheet.GetType().InvokeMember("PageSetup", BindingFlags.GetProperty, Nothing, WorkSheet, Nothing)
            PageSetup.GetType().InvokeMember("LeftMargin", BindingFlags.SetProperty, Nothing, PageSetup, New Object() {Left})
            PageSetup.GetType().InvokeMember("RightMargin", BindingFlags.SetProperty, Nothing, PageSetup, New Object() {Right})
            PageSetup.GetType().InvokeMember("TopMargin", BindingFlags.SetProperty, Nothing, PageSetup, New Object() {Top})
            PageSetup.GetType().InvokeMember("BottomMargin", BindingFlags.SetProperty, Nothing, PageSetup, New Object() {Bottom})
        End Sub

        'Page size enums
        Public Enum xlPaperSize
            xlPaperA4 = 9
            xlPaperA4Small = 10
            xlPaperA5 = 11
            xlPaperLetter = 1
            xlPaperLetterSmall = 2
            xlPaper10x14 = 16
            xlPaper11x17 = 17
            xlPaperA3 = 9
            xlPaperB4 = 12
            xlPaperB5 = 13
            xlPaperExecutive = 7
            xlPaperFolio = 14
            xlPaperLedger = 4
            xlPaperLegal = 5
            xlPaperNote = 18
            xlPaperQuarto = 15
            xlPaperStatement = 6
            xlPaperTabloid = 3
        End Enum

        'Set paper size
        Public Sub SetPaperSize(ByVal Size As xlPaperSize)
            Dim PageSetup As Object = WorkSheet.GetType().InvokeMember("PageSetup", BindingFlags.GetProperty, Nothing, WorkSheet, Nothing)
            PageSetup.GetType().InvokeMember("PaperSize", BindingFlags.SetProperty, Nothing, PageSetup, New Object() {Size})
        End Sub

        'Set page setup scale
        Public Sub SetZoom(ByVal Percent As Integer)
            Dim PageSetup As Object = WorkSheet.GetType().InvokeMember("PageSetup", BindingFlags.GetProperty, Nothing, WorkSheet, Nothing)
            PageSetup.GetType().InvokeMember("Zoom", BindingFlags.SetProperty, Nothing, PageSetup, New Object() {Percent})
        End Sub

        'Rename sheet
        Public Sub ReNamePage(ByVal n As Integer, ByVal Name As String)
            Dim Page As Object = WorkSheets.GetType().InvokeMember("Item", BindingFlags.GetProperty, Nothing, WorkSheets, New Object() {n})
            Page.GetType().InvokeMember("Name", BindingFlags.SetProperty, Nothing, Page, New Object() {Name})
        End Sub

        'Add sheet
        Public Sub AddNewPage(ByVal Name As String)
            WorkSheet = WorkSheets.GetType().InvokeMember("Add", BindingFlags.GetProperty, Nothing, WorkSheets, Nothing)
            Dim Page As Object = WorkSheets.GetType().InvokeMember("Item", BindingFlags.GetProperty, Nothing, WorkSheets, New Object() {1})
            Page.GetType().InvokeMember("Name", BindingFlags.SetProperty, Nothing, Page, New Object() {Name})
        End Sub

        'Set cell font
        Public Sub SetFont(ByVal range As String, ByVal font As Font)
            'Range.Font.Name
            range = WorkSheet.GetType().InvokeMember("Range", BindingFlags.GetProperty, Nothing, WorkSheet, New Object() {range})
            font = range.GetType().InvokeMember("Font", BindingFlags.GetProperty, Nothing, range, Nothing)
            range.GetType().InvokeMember("Name", BindingFlags.SetProperty, Nothing, font, New Object() {font.Name})
            range.GetType().InvokeMember("Size", BindingFlags.SetProperty, Nothing, font, New Object() {font.Size})
        End Sub

        ' Write string to cell
        Public Sub SetValue(ByVal range As String, ByVal value As String)
            range = WorkSheet.GetType().InvokeMember("Range", BindingFlags.GetProperty, Nothing, WorkSheet, New Object() {range})
            range.GetType().InvokeMember("Value", BindingFlags.SetProperty, Nothing, range, New Object() {value})
        End Sub


        ' Write string to cell
        Public Sub SetValue(ByVal row As Integer, ByVal col As Integer, ByVal value As String)

            Dim strRange As String = ColumnString(col) + row.ToString()

            Range = WorkSheet.GetType().InvokeMember("Range", BindingFlags.GetProperty, Nothing, WorkSheet, New Object() {strRange})
 
           Range.GetType().InvokeMember("Value", BindingFlags.SetProperty, Nothing, Range, New Object() {value})

        End Sub

        ' Set cell value using format 
    
    Public Sub SetValue(ByVal row As Integer, ByVal col As Integer, ByVal value As String, ByVal format As String)

            Dim strRange As String = ColumnString(col) + row.ToString()
   
         Dim Range As Object = WorkSheet.GetType().InvokeMember("Range", BindingFlags.GetProperty, Nothing, WorkSheet, New Object() {strRange})

 
           Range.GetType().InvokeMember("NumberFormat", BindingFlags.SetProperty, Nothing, Range, New Object() {format})


            Range.GetType().InvokeMember("Value", BindingFlags.SetProperty, Nothing, Range, New Object() {value})
 
       End Sub

        ' Set cell value using format 
      
  Public Sub SetValue(ByVal address As String, ByVal value As String, ByVal format As String)
     
       Dim Range As Object = WorkSheet.GetType().InvokeMember("Range", BindingFlags.GetProperty, Nothing, WorkSheet, New Object() {address})

  
          Range.GetType().InvokeMember("NumberFormat", BindingFlags.SetProperty, Nothing, Range, New Object() {format})

  
          Range.GetType().InvokeMember("Value", BindingFlags.SetProperty, Nothing, Range, New Object() {value})
  
      End Sub

   
     ' Set cell value using format and culture
 
       Public Sub SetValue(ByVal row As Integer, ByVal col As Integer, ByVal value As String, ByVal format As String, ByVal culture As CultureInfo)
  
          Dim strRange As String = ColumnString(col) + row.ToString()
  
          Dim Range As Object = WorkSheet.[GetType]().InvokeMember("Range", BindingFlags.GetProperty, Nothing, WorkSheet, New Object() {strRange})

 
           Range.GetType().InvokeMember("NumberFormat", BindingFlags.SetProperty, Nothing, Range, New Object() {format}, culture)

  
          Range.GetType().InvokeMember("Value", BindingFlags.SetProperty, Nothing, Range, New Object() {value})
 
       End Sub

   
     ' Set cell value using format and culture
  
      Public Sub SetValue(ByVal address As String, ByVal value As String, ByVal format As String, ByVal culture As CultureInfo)
   
         Dim Range As Object = WorkSheet.[GetType]().InvokeMember("Range", BindingFlags.GetProperty, Nothing, WorkSheet, New Object() {address})

  
          Range.GetType().InvokeMember("NumberFormat", BindingFlags.SetProperty, Nothing, Range, New Object() {format}, culture)
            Range.GetType().InvokeMember("Value", BindingFlags.SetProperty, Nothing, Range, New Object() {value})
 
       End Sub

        'Merge cells
        Public Sub SetMerge(ByVal MergeCells As Boolean, ByVal range As String)
            range = WorkSheet.GetType().InvokeMember("Range", BindingFlags.GetProperty, Nothing, WorkSheet, New Object() {"A1", "G1"})
            range.GetType().InvokeMember("MergeCells", BindingFlags.SetProperty, Nothing, range, New Object() {MergeCells})
        End Sub

        'Set column width
        Public Sub SetColumnWidth(ByVal range As String, ByVal Width As Double)
            range = WorkSheet.GetType().InvokeMember("Range", BindingFlags.GetProperty, Nothing, WorkSheet, New Object() {range})
            Dim args As Object() = New Object() {Width}
            range.GetType().InvokeMember("ColumnWidth", BindingFlags.SetProperty, Nothing, range, args)
        End Sub

        'Set text orientation
        Public Sub SetTextOrientation(ByVal range As String, ByVal Orientation As Integer)
            range = WorkSheet.GetType().InvokeMember("Range", BindingFlags.GetProperty, Nothing, WorkSheet, New Object() {range})
            Dim args As Object() = New Object() {Orientation}
            range.GetType().InvokeMember("Orientation", BindingFlags.SetProperty, Nothing, range, args)
        End Sub

        'Set Vertical cell alignment
        Public Sub SetVerticalAlignment(ByVal range As String, ByVal Alignment As Integer)
            range = WorkSheet.GetType().InvokeMember("Range", BindingFlags.GetProperty, Nothing, WorkSheet, New Object() {range})
            Dim args As Object() = New Object() {Alignment}
            range.GetType().InvokeMember("VerticalAlignment", BindingFlags.SetProperty, Nothing, range, args)
        End Sub

        'Set Horizontal cell alignment
        Public Sub SetHorisontalAlignment(ByVal range As String, ByVal Alignment As Integer)
            range = WorkSheet.GetType().InvokeMember("Range", BindingFlags.GetProperty, Nothing, WorkSheet, New Object() {range})
            Dim args As Object() = New Object() {Alignment}
            range.GetType().InvokeMember("HorizontalAlignment", BindingFlags.SetProperty, Nothing, range, args)
        End Sub

        'Set Cell format
        Public Sub SelectText(ByVal range As String, ByVal Start As Integer, ByVal Length As Integer, ByVal Color As Integer, ByVal FontStyle As String, ByVal FontSize As Integer)
            range = WorkSheet.GetType().InvokeMember("Range", BindingFlags.GetProperty, Nothing, WorkSheet, New Object() {range})
            Dim args As Object() = New Object() {Start, Length}
            Dim Characters As Object = range.GetType().InvokeMember("Characters", BindingFlags.GetProperty, Nothing, range, args)
            Dim Font As Object = Characters.GetType().InvokeMember("Font", BindingFlags.GetProperty, Nothing, Characters, Nothing)
            Font.GetType().InvokeMember("ColorIndex", BindingFlags.SetProperty, Nothing, Font, New Object() {Color})
            Font.GetType().InvokeMember("FontStyle", BindingFlags.SetProperty, Nothing, Font, New Object() {FontStyle})
            Font.GetType().InvokeMember("Size", BindingFlags.SetProperty, Nothing, Font, New Object() {FontSize})
        End Sub

        'Set text word wrap
        Public Sub SetWrapText(ByVal range As String, ByVal Value As Boolean)
            range = WorkSheet.GetType().InvokeMember("Range", BindingFlags.GetProperty, Nothing, WorkSheet, New Object() {range})
            Dim args As Object() = New Object() {Value}
            range.GetType().InvokeMember("WrapText", BindingFlags.SetProperty, Nothing, range, args)
        End Sub


        ' CONVERT COLUMN NUMBER TO STRING
        '(e.g.: col_no 27 returns "AA")'by Si_the_geek (VBForums.com)
       Function ColumnString(ByVal Col_No As Integer) As String
            'Only allow valid columns
            If Col_No < 1 Or Col_No > 256 Then
                Return String.Empty
                Exit Function
            End If '<--added
            If Col_No < 27 Then
                'Single letter 
                Return Chr(Col_No + 64)
            Else
                'Two letters  
                Return Chr(Int((Col_No - 1) / 26) + 64) & _
                    Chr(((Col_No - 1) Mod 26) + 1 + 64)
            End If
        End Function
   

     '        GET ROW AND COLUMN OF LAST CELL
    
    Public Function GetLastCell(ByVal WorkSheet As Object) As Integer()
    
        Dim RowCol As Integer() = New Integer(1) {}
         
   Dim xlCells As Object = WorkSheet.GetType().InvokeMember("Cells", BindingFlags.GetProperty, Nothing, WorkSheet, Nothing)
            'xlCellTypeLastCell
 
           Dim Range As Object = xlCells.GetType().InvokeMember("SpecialCells", BindingFlags.GetProperty, Nothing, xlCells, New Object() {11})
  
          Dim ColumnMax As Integer = Convert.ToInt32(Range.GetType().InvokeMember("Column", BindingFlags.GetProperty, Nothing, Range, Nothing))
   
         Dim RowMax As Integer = Convert.ToInt32(Range.GetType().InvokeMember("Row", BindingFlags.GetProperty, Nothing, Range, Nothing))
  
          RowCol(0) = RowMax
     
       RowCol(1) = ColumnMax
    
        Return RowCol
    
    End Function

        'Set row height
        Public Sub SetRowHeight(ByVal range As String, ByVal Height As Double)
            range = WorkSheet.GetType().InvokeMember("Range", BindingFlags.GetProperty, Nothing, WorkSheet, New Object() {range})
            Dim args As Object() = New Object() {Height}
            range.GetType().InvokeMember("RowHeight", BindingFlags.SetProperty, Nothing, range, args)
        End Sub

        'Set border style
        Public Sub SetBorderStyle(ByVal range As String, ByVal Style As Integer)
            range = WorkSheet.GetType().InvokeMember("Range", BindingFlags.GetProperty, Nothing, WorkSheet, New Object() {range})
            Dim args As Object() = New Object() {1}
            Dim args1 As Object() = New Object() {1}
            Dim Borders As Object = range.GetType().InvokeMember("Borders", BindingFlags.GetProperty, Nothing, range, Nothing)
            Borders = range.GetType().InvokeMember("LineStyle", BindingFlags.SetProperty, Nothing, Borders, args)
        End Sub

        'Read cell value
        Public Function GetValue(ByVal range As String) As String
            range = WorkSheet.GetType().InvokeMember("Range", BindingFlags.GetProperty, Nothing, WorkSheet, New Object() {range})
            Return range.GetType().InvokeMember("Value", BindingFlags.GetProperty, Nothing, range, Nothing).ToString()
        End Function

        'Close Excel
        Public Sub Quit()
            oExcel.GetType().InvokeMember("Quit", BindingFlags.InvokeMethod, Nothing, oExcel, Nothing)
        End Sub

        'Dispose Excel
        Public Sub Dispose() Implements IDisposable.Dispose
            Marshal.ReleaseComObject(oExcel)
            GC.GetTotalMemory(True)
        End Sub
    End Class
End Namespace   