namespace DVLD_Project.Manage_Application_Type
{
    partial class frmManageApplicationTypes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManageApplicationTypes));
            this.lblAddEditPerson = new System.Windows.Forms.Label();
            this.dgvAllApplicationTypes = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lblLiveNumberOfRecrords = new System.Windows.Forms.Label();
            this.lblNumberOfRecordsText = new System.Windows.Forms.Label();
            this.btnManagePeopleClose = new System.Windows.Forms.Button();
            this.editApplicationTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllApplicationTypes)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAddEditPerson
            // 
            this.lblAddEditPerson.AutoSize = true;
            this.lblAddEditPerson.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddEditPerson.ForeColor = System.Drawing.Color.Maroon;
            this.lblAddEditPerson.Location = new System.Drawing.Point(275, 311);
            this.lblAddEditPerson.Name = "lblAddEditPerson";
            this.lblAddEditPerson.Size = new System.Drawing.Size(555, 51);
            this.lblAddEditPerson.TabIndex = 44;
            this.lblAddEditPerson.Text = "Manage Application Types";
            // 
            // dgvAllApplicationTypes
            // 
            this.dgvAllApplicationTypes.AllowUserToAddRows = false;
            this.dgvAllApplicationTypes.AllowUserToDeleteRows = false;
            this.dgvAllApplicationTypes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAllApplicationTypes.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvAllApplicationTypes.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAllApplicationTypes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAllApplicationTypes.ColumnHeadersHeight = 29;
            this.dgvAllApplicationTypes.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAllApplicationTypes.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAllApplicationTypes.Location = new System.Drawing.Point(12, 406);
            this.dgvAllApplicationTypes.Name = "dgvAllApplicationTypes";
            this.dgvAllApplicationTypes.ReadOnly = true;
            this.dgvAllApplicationTypes.RowHeadersWidth = 30;
            this.dgvAllApplicationTypes.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvAllApplicationTypes.RowTemplate.Height = 24;
            this.dgvAllApplicationTypes.Size = new System.Drawing.Size(1073, 379);
            this.dgvAllApplicationTypes.TabIndex = 46;
            this.dgvAllApplicationTypes.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvAllApplicationTypes_CellMouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editApplicationTypeToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(256, 42);
            // 
            // lblLiveNumberOfRecrords
            // 
            this.lblLiveNumberOfRecrords.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLiveNumberOfRecrords.Location = new System.Drawing.Point(140, 824);
            this.lblLiveNumberOfRecrords.Name = "lblLiveNumberOfRecrords";
            this.lblLiveNumberOfRecrords.Size = new System.Drawing.Size(53, 38);
            this.lblLiveNumberOfRecrords.TabIndex = 48;
            this.lblLiveNumberOfRecrords.Text = "???";
            // 
            // lblNumberOfRecordsText
            // 
            this.lblNumberOfRecordsText.AutoSize = true;
            this.lblNumberOfRecordsText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumberOfRecordsText.Location = new System.Drawing.Point(12, 822);
            this.lblNumberOfRecordsText.Name = "lblNumberOfRecordsText";
            this.lblNumberOfRecordsText.Size = new System.Drawing.Size(122, 25);
            this.lblNumberOfRecordsText.TabIndex = 47;
            this.lblNumberOfRecordsText.Text = "# Records: ";
            // 
            // btnManagePeopleClose
            // 
            this.btnManagePeopleClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnManagePeopleClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnManagePeopleClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManagePeopleClose.Image = ((System.Drawing.Image)(resources.GetObject("btnManagePeopleClose.Image")));
            this.btnManagePeopleClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnManagePeopleClose.Location = new System.Drawing.Point(932, 808);
            this.btnManagePeopleClose.Name = "btnManagePeopleClose";
            this.btnManagePeopleClose.Size = new System.Drawing.Size(153, 52);
            this.btnManagePeopleClose.TabIndex = 49;
            this.btnManagePeopleClose.Text = "Close";
            this.btnManagePeopleClose.UseVisualStyleBackColor = true;
            this.btnManagePeopleClose.Click += new System.EventHandler(this.btnManagePeopleClose_Click);
            // 
            // editApplicationTypeToolStripMenuItem
            // 
            this.editApplicationTypeToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editApplicationTypeToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("editApplicationTypeToolStripMenuItem.Image")));
            this.editApplicationTypeToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.editApplicationTypeToolStripMenuItem.Name = "editApplicationTypeToolStripMenuItem";
            this.editApplicationTypeToolStripMenuItem.Size = new System.Drawing.Size(255, 38);
            this.editApplicationTypeToolStripMenuItem.Text = "Edit Application Type";
            this.editApplicationTypeToolStripMenuItem.Click += new System.EventHandler(this.editApplicationTypeToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(374, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(335, 269);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 45;
            this.pictureBox1.TabStop = false;
            // 
            // frmManageApplicationTypes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.btnManagePeopleClose;
            this.ClientSize = new System.Drawing.Size(1097, 981);
            this.Controls.Add(this.btnManagePeopleClose);
            this.Controls.Add(this.lblLiveNumberOfRecrords);
            this.Controls.Add(this.lblNumberOfRecordsText);
            this.Controls.Add(this.dgvAllApplicationTypes);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblAddEditPerson);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmManageApplicationTypes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage Application Types";
            this.Load += new System.EventHandler(this.frmManageApplicationTypes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllApplicationTypes)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAddEditPerson;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView dgvAllApplicationTypes;
        private System.Windows.Forms.Label lblLiveNumberOfRecrords;
        private System.Windows.Forms.Label lblNumberOfRecordsText;
        private System.Windows.Forms.Button btnManagePeopleClose;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editApplicationTypeToolStripMenuItem;
    }
}