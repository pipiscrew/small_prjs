enum EntryCategory {
    Programs
    InternetBrowsers
    RuntimesFrameworks
	ProgramsRepack
	Tweaks
	WinActivators
	Other
}

class Product {
    [int] $cmdNo
	[EntryCategory] $category
    [string] $appName
    [string] $executable
	[string] $execSilentSwitch
	[string] $downloadURL
	[string] $downloadURL2
}

function PrintRow {
    param (
        [string] $a,
        [int] $b
    )

	$charToRepeat = $a
	$repeatCount = $b
	$repeatedString = $charToRepeat * $repeatCount
	
	Write-Host $repeatedString -ForegroundColor yellow
}

function Get-WindowsInfo {
    $osInfo = Get-WmiObject -Class Win32_OperatingSystem
    $windowsVersion = $osInfo.Caption
    $architecture = $osInfo.OSArchitecture
	$computerName = $env:COMPUTERNAME

    $info = @{
        Version = $windowsVersion
        Architecture = $architecture
		ComputerName = $computerName
    }

    return $info
}

function GetProducts() {
	# Create an ArrayList to store Product objects
	$productList = New-Object System.Collections.ArrayList

	# Create Product objects and add them to the list
	$product1 = [Product]::new()
	$product1.CmdNo = 1
	$product1.Category = "Programs"
	$product1.AppName = "App1"
	$product1.Executable = "App1.exe"
	$product1.execSilentSwitch = "/silent"
	$product1.DownloadURL = "https://example.com/app1"
	$product1.DownloadURL2 = "https://example.com/app1/alternate"

	$product2 = [Product]::new()
	$product2.CmdNo = 2
	$product2.Category = "Programs"
	$product2.AppName = "App2"
	$product2.Executable = "App2.exe"
	$product2.execSilentSwitch = "/quiet"
	$product2.DownloadURL = "https://example.com/app2"
	$product2.DownloadURL2 = "https://example.com/app2/alternate"

	$product3 = [Product]::new()
	$product3.CmdNo = 3
	$product3.Category = "InternetBrowsers"
	$product3.AppName = "App3"
	$product3.Executable = "App3.exe"
	$product3.execSilentSwitch = "/s"
	$product3.DownloadURL = "https://example.com/app3"
	$product3.DownloadURL2 = "https://example.com/app3/beta"
	
	# Add the Product objects to the ArrayList
	$productList.Add($product1)
	$productList.Add($product2)
	$productList.Add($product3)

	# Display the list
	return $productList
}

function GetSelectedProduct(){
    param (
        [int] $index
    )
	
	# Create a filter to find the Product with CmdNo = 2
	$filter = { $_.CmdNo -eq $index }

	# Use Where-Object to filter the ArrayList
	$desiredProduct = $products | Where-Object $filter

	return $desiredProduct
}

function ProcessSelectedProduct(){
	param (
		[Product] $p
	)

	[string] $c = Get-Location;
	[string] $fullPath = [IO.Path]::Combine($c, 'apps', $p.executable);
	
	 if (-not([System.IO.File]::Exists($fullPath))){
		Write-Host "DOWNLOAD VIA INTERNET & EXECUTE" -ForegroundColor red
	 }
	 else {
		Write-Host "EXECUTE THE FILE" -ForegroundColor red
	 }
}

################################## TOP LAYOUT [start]
PrintRow -a '#' -b 100

# 2nd line - print text with padding
$paddedString = 'Tech Toolbox'.PadLeft(35, ' ')
Write-Host -NoNewline $paddedString -ForegroundColor yellow

# credits + OS
$windowsInfo = Get-WindowsInfo
$paddedString = 'NAME OS : '.PadLeft(30, ' ') + "$($windowsInfo.Version) $($windowsInfo.Architecture)"
Write-Host $paddedString -ForegroundColor yellow

# 3rd line - acronyms
$paddedString = '(Offline)=No Needs Internet | (Pro)=Activated Program'.PadLeft(55, ' ')
Write-Host -NoNewline $paddedString -ForegroundColor yellow

# computer name
$paddedString = 'PC NAME : '.PadLeft(30, ' ') + "$($windowsInfo.ComputerName)"
Write-Host $paddedString -ForegroundColor yellow

PrintRow -a '#' -b 100
################################## TOP LAYOUT [end]

#add entries to class
$products = GetProducts

#$products

# Set the cursor position to row 5, column 10
#[Console]::SetCursorPosition(10, 5)
# Write text at the specified position
#Write-Host "Hello, World!"

################################## PRINT MENU [start]
[string] $prevCategory
foreach ($prod in $products) {
	if ($prod.AppName -eq $null) {
		continue
	 }
	else {
	
		#when change category print the category name
	    if ($prevCategory -ne $prod.category) {
			Write-Host ;
			Write-Host "-- $($prod.category) --"
		}
		
		#print product
		Write-Host "[$($prod.CmdNo)] $($prod.AppName)"
		
		$prevCategory = $prod.category
	}
}
################################## PRINT MENU [end]


################################## wait for user option
do {
    $choice = Read-Host "Enter your choice (1-9 or 0 to exit)"
    switch ($choice) {
        "1111" { #wiil be needed for nonProduct options
            Write-Host "You selected Option 1111"
            # Perform action for Option 1
        }
        "0" {
            Write-Host "Exiting..."
            # You can add cleanup or exit logic here
        }
        default {
			$selected = GetSelectedProduct -index $choice
			
			if (!$selected) {
				Write-Host "Invalid choice. Please select a valid option (1-9 or 0 to exit)."
				$choice = $null # Reset choice to continue the loop
			}
			else {
				ProcessSelectedProduct -p $selected
			}
			
        }
    }
} while ($choice -ne "0")
