using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace HandleCSVdata
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] lst = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            if (lst[0].ToLower().EndsWith(".txt") || lst[0].ToLower().EndsWith(".dat"))
                (sender as TextBox).Text = lst[0];
        }

        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //LOAD DATA from CSV files
            DataTable dt1 = LoadCSV(txt1.Text);
            DataTable dt2 = LoadCSV(txt2.Text);

            //MERGE Datatables (contains same columns)
            dt1.Merge(dt2);

            //To the nerged table name the needed columns
            dt1.Columns[0].ColumnName = "col1";
            dt1.Columns[dt1.Columns.Count - 1].ColumnName = "col2";

            //create a Dataview so by able to sort by column (col2 = country)
            // *************** DATAVIEW is not requirement ***************
            // at first thought to do the casual loop with textfile lines, so should be sorted by the col2 to match the same country
            DataView dv = dt1.DefaultView;
            dv.Sort = "col2";  


            //NORMALIZE COUNTRY
            foreach (DataRowView r in dv)
            {
                r["col2"] = GetOrg(r["col2"].ToString());
            }

            //LINQ to DatatableVIEW - GROUP BY COUNTRY
            var kk = dv.OfType<DataRowView>().GroupBy(x => x.Row["col2"]).ToList();


            //EXPORT FILENAME TEMPLATE
            string filename = "{0}_2023.dat";
            string currFilename = string.Empty;
            string gropuName = string.Empty;
            StringBuilder sb = null;

            //FOR EACH GROUPING
            foreach (var item in kk)
            {
                sb = new StringBuilder();

                //catch ones with empoty COUNTRY
                gropuName = item.Key.ToString();
                if (string.IsNullOrEmpty(gropuName))
                    gropuName = "EMPTY";


                //prepare filename
                currFilename = string.Format(filename, gropuName);

                if (File.Exists(currFilename))
                    MessageBox.Show("File exists\r\n" + currFilename);

                //for each row in GROUP
                foreach (var d in item)
                {
                    sb.AppendLine(d.Row[0].ToString());
                }

                File.WriteAllText(currFilename, sb.ToString());
            }

            MessageBox.Show("Splitted! Please check near directory!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private Regex rg = new Regex("^MICROSOFT (.*)", RegexOptions.Compiled);

        private string GetOrg(string org)
        {
            MatchCollection match = rg.Matches(org);

            if (match.Count > 0)
                return match[0].Groups[1].Value;
            else
                return org;
        }

        public DataTable LoadCSV(string filename)
        {
            //out params
            string delimiter = "\\|";
            bool col_headers = false;
            bool eliminate_quotes = false;
            //ClearAll();


            DataTable dt = new DataTable();

            try
            {
                using (StreamReader file = new StreamReader(filename))
                {
                    //read the first line
                    string line = file.ReadLine();

                    if (line == null)
                        throw new ArgumentException("error on CSV");



                    //split the first line to create columns to datatable!
                    string[] columns = Regex.Split(line, delimiter).Select(x => x.Trim()).ToArray();

                    for (int i = 0; i < columns.Count(); i++)
                    {
                        if (col_headers)
                            dt.Columns.Add(columns[i].Replace("\"", ""));
                        else
                            dt.Columns.Add("no" + i.ToString());
                    }

                    if (!col_headers)
                    {
                        //rewind reader to start!
                        file.DiscardBufferedData();
                        file.BaseStream.Seek(0, SeekOrigin.Begin);
                        file.BaseStream.Position = 0;
                    }

                    while ((line = file.ReadLine()) != null)
                    {
                        if (line.Contains("Yemen Dummy Territory for ISL"))
                            Console.Write("df");

                        if (line.Trim().Length > 0)
                        {
                            if (eliminate_quotes)
                                line = line.Replace("\"", "");

                            string[] rows = Regex.Split(line, delimiter);
                            dt.Rows.Add(rows);

                        }
                    }
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message + "\r\n---\r\n" + x.StackTrace, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



            return dt;
        }


    }
}
