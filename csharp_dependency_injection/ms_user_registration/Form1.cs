using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication37.Applicati0n.Domain;
using WindowsFormsApplication37.Applicati0n.Interfaces;
using WindowsFormsApplication37.Applicati0n.Services;

namespace WindowsFormsApplication37
{
    public partial class Form1 : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IUserRepository _db;
        private readonly IServiceA _serviceA;
        private readonly IServiceB _serviceB;
        private readonly LoggerServiceResolver _myService;

        public Form1(IServiceProvider serviceProvider, IUserRepository userRepository, IServiceA serviceA, IServiceB serviceB, IServiceC serviceC, LoggerServiceResolver myService)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _db = userRepository;
            _serviceA = serviceA;
            _serviceB = serviceB;
            _myService = myService;
        }
        
        private async void button1_Click(object sender, EventArgs e)
        {
            //_db.AddUser("sdf");
            textBox1.Text = textBox1.Text.Trim();
            textBox2.Text = textBox2.Text.Trim();

            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
                return;

            //var users = await _db.GetAllUsers();

            //var user = users.Where(c => c.nam3.Equals(textBox1.Text.Trim(), StringComparison.InvariantCultureIgnoreCase));

            //if (user.Count() == 0)
            //{
            //    MessageBox.Show("Catastrofic error!!\n\nuser doesnt exist", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //--
            var user = await ReadUsers(textBox1.Text);
            if (user.Count()==0)
            {
                MessageBox.Show("Catastrofic error!!\n\nuser doesnt exist", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //dummy
            if (await PreValidation(user, textBox1.Text).ConfigureAwait(true))
            {
                //real validation 
                var h = await Validation(textBox1.Text, user).ConfigureAwait(true);

                if (h.Length.Equals(textBox2.Text.Length))
                {
                    var u = Compare(h, textBox2.Text);

                    if (!u.Equals("error"))
                    {
                        this.BackgroundImage = WindowsFormsApplication37.Properties.Resources.doh;
                        this.Controls.OfType<Control>().ToList().ForEach(c => c.Visible = false);

                        try
                        {
                            using (SoundPlayer player = new SoundPlayer(Properties.Resources.sound))
                            {
                                player.Play(); // Play the sound asynchronously
                            }
                        }
                        catch { }
                    }
                    else
                        MessageBox.Show(Compare(h, textBox2.Text));
                
                    //MessageBox.Show(Compare(h, textBox2.Text));
                }

            }
        }
        
        private async Task<IEnumerable<User>> ReadUsers(string h)
        {
            var users = await _db.GetAllUsers();

            return users.Where(c => c.nam3==h);
            //return users.Where(c => c.nam3.Equals(h, StringComparison.InvariantCultureIgnoreCase));
        }
        
        private string Compare(string h, string l)
        {
            var differences = h
               .Select((c, index) => new { Char1 = c, Char2 = l.ElementAtOrDefault(index), Index = index })
               .Where(x => x.Char2 != x.Char1)
               .ToList();

            if (!differences.Any())
                return "-------------------\n>> success <<\n-------------------";
            else
                return "error";
        }
        
        private async Task<string> Validation(string jj, IEnumerable<User> user)
        {
            Task<DaLock>[] tasks = new Task<DaLock>[]
            {
                CreateAndValidate("metabolic endotoxemia", jj),
                CreateAndValidate("oxidative stress", jj),
                CreateAndValidate("desire is the root cause of all evils", jj)
            };

            DaLock[] results = await Task.WhenAll(tasks);
            //Console.WriteLine(results[0]);
            //Console.WriteLine(results[1]);
            //Console.WriteLine(results[2]);
           return await PreCompareResults(results, user);
        }
        
        private async Task<bool> PreValidation(IEnumerable<User> user, string jj)
        {
            var _ = _serviceProvider.GetRequiredService<IServiceD>();

            var g = await _serviceB.Validate(jj, await _serviceA.Validate(user.First(), jj));
            if (!g)
            {
                MessageBox.Show("validation error", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                


                
            }

            return true;
        }
        
        private async Task<string> PreCompareResults(DaLock[] res, IEnumerable<User> user)
        {
            for (int i = 0; i < res.Length ; i++)
            {
                //var kk = await _db.GetCodeB(i.ToString(), user.First().id);
                //Console.WriteLine(kk);
                if (!(res[i].usercode1 - 999).ToString().Equals(await _db.GetCodeB(i, user.First().id)))
                    return string.Empty;
            }

            return CompareResults(res);
        }
        
        private string CompareResults(DaLock[] res)
        {
            uint x = 0;
            foreach (DaLock item in res)
            {
                //Console.WriteLine(item.usercode1);
                //Console.WriteLine(item.usercode2);
                //Console.WriteLine("--");

                x = item.usercode1 / 3;
                x -= item.usercode2 / 2;
            }

            //Console.WriteLine(x.ToString());

            return x.ToString();
        }

        private async Task<DaLock> CreateAndValidate(string loggerType, string jj)
        {
            var service = _myService(loggerType);
            service.username = jj;
            return await service.Validate();
        }
    }
}
