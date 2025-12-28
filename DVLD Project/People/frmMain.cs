using ConsoleApp1;
using DVLD_Project.Applications.Manage_Test_Types;
using DVLD_Project.Divers;
using DVLD_Project.Local_Driving_Licenses;
using DVLD_Project.LogIn;
using DVLD_Project.Manage_Application_Type;
using DVLD_Project.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class frmMain : Form
    {
        private frmLogin _frmLogin;
        public frmMain(frmLogin login)
        {
            InitializeComponent();
            _frmLogin = login;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMainMenuItemPeople_Click(object sender, EventArgs e)
        {
            Form frm = new frmManagePeople();
            frm.ShowDialog();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMainMenuItemUsers_Click(object sender, EventArgs e)
        {
            Form frm = new frmManageUsers();
            frm.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 3. Reset the Global User
            Global.CurrentUser = null;

            // 4. Show the Login form again (using the saved reference)
            _frmLogin.ShowLogin();

            // 5. Close this Main form
            this.Close();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmChangePassword(Global.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void ShowUserDetails_Click(object sender, EventArgs e)
        {
            Form frm = new frmShowUserInfo(Global.CurrentUser.UserID);
            frm.ShowDialog();
            
        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmManageApplicationTypes();
            frm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListTestTypes();
            frm.ShowDialog();
        }

        private void newDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddLocalDrivingLicense();
            frm.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListLocalDrivingLicenseApplications();
            frm.ShowDialog();   
        }

        private void toolStripMainMenuItemDrivers_Click(object sender, EventArgs e)
        {
            Form frm = new frmListDrivers();
            frm.ShowDialog();   
        }
    }
}
