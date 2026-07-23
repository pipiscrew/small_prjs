VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "clsTableInfo"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = False
Option Explicit

Private m_tableName As String
Private m_count As Long
Private m_PKs As Collection
Private m_FKs As Collection      ' Will now hold: "FK_Name|LocalCol|RefTable|RefCol"
Private m_Uniques As Collection
Private m_Indexes As Collection
Private m_FKNames As Collection  ' Internal blacklist

Private Sub Class_Initialize()
    Set m_PKs = New Collection
    Set m_FKs = New Collection
    Set m_Uniques = New Collection
    Set m_Indexes = New Collection
    Set m_FKNames = New Collection
End Sub

Private Sub Class_Terminate()
    Set m_PKs = Nothing
    Set m_FKs = Nothing
    Set m_Uniques = Nothing
    Set m_Indexes = Nothing
    Set m_FKNames = Nothing
End Sub

Public Property Get tableName() As String
    tableName = m_tableName
End Property
Public Property Let tableName(ByVal vNewValue As String)
    m_tableName = vNewValue
End Property

Public Property Get RecordCount() As Long
    RecordCount = m_count
End Property
Public Property Let RecordCount(ByVal vNewValue As Long)
    m_count = vNewValue
End Property

Public Property Get PKCollection() As Collection
    Set PKCollection = m_PKs
End Property
Public Property Get FKCollection() As Collection
    Set FKCollection = m_FKs
End Property
Public Property Get UniqueCollection() As Collection
    Set UniqueCollection = m_Uniques
End Property
Public Property Get IndexCollection() As Collection
    Set IndexCollection = m_Indexes
End Property

Public Sub AddPK(ByVal fieldName As String)
    If fieldName <> "" Then m_PKs.Add fieldName
End Sub

' UPDATED: Now accepts the original db relationship constraint name
Public Sub AddFK(ByVal fkName As String, ByVal localField As String, ByVal refTable As String, ByVal refField As String)
    If localField <> "" Then
        m_FKs.Add fkName & "|" & localField & "|" & refTable & "|" & refField
        RegisterFKName fkName
    End If
End Sub

Public Sub RegisterFKName(ByVal constraintName As String)
    On Error Resume Next
    m_FKNames.Add constraintName, constraintName
    On Error GoTo 0
End Sub

Public Function IsAnFKName(ByVal indexName As String) As Boolean
    Dim test As String
    On Error Resume Next
    test = m_FKNames(indexName)
    IsAnFKName = (Err.Number = 0)
    On Error GoTo 0
End Function

Public Sub AddIndexOrUnique(ByVal indexName As String, ByVal fieldsList As String, ByVal isUnique As Boolean)
    If isUnique Then
        m_Uniques.Add indexName & "|" & fieldsList
    Else
        m_Indexes.Add indexName & "|" & fieldsList
    End If
End Sub

