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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MyFirstProgramWithDatabase
{
    public partial class UpdateMemberFrm : Form
    {
        private SqlConnection sqlConnect;
        private SqlCommand sqlCommand;
        private SqlDataReader sqlReader;
        private string connectionString;
        public UpdateMemberFrm()
        {
            InitializeComponent();

            connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\rena0\source\repos\MyFirstProgramWithDatabase\
                                                                                       MyFirstProgramWithDatabase\DB\ClubDB.mdf;Integrated Security=True";
            sqlConnect = new SqlConnection(connectionString);
        }

        private void UpdateMemberFrm_Load(object sender, EventArgs e)
        {
            sqlConnect.Open();
            string query1 = "SELECT StudentID FROM ClubMembers";
            sqlCommand = new SqlCommand(query1, sqlConnect);
            sqlReader = sqlCommand.ExecuteReader();
            
            while(sqlReader.Read())
            {
                cbStudentID.Items.Add(sqlReader["StudentID"]);
            }

            sqlConnect.Close();
        }

        private void cbStudentID_SelectedValueChanged(object sender, EventArgs e)
        {
            sqlConnect.Open();
            sqlCommand = new SqlCommand("SELECT * FROM ClubMembers WHERE StudentID = @stuID", sqlConnect);
            sqlCommand.Parameters.AddWithValue("@stuID", cbStudentID.Text);
            sqlReader = sqlCommand.ExecuteReader();
            sqlReader.Read();

            txtFirstName.Text = sqlReader["FirstName"].ToString();
            txtMiddleName.Text = sqlReader["MiddleName"].ToString();
            txtLastName.Text = sqlReader["LastName"].ToString();
            txtAge.Text = sqlReader["Age"].ToString();
            cbGender.Text = sqlReader["Gender"].ToString();
            cbProgram.Text = sqlReader["Program"].ToString();
            sqlConnect.Close();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            sqlConnect.Open();
            string query = @"UPDATE ClubMembers SET FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, 
                            Age = @Age, Gender = @Gender, Program = @Program WHERE StudentID = @stuID";

            sqlCommand = new SqlCommand(query, sqlConnect);
            sqlCommand.Parameters.AddWithValue("@stuID", cbStudentID.Text);
            sqlCommand.Parameters.AddWithValue("@FirstName", txtFirstName.Text);
            sqlCommand.Parameters.AddWithValue("@MiddleName", txtMiddleName.Text);
            sqlCommand.Parameters.AddWithValue("@LastName", txtLastName.Text);
            sqlCommand.Parameters.AddWithValue("@Age", txtAge.Text);
            sqlCommand.Parameters.AddWithValue("@Gender", cbGender.Text);
            sqlCommand.Parameters.AddWithValue("@Program", cbProgram.Text);
            sqlCommand.ExecuteNonQuery();
            sqlConnect.Close();

            this.Close();
        }

    }
}
