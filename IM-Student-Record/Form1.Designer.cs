namespace IM_Student_Record
{
    partial class frmStudentRec
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStudentRec));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            txtStudentID = new TextBox();
            cmbCourse = new TextBox();
            txtFullName = new TextBox();
            cmbGender = new TextBox();
            cmbYear = new TextBox();
            dtpDOB = new DateTimePicker();
            txtEmail = new TextBox();
            txtPhone = new TextBox();
            btnAdd = new Button();
            imageList1 = new ImageList(components);
            btnDelete = new Button();
            btnUpdate = new Button();
            btnRefresh = new Button();
            label1 = new Label();
            label2 = new Label();
            panel1 = new Panel();
            panel3 = new Panel();
            pictureBox1 = new PictureBox();
            label3 = new Label();
            groupBox1 = new GroupBox();
            groupBox3 = new GroupBox();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            dgvStudents = new DataGridView();
            groupBox2 = new GroupBox();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStudents).BeginInit();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // txtStudentID
            // 
            txtStudentID.Font = new Font("Segoe UI", 9F);
            txtStudentID.Location = new Point(199, 59);
            txtStudentID.Margin = new Padding(3, 4, 3, 4);
            txtStudentID.Name = "txtStudentID";
            txtStudentID.Size = new Size(258, 27);
            txtStudentID.TabIndex = 1;
            // 
            // cmbCourse
            // 
            cmbCourse.Font = new Font("Segoe UI", 9F);
            cmbCourse.Location = new Point(199, 147);
            cmbCourse.Margin = new Padding(3, 4, 3, 4);
            cmbCourse.Name = "cmbCourse";
            cmbCourse.Size = new Size(258, 27);
            cmbCourse.TabIndex = 2;
            // 
            // txtFullName
            // 
            txtFullName.Font = new Font("Segoe UI", 9F);
            txtFullName.Location = new Point(202, 103);
            txtFullName.Margin = new Padding(3, 4, 3, 4);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(258, 27);
            txtFullName.TabIndex = 3;
            // 
            // cmbGender
            // 
            cmbGender.Font = new Font("Segoe UI", 9F);
            cmbGender.Location = new Point(634, 105);
            cmbGender.Margin = new Padding(3, 4, 3, 4);
            cmbGender.Name = "cmbGender";
            cmbGender.Size = new Size(258, 27);
            cmbGender.TabIndex = 4;
            // 
            // cmbYear
            // 
            cmbYear.Font = new Font("Segoe UI", 9F);
            cmbYear.Location = new Point(199, 191);
            cmbYear.Margin = new Padding(3, 4, 3, 4);
            cmbYear.Name = "cmbYear";
            cmbYear.Size = new Size(258, 27);
            cmbYear.TabIndex = 5;
            // 
            // dtpDOB
            // 
            dtpDOB.CalendarTitleBackColor = Color.DarkBlue;
            dtpDOB.Font = new Font("Segoe UI", 9F);
            dtpDOB.Format = DateTimePickerFormat.Custom;
            dtpDOB.Location = new Point(634, 60);
            dtpDOB.Margin = new Padding(3, 4, 3, 4);
            dtpDOB.Name = "dtpDOB";
            dtpDOB.Size = new Size(258, 27);
            dtpDOB.TabIndex = 6;
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Segoe UI", 9F);
            txtEmail.Location = new Point(634, 150);
            txtEmail.Margin = new Padding(3, 4, 3, 4);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(258, 27);
            txtEmail.TabIndex = 7;
            // 
            // txtPhone
            // 
            txtPhone.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPhone.Location = new Point(634, 195);
            txtPhone.Margin = new Padding(3, 4, 3, 4);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(258, 27);
            txtPhone.TabIndex = 8;
            // 
            // btnAdd
            // 
            btnAdd.Font = new Font("Segoe UI", 9F);
            btnAdd.ForeColor = SystemColors.WindowText;
            btnAdd.ImageAlign = ContentAlignment.MiddleLeft;
            btnAdd.ImageIndex = 0;
            btnAdd.ImageList = imageList1;
            btnAdd.Location = new Point(48, 30);
            btnAdd.Margin = new Padding(3, 4, 3, 4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(143, 44);
            btnAdd.TabIndex = 10;
            btnAdd.Text = "    Add";
            btnAdd.TextAlign = ContentAlignment.MiddleRight;
            btnAdd.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth4Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "Add");
            imageList1.Images.SetKeyName(1, "Update");
            imageList1.Images.SetKeyName(2, "Delete");
            imageList1.Images.SetKeyName(3, "Refresh");
            // 
            // btnDelete
            // 
            btnDelete.Font = new Font("Segoe UI", 9F);
            btnDelete.ForeColor = SystemColors.WindowText;
            btnDelete.ImageAlign = ContentAlignment.MiddleLeft;
            btnDelete.ImageIndex = 2;
            btnDelete.ImageList = imageList1;
            btnDelete.Location = new Point(48, 149);
            btnDelete.Margin = new Padding(3, 4, 3, 4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(143, 44);
            btnDelete.TabIndex = 11;
            btnDelete.Text = "    Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Font = new Font("Segoe UI", 9F);
            btnUpdate.ForeColor = SystemColors.WindowText;
            btnUpdate.ImageAlign = ContentAlignment.MiddleLeft;
            btnUpdate.ImageIndex = 1;
            btnUpdate.ImageList = imageList1;
            btnUpdate.Location = new Point(48, 88);
            btnUpdate.Margin = new Padding(3, 4, 3, 4);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(143, 44);
            btnUpdate.TabIndex = 12;
            btnUpdate.Text = "    Update";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Font = new Font("Segoe UI", 9F);
            btnRefresh.ForeColor = SystemColors.WindowText;
            btnRefresh.ImageAlign = ContentAlignment.MiddleLeft;
            btnRefresh.ImageIndex = 3;
            btnRefresh.ImageList = imageList1;
            btnRefresh.Location = new Point(48, 204);
            btnRefresh.Margin = new Padding(3, 4, 3, 4);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(143, 44);
            btnRefresh.TabIndex = 13;
            btnRefresh.Text = "    Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label1.Location = new Point(63, 61);
            label1.Name = "label1";
            label1.Size = new Size(102, 23);
            label1.TabIndex = 14;
            label1.Text = "Student ID:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Rockwell", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.DarkBlue;
            label2.Location = new Point(94, 43);
            label2.Name = "label2";
            label2.Size = new Size(333, 33);
            label2.TabIndex = 15;
            label2.Text = "Student Record System";
            label2.Click += label2_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.MidnightBlue;
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1339, 27);
            panel1.TabIndex = 16;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Cyan;
            panel3.Controls.Add(pictureBox1);
            panel3.Controls.Add(label3);
            panel3.Controls.Add(label2);
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(1339, 124);
            panel3.TabIndex = 18;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(21, 41);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(71, 62);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 17;
            pictureBox1.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Rockwell", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.DarkBlue;
            label3.Location = new Point(98, 76);
            label3.Name = "label3";
            label3.Size = new Size(238, 22);
            label3.TabIndex = 16;
            label3.Text = "Manage Student Records";
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.LightCyan;
            groupBox1.Controls.Add(btnUpdate);
            groupBox1.Controls.Add(btnAdd);
            groupBox1.Controls.Add(btnDelete);
            groupBox1.Controls.Add(btnRefresh);
            groupBox1.Font = new Font("Rockwell", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.ForeColor = Color.DarkBlue;
            groupBox1.Location = new Point(1069, 138);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(239, 260);
            groupBox1.TabIndex = 19;
            groupBox1.TabStop = false;
            groupBox1.Text = "System Functions";
            // 
            // groupBox3
            // 
            groupBox3.BackColor = Color.LightCyan;
            groupBox3.Controls.Add(label7);
            groupBox3.Controls.Add(label8);
            groupBox3.Controls.Add(label9);
            groupBox3.Controls.Add(label10);
            groupBox3.Controls.Add(label6);
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(label4);
            groupBox3.Controls.Add(txtStudentID);
            groupBox3.Controls.Add(cmbCourse);
            groupBox3.Controls.Add(txtFullName);
            groupBox3.Controls.Add(cmbGender);
            groupBox3.Controls.Add(label1);
            groupBox3.Controls.Add(cmbYear);
            groupBox3.Controls.Add(txtPhone);
            groupBox3.Controls.Add(dtpDOB);
            groupBox3.Controls.Add(txtEmail);
            groupBox3.Font = new Font("Rockwell", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox3.ForeColor = Color.DarkBlue;
            groupBox3.Location = new Point(34, 138);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(998, 261);
            groupBox3.TabIndex = 20;
            groupBox3.TabStop = false;
            groupBox3.Text = "Student Informations";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label7.Location = new Point(498, 197);
            label7.Name = "label7";
            label7.Size = new Size(64, 23);
            label7.TabIndex = 21;
            label7.Text = "Phone:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label8.Location = new Point(498, 152);
            label8.Name = "label8";
            label8.Size = new Size(59, 23);
            label8.TabIndex = 20;
            label8.Text = "Email:";
            label8.Click += label8_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label9.Location = new Point(63, 193);
            label9.Name = "label9";
            label9.Size = new Size(54, 23);
            label9.TabIndex = 19;
            label9.Text = "Year: ";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label10.Location = new Point(63, 149);
            label10.Name = "label10";
            label10.Size = new Size(69, 23);
            label10.TabIndex = 18;
            label10.Text = "Course:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label6.Location = new Point(498, 107);
            label6.Name = "label6";
            label6.Size = new Size(74, 23);
            label6.TabIndex = 17;
            label6.Text = "Gender:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label5.Location = new Point(498, 62);
            label5.Name = "label5";
            label5.Size = new Size(120, 23);
            label5.TabIndex = 16;
            label5.Text = "Date of Birth:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            label4.Location = new Point(63, 105);
            label4.Name = "label4";
            label4.Size = new Size(96, 23);
            label4.TabIndex = 15;
            label4.Text = "Full Name:";
            // 
            // dgvStudents
            // 
            dataGridViewCellStyle1.BackColor = Color.Turquoise;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dgvStudents.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudents.BackgroundColor = Color.LightCyan;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvStudents.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvStudents.DefaultCellStyle = dataGridViewCellStyle3;
            dgvStudents.Location = new Point(47, 44);
            dgvStudents.Margin = new Padding(3, 4, 3, 4);
            dgvStudents.Name = "dgvStudents";
            dgvStudents.ReadOnly = true;
            dgvStudents.RowHeadersWidth = 51;
            dgvStudents.Size = new Size(1178, 341);
            dgvStudents.TabIndex = 9;
            dgvStudents.CellContentClick += dgvStudents_CellContentClick;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(dgvStudents);
            groupBox2.Font = new Font("Rockwell", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox2.Location = new Point(35, 420);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1273, 418);
            groupBox2.TabIndex = 21;
            groupBox2.TabStop = false;
            groupBox2.Text = "Student Records";
            // 
            // frmStudentRec
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightCyan;
            ClientSize = new Size(1339, 865);
            Controls.Add(groupBox2);
            Controls.Add(groupBox3);
            Controls.Add(groupBox1);
            Controls.Add(panel1);
            Controls.Add(panel3);
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmStudentRec";
            Text = "Student Record System";
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvStudents).EndInit();
            groupBox2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TextBox txtStudentID;
        private TextBox cmbCourse;
        private TextBox txtFullName;
        private TextBox cmbGender;
        private TextBox cmbYear;
        private DateTimePicker dtpDOB;
        private TextBox txtEmail;
        private TextBox txtPhone;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnRefresh;
        private Label label1;
        private Label label2;
        private Panel panel1;
        private Panel panel3;
        private Label label3;
        private ImageList imageList1;
        private GroupBox groupBox1;
        private GroupBox groupBox3;
        private Label label5;
        private Label label4;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label6;
        private PictureBox pictureBox1;
        private DataGridView dgvStudents;
        private GroupBox groupBox2;
    }
}
