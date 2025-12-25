namespace DVLD_Project.Test_Appointments
{
    partial class frmTestAppointments
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTestAppointments));
            this.lblTestAppointmentName = new System.Windows.Forms.Label();
            this.dgvAllAppointments = new System.Windows.Forms.DataGridView();
            this.lblRecordsNumber = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddNewAppointment = new System.Windows.Forms.Button();
            this.btnManagePeopleClose = new System.Windows.Forms.Button();
            this.pbTestAppoitment = new System.Windows.Forms.PictureBox();
            this.ctrlLocalDriverLicenseApplicationInfo1 = new DVLD_Project.Local_Driving_Licenses.ctrlLocalDriverLicenseApplicationInfo();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.takeTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllAppointments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTestAppoitment)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTestAppointmentName
            // 
            this.lblTestAppointmentName.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTestAppointmentName.ForeColor = System.Drawing.Color.Maroon;
            this.lblTestAppointmentName.Location = new System.Drawing.Point(320, 153);
            this.lblTestAppointmentName.Name = "lblTestAppointmentName";
            this.lblTestAppointmentName.Size = new System.Drawing.Size(592, 51);
            this.lblTestAppointmentName.TabIndex = 11;
            this.lblTestAppointmentName.Text = "Vision Test Appointments";
            // 
            // dgvAllAppointments
            // 
            this.dgvAllAppointments.AllowUserToAddRows = false;
            this.dgvAllAppointments.AllowUserToDeleteRows = false;
            this.dgvAllAppointments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvAllAppointments.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAllAppointments.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvAllAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAllAppointments.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvAllAppointments.Location = new System.Drawing.Point(28, 720);
            this.dgvAllAppointments.Name = "dgvAllAppointments";
            this.dgvAllAppointments.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAllAppointments.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvAllAppointments.RowHeadersWidth = 51;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvAllAppointments.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvAllAppointments.RowTemplate.Height = 24;
            this.dgvAllAppointments.Size = new System.Drawing.Size(1073, 193);
            this.dgvAllAppointments.TabIndex = 12;
            // 
            // lblRecordsNumber
            // 
            this.lblRecordsNumber.AutoSize = true;
            this.lblRecordsNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordsNumber.Location = new System.Drawing.Point(151, 933);
            this.lblRecordsNumber.Name = "lblRecordsNumber";
            this.lblRecordsNumber.Size = new System.Drawing.Size(36, 25);
            this.lblRecordsNumber.TabIndex = 22;
            this.lblRecordsNumber.Text = "??";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 933);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 25);
            this.label2.TabIndex = 21;
            this.label2.Text = "# Records: ";
            // 
            // btnAddNewAppointment
            // 
            this.btnAddNewAppointment.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnAddNewAppointment.Image = ((System.Drawing.Image)(resources.GetObject("btnAddNewAppointment.Image")));
            this.btnAddNewAppointment.Location = new System.Drawing.Point(1046, 664);
            this.btnAddNewAppointment.Name = "btnAddNewAppointment";
            this.btnAddNewAppointment.Size = new System.Drawing.Size(55, 50);
            this.btnAddNewAppointment.TabIndex = 23;
            this.btnAddNewAppointment.UseVisualStyleBackColor = true;
            this.btnAddNewAppointment.Click += new System.EventHandler(this.btnAddNewAppointment_Click);
            // 
            // btnManagePeopleClose
            // 
            this.btnManagePeopleClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnManagePeopleClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnManagePeopleClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManagePeopleClose.Image = ((System.Drawing.Image)(resources.GetObject("btnManagePeopleClose.Image")));
            this.btnManagePeopleClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnManagePeopleClose.Location = new System.Drawing.Point(948, 919);
            this.btnManagePeopleClose.Name = "btnManagePeopleClose";
            this.btnManagePeopleClose.Size = new System.Drawing.Size(153, 52);
            this.btnManagePeopleClose.TabIndex = 20;
            this.btnManagePeopleClose.Text = "Close";
            this.btnManagePeopleClose.UseVisualStyleBackColor = true;
            this.btnManagePeopleClose.Click += new System.EventHandler(this.btnManagePeopleClose_Click);
            // 
            // pbTestAppoitment
            // 
            this.pbTestAppoitment.Image = global::DVLD_Project.Properties.Resources.Vision_512;
            this.pbTestAppoitment.Location = new System.Drawing.Point(366, 9);
            this.pbTestAppoitment.Name = "pbTestAppoitment";
            this.pbTestAppoitment.Size = new System.Drawing.Size(354, 141);
            this.pbTestAppoitment.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbTestAppoitment.TabIndex = 1;
            this.pbTestAppoitment.TabStop = false;
            // 
            // ctrlLocalDriverLicenseApplicationInfo1
            // 
            this.ctrlLocalDriverLicenseApplicationInfo1.Location = new System.Drawing.Point(22, 207);
            this.ctrlLocalDriverLicenseApplicationInfo1.Name = "ctrlLocalDriverLicenseApplicationInfo1";
            this.ctrlLocalDriverLicenseApplicationInfo1.Size = new System.Drawing.Size(1079, 451);
            this.ctrlLocalDriverLicenseApplicationInfo1.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editTestToolStripMenuItem,
            this.takeTestToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(227, 108);
            // 
            // editTestToolStripMenuItem
            // 
            this.editTestToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("editTestToolStripMenuItem.Image")));
            this.editTestToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.editTestToolStripMenuItem.Name = "editTestToolStripMenuItem";
            this.editTestToolStripMenuItem.Size = new System.Drawing.Size(226, 38);
            this.editTestToolStripMenuItem.Text = "Edit Test";
            // 
            // takeTestToolStripMenuItem
            // 
            this.takeTestToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("takeTestToolStripMenuItem.Image")));
            this.takeTestToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.takeTestToolStripMenuItem.Name = "takeTestToolStripMenuItem";
            this.takeTestToolStripMenuItem.Size = new System.Drawing.Size(226, 38);
            this.takeTestToolStripMenuItem.Text = "Take Test";
            // 
            // frmTestAppointments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.btnManagePeopleClose;
            this.ClientSize = new System.Drawing.Size(1155, 1055);
            this.Controls.Add(this.btnAddNewAppointment);
            this.Controls.Add(this.lblRecordsNumber);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnManagePeopleClose);
            this.Controls.Add(this.dgvAllAppointments);
            this.Controls.Add(this.lblTestAppointmentName);
            this.Controls.Add(this.pbTestAppoitment);
            this.Controls.Add(this.ctrlLocalDriverLicenseApplicationInfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmTestAppointments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmTestAppointments";
            this.Load += new System.EventHandler(this.frmTestAppointments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllAppointments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTestAppoitment)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Local_Driving_Licenses.ctrlLocalDriverLicenseApplicationInfo ctrlLocalDriverLicenseApplicationInfo1;
        private System.Windows.Forms.PictureBox pbTestAppoitment;
        private System.Windows.Forms.Label lblTestAppointmentName;
        private System.Windows.Forms.DataGridView dgvAllAppointments;
        private System.Windows.Forms.Button btnManagePeopleClose;
        private System.Windows.Forms.Label lblRecordsNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddNewAppointment;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem takeTestToolStripMenuItem;
    }
}