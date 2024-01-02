# I know a guy that knows another **guy** that surfing at **darkweb** that knows a **bitch** which his **boyfriend** d3v3l0ped a powershell called **unServiceWorker.ps1**

$code = @'
using System.Windows.Forms;
using System.Text;
using System.Linq;
using System.IO;
using System;

public class General
{
	public static string Patch(string f) {
		byte[] srcChromeDLL = new byte[]{ 0x66, 0x69, 0x6C, 0x65, 0x53, 0x79, 0x73, 0x74, 0x65, 0x6D, 0x00, 0x61, 0x70, 0x70, 0x63, 0x61, 0x63, 0x68, 0x65, 0x00, 0x73, 0x65, 0x72, 0x76, 0x69, 0x63, 0x65, 0x57, 0x6F, 0x72, 0x6B, 0x65, 0x72 };
		byte[] replaceChromeDLL = new byte[]{ 0x66, 0x69, 0x6C, 0x65, 0x53, 0x79, 0x73, 0x74, 0x65, 0x6D, 0x00, 0x61, 0x70, 0x70, 0x63, 0x61, 0x63, 0x68, 0x65, 0x00 };
		byte[] repl2 = replaceChromeDLL.Concat(Encoding.ASCII.GetBytes(General.GenerateRandomWord(13))).ToArray();
		bool patched = false;
		
		if (File.Exists(f) && General.ReplaceBytesInFile(f, srcChromeDLL, repl2))
		{
			patched = true;
			return "Patch success! \r\n\r\nDelete the 'Service Worker' folder is into 'Default' folder \r\n\r\n" + Path.Combine(General.GetUserLocalAppDataPath(), "BraveSoftware") + "\r\nor\r\n" + Path.Combine(General.GetUserLocalAppDataPath(), "Chromium");
		}
		else if (File.Exists(f))
		{
			byte[] srcElectronEXE = new byte[]{ 0x66, 0x69, 0x6C, 0x65, 0x53, 0x79, 0x73, 0x74, 0x65, 0x6D, 0x00, 0x73, 0x65, 0x72, 0x76, 0x69, 0x63, 0x65, 0x57, 0x6F, 0x72, 0x6B, 0x65, 0x72 };
			byte[] replaceElectronEXE = new byte[]{ 0x66, 0x69, 0x6C, 0x65, 0x53, 0x79, 0x73, 0x74, 0x65, 0x6D, 0x00 };
			byte[] repl3 = replaceElectronEXE.Concat(Encoding.ASCII.GetBytes(General.GenerateRandomWord(13))).ToArray();

			if (General.ReplaceBytesInFile(f, srcElectronEXE, repl3))
			{
				patched = true;
				return "Patch success! \r\n\r\nDelete the 'Service Worker' folder is into 'Default' folder \r\n\r\n" + Path.Combine(General.GetUserAppDataRoamingPath(), "Electron application name") + "\r\nor\r\n" + Path.Combine(General.GetUserAppDataRoamingPath(), "Electron application name\\Partitions\\x\\");
			}
		}
		
		if (!patched)
			return "Failed";
		else 
			return "Failed";
	}
	
	internal static string GenerateRandomWord(int length)
	{
		const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
		Random random = new Random();

		return new string(Enumerable.Repeat(allowedChars, length)
			.Select(s => s[random.Next(s.Length)]).ToArray());
	}
	
	internal static string GetUserLocalAppDataPath()
	{
		string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
		return localAppDataPath;
	}
	
	internal static string GetUserAppDataRoamingPath()
	{
		string appDataRoamingPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
		return appDataRoamingPath;
	}

	internal static bool ReplaceBytesInFile(string inputFileName, byte[] searchTextBytes, byte[] replaceTextBytes)
	{
		bool success = false;
		try
		{
			byte[] fileBytes = File.ReadAllBytes(inputFileName);
			for (int i = 0; i <= fileBytes.Length - searchTextBytes.Length; i++)
			{
				bool match = true;

				for (int j = 0; j < searchTextBytes.Length; j++)
				{
					if (fileBytes[i + j] != searchTextBytes[j])
					{
						match = false;
						break;
					}
				}

				if (match)
				{
					success = true;
					Array.Copy(replaceTextBytes, 0, fileBytes, i, replaceTextBytes.Length);
					File.WriteAllBytes(inputFileName, fileBytes);
					break;
				}
			}

			return success;
		}
		catch (Exception ex)
		{
			MessageBox.Show("An error occurred: " + ex.Message);
			return false;
		}
	}
		
}
'@

if (-not ('General' -as [type])) {
    $refs = @(
        'System.Windows.Forms',
        'System.Xml.Linq'
    )

    Add-Type -TypeDefinition $code -ReferencedAssemblies $refs
}

cls
Write-Host ** unServiceWorker for Chromium like browsers **
Write-Host make sure near this powershell script, the chrome.dll exists
Write-Host make sure the browser is not running
Write-Host
read-host "Press ENTER to continue..."


[string] $c = Get-Location
[string] $p = [IO.Path]::Combine($c, 'chrome.dll')
[string] $w = [General]::Patch($p)
Write-Host $w
Write-Host
read-host "Press ENTER to exit..."