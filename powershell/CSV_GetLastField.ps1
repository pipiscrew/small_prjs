<#
when you have a CSV file is more than 1.048.576 rows cannot be processed to EXCEL.... 
Microsoft suggesting - https://support.microsoft.com/en-gb/office/what-to-do-if-a-data-set-is-too-large-for-the-excel-grid-976e6a34-9756-48f4-828c-ca80b3d0e15c
but still have the limitation problem, right?

There are cases that we want to just have the value of Last CSV Column only by those > 1 million rows..
#>

param(
 [Parameter(Mandatory = $true)]
 [string]$inputfile,
 [Parameter(Mandatory = $true)]
 [string]$delimeter
)
  
if (![System.IO.File]::Exists($inputfile)) {
    Write-Warning "file does not exists"
    exit
}
  
$Source = @"
using System.IO;
using System.Text;
using System.Windows.Forms;
  
public class General
{
    public static string GetLastField(string filename, string delimeter)
    {
        string filenameNEw  = filename + "_new.txt";
 
        string line = string.Empty;
 
        int lastPipe = 0;
        string lastField = string.Empty;
        StringBuilder sb = new StringBuilder();
 
        using (StreamReader file = new StreamReader(filename))
        {
            while ((line = file.ReadLine()) != null)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    lastPipe = line.LastIndexOf(delimeter);
 
                    if (lastPipe > -1)
                    {
                        lastField = line.Remove(0, lastPipe + 1);
 
                        if (lastField.Contains("-"))
                            sb.AppendLine(lastField);
                    }
                }
            }
        }
 
        File.WriteAllText(filenameNEw, sb.ToString());
  
        MessageBox.Show("done!");
          
        return filenameNEw;
    }
}
"@
  
if ("General" -as [type]) {} else {
    Add-Type -TypeDefinition $Source -ReferencedAssemblies System.Windows.Forms
}
  
  
$fileNew = [General]::GetLastField($inputfile, $delimeter)
Write-Host "$fileNew successfully generated!" -ForegroundColor Green