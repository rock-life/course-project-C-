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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        private void richTextBox1_Enter(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "Опис проблеми")
            {
                richTextBox1.Text = "";
                richTextBox1.ForeColor = Color.Black;
            }
            else if (richTextBox1.Text == "")
            {
                richTextBox1.Text = "Опис проблеми";
                richTextBox1.ForeColor = Color.DarkGray;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "Низький" && comboBox1.Text!= "Високий" && comboBox1.Text != "Середній")
                MessageBox.Show("Виберіть пріоритет", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (richTextBox1.Text == "" || richTextBox1.Text == "Опис проблеми")
                    MessageBox.Show("Введіть опис програми", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    if (comboBox1.Text == "Низький" || comboBox1.Text == "Високий" || comboBox1.Text == "Середній" && richTextBox1.Text != "" || richTextBox1.Text != "Опис проблеми")
                    { bd.addQuery(comboBox1.Text, richTextBox1.Text); this.Close(); }
                }
            }
        }
    }
}
