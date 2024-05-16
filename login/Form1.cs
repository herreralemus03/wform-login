using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace login
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Ingresar(object sender, EventArgs e)
        {
            string username = userInput.Text;
            string password = passwordInput.Text;
            connectTo(username, password);
        }

        private void connectTo(string username, string password)
        {
            string connectionString = @"Data Source=localhost;Initial Catalog=universidad;Integrated Security=True";
            string query = $"SELECT * FROM users WHERE lower(username) = lower('{username}')";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string passwordDb = reader["password"].ToString();

                        if(password == passwordDb)
                        {
                            Form2 form2 = new Form2();
                            form2.Show();
                        } else
                        {
                            MessageBox.Show($"Credenciales invalidas");
                        }
                    }
                }

                reader.Close();
            }
        }
    }
}
