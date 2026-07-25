using App.Helpers;
using App.Interfaces.Services;
using Domain;
using Serilog;
using System;
using System.Windows.Forms;

namespace App.Dialogs
{
    public partial class frmProduct : Form
    {
        private BindingSource bindSource;

        private readonly ILogger _logger;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly Func<frmCategory> _categoryFormFactory;
        //private readonly ISupplierService _supplierService; // dummy service for combos

        //public frmProduct(ILogger logger, IProductService productService, ISupplierService supplierService)
        public frmProduct(ILogger logger, IProductService productService, ICategoryService categoryService, Func<frmCategory> categoryFormFactory)
        {
            InitializeComponent();
           
            this._logger = logger;
            this._productService = productService;
            this._categoryService = categoryService;
            this._categoryFormFactory = categoryFormFactory;

            _logger.Information("Product Form initialized");
        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            //master! dont forget @ Program.cs MUST run :
            //General.db = new DBASEWrapper(new SQLiteConnection(@"Data Source=c:\northwind.db;Version=3"));
            //
            FillCategories();
            FillGrid();
        }

        private async void FillGrid()
        {
            dg.SuspendLayout();
            dg.DataSource = null;
            bindSource = new BindingSource();

            //var data = (await _productService.GetListAsync()).ToList();
            var productList = await _productService.GetListAsync();
            bindSource.DataSource = productList.ToSortableBindingList();

            dg.DataSource = bindSource;
            dg.ResumeLayout();

            BIND();
        }

        private async void FillCategories()  //dummy method for combos
        {
            //cmbCategoryID.Items.Clear();
            cmbCategoryID.DataSource = await _categoryService.GetComboListAsync();
            cmbCategoryID.DisplayMember = "title";
            cmbCategoryID.ValueMember = "id";
        }

        private void BIND()
        {
            txtId.DataBindings.Add(new Binding("Text", this.bindSource, "id", false));
            txtTitle.DataBindings.Add(new Binding("Text", this.bindSource, "title", false));
            txtUrl.DataBindings.Add(new Binding("Text", this.bindSource, "url", false));
            txtWhen2check.DataBindings.Add(new Binding("Text", this.bindSource, "when2check", false));
            txtDateupdated.DataBindings.Add(new Binding("Text", this.bindSource, "dateupdated", false));
            txtSmarketab.DataBindings.Add(new Binding("Text", this.bindSource, "smarketab", false));
            txtSmarketsklav.DataBindings.Add(new Binding("Text", this.bindSource, "smarketsklav", false));
            txtSmarketbazaar.DataBindings.Add(new Binding("Text", this.bindSource, "smarketbazaar", false));
            txtSmarketmymarket.DataBindings.Add(new Binding("Text", this.bindSource, "smarketmymarket", false));
            txtComment.DataBindings.Add(new Binding("Text", this.bindSource, "comment", false));
            txtHomepage.DataBindings.Add(new Binding("Text", this.bindSource, "homepage", false));
            txtNutritiontable.DataBindings.Add(new Binding("Text", this.bindSource, "nutritiontable", false));
            txtIngredients.DataBindings.Add(new Binding("Text", this.bindSource, "ingredients", false));
            cmbCategoryID.DataBindings.Add(new Binding("SelectedValue", this.bindSource, "category_id", true));

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
                if (cmbCategoryID.SelectedValue == null)
                {
                    General.Mes("Category must filled!");
                    return;
                }

                //return the newly table ID as result, stored to bindSource collection!
                txtId.Text = (await _productService.InsertReturnIdAsync((Product)bindSource.Current)).ToString();

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
                var res = await _productService.UpdateAsync((Product)bindSource.Current);
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
                Product obj = (Product)bindSource.Current;
                if (General.Mes("Delete " + obj.id + " ?", MessageBoxIcon.Information, MessageBoxButtons.YesNoCancel) == System.Windows.Forms.DialogResult.Yes)
                {
                    var res = await _productService.DeleteAsync(obj.id);

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

                var productList = await _productService.GetListAsync(txtSearch.Text.Trim());
                bindSource.DataSource = productList.ToSortableBindingList();
                
                dg.ResumeLayout();

                Cursor = System.Windows.Forms.Cursors.Default;
            }
        }

        private async void btnExport_Click(object sender, EventArgs e)
        {
            Cursor = System.Windows.Forms.Cursors.WaitCursor;

            var dt = await _productService.GetDatatableAsync();

            General.Export2Excel(dt, "Products");

            Cursor = System.Windows.Forms.Cursors.Default;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (!groupBox1.Enabled)
                this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var categoryForm = _categoryFormFactory();
            categoryForm.ShowDialog();
            FillCategories();
        }
    }
}
