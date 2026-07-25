using App.Helpers;
using App.Interfaces.Services;
using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace App.Dialogs
{
    public partial class frmCheckProducts : Form
    {
        private readonly IAPIService _apiService;
        private readonly IProductService _productService;

        public frmCheckProducts(IAPIService apiService, IProductService productService)
        {
            InitializeComponent();

            this._apiService = apiService;
            this._productService = productService;
            FillGrid();
        }

        private async void FillGrid()
        {
            dg.DataSource = await _productService.GetListAsync();

            //set all readonly
            foreach (DataGridViewColumn c in dg.Columns)
                c.ReadOnly = true;

            dg.Columns[0].Visible = false; dg.Columns[2].Visible = false; dg.Columns[4].Visible = false;

            var checkCol = new DataGridViewCheckBoxColumn
            {
                Name = "colCheck",
                HeaderText = "",
                DataPropertyName = "IsDone" ,
                ReadOnly=false
            };

            dg.Columns.Insert(0, checkCol);
            
        }

        private async void btnCheck_Click(object sender, EventArgs e)
        {
          //var indices = dg.SelectedRows
          //              .Cast<DataGridViewRow>()
          //              .Select(r => r.Index)
          //              .ToList();

            var checkedRowIndexes =
    dg.Rows
      .Cast<DataGridViewRow>()
      .Where(r => !r.IsNewRow && Convert.ToBoolean(r.Cells[0].Value))
      .Select(r => r.Index)
      .ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var item in checkedRowIndexes)
	        {
               
               var posoKaneiURL = await _productService.GetProductURL(dg.Rows[item].Cells[1].Value.ToStrinX());

               if (string.IsNullOrEmpty(posoKaneiURL))
               {
                   sb.AppendLine("\r\n# NO POSOKANEI URL FOUND FOR - " + dg.Rows[item].Cells[2].Value.ToStrinX() + "\r\n");
                   continue;
               }
               else
                   sb.AppendLine("# " + dg.Rows[item].Cells[2].Value.ToStrinX() );

               var x = await _apiService.GetAsync(posoKaneiURL);
               foreach (var sm in x.retailer_prices)
	            {
                    //sb.AppendLine(sm.retailer_name);
                   sb.AppendLine((sm.is_discount ? "[disc]" : "") + sm.retailer_display_name + " >> " + sm.price.ToString());
	            }

               sb.AppendLine("");
	        }

            txtResult.Text = sb.ToString();
            
        }
    }
}
