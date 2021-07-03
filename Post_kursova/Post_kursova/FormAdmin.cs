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
    public partial class FormAdmin : Form
    {
        bd b = new bd();
        Form1 f;
        admin a = new admin();
        public FormAdmin(Form1 fa)
        {
            InitializeComponent();
            f = fa;
            panel1.Hide();
            panel2.Hide();
            dataGridView5.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView5.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            b.citys(ref City);
            b.citys(ref comboBox1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (login_adm.Text != "" && new_job.Text != "" && new_password.Text != "")
                a.edit_user(login_adm.Text, new_job.Text, new_password.Text);
            else
                MessageBox.Show("Заповніть усі данні");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            f.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button2_Click(null,null);
        }

        private void add_Click(object sender, EventArgs e)
        {
            if (City.Text != "" && Storage.Text != "")
            {
                City.Items.Clear();
                a.add_storage(City.Text, Storage.Text);
                b.citys(ref City);
            }
            else
                MessageBox.Show("Заповніть усі данні");

        }

        private void dellete_Click(object sender, EventArgs e)
        {

            if (City.Text != "" && Storage.Text != "")
            { City.Items.Clear(); a.dellette_storage(City.Text, Storage.Text); b.citys(ref City); b.citys(ref City); }
            else
                MessageBox.Show("Заповніть усі данні");
        }

        private void edit_Click(object sender, EventArgs e)
        {
            if (panel1.Visible == false)
                panel1.Show();
            else panel1.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (City.Text != "" && Storage.Text != "" && city_new.Text != "" && Storage_new.Text != "")
            {
                City.Items.Clear();
                a.edit_storage(City.Text, Storage.Text, city_new.Text, Storage_new.Text);
                b.citys(ref City);
            }
            else
                MessageBox.Show("Заповніть усі данні");
            panel1.Hide();
        }

        private void add_k_Click(object sender, EventArgs e)
        {
            if (login_adm.Text != "" && password_adm.Text != ""&& job_adm.Text!="")
                a.adduser(login_adm.Text, password_adm.Text, job_adm.Text);
            else
                MessageBox.Show("Заповніть усі данні");

        }

        private void delete_k_Click(object sender, EventArgs e)
        {
            if (login_adm.Text != "" && password_adm.Text != "" && job_adm.Text != "")
                a.dellette_user(login_adm.Text, password_adm.Text, job_adm.Text);
            else
                MessageBox.Show("Заповніть усі данні");
        }

        private void edit_k_Click(object sender, EventArgs e)
        {
            if (panel2.Visible == false)
                panel2.Show();
            else panel2.Hide();
        }

        private void City_SelectedIndexChanged(object sender, EventArgs e)
        {
            b.storagesearch(ref Storage, ref City);
        }

        private void FormAdmin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void buttonZaputOnowVykon_Click(object sender, EventArgs e)
        {
            toolStripProgressBar1.Value = 0;
            bd.UpdateQueryUnacted(comboBoxSortZapytVykon.Text, dataGridViewZapytyVykonaty, ref toolStripProgressBar1);
            toolStripProgressBar1.Value = 100;
        }

        private void buttonZapytVykon_Click(object sender, EventArgs e)
        {
            try
            {
                toolStripProgressBar1.Value = 0;
                bd.execute(ref dataGridViewZapytyVykonaty);
                bd.UpdateQueryUnacted(comboBoxSortZapytVykon.Text, dataGridViewZapytyVykonaty, ref toolStripProgressBar1);
                toolStripProgressBar1.Value = 100;
                MessageBox.Show("Успішно виконано)");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }


        }

        private void buttonZaputOnowSkasuw_Click(object sender, EventArgs e)
        {
            toolStripProgressBar1.Value = 0;
            bd.UpdateQueryActed(comboBoxSortZapytSkasuw.Text, dataGridViewZapytyDoVykonanna, ref toolStripProgressBar1);
            toolStripProgressBar1.Value = 100;
        }

        private void buttonZapytSkasuv_Click(object sender, EventArgs e)
        {
            try { 
            toolStripProgressBar1.Value = 0;
            bd.NonExecute(ref dataGridViewZapytyDoVykonanna);
            bd.UpdateQueryActed(comboBoxSortZapytSkasuw.Text, dataGridViewZapytyDoVykonanna, ref toolStripProgressBar1);
            toolStripProgressBar1.Value = 100;
                MessageBox.Show("Успішно виконано)");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
}

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripProgressBar1.Increment(1);
        }

        private void buttonIssuance_Click(object sender, EventArgs e)
        {
            textBoxIssuanceMax.Text = textBoxIssuanceMin.Text = "";
            dataGridViewIssuanceReport.Columns.Clear();
            bd.IssuanceReport(toolStripProgressBar2,dataGridViewIssuanceReport, checkBox1, dateTimePickerIssuanseWhith, dateTimePickerIssuanceTo, textBoxIssuanceMax, textBoxIssuanceMin);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBoxIssMax.Text = textBoxIssMin.Text = "";
            bd.IssReport(toolStripProgressBar2 ,dataGridViewIss,checkBoxPeriod, dateTimePickerIssWith, dateTimePickerIssTo, textBoxIssMax, textBoxIssMin);
            
        }

        private void comboBox6city_SelectedIndexChanged(object sender, EventArgs e)
        {
            b.storagesearch(ref comboBox5storage, ref comboBox1);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            
            bd.ReportDepartment(toolStripProgressBar2,dataGridView5, checkBox2, dateTimePicker2, dateTimePicker1, comboBox1.Text, comboBox5storage.Text );
        }

    }
}
