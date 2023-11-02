using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

//src - https://github.com/qualcosa/Compact-RAM-Cleaner
namespace Compact_RAM_Cleaner
{
    public partial class Notify : FormWithShadow
    {
        public Notify(string text)
        {
            InitializeComponent();
            Init();

            Label1.Text = text;
            Label2.Visible = false;

            Label1.TextAlign = ContentAlignment.MiddleLeft;
            Label1.AutoSize = false;
            Label1.Size = new Size(234, 40);
            Label1.Location = new Point(54, 6);
        }

        public Notify(double memoryReleased)
        {
            InitializeComponent();
            Init();

            Label1.ForeColor = SystemColors.ControlDark;
            Label1.Text = "MemoryReleased";

            Label2.Visible = true;
            //Label2.Text = BytesToString(memoryReleased);
            Label2.Location = new Point(Label1.Location.X + Label1.Width - 4, Label2.Location.Y);
        }

        void Init()
        {
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - Width - 10, Screen.PrimaryScreen.WorkingArea.Height - Height - 10);
            PictureBox1.Paint += (s, e) => e.Graphics.DrawIcon(Icon, (PictureBox1.Width - Icon.Width) / 2, (PictureBox1.Height - Icon.Height) / 2);
            Paint += (s, e) =>
            {
                using (var pen = new Pen(BackColor, 2))
                    e.Graphics.DrawLine(pen, 0, Height, Width, Height);
            };
            Load += async (s, e) =>
            {
                await StartAnimation(this);
                await Task.Delay(4000);
                ExitAnimationAndClose(this);
            };
        }

        public static async Task StartAnimation(Form form)
        {
            for (form.Opacity = 0; form.Opacity < 1; form.Opacity += 0.2)
                await Task.Delay(5);
        }

        public static async Task ExitAnimation(Form form)
        {
            for (form.Opacity = 1; form.Opacity > 0; form.Opacity -= 0.2)
                await Task.Delay(5);
        }

        public static async void ExitAnimationAndClose(Form form)
        {
            for (form.Opacity = 1; form.Opacity > 0; form.Opacity -= 0.2)
                await Task.Delay(5);
            form.Close();
        }
    }
}
