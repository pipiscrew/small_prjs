using System;
using System.Windows.Forms;

namespace App.Dialogs
{
    public partial class MainForm : Form
    {
        private readonly Func<frmProduct> _productFormFactory;
        private readonly Func<frmCheckProducts> _checkProductsFormFactory;

        public MainForm(Func<frmProduct> productFormFactory, Func<frmCheckProducts> checkProductsFormFactory)
        {
            InitializeComponent();

            this._productFormFactory = productFormFactory;
            this._checkProductsFormFactory = checkProductsFormFactory;
        }

        private void btnCheckProducts_Click(object sender, EventArgs e)
        {
            var checkProductsFormFactory = _checkProductsFormFactory();
            checkProductsFormFactory.ShowDialog();            
        }

        private void btnCRUDproducts_Click(object sender, EventArgs e)
        {
            var productForm = _productFormFactory();
            productForm.ShowDialog();
            
        }
    }
}
