using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace KK3market
{
    public partial class mainForm : Form
    {
        private OleDbConnection connection = new OleDbConnection();

        bool mouseDown;
        private Point offset;
        public mainForm()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\Skill\OOP\BetaMART\KK3market\bin\Debug\Database1.accdb;Persist Security Info=False;";
        }

        private void mouseDown_Event(object sender, MouseEventArgs e)
        {
            offset.X = e.X;
            offset.Y = e.Y;
            mouseDown = true;
        }

        private void mouseMove_Event(object sender, MouseEventArgs e)
        {
            if (mouseDown == true)
            {
                Point currrentScreenPos = PointToScreen(e.Location);
                Location = new Point(currrentScreenPos.X - offset.X, currrentScreenPos.Y - offset.Y);
            }
        }

        private void mouseUp_Event(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                connection.Open();

                connection.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show("Error " + a);
            }
        }


        private void exitBtn_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to Exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes){
                this.Close();
            }
            else
            {
                
            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void barangView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            GetBarangView();
        }

        public void GetBarangView()
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM BarangTab";
            DataTable dt = new DataTable();

            OleDbDataReader reader = command.ExecuteReader();
            dt.Load(reader);

            connection.Close();
        }



        private void insertBtn_Click(object sender, EventArgs e)
        {
            connection.Open();
            OleDbCommand command = new OleDbCommand();
            int jumlah = Convert.ToInt32(txtJumlah.Text);
            int harga = Convert.ToInt32(txtHarga.Text);
            command.Connection = connection;
            command.CommandText = "INSERT INTO BarangTab (NamaBarang, HargaBarang, JumlahBarang, tanggal) VALUES (@Name, @harga, @jumlah, @datenow)";
            command.Parameters.AddRange(new[]
            {
                new OleDbParameter("@Name", txtNama.Text),
                new OleDbParameter("@harga", harga),
                new OleDbParameter("@jumlah", jumlah),
                new OleDbParameter("@datenow", DateTime.Today),
            });
            command.ExecuteNonQuery();
            MessageBox.Show("Created");

            connection.Close();
        }
        private void editBtn_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                int jumlah = Convert.ToInt32(txtJumlah.Text);
                command.Connection = connection;
                string query = "UPDATE BarangTab " +
                   "SET NamaBarang = @Name, HargaBarang=@harga, JumlahBarang=@jumlah, tanggal=@datenow WHERE ID=" + txtID.Text + "";
                command.CommandText = query;
                command.Parameters.AddRange(new[]
                {
                new OleDbParameter("@Name", txtNama.Text),
                new OleDbParameter("@harga", txtHarga.Text),
                new OleDbParameter("@jumlah", jumlah),
                new OleDbParameter("@datenow", DateTime.Today),
            });
                command.ExecuteNonQuery();
                MessageBox.Show("Edited");


                connection.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show("ID must be filled");
            }


        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "SELECT * FROM BarangTab";
                command.CommandText = query;

                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();

                da.Fill(dt);
                barangView.DataSource = dt;

                connection.Close();
            }
            catch (Exception a)
            {
                MessageBox.Show("Error " + a);
            }

        }

        private void deleteBtn_Click_1(object sender, EventArgs e)
        {
            if(txtID.Text == "")
            {
                MessageBox.Show("Sorry, ID can't be empty");
            }
            else
            {
                if (MessageBox.Show("Do you want to delete this data?", "Delete data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    string query = "DELETE FROM BarangTab WHERE ID=" + txtID.Text + "";
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                    MessageBox.Show("Deleted");
                    connection.Close();
                }
                else
                {
                    MessageBox.Show("Data not deleted");
                }
            }



                
           
        }

        private void nameMarket2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}

