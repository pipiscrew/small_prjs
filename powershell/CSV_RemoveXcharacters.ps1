param(
 [Parameter(Mandatory = $true)]
 [string]$inputfile,
 [Parameter(Mandatory = $true)]
 [int]$cutOffset
)
 
if (![System.IO.File]::Exists($inputfile)) {
    Write-Warning "file does not exists"
    exit
} elseif ($cutOffset -lt 1) {
    Write-Warning "please enter a positive number"
    exit
}
 
$Source = @"
using System.IO;
using System.Text;
using System.Windows.Forms;
 
public class General
{
    public static string RemoveXfirstChars(string filename, int valCut)
    {
        StringBuilder sb = new StringBuilder();
 
        string fl = filename;
        string flNew = filename + "_new.txt";
 
        string line = string.Empty;
        using (StreamReader file = new StreamReader(fl))
        {
            while ((line = file.ReadLine()) != null)
            {
                if (line != null)
                    sb.AppendLine(line.Remove(0, valCut));
            }
        }
 
        File.WriteAllText(flNew, sb.ToString());
 
        MessageBox.Show("done!");
         
        return flNew;
    }
}
"@
 
if ("General" -as [type]) {} else {
    Add-Type -TypeDefinition $Source -ReferencedAssemblies System.Windows.Forms
}
 
 
$fileNew = [General]::RemoveXfirstChars($inputfile, $cutOffset)
Write-Host "$fileNew successfully generated!" -ForegroundColor Green