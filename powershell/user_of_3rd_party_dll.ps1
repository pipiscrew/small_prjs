/////////////
<#
https://www.nuget.org/packages/Oracle.ManagedDataAccess
https://blog.ironmansoftware.com/daily-powershell-2/
https://learn.microsoft.com/en-us/powershell/module/microsoft.powershell.utility/add-type?view=powershell-7.3
https://renenyffenegger.ch/notes/Microsoft/dot-net/namespaces-classes/Oracle/DataAccess/Client/_example/PowerShell
#>

[void][Reflection.Assembly]::LoadFile("C:\Temp\Oracle.ManagedDataAccess.dll")

$conn = New-Object Oracle.ManagedDataAccess.Client.OracleConnection('Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=x.x.x.com)(PORT=1000)))(CONNECT_DATA=(SERVICE_NAME=UAT)));User Id=x;Password=x')

$conn.Open()

$stmt = $conn.CreateCommand()
$stmt.CommandText = 'select id,name,address from customers where rownum < 10'

$r = $stmt.ExecuteReader()

while ($r.Read()) {
   write-host "$($r.GetOracleString(0).Value)|$($r.GetOracleString(1).Value)|$($r.GetOracleString(2).Value)"
}

$conn.Close()