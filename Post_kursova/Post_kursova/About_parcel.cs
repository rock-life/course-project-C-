using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Post_kursova
{
    
    public partial class About_parcel : Form
    {
        int i;
        public About_parcel(int ii)
        {
           i=ii;
            InitializeComponent();
            dataGridView1.AutoSizeRowsMode= DataGridViewAutoSizeRowsMode.AllCells;
        }

        private void About_parcel_Load(object sender, EventArgs e)
        {
            
            bd bd = new bd();
            bd.conect_to_bd();
            bd.conn.Open();
            SqlDataAdapter s = new SqlDataAdapter(@"select login_gr_par as [інформація про створення],edit_info as[історія змін], login_cl_par as [закрив вантаж користувач], pay_info as [інформація про оплату],
road_info as[історія руху], id from dbo.Table_info where id =" + i, bd.conn);
            DataSet ds = new DataSet();
            s.Fill(ds);
            

            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[3].Width = 50;
            dataGridView1.RowHeadersWidth = 28;
        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
        }
    }
}
