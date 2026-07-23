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
Private m_PK As String
Private m_FK As String
Private m_Unique As String

Public Property Get TableName() As String
    TableName = m_tableName
End Property
Public Property Let TableName(ByVal vNewValue As String)
    m_tableName = vNewValue
End Property

Public Property Get RecordCount() As Long
    RecordCount = m_count
End Property
Public Property Let RecordCount(ByVal vNewValue As Long)
    m_count = vNewValue
End Property

' Automatically generates the ALTER TABLE statement for the PK
Public Property Get PK() As String
    PK = m_PK
End Property
Public Property Let PK(ByVal fieldName As String)
    If fieldName <> "" Then
        m_PK = "ALTER TABLE [" & m_tableName & "] ADD CONSTRAINT [PK_" & m_tableName & "] PRIMARY KEY ([" & fieldName & "]);"
    Else
        m_PK = ""
    End If
End Property

' Automatically generates the ALTER TABLE statement for the FK
' Note: For an exact match, we pass the "FieldName, TargetTable, TargetField" separated by pipes
Public Property Get FK() As String
    FK = m_FK
End Property
Public Property Let FK(ByVal fkDetails As String)
    If fkDetails <> "" Then
        Dim parts() As String
        parts = Split(fkDetails, "|") ' Expecting: LocalField|RefTable|RefField
        
        m_FK = "ALTER TABLE [" & m_tableName & "] ADD CONSTRAINT [FK_" & m_tableName & "_" & parts(0) & "] " & _
               "FOREIGN KEY ([" & parts(0) & "]) REFERENCES [" & parts(1) & "] ([" & parts(2) & "]);"
    Else
        m_FK = ""
    End If
End Property

' Automatically generates the ALTER TABLE statement for the UNIQUE constraint
Public Property Get Unique() As String
    Unique = m_Unique
End Property
Public Property Let Unique(ByVal fieldName As String)
    If fieldName <> "" Then
        m_Unique = "ALTER TABLE [" & m_tableName & "] ADD CONSTRAINT [UQ_" & m_tableName & "_" & fieldName & "] UNIQUE ([" & fieldName & "]);"
    Else
        m_Unique = ""
    End If
End Property


