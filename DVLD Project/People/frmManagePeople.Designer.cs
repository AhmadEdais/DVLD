namespace DVLD_Project
{
    partial class frmManagePeople
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManagePeople));
            this.lblManagePeople = new System.Windows.Forms.Label();
            this.dgvAllPeople = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cShowDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cAddNewPerson = new System.Windows.Forms.ToolStripMenuItem();
            this.cEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.cDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cSendEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.cPhoneCall = new System.Windows.Forms.ToolStripMenuItem();
            this.lblFilterBy = new System.Windows.Forms.Label();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.lblNumberOfRecordsText = new System.Windows.Forms.Label();
            this.lblLiveNumberOfRecrords = new System.Windows.Forms.Label();
            this.mtxtBoxFilter = new System.Windows.Forms.MaskedTextBox();
            this.btnAddNewPerson = new System.Windows.Forms.Button();
            this.btnManagePeopleClose = new System.Windows.Forms.Button();
            this.pbManagePeople = new System.Windows.Forms.PictureBox();
            this.cbGender = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllPeople)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbManagePeople)).BeginInit();
            this.SuspendLayout();
            // 
            // lblManagePeople
            // 
            this.lblManagePeople.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblManagePeople.ForeColor = System.Drawing.Color.Maroon;
            this.lblManagePeople.Location = new System.Drawing.Point(694, 271);
            this.lblManagePeople.Name = "lblManagePeople";
            this.lblManagePeople.Size = new System.Drawing.Size(371, 51);
            this.lblManagePeople.TabIndex = 1;
            this.lblManagePeople.Text = "Manage People";
            // 
            // dgvAllPeople
            // 
            this.dgvAllPeople.AllowUserToAddRows = false;
            this.dgvAllPeople.AllowUserToDeleteRows = false;
            this.dgvAllPeople.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAllPeople.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvAllPeople.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAllPeople.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAllPeople.ColumnHeadersHeight = 29;
            this.dgvAllPeople.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAllPeople.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAllPeople.Location = new System.Drawing.Point(27, 384);
            this.dgvAllPeople.Name = "dgvAllPeople";
            this.dgvAllPeople.ReadOnly = true;
            this.dgvAllPeople.RowHeadersWidth = 30;
            this.dgvAllPeople.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvAllPeople.RowTemplate.Height = 24;
            this.dgvAllPeople.Size = new System.Drawing.Size(1700, 379);
            this.dgvAllPeople.TabIndex = 2;
            this.dgvAllPeople.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvAllPeople_CellMouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cShowDetails,
            this.toolStripSeparator1,
            this.cAddNewPerson,
            this.cEdit,
            this.cDelete,
            this.toolStripSeparator2,
            this.cSendEmail,
            this.cPhoneCall});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(204, 244);
            // 
            // cShowDetails
            // 
            this.cShowDetails.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cShowDetails.Image = ((System.Drawing.Image)(resources.GetObject("cShowDetails.Image")));
            this.cShowDetails.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cShowDetails.Name = "cShowDetails";
            this.cShowDetails.Size = new System.Drawing.Size(203, 38);
            this.cShowDetails.Text = "Show Details";
            this.cShowDetails.Click += new System.EventHandler(this.cShowDetails_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(200, 6);
            // 
            // cAddNewPerson
            // 
            this.cAddNewPerson.Image = ((System.Drawing.Image)(resources.GetObject("cAddNewPerson.Image")));
            this.cAddNewPerson.Name = "cAddNewPerson";
            this.cAddNewPerson.Size = new System.Drawing.Size(203, 38);
            this.cAddNewPerson.Text = "Add New Person";
            this.cAddNewPerson.Click += new System.EventHandler(this.cAddNewPerson_Click);
            // 
            // cEdit
            // 
            this.cEdit.Image = ((System.Drawing.Image)(resources.GetObject("cEdit.Image")));
            this.cEdit.Name = "cEdit";
            this.cEdit.Size = new System.Drawing.Size(203, 38);
            this.cEdit.Text = "Edit";
            this.cEdit.Click += new System.EventHandler(this.cEdit_Click);
            // 
            // cDelete
            // 
            this.cDelete.Image = ((System.Drawing.Image)(resources.GetObject("cDelete.Image")));
            this.cDelete.Name = "cDelete";
            this.cDelete.Size = new System.Drawing.Size(203, 38);
            this.cDelete.Text = "Delete";
            this.cDelete.Click += new System.EventHandler(this.cDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(200, 6);
            // 
            // cSendEmail
            // 
            this.cSendEmail.Image = ((System.Drawing.Image)(resources.GetObject("cSendEmail.Image")));
            this.cSendEmail.Name = "cSendEmail";
            this.cSendEmail.Size = new System.Drawing.Size(203, 38);
            this.cSendEmail.Text = "Send Email";
            this.cSendEmail.Click += new System.EventHandler(this.cSendEmail_Click);
            // 
            // cPhoneCall
            // 
            this.cPhoneCall.Image = ((System.Drawing.Image)(resources.GetObject("cPhoneCall.Image")));
            this.cPhoneCall.Name = "cPhoneCall";
            this.cPhoneCall.Size = new System.Drawing.Size(203, 38);
            this.cPhoneCall.Text = "Phone Call";
            this.cPhoneCall.Click += new System.EventHandler(this.cPhoneCall_Click);
            // 
            // lblFilterBy
            // 
            this.lblFilterBy.AutoSize = true;
            this.lblFilterBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterBy.Location = new System.Drawing.Point(22, 340);
            this.lblFilterBy.Name = "lblFilterBy";
            this.lblFilterBy.Size = new System.Drawing.Size(117, 29);
            this.lblFilterBy.TabIndex = 3;
            this.lblFilterBy.Text = "Filter By:";
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.BackColor = System.Drawing.SystemColors.Window;
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "None",
            "Person ID",
            "National NO",
            "First Name",
            "Second Name",
            "Third Name",
            "Last Name",
            "Nationality",
            "Gendor",
            "Phone",
            "Email"});
            this.cbFilterBy.Location = new System.Drawing.Point(145, 345);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(217, 33);
            this.cbFilterBy.TabIndex = 4;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // lblNumberOfRecordsText
            // 
            this.lblNumberOfRecordsText.AutoSize = true;
            this.lblNumberOfRecordsText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfRecordsText.Location = new System.Drawing.Point(30, 783);
            this.lblNumberOfRecordsText.Name = "lblNumberOfRecordsText";
            this.lblNumberOfRecordsText.Size = new System.Drawing.Size(122, 25);
            this.lblNumberOfRecordsText.TabIndex = 6;
            this.lblNumberOfRecordsText.Text = "# Records: ";
            // 
            // lblLiveNumberOfRecrords
            // 
            this.lblLiveNumberOfRecrords.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLiveNumberOfRecrords.Location = new System.Drawing.Point(158, 783);
            this.lblLiveNumberOfRecrords.Name = "lblLiveNumberOfRecrords";
            this.lblLiveNumberOfRecrords.Size = new System.Drawing.Size(49, 38);
            this.lblLiveNumberOfRecrords.TabIndex = 7;
            this.lblLiveNumberOfRecrords.Text = "???";
            // 
            // mtxtBoxFilter
            // 
            this.mtxtBoxFilter.HidePromptOnLeave = true;
            this.mtxtBoxFilter.Location = new System.Drawing.Point(368, 352);
            this.mtxtBoxFilter.Name = "mtxtBoxFilter";
            this.mtxtBoxFilter.PromptChar = ' ';
            this.mtxtBoxFilter.Size = new System.Drawing.Size(282, 22);
            this.mtxtBoxFilter.TabIndex = 9;
            this.mtxtBoxFilter.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.mtxtBoxFilter.ValidatingType = typeof(int);
            this.mtxtBoxFilter.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.mtxtBoxFilter_MaskInputRejected);
            this.mtxtBoxFilter.TextChanged += new System.EventHandler(this.mtxtBoxFilter_TextChanged);
            this.mtxtBoxFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mtxtBoxFilter_KeyPress);
            // 
            // btnAddNewPerson
            // 
            this.btnAddNewPerson.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddNewPerson.Image = ((System.Drawing.Image)(resources.GetObject("btnAddNewPerson.Image")));
            this.btnAddNewPerson.Location = new System.Drawing.Point(1617, 298);
            this.btnAddNewPerson.Name = "btnAddNewPerson";
            this.btnAddNewPerson.Size = new System.Drawing.Size(98, 71);
            this.btnAddNewPerson.TabIndex = 8;
            this.btnAddNewPerson.UseVisualStyleBackColor = true;
            this.btnAddNewPerson.Click += new System.EventHandler(this.btnAddNewPerson_Click);
            // 
            // btnManagePeopleClose
            // 
            this.btnManagePeopleClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnManagePeopleClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnManagePeopleClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManagePeopleClose.Image = ((System.Drawing.Image)(resources.GetObject("btnManagePeopleClose.Image")));
            this.btnManagePeopleClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnManagePeopleClose.Location = new System.Drawing.Point(1574, 769);
            this.btnManagePeopleClose.Name = "btnManagePeopleClose";
            this.btnManagePeopleClose.Size = new System.Drawing.Size(153, 52);
            this.btnManagePeopleClose.TabIndex = 5;
            this.btnManagePeopleClose.Text = "Close";
            this.btnManagePeopleClose.UseVisualStyleBackColor = true;
            this.btnManagePeopleClose.Click += new System.EventHandler(this.btnManagePeopleClose_Click);
            // 
            // pbManagePeople
            // 
            this.pbManagePeople.Image = ((System.Drawing.Image)(resources.GetObject("pbManagePeople.Image")));
            this.pbManagePeople.Location = new System.Drawing.Point(675, 30);
            this.pbManagePeople.Name = "pbManagePeople";
            this.pbManagePeople.Size = new System.Drawing.Size(377, 238);
            this.pbManagePeople.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbManagePeople.TabIndex = 0;
            this.pbManagePeople.TabStop = false;
            // 
            // cbGender
            // 
            this.cbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGender.FormattingEnabled = true;
            this.cbGender.Items.AddRange(new object[] {
            "Male",
            "Female"});
            this.cbGender.Location = new System.Drawing.Point(368, 350);
            this.cbGender.Name = "cbGender";
            this.cbGender.Size = new System.Drawing.Size(138, 24);
            this.cbGender.TabIndex = 10;
            this.cbGender.SelectedIndexChanged += new System.EventHandler(this.cbGender_SelectedIndexChanged);
            // 
            // frmManagePeople
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.btnManagePeopleClose;
            this.ClientSize = new System.Drawing.Size(1805, 833);
            this.Controls.Add(this.cbGender);
            this.Controls.Add(this.mtxtBoxFilter);
            this.Controls.Add(this.btnAddNewPerson);
            this.Controls.Add(this.lblLiveNumberOfRecrords);
            this.Controls.Add(this.lblNumberOfRecordsText);
            this.Controls.Add(this.btnManagePeopleClose);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.lblFilterBy);
            this.Controls.Add(this.dgvAllPeople);
            this.Controls.Add(this.lblManagePeople);
            this.Controls.Add(this.pbManagePeople);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmManagePeople";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage People";
            this.Load += new System.EventHandler(this.frmManagePeople_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllPeople)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbManagePeople)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbManagePeople;
        private System.Windows.Forms.Label lblManagePeople;
        private System.Windows.Forms.DataGridView dgvAllPeople;
        private System.Windows.Forms.Label lblFilterBy;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Button btnManagePeopleClose;
        private System.Windows.Forms.Label lblNumberOfRecordsText;
        private System.Windows.Forms.Label lblLiveNumberOfRecrords;
        private System.Windows.Forms.Button btnAddNewPerson;
        private System.Windows.Forms.MaskedTextBox mtxtBoxFilter;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cShowDetails;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cAddNewPerson;
        private System.Windows.Forms.ToolStripMenuItem cEdit;
        private System.Windows.Forms.ToolStripMenuItem cDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem cSendEmail;
        private System.Windows.Forms.ToolStripMenuItem cPhoneCall;
        private System.Windows.Forms.ComboBox cbGender;
    }
}