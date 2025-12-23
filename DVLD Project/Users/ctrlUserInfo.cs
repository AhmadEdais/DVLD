using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Users
{
    public partial class ctrlUserInfo : UserControl
    {
        int _UserID = -1;
        clsUser _User;
        public ctrlUserInfo()
        {
            InitializeComponent();
        }
        public ctrlUserInfo(int UserID)
        {
            InitializeComponent();
            LoadInfo(UserID);

        }
        public void LoadInfo(int UserID)
        {
            _UserID = UserID;
            _User = clsUser.FindByUserID(_UserID);
            if (_User == null)
            {
                // Handle case where user is not found (e.g. clear controls)
                
                MessageBox.Show("No User with UserID = " + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ctrlPersonDetails1.SetID(clsUser.FindByUserID(_UserID).PersonID);
            lblUserID.Text = _UserID.ToString();
            lblUserName.Text = _User.UserName;
            lblIsActive.Text = _User.IsActive ? "Yes" : "No";
        }
        private void ctrlUserInfo_Load(object sender, EventArgs e)
        {
           
        }
    }
}
