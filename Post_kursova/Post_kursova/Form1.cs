using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Post_kursova
{
    public partial class Form1 : Form
    {
        private FormAdmin formAdmin;
        private FormSend formSend;
        Form2 form2;
        bd bd = new bd();
        public Form1()
        {
            InitializeComponent();
        }

        void cleardate()
        {
            textBoxLogin.Text = "";
            textBoxPassword.Text = "";
            comboBoxCity.Text = "";
            comboBoxStorage.Text = "";
            comboBoxStorage.Items.Clear();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            user u = new user();
            String l = "", p = "", j = "";
            if (textBoxLogin.Text == "" || textBoxPassword.Text == "" || comboBoxCity.Text == "" || comboBoxStorage.Text == "")
                MessageBox.Show("Введіть усі данні!");
            else
            if (u.identyf(textBoxLogin.Text, textBoxPassword.Text, ref l, ref p, ref j))
            {
                if (j == "Адміністратор")
                {
                    if (formAdmin == null)
                    formAdmin = new FormAdmin(this);
                    cleardate();
                    formAdmin.Show();
                    this.Hide();
                }
                else if (j == "Оператор")
                {
                    if (formSend == null)
                    {
                        date.login = textBoxLogin.Text;
                        formSend = new FormSend(this, comboBoxCity.Text, comboBoxStorage.Text);
                    }
                    else
                        formSend.load(comboBoxCity.Text, comboBoxStorage.Text);
                    cleardate();
                    formSend.Show();
                    this.Hide();
                }
                else if (j == "Вантажник")
                {
                        form2 = new Form2(this, comboBoxCity.Text, comboBoxStorage.Text);
                        form2.load(comboBoxCity.Text, comboBoxStorage.Text);
                    cleardate();
                    form2.Show();
                    this.Hide();
                }
            }
            else
                MessageBox.Show("Невірний логін чи пароль!");
        }

        private void comboBoxCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            bd.storagesearch(ref comboBoxStorage, ref comboBoxCity);
        }

        private void Form1_VisibleChanged(object sender, EventArgs e)
        {
            comboBoxCity.Items.Clear();
            bd.citys(ref comboBoxCity);
        }
    }
}
