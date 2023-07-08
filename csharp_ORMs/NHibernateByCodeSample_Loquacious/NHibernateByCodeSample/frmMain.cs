using NHibernateByCodeSample.Domain;
using System;
using System.Linq;
using System.Windows.Forms;

namespace NHibernateByCodeSample
{
    public partial class frmMain : Form
    {
        public frmMain()
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
            button1.PerformClick();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DBASE.CloseSession();
        }

        //////////////////////////////////////////////////////////


        // ALL CUSTOMERS
        private void button1_Click(object sender, EventArgs e)
        {
            dg.DataSource = null;
            dg.Columns.Clear();

            dg.DataSource = DBASE.Session.Query<Customers>().ToList();
        }

        // CUSTOMERS WITH > 2 PRODUCTS
        private void button2_Click(object sender, EventArgs e)
        {
            dg.DataSource = null;
            dg.Columns.Clear();
            dg.DataSource = DBASE.Session.Query<Orders>().Where(x => x.OrderDetail.Count > 2).Select(y => y.Customers).ToList();
        }

        // CUSTOMERS WITH > 2 PRODUCTS *EXTENDED*
        private void button3_Click(object sender, EventArgs e)
        {
            dg.DataSource = null;
            dg.Columns.Clear();

            dg.DataSource = DBASE.Session.Query<Orders>().Where(x => x.OrderDetail.Count > 2)
               .OrderByDescending(x => x.OrderDetail.Count)
               .Select(y => new
               {
                   CustId = y.Customers.CustId,
                   CustName = y.Customers.CustName,
                   CustAddess = y.Customers.CustAddess,
                   OrderDetailProducts = y.OrderDetail.Count,
                   OrderId = y.OrderId
               }).ToList();
        }

        // CUSTOMERS WITH > 2 ORDERS
        private void button4_Click(object sender, EventArgs e)
        {
            dg.DataSource = null;
            dg.Columns.Clear();
            dg.DataSource = DBASE.Session.Query<Customers>().Where(x => x.Orders.Count > 2).ToList();
        }

        // DBASE DIRECT - ALL CUSTOMERS
        private void button5_Click(object sender, EventArgs e)
        {
            dg.DataSource = DBASE.db.GetDATATABLE("select * from customers");
        }


        //////////////////////////////////////////////////////////


        //
        // COUNT ROWS
        //
        private void dg_DataSourceChanged(object sender, EventArgs e)
        {
            label2.Text = string.Format("Count : {0}", dg.Rows.Count);
        }


        //
        // DATAGRIDVIEW - DOUBLE CLICK
        //
        private void dg_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dg.SelectedRows.Count == 0)
                return;

            frmOrders x = new frmOrders(int.Parse(dg.SelectedRows[0].Cells[0].Value.ToString()));
            x.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmCustomer x = new frmCustomer();
            x.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dg.SelectedRows.Count == 0)
                return;

            frmCustomer x = new frmCustomer(int.Parse(dg.SelectedRows[0].Cells[0].Value.ToString()));
            x.ShowDialog();

            
        }


    }
}
