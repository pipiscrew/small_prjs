#src - https://tekcookie.com/send-email-from-outlook-using-powershell/

$outlook = new-object -comobject outlook.application

$email = $outlook.CreateItem(0)
$email.To = "info@tekcookie.com"
$email.Subject = "New email test"
$email.Body = "This is a testing email" 
$email.Attachments.add("D:\Files\project-report.pptx")

$email.Send()
$outlook.Quit()

<#
	Even is possible to put Outlook in online mode with 
	($outlook.ActiveExplorer()).CommandBars.ExecuteMso("ToggleOnline")
	 
	and disable 'sent item storage' using 
	$email.DeleteAfterSubmit = "True"
#>
