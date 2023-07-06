using NHibernateByCodeSample.Domain;
using System;
using System.Windows.Forms;

namespace NHibernateByCodeSample
{
    public partial class frmCustomer : Form
    {
        private int cust_id; 

        public frmCustomer()
        {
            InitializeComponent();

            this.Text = "New Customer";
        }

        public frmCustomer(int cust_id)
        {
            InitializeComponent();

            this.Text = "Update Customer with ID = " + cust_id;

            this.cust_id=cust_id;

            //read existing record
            var c = DBASE.Session.Get<Customers>(this.cust_id);

            txtCustName.Text = c.CustName;
            txtAddress.Text = c.CustAddess;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.cust_id == 0)
                {
                    using (var transaction = DBASE.Session.BeginTransaction())
                    {
                        var c = new Customers() { CustName = txtCustName.Text.Trim(), CustAddess = txtAddress.Text.Trim() };
                        DBASE.Session.Save(c);
                        
                        transaction.Commit();

                        Console.WriteLine("newId = " + c.CustId);
                    }
                }
                else
                {
                    using (var transaction = DBASE.Session.BeginTransaction())
                    {
                        var c = DBASE.Session.Get<Customers>(this.cust_id);
                        c.CustName = txtCustName.Text;
                        c.CustAddess = txtAddress.Text;

                        DBASE.Session.Update(c);

                        transaction.Commit();
                    }
                }

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }
    }
}
