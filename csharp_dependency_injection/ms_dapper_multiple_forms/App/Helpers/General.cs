using System;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace App.Helpers
{
    internal static class General
    {
        internal static DialogResult Mes(string descr, MessageBoxIcon icon = MessageBoxIcon.Information, MessageBoxButtons butt = MessageBoxButtons.OK)
        {
            if (descr.Length > 0)
                return MessageBox.Show(descr, Application.ProductName, butt, icon);
            else
                return DialogResult.OK;
        }

        internal static string LoadSetting(string key)
        {
            return ConfigurationManager.AppSettings[key] ?? string.Empty;
        }

        internal static void SaveSetting(string key, string value)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (config.AppSettings.Settings[key] != null)
                config.AppSettings.Settings[key].Value = value;
            else
                config.AppSettings.Settings.Add(key, value);

            config.Save(ConfigurationSaveMode.Modified);

            // Refresh the appSettings section for updated data
            ConfigurationManager.RefreshSection("appSettings");
        }

        internal static bool Export2Excel(DataTable dt, string domain="")
        {
            string now = DateTime.Now.ToString("yyyy-MM-dd_HHmmss");
            string output = string.Format("{0}\\{1}-{2}.xlsx", Application.StartupPath, domain, now);

            var res = ExcelExport.GenerateExcel(now, dt, output, true);

            if (!res)
                General.Mes("Excel cannot be found, operation aborted!", MessageBoxIcon.Exclamation);

            return res;
        }

    }


}
