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
            txtStudentID = new TextBox();
            cmbCourse = new TextBox();
            txtFullName = new TextBox();
            cmbGender = new TextBox();
            cmbYear = new TextBox();
            dtpDOB = new DateTimePicker();
            txtEmail = new TextBox();
            txtPhone = new TextBox();
            dgvStudents = new DataGridView();
            btnAdd = new Button();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnRefresh = new Button();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvStudents).BeginInit();
            SuspendLayout();
            // 
            // txtStudentID
            // 
            txtStudentID.Location = new Point(30, 26);
            txtStudentID.Name = "txtStudentID";
            txtStudentID.Size = new Size(100, 23);
            txtStudentID.TabIndex = 1;
            // 
            // cmbCourse
            // 
            cmbCourse.Location = new Point(32, 170);
            cmbCourse.Name = "cmbCourse";
            cmbCourse.Size = new Size(100, 23);
            cmbCourse.TabIndex = 2;
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(32, 60);
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(100, 23);
            txtFullName.TabIndex = 3;
            // 
            // cmbGender
            // 
            cmbGender.Location = new Point(30, 134);
            cmbGender.Name = "cmbGender";
            cmbGender.Size = new Size(100, 23);
            cmbGender.TabIndex = 4;
            // 
            // cmbYear
            // 
            cmbYear.Location = new Point(34, 208);
            cmbYear.Name = "cmbYear";
            cmbYear.Size = new Size(100, 23);
            cmbYear.TabIndex = 5;
            // 
            // dtpDOB
            // 
            dtpDOB.Location = new Point(30, 98);
            dtpDOB.Name = "dtpDOB";
            dtpDOB.Size = new Size(200, 23);
            dtpDOB.TabIndex = 6;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(36, 246);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(100, 23);
            txtEmail.TabIndex = 7;
            // 
            // txtPhone
            // 
            txtPhone.Location = new Point(36, 282);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(100, 23);
            txtPhone.TabIndex = 8;
            // 
            // dgvStudents
            // 
            dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvStudents.Location = new Point(38, 324);
            dgvStudents.Name = "dgvStudents";
            dgvStudents.ReadOnly = true;
            dgvStudents.Size = new Size(1024, 254);
            dgvStudents.TabIndex = 9;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(506, 68);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 23);
            btnAdd.TabIndex = 10;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(628, 66);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 23);
            btnDelete.TabIndex = 11;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(512, 122);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(75, 23);
            btnUpdate.TabIndex = 12;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(630, 122);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(75, 23);
            btnRefresh.TabIndex = 13;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(138, 30);
            label1.Name = "label1";
            label1.Size = new Size(62, 15);
            label1.TabIndex = 14;
            label1.Text = "Student ID";
            // 
            // frmStudentRec
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1172, 649);
            Controls.Add(label1);
            Controls.Add(btnRefresh);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(btnAdd);
            Controls.Add(dgvStudents);
            Controls.Add(txtPhone);
            Controls.Add(txtEmail);
            Controls.Add(dtpDOB);
            Controls.Add(cmbYear);
            Controls.Add(cmbGender);
            Controls.Add(txtFullName);
            Controls.Add(cmbCourse);
            Controls.Add(txtStudentID);
            Name = "frmStudentRec";
            Text = "Student Record System";
            ((System.ComponentModel.ISupportInitialize)dgvStudents).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
        private DataGridView dgvStudents;
        private Button btnAdd;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnRefresh;
        private Label label1;
    }
}
