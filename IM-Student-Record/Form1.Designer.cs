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
            label1 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            cmbSection = new ComboBox();
            lblSection = new Label();
            label2 = new Label();
            toolTip1 = new ToolTip(components);
            tableLayoutPanel2 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)dgvStudents).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbSRS).BeginInit();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // txtStudentID
            // 
            txtStudentID.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtStudentID.Font = new Font("Segoe UI", 11.25F);
            txtStudentID.Location = new Point(158, 11);
            txtStudentID.MaxLength = 15;
            txtStudentID.Name = "txtStudentID";
            txtStudentID.Size = new Size(391, 27);
            txtStudentID.TabIndex = 1;
            // 
            // txtFullName
            // 
            txtFullName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtFullName.Font = new Font("Segoe UI", 11.25F);
            txtFullName.Location = new Point(158, 61);
            txtFullName.MaxLength = 100;
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(391, 27);
            txtFullName.TabIndex = 3;
            // 
            // dtpDOB
            // 
            dtpDOB.Anchor = AnchorStyles.Left;
            dtpDOB.Font = new Font("Segoe UI", 11.25F);
            dtpDOB.Format = DateTimePickerFormat.Short;
            dtpDOB.Location = new Point(158, 111);
            dtpDOB.Name = "dtpDOB";
            dtpDOB.Size = new Size(169, 27);
            dtpDOB.TabIndex = 6;
            // 
            // cmbGender
            // 
            cmbGender.Anchor = AnchorStyles.Left;
            cmbGender.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbGender.Font = new Font("Segoe UI", 11.25F);
            cmbGender.Items.AddRange(new object[] { "Male", "Female", "Non-binary" });
            cmbGender.Location = new Point(158, 161);
            cmbGender.Name = "cmbGender";
            cmbGender.Size = new Size(168, 28);
            cmbGender.TabIndex = 4;
            // 
            // cmbCourse
            // 
            cmbCourse.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.SetColumnSpan(cmbCourse, 3);
            cmbCourse.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCourse.Font = new Font("Segoe UI", 11.25F);
            cmbCourse.Items.AddRange(new object[] { "Bachelor of Science in Accountancy", "Bachelor of Science in Computer Engineering", "Bachelor of Science in Entrepreneurship", "Bachelor of Science in Hospitality Management", "Bachelor of Science in Information Technology", "Bachelor of Secondary Education major in English", "Bachelor of Secondary Education major in Mathematics", "Diploma in Office Management Technology with Specialization in Legal Office Management " });
            cmbCourse.Location = new Point(677, 11);
            cmbCourse.Name = "cmbCourse";
            cmbCourse.Size = new Size(434, 28);
            cmbCourse.TabIndex = 2;
            // 
            // cmbYear
            // 
            cmbYear.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbYear.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbYear.Font = new Font("Segoe UI", 9.75F);
            cmbYear.ItemHeight = 17;
            cmbYear.Items.AddRange(new object[] { "1", "2", "3", "4" });
            cmbYear.Location = new Point(677, 62);
            cmbYear.MaxLength = 1;
            cmbYear.Name = "cmbYear";
            cmbYear.Size = new Size(149, 25);
            cmbYear.TabIndex = 5;
            // 
            // txtEmail
            // 
            txtEmail.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.SetColumnSpan(txtEmail, 3);
            txtEmail.Font = new Font("Segoe UI", 9.75F);
            txtEmail.Location = new Point(677, 112);
            txtEmail.MaxLength = 100;
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(434, 25);
            txtEmail.TabIndex = 7;
            // 
            // dgvStudents
            // 
            dgvStudents.AllowUserToAddRows = false;
            dgvStudents.AllowUserToDeleteRows = false;
            dgvStudents.AllowUserToResizeColumns = false;
            dgvStudents.AllowUserToResizeRows = false;
            dgvStudents.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudents.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvStudents.BackgroundColor = Color.White;
            dgvStudents.BorderStyle = BorderStyle.None;
            dgvStudents.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.SteelBlue;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.SteelBlue;
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvStudents.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvStudents.ColumnHeadersHeight = 40;
            dgvStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.GradientInactiveCaption;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.Padding = new Padding(5);
            dataGridViewCellStyle2.SelectionBackColor = Color.LightSteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvStudents.DefaultCellStyle = dataGridViewCellStyle2;
            dgvStudents.EnableHeadersVisualStyles = false;
            dgvStudents.GridColor = Color.Gainsboro;
            dgvStudents.Location = new Point(12, 411);
            dgvStudents.MinimumSize = new Size(1016, 237);
            dgvStudents.MultiSelect = false;
            dgvStudents.Name = "dgvStudents";
            dgvStudents.ReadOnly = true;
            dgvStudents.RowHeadersVisible = false;
            dgvStudents.RowHeadersWidth = 51;
            dgvStudents.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvStudents.RowsDefaultCellStyle = dataGridViewCellStyle3;
            dgvStudents.RowTemplate.Height = 35;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.Size = new Size(1128, 237);
            dgvStudents.TabIndex = 9;
            dgvStudents.SelectionChanged += dgvStudents_SelectionChanged;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAdd.BackColor = Color.SteelBlue;
            btnAdd.Font = new Font("Segoe UI", 7.68000031F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAdd.ForeColor = Color.White;
            btnAdd.Image = Properties.Resources.plus_8_16;
            btnAdd.Location = new Point(912, 6);
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
            btnUpdate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnUpdate.BackColor = Color.CornflowerBlue;
            btnUpdate.FlatAppearance.BorderColor = Color.SteelBlue;
            btnUpdate.FlatAppearance.BorderSize = 2;
            btnUpdate.Font = new Font("Segoe UI", 7.68000031F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Image = Properties.Resources.edit_property_16__1_;
            btnUpdate.ImageAlign = ContentAlignment.MiddleRight;
            btnUpdate.Location = new Point(3, 3);
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
            btnDelete.BackColor = Color.SlateBlue;
            btnDelete.Font = new Font("Segoe UI", 7.68000031F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDelete.ForeColor = Color.White;
            btnDelete.Image = Properties.Resources.delete_16__1_;
            btnDelete.Location = new Point(10, 368);
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
            btnRefresh.Font = new Font("Segoe UI", 7.68000031F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnRefresh.ForeColor = Color.SteelBlue;
            btnRefresh.Image = Properties.Resources.sinchronize_16;
            btnRefresh.Location = new Point(195, 3);
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
            lblStudentID.Anchor = AnchorStyles.Left;
            lblStudentID.AutoSize = true;
            lblStudentID.Font = new Font("Segoe UI", 12F);
            lblStudentID.ForeColor = Color.SteelBlue;
            lblStudentID.Location = new Point(3, 14);
            lblStudentID.Name = "lblStudentID";
            lblStudentID.Size = new Size(85, 21);
            lblStudentID.TabIndex = 21;
            lblStudentID.Text = "Student ID:";
            // 
            // lblFullName
            // 
            lblFullName.Anchor = AnchorStyles.Left;
            lblFullName.AutoSize = true;
            lblFullName.Font = new Font("Segoe UI", 12F);
            lblFullName.ForeColor = Color.SteelBlue;
            lblFullName.Location = new Point(3, 64);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(84, 21);
            lblFullName.TabIndex = 20;
            lblFullName.Text = "Full Name:";
            // 
            // lblDOB
            // 
            lblDOB.Anchor = AnchorStyles.Left;
            lblDOB.AutoSize = true;
            lblDOB.Font = new Font("Segoe UI", 12F);
            lblDOB.ForeColor = Color.SteelBlue;
            lblDOB.Location = new Point(3, 114);
            lblDOB.Name = "lblDOB";
            lblDOB.Size = new Size(100, 21);
            lblDOB.TabIndex = 19;
            lblDOB.Text = "Date of Birth:";
            lblDOB.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGender
            // 
            lblGender.Anchor = AnchorStyles.Left;
            lblGender.AutoSize = true;
            lblGender.Font = new Font("Segoe UI", 12F);
            lblGender.ForeColor = Color.SteelBlue;
            lblGender.Location = new Point(3, 164);
            lblGender.Name = "lblGender";
            lblGender.Size = new Size(64, 21);
            lblGender.TabIndex = 18;
            lblGender.Text = "Gender:";
            // 
            // lblCourse
            // 
            lblCourse.Anchor = AnchorStyles.Left;
            lblCourse.AutoSize = true;
            lblCourse.Font = new Font("Segoe UI", 12F);
            lblCourse.ForeColor = Color.SteelBlue;
            lblCourse.Location = new Point(555, 14);
            lblCourse.Name = "lblCourse";
            lblCourse.Size = new Size(62, 21);
            lblCourse.TabIndex = 17;
            lblCourse.Text = "Course:";
            // 
            // lblYear
            // 
            lblYear.Anchor = AnchorStyles.Left;
            lblYear.AutoSize = true;
            lblYear.Font = new Font("Segoe UI", 12F);
            lblYear.ForeColor = Color.SteelBlue;
            lblYear.Location = new Point(555, 64);
            lblYear.Name = "lblYear";
            lblYear.Size = new Size(83, 21);
            lblYear.TabIndex = 16;
            lblYear.Text = "Year Level:";
            // 
            // lblEmail
            // 
            lblEmail.Anchor = AnchorStyles.Left;
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Segoe UI", 12F);
            lblEmail.ForeColor = Color.SteelBlue;
            lblEmail.Location = new Point(555, 114);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(51, 21);
            lblEmail.TabIndex = 15;
            lblEmail.Text = "Email:";
            // 
            // lblPhone
            // 
            lblPhone.Anchor = AnchorStyles.Left;
            lblPhone.AutoSize = true;
            lblPhone.Font = new Font("Segoe UI", 12F);
            lblPhone.ForeColor = Color.SteelBlue;
            lblPhone.Location = new Point(555, 164);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(57, 21);
            lblPhone.TabIndex = 14;
            lblPhone.Text = "Phone:";
            // 
            // mtbPhone
            // 
            mtbPhone.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.SetColumnSpan(mtbPhone, 3);
            mtbPhone.Font = new Font("Segoe UI", 11.25F);
            mtbPhone.Location = new Point(677, 161);
            mtbPhone.Mask = "(+63)000-000-0000";
            mtbPhone.Name = "mtbPhone";
            mtbPhone.PromptChar = ' ';
            mtbPhone.Size = new Size(434, 27);
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
            lblSRS.Font = new Font("Segoe UI", 17.28F, FontStyle.Bold);
            lblSRS.ForeColor = Color.SteelBlue;
            lblSRS.Location = new Point(89, 12);
            lblSRS.Name = "lblSRS";
            lblSRS.Size = new Size(276, 32);
            lblSRS.TabIndex = 24;
            lblSRS.Text = "Student Record System";
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.White;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Controls.Add(btnAdd);
            panel1.Font = new Font("Segoe UI Semibold", 7.68000031F, FontStyle.Bold, GraphicsUnit.Point, 0);
            panel1.ForeColor = Color.SteelBlue;
            panel1.Location = new Point(12, 105);
            panel1.Name = "panel1";
            panel1.Size = new Size(1132, 251);
            panel1.TabIndex = 26;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.120002F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.SteelBlue;
            label1.Location = new Point(12, 10);
            label1.Name = "label1";
            label1.Size = new Size(103, 17);
            label1.TabIndex = 27;
            label1.Text = "Student Details";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 6;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13.9041634F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35.6637878F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10.9976435F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13.9041634F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.562451F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.8892384F));
            tableLayoutPanel1.Controls.Add(cmbSection, 5, 1);
            tableLayoutPanel1.Controls.Add(lblStudentID, 0, 0);
            tableLayoutPanel1.Controls.Add(mtbPhone, 3, 3);
            tableLayoutPanel1.Controls.Add(lblSection, 4, 1);
            tableLayoutPanel1.Controls.Add(lblFullName, 0, 1);
            tableLayoutPanel1.Controls.Add(txtEmail, 3, 2);
            tableLayoutPanel1.Controls.Add(lblDOB, 0, 2);
            tableLayoutPanel1.Controls.Add(cmbGender, 1, 3);
            tableLayoutPanel1.Controls.Add(lblGender, 0, 3);
            tableLayoutPanel1.Controls.Add(cmbYear, 3, 1);
            tableLayoutPanel1.Controls.Add(cmbCourse, 3, 0);
            tableLayoutPanel1.Controls.Add(lblCourse, 2, 0);
            tableLayoutPanel1.Controls.Add(lblEmail, 2, 2);
            tableLayoutPanel1.Controls.Add(lblPhone, 2, 3);
            tableLayoutPanel1.Controls.Add(lblYear, 2, 1);
            tableLayoutPanel1.Controls.Add(txtFullName, 1, 1);
            tableLayoutPanel1.Controls.Add(dtpDOB, 1, 2);
            tableLayoutPanel1.Controls.Add(txtStudentID, 1, 0);
            tableLayoutPanel1.Location = new Point(14, 46);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel1.Size = new Size(1114, 200);
            tableLayoutPanel1.TabIndex = 30;
            // 
            // cmbSection
            // 
            cmbSection.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            cmbSection.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSection.Font = new Font("Segoe UI", 9.75F);
            cmbSection.IntegralHeight = false;
            cmbSection.Items.AddRange(new object[] { "1", "2", "3", "4" });
            cmbSection.Location = new Point(927, 62);
            cmbSection.MaxLength = 1;
            cmbSection.Name = "cmbSection";
            cmbSection.Size = new Size(184, 25);
            cmbSection.TabIndex = 28;
            // 
            // lblSection
            // 
            lblSection.Anchor = AnchorStyles.Left;
            lblSection.AutoSize = true;
            lblSection.Font = new Font("Segoe UI", 12F);
            lblSection.ForeColor = Color.SteelBlue;
            lblSection.Location = new Point(832, 64);
            lblSection.Name = "lblSection";
            lblSection.Size = new Size(64, 21);
            lblSection.TabIndex = 29;
            lblSection.Text = "Section:";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 7.68000031F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(100, 60);
            label2.Name = "label2";
            label2.Size = new Size(294, 13);
            label2.TabIndex = 27;
            label2.Text = "Manages student records (Create, Read, Update, Delete)";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(btnUpdate, 0, 0);
            tableLayoutPanel2.Controls.Add(btnRefresh, 1, 0);
            tableLayoutPanel2.Location = new Point(806, 364);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(335, 41);
            tableLayoutPanel2.TabIndex = 28;
            // 
            // frmStudentRec
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(1153, 664);
            Controls.Add(tableLayoutPanel2);
            Controls.Add(label2);
            Controls.Add(panel1);
            Controls.Add(lblSRS);
            Controls.Add(pbSRS);
            Controls.Add(btnDelete);
            Controls.Add(dgvStudents);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(1169, 703);
            Name = "frmStudentRec";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Student Record System";
            FormClosing += frmStudentRec_FormClosing;
            Load += frmStudentRec_Load;
            ((System.ComponentModel.ISupportInitialize)dgvStudents).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbSRS).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
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
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
    }
}