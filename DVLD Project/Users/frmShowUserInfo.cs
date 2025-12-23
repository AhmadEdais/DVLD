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
    public partial class frmShowUserInfo : Form
    {
        int _UserID = -1;
        clsUser _User;
        public frmShowUserInfo(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            ctrlUserInfo1.LoadInfo(_UserID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
