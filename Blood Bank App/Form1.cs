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

namespace Blood_Bank_App
{
    public partial class Form1 : Form
    {
        int S_No = 0;
        SqlConnection sqlCon = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Saad Xeon\Documents\Visual Studio 2013\Projects\Blood Bank App\DB\BloodBank_DB.mdf;Integrated Security=True;Connect Timeout=30");
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                if (btnSave.Text == "Save")
                {
                    SqlCommand sqlCmd = new SqlCommand("BloodbankAddorEdit", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@mode", "Add");
                    sqlCmd.Parameters.AddWithValue("@S_No", S_No);
                    sqlCmd.Parameters.AddWithValue("@Donor_Reg_No", donorNumber.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Donor_Name", donorName.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Weight", txtWeight.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Blood_Group", donorBloodGrp.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Gender", txtgender.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Date_of_Donation", donordateofdonation.Text);
                    sqlCmd.Parameters.AddWithValue("@Contact_Number", donorContact.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Address", donorAddress.Text.Trim());
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Saved");
                }
                    
                else
                {
                    SqlCommand sqlCmd = new SqlCommand("BloodbankAddorEdit", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@mode", "Edit");
                    sqlCmd.Parameters.AddWithValue("@S_No", S_No);
                    sqlCmd.Parameters.AddWithValue("@Donor_Reg_No", donorNumber.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Donor_Name", donorName.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Weight", txtWeight.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Blood_Group", donorBloodGrp.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Gender", txtgender.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Date_of_Donation", donordateofdonation.Text);
                    sqlCmd.Parameters.AddWithValue("@Contact_Number", donorContact.Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Address", donorAddress.Text.Trim());
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Updated");

                }
                //Reset();
                FillDataGridView();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message,"Error Message");
            }
            finally
            {
                sqlCon.Close();
            }
        }
        void FillDataGridView()
        {
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlDataAdapter SqlDa = new SqlDataAdapter("BloodBankVieworSearch", sqlCon);
            SqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            SqlDa.SelectCommand.Parameters.AddWithValue("@Blood_Group", listSearch.Text.Trim());
            DataTable dtbl = new DataTable();
            SqlDa.Fill(dtbl);
            dashboardInfo.DataSource = dtbl;
            dashboardInfo.Columns[0].Visible = false;
            sqlCon.Close();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FillDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error Message");
            }
        }

        private void dashboardInfo_DoubleClick(object sender, EventArgs e)
        {
            if (dashboardInfo.CurrentRow.Index!= -1)
            {
                S_No = Convert.ToInt32(dashboardInfo.CurrentRow.Cells[0].Value.ToString());
                donorNumber.Text = dashboardInfo.CurrentRow.Cells[1].Value.ToString();
                donorName.Text = dashboardInfo.CurrentRow.Cells[2].Value.ToString();
                txtWeight.Text = dashboardInfo.CurrentRow.Cells[3].Value.ToString();
                donorBloodGrp.Text = dashboardInfo.CurrentRow.Cells[4].Value.ToString();
                txtgender.Text = dashboardInfo.CurrentRow.Cells[5].Value.ToString();
                donordateofdonation.Text = dashboardInfo.CurrentRow.Cells[6].Value.ToString();
                donorContact.Text = dashboardInfo.CurrentRow.Cells[7].Value.ToString();
                donorAddress.Text = dashboardInfo.CurrentRow.Cells[8].Value.ToString();

                btnSave.Text = "Update";
                btnDelete.Enabled = true;

            }
        }

        void Reset()
        {
            donorNumber.Text = donorName.Text = txtWeight.Text = donorBloodGrp.Text = txtgender.Text = donordateofdonation.Text = donorContact.Text = donorAddress.Text = "";
            btnSave.Text = "Save";
            S_No = 0;
            btnDelete.Enabled = false;
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Reset();
            FillDataGridView();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand("Bloodbankdeletion", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@S_No", S_No);
                sqlCmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Deleted");
                Reset();
                FillDataGridView();

            }
            catch (Exception ex)
            {
                
               MessageBox.Show(ex.Message,"Error Message");
            }
        }
    }
}
