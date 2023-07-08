using NHibernate;
using NHibernateByCodeSample.Domain;
using System;
using System.Linq;
using System.Windows.Forms;

namespace NHibernateByCodeSample
{
    public partial class frmMainBinding : Form
    {
        internal BindingSource bindSource;

        public frmMainBinding()
        {
            InitializeComponent();

            try
            {
                //to use only NHibernate
                //DBASE.Initialize();

                //initialize along side with SQLConnection 
                DBASE.InitializeWithExistingConnection();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.InnerException.ToString());
                Console.WriteLine(x.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bindSource = new BindingSource();
            PerformBinding();
        }

        private void PerformBinding(string scrollToIndex=null)
        {
            //clear databindings
            groupRecord.Controls.OfType<Control>().Where(x => x.GetType().Equals(typeof(TextBox))).ToList().ForEach(x =>
            {
                x.DataBindings.Clear();
            });

            bindSource.DataSource = null;
            bindSource.DataSource = DBASE.db.GetDATATABLE("select * from customers");
            dg.DataSource = bindSource;

            //one-way binding - after this example - nah.. I will not go with... ;)
            txtCustName.DataBindings.Add(new Binding("Text", this.bindSource, "cust_name", false, DataSourceUpdateMode.Never));
            txtAddress.DataBindings.Add(new Binding("Text", this.bindSource, "cust_addess", false, DataSourceUpdateMode.Never));

            //scroll to new entry
            if (!string.IsNullOrEmpty(scrollToIndex))
            {
                int newEntry = dg.Rows.Cast<DataGridViewRow>().Where(x => x.Cells[0].Value.ToString() == scrollToIndex).Select(x => x.Index).First();
                dg.FirstDisplayedScrollingRowIndex = newEntry;
                dg.Rows[newEntry].Selected = true;
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DBASE.CloseSession();
        }

        //
        // COUNT ROWS
        //
        private void dg_DataSourceChanged(object sender, EventArgs e)
        {
            label2.Text = string.Format("Count : {0}", dg.Rows.Count);
        }

        private void btnAddnew_Click(object sender, EventArgs e)
        {
            if (btnAddnew.Text.Equals("add new"))
            {
                btnAddnew.Text = "save";
                btnDelete.Text = "cancel";
                dg.Enabled = btnEdit.Enabled = false;
                groupRecord.Enabled = true;
                txtCustName.Focus();
                
                //bindSource.SuspendBinding();

                groupRecord.Controls.OfType<Control>().Where(x => x.GetType().Equals(typeof(TextBox))).ToList().ForEach(x =>
                {
                    x.Text = string.Empty;
                });

            }
            else
            {
                //save
                string newId = string.Empty;
                try
                {
                    using (var transaction = DBASE.Session.BeginTransaction())
                    {
                        var c = new Customers() { CustName = txtCustName.Text.Trim(), CustAddess = txtAddress.Text.Trim() };
                        DBASE.Session.Save(c);

                        transaction.Commit();

                        newId = c.CustId.ToString();
                    }

                    btnAddnew.Text = "add new";
                    btnDelete.Text = "delete selected";
                    dg.Enabled = btnEdit.Enabled = true;
                    groupRecord.Enabled = false;
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message);
                }

                PerformBinding(newId);
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dg.SelectedRows.Count == 0)
                return;

            if (btnEdit.Text.Equals("edit selected"))
            {
                btnEdit.Text = "update";
                btnDelete.Text = "cancel";
                dg.Enabled = btnAddnew.Enabled = false;
                groupRecord.Enabled = true;
                txtCustName.Focus();
            }
            else
            {
                //update
                try
                {
                    using (var transaction = DBASE.Session.BeginTransaction())
                    {
                        var c = DBASE.Session.Get<Customers>(int.Parse(dg.SelectedRows[0].Cells[0].Value.ToString()));
                        c.CustName = txtCustName.Text;
                        c.CustAddess = txtAddress.Text;

                        DBASE.Session.Update(c);

                        transaction.Commit();
                    }

                    btnEdit.Text = "edit selected";
                    btnDelete.Text = "delete selected";
                    dg.Enabled = btnAddnew.Enabled = true;
                    groupRecord.Enabled = false;
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message);
                }

                PerformBinding(dg.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (btnDelete.Text.Equals("delete selected"))
            {
                using (var transaction = DBASE.Session.BeginTransaction())
                {
                    var c = DBASE.Session.Get<Customers>(int.Parse(dg.SelectedRows[0].Cells[0].Value.ToString()));
                    DBASE.Session.Delete(c);

                    dg.Rows.Remove(dg.SelectedRows[0]);
     
                    transaction.Commit();
                }
            }
            else
            {
                btnAddnew.Text = "add new";
                btnEdit.Text = "edit selected";
                btnDelete.Text = "delete selected";
                btnAddnew.Enabled = btnEdit.Enabled = true;
                groupRecord.Enabled = false;
                dg.Enabled = true;

                if (dg.SelectedRows.Count > 0)
                {
                   //here needed to simulate dg click, to update control with new values
                }
            }
        }



        //////////////////////////////////////////////////////////


    }
}
