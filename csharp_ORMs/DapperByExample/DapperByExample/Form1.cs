using DBManager.DBASES;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dapper;
using DapperByExample.Services;
using DapperByExample.Models;

namespace DapperByExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            string connection_string = string.Format("Data Source={0};Version=3;New=False;", Application.StartupPath + "\\..\\..\\..\\dbase.db");
            General.db = new SQLiteClass(connection_string);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //https://dapper-tutorial.net/dapper-insert-and-update#insert
            List<CustomersTable> customers = new List<CustomersTable>();
            // using (IDbConnection db = new SqlConnection("asdf"))
            {

                //var db = (IDbConnection)General.db.GetConnection();
                customers = General.db.GetConnection().Query<CustomersTable>("select * From Customers").ToList();

            }

            dg.DataSource = customers;


        }


        private void button2_Click(object sender, EventArgs e)
        {

            //https://dapper-tutorial.net/dapper-insert-and-update
            CustomersTable x = new CustomersTable();
            x.Cust_name = "asfd";
            x.Cust_addess = "adsfd";

            string sqlQuery = @"INSERT INTO [customers] (
[cust_name],
[cust_addess]) VALUES (
@cust_name,
@cust_addess)";


            int rowsAffected = General.db.GetConnection().Execute(sqlQuery, x) ;
            Console.WriteLine(rowsAffected);
            Console.WriteLine(x.Cust_id);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dg.SelectedRows.Count == 0)
                return;

            CustomersTable x = General.db.GetConnection().QuerySingle<CustomersTable>("Select * From Customers where cust_id=@cust_id", new { Cust_id = long.Parse(dg.SelectedRows[0].Cells[0].Value.ToString()) });

            x.Cust_name += "yupi";

            string sqlQuery = @"UPDATE [customers] 
SET 
[cust_name] = @cust_name,
[cust_addess] = @cust_addess where cust_id=@cust_id";


            int rowsAffected = General.db.GetConnection().Execute(sqlQuery, x);
            Console.WriteLine(rowsAffected);


        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dg.SelectedRows.Count == 0)
                return;

            string sqlQuery = @"delete from [customers] where cust_id=@cust_id";


            int rowsAffected = General.db.GetConnection().Execute(sqlQuery, new CustomersTable() { Cust_id = long.Parse(dg.SelectedRows[0].Cells[0].Value.ToString()) });
            Console.WriteLine(rowsAffected);

            //dg.Rows.RemoveAt(dg.SelectedRows[0].Index);
        }

        private CustomerService _cust = new CustomerService();
        private void button8_Click(object sender, EventArgs e)
        {
            dg.DataSource = _cust.GetList();
        }

        private void button7_Click(object sender, EventArgs e)
        {

            //https://dapper-tutorial.net/dapper-insert-and-update
            Customer x = new Customer();
            x.Cust_name = "axxsfd";
            x.Cust_addess = "adxxxxsfd";

            Console.WriteLine(_cust.Insert(x));
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dg.SelectedRows.Count == 0)
                return;

            Customer x = _cust.Get(long.Parse(dg.SelectedRows[0].Cells[0].Value.ToString()));

            x.Cust_name = "bzzzzz";
            _cust.Update(x);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dg.SelectedRows.Count == 0)
                return;

            _cust.Delete(long.Parse(dg.SelectedRows[0].Cells[0].Value.ToString()));

        }

        private void button9_Click(object sender, EventArgs e)
        {
            Customer x = new Customer();
            x.Cust_name = "axxsfd";
            x.Cust_addess = "adxxxxsfd";

           Console.WriteLine( _cust.InsertReturnId(x));
        }

        // RECORD COUNT
        private void dg_DataSourceChanged(object sender, EventArgs e)
        {
            label1.Text = string.Format("Count : {0}", dg.RowCount);
        }

        //SPITON -- GROUP BY ORDER ID -- consoole out the #result# to see the chil-records
        private void button10_Click(object sender, EventArgs e)
        {
            //ref - https://www.learndapper.com/relationships

            string sqlQuery = @"select  customers.* , orders.* from customers
inner join orders on orders.cust_id = customers.cust_id"; // where customers.cust_id in (675,685,686,696,723,731,737,740,741)"; // where cust_name = 'Nakamura Mio'";

            //<Customer, Order, Customer>
            //The last parameter represents the return type, while the others are input parameters to be processed within the body 
            var t = General.db.GetConnection().Query<Customer, Order, Customer>(sqlQuery, (customer, order) =>
            {
                customer.Orders = new List<Order>();
                customer.Orders.Add(order);

                return customer;
            }, splitOn: "order_id");


            // GROUP by CUSTOMER & ORDERS
            var result = t.GroupBy(p => p.Cust_id).Select(g =>
            {
                var groupedCustomer = g.First();
                groupedCustomer.Orders = g.Select(p => p.Orders.Single()).ToList();
                return groupedCustomer;
            });

            dg.DataSource = result.ToList();
        }

        //SPITON -- on 3 tables -- consoole out the #result# to see the chil-records
        private void button11_Click(object sender, EventArgs e)
        {
            //ref - https://www.learndapper.com/relationships

            string sqlQuery = @"select  customers.* , orders.*, order_detail.* from customers
                inner join orders on orders.cust_id = customers.cust_id
                inner join order_detail on order_detail.order_id = orders.order_id";

            //here we getting raw the data *1* multiple cust_id, *2* multiple order_id, *3* multiple order_detail.order_id
            var t = General.db.GetConnection().Query<Customer, Order, OrderDetail, Customer>(sqlQuery, (customer, order, orderdetail) =>
            {
                //1st rel
                customer.Orders = new List<Order>();
                customer.Orders.Add(order);

                //2nd rel
                customer.Orders[0].OrderDetails = new List<OrderDetail>();
                customer.Orders[0].OrderDetails.Add(orderdetail);

                return customer;
            }, splitOn: "order_id, odetail_id");


            dg.DataSource = t.ToList();

            //Console.WriteLine(t);

            //todo : group by ustomer>order>orderdeail
            //..
            //..

        }


    }
}
