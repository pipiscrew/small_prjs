#add type as we gonna manipulate Forms components
Add-Type -AssemblyName System.Windows.Forms

#enable as winforms application doing
[System.Windows.Forms.Application]::EnableVisualStyles()

#cisntantiate a new form to a variable
$Form1 = New-Object -TypeName System.Windows.Forms.Form

#the controls of the form
[System.Windows.Forms.Button]$Button1 = $null
[System.Windows.Forms.TextBox]$TextBox1 = $null
[System.Windows.Forms.Label]$Label1 = $null

#this tie with the Form Button Click see add_Click
$btnClick = {

    if ($TextBox1.TextLength -eq 0)
    {
        [System.Windows.Forms.MessageBox]::Show('Please enter your name','Powershell', 0, 48)
        return
    }
	
	[General]::ReadTextboxValue($TextBox1.Text)
	
	[System.Windows.Forms.MessageBox]::Show("This is by powershell `n`n" + $TextBox1.Text,'Powershell', 0, 48)
	
	if ($TextBox1.Text -eq 'test') {
		$Form1.Close()
	}
}

#this is the converted C# InitializeComponent Form,  to Powershell
function InitializeComponent
{
	$Button1 = (New-Object -TypeName System.Windows.Forms.Button)
	$TextBox1 = (New-Object -TypeName System.Windows.Forms.TextBox)
	$Label1 = (New-Object -TypeName System.Windows.Forms.Label)
	$Form1.SuspendLayout()
	#
	#Button1
	#
	$Button1.Location = (New-Object -TypeName System.Drawing.Point -ArgumentList @([System.Int32]12,[System.Int32]84))
	$Button1.Name = [System.String]'Button1'
	$Button1.Size = (New-Object -TypeName System.Drawing.Size -ArgumentList @([System.Int32]243,[System.Int32]29))
	$Button1.TabIndex = [System.Int32]0
	$Button1.Text = [System.String]'messagebox me'
	$Button1.UseVisualStyleBackColor = $true
	$Button1.add_Click($btnClick)
	#
	#TextBox1
	#
	$TextBox1.Font = (New-Object -TypeName System.Drawing.Font -ArgumentList @([System.String]'Consolas',[System.Single]12,[System.Drawing.FontStyle]::Regular,[System.Drawing.GraphicsUnit]::Point,([System.Byte][System.Byte]161)))
	$TextBox1.Location = (New-Object -TypeName System.Drawing.Point -ArgumentList @([System.Int32]12,[System.Int32]41))
	$TextBox1.Name = [System.String]'TextBox1'
	$TextBox1.Size = (New-Object -TypeName System.Drawing.Size -ArgumentList @([System.Int32]243,[System.Int32]26))
	$TextBox1.TabIndex = [System.Int32]1
	$TextBox1.TextAlign = [System.Windows.Forms.HorizontalAlignment]::Center
	#
	#Label1
	#
	$Label1.Font = (New-Object -TypeName System.Drawing.Font -ArgumentList @([System.String]'Consolas',[System.Single]12,[System.Drawing.FontStyle]::Regular,[System.Drawing.GraphicsUnit]::Point,([System.Byte][System.Byte]161)))
	$Label1.Location = (New-Object -TypeName System.Drawing.Point -ArgumentList @([System.Int32]12,[System.Int32]9))
	$Label1.Name = [System.String]'Label1'
	$Label1.Size = (New-Object -TypeName System.Drawing.Size -ArgumentList @([System.Int32]160,[System.Int32]29))
	$Label1.TabIndex = [System.Int32]2
	$Label1.Text = [System.String]'enter your name'
	$Label1.add_Click($Label1_Click)
	#
	#Form1
	#
	$Form1.ClientSize = (New-Object -TypeName System.Drawing.Size -ArgumentList @([System.Int32]268,[System.Int32]131))
	$Form1.Controls.Add($Label1)
	$Form1.Controls.Add($TextBox1)
	$Form1.Controls.Add($Button1)
	$Form1.FormBorderStyle = [System.Windows.Forms.FormBorderStyle]::FixedSingle
	$Form1.MaximizeBox = $false
	$Form1.StartPosition = [System.Windows.Forms.FormStartPosition]::CenterScreen
	$Form1.Text = [System.String]'test Winform App'
	$Form1.ResumeLayout($false)
	$Form1.PerformLayout()
	Add-Member -InputObject $Form1 -Name Button1 -Value $Button1 -MemberType NoteProperty
	Add-Member -InputObject $Form1 -Name TextBox1 -Value $TextBox1 -MemberType NoteProperty
	Add-Member -InputObject $Form1 -Name Label1 -Value $Label1 -MemberType NoteProperty
}



$code = @'
using System;
using System.Windows.Forms;

public class General
{
    public static void ReadTextboxValue(string txt)
    {
        MessageBox.Show("This is by C#\n\n" + txt);
    }
}
'@


#load General type to session
if (-not ('General' -as [type])) {

    $refs = @(
        'System.Windows.Forms'
    )

    # Compile and execute C# code with the specified assemblies
    Add-Type -TypeDefinition $code -ReferencedAssemblies $refs
}

#the dot needed - execute /function/ to #current scope# rather into a subshell
. InitializeComponent

#show the form
$Form1.ShowDialog()

[Console]::ReadKey()