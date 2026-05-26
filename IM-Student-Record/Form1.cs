using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace IM_Student_Record
{
    public partial class frmStudentRec : Form
    {
        private readonly string connString = dbconnector.GetConnectionString();

        public frmStudentRec()
        {
            InitializeComponent();

            // Disable form resizing
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;  // Optional: disables maximize button
            this.MinimizeBox = true;   // Keep minimize button enabled

            SetupForm();
            LoadGridData();
        }

        private void SetupForm()
        {
            // Make Student ID read-only and auto-generated
            txtStudentID.ReadOnly = true;
            txtStudentID.BackColor = Color.LightGray;
            txtStudentID.Text = "Auto-generated";

            // Set default Phone mask properties
            mtbPhone.PromptChar = ' ';
            mtbPhone.TextMaskFormat = MaskFormat.IncludeLiterals;

            txtFullName.Leave += (s, e) =>
            {
                if (!string.IsNullOrWhiteSpace(txtFullName.Text))
                {
                    string text = txtFullName.Text.Trim();
                    System.Globalization.TextInfo ti = System.Globalization.CultureInfo.CurrentCulture.TextInfo;
                    string capitalized = ti.ToTitleCase(text.ToLower());
                    capitalized = Regex.Replace(capitalized, @"\s+Jr\.?", " Jr.");
                    capitalized = Regex.Replace(capitalized, @"\s+Sr\.?", " Sr.");
                    capitalized = Regex.Replace(capitalized, @"\s+Iii\b", " III");
                    capitalized = Regex.Replace(capitalized, @"\s+Iv\b", " IV");

                    if (capitalized != txtFullName.Text)
                    {
                        txtFullName.Text = capitalized;
                    }
                }
                ValidateFullName();
            };

        }


        // ========== GET NEXT STUDENT ID (Fills gaps - reuses deleted IDs) ==========
        private int GetNextStudentID()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();

                    // Finds the smallest missing ID in the sequence
                    string query = @"
                SELECT MIN(t1.student_id + 1) AS next_id
                FROM students t1
                LEFT JOIN students t2 ON t1.student_id + 1 = t2.student_id
                WHERE t2.student_id IS NULL";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        object result = cmd.ExecuteScalar();

                        if (result != DBNull.Value && result != null)
                        {
                            int nextId = Convert.ToInt32(result);
                            return nextId;
                        }
                    }

                    // If no gaps found, get max + 1
                    string maxQuery = "SELECT IFNULL(MAX(student_id), 0) + 1 FROM students";
                    using (MySqlCommand maxCmd = new MySqlCommand(maxQuery, conn))
                    {
                        return Convert.ToInt32(maxCmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting next ID: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 1;
            }
        }

        // ========== CHECK DUPLICATES (Email and Phone) ==========
        private bool IsEmailDuplicate(string email, int? excludeStudentId = null)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM students WHERE email = @email";
                    if (excludeStudentId.HasValue)
                    {
                        query += " AND student_id != @studentId";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@email", email.Trim().ToLower());
                        if (excludeStudentId.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@studentId", excludeStudentId.Value);
                        }

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        private bool IsPhoneDuplicate(string phone, int? excludeStudentId = null)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM students WHERE phone = @phone";
                    if (excludeStudentId.HasValue)
                    {
                        query += " AND student_id != @studentId";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@phone", phone.Trim());
                        if (excludeStudentId.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@studentId", excludeStudentId.Value);
                        }

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        // ========== LOAD GRID DATA ==========
        private void LoadGridData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();
                    string query = "SELECT student_id AS 'Student ID', full_name AS 'Full Name', " +
                                   "date_of_birth AS 'Date of Birth', gender AS 'Gender', " +
                                   "course AS 'Course', year_level AS 'Year', " +
                                   "email AS 'Email', phone AS 'Phone' FROM students ORDER BY student_id";

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvStudents.DataSource = dt;

                        // Update the "Auto-generated" text to show next available ID
                        if (dt.Rows.Count > 0)
                        {
                            int nextId = GetNextStudentID();
                            txtStudentID.Text = $"Will be: {nextId}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========== VALIDATION METHODS ==========
        private bool ValidateFullName()
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Full Name is required!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            string fullName = txtFullName.Text.Trim();

            // Simple check: must contain a comma
            if (!fullName.Contains(","))
            {
                MessageBox.Show(
                    "Invalid Name Format!\n\n" +
                    "Please use this format: Last Name, First Name\n\n" +
                    "Examples:\n" +
                    "✓ Gonzales III, Pedro B.\n" +
                    "✓ Santos, Maria C.\n" +
                    "✓ Dela Cruz, Juan M.\n" +
                    "✓ Sarmiento, Sharlynne Jemima\n" +
                    "✓ Reyes, Ana",
                    "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            if (fullName.Length < 5)
            {
                MessageBox.Show("Please enter a complete full name!",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            return true;
        }


        private bool ValidateDOB()
        {
            DateTime selectedDate = dtpDOB.Value;
            DateTime today = DateTime.Today;

            int age = today.Year - selectedDate.Year;
            if (selectedDate.Date > today.AddYears(-age)) age--;

            if (age < 17)
            {
                MessageBox.Show($"Student must be at least 17 years old! Current age: {age}",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (age > 35)
            {
                MessageBox.Show($"Age exceeds typical college range! Current age: {age}",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (selectedDate > today)
            {
                MessageBox.Show("Date of birth cannot be in the future!",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private bool ValidateGender()
        {
            if (string.IsNullOrWhiteSpace(cmbGender.Text))
            {
                MessageBox.Show("Please select a Gender!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbGender.Focus();
                return false;
            }
            return true;
        }

        private bool ValidateCourse()
        {
            if (string.IsNullOrWhiteSpace(cmbCourse.Text))
            {
                MessageBox.Show("Please select a Course!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCourse.Focus();
                return false;
            }
            return true;
        }

        private bool ValidateYearLevel()
        {
            if (string.IsNullOrWhiteSpace(cmbYear.Text))
            {
                MessageBox.Show("Please select a Year Level!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbYear.Focus();
                return false;
            }
            return true;
        }

        // ========== EMAIL VALIDATION (with duplicate check) ==========
        private bool ValidateEmail(bool isUpdate = false)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Email address is required!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            string email = txtEmail.Text.Trim().ToLower();

            // Check email format
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                if (addr.Address != email)
                {
                    MessageBox.Show("Please enter a valid email address!", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("Please enter a valid email address!\n\nExample: student@domain.com",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            // Check for duplicate email (skip during update if it's the same student)
            int? excludeId = isUpdate && !string.IsNullOrWhiteSpace(txtStudentID.Text) && txtStudentID.Text != "Auto-generated" && !txtStudentID.Text.StartsWith("Will be:")
                ? int.Parse(txtStudentID.Text)
                : (int?)null;

            if (IsEmailDuplicate(email, excludeId))
            {
                MessageBox.Show($"Email '{email}' is already used by another student!\n\nPlease use a different email address.",
                    "Duplicate Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                txtEmail.SelectAll();
                return false;
            }

            return true;
        }

        // ========== PHONE VALIDATION (with duplicate check) ==========
        private bool ValidatePhone(bool isUpdate = false)
        {
            if (string.IsNullOrWhiteSpace(mtbPhone.Text))
            {
                MessageBox.Show("Phone number is required!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbPhone.Focus();
                return false;
            }

            // Extract only digits for validation
            string cleanPhone = mtbPhone.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace("+", "").Replace(" ", "");

            if (cleanPhone.Length < 10)
            {
                MessageBox.Show("Please enter a valid phone number with at least 10 digits!",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbPhone.Focus();
                return false;
            }

            if (cleanPhone.Length > 13)
            {
                MessageBox.Show("Phone number cannot exceed 13 digits!",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbPhone.Focus();
                return false;
            }

            // Check for duplicate phone number
            int? excludeId = isUpdate && !string.IsNullOrWhiteSpace(txtStudentID.Text) && txtStudentID.Text != "Auto-generated" && !txtStudentID.Text.StartsWith("Will be:")
                ? int.Parse(txtStudentID.Text)
                : (int?)null;

            if (IsPhoneDuplicate(mtbPhone.Text.Trim(), excludeId))
            {
                MessageBox.Show($"Phone number '{mtbPhone.Text}' is already used by another student!\n\nPlease use a different phone number.",
                    "Duplicate Phone Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbPhone.Focus();
                mtbPhone.SelectAll();
                return false;
            }

            return true;
        }

        private bool ValidateAllFields(bool isUpdate = false)
        {
            if (!ValidateFullName()) return false;
            if (!ValidateDOB()) return false;
            if (!ValidateGender()) return false;
            if (!ValidateCourse()) return false;
            if (!ValidateYearLevel()) return false;
            if (!ValidateEmail(isUpdate)) return false;
            if (!ValidatePhone(isUpdate)) return false;

            return true;
        }

        private void ClearForm()
        {
            txtFullName.Clear();
            dtpDOB.Value = DateTime.Now.AddYears(-18);
            cmbGender.SelectedIndex = -1;
            cmbCourse.SelectedIndex = -1;
            cmbYear.SelectedIndex = -1;
            txtEmail.Clear();
            mtbPhone.Clear();

            // Show next available ID instead of "Auto-generated"
            int nextId = GetNextStudentID();
            txtStudentID.Text = $"Will be: {nextId}";
        }

        // ========== ADD BUTTON (with C# auto-increment) ==========
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateAllFields(isUpdate: false))
            {
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();

                    // Get the name in display format for duplicate check
                    string displayName = txtFullName.Text.Trim();

                    // Check for duplicate student (Same Name AND Date of Birth)
                    string checkQuery = "SELECT COUNT(*) FROM students WHERE full_name = @checkName AND date_of_birth = @checkDob";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@checkName", displayName);
                        checkCmd.Parameters.AddWithValue("@checkDob", dtpDOB.Value.ToString("yyyy-MM-dd"));

                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (count > 0)
                        {
                            MessageBox.Show("A student with this exact Name and Date of Birth already exists!",
                                            "Duplicate Student", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Get next available Student ID
                    int nextId = GetNextStudentID();

                    // Convert name to database format: "First Name Middle Last Name"
                    string databaseName = txtFullName.Text.Trim();  // Stores exactly as typed

                    // Insert new student record with converted name format
                    string insertQuery = "INSERT INTO students (student_id, full_name, date_of_birth, gender, course, year_level, email, phone) " +
                                         "VALUES (@id, @name, @dob, @gender, @course, @year, @email, @phone)";

                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", nextId);
                        cmd.Parameters.AddWithValue("@name", databaseName);  // ← Stored in DB format
                        cmd.Parameters.AddWithValue("@dob", dtpDOB.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@gender", cmbGender.Text);
                        cmd.Parameters.AddWithValue("@course", cmbCourse.Text);
                        cmd.Parameters.AddWithValue("@year", int.Parse(cmbYear.Text));
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim().ToLower());
                        cmd.Parameters.AddWithValue("@phone", mtbPhone.Text);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show($"Student record added successfully!\n\n" +
                                       $"Assigned Student ID: {nextId}\n" +
                                       $"Name in Database: {databaseName}",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadGridData();
                        ClearForm();
                    }
                }
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1062)
                {
                    MessageBox.Show("Student ID conflict! Please try again.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Database Error: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========== UPDATE BUTTON ==========
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtStudentID.Text == "Auto-generated" || txtStudentID.Text.StartsWith("Will be:") || string.IsNullOrWhiteSpace(txtStudentID.Text))
            {
                MessageBox.Show("Please select a record to update from the table.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateAllFields(isUpdate: true))
            {
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to update this record?",
                "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            string query = "UPDATE students SET full_name = @name, date_of_birth = @dob, gender = @gender, " +
                           "course = @course, year_level = @year, email = @email, phone = @phone " +
                           "WHERE student_id = @id";

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
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim().ToLower());
                        cmd.Parameters.AddWithValue("@phone", mtbPhone.Text);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Student record updated successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadGridData();
                            ClearForm();
                        }
                        else
                        {
                            MessageBox.Show("No student found with that ID.", "Not Found",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating record: " + ex.Message, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========== DELETE BUTTON ==========
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtStudentID.Text == "Auto-generated" || txtStudentID.Text.StartsWith("Will be:") || string.IsNullOrWhiteSpace(txtStudentID.Text))
            {
                MessageBox.Show("Please select a record to delete from the table.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Are you sure you want to delete Student ID: {txtStudentID.Text}?\n\nThis action cannot be undone!",
                                                  "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

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
                                MessageBox.Show("Student record deleted successfully.", "Deleted",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadGridData();
                                ClearForm();
                            }
                            else
                            {
                                MessageBox.Show("No student found with that ID.", "Not Found",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting record: " + ex.Message, "Database Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ========== REFRESH BUTTON ==========
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadGridData();
            ClearForm();
        }

        // ========== DATA GRID VIEW SELECTION ==========
        private void dgvStudents_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvStudents.SelectedRows[0];

                txtStudentID.Text = row.Cells["Student ID"].Value.ToString();

                // Display exactly what's in the database
                txtFullName.Text = row.Cells["Full Name"].Value.ToString();

                dtpDOB.Value = Convert.ToDateTime(row.Cells["Date of Birth"].Value);
                cmbGender.Text = row.Cells["Gender"].Value.ToString();
                cmbCourse.Text = row.Cells["Course"].Value.ToString();
                cmbYear.Text = row.Cells["Year"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                mtbPhone.Text = row.Cells["Phone"].Value.ToString();
            }
        }

        // ========== FORM LOAD ==========
        private void frmStudentRec_Load(object sender, EventArgs e)
        {
            LoadGridData();
            this.ShowIcon = false;       // Hides form icon mwehehehe
        }

    }
}