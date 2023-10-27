using SimplexMethod;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace UI.WinForm
{
    public partial class Bienvenido : Form
    {
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();//Salir de la aplicación.
            //No use this.Close() en este caso.
        }
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;//Minimizar
        }

        public Bienvenido(string username)
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        private void WelcomeForm_Load(object sender, EventArgs e)
        {
            this.Opacity = 0.0;
            circularProgressBar1.Value = 0;
            circularProgressBar1.Minimum = 0;
            circularProgressBar1.Maximum = 100;

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1) this.Opacity += 0.05;
            circularProgressBar1.Value += 5;
            circularProgressBar1.Text = circularProgressBar1.Value.ToString();
            if (circularProgressBar1.Value == 100)
            {
                timer1.Stop();
                timer2.Start();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.Opacity -= 0.1;
            if (this.Opacity == 0)
            {
                timer2.Stop();
                this.Close();

                MainScreen mainScreen = new MainScreen();
                mainScreen.Show();

            }
        }

        private void WelcomeForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Gray), 0, 0, this.Width - 1, this.Height - 1);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void btnMinimize_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;//Minimizar
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            Application.Exit();//Salir de la aplicación.
            //No use this.Close() en este caso.
        }
    }
}
