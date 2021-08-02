using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Mammoth;

/**
* @link https://pipiscrew.com
* @copyright Copyright (c) 2021 PipisCrew
*/
// ref lib - https://github.com/mwilliamson/dotnet-mammoth

namespace word2html
{
    public partial class Form1 : Form
    {
        string appname = "word2html - mammoth.docx2html";

        public Form1()
        {
            InitializeComponent();

            this.Text = appname;
        }

        #region " LSTV DRAG and DROP "

        private void lstv_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Move | DragDropEffects.Copy | DragDropEffects.Scroll;
            }
        }

        private void lstv_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] data = (string[])e.Data.GetData(DataFormats.FileDrop);

                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i].ToLower().EndsWith(".docx"))
                    {
                        ListViewItem h = new ListViewItem(Path.GetFileName(data[i]));
                        h.Tag = new Litem(data[i]);

                        h.SubItems.Add("");

                        lstv.Items.Add(h);
                    }

                    button2.Enabled = false;
                }

                this.Text = appname + " - files loaded : " + lstv.Items.Count;
            }
        }

        #endregion


        internal class Litem
        {
            public string filepath { get; set; }
            public List<string> w { get; set; }

            public Litem(string filepath)
            {
                this.filepath = filepath;
                w = new List<string>();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lstv.Items.Count == 0)
                return;
            else
            {
                ClearAll();
            }

            foreach (ListViewItem item in lstv.Items)
            {
                Litem file = (Litem)item.Tag;
                var converter = new DocumentConverter();
                var result = converter.ConvertToHtml(file.filepath);
                var html = result.Value; // The generated HTML
                var warnings = result.Warnings; // Any warnings during conversion

                file.w = warnings.ToList<string>();

                item.SubItems[1].Text = file.w.Count.ToString();

                if (file.w.Count > 0)
                {
                    txtWarnings.AppendText("\r\n##" + item.Text + "##\r\n");
                    txtWarnings.AppendText(string.Join("\r\n", file.w) + "\r\n");
                }
            }

            //find all unknown paragraph styles
            MatchCollection ms = Regex.Matches(txtWarnings.Text, "Unrecognised paragraph style(.*)(Style ID: (.*)\\))");

            foreach (Match m in ms)
            {
                string matchSTR = m.Groups[3].Value;

                //check if already exist to datagrid
                int occurrences = (dg.Rows.Cast<DataGridViewRow>()
                    .Where(c => c.Cells[0].Value != null && c.Cells[0].Value.ToString().Equals(matchSTR))).Count();

                if (occurrences == 0)
                {
                    dg.Rows.Add();
                    dg.Rows[dg.Rows.Count - 1].Cells[0].Value = matchSTR;
                    dg.Rows[dg.Rows.Count - 1].Cells[1].Value = "h1:fresh";
                }
            }

            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (lstv.Items.Count == 0)
                return;


            string styleTemplate = "p[style-name='{0}'] => {1}";

            List<string> gg = (dg.Rows.Cast<DataGridViewRow>()
             .Where(c => c.Cells[0].Value != null && c.Cells[1].Value != null).Select(x => string.Format(styleTemplate, x.Cells[0].Value.ToString().Trim(), x.Cells[1].Value.ToString().Trim())

             )).ToList();

            string styleTemplateMaterialize = string.Join("\n", gg);

            string destPath, filename, t;
            t = "{0}\\{1}.html";

            foreach (ListViewItem item in lstv.Items)
            {
                Litem file = (Litem)item.Tag;
                var converter = chkReplaceStyles.Checked ? new DocumentConverter().AddStyleMap(styleTemplateMaterialize) : new DocumentConverter();
                var result = converter.ConvertToHtml(file.filepath);
                var html = result.Value;

                destPath = Path.GetDirectoryName(file.filepath);
                filename = Path.GetFileNameWithoutExtension(file.filepath);

                File.WriteAllText(string.Format(t, destPath, filename), html, new UTF8Encoding(false)); //ANSI
            }

            MessageBox.Show("Completed, please find the html near the docx file.", appname, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lstv.Items.Clear();

            ClearAll();

            this.Text = appname;

            lstv.Refresh();
        }

        internal void ClearAll()
        {
            txtWarnings.Text = "";
            dg.Rows.Clear();
        }
    }
}
