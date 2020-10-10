using System;
using System.Windows.Forms;

namespace WindowsFormsApplication37
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            IHttpRequestProcessor reqProcessor = new RequestProcessor();
            try
            {
                new HttpServer(reqProcessor).Start();
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
                HttpServer.RegisterHttpServer();
                Application.Restart();
                return;
            }

        }







    }
}
