# sqlitex64 https://www.nuget.org/packages/System.Data.SQLite.x64
#
# Load the System.Data.SQLite.dll assembly
# so later can be referenced at ReferencedAssemblies (otherwise looking at powershell directory) - see more https://www.pipiscrew.com/threads/run-csharp-code-to-powershell-and-use-of-3rd-party-dll.85532
Add-Type -Path "$pwd\myDLL\System.Data.SQLite.dll"
# or [System.Reflection.Assembly]::LoadFile("$pwd\myDLL\System.Data.SQLite.dll")


if ($env:PROCESSOR_ARCHITECTURE -eq 'x86') {
    Write-Host 'PowerShell is running as x86 (32-bit).'
} else {
    Write-Host 'PowerShell is running as x64 (64-bit).'
}

$code = @'
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;

public class General
{
    public static string CreateDB(string dbLocation)
    {
        var sqlite = new System.Data.SQLite.SQLiteConnection("Data Source=" + dbLocation);
        sqlite.Open();
 
        var cmd = sqlite.CreateCommand();
        cmd.CommandText = "create table test(col_ID integer primary key, name text, shape blob)";
        cmd.ExecuteNonQuery();
        cmd.Dispose();
 
        sqlite.Close();
        sqlite.Dispose();
 
        MessageBox.Show("Database created!");
        return "returnValue from C#\nfilename: " + dbLocation;
    }
	
	public int Multiply(int a, int b)
	{
		return (a * b);
	}
}
'@

#load the C# code TYPE - and reference to C# code TYPE
#this id xhwxk if the type already loaded (in a PS session, the types loaded stay in memory till we close the PS window)
#so sometimes when try to re execute the same script in same session the type is already there and throws an error
if (-not ('General' -as [type])) {

    $refs = @(
        'System.Data',
        'System.Windows.Forms',
        "$pwd\myDLL\System.Data.SQLite.dll"
    )

    # Compile and execute C# code with the specified assemblies
    Add-Type -TypeDefinition $code -ReferencedAssemblies $refs
}

try {
    $dbPath = "$pwd\dbase.db";

    if (![System.IO.File]::Exists($dbPath)) {
        Write-Warning 'dbase not found, it will be created'
    } else {
		Write-Warning "dbase found, please delete amd rerun the PS`n`nPress any key.."
		[Console]::ReadKey()
		exit
	}

	#instantiate class to access Multiply method [start] - https://learn.microsoft.com/en-us/powershell/module/microsoft.powershell.utility/add-type?view=powershell-7.3#example-1-add-a-net-type-to-a-session
	$instOBJ = New-Object General
	$m = $instOBJ.Multiply(5,5)
	Write-Host $m
	#instantiate class to access Multiply method [end] 
	# or $fileNew = $instOBJ::CreateDB($dbPath)
    $fileNew = [General]::CreateDB($dbPath)
    Write-Host "`n$fileNew successfully generated!" -ForegroundColor Green
}
catch { Write-Error "Ran into an issue: $PSItem" }

Write-Host 'Press any key..'
[Console]::ReadKey()