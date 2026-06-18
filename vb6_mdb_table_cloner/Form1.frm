VERSION 5.00
Object = "{97BD7A06-77E0-11D2-8EAE-008048E27A77}#1.0#0"; "beegdo10.ocx"
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   8415
   ClientLeft      =   120
   ClientTop       =   465
   ClientWidth     =   13275
   LinkTopic       =   "Form1"
   ScaleHeight     =   8415
   ScaleWidth      =   13275
   StartUpPosition =   3  'Windows Default
   Begin VB.CommandButton Command2 
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Left            =   480
      TabIndex        =   2
      Top             =   1740
      Width           =   1470
   End
   Begin BeeGridOLEDB10.SGGrid dg 
      Height          =   8115
      Left            =   2400
      TabIndex        =   1
      Top             =   255
      Width           =   10725
      _cx             =   18918
      _cy             =   14314
      DataMember      =   ""
      DataMode        =   1
      AutoFields      =   -1  'True
      Enabled         =   -1  'True
      GridBorderStyle =   1
      ScrollBars      =   3
      FlatScrollBars  =   0
      ScrollBarTrack  =   0   'False
      DataRowCount    =   2
      BeginProperty HeadingFont {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      DataColCount    =   2
      HeadingRowCount =   1
      HeadingColCount =   1
      TextAlignment   =   0
      WordWrap        =   0   'False
      Ellipsis        =   1
      HeadingBackColor=   -2147483633
      HeadingForeColor=   -2147483630
      HeadingTextAlignment=   0
      HeadingWordWrap =   0   'False
      HeadingEllipsis =   1
      GridLines       =   1
      HeadingGridLines=   2
      GridLinesColor  =   -2147483633
      HeadingGridLinesColor=   -2147483640
      EvenOddStyle    =   0
      ColorEven       =   -2147483628
      ColorOdd        =   -2147483624
      UserResizeAnimate=   1
      UserResizing    =   3
      RowHeightMin    =   0
      RowHeightMax    =   0
      ColWidthMin     =   0
      ColWidthMax     =   0
      UserDragging    =   2
      UserHiding      =   2
      CellPadding     =   15
      CellBkgStyle    =   1
      CellBackColor   =   -2147483643
      CellForeColor   =   -2147483640
      BeginProperty Font {0BE35203-8F91-11CE-9DE3-00AA004BB851} 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   161
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      FocusRect       =   1
      FocusRectColor  =   0
      FocusRectLineWidth=   1
      TabKeyBehavior  =   0
      EnterKeyBehavior=   0
      NavigationWrapMode=   1
      SkipReadOnly    =   0   'False
      DefaultColWidth =   1200
      DefaultRowHeight=   255
      CellsBorderColor=   0
      CellsBorderVisible=   -1  'True
      RowNumbering    =   0   'False
      EqualRowHeight  =   0   'False
      EqualColWidth   =   0   'False
      HScrollHeight   =   0
      VScrollWidth    =   0
      Format          =   "General"
      Appearance      =   2
      FitLastColumn   =   0   'False
      SelectionMode   =   0
      MultiSelect     =   0
      AllowAddNew     =   0   'False
      AllowDelete     =   0   'False
      AllowEdit       =   -1  'True
      ScrollBarTips   =   0
      CellTips        =   0
      CellTipsDelay   =   1000
      SpecialMode     =   0
      OutlineLines    =   1
      CacheAllRecords =   -1  'True
      ColumnClickSort =   0   'False
      PreviewPaneColumn=   ""
      PreviewPaneType =   0
      PreviewPanePosition=   2
      PreviewPaneSize =   2000
      GroupIndentation=   225
      InactiveSelection=   1
      AutoScroll      =   -1  'True
      AutoResize      =   1
      AutoResizeHeadings=   -1  'True
      OLEDragMode     =   0
      OLEDropMode     =   0
      Caption         =   ""
      ScrollTipColumn =   ""
      MaxRows         =   4194304
      MaxColumns      =   8192
      NewRowPos       =   1
      CustomBkgDraw   =   0
      AutoGroup       =   -1  'True
      GroupByBoxVisible=   -1  'True
      GroupByBoxText  =   "Drag a column header here to group by that column"
      AlphaBlendEnabled=   0   'False
      DragAlphaLevel  =   206
      StylesCollection=   $"Form1.frx":0000
      ColumnsCollection=   $"Form1.frx":1A01
      ValueItems      =   $"Form1.frx":27D4
   End
   Begin VB.CommandButton Command 
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   315
      Left            =   495
      TabIndex        =   0
      Top             =   930
      Width           =   1410
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub Command_Click()
  Dim conn As ADODB.Connection
  Dim sourceDb As String
  Dim destDb As String
  Dim tableName As String
  Dim sqlCreate As String
  Dim sourceTableName, destTableName As String

  sourceDb = "C:\Users\admin\Desktop\prj\Northwind.MDB"
  destDb = "C:\Users\admin\Desktop\prj\2.mdb"
  sourceTableName = "Orders"
  destTableName = "OrdersClone"

  Set conn = New ADODB.Connection
  conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & destDb & ";"
  conn.Open

  sqlCreate = "SELECT * INTO " & destTableName & " FROM [" & sourceTableName & "] IN '" & sourceDb & "';"
  conn.Execute sqlCreate

  conn.Close
  Set conn = Nothing
  MsgBox "Table cloned using SELECT INTO!"
End Sub

Private Sub Command2_Click()
Dim rs As ADODB.Recordset
dg.DataMode = sgBound
Set rs = GetRecordSet("select * from fitments")
  Set dg.DataSource = rs
   
End Sub

Public Function GetRecordSet(SQL As String) As ADODB.Recordset
  On Error GoTo ErrLoop

  Dim rs As ADODB.Recordset

  Set rs = New ADODB.Recordset

  rs.Open SQL, "Provider=MSDataShape;Data Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Users\admin\Desktop\thyrae.th", adOpenStatic, adLockOptimistic

  Set GetRecordSet = rs

  Exit Function
ErrLoop:
  MsgBox Err.Description & vbCrLf & vbCrLf & "The program now exit....Sorry!", vbCritical, "": End
End Function

Private Sub dg_GroupByBoxHeaderClick(ByVal GroupIndex As Long)
dg.CollapseAll
End Sub
