using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LavyerApp
{
    public partial class Form3 : Form
    {
        SqlConnection sqlConnection;
        public Form3()
        {
            InitializeComponent();
        }

        private async void Form3_Load(object sender, EventArgs e)
        {
            try
            {
                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lutik\source\repos\LavyerApp\LavyerApp\Database1.mdf;Integrated Security=True";
                sqlConnection = new SqlConnection(connectionString);
                await sqlConnection.OpenAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand command = new SqlCommand("INSERT INTO [Clients] (Name, Password, Company, Email, Number, address)VALUES(@Name,@Password,@Company,@Email,@Number,@address)", sqlConnection);
                command.Parameters.AddWithValue("Name", textBox1.Text);
                command.Parameters.AddWithValue("Password", textBox3.Text);
                command.Parameters.AddWithValue("Company", textBox2.Text);
                command.Parameters.AddWithValue("Email", textBox4.Text);
                command.Parameters.AddWithValue("Number", textBox5.Text);
                command.Parameters.AddWithValue("address", textBox6.Text);
                await command.ExecuteNonQueryAsync();
                MessageBox.Show("Account added","Reg", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
