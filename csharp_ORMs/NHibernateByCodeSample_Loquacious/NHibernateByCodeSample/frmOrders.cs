using NHibernateByCodeSample.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NHibernateByCodeSample
{
    public partial class frmOrders : Form
    {
        public frmOrders()
        {
            InitializeComponent();
        }

        public frmOrders(int custID)
        {
            InitializeComponent();

            dg.DataSource = DBASE.Session.Query<Orders>().Where(x => x.Customers.CustId == custID).Select(y => new
            {
                OrderID = y.OrderId,
                customer = y.Customers.CustName,
                OrderNo = y.OrderNo,
                OrderDate = y.OrderDate,
                Comment = y.Comment
            }).ToList();
        }

        private void dg_SelectionChanged(object sender, EventArgs e)
        {
              if (dg.SelectedRows.Count == 0)
                return;

            int selectedOrderID = int.Parse(dg.SelectedRows[0].Cells[0].Value.ToString());
            dg2.DataSource = DBASE.Session.Query<OrderDetail>().Where(x => x.Orders.OrderId == selectedOrderID).Select(y => new
            {
                OrderDetailID = y.OdetailId ,
                ProductName =  y.Products.ProdName,
                Price = y.Products.ProdPrice,
                ProductID =  y.Products.ProductId
            }).ToList();
        }


        private void dg_DataSourceChanged(object sender, EventArgs e)
        {
            label3.Text = string.Format("Count : {0}", dg.Rows.Count);
        }

        private void dg2_DataSourceChanged(object sender, EventArgs e)
        {
            label4.Text = string.Format("Count : {0}", dg2.Rows.Count);
        }
    }
}
