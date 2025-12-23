namespace DVLD_Project.People
{
    partial class ctrlFindPerson
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrlFindPerson));
            this.gbFilteredBy = new System.Windows.Forms.GroupBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.txtFindBy = new System.Windows.Forms.TextBox();
            this.cbFindBy = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.ctrlPersonDetails1 = new DVLD_Project.ctrlPersonDetails();
            this.gbFilteredBy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // gbFilteredBy
            // 
            this.gbFilteredBy.Controls.Add(this.btnAdd);
            this.gbFilteredBy.Controls.Add(this.btnFind);
            this.gbFilteredBy.Controls.Add(this.txtFindBy);
            this.gbFilteredBy.Controls.Add(this.cbFindBy);
            this.gbFilteredBy.Controls.Add(this.label1);
            this.gbFilteredBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFilteredBy.Location = new System.Drawing.Point(6, 22);
            this.gbFilteredBy.Name = "gbFilteredBy";
            this.gbFilteredBy.Size = new System.Drawing.Size(1064, 92);
            this.gbFilteredBy.TabIndex = 1;
            this.gbFilteredBy.TabStop = false;
            this.gbFilteredBy.Text = "Filter";
            // 
            // btnAdd
            // 
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(756, 24);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(66, 62);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnFind
            // 
            this.btnFind.Image = ((System.Drawing.Image)(resources.GetObject("btnFind.Image")));
            this.btnFind.Location = new System.Drawing.Point(666, 24);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(66, 62);
            this.btnFind.TabIndex = 3;
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtFindBy
            // 
            this.txtFindBy.Location = new System.Drawing.Point(432, 42);
            this.txtFindBy.Name = "txtFindBy";
            this.txtFindBy.Size = new System.Drawing.Size(214, 24);
            this.txtFindBy.TabIndex = 2;
            this.txtFindBy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFindBy_KeyPress);
            this.txtFindBy.Validating += new System.ComponentModel.CancelEventHandler(this.txtFindBy_Validating);
            // 
            // cbFindBy
            // 
            this.cbFindBy.BackColor = System.Drawing.SystemColors.Window;
            this.cbFindBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFindBy.FormattingEnabled = true;
            this.cbFindBy.Items.AddRange(new object[] {
            "Person ID",
            "National NO"});
            this.cbFindBy.Location = new System.Drawing.Point(147, 40);
            this.cbFindBy.Name = "cbFindBy";
            this.cbFindBy.Size = new System.Drawing.Size(265, 26);
            this.cbFindBy.TabIndex = 1;
            this.cbFindBy.SelectionChangeCommitted += new System.EventHandler(this.cbFindBy_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(37, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Find By : ";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ctrlPersonDetails1
            // 
            this.ctrlPersonDetails1.EditLinkVisible = true;
            this.ctrlPersonDetails1.Location = new System.Drawing.Point(3, 114);
            this.ctrlPersonDetails1.Name = "ctrlPersonDetails1";
            this.ctrlPersonDetails1.Size = new System.Drawing.Size(1099, 422);
            this.ctrlPersonDetails1.TabIndex = 0;
            this.ctrlPersonDetails1.Load += new System.EventHandler(this.ctrlPersonDetails1_Load);
            // 
            // ctrlFindPerson
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbFilteredBy);
            this.Controls.Add(this.ctrlPersonDetails1);
            this.Name = "ctrlFindPerson";
            this.Size = new System.Drawing.Size(1131, 549);
            this.Load += new System.EventHandler(this.ctrlFindPerson_Load);
            this.gbFilteredBy.ResumeLayout(false);
            this.gbFilteredBy.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlPersonDetails ctrlPersonDetails1;
        private System.Windows.Forms.GroupBox gbFilteredBy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFindBy;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.TextBox txtFindBy;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
