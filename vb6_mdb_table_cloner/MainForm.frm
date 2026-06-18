VERSION 5.00
Object = "{831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.2#0"; "MSCOMCTL.OCX"
Begin VB.Form MainForm 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "MainForm"
   ClientHeight    =   9195
   ClientLeft      =   45
   ClientTop       =   390
   ClientWidth     =   12750
   BeginProperty Font 
      Name            =   "Tahoma"
      Size            =   8.25
      Charset         =   0
      Weight          =   400
      Underline       =   0   'False
      Italic          =   0   'False
      Strikethrough   =   0   'False
   EndProperty
   LinkTopic       =   "Form2"
   MaxButton       =   0   'False
   ScaleHeight     =   9195
   ScaleWidth      =   12750
   StartUpPosition =   2  'CenterScreen
   Begin VB.CheckBox chkDeleteBeforeAddTable 
      Caption         =   "Delete Before AddTable"
      Height          =   255
      Left            =   5300
      TabIndex        =   5
      Top             =   2310
      Value           =   1  'Checked
      Width           =   2115
   End
   Begin VB.CommandButton btnExecute 
      Caption         =   ">> CLONE >>"
      Height          =   645
      Left            =   5310
      TabIndex        =   4
      Top             =   1245
      Width           =   1965
   End
   Begin MSComctlLib.ListView lstSource 
      Height          =   8355
      Left            =   210
      TabIndex        =   0
      Top             =   735
      Width           =   4695
      _ExtentX        =   8281
      _ExtentY        =   14737
      View            =   3
      LabelEdit       =   1
      MultiSelect     =   -1  'True
      LabelWrap       =   -1  'True
      HideSelection   =   0   'False
      OLEDragMode     =   1
      OLEDropMode     =   1
      Checkboxes      =   -1  'True
      FullRowSelect   =   -1  'True
      _Version        =   393217
      ForeColor       =   -2147483640
      BackColor       =   -2147483643
      BorderStyle     =   1
      Appearance      =   0
      OLEDragMode     =   1
      OLEDropMode     =   1
      NumItems        =   3
      BeginProperty ColumnHeader(1) {BDD1F052-858B-11D1-B16A-00C0F0283628} 
         Text            =   "Table"
         Object.Width           =   2540
      EndProperty
      BeginProperty ColumnHeader(2) {BDD1F052-858B-11D1-B16A-00C0F0283628} 
         SubItemIndex    =   1
         Text            =   "Count"
         Object.Width           =   2540
      EndProperty
      BeginProperty ColumnHeader(3) {BDD1F052-858B-11D1-B16A-00C0F0283628} 
         SubItemIndex    =   2
         Text            =   "PrimaryKey"
         Object.Width           =   2540
      EndProperty
   End
   Begin MSComctlLib.ListView lstDest 
      Height          =   8355
      Left            =   7785
      TabIndex        =   1
      Top             =   735
      Width           =   4695
      _ExtentX        =   8281
      _ExtentY        =   14737
      View            =   3
      LabelEdit       =   1
      LabelWrap       =   -1  'True
      HideSelection   =   0   'False
      OLEDragMode     =   1
      OLEDropMode     =   1
      Checkboxes      =   -1  'True
      FullRowSelect   =   -1  'True
      _Version        =   393217
      ForeColor       =   -2147483640
      BackColor       =   -2147483643
      BorderStyle     =   1
      Appearance      =   0
      OLEDragMode     =   1
      OLEDropMode     =   1
      NumItems        =   3
      BeginProperty ColumnHeader(1) {BDD1F052-858B-11D1-B16A-00C0F0283628} 
         Text            =   "Table"
         Object.Width           =   2540
      EndProperty
      BeginProperty ColumnHeader(2) {BDD1F052-858B-11D1-B16A-00C0F0283628} 
         SubItemIndex    =   1
         Text            =   "Count"
         Object.Width           =   2540
      EndProperty
      BeginProperty ColumnHeader(3) {BDD1F052-858B-11D1-B16A-00C0F0283628} 
         SubItemIndex    =   2
         Text            =   "PrimaryKey"
         Object.Width           =   2540
      EndProperty
   End
   Begin VB.Label Label1 
      AutoSize        =   -1  'True
      Caption         =   "DESTINATION"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   15.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   7770
      TabIndex        =   3
      Top             =   225
      Width           =   2010
   End
   Begin VB.Label lblSource 
      AutoSize        =   -1  'True
      Caption         =   "SOURCE"
      BeginProperty Font 
         Name            =   "Tahoma"
         Size            =   15.75
         Charset         =   0
         Weight          =   400
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      Height          =   375
      Left            =   240
      TabIndex        =   2
      Top             =   225
      Width           =   1185
   End
End
Attribute VB_Name = "MainForm"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub Form_Load()
  Me.Caption = App.Title & " v" & App.Major & "." & App.Minor
End Sub

Private Sub btnExecute_Click()
  Dim sourceTBL() As Variant
  Dim destTBL() As Variant
  sourceTBL = GetCheckListivewItems(lstSource)
  destTBL = GetCheckListivewItems(lstDest)

  If (IsArrayEmpty(sourceTBL)) Then
    MsgBox "Please select source tables", vbExclamation
    Exit Sub
  ElseIf (IsNullOrEmpty(lstDest.Tag)) Then
    MsgBox "Please add destination database", vbExclamation
    Exit Sub
  End If

  Call CloneTableByTBL1toTBL2(lstSource.Tag, lstDest.Tag, chkDeleteBeforeAddTable.Value, sourceTBL)

  ' refresh destination
  Call AddTablesToListview(lstDest, lstDest.Tag)
End Sub

Private Sub lstSource_OLEDragDrop(Data As MSComctlLib.DataObject, Effect As Long, Button As Integer, Shift As Integer, x As Single, y As Single)
  If Data.GetFormat(vbCFFiles) Then
    lstSource.Tag = Data.Files(1)

    Call AddTablesToListview(lstSource, Data.Files(1))
  End If
End Sub

Private Sub lstDest_OLEDragDrop(Data As MSComctlLib.DataObject, Effect As Long, Button As Integer, Shift As Integer, x As Single, y As Single)
  If Data.GetFormat(vbCFFiles) Then
    lstDest.Tag = Data.Files(1)

    Call AddTablesToListview(lstDest, Data.Files(1))
  End If
End Sub
