namespace DVLD_Project
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.pbMainBackGround = new System.Windows.Forms.PictureBox();
            this.toolStripMainMenuItemApplications = new System.Windows.Forms.ToolStripMenuItem();
            this.tDrivingLicenseServices = new System.Windows.Forms.ToolStripMenuItem();
            this.newDrivingLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renewDrivingLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.replacementForLostOrDamagedLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.releaseDetainedDrivingLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.retakeTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.manageApplicationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.localDrivingLicenseApplicationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.internationalLicenseApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.detainLicensesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageApplicationTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageTestTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemPeople = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemDrivers = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMainMenuItemAccountSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowUserDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.signOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMainBackGround)).BeginInit();
            this.SuspendLayout();
            // 
            // MainMenuStrip
            // 
            this.MainMenuStrip.AutoSize = false;
            this.MainMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMainMenuItemApplications,
            this.toolStripMainMenuItemPeople,
            this.toolStripMainMenuItemDrivers,
            this.toolStripMainMenuItemUsers,
            this.toolStripMainMenuItemAccountSettings});
            this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.Size = new System.Drawing.Size(1924, 98);
            this.MainMenuStrip.TabIndex = 1;
            this.MainMenuStrip.Text = "menuStrip1";
            // 
            // pbMainBackGround
            // 
            this.pbMainBackGround.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pbMainBackGround.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbMainBackGround.Image = ((System.Drawing.Image)(resources.GetObject("pbMainBackGround.Image")));
            this.pbMainBackGround.Location = new System.Drawing.Point(0, 98);
            this.pbMainBackGround.Name = "pbMainBackGround";
            this.pbMainBackGround.Size = new System.Drawing.Size(1924, 957);
            this.pbMainBackGround.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMainBackGround.TabIndex = 0;
            this.pbMainBackGround.TabStop = false;
            this.pbMainBackGround.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // toolStripMainMenuItemApplications
            // 
            this.toolStripMainMenuItemApplications.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tDrivingLicenseServices,
            this.toolStripSeparator2,
            this.manageApplicationsToolStripMenuItem,
            this.toolStripSeparator3,
            this.detainLicensesToolStripMenuItem,
            this.manageApplicationTypesToolStripMenuItem,
            this.manageTestTypesToolStripMenuItem});
            this.toolStripMainMenuItemApplications.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMainMenuItemApplications.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMainMenuItemApplications.Image")));
            this.toolStripMainMenuItemApplications.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMainMenuItemApplications.Name = "toolStripMainMenuItemApplications";
            this.toolStripMainMenuItemApplications.Size = new System.Drawing.Size(196, 94);
            this.toolStripMainMenuItemApplications.Text = "Applications";
            // 
            // tDrivingLicenseServices
            // 
            this.tDrivingLicenseServices.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newDrivingLicenseToolStripMenuItem,
            this.renewDrivingLicenseToolStripMenuItem,
            this.toolStripSeparator4,
            this.replacementForLostOrDamagedLicenseToolStripMenuItem,
            this.toolStripSeparator5,
            this.releaseDetainedDrivingLicenseToolStripMenuItem,
            this.retakeTestToolStripMenuItem});
            this.tDrivingLicenseServices.Image = ((System.Drawing.Image)(resources.GetObject("tDrivingLicenseServices.Image")));
            this.tDrivingLicenseServices.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tDrivingLicenseServices.Name = "tDrivingLicenseServices";
            this.tDrivingLicenseServices.Size = new System.Drawing.Size(369, 70);
            this.tDrivingLicenseServices.Text = "Driving Licenses Services";
            // 
            // newDrivingLicenseToolStripMenuItem
            // 
            this.newDrivingLicenseToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newDrivingLicenseToolStripMenuItem.Image")));
            this.newDrivingLicenseToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.newDrivingLicenseToolStripMenuItem.Name = "newDrivingLicenseToolStripMenuItem";
            this.newDrivingLicenseToolStripMenuItem.Size = new System.Drawing.Size(467, 38);
            this.newDrivingLicenseToolStripMenuItem.Text = "New Driving License";
            this.newDrivingLicenseToolStripMenuItem.Click += new System.EventHandler(this.newDrivingLicenseToolStripMenuItem_Click);
            // 
            // renewDrivingLicenseToolStripMenuItem
            // 
            this.renewDrivingLicenseToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("renewDrivingLicenseToolStripMenuItem.Image")));
            this.renewDrivingLicenseToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.renewDrivingLicenseToolStripMenuItem.Name = "renewDrivingLicenseToolStripMenuItem";
            this.renewDrivingLicenseToolStripMenuItem.Size = new System.Drawing.Size(467, 38);
            this.renewDrivingLicenseToolStripMenuItem.Text = "Renew Driving License";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(464, 6);
            // 
            // replacementForLostOrDamagedLicenseToolStripMenuItem
            // 
            this.replacementForLostOrDamagedLicenseToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("replacementForLostOrDamagedLicenseToolStripMenuItem.Image")));
            this.replacementForLostOrDamagedLicenseToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.replacementForLostOrDamagedLicenseToolStripMenuItem.Name = "replacementForLostOrDamagedLicenseToolStripMenuItem";
            this.replacementForLostOrDamagedLicenseToolStripMenuItem.Size = new System.Drawing.Size(467, 38);
            this.replacementForLostOrDamagedLicenseToolStripMenuItem.Text = "Replacement for Lost or Damaged License";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(464, 6);
            // 
            // releaseDetainedDrivingLicenseToolStripMenuItem
            // 
            this.releaseDetainedDrivingLicenseToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("releaseDetainedDrivingLicenseToolStripMenuItem.Image")));
            this.releaseDetainedDrivingLicenseToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.releaseDetainedDrivingLicenseToolStripMenuItem.Name = "releaseDetainedDrivingLicenseToolStripMenuItem";
            this.releaseDetainedDrivingLicenseToolStripMenuItem.Size = new System.Drawing.Size(467, 38);
            this.releaseDetainedDrivingLicenseToolStripMenuItem.Text = "Release Detained Driving License";
            // 
            // retakeTestToolStripMenuItem
            // 
            this.retakeTestToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("retakeTestToolStripMenuItem.Image")));
            this.retakeTestToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.retakeTestToolStripMenuItem.Name = "retakeTestToolStripMenuItem";
            this.retakeTestToolStripMenuItem.Size = new System.Drawing.Size(467, 38);
            this.retakeTestToolStripMenuItem.Text = "Retake Test";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(366, 6);
            // 
            // manageApplicationsToolStripMenuItem
            // 
            this.manageApplicationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.localDrivingLicenseApplicationsToolStripMenuItem,
            this.internationalLicenseApplicationToolStripMenuItem});
            this.manageApplicationsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("manageApplicationsToolStripMenuItem.Image")));
            this.manageApplicationsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.manageApplicationsToolStripMenuItem.Name = "manageApplicationsToolStripMenuItem";
            this.manageApplicationsToolStripMenuItem.Size = new System.Drawing.Size(369, 70);
            this.manageApplicationsToolStripMenuItem.Text = "Manage Applications";
            // 
            // localDrivingLicenseApplicationsToolStripMenuItem
            // 
            this.localDrivingLicenseApplicationsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("localDrivingLicenseApplicationsToolStripMenuItem.Image")));
            this.localDrivingLicenseApplicationsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.localDrivingLicenseApplicationsToolStripMenuItem.Name = "localDrivingLicenseApplicationsToolStripMenuItem";
            this.localDrivingLicenseApplicationsToolStripMenuItem.Size = new System.Drawing.Size(399, 38);
            this.localDrivingLicenseApplicationsToolStripMenuItem.Text = "Local Driving License Applications";
            this.localDrivingLicenseApplicationsToolStripMenuItem.Click += new System.EventHandler(this.localDrivingLicenseApplicationsToolStripMenuItem_Click);
            // 
            // internationalLicenseApplicationToolStripMenuItem
            // 
            this.internationalLicenseApplicationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("internationalLicenseApplicationToolStripMenuItem.Image")));
            this.internationalLicenseApplicationToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.internationalLicenseApplicationToolStripMenuItem.Name = "internationalLicenseApplicationToolStripMenuItem";
            this.internationalLicenseApplicationToolStripMenuItem.Size = new System.Drawing.Size(399, 38);
            this.internationalLicenseApplicationToolStripMenuItem.Text = "International License Application";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(366, 6);
            // 
            // detainLicensesToolStripMenuItem
            // 
            this.detainLicensesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("detainLicensesToolStripMenuItem.Image")));
            this.detainLicensesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.detainLicensesToolStripMenuItem.Name = "detainLicensesToolStripMenuItem";
            this.detainLicensesToolStripMenuItem.Size = new System.Drawing.Size(369, 70);
            this.detainLicensesToolStripMenuItem.Text = "Detain Licenses";
            // 
            // manageApplicationTypesToolStripMenuItem
            // 
            this.manageApplicationTypesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("manageApplicationTypesToolStripMenuItem.Image")));
            this.manageApplicationTypesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.manageApplicationTypesToolStripMenuItem.Name = "manageApplicationTypesToolStripMenuItem";
            this.manageApplicationTypesToolStripMenuItem.Size = new System.Drawing.Size(369, 70);
            this.manageApplicationTypesToolStripMenuItem.Text = "Manage Application Types";
            this.manageApplicationTypesToolStripMenuItem.Click += new System.EventHandler(this.manageApplicationTypesToolStripMenuItem_Click);
            // 
            // manageTestTypesToolStripMenuItem
            // 
            this.manageTestTypesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("manageTestTypesToolStripMenuItem.Image")));
            this.manageTestTypesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.manageTestTypesToolStripMenuItem.Name = "manageTestTypesToolStripMenuItem";
            this.manageTestTypesToolStripMenuItem.Size = new System.Drawing.Size(369, 70);
            this.manageTestTypesToolStripMenuItem.Text = "Manage Test Types";
            this.manageTestTypesToolStripMenuItem.Click += new System.EventHandler(this.manageTestTypesToolStripMenuItem_Click);
            // 
            // toolStripMainMenuItemPeople
            // 
            this.toolStripMainMenuItemPeople.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMainMenuItemPeople.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMainMenuItemPeople.Image")));
            this.toolStripMainMenuItemPeople.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripMainMenuItemPeople.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMainMenuItemPeople.Name = "toolStripMainMenuItemPeople";
            this.toolStripMainMenuItemPeople.Size = new System.Drawing.Size(147, 94);
            this.toolStripMainMenuItemPeople.Text = "People";
            this.toolStripMainMenuItemPeople.Click += new System.EventHandler(this.toolStripMainMenuItemPeople_Click);
            // 
            // toolStripMainMenuItemDrivers
            // 
            this.toolStripMainMenuItemDrivers.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMainMenuItemDrivers.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMainMenuItemDrivers.Image")));
            this.toolStripMainMenuItemDrivers.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMainMenuItemDrivers.Name = "toolStripMainMenuItemDrivers";
            this.toolStripMainMenuItemDrivers.Size = new System.Drawing.Size(150, 94);
            this.toolStripMainMenuItemDrivers.Text = "Drivers";
            // 
            // toolStripMainMenuItemUsers
            // 
            this.toolStripMainMenuItemUsers.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMainMenuItemUsers.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMainMenuItemUsers.Image")));
            this.toolStripMainMenuItemUsers.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMainMenuItemUsers.Name = "toolStripMainMenuItemUsers";
            this.toolStripMainMenuItemUsers.Size = new System.Drawing.Size(136, 94);
            this.toolStripMainMenuItemUsers.Text = "Users";
            this.toolStripMainMenuItemUsers.Click += new System.EventHandler(this.toolStripMainMenuItemUsers_Click);
            // 
            // toolStripMainMenuItemAccountSettings
            // 
            this.toolStripMainMenuItemAccountSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowUserDetails,
            this.changePasswordToolStripMenuItem,
            this.toolStripSeparator1,
            this.signOutToolStripMenuItem});
            this.toolStripMainMenuItemAccountSettings.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMainMenuItemAccountSettings.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMainMenuItemAccountSettings.Image")));
            this.toolStripMainMenuItemAccountSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMainMenuItemAccountSettings.Name = "toolStripMainMenuItemAccountSettings";
            this.toolStripMainMenuItemAccountSettings.Size = new System.Drawing.Size(235, 94);
            this.toolStripMainMenuItemAccountSettings.Text = "Account Settings";
            // 
            // ShowUserDetails
            // 
            this.ShowUserDetails.Image = ((System.Drawing.Image)(resources.GetObject("ShowUserDetails.Image")));
            this.ShowUserDetails.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ShowUserDetails.Name = "ShowUserDetails";
            this.ShowUserDetails.Size = new System.Drawing.Size(259, 38);
            this.ShowUserDetails.Text = "Current User Info";
            this.ShowUserDetails.Click += new System.EventHandler(this.ShowUserDetails_Click);
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("changePasswordToolStripMenuItem.Image")));
            this.changePasswordToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(259, 38);
            this.changePasswordToolStripMenuItem.Text = "Change Password";
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(256, 6);
            // 
            // signOutToolStripMenuItem
            // 
            this.signOutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("signOutToolStripMenuItem.Image")));
            this.signOutToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.signOutToolStripMenuItem.Name = "signOutToolStripMenuItem";
            this.signOutToolStripMenuItem.Size = new System.Drawing.Size(259, 38);
            this.signOutToolStripMenuItem.Text = "Sign Out";
            this.signOutToolStripMenuItem.Click += new System.EventHandler(this.signOutToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(1924, 1055);
            this.Controls.Add(this.pbMainBackGround);
            this.Controls.Add(this.MainMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.Name = "frmMain";
            this.Text = "Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MainMenuStrip.ResumeLayout(false);
            this.MainMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMainBackGround)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbMainBackGround;
        private System.Windows.Forms.MenuStrip MainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMainMenuItemPeople;
        private System.Windows.Forms.ToolStripMenuItem toolStripMainMenuItemApplications;
        private System.Windows.Forms.ToolStripMenuItem toolStripMainMenuItemDrivers;
        private System.Windows.Forms.ToolStripMenuItem toolStripMainMenuItemUsers;
        private System.Windows.Forms.ToolStripMenuItem toolStripMainMenuItemAccountSettings;
        private System.Windows.Forms.ToolStripMenuItem ShowUserDetails;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem signOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tDrivingLicenseServices;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem manageApplicationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem detainLicensesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageApplicationTypesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manageTestTypesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newDrivingLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renewDrivingLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem replacementForLostOrDamagedLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem releaseDetainedDrivingLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem retakeTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem localDrivingLicenseApplicationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem internationalLicenseApplicationToolStripMenuItem;
    }
}

