using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LibraryApp
{
    public partial class ReturnForm : Form
    {
        public ReturnForm()
        {
            InitializeComponent();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            string copyID = txtCopyID.Text.Trim();

            if (string.IsNullOrEmpty(copyID))
            {
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Text = "Please enter a valid Copy ID.";
                return;
            }

            string connectionString = @"Data Source=DESKTOP-KRQ345K\SQLEXPRESS;Initial Catalog=LibraryDB;Integrated Security=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Check reservation status
                    string checkReserveQuery = "SELECT ReservedBy FROM BookCopies WHERE CopyID = @CopyID";
                    SqlCommand checkCmd = new SqlCommand(checkReserveQuery, conn);
                    checkCmd.Parameters.AddWithValue("@CopyID", copyID);

                    object reservedBy = checkCmd.ExecuteScalar();

                    // Update copy status regardless of reservation
                    string updateQuery = "UPDATE BookCopies SET IsAvailable = 1, ReservedBy = NULL WHERE CopyID = @CopyID";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                    updateCmd.Parameters.AddWithValue("@CopyID", copyID);
                    updateCmd.ExecuteNonQuery();

                    // Set message
                    if (reservedBy != null && reservedBy != DBNull.Value)
                    {
                        lblStatus.ForeColor = System.Drawing.Color.DarkOrange;
                        lblStatus.Text = $"Copy returned. Notify member: {reservedBy} (Reserved).";
                    }
                    else
                    {
                        lblStatus.ForeColor = System.Drawing.Color.DarkGreen;
                        lblStatus.Text = "Copy returned successfully. No reservation.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblStatus.ForeColor = System.Drawing.Color.Red;
                lblStatus.Text = "Error: " + ex.Message;
            }
        }
    }
}
