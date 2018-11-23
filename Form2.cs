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
    public partial class Form2 : Form
    {
        SqlConnection sqlconnection;
        OpenFileDialog openFile1 = new OpenFileDialog();
        SaveFileDialog saveFile1 = new SaveFileDialog();
        static string Chosen_File = @"C:\\Users\\Lutik\\Desktop\blya.rtf";
        static int i = 0;
        static int applic = 0;
        static string casee = null;

        const string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Lutik\source\repos\LavyerApp\LavyerApp\Database1.mdf;Integrated Security=True";
        public Form2()
        {
            InitializeComponent();
        }

        private async void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                sqlconnection = new SqlConnection(connectionString);
                await sqlconnection.OpenAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [case_data]", sqlconnection);
            try
            {
                sqlReader = await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    if (Convert.ToString(sqlReader["client_id"]) == Form1.clientt)
                    {
                        applic++;
                        label2.Text = "000" + Convert.ToString(sqlReader["case_id"]);
                        casee = Convert.ToString(sqlReader["case_id"]);
                    }
                }
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
            zagryz();
            panel2.Hide();
            panel3.Hide();
            label4.Text = "Клієнт №:000" + Form1.clientt;
            button7.Hide();
            label6.Hide();
            richTextBox3.Hide();
            textBox3.Hide();
            label7.Hide();
            richTextBox4.Hide();
            richTextBox1.LoadFile(Chosen_File, RichTextBoxStreamType.PlainText);

        }
        private void zagryz()
        {
            if (casee != null)
            {
                SqlCommand command_dt = new SqlCommand();
                command_dt.Connection = sqlconnection;
                SqlDataAdapter sql_ada = new SqlDataAdapter();
                DataTable dt = new DataTable();
                try
                {
                    command_dt.CommandText = "SELECT action, cost FROM [accounting] WHERE case_id=" + casee;
                    sql_ada.SelectCommand = command_dt;
                    sql_ada.Fill(dt);
                    dataGridView1.DataSource = dt;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Тут", ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private async void button4_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand command = new SqlCommand("INSERT INTO [Consults] (client_id,status)VALUES(@client_id,@status)", sqlconnection);
                command.Parameters.AddWithValue("client_id", Form1.clientt);
                command.Parameters.AddWithValue("status", "Opened");
                await command.ExecuteNonQueryAsync();
                MessageBox.Show("Заявка подана, очікуйте дзвінка від представника.", "Consult", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Show();
            panel3.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Show();
            panel3.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(i != 2)
                i++;
                switch(i)
                {
                    case 1:
                        label5.Hide();
                        richTextBox2.Hide();
                        label6.Show();
                        richTextBox3.Show();
                        textBox3.Show();
                        break;
                    case 2:
                        label6.Hide();
                        richTextBox3.Hide();
                        textBox3.Hide();
                        label7.Show();
                        richTextBox4.Show();
                        button7.Show();
                        break;
                }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (i != 0)
                i--;
                switch (i)
                {
                    case 0:
                        label5.Show();
                        richTextBox2.Show();
                        label6.Hide();
                        richTextBox3.Hide();
                        textBox3.Hide();
                        break;
                    case 1:
                        label7.Hide();
                        richTextBox4.Hide();
                        label6.Show();
                        richTextBox3.Show();
                        textBox3.Show();
                        button7.Hide();
                        break;
                }

        }

        private async void button7_Click(object sender, EventArgs e)
        {
            if (applic == 0)
            {
                DateTime thisday = DateTime.Today;
                try
                {
                    SqlCommand commandd = new SqlCommand("INSERT INTO [case_data] (client_id,area_of_law,start_date)VALUES(@client_id,@area_of_law,@start_date)", sqlconnection);
                    commandd.Parameters.AddWithValue("client_id", Form1.clientt);
                    commandd.Parameters.AddWithValue("area_of_law", textBox3.Text);
                    commandd.Parameters.AddWithValue("start_date", Convert.ToString(thisday));
                    await commandd.ExecuteNonQueryAsync();
                    MessageBox.Show("Заявка подана, очікуйте дзвінка від представника.", "Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    applic++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Заявка вже подана, очікуйте обробку інформації", "Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            zagryz();
        }

        private void button8_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += Form1.client_name + ": " + textBox1.Text + "\n";
            richTextBox1.SaveFile(Chosen_File, RichTextBoxStreamType.PlainText);
            richTextBox1.LoadFile(Chosen_File, RichTextBoxStreamType.PlainText);
        }


    }
}
