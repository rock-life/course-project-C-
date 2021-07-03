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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        void volume_p()
        {
            try
            {
                if (Convert.ToSingle(numericUpDown1.Text) != 0 && Convert.ToSingle(numericUpDown2.Text) != 0 && Convert.ToSingle(numericUpDown3.Text) != 0)
                {
                    float v = (Convert.ToSingle(numericUpDown1.Text) * Convert.ToSingle(numericUpDown2.Text) * Convert.ToSingle(numericUpDown3.Text)) / 4000;
                    date.volume = Convert.ToSingle(v);
                }
            }
            catch (Exception) { MessageBox.Show("incorect imput"); }
        }

        private void Form4_Leave(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            Location = new Point(MousePosition.X, MousePosition.Y);
            numericUpDown1.Text = "";
            numericUpDown2.Text = "";
            numericUpDown3.Text = "";
            date.volume = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            volume_p();
            this.Close();
        }
    }
}
