using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LavyerApp
{
    public partial class Form1 : Form
    {
        public static string clientt = null;
        public static string client_name = null;
        SqlConnection sqlconnection;
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lutik\source\repos\LavyerApp\LavyerApp\Database1.mdf;Integrated Security=True";
            sqlconnection = new SqlConnection(connectionString);
            await sqlconnection.OpenAsync();
            
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Clients]", sqlconnection);
            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    if (Convert.ToString(sqlReader["Name"]) == textBox1.Text)
                    {
                        if (Convert.ToString(sqlReader["Password"]) == textBox2.Text)
                        {
                            clientt = Convert.ToString(sqlReader["client_id"]);
                            client_name = Convert.ToString(sqlReader["Name"]);
                            Hide();
                            Form2 Main = new Form2();
                            Main.ShowDialog();
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Incorrect password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
                MessageBox.Show("Incorrect login", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
                    
            }
            
        }

        public async void button2_Click(object sender, EventArgs e)
        {
            Form3 reg = new Form3();
            reg.ShowDialog();
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sqlconnection != null && sqlconnection.State != ConnectionState.Closed)
                sqlconnection.Close();
        }
    }
}
