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
    public partial class Form2 : Form
    {
        Form1 f;
        Parcel p = new Parcel();
        public Form2(Form1 f, string c, string s)
        {   
            InitializeComponent();
            this.f = f;
            toolStripStatusLabel1.Text = c;
            toolStripStatusLabel2.Text = s;
            this.KeyPreview = true;
        }
        void selAllOne()
        {
            try { 
            if (radioButton7.Checked == true)
            {
                for (int i = 0; i < checkedListBoxFrom.Items.Count; i++)
                    checkedListBoxFrom.SetItemChecked(i, true);
            }
            if (radioButton8.Checked == true)
            {
                for (int i = 0; i < checkedListBoxFrom.Items.Count; i++)
                    checkedListBoxFrom.SetItemChecked(i, false);
                }
            }
            catch (Exception e) { }
        }
        void deSelAllOne()
        {
            try { 
            if (radioButton5.Checked == true)
            {
                for (int i = 0; i < checkedListBoxFromNext.Items.Count; i++)
                    checkedListBoxFromNext.SetItemChecked(i, true);
            }
            if (radioButton6.Checked == true)
            {
                for (int i = 0; i < checkedListBoxFromNext.Items.Count; i++)
                    checkedListBoxFromNext.SetItemChecked(i, false);
                }
            }
            catch (Exception e) { }
        }
        private void checkAll()
        {
            try { 
            if (radioButton1.Checked == true)
            {
                for (int i = 0; i < checkedListBoxTo.Items.Count; i++)
                    checkedListBoxTo.SetItemChecked(i, true);
            }
            if (radioButton2.Checked == true)
            {
                for (int i = 0; i < checkedListBoxTo.Items.Count; i++)
                        checkedListBoxTo.SetItemChecked(i, false);
                }
            }
            catch (Exception e) { }
        }
        private void checkAllToIs()
        {
            try
            {
                if (radioButton3.Checked == true)
                {
                    for (int i = 0; i < checkedListBoxTo.Items.Count; i++)
                        checkedListBoxToIs.SetItemChecked(i, true);
                }
                if (radioButton4.Checked == true)
                {
                    for (int i = 0; i < checkedListBoxTo.Items.Count; i++)
                        checkedListBoxToIs.SetItemChecked(i, false);
                }
            }
            catch (Exception e) { }
        }

        void ToMoveTo()
        {
            bool f = false;
            for (int i = checkedListBoxTo.Items.Count - 1; i >= 0; i--)
            {
                if (checkedListBoxTo.GetItemChecked(i))
                {
                    checkedListBoxToIs.Items.Add(Convert.ToString(checkedListBoxTo.Items[i]));
                    p.lokedit(Convert.ToString(checkedListBoxTo.Items[i]), toolStripStatusLabel1.Text+" "+toolStripStatusLabel2.Text);
                    p.road_info(" приїзд: "+ DateTime.Now.ToString() + " до " + toolStripStatusLabel1.Text + toolStripStatusLabel2.Text, Convert.ToInt32(checkedListBoxTo.Items[i]));
                    f = true;
                    checkedListBoxTo.Items.RemoveAt(i);
                }
            }
            if (f == false)
                MessageBox.Show("Виберіть накладні!");

        }

        void ToMoveToTxt()
        {
            bool f = false;
            for (int i = checkedListBoxTo.Items.Count - 1; i >= 0; i--)
            {
                if (textBoxIdTo.Text == checkedListBoxTo.Items[i].ToString())
                {
                    checkedListBoxToIs.Items.Add(Convert.ToString(checkedListBoxTo.Items[i]));
                    p.lokedit(Convert.ToString(checkedListBoxTo.Items[i]), toolStripStatusLabel1.Text + " " + toolStripStatusLabel2.Text);
                    p.road_info(" приїзд: " + DateTime.Now.ToString() + " до " + toolStripStatusLabel1.Text + toolStripStatusLabel2.Text, Convert.ToInt32(textBoxIdTo.Text));
                    f = true;
                    checkedListBoxTo.Items.RemoveAt(i);
                    MessageBox.Show("Відправлення переміщено!");
                }
            }
            if (f == false)
                MessageBox.Show("Виберіть накладні!");

        }
        void ToMoveToCancel()
        {
            bool f = false;
            for (int i = checkedListBoxToIs.Items.Count - 1; i >= 0; i--)
            {
                if (checkedListBoxToIs.GetItemChecked(i))
                {
                    checkedListBoxTo.Items.Add(Convert.ToString(checkedListBoxToIs.Items[i]));
                    p.lokedit(Convert.ToString(checkedListBoxToIs.Items[i]), p.getcitysend(Convert.ToInt32(checkedListBoxToIs.Items[i].ToString())) +" "+p.getstorsend( Convert.ToInt32(checkedListBoxToIs.Items[i].ToString())));
                    f = true;
                    checkedListBoxToIs.Items.RemoveAt(i);
                }
            }
            if (f == false)
                MessageBox.Show("Виберіть накладні!");
        }
        void ToMoveFrom()
        {
            bool f = false;
            for (int i = checkedListBoxFrom.Items.Count - 1; i >= 0; i--)
            {
                if (checkedListBoxFrom.GetItemChecked(i))
                {
                    checkedListBoxFromNext.Items.Add(Convert.ToString(checkedListBoxFrom.Items[i]));
                    p.setStatusOne(Convert.ToInt32( checkedListBoxFrom.Items[i]), "in road");
                    p.road_info(" Виїзд: " + DateTime.Now.ToString()+" з "+toolStripStatusLabel1.Text+toolStripStatusLabel2.Text, Convert.ToInt32(checkedListBoxFrom.Items[i]));

                    f = true;
                    checkedListBoxFrom.Items.RemoveAt(i);
                }
            }
            if (f == false)
                MessageBox.Show("Виберіть накладні!");
        }

        void ToMoveFromOne()
        {
            bool f = false;
            for (int i = checkedListBoxFrom.Items.Count - 1; i >= 0; i--)
            {
                if (checkedListBoxFrom.Items[i].ToString()==textBoxIdFrom.Text)
                {
                    checkedListBoxFromNext.Items.Add(Convert.ToString(checkedListBoxFrom.Items[i]));
                    p.setStatusOne(Convert.ToInt32(checkedListBoxFrom.Items[i]), "in road");
                    p.road_info(" Виїзд: " + DateTime.Now.ToString() + " з " + toolStripStatusLabel1.Text + toolStripStatusLabel2.Text, Convert.ToInt32(textBoxIdFrom.Text));
                    f = true;
                    checkedListBoxFrom.Items.RemoveAt(i);
                }
            }
            if (f == false)
                MessageBox.Show("Виберіть накладні!");
        }

        void ToMoveFromCancel() 
        {
            bool f = false;
            for (int i = checkedListBoxFromNext.Items.Count - 1; i >= 0; i--)
            {
                if (checkedListBoxFromNext.GetItemChecked(i))
                {
                    checkedListBoxFrom.Items.Add(Convert.ToString(checkedListBoxFromNext.Items[i]));
                    p.setStatusOne(Convert.ToInt32(checkedListBoxFromNext.Items[i]),"");
                        f= true;
                    checkedListBoxFromNext.Items.RemoveAt(i);
                }
            }
            if (f == false)
                MessageBox.Show("Виберіть накладні!");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ToMoveFrom();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ToMoveFromCancel();
        }
        public void load(String c, String s)
        {
            toolStripStatusLabel1.Text = c;
            toolStripStatusLabel2.Text = s;
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Size = new Size(320, 34);
            dateTimePicker1.Location = new Point(440,480);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
        }

        private void textBoxIdFrom_MouseEnter(object sender, EventArgs e)
        {
            if (textBoxIdFrom.Text == "Введіть номер вантажу")
            { textBoxIdFrom.Text = "";
                textBoxIdFrom.ForeColor = System.Drawing.Color.Black;
            }

        }

        private void textBoxIdFrom_MouseLeave(object sender, EventArgs e)
        {
            if (textBoxIdFrom.Text == "")
            { textBoxIdFrom.Text = "Введіть номер вантажу"; textBoxIdFrom.ForeColor = System.Drawing.Color.Gray; checkedListBoxFrom.Select(); }
        }

        private void textBoxIdTo_MouseEnter(object sender, EventArgs e)
        {
            if (textBoxIdTo.Text == "Введіть номер вантажу")
            { textBoxIdTo.Text = ""; textBoxIdTo.ForeColor = Color.Black; }
        }

        private void textBoxIdTo_MouseLeave(object sender, EventArgs e)
        {
            if (textBoxIdTo.Text == "")
            { textBoxIdTo.Text = "Введіть номер вантажу"; textBoxIdTo.ForeColor = Color.Gray; checkedListBoxTo.Select(); }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            p.getDatatoStorageWhith(checkedListBoxFrom, toolStripStatusLabel1.Text + " " + toolStripStatusLabel2.Text);
            checkedListBoxFromNext.Items.Clear();
        }

        private void buttonUpdate1_Click(object sender, EventArgs e)
        {
            p.getDatatoStorage(checkedListBoxTo, toolStripStatusLabel1.Text + " " + toolStripStatusLabel2.Text);
            checkedListBoxToIs.Items.Clear();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            checkAll();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            checkAllToIs();
        }

        private void buttonUpLoad_Click(object sender, EventArgs e)
        {
            ToMoveTo();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            ToMoveToTxt();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            f.Show();
        }

        private void buttonCancelTo_Click(object sender, EventArgs e)
        {
            ToMoveToCancel();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ToMoveFromOne();
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            selAllOne();
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            selAllOne();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            deSelAllOne();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            deSelAllOne();
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && textBoxIdFrom.Focused == true)
                ToMoveFromOne();
            if (e.KeyCode == Keys.Enter && textBoxIdTo.Focused == true)
                ToMoveToTxt();
        }
    }
}
