<#
__________.__       .__       _________                        
\______   \__|_____ |__| _____\_   ___ \_______   ______  _  __
 |     ___/  \____ \|  |/  ___/    \  \/\_  __ \_/ __ \ \/ \/ /
 |    |   |  |  |_> >  |\___ \\     \____|  | \/\  ___/\     / 
 |____|   |__|   __/|__/____  >\______  /|__|    \___  >\/\_/  
             |__|           \/        \/             \/        
             
what does
---------
with the use of (https://github.com/cognidox/OfficeToPDF) allows to batch convert docx to PDF


how to use 
----------
1-create a text file, with this script (fix line 91), name it batch.ps1, near the docx files.
2-on the directory the docx files, press Shidt + Right Click > 'Open PowerShell window here'
3-type without the quotes : '.\batch.ps1' and press ENTER
4-wait till you get 'Press any key to continue...'

references :
list files - https://stackoverflow.com/questions/18847145/loop-through-files-in-a-directory-using-powershell
execute exe - https://stackoverflow.com/questions/4639894/executing-an-exe-file-using-a-powershell-script
execute exe - https://social.technet.microsoft.com/wiki/contents/articles/7703.powershell-running-executables.aspx
execute exe - https://pwrshell.net/using-third-party-executables/
concat - https://blogs.technet.microsoft.com/heyscriptingguy/2014/07/15/keep-your-hands-clean-use-powershell-to-glue-strings-together/
IDE - C:\Windows\System32\WindowsPowerShell\v1.0\powershell_ise.exe

#>

#add a reference to winforms for messagebox
Add-Type -AssemblyName System.Windows.Forms


#check if winword is present - https://stackoverflow.com/a/28482050
$wword = Get-Process winword -ErrorAction SilentlyContinue
if ($wword) {
    #ask user if he likes to shutdown the word
    $res = [System.Windows.Forms.MessageBox]::Show('The WinWord is running, must close are you sure?','PipisCrew','YesNoCancel','Error')

	#https://mcpmag.com/articles/2016/03/09/working-with-the-if-statement.aspx
    if ($res -eq "No" -Or $res -eq "Cancel"){
		exit # or return to skip the loop for this item
	}


  # try gracefully first
  $wword.CloseMainWindow()

  # kill after five seconds
  Sleep 5

  if (!$wword.HasExited) {
    $wword | Stop-Process -Force
  }
}
Remove-Variable wword

# http://get-powershell.com/post/2008/02/22/Powershell-Function-Start-Proc.aspx
function Start-Proc  {
    param (
            [string]$exe = $(Throw "An executable must be specified"),
            [string]$arguments,
            [switch]$hidden,
            [switch]$waitforexit
            )    
    
    # Build Startinfo and set options according to parameters
    $startinfo = new-object System.Diagnostics.ProcessStartInfo 
    $startinfo.FileName = $exe
    $startinfo.Arguments = $arguments
    if ($hidden){
                $startinfo.WindowStyle = "Hidden"
                $startinfo.CreateNoWindow = $TRUE
                }
    $process = [System.Diagnostics.Process]::Start($startinfo)
    if ($waitforexit) {$process.WaitForExit()}
    
}


#get current dir object
$path = (Get-Item -Path ".\").FullName 

#loop through the items
Get-ChildItem $path -Filter *.docx | 
Foreach-Object {

   #outputs only the filename Write-Host $_.BaseName
   
   #outputs the fullfilepath
   Write-Host $_.FullName
   
   $arg = "/excludeprops " + $_.FullName

   Start-Proc 'C:\Users\x\Downloads\OfficeToPDF.exe' $arg -hidden -waitforexit
}

#https://stackoverflow.com/a/20886446
Write-Host -NoNewLine 'Press any key to continue...';
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');