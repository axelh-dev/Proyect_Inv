using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace UI.WinForm
{
    public partial class Acceso : Form
    {
        #region -> Definición de Campos
        private string usernamePlaceholder;//Marca de agua(Placeholder) para el cuadro de texto usuario.
        private string passwordPlaceholder;//Marca de agua(Placeholder) para el cuadro de texto contraseña.
        private Color placeholderColor;//Color del marca de agua(Placeholder).
        private Color textColor;//Color para el texto del cuadro texto.

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        #endregion

        #region -> Constructor
        public Acceso()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;//Quitar borde del formulario.
            usernamePlaceholder = txtUsername.Text;//Establecer placeholder del cuadro de texto usuario.
            passwordPlaceholder = txtPassword.Text;//Establecer placeholder del cuadro de texto contraseña.
            placeholderColor = txtUsername.ForeColor;////Establecer color de placeholder.
            textColor = Color.DimGray;//Establecer color del cuadro de texto usuario y contraseña.
            lblDescription.Select();//Seleccionar un control diferente, para que los cuadros de texto no se inicien enfocados.
        }
        #endregion

        #region -> Definición de Métodos
        private void SetPlaceholder()
        {//Establecer la marca de agua (Placeholder) al cuadro de texto usuario y contraseña,
            //Siempre en cuando el valor sea nulo o tiene espacios en blanco.
            if (string.IsNullOrWhiteSpace(txtUsername.Text))//Usuario
            {
                txtUsername.Text = usernamePlaceholder;
                txtUsername.ForeColor = placeholderColor;
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Text))//Contraseña
            {
                txtPassword.Text = passwordPlaceholder;
                txtPassword.ForeColor = placeholderColor;
                txtPassword.UseSystemPasswordChar = false;//Quitar el enmascaramiento de caracteres.
            }
        }
        private void RemovePlaceHolder(TextBox textBox, string placeholderText)
        {//Quitar la marca de agua (Placeholder) de un cuadro de texto.
            if (textBox.Text == placeholderText)
            {
                textBox.Text = "";//Quitar placeholder
                textBox.ForeColor = textColor;//Establecer color normal de texto
                if (textBox == txtPassword)//Si el cuadro de texto es contraseña, enmascarar los caracteres.
                    textBox.UseSystemPasswordChar = true;

            }
        }
        private void Login()
        {
            // Simplemente abre el formulario de bienvenida sin validar usuario o contraseña
            Bienvenido welcomeForm = new Bienvenido("");
            welcomeForm.Show();

            // Oculte el formulario de inicio de sesión (LoginForm)
            this.Hide();

        }
        private void ShowMessage(string message)
        {
            lblErrorMessage.Text = "    " + message;
            lblErrorMessage.Visible = true;
        }
        #endregion

        #region -> Definición de Métodos de Evento

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();//Invocar el método Iniciar sesión.
        }
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Login();//Invocar el método Iniciar sesión, si preciona enter en el cuadro de texto contraseña.
        }

        private void LoginForm_Paint(object sender, PaintEventArgs e)
        {
            //Dibujar la linea/subrayado del cuadro de texto contraseña.
            e.Graphics.DrawLine(new Pen(Color.Gray, 1), txtPassword.Location.X, txtPassword.Bottom + 5, txtPassword.Right, txtPassword.Bottom + 5);
            //Dibujar la linea/subrayado del cuadro de texto usuario.
            e.Graphics.DrawLine(new Pen(Color.Gray, 1), txtUsername.Location.X, txtUsername.Bottom + 5, txtUsername.Right, txtUsername.Bottom + 5);
            //Dibujar el borde del formulario.
            e.Graphics.DrawRectangle(new Pen(Color.Gray), 0, 0, this.Width - 1, this.Height - 1);
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //Dibujar la linea superior de la descripción.
            e.Graphics.DrawLine(new Pen(lblDescription.ForeColor, 1), lblDescription.Location.X, lblDescription.Top - 5, lblDescription.Right - 5, lblDescription.Top - 5);
        }

        private void txtUsername_Enter(object sender, EventArgs e)
        {
            //Quitar la marca de agua cuando el cursor ingrese en el cuadro de texto usuario.
            RemovePlaceHolder(txtUsername, usernamePlaceholder);
        }
        private void txtUsername_Leave(object sender, EventArgs e)
        {
            //Establecer la marca de agua cuando el cursor deje el cuadro de texto usuario.
            SetPlaceholder();
        }
        private void txtPassword_Enter(object sender, EventArgs e)
        {
            //Quitar la marca de agua cuando el cursor ingrese en el cuadro de texto contraseña.
            RemovePlaceHolder(txtPassword, passwordPlaceholder);
        }
        private void txtPassword_Leave(object sender, EventArgs e)
        {
            //Establecer la marca de agua cuando el cursor deje el cuadro de texto contraseña.
            SetPlaceholder();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();//Salir de la aplicación.
            //No use this.Close() en este caso.
        }
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;//Minimizar
        }

        private void LoginForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        #endregion

        #region -> Anulaciones

        protected override CreateParams CreateParams
        {//Anular los parámetros de creación de formularios
            get
            {
                CreateParams param = base.CreateParams;
                param.Style |= 0x20000; //Establece un cuadro de minimizar en el estilo de la ventana / Permitirá minimizar el formulario desde el icono de la barra de tareas
                return param;
            }
        }
        #endregion
    }
}
