using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data;
using System.Text;

namespace IM_Student_Record
{
    public partial class frmStudentRec : Form
    {
        private readonly string connString = dbconnector.GetConnectionString();

        public frmStudentRec()
        {
            InitializeComponent();
        }

        private void LoadGridData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT student_id AS 'Student ID', full_name AS 'Full Name', date_of_birth AS 'Date of Birth', gender AS 'Gender', course AS 'Course', year_level AS 'Year', email AS 'Email', phone AS 'Phone' FROM students";

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvStudents.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Validates empty fields
            if (string.IsNullOrWhiteSpace(txtStudentID.Text) || string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(cmbGender.Text) || string.IsNullOrWhiteSpace(cmbCourse.Text) ||
                string.IsNullOrWhiteSpace(cmbYear.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("All fields are required!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();

                    // Checks duplicate student (Same Name AND Date of Birth)
                    string checkQuery = "SELECT COUNT(*) FROM students WHERE full_name = @checkName AND date_of_birth = @checkDob";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@checkName", txtFullName.Text.Trim());
                        checkCmd.Parameters.AddWithValue("@checkDob", dtpDOB.Value.ToString("yyyy-MM-dd"));

                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (count > 0)
                        {
                            // Blocks the operation if the student details already match another record
                            MessageBox.Show("A student with this exact Name and Date of Birth already exists in the system!",
                                            "Duplicate Student Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // If no duplicate is found, proceeds to insert the new student record
                    string insertQuery = "INSERT INTO students (student_id, full_name, date_of_birth, gender, course, year_level, email, phone) " +
                                         "VALUES (@id, @name, @dob, @gender, @course, @year, @email, @phone)";

                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", int.Parse(txtStudentID.Text));
                        cmd.Parameters.AddWithValue("@name", txtFullName.Text.Trim());
                        cmd.Parameters.AddWithValue("@dob", dtpDOB.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@gender", cmbGender.Text);
                        cmd.Parameters.AddWithValue("@course", cmbCourse.Text);
                        cmd.Parameters.AddWithValue("@year", int.Parse(cmbYear.Text));
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Student record added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //LoadGridData();
                    }
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062) // In case the ID itself matches
                {
                    MessageBox.Show($"The Student ID '{txtStudentID.Text}' already exists!", "Duplicate ID Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("General Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStudentID.Text))
            {
                MessageBox.Show("Please specify a Student ID to update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "UPDATE students SET full_name = @name, date_of_birth = @dob, gender = @gender, " +
                           "course = @course, year_level = @year, email = @email, phone = @phone WHERE student_id = @id";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", int.Parse(txtStudentID.Text));
                        cmd.Parameters.AddWithValue("@name", txtFullName.Text.Trim());
                        cmd.Parameters.AddWithValue("@dob", dtpDOB.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@gender", cmbGender.Text);
                        cmd.Parameters.AddWithValue("@course", cmbCourse.Text);
                        cmd.Parameters.AddWithValue("@year", int.Parse(cmbYear.Text));
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                        cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Student record updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //LoadGridData();
                        }
                        else
                        {
                            MessageBox.Show("No student found with that ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating record: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtStudentID.Text))
            {
                MessageBox.Show("Please enter or select a Student ID to delete.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Confirmation pop-up box
            DialogResult result = MessageBox.Show("Are you sure you want to delete this student record?",
                                                  "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string query = "DELETE FROM students WHERE student_id = @id";

                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connString))
                    {
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", int.Parse(txtStudentID.Text));

                            conn.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Student record deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //LoadGridData();
                            }
                            else
                            {
                                MessageBox.Show("No student found with that ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting record: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadGridData();
        }
    }
}
