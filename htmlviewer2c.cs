using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        List<string> files = null;
        internal void FillFiles()
        {
            var directory = new DirectoryInfo(Application.StartupPath);
            files = directory.GetFiles("*.html").OrderBy(x => x.FullName).Select(x => x.Name).ToList();

            lstv.BeginUpdate();

            foreach (string item in files)
            {
                lstv.Items.Add(item);
            }

            if (lstv.Items.Count > 0)
                lstv.Items[0].Selected = true;

            lstv.EndUpdate();
        }

        internal void PreRead()
        {
            if (lstv.SelectedItems.Count == 0)
                return;

            ReadHTML(lstv.SelectedItems[0].Text);
        }

        bool intenralNavigate = false;

        internal void ReadHTML(string fl)
        {
            fl = Application.StartupPath + "\\" + fl;

            if (!File.Exists(fl))
            {
                MessageBox.Show("File not found\r\n\r\n" + fl);
                return;
            }

            SetHTML(fl);
        }

        internal void SetHTML(string fl)
        {
            intenralNavigate = true;

            string k = File.ReadAllText(fl);

            if (!string.IsNullOrEmpty(keywords))
                k = Regex.Replace(k, keywords, "<span style='background:yellow'>$1</span>", RegexOptions.IgnoreCase);

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


        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) && (textBox1.Text.Length == 0))
            {
                if (lstv.SelectedItems.Count > 0)
                    lstv.SelectedItems.Clear();

                lstv.BeginUpdate();

                lstv.Items.Clear();

                foreach (string item in files)
                {
                    lstv.Items.Add(item);
                }

                lstv.EndUpdate();
            }
            else if ((lstv.Items.Count > 0) && (e.KeyChar == 13) && (textBox1.Text.Length > 0))
            {
                e.Handled = true;

                int prevIndex = 0;
                if (lstv.SelectedItems.Count > 0 && lstv.SelectedItems[0].Text.ToLower().Contains(textBox1.Text.ToLower()))
                    prevIndex = lstv.SelectedItems[0].Index + 1;

                if (lstv.SelectedItems.Count > 0)
                    lstv.SelectedItems.Clear();

                var filesSearch = files.Where(x => x.IndexOf(textBox1.Text.Trim(), StringComparison.OrdinalIgnoreCase) > -1).OrderBy(x => x).ToList();

                lstv.BeginUpdate();

                lstv.Items.Clear();
                foreach (string item in filesSearch)
                {
                    lstv.Items.Add(item);
                }

                lstv.EndUpdate();
            }
        }

        //support highlighting when a html drg&drp to webcontrol
        private void wbrDisplay_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        { //https://stackoverflow.com/a/3184763

            if (intenralNavigate)
            { intenralNavigate = false; }
            else
            {
                string fl = e.Url.ToString();

                if (!fl.StartsWith("file:///"))
                    return;
                else if (!fl.ToLower().EndsWith(".html"))
                {
                    e.Cancel = true;
                    return;
                }
                else
                    e.Cancel = true;

                lstv.SelectedItems.Clear();

                fl = e.Url.ToString().Replace(@"file:///", "");
                
                SetHTML(fl);
            }

        }

        private void toolWordlist_Click(object sender, EventArgs e)
        {
            string fl = Application.StartupPath + "\\keywords.txt";

            if (!File.Exists(fl))
            {
                MessageBox.Show("File not found\r\n\r\n" + fl);
                return;
            }

            Process.Start(fl);
        }

        private void toolWordListRefresh_Click(object sender, EventArgs e)
        {
            FillKeywords();
            PreRead();
        }

        private void wbrDisplay_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //disable the F5
            if (e.KeyData == Keys.F5)
            {
                e.IsInputKey = true;

                PreRead();
            }
        }


    }
}
