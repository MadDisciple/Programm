using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.PassField.AutoSize = false;
            this.PassField.Size = new Size(this.PassField.Size.Width, 64);

            this.UsersurnameField.AutoSize = false;
            this.UsersurnameField.Size = new Size(this.UsersurnameField.Size.Width, 64);

            UsernameField.Text = "Введите Логин";
            UsernameField.ForeColor = Color.Gray;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        Point lastPoint;
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void PassField_TextChanged(object sender, EventArgs e)
        {

        }

        private void UsernameField_Enter(object sender, EventArgs e)
        {

            if (UsernameField.Text == "Введите логин")
                UsernameField.Text = "";

        }

        private void UsernameField_Leave(object sender, EventArgs e)
        {
            if (UsernameField.Text == "")
            {
                UsernameField.Text = "Введите логин";
                UsernameField.ForeColor = Color.Gray;
            }
        }

        private void LoginField_Enter(object sender, EventArgs e)
        {
            if (LoginField.Text == "Введите имя")
                LoginField.Text = "";
        }

        private void LoginField_Leave(object sender, EventArgs e)
        {
            if (LoginField.Text == "")
            {
                LoginField.Text = "Введите имя";
                LoginField.ForeColor = Color.Gray;

            }
        }

        private void ButtonRegister_Click(object sender, EventArgs e)
        {
            if (UsernameField.Text == "Введите логин")
            {
                MessageBox.Show("Введите логин");
                return;
            }

            if (UsersurnameField.Text == "")
            {
                MessageBox.Show("Введите фамилию");
                return;
            }

            if (PassField.Text == "")
            {
                MessageBox.Show("Введите пароль");
                return;
            }

            if (LoginField.Text == "Введите имя")
            {
                MessageBox.Show("Введите имя");
                return;
            }

            if (isUserExists())
                return;


            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `users` (`name`, `surname`, `login`, `pass`) VALUES (@login, @surname, @name, @pass)", db.getConnection());
            command.Parameters.Add("@login", MySqlDbType.VarChar).Value = LoginField.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = PassField.Text;
            command.Parameters.Add("@name", MySqlDbType.VarChar).Value = UsernameField.Text;
            command.Parameters.Add("@surname", MySqlDbType.VarChar).Value = UsersurnameField.Text;

            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
                MessageBox.Show("Аккаунт был создан");
            else MessageBox.Show("Аккаунт не был создан");




            db.closeConnection();

        }



        public Boolean isUserExists()
        {
            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL", db.getConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = LoginField.Text;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("Такой логин уже есть, введите другой");
                return true;
            }

            else
                return false;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        private void UsernameField_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


