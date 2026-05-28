using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Devices;
using MySqlConnector;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using static System.Collections.Specialized.BitVector32;

namespace IM_Student_Record
{
    // ================== STUDENT RECORD MANAGEMENT SYSTEM (C# WinForms) ==================
    // This application allows users to manage student records with features to add, update, delete, and view records in a MySQL database.
    // Key Features:
    // 1. Auto-generated Student ID with gap filling (reuses deleted IDs).
    // 2. Comprehensive validation for all input fields, including duplicate checks for email and phone.
    // 3. User-friendly error messages and confirmation dialogs for critical actions.
    // 4. Data grid view to display all student records with selection for editing.
    // 5. Form design with fixed size and disabled resizing for consistent user experience.
    // Note: Ensure that the MySQL database is set up with a 'students' table matching the expected schema for this application to function correctly.

    // ================== CONSTRUCTOR AND INITIALIZATION ==================
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
            ClearForm();
            ClearSelection();

            this.Shown += (s, e) =>
            {
                ClearForm();
                ClearSelection();
                this.ActiveControl = null;  // No control focused
            };
        }

        // ========== FORM SETUP (Read-only Student ID, Phone Mask, Name Capitalization) ==========
        private void SetupForm()
        {
            // Make Student ID editable (user will enter PUP format)
            txtStudentID.ReadOnly = false;
            txtStudentID.BackColor = Color.White;

            // Set default Phone mask properties
            mtbPhone.PromptChar = ' ';
            mtbPhone.TextMaskFormat = MaskFormat.IncludeLiterals;

            // Setup placeholders for textboxes
            SetupPlaceholders();

            // Use tooltip version for email instead of regular placeholder
            SetupEmailWithTooltip();

            // Setup comboboxes with default selections
            SetupComboBoxes();

            // Set default Date of Birth
            dtpDOB.Value = DateTime.Now.AddYears(-18);

            // Disable automatic selection in DataGridView
            dgvStudents.TabStop = false;  // Prevents tab from selecting grid

            // Clear any default selection
            dgvStudents.ClearSelection();

            // Your existing Leave event for Full Name
            txtFullName.Leave += (s, e) =>
            {
                if (!string.IsNullOrWhiteSpace(txtFullName.Text) && txtFullName.Text != "Dela Cruz, Juan M.")
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
            };

            // Setup real-time visual validation (no message boxes)
            SetupRealTimeValidation();

            // Hide "Others" textbox initially
            txtOthers.Visible = false;
            lblOthers.Visible = false;
        }

        // ========== PLACEHOLDER, TOOLTIP, & COMBOBOX SETUP METHODS ==========
        private void SetupPlaceholders()
        {
            SetPlaceholder(txtStudentID, "2025-00126-SM-0");
            SetPlaceholder(txtFullName, "Dela Cruz, Juan M.");
            SetPlaceholder(txtEmail, "juandelacruz@iskolarngbayan.pup.edu.ph");
        }

        private void SetPlaceholder(TextBox textBox, string placeholderText)
        {
            textBox.Tag = placeholderText;
            textBox.Text = placeholderText;
            textBox.ForeColor = Color.Gray;

            textBox.Enter += (s, e) =>
            {
                if (textBox.Text == placeholderText)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            };

            textBox.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholderText;
                    textBox.ForeColor = Color.Gray;
                }
            };
        }

        private void SetupEmailWithTooltip()
        {
            string placeholderText = "juandelacruz@iskolarngbayan.pup.edu.ph";
            string tooltipText = "Use your PUP email: firstnameMIlastname@iskolarngbayan.pup.edu.ph\n" +
                                 "Or use Gmail: username@gmail.com";

            // Set placeholder
            txtEmail.Tag = placeholderText;
            txtEmail.Text = placeholderText;
            txtEmail.ForeColor = Color.Gray;

            // Add tooltip
            toolTip1.SetToolTip(txtEmail, tooltipText);
            toolTip1.ToolTipTitle = "Email Format";
            toolTip1.ToolTipIcon = ToolTipIcon.Info;
            toolTip1.IsBalloon = true;  // Optional: balloon style

            // Placeholder behavior
            txtEmail.Enter += (s, e) =>
            {
                if (txtEmail.Text == placeholderText)
                {
                    txtEmail.Text = "";
                    txtEmail.ForeColor = Color.Black;
                }
            };

            txtEmail.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtEmail.Text))
                {
                    txtEmail.Text = placeholderText;
                    txtEmail.ForeColor = Color.Gray;
                }
            };
        }

        private void SetupComboBoxes()
        {
            // Gender ComboBox
            cmbGender.Items.Clear();
            cmbGender.Items.Add("-- Select Gender --");
            cmbGender.Items.Add("Male");
            cmbGender.Items.Add("Female");
            cmbGender.Items.Add("Others");
            cmbGender.SelectedIndex = 0;
            cmbGender.DropDownStyle = ComboBoxStyle.DropDownList;

            // Course ComboBox
            cmbCourse.Items.Clear();
            cmbCourse.Items.Add("-- Select Courses --");
            cmbCourse.Items.Add("Bachelor of Science in Accountancy");
            cmbCourse.Items.Add("Bachelor of Science in Computer Engineering");
            cmbCourse.Items.Add("Bachelor of Science in Entrepreneurship");
            cmbCourse.Items.Add("Bachelor of Science in Hospitality Management");
            cmbCourse.Items.Add("Bachelor of Science in Information Technology");
            cmbCourse.Items.Add("Bachelor of Secondary Education major in English");
            cmbCourse.Items.Add("Bachelor of Secondary Education major in Mathematics");
            cmbCourse.Items.Add("Diploma in Office Management Technology with Specialization in Legal Office Management");
            cmbCourse.SelectedIndex = 0;
            cmbCourse.DropDownStyle = ComboBoxStyle.DropDownList;

            // Year Level ComboBox
            cmbYear.Items.Clear();
            cmbYear.Items.Add("-- Select Year --");
            cmbYear.Items.Add("1");
            cmbYear.Items.Add("2");
            cmbYear.Items.Add("3");
            cmbYear.Items.Add("4");
            cmbYear.SelectedIndex = 0;
            cmbYear.DropDownStyle = ComboBoxStyle.DropDownList;

            // Section ComboBox (if needed, otherwise remove this)
            cmbSection.Items.Clear();
            cmbSection.Items.Add("-- Select Section --");
            cmbSection.Items.Add("1");
            cmbSection.Items.Add("2");
            cmbSection.Items.Add("3");
            cmbSection.Items.Add("4");
            cmbSection.SelectedIndex = 0;
            cmbSection.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        // ================== PUP ID CONVERSION METHODS ==================
        // Convert PUP format to number for database storage
        // Input: "2025-00126-SM-0" or "2025-00126-SM-1"
        // Output: 2025001260 or 2025001261
        private int ConvertPUPToNumber(string pupId)
        {
            // Remove everything except numbers
            string numbersOnly = Regex.Replace(pupId, @"[^0-9]", "");
            return int.Parse(numbersOnly);
        }

        // Convert number back to PUP format for display
        // Input: 2025001260
        // Output: "2025-00126-SM-0"
        private string ConvertNumberToPUP(int number)
        {
            string numStr = number.ToString();

            // Ensure we have enough digits
            if (numStr.Length >= 10)
            {
                string year = numStr.Substring(0, 4);        // "2025"
                string enrollNo = numStr.Substring(4, 5);    // "00126"
                string type = numStr.Substring(9, 1);        // "0" or "1"

                // Campus is always "SM" as you mentioned
                return $"{year}-{enrollNo}-SM-{type}";
            }

            return numStr;
        }


        // ================== DATABASE HELPER METHODS ==================

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
                    // updated the query to include section
                    string query = "SELECT student_id AS 'Student ID', full_name AS 'Full Name', " +
                                   "date_of_birth AS 'Date of Birth', gender AS 'Gender', " +
                                   "course AS 'Course', year_level AS 'Year', section AS 'Section', " +  // ADD THIS
                                   "email AS 'Email', phone AS 'Phone' FROM students ORDER BY student_id";

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvStudents.DataSource = dt;

                        // Clear any selection after loading data
                        ClearSelection();
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
        private void SetupRealTimeValidation()
        {
            // Real-time visual feedback (no message boxes)
            txtFullName.Leave += (s, e) =>
            {
                if (!string.IsNullOrWhiteSpace(txtFullName.Text) && txtFullName.Text != "Dela Cruz, Juan M.")
                {
                    string fullName = txtFullName.Text.Trim();
                    if (!fullName.Contains(","))
                    {
                        txtFullName.BackColor = Color.LightPink;  // Highlight invalid
                    }
                    else
                    {
                        txtFullName.BackColor = Color.White;  // Clear highlight
                    }
                }
                else
                {
                    txtFullName.BackColor = Color.White;
                }
            };

            txtStudentID.Leave += (s, e) =>
            {
                if (!string.IsNullOrWhiteSpace(txtStudentID.Text) && txtStudentID.Text != "2025-00126-SM-0")
                {
                    Regex pupIdRegex = new Regex(@"^\d{4}-\d{5}-SM-[01]$");
                    if (!pupIdRegex.IsMatch(txtStudentID.Text.Trim()))
                    {
                        txtStudentID.BackColor = Color.LightPink;
                    }
                    else
                    {
                        txtStudentID.BackColor = Color.White;
                    }
                }
                else
                {
                    txtStudentID.BackColor = Color.White;
                }
            };

            txtEmail.Leave += (s, e) =>
            {
                if (!string.IsNullOrWhiteSpace(txtEmail.Text) && txtEmail.Text != "juandelacruz@iskolarngbayan.pup.edu.ph")
                {
                    try
                    {
                        var addr = new System.Net.Mail.MailAddress(txtEmail.Text.Trim());
                        if (addr.Address != txtEmail.Text.Trim())
                        {
                            txtEmail.BackColor = Color.LightPink;
                        }
                        else
                        {
                            txtEmail.BackColor = Color.White;
                        }
                    }
                    catch
                    {
                        txtEmail.BackColor = Color.LightPink;
                    }
                }
                else
                {
                    txtEmail.BackColor = Color.White;
                }
            };
        }

        private bool ValidateStudentID()
        {
            // Skip if placeholder is showing
            if (txtStudentID.Text == "2025-00126-SM-0" || string.IsNullOrWhiteSpace(txtStudentID.Text))
            {
                MessageBox.Show("Student ID is required!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStudentID.Focus();
                return false;
            }

            string studentId = txtStudentID.Text.Trim();

            Regex pupIdRegex = new Regex(@"^\d{4}-\d{5}-SM-[01]$");

            if (!pupIdRegex.IsMatch(studentId))
            {
                MessageBox.Show(
                    "Invalid Student ID Format!\n\n" +
                    "Please use this format: YYYY-NNNNN-SM-0 or YYYY-NNNNN-SM-1\n\n" +
                    "Examples:\n" +
                    "✓ 2025-00126-SM-0 (Regular student)\n" +
                    "✓ 2025-00126-SM-1 (Transferee)",
                    "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStudentID.Focus();
                return false;
            }

            // Extract year for validation
            string yearPart = studentId.Substring(0, 4);
            int year = int.Parse(yearPart);
            int currentYear = DateTime.Now.Year;

            // PUP Sta. Maria was founded in 2005
            const int pupFoundingYear = 2005;

            // Check if year is before PUP Sta. Maria was founded
            if (year < pupFoundingYear)
            {
                MessageBox.Show($"PUP Sta. Maria Campus was founded in {pupFoundingYear}.\n\n" +
                               $"Student ID year '{year}' is before the campus existed!\n\n" +
                               $"Please enter a year from {pupFoundingYear} onwards.",
                    "Invalid Year", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStudentID.Focus();
                return false;
            }

            // Check if year is in the future
            if (year > currentYear)
            {
                MessageBox.Show($"Student ID year cannot be in the future!\n\n" +
                               $"Current year is {currentYear}. Please enter a valid year.",
                    "Invalid Year", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStudentID.Focus();
                return false;
            }

            // Optional: Add warning for very old students (older than 20 years)
            if (currentYear - year > 20)
            {
                DialogResult result = MessageBox.Show(
                    $"Student ID year is {year} ({currentYear - year} years ago).\n\n" +
                    $"This student would be older than typical college age.\n\n" +
                    $"Continue anyway?",
                    "Unusual Year Warning",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    txtStudentID.Focus();
                    return false;
                }
            }

            return true;
        }

        private bool ValidateFullName()
        {
            // Skip if placeholder is showing
            if (txtFullName.Text == "Dela Cruz, Juan M." || string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Full Name is required!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            string fullName = txtFullName.Text.Trim();

            if (!fullName.Contains(","))
            {
                MessageBox.Show(
                    "Invalid Name Format!\n\n" +
                    "Please use this format: Last Name, First Name\n\n" +
                    "Examples:\n" +
                    "✓ Gonzales III, Pedro B.\n" +
                    "✓ Santos, Maria C.\n" +
                    "✓ Dela Cruz, Juan M.\n" +
                    "✓ Sarmiento, Sharlynne Jemima",
                    "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (cmbGender.SelectedIndex == 0 || cmbGender.SelectedIndex == -1)  // Skip placeholder
            {
                MessageBox.Show("Please select a Gender!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbGender.Focus();
                return false;
            }

            // If "Others" is selected, check if textbox is filled
            if (cmbGender.SelectedItem?.ToString() == "Others")
            {
                if (string.IsNullOrWhiteSpace(txtOthers.Text))
                {
                    MessageBox.Show("Please specify your gender!", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtOthers.Focus();
                    return false;
                }
                // pa-change na lang please kapag na edit na yung varchar limit sa database thanks
                if (txtOthers.Text.Length > 8)
                {
                    MessageBox.Show("Gender specification too long (max 8 chars)!", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtOthers.Focus();
                    return false;
                }
            }

            return true;
        }

        private bool ValidateCourse()
        {
            if (cmbCourse.SelectedIndex == 0 || cmbCourse.SelectedIndex == -1)  // Skip placeholder
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
            if (cmbYear.SelectedIndex == 0 || cmbYear.SelectedIndex == -1)  // Skip placeholder
            {
                MessageBox.Show("Please select a Year Level!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbYear.Focus();
                return false;
            }
            return true;
        }

        private bool ValidateSection()
        {
            if (cmbSection.SelectedIndex == 0 || cmbSection.SelectedIndex == -1)  // Skip placeholder
            {
                MessageBox.Show("Please select a Section!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbSection.Focus();
                return false;
            }
            return true;
        }

        // EMAIL VALIDATION (with duplicate check) 
        private bool ValidateEmail(bool isUpdate = false)
        {
            // Skip if placeholder is showing
            if (txtEmail.Text == "juandelacruz@iskolarngbayan.pup.edu.ph" || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Email address is required!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            string email = txtEmail.Text.Trim().ToLower();

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
            int? excludeId = isUpdate && !string.IsNullOrWhiteSpace(txtStudentID.Text) && txtStudentID.Text != "2025-00126-SM-0"
                ? ConvertPUPToNumber(txtStudentID.Text)
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

        // PHONE VALIDATION (with duplicate check) 
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
            int? excludeId = isUpdate && !string.IsNullOrWhiteSpace(txtStudentID.Text) && txtStudentID.Text != "2025-00126-SM-0"
            ? ConvertPUPToNumber(txtStudentID.Text)
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
            if (!ValidateStudentID()) return false;
            if (!ValidateFullName()) return false;
            if (!ValidateDOB()) return false;
            if (!ValidateGender()) return false;
            if (!ValidateCourse()) return false;
            if (!ValidateYearLevel()) return false;
            if (!ValidateSection()) return false;
            if (!ValidateEmail(isUpdate)) return false;
            if (!ValidatePhone(isUpdate)) return false;

            return true;
        }

        // ========== UI HELPERS ==========

        private void ClearForm()
        {
            // Reset textboxes to placeholders
            txtStudentID.Text = "2025-00126-SM-0";
            txtStudentID.ForeColor = Color.Gray;

            txtFullName.Text = "Dela Cruz, Juan M.";
            txtFullName.ForeColor = Color.Gray;

            txtEmail.Text = "juandelacruz@iskolarngbayan.pup.edu.ph";
            txtEmail.ForeColor = Color.Gray;

            // Clear phone
            mtbPhone.Text = "";

            // Reset comboboxes to default selection (placeholder)
            cmbGender.SelectedIndex = 0;   // "-- Select Gender --"
            cmbCourse.SelectedIndex = 0;    // "-- Select Course --"
            cmbYear.SelectedIndex = 0;      // "-- Select Year --"
            cmbSection.SelectedIndex = 0;   // "-- Select Section --" (NEW)

            // Hide "Others" textbox
            txtOthers.Visible = false;
            txtOthers.Text = "";
            lblOthers.Visible = false;

            // Reset DatePicker
            dtpDOB.Value = DateTime.Now.AddYears(-18);

            // Remove focus from any control
            this.ActiveControl = null;
        }

        // CLEAR DATAGRIDVIEW SELECTION
        private void ClearSelection()
        {
            // Clear any selected rows in DataGridView
            if (dgvStudents.Rows.Count > 0)
            {
                dgvStudents.ClearSelection();
            }

            // Also ensure no cell is selected
            dgvStudents.CurrentCell = null;
        }



        // ================== CRUD OPERATIONS ==================
        // ADD BUTTON 
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Only here we show error messages
            if (!ValidateAllFields(isUpdate: false))
            {
                return;  // Validation failed with message box
            }

            if (!ValidateAllFields(isUpdate: false))
            {
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    conn.Open();

                    // Check for duplicate student ID
                    string checkIdQuery = "SELECT COUNT(*) FROM students WHERE student_id = @id";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkIdQuery, conn))
                    {
                        int studentIdNumber = ConvertPUPToNumber(txtStudentID.Text.Trim());
                        checkCmd.Parameters.AddWithValue("@id", studentIdNumber);

                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (count > 0)
                        {
                            MessageBox.Show($"Student ID '{txtStudentID.Text}' already exists!",
                                "Duplicate ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Check for duplicate student (Same Name AND Date of Birth)
                    string checkQuery = "SELECT COUNT(*) FROM students WHERE full_name = @checkName AND date_of_birth = @checkDob";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@checkName", txtFullName.Text.Trim());
                        checkCmd.Parameters.AddWithValue("@checkDob", dtpDOB.Value.ToString("yyyy-MM-dd"));

                        int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (count > 0)
                        {
                            MessageBox.Show("A student with this exact Name and Date of Birth already exists!",
                                            "Duplicate Student", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO students (student_id, full_name, date_of_birth, gender, course, year_level, email, phone) " +
                                         "VALUES (@id, @name, @dob, @gender, @course, @year, @email, @phone)";

                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", ConvertPUPToNumber(txtStudentID.Text.Trim()));
                        cmd.Parameters.AddWithValue("@name", txtFullName.Text.Trim());
                        cmd.Parameters.AddWithValue("@dob", dtpDOB.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@gender", cmbGender.Text);
                        cmd.Parameters.AddWithValue("@course", cmbCourse.Text);
                        cmd.Parameters.AddWithValue("@year", int.Parse(cmbYear.Text));
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim().ToLower());
                        cmd.Parameters.AddWithValue("@phone", mtbPhone.Text);

                        cmd.ExecuteNonQuery();

                        MessageBox.Show($"Student record added successfully!\n\nStudent ID: {txtStudentID.Text}",
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
                    MessageBox.Show("Duplicate entry detected!", "Error",
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

        // UPDATE BUTTON
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Only here we show error messages
            if (!ValidateAllFields(isUpdate: false))
            {
                return;  // Validation failed with message box
            }

            if (txtStudentID.Text == "2025-00126-SM-0" || string.IsNullOrWhiteSpace(txtStudentID.Text))
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
                        cmd.Parameters.AddWithValue("@id", ConvertPUPToNumber(txtStudentID.Text));
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

        // DELETE BUTTON 
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtStudentID.Text == "2025-00126-SM-0" || string.IsNullOrWhiteSpace(txtStudentID.Text))
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
                            cmd.Parameters.AddWithValue("@id", ConvertPUPToNumber(txtStudentID.Text));

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

        // REFRESH BUTTON
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadGridData();
            ClearForm();
        }

        // ========== EVENT HANDLERS ==========

        // DATA GRID VIEW SELECTION
        private void dgvStudents_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvStudents.SelectedRows[0];

                int studentIdNumber = Convert.ToInt32(row.Cells["Student ID"].Value);
                txtStudentID.Text = ConvertNumberToPUP(studentIdNumber);

                txtFullName.Text = row.Cells["Full Name"].Value.ToString();
                dtpDOB.Value = Convert.ToDateTime(row.Cells["Date of Birth"].Value);

                string gender = row.Cells["Gender"].Value.ToString();
                if (cmbGender.Items.Contains(gender))
                {
                    cmbGender.SelectedItem = gender;
                    txtOthers.Visible = false;
                }
                else
                {
                    cmbGender.SelectedItem = "Others";
                    txtOthers.Text = gender;
                    txtOthers.Visible = true;
                    lblOthers.Visible = true;
                }

                cmbCourse.SelectedItem = row.Cells["Course"].Value.ToString();
                cmbYear.SelectedItem = row.Cells["Year"].Value.ToString();
                cmbSection.SelectedItem = row.Cells["Section"].Value.ToString();  // ADD THIS
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                mtbPhone.Text = row.Cells["Phone"].Value.ToString();
            }
        }

        // FORM LOAD 
        private void frmStudentRec_Load(object sender, EventArgs e)
        {
            LoadGridData();
            this.ShowIcon = false;       // Hides form icon mwehehehe
        }

        // wait a moment i'll organize this a bit more, it's a bit messy right now

        private void CmbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGender.SelectedItem?.ToString() == "Others")
            {
                txtOthers.Visible = true;
                lblOthers.Visible = true;
                txtOthers.Focus();
            }
            else
            {
                txtOthers.Visible = false;
                lblOthers.Visible = false;
                txtOthers.Text = "";
            }
        }

        private string GetGenderForDatabase()
        {
            string gender = cmbGender.SelectedItem?.ToString();
            if (gender == "Others" && !string.IsNullOrWhiteSpace(txtOthers.Text))
            {
                return txtOthers.Text.Trim();
            }
            return gender;
        }

    }
}