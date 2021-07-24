using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

//use of .NET WebControl - Microsoft.mshtml.dll
namespace htmlviewer2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.Text = Application.ProductName + " v" + Application.ProductVersion;
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            //wbrDisplay.Navigate("about:blank");
            ////while (wbrDisplay.ReadyState != WebBrowserReadyState.Complete)
            ////{
            ////}

            //this.wbrDisplay.Url = new System.Uri("https://www.someURL.com", System.UriKind.Absolute);
            //while (wbrDisplay.ReadyState != WebBrowserReadyState.Complete)
            //{
            //   // Application.DoEvents();
            //}
            wbrDisplay.Navigate("about:blank");
            if (wbrDisplay.Document != null)
            {
                wbrDisplay.Document.Write(string.Empty);
            }
            wbrDisplay.DocumentText = "";

            FillKeywords();
            FillFiles();

             //PreRead();
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

            //keywords = @"\b(" + string.Join("|", keywordsList) + @")\b";
            keywords = "(" + string.Join("|", keywordsList) + ")";

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

           //method1
            //HtmlDocument doc = wbrDisplay.Document;
            ////if (doc != null && doc.Body != null)
                //doc.Body.InnerHtml = k;

            //method2
             wbrDisplay.DocumentText = k;
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
