using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace Post_kursova
{
	public abstract class Logistic
	{
		public abstract void add(String textBoxDescription, String PIB_rec, String PIB_send, String Tel_rec, String Tel_send, float Weight, String City_rec, String City_send, String Storage_rec, String Storage_send, String location, float cost, String volume);
		public abstract void edit(String id, String textBoxDescription, String PIB_rec, String PIB_send, String Tel_rec, String Tel_send, String Weight, String City_rec, String Storage_rec);
		public abstract void dellete(int id);
		public abstract String primt_TTN(int id);
	};
	public class date
	{
	public static String ValueId;
	public static String ValueTel;
	public static String ValuePib;
	public static String ValueSum;
	public static String ValueOPenWSnd;
	public static String ValueCost;
	public static float volume=0;
	public static String login="test";
		 // static int add(int a, int b) {  return a + b;};
	};
	public class bd
	{
		public SqlConnection conn;
		private SqlConnectionStringBuilder connstring;
		public static void addQuery(String status, String text)
		{
			bd b = new bd();
			b.conect_to_bd();
			try {
				SqlCommand sqlCommand = new SqlCommand("Create table Query ( id int identity(1,1), pryoritet nvarchar(max) not null, text nvarchar(max) not null, data date, status nvarchar(max) )", b.conn);
				b.conn.Open();
				sqlCommand.ExecuteNonQuery();

			}
			catch (Exception e) { };
			b.conn.Close();
			try
			{
				SqlCommand comm = new SqlCommand("insert Query (pryoritet, text, data, status) values (@status,@text, @Date, 'open')", b.conn);
				b.conn.Open();
				comm.Parameters.AddWithValue("@status", status);
				comm.Parameters.AddWithValue("@text", text);
				comm.Parameters.AddWithValue("@Date", DateTime.Now);
				comm.ExecuteNonQuery();
				b.conn.Close();
				MessageBox.Show("Запис додано)", "Виконано", MessageBoxButtons.OK);
			}
			catch (Exception e) { MessageBox.Show(e.Message.ToString(), "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error); }
		}
		public static void UpdateQueryUnacted(String Text, DataGridView data, ref ToolStripProgressBar progress)
		{
			bd b = new bd();
			progress.Value = 16;
			b.conect_to_bd();
			progress.Value = 32;
			try
			{
				switch (Text)
				{
					case "За датою створення (за зростанням)":
						{
							SqlDataAdapter s = new SqlDataAdapter(@"select id as[Номер запиту], pryoritet as[Пріоритет], text as[Опис], data as [Дата] from dbo.Query where status like 'open' order by data desc ", b.conn);
							progress.Value = 48;
							DataSet ds = new DataSet();
							progress.Value = 64;
							s.Fill(ds);
							progress.Value = 80;
							data.DataSource = ds.Tables[0];
							progress.Value = 98;
						}
						; break;
					case "За датою створення (за спаданням)":
						{
							SqlDataAdapter s = new SqlDataAdapter(@"select id as[Номер запиту], pryoritet as[Пріоритет], text as[Опис], data as [Дата] from dbo.Query where status like 'open' order by data ", b.conn);
							progress.Value = 48;
							DataSet ds = new DataSet();
							progress.Value = 64;
							s.Fill(ds);
							progress.Value = 80;
							data.DataSource = ds.Tables[0];
							progress.Value = 98;
						}
						; break;
                    case "З пріоритетом (за зростанням)":
						{
							SqlDataAdapter s = new SqlDataAdapter(@"(select id as[Номер запиту], pryoritet as[Пріоритет], text as[Опис], data as [Дата] from dbo.Query where status like 'open' and pryoritet like 'Високий' ) union all ( select id as[Номер запиту], pryoritet as[Пріоритет], text as[Опис], data as [Дата] from dbo.Query where status like 'open' and pryoritet like 'Середній' ) union all (   select id as[Номер запиту], pryoritet as[Пріоритет], text as[Опис], data as [Дата] from dbo.Query where status like 'open' and pryoritet like 'Низький')", b.conn);
							DataSet ds = new DataSet();
							s.Fill(ds);
							data.DataSource = ds.Tables[0];
                        }; break;
                    case "З пріоритетом (за спаданням)":
						{
							SqlDataAdapter s = new SqlDataAdapter(@" (select id as[Номер запиту], pryoritet as[Пріоритет], text as[Опис], data as [Дата] from dbo.Query where status like 'open' and pryoritet like 'Низький') union all ( select id as[Номер запиту], pryoritet as[Пріоритет], text as[Опис], data as [Дата] from dbo.Query where status like 'open' and pryoritet like 'Середній') union all ( select id as[Номер запиту], pryoritet as[Пріоритет], text as[Опис], data as [Дата] from dbo.Query where status like 'open' and pryoritet like 'Високий')", b.conn);
							DataSet ds = new DataSet();
							s.Fill(ds);
							data.DataSource = ds.Tables[0];
						}; break;
                    default:
						{
							SqlDataAdapter s = new SqlDataAdapter(@"select id as[Номер запиту], pryoritet as[Пріоритет], text as[Опис], data as [Дата] from dbo.Query where status like 'open' ", b.conn);
							DataSet ds = new DataSet();
							s.Fill(ds);
							data.DataSource = ds.Tables[0];
						}
						break;
				}

			}
			catch (Exception e) { MessageBox.Show(e.Message); }
		}

		public static void UpdateQueryActed(String Text, DataGridView data, ref ToolStripProgressBar progress)
		{
			bd b = new bd();
			b.conect_to_bd();
			try
			{
				switch (Text)
				{
					case "За датою створення (за зростанням)":
						{
							SqlDataAdapter s = new SqlDataAdapter(@"select id as[Номер запиту], pryoritet as[Пріоритет], text as[Опис], data as [Дата] from dbo.Query where status like 'close' order by data desc ", b.conn);
							progress.Value = 48;
							DataSet ds = new DataSet();
							progress.Value = 64;
							s.Fill(ds);
							progress.Value = 80;
							data.DataSource = ds.Tables[0];
							progress.Value = 98;
						}
						; break;
					case "За датою створення (за спаданням)":
						{
							SqlDataAdapter s = new SqlDataAdapter(@"select id as[Номер запиту], pryoritet as[Пріоритет], text as[Опис], data as [Дата] from dbo.Query where status like 'close' order by data ", b.conn);
							progress.Value = 48;
							DataSet ds = new DataSet();
							progress.Value = 64;
							s.Fill(ds);
							progress.Value = 80;
							data.DataSource = ds.Tables[0];
							progress.Value = 98;
						}
						; break;
                    case "З пріоритетом (за зростанням)":
                        {
                            SqlDataAdapter s = new SqlDataAdapter(@"(select id as[Номер запиту], pryoritet as[Пріоритет], text as[Опис], data as [Дата] from dbo.Query where status like 'close' and pryoritet like 'Високий') union all(  select id as[Номер запиту], pryoritet as[Пріоритет], text as[Опис], data as [Дата] from dbo.Query where status like 'close' and pryoritet like 'Середній') union all ( select id as[Номер запиту], pryoritet as[Пріоритет], text as[Опис], data as [Дата] from dbo.Query where status like 'close' and pryoritet like 'Низький')", b.conn);
                            DataSet ds = new DataSet();
                            s.Fill(ds);
                            data.DataSource = ds.Tables[0];
                        }; break;
                    case "З пріоритетом (за спаданням)":
                        {
                            SqlDataAdapter s = new SqlDataAdapter(@" (select id as[Номер запиту], pryoritet as[Пріоритет], text as[Опис], data as [Дата] from dbo.Query where status like 'close' and pryoritet like 'Низький') union all(  select id as[Номер запиту], pryoritet as[Пріоритет], text as[Опис], data as [Дата] from dbo.Query where status like 'close' and pryoritet like 'Середній') union all ( select id as[Номер запиту], pryoritet as[Пріоритет], text as[Опис], data as [Дата] from dbo.Query where status like 'close' and pryoritet like 'Високий')", b.conn);
                            DataSet ds = new DataSet();
                            s.Fill(ds);
                            data.DataSource = ds.Tables[0];
                        }; break;
                    default:
						{
							SqlDataAdapter s = new SqlDataAdapter(@"select id as[Номер запиту], pryoritet as[Пріоритет], text as[Опис], data as [Дата] from dbo.Query where status like 'close' ", b.conn);
							DataSet ds = new DataSet();
							s.Fill(ds);
							data.DataSource = ds.Tables[0];
						}
						break;
				}

			}
			catch (Exception e) { MessageBox.Show(e.Message); }
		}
		public static void IssuanceReport(ToolStripProgressBar bar, DataGridView data, CheckBox check, DateTimePicker dateWhith, DateTimePicker dateTo, TextBox textMax, TextBox textMin)
		{
			if (check.Checked == false)
			{
				try
				{
					bar.Value = 0;
					bd bd = new bd();
					bd.conect_to_bd();
					bd.conn.Open();
					SqlDataAdapter adapter = new SqlDataAdapter(@"select Місто=cast(city_sender as varchar(max)), Склад=cast(storage_sender as varchar(max)), count(*) as[Кількість] from Table_Parcels group by cast(city_sender as varchar(max)), cast(storage_sender as varchar(max)) ", bd.conn);
					DataSet dataSet = new DataSet();
					DataTable table = new DataTable();
					adapter.Fill(table);
					bar.Value = 25;
					adapter.Fill(dataSet);
					data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
					data.DataSource = dataSet.Tables[0];
					bd.conn.Close();
					bd.conn.Open();
					SqlCommand comm = new SqlCommand (@"select * from (" + adapter.SelectCommand.CommandText + ") as tabl where Кількість=(select max(Кількість) from (" + adapter.SelectCommand.CommandText + ") as tab ) ", bd.conn);
					SqlDataReader reader = comm.ExecuteReader();
					bar.Value = 50;
					while (reader.Read()) 
					{
						textMax.Text = reader["Місто"].ToString();
						textMax.Text =textMax.Text+" "+ reader["Склад"].ToString();
						textMax.Text = textMax.Text + ": " + reader["Кількість"].ToString()+" вантажів";
					}
					bd.conn.Close();
					bd.conn.Open();
					SqlCommand com = new SqlCommand(@"select * from (" + adapter.SelectCommand.CommandText + ") as tabl where Кількість=(select min(Кількість) from (" + adapter.SelectCommand.CommandText + ") as tab ) ", bd.conn);
					bar.Value = 75;
					SqlDataReader reade = com.ExecuteReader();
					while (reade.Read())
					{
						textMin.Text = reade["Місто"].ToString();
						textMin.Text = textMin.Text + " " + reade["Склад"].ToString();
						textMin.Text = textMin.Text + ": " + reade["Кількість"].ToString() + " вантажів";
					}
					bar.Value = 100;
					bd.conn.Close();
				}
				catch (Exception e) { MessageBox.Show(e.Message); }
			}
			else
			{
				try
				{
					bar.Value = 0;
					String dw = dateWhith.Value.Year + "-" + dateWhith.Value.Month + "-" + dateWhith.Value.Day;
					String dt = dateTo.Value.Year + "-" + dateTo.Value.Month + "-" + dateTo.Value.Day;
					bd bd = new bd();
					bd.conect_to_bd();
					bd.conn.Open();
					SqlCommand command = new SqlCommand("select");
					SqlDataAdapter adapter = new SqlDataAdapter(@"select Місто=cast(city_sender as varchar(max)),
																	Склад=cast(storage_sender as varchar(max)),
																	count(*) as[Кількість] 
																	from Table_Parcels, Table_info
																	where Table_Parcels.id=Table_info.id and Table_info.dat>= '"+dw+"' and dat<='"+dt+"' " +
																	" group by cast(city_sender as varchar(max)), cast(storage_sender as varchar(max)) ", 
																	bd.conn);
					bar.Value = 25;
					DataSet dataSet = new DataSet();
					adapter.Fill(dataSet);
					data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
					data.DataSource = dataSet.Tables[0];
					bd.conn.Close();
					SqlCommand comm = new SqlCommand(@"select * from (" + adapter.SelectCommand.CommandText + ") as tabl where Кількість=(select min(Кількість) from (" + adapter.SelectCommand.CommandText + ") as tab ) ", bd.conn);
					bd.conn.Open();
					bar.Value = 50;
					SqlDataReader reader = comm.ExecuteReader();
					while (reader.Read())
					{
						textMin.Text = reader["Місто"].ToString();
						textMin.Text = textMin.Text + " " + reader["Склад"].ToString();
						textMin.Text = textMin.Text + ": " + reader["Кількість"].ToString() + " вантажів";
					}
					bd.conn.Close();
					SqlCommand com = new SqlCommand(@"select * from (" + adapter.SelectCommand.CommandText + ") as tabl where Кількість=(select max(Кількість) from (" + adapter.SelectCommand.CommandText + ") as tab ) ", bd.conn);
					bd.conn.Open();
					bar.Value = 75;
					SqlDataReader reade = com.ExecuteReader();
					while (reade.Read())
					{
						textMax.Text = reade["Місто"].ToString();
						textMax.Text = textMax.Text + " " + reade["Склад"].ToString();
						textMax.Text = textMax.Text + ": " + reade["Кількість"].ToString() + " вантажів";
					}
					bd.conn.Close();
					bar.Value = 100;
				}
				catch (Exception e) { MessageBox.Show(e.Message); };
			}

		}
		public static void IssReport(ToolStripProgressBar bar, DataGridView data, CheckBox check, DateTimePicker dateWhith, DateTimePicker dateTo, TextBox textMax, TextBox textMin)
		{
			if (check.Checked == false)
			{
				try
				{
					bar.Value = 0;
					bd bd = new bd();
					bd.conect_to_bd();
					bd.conn.Open();
					SqlDataAdapter adapter = new SqlDataAdapter(@"select Місто=cast(city_recipient as varchar(max)), Склад=cast(storage_recipient as varchar(max)), count(*) as[Кількість] from Table_Parcels where status like 'close' group by cast(city_recipient as varchar(max)), cast(storage_recipient as varchar(max)) ", bd.conn);
					DataSet dataSet = new DataSet();
					DataTable table = new DataTable();
					bar.Value = 16;
					adapter.Fill(table);
					adapter.Fill(dataSet);
					data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
					data.DataSource = dataSet.Tables[0];
					bd.conn.Close();
					bd.conn.Open();
					bar.Value = 32;
					SqlCommand comm = new SqlCommand(@"select * from (" + adapter.SelectCommand.CommandText + ") as tabl where Кількість=(select max(Кількість) from (" + adapter.SelectCommand.CommandText + ") as tab ) ", bd.conn);
					SqlDataReader reader = comm.ExecuteReader();
					while (reader.Read())
					{
						textMax.Text = reader["Місто"].ToString();
						textMax.Text = textMax.Text + " " + reader["Склад"].ToString();
						textMax.Text = textMax.Text + ": " + reader["Кількість"].ToString() + " вантажів";
					}
					bar.Value = 68;
					bd.conn.Close();
					bd.conn.Open();
					SqlCommand com = new SqlCommand(@"select * from (" + adapter.SelectCommand.CommandText + ") as tabl where Кількість=(select min(Кількість) from (" + adapter.SelectCommand.CommandText + ") as tab ) ", bd.conn);
					SqlDataReader reade = com.ExecuteReader();
					bar.Value = 80;
					while (reade.Read())
					{
						textMin.Text = reade["Місто"].ToString();
						textMin.Text = textMin.Text + " " + reade["Склад"].ToString();
						textMin.Text = textMin.Text + ": " + reade["Кількість"].ToString() + " вантажів";
					}
					bar.Value = 99;
					bd.conn.Close();
					bar.Value = 100;
				}
				catch (Exception e) { MessageBox.Show(e.Message); }
			}
			else
			{
				try
				{
					bar.Value = 0;
					String dw = dateWhith.Value.Year + "-" + dateWhith.Value.Month + "-" + dateWhith.Value.Day;
					String dt = dateTo.Value.Year + "-" + dateTo.Value.Month + "-" + dateTo.Value.Day;
					bd bd = new bd();
					bd.conect_to_bd();
					bd.conn.Open();
					bar.Value = 16;
					SqlCommand command = new SqlCommand("select");
					SqlDataAdapter adapter = new SqlDataAdapter(@"select Місто=cast(city_recipient as varchar(max)),
																	Склад=cast(storage_recipient as varchar(max)),
																	count(*) as[Кількість] 
																	from Table_Parcels, Table_info
																	where Table_Parcels.id=Table_info.id and Table_info.dat>= '" + dw + "' and dat<='" + dt + "' " +
																	" and status like 'close' group by cast(city_recipient as varchar(max)), cast(storage_recipient as varchar(max)) ",
																	bd.conn);
					DataSet dataSet = new DataSet();
					adapter.Fill(dataSet);
					data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
					bar.Value = 30;
					data.DataSource = dataSet.Tables[0];
					bd.conn.Close();
					SqlCommand comm = new SqlCommand(@"select * from (" + adapter.SelectCommand.CommandText + ") as tabl where Кількість=(select min(Кількість) from (" + adapter.SelectCommand.CommandText + ") as tab ) ", bd.conn);
					bd.conn.Open();
					SqlDataReader reader = comm.ExecuteReader();
					bar.Value = 60;
					while (reader.Read())
					{
						textMin.Text = reader["Місто"].ToString();
						textMin.Text = textMin.Text + " " + reader["Склад"].ToString();
						textMin.Text = textMin.Text + ": " + reader["Кількість"].ToString() + " вантажів";
					}
					bd.conn.Close();
					SqlCommand com = new SqlCommand(@"select * from (" + adapter.SelectCommand.CommandText + ") as tabl where Кількість=(select max(Кількість) from (" + adapter.SelectCommand.CommandText + ") as tab ) ", bd.conn);
					bd.conn.Open();
					bar.Value = 80;
					SqlDataReader reade = com.ExecuteReader();
					while (reade.Read())
					{
						textMax.Text = reade["Місто"].ToString();
						textMax.Text = textMax.Text + " " + reade["Склад"].ToString();
						textMax.Text = textMax.Text + ": " + reade["Кількість"].ToString() + " вантажів";
					}
					bar.Value = 95;
					bd.conn.Close();
					bar.Value = 100;
				}
				catch (Exception e) { MessageBox.Show(e.Message); };
			}

		}

		public static void ReportDepartment(ToolStripProgressBar bar,DataGridView data,CheckBox check,DateTimePicker dateWhith,DateTimePicker dateTo,String city,String storage )
		{
			if (check.Checked == false)
			{
				try
				{
					bar.Value = 0;
					bd bd = new bd();
					bd.conect_to_bd();
					bd.conn.Open();
					SqlCommand comm = new SqlCommand("select count(*) as[Кількість прийнятих вантажів] from Table_Parcels where city_sender like '" + city + "' and storage_sender like '" + storage + "' ", bd.conn);
					SqlDataReader reader = comm.ExecuteReader();
					bar.Value = 25;
					while (reader.Read())
					{
						data.Rows[0].Cells[2].Value = reader["Кількість прийнятих вантажів"].ToString();
					}
					bd.conn.Close();
					bd.conn.Open();
					bar.Value = 50;
					SqlCommand com = new SqlCommand("select count(*) as[Кількість вантажів] from Table_Parcels where city_recipient like '" + city + "' and storage_recipient like '" + storage + "' and status like 'close'", bd.conn);
					SqlDataReader reade = com.ExecuteReader();
					while (reade.Read())
					{
						data.Rows[0].Cells[3].Value = reade["Кількість вантажів"].ToString();
					}
					bar.Value = 75;
					data.Rows[0].Cells[0].Value = city;
					data.Rows[0].Cells[1].Value = storage;
					bd.conn.Close();
					bar.Value = 100;
				}
				catch (Exception e) { MessageBox.Show(e.Message); }
			}
			else
			{
				try
				{
					bar.Value = 0;
					String dw = dateWhith.Value.Year + "-" + dateWhith.Value.Month + "-" + dateWhith.Value.Day;
					String dt = dateTo.Value.Year + "-" + dateTo.Value.Month + "-" + dateTo.Value.Day;
					bar.Value = 0;
					bd bd = new bd();
					bd.conect_to_bd();
					bd.conn.Open();
					bar.Value = 25;
					SqlCommand comm = new SqlCommand("select count(*) as[Кількість прийнятих вантажів] from Table_Parcels, Table_info where Table_Parcels.id=Table_info.id and Table_info.dat>= '" + dw + "' and dat<='" + dt + "' and city_sender like '" + city + "' and storage_sender like '" + storage + "' ", bd.conn);
					SqlDataReader reader = comm.ExecuteReader();
					while (reader.Read())
					{
						data.Rows[0].Cells[2].Value = reader["Кількість прийнятих вантажів"].ToString();
					}
					bd.conn.Close();
					bd.conn.Open();
					bar.Value = 50;
					SqlCommand com = new SqlCommand("select count(*) as[Кількість вантажів] from Table_Parcels, Table_info where Table_Parcels.id=Table_info.id and Table_info.dat_cl>= '" + dw + "' and dat_cl<='" + dt + "' and city_recipient like '" + city + "' and storage_recipient  like '" + storage + "' and status like 'close'", bd.conn);
					SqlDataReader reade = com.ExecuteReader();
					while (reade.Read())
					{
						data.Rows[0].Cells[3].Value = reade["Кількість вантажів"].ToString();
					}
					bar.Value = 75;
					data.Rows[0].Cells[0].Value = city;
					data.Rows[0].Cells[1].Value = storage;
					bd.conn.Close();
					bar.Value = 100;
				}
				catch (Exception e) { MessageBox.Show(e.Message); }
			}
			
		}
		public static void NonExecute(ref DataGridView data)
		{
			bd bd = new bd();
			try
			{
				bd.conect_to_bd();
				int i = Convert.ToInt32(data[0, data.CurrentRow.Index].Value.ToString());
				SqlCommand command = new SqlCommand("update Query set status='open' where id = " + i + "", bd.conn);
				bd.conn.Open();
				command.ExecuteNonQuery();
			}
			catch (Exception e) { MessageBox.Show(e.Message); };
			bd.conn.Close();
		}
		public static void execute(ref DataGridView data)
		{
			bd bd = new bd();
			try
			{
				bd.conect_to_bd();
				int i= Convert.ToInt32(data[0, data.CurrentRow.Index].Value.ToString());
				SqlCommand command = new SqlCommand("update Query set status='close' where id = "+i+"",bd.conn);
				bd.conn.Open();
				command.ExecuteNonQuery();
			}
			catch (Exception e) { MessageBox.Show(e.Message); };
			bd.conn.Close();
		}

				public void conect_to_bd()
		{
			try
			{
				connstring = new SqlConnectionStringBuilder();
				connstring.DataSource = "DESKTOP-MQ8IB7M\\SQLEXPRESS";
				connstring.InitialCatalog = "logistic";
				connstring.IntegratedSecurity = true;
				conn = new SqlConnection(Convert.ToString(connstring));
			}
			catch (Exception e) { MessageBox.Show(e.Message); }
		}
				public void citys(ref ComboBox c)
		{ try
			{
				conect_to_bd();
				String cmdTxt = "SELECT CAST(City AS NVARCHAR(100)) City FROM dbo.Table_Storage GROUP BY CAST((City) AS NVARCHAR(100))";
				SqlCommand cmd = new SqlCommand(cmdTxt, conn);
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					c.Items.Add(Convert.ToString(reader["City"]));
				}
				conn.Close();
			}
			catch (Exception e) { MessageBox.Show(e.Message); }

		}
				public void storagesearch(ref ComboBox s, ref ComboBox c)
		{
			try {
				s.Items.Clear();
				s.Text = "";
				conect_to_bd();
				String cmdTxt = "SELECT* FROM dbo.Table_Storage ";
				SqlCommand cmd = new SqlCommand(cmdTxt, conn);
				conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					if (c.Text == Convert.ToString(reader["City"]))
						s.Items.Add(Convert.ToString(reader["Storage"]));
				}
				conn.Close();
			}
			catch (Exception e) { MessageBox.Show(e.Message); }

		} }
	public class Parcel : Logistic
	{
		bd bd = new bd();
		private String id, Description, PIB_rec, PIB_send, Tel_rec, Tel_send, Weight, City_rec, City_send, Storage_rec, Storage_send, location, cost, weight;

		public String print_check(int[] mas, int value, String cost, String cash)
		{
			String check, dok = "";
			bd b = new bd();
			try
			{
				b.conect_to_bd();
				String cmdText = "SELECT* FROM dbo.Table_Parcels";
				SqlCommand cmd = new SqlCommand(cmdText, b.conn);
				b.conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				check = "Розрахунковий чек";
				while (reader.Read())
				{
					for (int i = 0; i <= value; i++)
						if (mas[i] == Convert.ToInt32(reader["id"].ToString()))
						{
							dok = dok + "\nДекларація:\n" + Convert.ToString(reader["id"].ToString()) + "\nСума :" + Convert.ToString(reader["cost"].ToString());
							break;
						}
				}
				int r = Convert.ToInt32(cash) - Convert.ToInt32(cost);
				check = check + dok + "\nЗагальна сума:" + "\n" + cost + "\nГотівкою:" + cash + "\nРешта: " + Convert.ToString(r);
			}
			finally
			{
				if (b.conn != null)
					b.conn.Close();
			}
			for (int i = 0; i <= value; i++)
			{
				try
				{
					weight = "0";
					b.conect_to_bd();
					String cmdtext = "UPDATE dbo.Table_Parcels SET cost=@cost WHERE id=@id";
					SqlCommand cmd = new SqlCommand(cmdtext, b.conn);
					cmd.Parameters.AddWithValue("@id", Convert.ToInt32(mas[i]));
					cmd.Parameters.AddWithValue("@cost", weight);
					b.conn.Open();
					cmd.ExecuteNonQuery();
				}
				finally
				{
					if (b.conn != null)
						b.conn.Close();
				}
			}
			return check;
		}
		public void lokedit(String id, String lok)
		{
			bd b = new bd();
			try
			{
				b.conect_to_bd();
				String cmdtext = "UPDATE dbo.Table_Parcels SET location=@lokation WHERE id LIKE @id";
				SqlCommand cmd = new SqlCommand(cmdtext, b.conn);
				cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id));
				cmd.Parameters.AddWithValue("@lokation", lok);
				b.conn.Open();
				cmd.ExecuteNonQuery();
			}
			finally
			{
				if (b.conn != null)
					b.conn.Close();
			}
		}
		public override void add(String textBoxDescription, String PIB_rec, String PIB_send, String Tel_rec, String Tel_send, float Weight, String City_rec, String City_send,
								String Storage_rec, String Storage_send, String location, float cost, String volume)
		{
			bd g = new bd();
			try
			{
				g.conect_to_bd();
				String cmdtex = "create table dbo.Table_Parcels( id int, pib_sender text, tel_sender text, city_sender text, storage_sender text, pib_recipient text, tel_recipient text, city_recipient text, storage_recipient text,description text, weight float, cost int, volume text, location text)";
				SqlCommand cmd = new SqlCommand(cmdtex, g.conn);
				g.conn.Open();
				cmd.ExecuteNonQuery();
			}
			finally
			{
				if (g.conn != null)
					g.conn.Close();
			}
			try
			{
				g.conect_to_bd();
				String cmdtex = "INSERT INTO dbo.Table_Parcels(pib_sender, tel_sender, city_sender, storage_sender, pib_recipient, tel_recipient, city_recipient, storage_recipient,description, weight, cost, volume, location) VALUES(@pib_sender, @tel_sender, @city_sender, @storage_sender, @pib_recipient, @tel_recipient, @city_recipient, @storage_recipient,@description, @weight, @cost,@volume, @location)";
				SqlCommand cmd = new SqlCommand(cmdtex, g.conn);
				cmd.Parameters.AddWithValue("@pib_sender", PIB_send);
				cmd.Parameters.AddWithValue("@tel_sender", Tel_send);
				cmd.Parameters.AddWithValue("@city_sender", City_send);
				cmd.Parameters.AddWithValue("@storage_sender", Storage_send);
				cmd.Parameters.AddWithValue("@pib_recipient", PIB_rec);
				cmd.Parameters.AddWithValue("@tel_recipient", Tel_rec);
				cmd.Parameters.AddWithValue("@city_recipient", City_rec);
				cmd.Parameters.AddWithValue("@storage_recipient", Storage_rec);
				cmd.Parameters.AddWithValue("@description", textBoxDescription);
				cmd.Parameters.AddWithValue("@weight", Weight);
				cmd.Parameters.AddWithValue("@volume", volume);
				cmd.Parameters.AddWithValue("@cost", cost);
				cmd.Parameters.AddWithValue("@location", location);
				g.conn.Open();
				cmd.ExecuteNonQuery();
			}
			finally
			{
				if (g.conn != null)
					g.conn.Close();
			}
		}
		public override void edit(String id, String textBoxDescription, String PIB_rec, String PIB_send, String Tel_rec, String Tel_send, String Weight, String City_rec, String Storage_rec)
		{
			bd b = new bd();
			try
			{
				b.conect_to_bd();
				String cmdtext = "UPDATE dbo.Table_Parcels SET pib_sender=@pib_sender, tel_sender=@tel_sender, pib_recipient=@pib_recipient, tel_recipient=@tel_recipient, city_recipient=@city_recipient, storage_recipient=@storage_recipient, description=@description, weight=@weight WHERE id LIKE @id";
				SqlCommand cmd = new SqlCommand(cmdtext, b.conn);
				cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id));
				cmd.Parameters.AddWithValue("@pib_sender", PIB_send);
				cmd.Parameters.AddWithValue("@tel_sender", Tel_send);
				cmd.Parameters.AddWithValue("@pib_recipient", PIB_rec);
				cmd.Parameters.AddWithValue("@tel_recipient", Tel_rec);
				cmd.Parameters.AddWithValue("@city_recipient", City_rec);
				cmd.Parameters.AddWithValue("@storage_recipient", Storage_rec);
				cmd.Parameters.AddWithValue("@description", textBoxDescription);
				cmd.Parameters.AddWithValue("@weight", Weight);
				b.conn.Open();
				cmd.ExecuteNonQuery();
			}
			finally
			{
				if (b.conn != null)
					b.conn.Close();
			}
		}
		public override void dellete(int id)
		{

			bd g = new bd();
			try
			{
				g.conect_to_bd();
				String cmdtext = " DELETE FROM dbo.Table_info  WHERE id = @id;DELETE FROM dbo.Table_Parcels  WHERE id = @id";
				SqlCommand cmd = new SqlCommand(cmdtext, g.conn);
				cmd.Parameters.AddWithValue("@id", id);
				g.conn.Open();
				cmd.ExecuteNonQuery();
				MessageBox.Show("Успішно видаленно!");
			}
			finally
			{
				if (g.conn != null)
					g.conn.Close();
			}
		}
		public override String primt_TTN(int id)
		{
			String check = "";
			bd b = new bd();
			try
			{
				b.conect_to_bd();
				String cmdText = "SELECT* FROM dbo.Table_Parcels";
				SqlCommand cmd = new SqlCommand(cmdText, b.conn);
				b.conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					if (id == Convert.ToInt32(reader["id"].ToString()))
					{
						check = "Декларація:" + "\n" + Convert.ToString(reader["id"].ToString()) + "\nВідправник:" + "\n" + Convert.ToString(reader["pib_sender"].ToString()) + "\nТелефон\n" + Convert.ToString(reader["tel_sender"].ToString()) + "\n" + "Адреса відправлення:" + "\n" + Convert.ToString(reader["city_sender"].ToString()) + "\n" + Convert.ToString(reader["storage_sender"].ToString()) + "\nОпис:" + "\n" + Convert.ToString(reader["description"].ToString()) + "\nВага: " + Convert.ToString(reader["weight"].ToString())+ "\nОбємна вага: " + Convert.ToString(reader["volume"].ToString()) + "\nОтримувач:\n" + Convert.ToString(reader["pib_recipient"].ToString()) + "\n" + Convert.ToString(reader["tel_recipient"].ToString()) + "\n" + "Адреса Отримувача:" + "\n" + Convert.ToString(reader["city_recipient"].ToString()) + "\n" + Convert.ToString(reader["storage_recipient"].ToString()) + "\n" + "Вартість:" + "\n" + Convert.ToString(reader["cost"].ToString()) + "\n" + "\nПідпис:_______________";
						break;
					}
				}

			}
			finally
			{
				if (b.conn != null)
					b.conn.Close();
			}
			return check;
		}
		public static bool operator -(Parcel j) { return true; }
		public int getId()
		{
			int id = 0;

			bd b = new bd();
			try
			{
				b.conect_to_bd();

				String cmdText1 = "SELECT MAX(id) AS id FROM dbo.Table_Parcels ";
				SqlCommand cmd1 = new SqlCommand(cmdText1, b.conn);
				b.conn.Open();
				SqlDataReader t = cmd1.ExecuteReader();
				while (t.Read())
				{
					id = Convert.ToInt32(t["id"].ToString());
				}
			}
			finally
			{
				if (b.conn != null)
					b.conn.Close();
			}

			return id;
		}
		public void greate_info(string date, int id)
		{
			try
			{
				bd.conect_to_bd();
				String cmdtext = "create table dbo.Table_info( login_gr_par text, id int,login_cl_par text, road_info text, edit_info text, pay_info text, dat date, dat_cl date )";
				SqlCommand cmd = new SqlCommand(cmdtext, bd.conn);
				bd.conn.Open();
				cmd.ExecuteNonQuery();
			}
			finally
			{
				if (bd.conn != null)
					bd.conn.Close();
			}
			try
			{
				bd.conect_to_bd();
				String cmdtext = "insert into dbo.Table_info (login_gr_par, id, dat) values(@date, @id, @d) ";
				SqlCommand cmd = new SqlCommand(cmdtext, bd.conn);
				cmd.Parameters.AddWithValue("@date", date);
				cmd.Parameters.AddWithValue("@id", id);
				cmd.Parameters.AddWithValue("@d",DateTime.Now);
				bd.conn.Open();
				cmd.ExecuteNonQuery();
			}
			finally
			{
				if (bd.conn != null)
					bd.conn.Close();
			}
		}
		public void setStatus(CheckedListBox c)
		{
			for (int i = c.Items.Count - 1; i >= 0; i--)
			{
				try
				{
					bd.conect_to_bd();
					String cmdtext = "UPDATE dbo.Table_Parcels SET status=@st WHERE id LIKE @id";
					SqlCommand cmd = new SqlCommand(cmdtext, bd.conn);
					cmd.Parameters.AddWithValue("@id", Convert.ToInt32(c.Items[i]));
					cmd.Parameters.AddWithValue("@st", "close");
					bd.conn.Open();
					cmd.ExecuteNonQuery();
					login_cl_par("Закрив(ла): "+ date.login+" - "+DateTime.Now.ToString(), Convert.ToInt32(c.Items[i]));
				}
				finally
				{
					if (bd.conn != null)
						bd.conn.Close();
				}
			}
		}
		public void setStatusOne(int id, String s)
		{
				try
				{
					bd.conect_to_bd();
					String cmdtext = "UPDATE dbo.Table_Parcels SET status=@st WHERE id LIKE @id";
					SqlCommand cmd = new SqlCommand(cmdtext, bd.conn);
					cmd.Parameters.AddWithValue("@id", id);
					cmd.Parameters.AddWithValue("@st", s);
					bd.conn.Open();
					cmd.ExecuteNonQuery();
				}
				finally
				{
					if (bd.conn != null)
						bd.conn.Close();
				}
		}
		public void edit_info(string date, int id)
		{
			try
			{
				String t = "";
				bd.conect_to_bd();
				bd.conn.Open();
				String cmdt = "Select edit_info  from dbo.Table_info where id=@id";
				SqlCommand cmd = new SqlCommand(cmdt, bd.conn);
				cmd.Parameters.AddWithValue("@id", id);
				SqlDataReader r = cmd.ExecuteReader();
				while (r.Read())
					t = " | " + r["edit_info"].ToString();
				bd.conn.Close();
				bd.conn.Open();
				String cmdt1 = "update dbo.Table_info set edit_info=@info where id=@id";
				SqlCommand qcmd = new SqlCommand(cmdt1, bd.conn);
				qcmd.Parameters.AddWithValue("@id", id);
				qcmd.Parameters.AddWithValue("@info", t + date + " | ");
				qcmd.ExecuteNonQuery();
			}
			finally
			{
				if (bd.conn != null) ;
			}
		}
		public void road_info(string date, int id)
		{
			try
			{
				String t = "";
				bd.conect_to_bd();
				bd.conn.Open();
				String cmdt = "Select road_info  from dbo.Table_info where id=@id";
				SqlCommand cmd = new SqlCommand(cmdt, bd.conn);
				cmd.Parameters.AddWithValue("@id", id);
				SqlDataReader r = cmd.ExecuteReader();
				while (r.Read())
					t = " | " + r["road_info"].ToString();
				bd.conn.Close();
				bd.conn.Open();
				String cmdt1 = "update dbo.Table_info set road_info=@info where id=@id";
				SqlCommand qcmd = new SqlCommand(cmdt1, bd.conn);
				qcmd.Parameters.AddWithValue("@id", id);
				qcmd.Parameters.AddWithValue("@info", t + date + " | ");
				qcmd.ExecuteNonQuery();
			}
			finally
			{
				if (bd.conn != null) ;
			}
		}
		private void login_cl_par(string date, int id)
		{
			try
			{
				String t = "";
				bd.conect_to_bd();
				bd.conn.Open();
				String cmdt = "Select login_cl_par  from dbo.Table_info where id=@id";
				SqlCommand cmd = new SqlCommand(cmdt, bd.conn);
				cmd.Parameters.AddWithValue("@id", id);
				SqlDataReader r = cmd.ExecuteReader();
				while (r.Read())
					t = " | " + r["login_cl_par"].ToString();
				bd.conn.Close();
				bd.conn.Open();
				String cmdt1 = "update dbo.Table_info set login_cl_par=@info, dat_cl=@d where id=@id";
				SqlCommand qcmd = new SqlCommand(cmdt1, bd.conn);
				qcmd.Parameters.AddWithValue("@id", id);
				qcmd.Parameters.AddWithValue("@d",DateTime.Now);
				qcmd.Parameters.AddWithValue("@info", t + date + " | ");
				qcmd.ExecuteNonQuery();
			}
			finally
			{
				if (bd.conn != null) ;
			}
		}
		public void pay_edit(int[] mas, int value, string date)
		{
			for (int i = 0; i <= value; i++)
			{
				try
				{
					String t = "";
					bd.conect_to_bd();
					bd.conn.Open();
					String cmdt = "Select pay_info  from dbo.Table_info where id=@id";
					SqlCommand cmd = new SqlCommand(cmdt, bd.conn);
					cmd.Parameters.AddWithValue("@id", Convert.ToInt32(mas[i]));
					SqlDataReader r = cmd.ExecuteReader();
					while (r.Read())
						t = " | " + r["pay_info"].ToString();
					bd.conn.Close();
					bd.conn.Open();
					String cmdt1 = "update dbo.Table_info set pay_info=@info where id=@id";
					SqlCommand qcmd = new SqlCommand(cmdt1, bd.conn);
					qcmd.Parameters.AddWithValue("@id", Convert.ToInt32(mas[i]));
					qcmd.Parameters.AddWithValue("@info", t + date + " | ");
					qcmd.ExecuteNonQuery();
				}
				finally
				{
					if (bd.conn != null)
						bd.conn.Close();
				};
			}
		}
		public String getstatus(int id)
		{
			String status = "";
			bd b = new bd();
			try
			{
				b.conect_to_bd();
				String cmdText = "SELECT* FROM dbo.Table_Parcels";
				SqlCommand cmd = new SqlCommand(cmdText, b.conn);
				b.conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					if (id == Convert.ToInt32(reader["id"].ToString()))
						status = reader["status"].ToString();
				}

			}
			finally
			{
				if (b.conn != null)
					b.conn.Close();
			}
			return status;
		}
		public String getvol(int id)
		{
			bd b = new bd();
			try
			{
				b.conect_to_bd();
				String cmdText = "SELECT* FROM dbo.Table_Parcels";
				SqlCommand cmd = new SqlCommand(cmdText, b.conn);
				b.conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					if (id == Convert.ToInt32(reader["id"].ToString()))
						weight = reader["volume"].ToString();
				}

			}
			finally
			{
				if (b.conn != null)
					b.conn.Close();
			}
			return weight;
		}
		public String getweight(int id)
		{
			bd b = new bd();
			try
			{
				b.conect_to_bd();
				String cmdText = "SELECT* FROM dbo.Table_Parcels";
				SqlCommand cmd = new SqlCommand(cmdText, b.conn);
				b.conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					if (id == Convert.ToInt32(reader["id"].ToString()))
						weight = reader["weight"].ToString();
				}

			}
			finally
			{
				if (b.conn != null)
					b.conn.Close();
			}
			return weight;
		}
		public String getdesk(int id)
		{
			bd b = new bd();
			try
			{
				b.conect_to_bd();
				String cmdText = "SELECT* FROM dbo.Table_Parcels";
				SqlCommand cmd = new SqlCommand(cmdText, b.conn);
				b.conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					if (id == Convert.ToInt32(reader["id"].ToString()))
						Description = reader["description"].ToString();
				}

			}
			finally
			{
				if (b.conn != null)
					b.conn.Close();
			}
			return Description;
		}
		public String getpibsend(int id)
		{
			bd b = new bd();
			try
			{
				b.conect_to_bd();
				String cmdText = "SELECT * FROM dbo.Table_Parcels";
				SqlCommand cmd = new SqlCommand(cmdText, b.conn);
				b.conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					if (id == Convert.ToInt32(reader["id"].ToString()))
						PIB_send = reader["pib_sender"].ToString();
				}

			}
			finally
			{
				if (b.conn != null)
					b.conn.Close();
			}
			return PIB_send;
		}
		public String gettelsend(int id)
		{
			bd b = new bd();
			try
			{
				b.conect_to_bd();
				String cmdText = "SELECT* FROM dbo.Table_Parcels";
				SqlCommand cmd = new SqlCommand(cmdText, b.conn);
				b.conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					if (id == Convert.ToInt32(reader["id"].ToString()))
						Tel_send = reader["tel_sender"].ToString();
				}

			}
			finally
			{
				if (b.conn != null)
					b.conn.Close();
			}
			return Tel_send;
		}
		public String getcitysend(int id)
		{
			bd b = new bd();
			try
			{
				b.conect_to_bd();
				String cmdText = "SELECT* FROM dbo.Table_Parcels";
				SqlCommand cmd = new SqlCommand(cmdText, b.conn);
				b.conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					if (id == Convert.ToInt32(reader["id"].ToString()))
						City_send = reader["city_sender"].ToString();
				}

			}
			finally
			{
				if (b.conn != null)
					b.conn.Close();
			}
			return City_send;
		}
		public String getstorsend(int id)
		{
			bd b = new bd();
			try
			{
				b.conect_to_bd();
				String cmdText = "SELECT* FROM dbo.Table_Parcels";
				SqlCommand cmd = new SqlCommand(cmdText, b.conn);
				b.conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					if (id == Convert.ToInt32(reader["id"].ToString()))
						Storage_send = reader["storage_sender"].ToString();
				}

			}
			finally
			{
				if (b.conn != null)
					b.conn.Close();
			}
			return Storage_send;
		}
		public String getpibr(int id)
		{
			bd b = new bd();
			PIB_rec = "";
			try
			{
				b.conect_to_bd();
				String cmdText = "SELECT* FROM dbo.Table_Parcels";
				SqlCommand cmd = new SqlCommand(cmdText, b.conn);
				b.conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					if (id == Convert.ToInt32(reader["id"].ToString()))
						PIB_rec = reader["pib_recipient"].ToString();
				}

			}
			finally
			{
				if (b.conn != null)
					b.conn.Close();
			}
			return PIB_rec;
		}
		public String gettelr(int id)
		{
			bd b = new bd();
			try
			{
				b.conect_to_bd();
				String cmdText = "SELECT* FROM dbo.Table_Parcels";
				SqlCommand cmd = new SqlCommand(cmdText, b.conn);
				b.conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					if (id == Convert.ToInt32(reader["id"].ToString()))
						Tel_rec = reader["tel_recipient"].ToString();
				}

			}
			finally
			{
				if (b.conn != null)
					b.conn.Close();
			}
			return Tel_rec;
		}
		public String getcityr(int id)
		{
			bd b = new bd();
			try
			{
				b.conect_to_bd();
				String cmdText = "SELECT* FROM dbo.Table_Parcels";
				SqlCommand cmd = new SqlCommand(cmdText, b.conn);
				b.conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					if (id == Convert.ToInt32(reader["id"].ToString()))
						City_rec = reader["city_recipient"].ToString();
				}

			}
			finally
			{
				if (b.conn != null)
					b.conn.Close();
			}
			return City_rec;
		}
		public String getstorr(int id)
		{
			bd b = new bd();
			try
			{
				b.conect_to_bd();
				String cmdText = "SELECT* FROM dbo.Table_Parcels";
				SqlCommand cmd = new SqlCommand(cmdText, b.conn);
				b.conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					if (id == Convert.ToInt32(reader["id"].ToString()))
						Storage_rec = reader["storage_recipient"].ToString();
				}

			}
			finally
			{
				if (b.conn != null)
					b.conn.Close();
			}
			return Storage_rec;
		}
		public String getlok(int id)
		{
			bd b = new bd();
			try
			{
				b.conect_to_bd();
				String cmdText = "SELECT* FROM dbo.Table_Parcels";
				SqlCommand cmd = new SqlCommand(cmdText, b.conn);
				b.conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					if (id == Convert.ToInt32(reader["id"].ToString()))
						location = reader["location"].ToString();
				}

			}
			finally
			{
				if (b.conn != null)
					b.conn.Close();
			}
			return location;
		}
		public String getsum(int id)
		{
			bd b = new bd();
			try
			{
				b.conect_to_bd();
				String cmdText = "SELECT* FROM dbo.Table_Parcels";
				SqlCommand cmd = new SqlCommand(cmdText, b.conn);
				b.conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();

				while (reader.Read())
				{
					if (id == Convert.ToInt32(reader["id"].ToString()))
						cost = reader["cost"].ToString();
				}

			}
			finally
			{
				if (b.conn != null)
					b.conn.Close();
			}
			return cost;
		}
		public void getDatatoStorage(CheckedListBox clb, String lok)
		{
			bool f = false;
			try
			{
				bd.conect_to_bd();
				String cmdText = "SELECT id,status, city_recipient,storage_recipient, location FROM dbo.Table_Parcels";
				SqlCommand cmd = new SqlCommand(cmdText, bd.conn);
				bd.conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					if (lok == reader["city_recipient"].ToString() + " " + reader["storage_recipient"].ToString() &&
						lok != reader["location"].ToString()&& reader["status"].ToString()=="in road")
					{
						for (int i = 0; i < clb.Items.Count; i++)
							if (clb.Items[i].ToString() == reader["id"].ToString())
								f = true;
						if (f != true)
							clb.Items.Add(reader["id"].ToString());
					}
					f = false;
				}
			}
			finally
			{
				if (bd.conn != null)
					bd.conn.Close();
			}

		}
		public void getDatatoStorageWhith(CheckedListBox clb, String lok)
		{
			bool f = false;
			try
			{
				bd.conect_to_bd();
				String cmdText = "SELECT id,city_sender,storage_sender,status, location FROM dbo.Table_Parcels";
				SqlCommand cmd = new SqlCommand(cmdText, bd.conn);
				bd.conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					if (lok == reader["city_sender"].ToString() + " " + reader["storage_sender"].ToString() &&
						lok == reader["location"].ToString()&& reader["status"].ToString() =="")
					{
						for (int i = 0; i < clb.Items.Count; i++)
							if (clb.Items[i].ToString() == reader["id"].ToString())
								f = true;
						if (f != true)
							clb.Items.Add(reader["id"].ToString());
					}
					f = false;
				}
			}
			finally
			{
				if (bd.conn != null)
					bd.conn.Close();
			}

		}
	
	};

public class user
{
		bd b = new bd();
		public user() { }
		public bool identyf(String Login, String Password, ref String myLog, ref String myPass, ref String myJob)
		{
			try
			{
				b.conect_to_bd();
				String cmdText = "SELECT* FROM dbo.Table_user";
				SqlCommand cmd = new SqlCommand(cmdText, b.conn);
				b.conn.Open();
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					String l = Convert.ToString(reader["Login"].ToString());
					String p = Convert.ToString(reader["Password"].ToString());
					if (Login == l)
						if (Password == p)
						{
							myLog = Login;
							myPass = Password;
							myJob = Convert.ToString(reader["Job"].ToString());
							return true;
						}
				}

			}
			finally
			{
				if (b.conn != null)
					b.conn.Close();
			}
			;
			return false;
		}

protected	String  login, pass,Job;
};
public class admin : user
{
		bd b = new bd();
		public admin() { }
		public void edit_user(String login, String pass, String job)
		{
			try
			{
				b.conect_to_bd();
				String  cmdtext = "UPDATE dbo.Table_user SET Password=@pass, Job=@job WHERE Login LIKE @login";
				SqlCommand  cmd = new SqlCommand(cmdtext, b.conn);
				cmd.Parameters.AddWithValue("@login", login);
				cmd.Parameters.AddWithValue("@pass", pass);
				cmd.Parameters.AddWithValue("@job", job);
				b.conn.Open();
				cmd.ExecuteNonQuery();
				MessageBox.Show("Данні успішно збережено!");
			}
			finally
			{
				if (b.conn != null)
					b.conn.Close();
			}
		}
		public void dellette_user(String login, String pass, String job)
		{
			try
			{
				b.conect_to_bd();
				String  cmdtext = "DELETE FROM dbo.Table_user  WHERE Login LIKE @login AND Password LIKE @pass AND Job LIKE @job ";
				SqlCommand  cmd = new SqlCommand(cmdtext, b.conn);
				cmd.Parameters.AddWithValue("@login", login);
				cmd.Parameters.AddWithValue("@pass", pass);
				cmd.Parameters.AddWithValue("@job", job);
				b.conn.Open();
				cmd.ExecuteNonQuery();
			}
			finally
			{
				if (b.conn != null)
					b.conn.Close();
			}
		}
		public void add_storage(String city, String storage)
		{
			try
			{
				b.conect_to_bd();
				String cmdText = "create table Table_Storage (City text not null ,Storage text not null)";
				SqlCommand cmd = new SqlCommand(cmdText, b.conn);
				b.conn.Open();
				cmd.ExecuteNonQuery();
			}
			finally
			{
				if (b.conn != null)
					b.conn.Close();
			}
			try
			{
				b.conect_to_bd();
				String  cmdtext = "INSERT INTO dbo.Table_Storage(City,Storage)VALUES(@city,@storage)";
				SqlCommand  cmd = new SqlCommand(cmdtext, b.conn);
				cmd.Parameters.AddWithValue("@city", city);
				cmd.Parameters.AddWithValue("@storage", storage);
				b.conn.Open();
				cmd.ExecuteNonQuery();
				MessageBox.Show("Данні успішно збережено!");
			}
			finally
			{
				if (b.conn != null)
					b.conn.Close();
			}
		}
		public void edit_storage(String city, String storage, String ncity, String nstorage)
		{
			try
			{
				b.conect_to_bd();
				String  cmdtext = "UPDATE dbo.Table_Storage SET Storage=@nstorage, City=@ncity WHERE City LIKE @city AND Storage LIKE @storage";
				SqlCommand  cmd = new SqlCommand(cmdtext,b. conn);
				cmd.Parameters.AddWithValue("@city", city);
				cmd.Parameters.AddWithValue("@storage", storage);
				cmd.Parameters.AddWithValue("@ncity", ncity);
				cmd.Parameters.AddWithValue("@nstorage", nstorage);
				b.conn.Open();
				cmd.ExecuteNonQuery();
				MessageBox.Show("Данні успішно збережено!");
			}
			finally
			{
				if (b.conn != null)
					b.conn.Close();
			}
		}
		public void dellette_storage(String city, String storage)
		{
			try
			{
				b.conect_to_bd();
				String  cmdtext = "DELETE FROM dbo.Table_Storage  WHERE City LIKE @city AND Storage LIKE @storage";
				SqlCommand  cmd = new SqlCommand(cmdtext, b.conn);
				cmd.Parameters.AddWithValue("@city", city);
				cmd.Parameters.AddWithValue("@storage", storage);
				b.conn.Open();
				cmd.ExecuteNonQuery();
			}
			finally
			{
				if (b.conn != null)
					b.conn.Close();
			}
		}
		public void adduser(String Login, String Password, String job)
		{
			try
			{
				b.conect_to_bd();
				String cmdText = "create table Table_user( Login text not null,Password text not null, Job text not null)";
				SqlCommand cmd = new SqlCommand(cmdText, b.conn);
				b.conn.Open();
				cmd.ExecuteNonQuery();
			}
			finally
			{
				if (b.conn != null)
					b.conn.Close();
			}
			try
			{
				b.conect_to_bd();
				String  cmdText = "INSERT INTO dbo.Table_user(Login,Password, Job) VALUES(@login,@pass,@job)";
				SqlCommand  cmd = new SqlCommand(cmdText,b. conn);
				cmd.Parameters.AddWithValue("@login", Login);
				cmd.Parameters.AddWithValue("@pass", Password);
				cmd.Parameters.AddWithValue("@job", job);
				b.conn.Open();
				cmd.ExecuteNonQuery();
				MessageBox.Show("Данні успішно збережено!");
			}
			finally
			{
				if (b.conn != null)
					b.conn.Close();
			}
	;
			
		}

	};
}
