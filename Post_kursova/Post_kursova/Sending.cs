using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace Post_kursova
{
    public partial class Sending : Form
    {
        bd bd = new bd();
        Parcel p = new Parcel();
        Form4 f4;
        String location;
        int c=0;
        String check;
        public Sending( String c, String s)
        {
            InitializeComponent();
            city_r_comboBox1.DropDownStyle = ComboBoxStyle.Simple;
            storage_r_comboBox2.DropDownStyle = ComboBoxStyle.Simple;
            open();
            city_r_comboBox1.Enabled = false;
            storage_r_comboBox2.Enabled = false;
            weight_textBox6.Enabled = false;
            volume_textBox7.Enabled = false;
            pib_r_textBox11.Enabled = false;
            pib_s_textBox1.Enabled = false;
            tel_r_textBox10.Enabled = false;
            description_textBox5.Enabled = false;
            tel_s_textBox2.Enabled = false;
            save_button1.Visible = false;
            toolStripStatusLabel1.Text = c;
            toolStripStatusLabel2.Text = s;
            this.MouseEnter -= new EventHandler(this.Sending_MouseEnter); ;

        }
        public Sending(string o,String c, String s)
        {
            
            InitializeComponent();
            toolStripStatusLabel1.Text = c;
            toolStripStatusLabel2.Text = s;
            city_s_textBox4.Text = c;
            storage_s_textBox3.Text = s;
            bd.citys(ref city_r_comboBox1);
            location = c + " " + s;
            this.KeyPreview = true;
            save_button1.Text = o;
            if (o == "Редагувати")
                open();
        }
        void open()
        {
                id.Text = date.ValueId;
                pib_s_textBox1.Text = p.getpibsend(Convert.ToInt32(date.ValueId));
                tel_s_textBox2.Text = p.gettelsend(Convert.ToInt32(date.ValueId));
                storage_s_textBox3.Text = p.getstorsend(Convert.ToInt32(date.ValueId));
            description_textBox5.Text = p.getdesk(Convert.ToInt32(date.ValueId));
            weight_textBox6.Text = p.getweight(Convert.ToInt32(date.ValueId));
            volume_textBox7.Text = p.getvol(Convert.ToInt32(date.ValueId));
            pib_r_textBox11.Text = p.getpibr(Convert.ToInt32(date.ValueId));
            tel_r_textBox10.Text = p.gettelr(Convert.ToInt32(date.ValueId));
            city_s_textBox4.Text = p.getcitysend (Convert.ToInt32(date.ValueId));
            city_r_comboBox1.Text = p.getcityr(Convert.ToInt32(date.ValueId));
            storage_r_comboBox2.Text = p.getstorr(Convert.ToInt32(date.ValueId));
            cost_l.Text = p.getsum(Convert.ToInt32(date.ValueId));
        }
        private int cost(float w,float v)
        {
            if (w < v)
                w = v;
            if (0.5 >= w)
                c = 35;
            else if (1 >= w)
                c = 40;
            else if (2 >= w)
                c = 45;
            else if (5 >= w)
                c = 50;
            else if (10 >= w)
                c = 60;
            else
            {
                c = 60;
                int i = 20;
                do
                {
                    i = i + 10;
                    c = c + 10;
                }
                while (i<=w);
            }
            return c;
        }
        private void add()
        {
            try
            {
                if (tel_r_textBox10.Text != "(   )    -" && tel_s_textBox2.Text != "(   )    -")
                {
                    p.add(description_textBox5.Text, pib_r_textBox11.Text, pib_s_textBox1.Text, tel_r_textBox10.Text, tel_s_textBox2.Text, Convert.ToSingle(weight_textBox6.Text), city_r_comboBox1.Text, city_s_textBox4.Text, storage_r_comboBox2.Text, storage_s_textBox3.Text, location, cost(Convert.ToSingle(weight_textBox6.Text), Convert.ToSingle(volume_textBox7.Text)),volume_textBox7.Text);
                    id.Text = Convert.ToString(p.getId());
                    printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 200, 500);
                    check = p.primt_TTN(Convert.ToInt32(id.Text));
                    printPreviewDialog1.ShowDialog();
                    p.greate_info("Логін "+date.login+" дата - "+dateTimePicker1.Value,p.getId());
                }

            }
            catch (Exception e) { MessageBox.Show(e.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error); }

        }
        private void edit()
        {
            if(id.Text!="0")
                try
                {
                    if (tel_r_textBox10.Text != "(   )    -" && tel_s_textBox2.Text != "(   )    -")
                    {
                        p.edit(id.Text,description_textBox5.Text,pib_r_textBox11.Text,pib_s_textBox1.Text,tel_r_textBox10.Text,tel_s_textBox2.Text,weight_textBox6.Text, city_r_comboBox1.Text,storage_r_comboBox2.Text);
                        printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 200, 500);
                        check = p.primt_TTN(Convert.ToInt32(id.Text));
                        printPreviewDialog1.ShowDialog();
                        p.edit_info("Логін " + date.login + " дата - " + dateTimePicker1.Value, p.getId());
                    }

                }
                catch (Exception e) { MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Location = new Point(0, 0);
            date.volume = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void city_r_comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bd.storagesearch(ref storage_r_comboBox2, ref city_r_comboBox1);
        }

        private void volume_textBox7_MouseEnter(object sender, EventArgs e)
        {
            if (f4 == null || f4.Visible == false)
            {
                f4 = new Form4();
                f4.Show();
            }
        }

        private void Sending_Enter(object sender, EventArgs e)
        {
            if(save_button1.Visible==true)
            if (f4 != null || f4.Visible == false)
                f4.Close();
        }

        private void Sending_MouseEnter(object sender, EventArgs e)
        {
            if (f4 != null )
            {
                f4.Close(); 
            }
            if (pib_r_textBox11.Text == "")
                volume_textBox7.Text = "0";
            else
            volume_textBox7.Text = date.volume.ToString();
        }

        private void save_button1_Click(object sender, EventArgs e)
        {
            if(save_button1.Text=="Зберегти")
            add();
            if (save_button1.Text == "Редагувати")
                edit();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            BarcodeWriter bw = new BarcodeWriter() { Format = BarcodeFormat.CODE_128 };
            PointF ulCorner = new PointF(110, 1);
            PointF urCorner = new PointF(220, 1);
            PointF llCorner = new PointF(110, 49);
            PointF[] destPara = { ulCorner, urCorner, llCorner };
            e.Graphics.DrawImage(bw.Write(id.Text), destPara );

            e.Graphics.DrawString(check, new Font ("Arial",12, FontStyle.Regular), Brushes.Black, new Point(1,1));
        }

        private void print_button2_Click(object sender, EventArgs e)
        {
            if (id.Text != "0")
            {
                
                printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 200, 500);
                check = p.primt_TTN(Convert.ToInt32(id.Text));
                printPreviewDialog1.ShowDialog();
            }
        }

        private void Sending_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4 && e.Alt)
            {
                MessageBox.Show("Тест");
                e.Handled = true;
            }
    }
    }
}
