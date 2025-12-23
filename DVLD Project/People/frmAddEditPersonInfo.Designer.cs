namespace DVLD_Project
{
    partial class frmAddEditPersonInfo
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
            this.ctrlAddUpdatePerson1 = new DVLD_Project.ctrlAddUpdatePerson();
            this.SuspendLayout();
            // 
            // ctrlAddUpdatePerson1
            // 
            this.ctrlAddUpdatePerson1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ctrlAddUpdatePerson1.Location = new System.Drawing.Point(12, 3);
            this.ctrlAddUpdatePerson1.Name = "ctrlAddUpdatePerson1";
            this.ctrlAddUpdatePerson1.Size = new System.Drawing.Size(1058, 635);
            this.ctrlAddUpdatePerson1.TabIndex = 5;
            this.ctrlAddUpdatePerson1.Load += new System.EventHandler(this.ctrlAddUpdatePerson1_Load);
            // 
            // frmAddEditPersonInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1071, 637);
            this.Controls.Add(this.ctrlAddUpdatePerson1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddEditPersonInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add / Edit Person Info. ";
            this.Load += new System.EventHandler(this.frmAddEditPersonInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private ctrlAddUpdatePerson ctrlAddUpdatePerson1;
    }
}