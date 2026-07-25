using App.Helpers;
using App.Interfaces.Services;
using Domain;
using Serilog;
using System;
using System.Windows.Forms;

namespace App.Dialogs
{
    public partial class frmCategory : Form
    {
        private BindingSource bindSource;

        private readonly ILogger _logger;
        private readonly ICategoryService _categoryService;
        //private readonly ISupplierService _supplierService; // dummy service for combos

        //public frmCategory(ILogger logger, ICategoryService categoryService, ISupplierService supplierService)
        public frmCategory(ILogger logger, ICategoryService categoryService)
        {
            InitializeComponent();
           
            this._logger = logger;
            this._categoryService = categoryService;

            _logger.Information("Category Form initialized");
        }

        private void frmCategory_Load(object sender, EventArgs e)
        {
            //master! dont forget @ Program.cs MUST run :
            //General.db = new DBASEWrapper(new SQLiteConnection(@"Data Source=c:\northwind.db;Version=3"));
            //
            //FillSuppliers(); //dummy method for combos
            FillGrid();
        }

        private async void FillGrid()
        {
            dg.SuspendLayout();
            dg.DataSource = null;
            bindSource = new BindingSource();

            //var data = (await _categoryService.GetListAsync()).ToList();
            var categoryList = await _categoryService.GetListAsync();
            bindSource.DataSource = categoryList.ToSortableBindingList();

            dg.DataSource = bindSource;
            dg.ResumeLayout();

            BIND();
        }

        //private async void FillSuppliers()  //dummy method for combos
        //{
        //    txtSupplierID.DataSource = await _supplierService.GetComboListAsync();
        //    txtSupplierID.DisplayMember = "title";
        //    txtSupplierID.ValueMember = "id";
        //}

        private void BIND()
        {
            txtId.DataBindings.Add(new Binding("Text", this.bindSource, "id", false));
            txtTitle.DataBindings.Add(new Binding("Text", this.bindSource, "title", false));


            //for combos this must be TRUE, otherwise on lost_focus returns to dbase value! - similar -- https://developer.mescius.com/forums/winforms-edition/combobox-binding
            //txtSupplierID.DataBindings.Add(new Binding("SelectedValue", this.bindSource, "SupplierID", true));
        }

        private async void btnNew_Click(object sender, EventArgs e)
        {
            if (btnNew.Text.Equals("new"))
            {
                dg.Enabled = btnEdit.Enabled = false;
                groupBox1.Enabled = true;
                btnNew.Text = "save";
                btnDelete.Text = "cancel";

                bindSource.AddNew();

                txtId.Focus();
            }
            else
            {
                //dummy validation for combos
                //if (txtSupplierID.SelectedValue == null)
                //{
                //    General.Mes("cmdSupplierID.SelectedValue is null");
                //    return;
                //}

                //return the newly table ID as result, stored to bindSource collection!
                txtId.Text = (await _categoryService.InsertReturnIdAsync((Category)bindSource.Current)).ToString();

                // Commit any pending changes to bindingsource ( in memory )
                bindSource.EndEdit();

                ResetActionButtons();
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text.Equals("edit"))
            {
                dg.Enabled = btnNew.Enabled = false;
                groupBox1.Enabled = true;
                btnEdit.Text = "update";
                btnDelete.Text = "cancel";
            }
            else
            {
                //use the bindsource *current object* to update the dbase!
                var res = await _categoryService.UpdateAsync((Category)bindSource.Current);
                if (!res)
                {
                    General.Mes("Update is not performed!\r\n\r\nContact support!", MessageBoxIcon.Exclamation);
                    return;
                }

                // Commit any pending changes to bindingsource ( in memory )
                bindSource.EndEdit();

                ResetActionButtons();
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (btnDelete.Text.Equals("cancel"))
            {
                bindSource.CancelEdit();
                ResetActionButtons();
            }
            else
            {
                //delete logic
                Category obj = (Category)bindSource.Current;
                if (General.Mes("Delete " + obj.id + " ?", MessageBoxIcon.Information, MessageBoxButtons.YesNoCancel) == System.Windows.Forms.DialogResult.Yes)
                {
                    var res = await _categoryService.DeleteAsync(obj.id);

                    if (res)
                        bindSource.Remove(bindSource.Current);
                    else
                        General.Mes("Could not delete the record.\r\n\r\nContact support!");
                }
            }
        }

        private void ResetActionButtons()
        {
            dg.Enabled = btnEdit.Enabled = btnNew.Enabled = true;
            groupBox1.Enabled = false;
            btnNew.Text = "new";
            btnEdit.Text = "edit";
            btnDelete.Text = "delete";
        }

        private async void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (groupBox1.Enabled)
                return;

            if (e.KeyChar == 13)
            {
                Cursor = System.Windows.Forms.Cursors.WaitCursor;

                e.Handled = true;

                dg.SuspendLayout();

                var categoryList = await _categoryService.GetListAsync(txtSearch.Text.Trim());
                bindSource.DataSource = categoryList.ToSortableBindingList();
                
                dg.ResumeLayout();

                Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        private async void btnExport_Click(object sender, EventArgs e)
        {
            Cursor = System.Windows.Forms.Cursors.WaitCursor;

            var dt = await _categoryService.GetDatatableAsync();

            General.Export2Excel(dt, "Categorys");

            Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (!groupBox1.Enabled)
                this.Close();
        }
    }
}
