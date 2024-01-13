using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ValidateURLList
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }
        
        private static StringBuilder b = new StringBuilder();

        private async void button1_Click(object sender, EventArgs e)
        {
        //    var urls = new[]
        //{
        //    "https://pipiscrew.com",
        //    "https://google.com",
        //    // Add more URLs as needed
        //};

            string[] urls = textBox1.Lines;

            int c = 0;
            b.Clear();
            b.AppendLine("<table border=1>");
            b.AppendLine("<tr><th>URL</th><th>Result</th></tr>");

            foreach (var url in urls)
            {
                try
                {
                    bool isValid = await ValidateUrl(url);

                    b.AppendLine("<tr><td>" + url + "</td><td>" + (isValid ? "Valid" : "Invalid") + "</td></tr>");
                }
                catch (Exception x) {
                    b.AppendLine("Failed to connect to " + url + ": " + x.Message);
                }

                c++;

                this.Text = c.ToString();
            }

            b.AppendLine("</table>");

            File.WriteAllText(Application.StartupPath + "\\result.html", b.ToString());
            MessageBox.Show("!!");
        }


        static async Task<bool> ValidateUrl(string url)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout=TimeSpan.FromSeconds(5);
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            b.AppendLine(url + " returns a 404 status code.");
                            return false;
                        }

                        return true;
                    }
                    else
                    {
                        b.AppendLine(url + " returned a non-success status code: " + response.StatusCode);
                        return false;
                    }
                }
                catch (HttpRequestException ex)
                {
                    b.AppendLine("Failed to connect to " + url + ": " + ex.Message);
                    return false;
                }
            }
        }
    }
}
