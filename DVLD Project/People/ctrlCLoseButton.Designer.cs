namespace DVLD_Project
{
    partial class ctrlCLoseButton
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrlCLoseButton));
            this.btnManagePeopleClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnManagePeopleClose
            // 
            this.btnManagePeopleClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnManagePeopleClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManagePeopleClose.Image = ((System.Drawing.Image)(resources.GetObject("btnManagePeopleClose.Image")));
            this.btnManagePeopleClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnManagePeopleClose.Location = new System.Drawing.Point(0, 1);
            this.btnManagePeopleClose.Name = "btnManagePeopleClose";
            this.btnManagePeopleClose.Size = new System.Drawing.Size(153, 52);
            this.btnManagePeopleClose.TabIndex = 6;
            this.btnManagePeopleClose.Text = "Close";
            this.btnManagePeopleClose.UseVisualStyleBackColor = true;
            // 
            // ctrlCLoseButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnManagePeopleClose);
            this.Name = "ctrlCLoseButton";
            this.Size = new System.Drawing.Size(155, 56);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnManagePeopleClose;
    }
}
