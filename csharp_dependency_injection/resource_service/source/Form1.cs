using System;
using System.Globalization;
using System.Resources;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication39.Applicati0n.Interfaces;

namespace WindowsFormsApplication39
{
    public partial class Form1 : Form
    {
        private readonly IResourceService _resourceService;

        public Form1(IResourceService resourceService)
        {
            _resourceService = resourceService;

            InitializeComponent();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        //read resource by this assembly properties.resources (already setted on ServiceRegistrationExtension.cs)
        private void button1_Click(object sender, EventArgs e)
        {
            var value = _resourceService.GetString("String2", new CultureInfo("el-GR"));
            //default - var value = _resourceService.GetString("String2", CultureInfo.InvariantCulture);
            MessageBox.Show(value);

            //or else without DI
            //Properties.Resources.Culture = new CultureInfo("el-GR");
            //MessageBox.Show(Properties.Resources.String2);
        }


        /////////////////////////////////////////
        //read resource by Form - DONT USE THIS!
        private async Task<string> GetCode(string key, string culture)
        {
            return await Task.Run(() =>
            {
                ResourceManager resourceManager = new ResourceManager(typeof(Form1));
                CultureInfo cultureInfo = new CultureInfo(culture);
                return resourceManager.GetString(key, cultureInfo);
            });

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(await GetCode("goal", "el-GR"));
        }


    }
}
