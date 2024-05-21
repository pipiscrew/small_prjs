using System.Windows.Forms;

internal static class General
{
    internal static DBASEWrapper db;

    internal static DialogResult Mes(string descr, MessageBoxIcon icon = MessageBoxIcon.Information, MessageBoxButtons butt = MessageBoxButtons.OK)
    {
        if (descr.Length > 0)
            return MessageBox.Show(descr, Application.ProductName, butt, icon);
        else
            return DialogResult.OK;
    }


}


