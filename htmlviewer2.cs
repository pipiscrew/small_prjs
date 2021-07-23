using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace htmlviewer2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.Text = Application.ProductName + " v" + Application.ProductVersion;

            FillKeywords();
            FillFiles();

            PreRead();
        }

        internal string keywords = "";

        internal void FillKeywords()
        {
            List<string> keywordsList = null;
            string fl = Application.StartupPath + "\\keywords.txt";

            if (!File.Exists(fl))
            {
                MessageBox.Show("File not found\r\n\r\n" + fl);
                return;
            }

            keywordsList = File.ReadAllLines(fl).ToList();

            keywords = @"\b(" + string.Join("|", keywordsList) + @")\b";

        }
        internal void FillFiles()
        {
            var directory = new DirectoryInfo(Application.StartupPath);
            List<FileInfo> files = directory.GetFiles("*.html").OrderBy(x => x.FullName).ToList();

            foreach (FileInfo item in files)
            {
                lstv.Items.Add(item.Name);
            }

            if (lstv.Items.Count > 0)
                lstv.Items[0].Selected = true;
        }

        private void lstv_MouseClick(object sender, MouseEventArgs e)
        {
            //if (lstv.SelectedItems.Count == 0)
            //    return;

            //ReadHTML(lstv.SelectedItems[0].Text);
        }


        internal void PreRead()
        {
            if (lstv.SelectedItems.Count == 0)
                return;

            ReadHTML(lstv.SelectedItems[0].Text);
        }

        internal void ReadHTML(string fl)
        {
            fl = Application.StartupPath + "\\" + fl;

            if (!File.Exists(fl))
            {
                MessageBox.Show("File not found\r\n\r\n" + fl);
                return;
            }

            string k = File.ReadAllText(fl);
            
            if (!string.IsNullOrEmpty(keywords))
            //"(fantasies|occasionally)"
            k = Regex.Replace(k, keywords, "<span style='background:yellow'>$1</span>", RegexOptions.IgnoreCase);

            html.DocumentHtml = k;
        }

        private void lstv_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            PreRead();
        }

        private void toolStripShowHide_Click(object sender, System.EventArgs e)
        {
            lstv.Visible = !lstv.Visible;
        }

    }
}
