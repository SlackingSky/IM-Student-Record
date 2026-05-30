using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.Devices;
using MySqlConnector;
using System.ComponentModel.DataAnnotations;
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
        private static int? studentID;

        public frmStudentRec()
        {
            InitializeComponent();

            // Disable form resizing
            this.MaximizeBox = false;  // Optional: disables maximize button
            this.MinimizeBox = true;   // Keep minimize button enabled

            SetupForm();
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
        }

        // ========== PLACEHOLDER, TOOLTIP, & COMBOBOX SETUP METHODS ==========
        private void SetupPlaceholders()
        {
            SetPlaceholder(txtStudentID, "e.g., 2025-00126-SM-0");
            SetPlaceholder(txtFullName, "e.g., Dela Cruz, Juan M.");
            SetPlaceholder(txtEmail, "e.g., juandelacruz@iskolarngbayan.pup.edu.ph");
        }

        private void SetPlaceholder(TextBox textBox, string placeholderText)
        {
            textBox.PlaceholderText = placeholderText;
        }

        private void SetupEmailWithTooltip()
        {
            string placeholderText = "juandelacruz@iskolarngbayan.pup.edu.ph";
            string tooltipText = "Use your PUP email: firstnameMIlastname@iskolarngbayan.pup.edu.ph\n" +
                                 "Or use Gmail: username@gmail.com";

            // Set placeholder
            txtEmail.Tag = placeholderText;
            txtEmail.Text = placeholderText;

            // Add tooltip
            toolTip1.SetToolTip(txtEmail, tooltipText);
            toolTip1.ToolTipTitle = "Email Format";
            toolTip1.ToolTipIcon = ToolTipIcon.Info;
            toolTip1.IsBalloon = true;  // Optional: balloon style
        }

        private void SetupComboBoxes()
        {
            // Gender ComboBox
            cmbGender.Items.Clear();
            cmbGender.Items.Add("-- Select Gender --");
            cmbGender.Items.Add("Male");
            cmbGender.Items.Add("Female");
            cmbGender.Items.Add("Non-Binary");
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
            AdjustComboBoxDropdownWidth(cmbCourse);

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
        private async Task<bool> IsEmailDuplicate(string email, int? excemptStudentID)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT COUNT(*) FROM students WHERE email = @email";
                    if (excemptStudentID.HasValue)
                    {
                        query += " AND student_id != @studentId";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@email", email.Trim().ToLower());
                        if (excemptStudentID.HasValue != null)
                        {
                            cmd.Parameters.AddWithValue("@studentId", excemptStudentID);
                        }

                        object? result = await cmd.ExecuteScalarAsync();
                        int count = Convert.ToInt32(result);
                        return count > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> IsPhoneDuplicate(string phone, int? excemptStudentID)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    await conn.OpenAsync();
                    string query = "SELECT COUNT(*) FROM students WHERE phone = @phone";
                    if (excemptStudentID.HasValue)
                    {
                        query += " AND student_id != @studentId";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@phone", phone.Trim());
                        if (excemptStudentID.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@studentId", excemptStudentID);
                        }

                        object? result = await cmd.ExecuteScalarAsync();
                        int count = Convert.ToInt32(result);
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
        private async Task LoadGridData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    await conn.OpenAsync();
                    // updated the query to include section
                    string query = "SELECT student_id, student_number AS 'Student Number', full_name AS 'Full Name', " +
                                   "date_of_birth AS 'Date of Birth', gender AS 'Gender', " +
                                   "course AS 'Course', year_level AS 'Year', section AS 'Section', " +  // ADD THIS
                                   "email AS 'Email', phone AS 'Phone' FROM students ORDER BY student_id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            dgvStudents.SelectionChanged -= dgvStudents_SelectionChanged;
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            dgvStudents.DataSource = dt;
                            if (dgvStudents.Columns["student_id"] != null)
                            {
                                dgvStudents.Columns["student_id"].Visible = false;
                            }

                            // Clear any selection after loading data
                            ClearSelection();

                            //Makes columns not take too much space
                            dgvStudents.Columns["Email"].FillWeight = 150;
                            dgvStudents.Columns["Full Name"].FillWeight = 150;
                            dgvStudents.Columns["Course"].FillWeight = 200;

                            dgvStudents.Columns["Year"].FillWeight = 40;
                            dgvStudents.Columns["Section"].FillWeight = 50;
                            dgvStudents.Columns["Gender"].FillWeight = 60;
                            dgvStudents.SelectionChanged += dgvStudents_SelectionChanged;
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
        private void SetupRealTimeValidation()
        {
            // Real-time visual feedback (no message boxes)
            txtFullName.Leave += (s, e) =>
            {
                if (!string.IsNullOrWhiteSpace(txtFullName.Text))
                {
                    string fullName = txtFullName.Text.Trim();
                    if (IsNameInValid(fullName))
                    {
                        txtFullName.BackColor = Color.LightPink;  // Highlight invalid
                    }
                    else
                    {
                        txtFullName.BackColor = Color.White;  // Clear highlight
                    }
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

                        string email = txtEmail.Text.Trim().ToLower();

                        // Self explanatory naman, checks email pattern, prevents g@com
                        string emailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

                        if (!System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern))
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
        private bool IsNameInValid(string fullName)
        {
            string[] separatedName = fullName.Split(' ');
            if (fullName.Any(char.IsDigit))
            {
                return true;
            }

            if (!fullName.Contains(','))
            {
                return true;
            }

            if (fullName.Count() < 5)
            {
                return true;
            }

            foreach (string s in separatedName)
            {
                if (s.Count() < 2 && !s.IsWhiteSpace())
                {
                    return true;
                }
            }

            // Invalid symbol checker
            if (!Regex.IsMatch(fullName, @"^[a-zA-Z\s,\.\-]+$"))
            {
                return true;
            }

            // Repeating character checker
            if (Regex.IsMatch(fullName, @"(.)\1{3,}"))
            {
                return true;
            }

            // Spam checker
            if (Regex.IsMatch(fullName, @"(?i)[bcdfghjklmnpqrstvwxz]{6,}"))
            {
                return true;
            }

            // If it passes all tests, it looks like a real name
            return false;
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

            if (IsNameInValid(txtFullName.Text.Trim()))
            {
                MessageBox.Show(
                "Invalid Name Format!\n\n" +
                "Please use this format: Last Name, First Name Middle Initial optional\n\n" +
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
            if (dtpDOB.Format == DateTimePickerFormat.Custom)
            {
                MessageBox.Show("Please select a Date of Birth!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpDOB.Focus();
                return false;
            }

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
        private async Task<bool> ValidateEmail(bool isUpdate = false)
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

            // Self explanatory naman, checks email pattern, prevents g@com
            string emailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

            if (!System.Text.RegularExpressions.Regex.IsMatch(email, emailPattern))
            {
                MessageBox.Show("Please enter a valid email address!\n\nExample: student@domain.com",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

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

            int? excemptedId = isUpdate
                ? studentID 
                : (int?)null;

            // Check for duplicate email (skip during update if it's the same student)

            if (await IsEmailDuplicate(email, excemptedId))
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
        private async Task<bool> ValidatePhone(bool isUpdate = false)
        {
            if (string.IsNullOrWhiteSpace(mtbPhone.Text))
            {
                MessageBox.Show("Phone number is required!", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbPhone.Focus();
                return false;
            }

            // Extract only digits for validation
            string cleanPhone = mtbPhone.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace("+", "").Replace(" ", "").Replace(" ", "");

            if (cleanPhone[2] != '9')
            {
                MessageBox.Show("Please enter a valid phone number that starts with 9",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbPhone.Focus();
                return false;
            }

            if (cleanPhone.Length < 12)
            {

                lblSRS.Text = cleanPhone;
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

            int? excemptedId = isUpdate
            ? studentID
            : (int?)null;

            if (await IsPhoneDuplicate(mtbPhone.Text.Trim(), excemptedId))
            {
                MessageBox.Show($"Phone number '{mtbPhone.Text}' is already used by another student!\n\nPlease use a different phone number.",
                    "Duplicate Phone Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbPhone.Focus();
                mtbPhone.SelectAll();
                return false;
            }

            return true;
        }

        private async Task<bool> ValidateAllFields(bool isUpdate = false)
        {
            if (!ValidateStudentID()) return false;
            if (!ValidateFullName()) return false;
            if (!ValidateDOB()) return false;
            if (!ValidateGender()) return false;
            if (!ValidateCourse()) return false;
            if (!ValidateYearLevel()) return false;
            if (!ValidateSection()) return false;
            if (!await ValidateEmail(isUpdate)) return false;
            if (!await ValidatePhone(isUpdate)) return false;

            return true;
        }

        // ========== UI HELPERS ==========

        private void ClearForm()
        {
            // Clear info
            studentID = null;
            txtStudentID.Text = "";
            txtStudentID.BackColor = Color.White;
            txtFullName.Text = "";
            txtFullName.BackColor = Color.White;
            txtEmail.Text = "";
            txtEmail.BackColor = Color.White;

            // Clear phone
            mtbPhone.Text = "";

            // Reset comboboxes to default selection (placeholder)
            cmbGender.SelectedIndex = 0;   // "-- Select Gender --"
            cmbCourse.SelectedIndex = 0;    // "-- Select Course --"
            cmbYear.SelectedIndex = 0;      // "-- Select Year --"
            cmbSection.SelectedIndex = 0;   // "-- Select Section --" (NEW)

            // Reset DatePicker
            dtpDOB.Format = DateTimePickerFormat.Custom;
            dtpDOB.CustomFormat = " 'Select Date' ";

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
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to add this student?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            if (!await ValidateAllFields(isUpdate: false))
            {
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    await conn.OpenAsync();

                    // Check for duplicate student ID
                    string checkIdQuery = "SELECT COUNT(*) FROM students WHERE student_number = @sn";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkIdQuery, conn))
                    {
                        string studentIdNumber = txtStudentID.Text.Trim();
                        checkCmd.Parameters.AddWithValue("@sn", studentIdNumber);

                        object result = await checkCmd.ExecuteScalarAsync();
                        int count = Convert.ToInt32(result);
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

                        object result = await checkCmd.ExecuteScalarAsync();
                        int count = Convert.ToInt32(result);
                        if (count > 0)
                        {
                            MessageBox.Show("A student with this exact Name and Date of Birth already exists!",
                                            "Duplicate Student", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }



                    string insertQuery = "INSERT INTO students (student_number, full_name, date_of_birth, gender, course, year_level, section, email, phone) " +
                                         "VALUES (@sn, @name, @dob, @gender, @course, @year, @section, @email, @phone)";

                    using (MySqlCommand cmd = new MySqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@sn", txtStudentID.Text.Trim());
                        cmd.Parameters.AddWithValue("@name", txtFullName.Text.Trim());
                        cmd.Parameters.AddWithValue("@dob", dtpDOB.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@gender", cmbGender.Text);
                        cmd.Parameters.AddWithValue("@course", cmbCourse.Text);
                        cmd.Parameters.AddWithValue("@year", int.Parse(cmbYear.Text));
                        cmd.Parameters.AddWithValue("@section", int.Parse(cmbSection.Text));
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim().ToLower());
                        cmd.Parameters.AddWithValue("@phone", mtbPhone.Text);

                        await cmd.ExecuteNonQueryAsync();

                        MessageBox.Show($"Student record added successfully!\n\nStudent ID: {txtStudentID.Text}",
                            "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        await LoadGridData();
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
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            // Only here we show error messages
            //if (!ValidateAllFields(isUpdate: false))
            //{
            //    return;  // Validation failed with message box
            //}

            if (txtStudentID.Text == "2025-00126-SM-0" || string.IsNullOrWhiteSpace(txtStudentID.Text))
            {
                MessageBox.Show("Please select a record to update from the table.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string updatedStudentID = txtStudentID.Text != txtStudentID.Tag?.ToString() ? txtStudentID.Text : txtStudentID.Tag?.ToString();

            if (!await ValidateAllFields(isUpdate: true))
            {
                return;
            }

            if (!CheckIfUpdated(tableLayoutPanel1))
            {
                MessageBox.Show("There are no updates to make",
                    "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Are you sure you want to update this record?",
                "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            string query = "UPDATE students SET student_number = @sn, full_name = @name, date_of_birth = @dob, gender = @gender, " +
                           "course = @course, year_level = @year, email = @email, phone = @phone " +
                           $"WHERE student_id = @id";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("id", studentID);
                        cmd.Parameters.AddWithValue("sn", updatedStudentID);
                        cmd.Parameters.AddWithValue("@name", txtFullName.Text.Trim());
                        cmd.Parameters.AddWithValue("@dob", dtpDOB.Value.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@gender", cmbGender.Text);
                        cmd.Parameters.AddWithValue("@course", cmbCourse.Text);
                        cmd.Parameters.AddWithValue("@year", int.Parse(cmbYear.Text));
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim().ToLower());
                        cmd.Parameters.AddWithValue("@phone", mtbPhone.Text);

                        await conn.OpenAsync();
                        int rowsAffected = await cmd.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Student record updated successfully!", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await LoadGridData();
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
        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count <= 0)
            {
                MessageBox.Show("You need to select the student to delete", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //if (txtStudentID.Text == "2025-00126-SM-0" || string.IsNullOrWhiteSpace(txtStudentID.Text))
            //{
            //    MessageBox.Show("Please select a record to delete from the table.",
            //        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

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
                            cmd.Parameters.AddWithValue("@id", studentID);

                            await conn.OpenAsync();
                            int rowsAffected = await cmd.ExecuteNonQueryAsync();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Student record deleted successfully.", "Deleted",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                                await LoadGridData();
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
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadGridData();
            ClearForm();
        }

        // ========== EVENT HANDLERS ==========

        // DATA GRID VIEW SELECTION
        private void dgvStudents_SelectionChanged(object sender, EventArgs e)
        {
            ClearForm();
            if (dgvStudents.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvStudents.SelectedRows[0];

                studentID = (int)row.Cells["student_id"].Value;
                string studentIdNumber = Convert.ToString(row.Cells["Student Number"].Value);
                txtStudentID.Text = studentIdNumber;
                txtStudentID.Tag = studentIdNumber;

                string fullName = row.Cells["Full Name"].Value.ToString();
                txtFullName.Text = fullName; 
                txtFullName.Tag = fullName;

                dtpDOB.Format = DateTimePickerFormat.Short;
                DateTime date = Convert.ToDateTime(row.Cells["Date of Birth"].Value);
                dtpDOB.Value = date;
                dtpDOB.Tag = date;

                string gender = row.Cells["Gender"].Value.ToString();
                if (cmbGender.Items.Contains(gender))
                {
                    cmbGender.SelectedItem = gender;
                }
                cmbGender.Tag = gender;

                string course = row.Cells["Course"].Value.ToString();
                cmbCourse.SelectedItem = course; 
                cmbCourse.Tag = course;

                string year = row.Cells["Year"].Value.ToString();
                cmbYear.SelectedItem = year; 
                cmbYear.Tag = year;

                string section = row.Cells["Section"].Value.ToString();  // Added
                cmbSection.SelectedItem = section;
                cmbSection.Tag = section;

                string email = row.Cells["Email"].Value.ToString();
                txtEmail.Text = email; 
                txtEmail.Tag = email;

                string phone = row.Cells["Phone"].Value.ToString();
                mtbPhone.Text = phone;
                mtbPhone.Tag = phone;
            }
        }

        // FORM LOAD 
        private async void frmStudentRec_Load(object sender, EventArgs e)
        {
            dtpDOB.ValueChanged += dtpDOB_ValueChanged;
            txtStudentID.Enter += ClearHiglight;
            txtFullName.Enter += ClearHiglight;
            txtEmail.Enter += ClearHiglight;

            mtbPhone.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Space)
                {
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            };

            mtbPhone.KeyUp += (s, e) => EnforcePhoneCursor();

            mtbPhone.Click += (s, e) => EnforcePhoneCursor();

            mtbPhone.Enter += (s, e) => this.BeginInvoke(new Action(EnforcePhoneCursor));

            mtbPhone.Click += (s, e) => MoveMaskedCursorToStart(mtbPhone);
            mtbPhone.Enter += (s, e) => MoveMaskedCursorToStart(mtbPhone);

            await LoadGridData();
            this.ShowIcon = false;
        }

        private void frmStudentRec_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show("Are you sure you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void ClearHiglight(object sender, EventArgs e)
        {
            if (sender is TextBox txt)
            {
                txt.BackColor = Color.White;
            }
        }

        private void AdjustComboBoxDropdownWidth(ComboBox cmb)
        {
            // Self Explanatory na naman
            int maxWidth = cmb.Width;

            foreach (var item in cmb.Items)
            {
                int itemWidth = TextRenderer.MeasureText(item.ToString(), cmb.Font).Width;
                if (itemWidth > maxWidth)
                {
                    maxWidth = itemWidth;
                }
            }

            cmb.DropDownWidth = maxWidth + SystemInformation.VerticalScrollBarWidth;
        }

        private void dtpDOB_ValueChanged(object sender, EventArgs e)
        {
            // Changes dtp to be normal
            if (dtpDOB.Format == DateTimePickerFormat.Custom)
            {
                dtpDOB.Format = DateTimePickerFormat.Short;
            }
        }

        private bool CheckIfUpdated(TableLayoutPanel tableLayoutPanel)
        {
            foreach (Control c in tableLayoutPanel.Controls)
            {
                if (c is TextBox || c is ComboBox || c is MaskedTextBox)
                {
                    string originalValue = c.Tag?.ToString() ?? "";

                    if (c.Text != originalValue)
                    {
                        return true;
                    }
                }
                else if (c is DateTimePicker dtp)
                {
                    if (dtp.Tag != null && DateTime.TryParse(dtp.Tag.ToString(), out DateTime originalDate))
                    {
                        if (dtp.Value.Date != originalDate.Date)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void MoveMaskedCursorToStart(MaskedTextBox mtb)
        {
            this.BeginInvoke(new Action(() =>
            {
                if (mtb.MaskedTextProvider != null && mtb.MaskedTextProvider.AssignedEditPositionCount == 0)
                {
                    int firstEditIndex = mtb.MaskedTextProvider.FindEditPositionFrom(0, true);
                    if (firstEditIndex != -1)
                    {
                        mtb.SelectionStart = firstEditIndex;
                    }
                    else
                    {
                        mtb.SelectionStart = 0;
                    }
                }
            }));
        }

        private void EnforcePhoneCursor()
        {
            int startIndex = mtbPhone.Mask.IndexOf('0');
            if (startIndex == -1) startIndex = 0;

            if (mtbPhone.SelectionLength > 0) return;

            if (mtbPhone.SelectionStart < startIndex)
            {
                mtbPhone.SelectionStart = startIndex;
            }
        }
    }
}