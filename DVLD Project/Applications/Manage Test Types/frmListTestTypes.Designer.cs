namespace DVLD_Project.Applications.Manage_Test_Types
{
    partial class frmListTestTypes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmListTestTypes));
            this.lblLiveNumberOfRecrords = new System.Windows.Forms.Label();
            this.lblNumberOfRecordsText = new System.Windows.Forms.Label();
            this.dgvTestTypes = new System.Windows.Forms.DataGridView();
            this.lblAddEditPerson = new System.Windows.Forms.Label();
            this.btnManagePeopleClose = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editTypeTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblLiveNumberOfRecrords
            // 
            this.lblLiveNumberOfRecrords.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLiveNumberOfRecrords.Location = new System.Drawing.Point(140, 808);
            this.lblLiveNumberOfRecrords.Name = "lblLiveNumberOfRecrords";
            this.lblLiveNumberOfRecrords.Size = new System.Drawing.Size(53, 38);
            this.lblLiveNumberOfRecrords.TabIndex = 54;
            this.lblLiveNumberOfRecrords.Text = "???";
            // 
            // lblNumberOfRecordsText
            // 
            this.lblNumberOfRecordsText.AutoSize = true;
            this.lblNumberOfRecordsText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfRecordsText.Location = new System.Drawing.Point(12, 806);
            this.lblNumberOfRecordsText.Name = "lblNumberOfRecordsText";
            this.lblNumberOfRecordsText.Size = new System.Drawing.Size(122, 25);
            this.lblNumberOfRecordsText.TabIndex = 53;
            this.lblNumberOfRecordsText.Text = "# Records: ";
            // 
            // dgvTestTypes
            // 
            this.dgvTestTypes.AllowUserToAddRows = false;
            this.dgvTestTypes.AllowUserToDeleteRows = false;
            this.dgvTestTypes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTestTypes.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvTestTypes.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTestTypes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTestTypes.ColumnHeadersHeight = 29;
            this.dgvTestTypes.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTestTypes.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTestTypes.Location = new System.Drawing.Point(12, 394);
            this.dgvTestTypes.Name = "dgvTestTypes";
            this.dgvTestTypes.ReadOnly = true;
            this.dgvTestTypes.RowHeadersWidth = 30;
            this.dgvTestTypes.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvTestTypes.RowTemplate.Height = 24;
            this.dgvTestTypes.Size = new System.Drawing.Size(1157, 379);
            this.dgvTestTypes.TabIndex = 52;
            this.dgvTestTypes.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvTestTypes_CellMouseDown);
            // 
            // lblAddEditPerson
            // 
            this.lblAddEditPerson.AutoSize = true;
            this.lblAddEditPerson.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddEditPerson.ForeColor = System.Drawing.Color.Maroon;
            this.lblAddEditPerson.Location = new System.Drawing.Point(300, 305);
            this.lblAddEditPerson.Name = "lblAddEditPerson";
            this.lblAddEditPerson.Size = new System.Drawing.Size(419, 51);
            this.lblAddEditPerson.TabIndex = 50;
            this.lblAddEditPerson.Text = "Manage Test Types";
            // 
            // btnManagePeopleClose
            // 
            this.btnManagePeopleClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnManagePeopleClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnManagePeopleClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManagePeopleClose.Image = ((System.Drawing.Image)(resources.GetObject("btnManagePeopleClose.Image")));
            this.btnManagePeopleClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnManagePeopleClose.Location = new System.Drawing.Point(1016, 794);
            this.btnManagePeopleClose.Name = "btnManagePeopleClose";
            this.btnManagePeopleClose.Size = new System.Drawing.Size(153, 52);
            this.btnManagePeopleClose.TabIndex = 55;
            this.btnManagePeopleClose.Text = "Close";
            this.btnManagePeopleClose.UseVisualStyleBackColor = true;
            this.btnManagePeopleClose.Click += new System.EventHandler(this.btnManagePeopleClose_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(347, 23);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(335, 269);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 51;
            this.pictureBox1.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editTypeTestToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(205, 42);
            // 
            // editTypeTestToolStripMenuItem
            // 
            this.editTypeTestToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editTypeTestToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("editTypeTestToolStripMenuItem.Image")));
            this.editTypeTestToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.editTypeTestToolStripMenuItem.Name = "editTypeTestToolStripMenuItem";
            this.editTypeTestToolStripMenuItem.Size = new System.Drawing.Size(204, 38);
            this.editTypeTestToolStripMenuItem.Text = "Edit Type Test";
            this.editTypeTestToolStripMenuItem.Click += new System.EventHandler(this.editTypeTestToolStripMenuItem_Click);
            // 
            // frmListTestTypes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.btnManagePeopleClose;
            this.ClientSize = new System.Drawing.Size(1181, 898);
            this.Controls.Add(this.btnManagePeopleClose);
            this.Controls.Add(this.lblLiveNumberOfRecrords);
            this.Controls.Add(this.lblNumberOfRecordsText);
            this.Controls.Add(this.dgvTestTypes);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblAddEditPerson);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmListTestTypes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmListTestTypes";
            this.Load += new System.EventHandler(this.frmListTestTypes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTestTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnManagePeopleClose;
        private System.Windows.Forms.Label lblLiveNumberOfRecrords;
        private System.Windows.Forms.Label lblNumberOfRecordsText;
        private System.Windows.Forms.DataGridView dgvTestTypes;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblAddEditPerson;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editTypeTestToolStripMenuItem;
    }
}