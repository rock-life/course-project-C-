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
    public partial class FormSend : Form
    {
        int i=0;
        Form1 f;
        Sending f2;
        Parcel p = new Parcel();
        String check = "";
        public FormSend(Form1 f, String c, String s)
        {
            InitializeComponent();
            toolStripStatusLabel1.Text = c;
            toolStripStatusLabel2.Text = s;
            this.f = f;
            panel1.Hide();
            this.KeyPreview = true;
        }
        void open()
        {
            try
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    if (checkedListBox1.GetSelected(i))
                    {
                        date.ValueId =Convert.ToString( checkedListBox1.Items[i]);
                        f2 = new Sending(toolStripStatusLabel1.Text, toolStripStatusLabel2.Text);
                        f2.ShowDialog();
                    }
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }
        void clean() 
        {
            checkedListBox1.Items.Clear();
            parcel_TextBox2.Text = ""; pib_textbox.Text = "";telephone_textBox2.Text = "";oplata_textBox3.Text = ""; ;cost_label3.Text = "0"; }
        public void search()
        {
            try
            {
                
               if(p.getlok(Convert.ToInt32(parcel_TextBox2.Text)) =="") MessageBox.Show("Вантаж відсутній", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
               else if (p.getstatus(Convert.ToInt32(parcel_TextBox2.Text)) != "close")
                {
                    if (p.getpibr(Convert.ToInt32(parcel_TextBox2.Text)) != "")
                    {
                        if (toolStripStatusLabel1.Text + toolStripStatusLabel2.Text == p.getcityr(Convert.ToInt32(parcel_TextBox2.Text)) + p.getstorr(Convert.ToInt32(parcel_TextBox2.Text)) &&
                            toolStripStatusLabel1.Text + " " + toolStripStatusLabel2.Text == p.getlok(Convert.ToInt32(parcel_TextBox2.Text)))
                        {
                            op();
                        }
                        else if (toolStripStatusLabel1.Text + toolStripStatusLabel2.Text == p.getcityr(Convert.ToInt32(parcel_TextBox2.Text)) + p.getstorr(Convert.ToInt32(parcel_TextBox2.Text)) &&
                            toolStripStatusLabel1.Text + " " + toolStripStatusLabel2.Text != p.getlok(Convert.ToInt32(parcel_TextBox2.Text)))
                        {
                            if (MessageBox.Show("Вантaж у дорозі!\nВідкрити?", "Увага", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                op();
                        }
                        else
                        {
                            if (MessageBox.Show("Вантaж прямує до іншого відділення\nВідкрити?", "Увага", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                                op(); ;
                        }
                    }
                }
                else if (p.getstatus(Convert.ToInt32(parcel_TextBox2.Text)) == "close")
                {
                    if (MessageBox.Show("Вантaж видано!\nВідкрити?", "Увага", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        op();
                }
               
            }
            catch (Exception e) { MessageBox.Show( "Вантаж відсутній", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }
        void op()
        {
            bool k = false;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                if (checkedListBox1.Items[i].ToString() == parcel_TextBox2.Text)
                    k = true;
            if (k == false)
            {
                pib_textbox.Text = p.getpibr(Convert.ToInt32(parcel_TextBox2.Text));
                checkedListBox1.Items.Add(parcel_TextBox2.Text);
                telephone_textBox2.Text = p.gettelr(Convert.ToInt32(parcel_TextBox2.Text));
                cost_label3.Text = Convert.ToString(Convert.ToSingle(cost_label3.Text) + Convert.ToSingle(p.getsum(Convert.ToInt32(parcel_TextBox2.Text))));
            }
        }
        public void del_vizit()
        {
            try
            {
                checkedListBox1.Items.Remove(checkedListBox1.SelectedItem);
            }
            catch (Exception ex) { };
            try
            {
                for (int i = checkedListBox1.Items.Count - 1; i >= 0; i--)
                {
                    if (checkedListBox1.GetItemChecked(i))
                        checkedListBox1.Items.RemoveAt(i);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public void delete()
        {
            if (checkedListBox1.Items.Count > 0)
            {
                if (MessageBox.Show("Справді видалити  інформацію про вантаж?", "Увага!", MessageBoxButtons.YesNo, MessageBoxIcon.Information)== DialogResult.Yes)
                {
                    try
                    {
                        p.dellete(Convert.ToInt32(checkedListBox1.SelectedItem));
                    }
                    catch (Exception ex) { };
                    try
                    {
                        for (int i = 0; i < checkedListBox1.Items.Count; i++)
                        {
                            if (checkedListBox1.GetItemChecked(i))
                            {

                                p.dellete(Convert.ToInt32(checkedListBox1.Items[i]));
                            }
                        }
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    del_vizit();
                }
            }
        }
        void edit()
        {
            try
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    if (checkedListBox1.GetSelected(i))
                    {
                        date.ValueId = Convert.ToString(checkedListBox1.Items[i]);
                        f2 = new Sending("Редагувати",toolStripStatusLabel1.Text, toolStripStatusLabel2.Text);
                        f2.ShowDialog();
                    }
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }
        void clear()
        {
            for (int i = checkedListBox1.Items.Count - 1; i >= 0; i--)
            {
                    checkedListBox1.Items.RemoveAt(i);
            }
            telephone_textBox2.Text = "";
            pib_textbox.Text = "";
            cost_label3.Text = "0";
        }
        void end_vizyt()
        {
           
                if (cost_label3.Text == "0")
            {
                if (p.getstatus(Convert.ToInt32(parcel_TextBox2.Text)) != "close")
                {
                    p.setStatus(checkedListBox1);
                    MessageBox.Show("Вантаж успішно видано", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                clear();
            }
            else MessageBox.Show("Оплатіть будь-ласка доставку вантажів!","", MessageBoxButtons.OK,MessageBoxIcon.Information);

        }
        private void FormSend_Load(object sender, EventArgs e)
        {
            this.toolStripButton4.Size = new System.Drawing.Size(80, 35);
        }
        public void load(String c, String s)
        {
            toolStripStatusLabel1.Text = c;
            toolStripStatusLabel2.Text = s;
        }
        void pay() 
        {
            try
            {
                if (cost_label3.Text != "0")
                {
                    try
                    {
                        int i;
                        if (oplata_textBox3.Text == "")
                            MessageBox.Show("Введіть суму!");
                        else if (Convert.ToInt32(oplata_textBox3.Text) < Convert.ToInt32(cost_label3.Text))
                            MessageBox.Show("Недостатньо готівки!");
                        else  
                        {
                            int[] mas = new int[checkedListBox1.Items.Count];
                            for (i = checkedListBox1.Items.Count - 1; i >= 0; i--)
                                mas[i] = Convert.ToInt32(checkedListBox1.Items[i]);
                            check = p.print_check(mas, Convert.ToInt32(checkedListBox1.Items.Count - 1), cost_label3.Text, oplata_textBox3.Text);
                            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 200, 280+ checkedListBox1.Items.Count*30);
                            printPreviewDialog1.ShowDialog();
                            cost_label3.Text = "0";
                            p.pay_edit(mas, Convert.ToInt32(checkedListBox1.Items.Count - 1)," Логін "+date.login+" Платник - " +pib_textbox.Text);
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("Невірний формат готівки!" + ex.Message); }

                }

                else MessageBox.Show("Вантаж оплачено", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { };
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
            if (i == 0)
            { parcel_TextBox2.Focus(); i++; }
        }

        private void FormSend_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            search();
        }
             
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            clean();
            f.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (panel1.Visible == false)
                panel1.Show();
            else
                panel1.Hide();
        }

        private void Pryjom_button3_Click(object sender, EventArgs e)
        {
            if (f2 == null||f2.Visible==false)
            {
                f2 = new Sending("Зберегти",toolStripStatusLabel1.Text, toolStripStatusLabel2.Text);
                f2.Show();
            }
        }

        private void Pay_button2_Click(object sender, EventArgs e)
        {
            if (p.getstatus(Convert.ToInt32(parcel_TextBox2.Text)) != "close")
                pay();

        }

        private void open_button5_Click(object sender, EventArgs e)
        {
            open(); 
        }

        private void delette_wizitbutton8_Click(object sender, EventArgs e)
        {
            del_vizit();
            if (checkedListBox1.Items.Count==0)
                clear();
        }

        private void delete_button7_Click(object sender, EventArgs e)
        {
            delete();
            if (checkedListBox1.Items.Count == 0)
                clear();
        }

        private void edit_button6_Click(object sender, EventArgs e)
        {
            if (p.getstatus(Convert.ToInt32(parcel_TextBox2.Text)) != "close") 
                edit();
        }

        private void end_button1_Click(object sender, EventArgs e)
        {
            end_vizyt();
        }

        private void FormSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && e.Control)
                Pryjom_button3_Click(null,null);
            if (e.KeyCode == Keys.R && e.Control)
            {
                About_parcel ab = new About_parcel(Convert.ToInt32(checkedListBox1.SelectedItem));
                ab.ShowDialog();
            }
            if (e.KeyCode == Keys.Enter )
            {
                if(parcel_TextBox2.Focused==true)
                search();
            }
            if (e.KeyCode==Keys.O&&e.Control)
            {
                open();
            }
            if (e.KeyCode == Keys.D && e.Control)
                delete_button7_Click(null,null);
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(check, new Font("Arial", 12, FontStyle.Regular), Brushes.Black, new Point(1, 1));
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }

        private void parcel_TextBox2_Enter(object sender, EventArgs e)
        {
            if (parcel_TextBox2.Text == "Номер вантажу")
            { parcel_TextBox2.Text = "";
                parcel_TextBox2.ForeColor = Color.Black;
            }
        }

        private void parcel_TextBox2_Leave(object sender, EventArgs e)
        {
            if (parcel_TextBox2.Text == "")
            {
                parcel_TextBox2.Text = "Номер вантажу";
                parcel_TextBox2.ForeColor = Color.Gray;
            }
        }
    }
}
