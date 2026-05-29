namespace IM_Student_Record
{
    partial class frmStudentRec
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStudentRec));
            txtStudentID = new TextBox();
            txtFullName = new TextBox();
            dtpDOB = new DateTimePicker();
            cmbGender = new ComboBox();
            cmbCourse = new ComboBox();
            cmbYear = new ComboBox();
            txtEmail = new TextBox();
            dgvStudents = new DataGridView();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnRefresh = new Button();
            lblStudentID = new Label();
            lblFullName = new Label();
            lblDOB = new Label();
            lblGender = new Label();
            lblCourse = new Label();
            lblYear = new Label();
            lblEmail = new Label();
            lblPhone = new Label();
            mtbPhone = new MaskedTextBox();
            pbSRS = new PictureBox();
            lblSRS = new Label();
            panel1 = new Panel();
            cmbSection = new ComboBox();
            lblSection = new Label();
            label1 = new Label();
            label2 = new Label();
            toolTip1 = new ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)dgvStudents).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbSRS).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // txtStudentID
            // 
            txtStudentID.Font = new Font("Segoe UI", 12F);
            txtStudentID.Location = new Point(163, 53);
            txtStudentID.Name = "txtStudentID";
            txtStudentID.Size = new Size(411, 29);
            txtStudentID.TabIndex = 1;
            // 
            // txtFullName
            // 
            txtFullName.Font = new Font("Segoe UI", 12F);
            txtFullName.Location = new Point(163, 94);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(411, 29);
            txtFullName.TabIndex = 3;
            // 
            // dtpDOB
            // 
            dtpDOB.Font = new Font("Segoe UI", 12F);
            dtpDOB.Format = DateTimePickerFormat.Short;
            dtpDOB.Location = new Point(163, 138);
            dtpDOB.Name = "dtpDOB";
            dtpDOB.Size = new Size(411, 29);
            dtpDOB.TabIndex = 6;
            // 
            // cmbGender
            // 
            cmbGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGender.Font = new Font("Segoe UI", 12F);
            cmbGender.Items.AddRange(new object[] { "Male", "Female", "Non-Binary" });
            cmbGender.Location = new Point(163, 181);
            cmbGender.Name = "cmbGender";
            cmbGender.Size = new Size(199, 29);
            cmbGender.TabIndex = 4;
            // 
            // cmbCourse
            // 
            cmbCourse.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCourse.Font = new Font("Segoe UI", 12F);
            cmbCourse.Items.AddRange(new object[] { "Bachelor of Science in Accountancy", "Bachelor of Science in Computer Engineering", "Bachelor of Science in Entrepreneurship", "Bachelor of Science in Hospitality Management", "Bachelor of Science in Information Technology", "Bachelor of Secondary Education major in English", "Bachelor of Secondary Education major in Mathematics", "Diploma in Office Management Technology with Specialization in Legal Office Management " });
            cmbCourse.Location = new Point(697, 55);
            cmbCourse.Name = "cmbCourse";
            cmbCourse.Size = new Size(431, 29);
            cmbCourse.TabIndex = 2;
            // 
            // cmbYear
            // 
            cmbYear.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbYear.Font = new Font("Segoe UI", 12F);
            cmbYear.Items.AddRange(new object[] { "1", "2", "3", "4" });
            cmbYear.Location = new Point(697, 96);
            cmbYear.Name = "cmbYear";
            cmbYear.Size = new Size(160, 29);
            cmbYear.TabIndex = 5;
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Segoe UI", 12F);
            txtEmail.Location = new Point(697, 138);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(346, 29);
            txtEmail.TabIndex = 7;
            // 
            // dgvStudents
            // 
            dgvStudents.AllowUserToAddRows = false;
            dgvStudents.AllowUserToDeleteRows = false;
            dgvStudents.AllowUserToResizeColumns = false;
            dgvStudents.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.GhostWhite;
            dgvStudents.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvStudents.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudents.BackgroundColor = Color.White;
            dgvStudents.BorderStyle = BorderStyle.None;
            dgvStudents.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.SteelBlue;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.SteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvStudents.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvStudents.ColumnHeadersHeight = 40;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.Padding = new Padding(5);
            dataGridViewCellStyle3.SelectionBackColor = Color.LightSteelBlue;
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvStudents.DefaultCellStyle = dataGridViewCellStyle3;
            dgvStudents.EnableHeadersVisualStyles = false;
            dgvStudents.GridColor = Color.Gainsboro;
            dgvStudents.Location = new Point(12, 411);
            dgvStudents.MultiSelect = false;
            dgvStudents.Name = "dgvStudents";
            dgvStudents.ReadOnly = true;
            dgvStudents.RowHeadersVisible = false;
            dgvStudents.RowTemplate.Height = 35;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.Size = new Size(1184, 263);
            dgvStudents.TabIndex = 9;
            dgvStudents.SelectionChanged += dgvStudents_SelectionChanged;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAdd.BackColor = Color.SteelBlue;
            btnAdd.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAdd.ForeColor = Color.White;
            btnAdd.Image = Properties.Resources.plus_8_16;
            btnAdd.Location = new Point(950, 6);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(218, 35);
            btnAdd.TabIndex = 10;
            btnAdd.Text = " Add New Student";
            btnAdd.TextAlign = ContentAlignment.MiddleRight;
            btnAdd.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnUpdate.BackColor = Color.CornflowerBlue;
            btnUpdate.FlatAppearance.BorderColor = Color.SteelBlue;
            btnUpdate.FlatAppearance.BorderSize = 2;
            btnUpdate.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Image = Properties.Resources.edit_property_16__1_;
            btnUpdate.ImageAlign = ContentAlignment.MiddleRight;
            btnUpdate.Location = new Point(852, 370);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(186, 35);
            btnUpdate.TabIndex = 12;
            btnUpdate.Text = "Update Record";
            btnUpdate.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDelete.BackColor = Color.SlateBlue;
            btnDelete.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDelete.ForeColor = Color.White;
            btnDelete.Image = Properties.Resources.delete_16__1_;
            btnDelete.Location = new Point(10, 370);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(180, 35);
            btnDelete.TabIndex = 11;
            btnDelete.Text = "Delete Record";
            btnDelete.TextAlign = ContentAlignment.MiddleRight;
            btnDelete.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnRefresh.BackColor = Color.LightSteelBlue;
            btnRefresh.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRefresh.ForeColor = Color.SteelBlue;
            btnRefresh.Image = Properties.Resources.sinchronize_16;
            btnRefresh.Location = new Point(1059, 370);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(137, 35);
            btnRefresh.TabIndex = 13;
            btnRefresh.Text = "Refresh";
            btnRefresh.TextAlign = ContentAlignment.MiddleRight;
            btnRefresh.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnRefresh.UseVisualStyleBackColor = false;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // lblStudentID
            // 
            lblStudentID.AutoSize = true;
            lblStudentID.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblStudentID.ForeColor = Color.SteelBlue;
            lblStudentID.Location = new Point(51, 58);
            lblStudentID.Name = "lblStudentID";
            lblStudentID.Size = new Size(92, 21);
            lblStudentID.TabIndex = 21;
            lblStudentID.Text = "Student ID:";
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblFullName.ForeColor = Color.SteelBlue;
            lblFullName.Location = new Point(50, 99);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(86, 21);
            lblFullName.TabIndex = 20;
            lblFullName.Text = "Full Name:";
            // 
            // lblDOB
            // 
            lblDOB.AutoSize = true;
            lblDOB.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblDOB.ForeColor = Color.SteelBlue;
            lblDOB.Location = new Point(51, 141);
            lblDOB.Name = "lblDOB";
            lblDOB.Size = new Size(107, 21);
            lblDOB.TabIndex = 19;
            lblDOB.Text = "Date of Birth:";
            lblDOB.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGender
            // 
            lblGender.AutoSize = true;
            lblGender.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblGender.ForeColor = Color.SteelBlue;
            lblGender.Location = new Point(51, 184);
            lblGender.Name = "lblGender";
            lblGender.Size = new Size(69, 21);
            lblGender.TabIndex = 18;
            lblGender.Text = "Gender:";
            // 
            // lblCourse
            // 
            lblCourse.AutoSize = true;
            lblCourse.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblCourse.ForeColor = Color.SteelBlue;
            lblCourse.Location = new Point(598, 58);
            lblCourse.Name = "lblCourse";
            lblCourse.Size = new Size(65, 21);
            lblCourse.TabIndex = 17;
            lblCourse.Text = "Course:";
            // 
            // lblYear
            // 
            lblYear.AutoSize = true;
            lblYear.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblYear.ForeColor = Color.SteelBlue;
            lblYear.Location = new Point(598, 99);
            lblYear.Name = "lblYear";
            lblYear.Size = new Size(87, 21);
            lblYear.TabIndex = 16;
            lblYear.Text = "Year Level:";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblEmail.ForeColor = Color.SteelBlue;
            lblEmail.Location = new Point(598, 141);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(52, 21);
            lblEmail.TabIndex = 15;
            lblEmail.Text = "Email:";
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblPhone.ForeColor = Color.SteelBlue;
            lblPhone.Location = new Point(598, 184);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(60, 21);
            lblPhone.TabIndex = 14;
            lblPhone.Text = "Phone:";
            // 
            // mtbPhone
            // 
            mtbPhone.Font = new Font("Segoe UI", 12F);
            mtbPhone.Location = new Point(697, 181);
            mtbPhone.Mask = "(+63)000-000-0000";
            mtbPhone.Name = "mtbPhone";
            mtbPhone.PromptChar = ' ';
            mtbPhone.Size = new Size(346, 29);
            mtbPhone.TabIndex = 22;
            // 
            // pbSRS
            // 
            pbSRS.Image = Properties.Resources.graduation_cap_64;
            pbSRS.Location = new Point(12, 12);
            pbSRS.Name = "pbSRS";
            pbSRS.Size = new Size(82, 75);
            pbSRS.SizeMode = PictureBoxSizeMode.CenterImage;
            pbSRS.TabIndex = 23;
            pbSRS.TabStop = false;
            // 
            // lblSRS
            // 
            lblSRS.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblSRS.AutoSize = true;
            lblSRS.Font = new Font("Segoe UI", 27F, FontStyle.Bold);
            lblSRS.ForeColor = Color.SteelBlue;
            lblSRS.Location = new Point(89, 12);
            lblSRS.Name = "lblSRS";
            lblSRS.Size = new Size(409, 48);
            lblSRS.TabIndex = 24;
            lblSRS.Text = "Student Record System";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.White;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(cmbSection);
            panel1.Controls.Add(lblSection);
            panel1.Controls.Add(lblStudentID);
            panel1.Controls.Add(lblFullName);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(lblDOB);
            panel1.Controls.Add(cmbGender);
            panel1.Controls.Add(mtbPhone);
            panel1.Controls.Add(btnAdd);
            panel1.Controls.Add(cmbCourse);
            panel1.Controls.Add(lblGender);
            panel1.Controls.Add(txtFullName);
            panel1.Controls.Add(lblCourse);
            panel1.Controls.Add(cmbYear);
            panel1.Controls.Add(lblYear);
            panel1.Controls.Add(txtStudentID);
            panel1.Controls.Add(lblEmail);
            panel1.Controls.Add(txtEmail);
            panel1.Controls.Add(lblPhone);
            panel1.Controls.Add(dtpDOB);
            panel1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            panel1.ForeColor = Color.SteelBlue;
            panel1.Location = new Point(12, 105);
            panel1.Name = "panel1";
            panel1.Size = new Size(1184, 247);
            panel1.TabIndex = 26;
            // 
            // cmbSection
            // 
            cmbSection.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSection.Font = new Font("Segoe UI", 12F);
            cmbSection.Items.AddRange(new object[] { "1", "2", "3", "4" });
            cmbSection.Location = new Point(966, 96);
            cmbSection.Name = "cmbSection";
            cmbSection.Size = new Size(160, 29);
            cmbSection.TabIndex = 28;
            // 
            // lblSection
            // 
            lblSection.AutoSize = true;
            lblSection.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            lblSection.ForeColor = Color.SteelBlue;
            lblSection.Location = new Point(891, 99);
            lblSection.Name = "lblSection";
            lblSection.Size = new Size(69, 21);
            lblSection.TabIndex = 29;
            lblSection.Text = "Section:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.SteelBlue;
            label1.Location = new Point(12, 10);
            label1.Name = "label1";
            label1.Size = new Size(147, 25);
            label1.TabIndex = 27;
            label1.Text = "Student Details";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(100, 60);
            label2.Name = "label2";
            label2.Size = new Size(421, 21);
            label2.TabIndex = 27;
            label2.Text = "Manages student records (Create, Read, Update, Delete)";
            // 
            // frmStudentRec
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(1208, 686);
            Controls.Add(label2);
            Controls.Add(panel1);
            Controls.Add(lblSRS);
            Controls.Add(pbSRS);
            Controls.Add(btnRefresh);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(dgvStudents);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "frmStudentRec";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Student Record System";
            FormClosing += frmStudentRec_FormClosing;
            Load += frmStudentRec_Load;
            ((System.ComponentModel.ISupportInitialize)dgvStudents).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbSRS).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        // Control declarations
        private TextBox txtStudentID;
        private TextBox txtFullName;
        private DateTimePicker dtpDOB;
        private ComboBox cmbGender;  // Changed from TextBox
        private ComboBox cmbCourse;   // Changed from TextBox
        private ComboBox cmbYear;     // Changed from TextBox
        private TextBox txtEmail;
        private DataGridView dgvStudents;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnRefresh;
        private MaskedTextBox mtbPhone;

        // Label declarations
        private Label lblStudentID;
        private Label lblFullName;
        private Label lblDOB;
        private Label lblGender;
        private Label lblCourse;
        private Label lblYear;
        private Label lblEmail;
        private Label lblPhone;
        private PictureBox pbSRS;
        private Label lblSRS;
        private Panel panel1;
        private Label label1;
        private Label label2;
        private ComboBox cmbSection;
        private Label lblSection;
        private ToolTip toolTip1;
    }
}