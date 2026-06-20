using posokanei.Entities;
using posokanei.Interfaces.Repositories;
using System;
using System.Windows.Forms;

namespace posokanei
{
    public partial class Form1 : Form
    {
        private readonly IServiceProduct serviceRepo;

        public Form1(IServiceProduct ServiceProduct)
        {
            InitializeComponent();

            this.serviceRepo = ServiceProduct;
     
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            var x = serviceRepo.InsertReturnId((Product)new Product() { title="test",url="test2",when2check="222"});
            Console.WriteLine(x.ToString());
        }

        private void btnGetList_Click(object sender, EventArgs e)
        {
            var x = serviceRepo.GetList();
            Console.WriteLine(x.Count);
        }
    }
}
