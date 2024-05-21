using System;
using System.Drawing;
using System.Windows.Forms;

public class CustomTextBox : TextBox
{
    public CustomTextBox()
    {
        this.Enter += CustomTextBox_Enter;
        this.Leave += CustomTextBox_Leave;
    }

    private void CustomTextBox_Enter(object sender, EventArgs e)
    {
        this.BackColor = Color.Yellow; 
    }

    private void CustomTextBox_Leave(object sender, EventArgs e)
    {
        this.BackColor = Color.White;
    }
}
