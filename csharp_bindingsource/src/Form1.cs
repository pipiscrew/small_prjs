using Models;
using Services;
using System;
using System.Windows.Forms;

/*
 * development abandoned. When on 'UPDATE' state, and user presses 'CANCEL'
 * is not restore the original values - normally must implement https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.ieditableobject
 */
namespace sampleSQLite
{
    public partial class Form1 : Form
    {
        private CustomerService customerService = new CustomerService();
        private SupplierService supplierService = new SupplierService();

        private BindingSource bindSource;

        public Form1()
        {
            InitializeComponent();   
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //SQLiteException g;

            //SQLiteClass x = new SQLiteClass("Data Source=northwind.db;Version=3", out g);

            //dg2.DataSource = x.GetDATATABLE("select * from customers");

            //*1
            FillSuppliers();

            //*2
            //dg2.DataSource = customerService.GetList();

            //*3
            //bindSource = new BindingSource();
            //bindSource.DataSource = new SortableBindingList<Customer>(customerService.GetList());  //customerService.GetList();
            //dg2.DataSource = bindSource;
            //BIND();

            FillGrid();
        }

        private void FillGrid(){
            dg2.DataSource = null;
            bindSource = new BindingSource();
            bindSource.DataSource = new SortableBindingList<Customer>(customerService.GetList());  //customerService.GetList();
            dg2.DataSource = bindSource;

            BIND();
        }

        private void BIND()
        {
            txtcustomerid.DataBindings.Add(new Binding("Text", this.bindSource, "customerid", false));
            txtcompanyname.DataBindings.Add(new Binding("Text", this.bindSource, "companyname", false));
            txtcontactname.DataBindings.Add(new Binding("Text", this.bindSource, "contactname", false));

            //for combos this must be TRUE, otherwise on lost_focus return to dbase value! - similar -- https://developer.mescius.com/forums/winforms-edition/combobox-binding
            cmdSupplierID.DataBindings.Add(new Binding("SelectedValue", this.bindSource, "SupplierID", true));
        }

        //private void UNBIND()
        //{
        //    txtcustomerid.DataBindings.Clear();
        //    txtcompanyname.DataBindings.Clear();
        //    txtcontactname.DataBindings.Clear();
        //    txtcustomerid.Text = txtcompanyname.Text = txtcontactname.Text = string.Empty;
        //    cmdSupplierID.DataBindings.Clear();
        //    cmdSupplierID.SelectedValue = 0;
        //}

        private void FillSuppliers()
        {
            cmdSupplierID.Items.Clear();
            cmdSupplierID.DataSource = supplierService.GetComboList();
            cmdSupplierID.DisplayMember = "companyname";
            cmdSupplierID.ValueMember = "supplierid";
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (btnNew.Text.Equals("new"))
            {
                dg2.Enabled = btnEdit.Enabled = false;
                groupBox1.Enabled = true;
                btnNew.Text = "save";
                btnDelete.Text = "cancel";

                // *new 
                bindSource.AddNew();
                //bindSource.SuspendBinding(); //also resets the CTLS

                txtcustomerid.Focus();
            }
            else
            {
                if (cmdSupplierID.SelectedValue==null)
                {
                    MessageBox.Show("cmdSupplierID.SelectedValue is null");
                    return;
                }

                // Commit any pending changes to bindingsource ( in memory )
                bindSource.EndEdit();

                //gray zone -- we dont know what ended to dbase fields (we hope all are as we fill on ctls)

                customerService.InsertReturnId((Customer)bindSource.Current);

                //Console.WriteLine(customerService.InsertReturnId(new Customer() { companyname = txtcompanyname.Text, contactname = txtcontactname.Text, supplierid = long.Parse(cmdSupplierID.SelectedValue.ToString()) }));

                ResetActionButtons();
            }

        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text.Equals("edit"))
            {
                dg2.Enabled = btnNew.Enabled = false;
                groupBox1.Enabled = true;
                btnEdit.Text = "update";
                btnDelete.Text = "cancel";
            }
            else
            {
                // Commit any pending changes to bindingsource ( in memory )
                bindSource.EndEdit();

                //use the bindsource *current object* to update the dbase!
                customerService.Update((Customer)bindSource.Current);

                ResetActionButtons();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (btnDelete.Text.Equals("cancel"))
            {
                //bindSource.EndEdit(); when 'NEW' - adds empty record
                //when 'UPDATE' is not restore the original values - normally must implement https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.ieditableobject
                bindSource.CancelEdit();
                ResetActionButtons();
            }
            else
            {
                //delete logic
            }
        }

        private void ResetActionButtons()
        {
            dg2.Enabled = btnEdit.Enabled = btnNew.Enabled = true;
            groupBox1.Enabled = false;
            btnNew.Text = "new";
            btnEdit.Text = "edit";
            btnDelete.Text = "delete";
        }
    }
}
