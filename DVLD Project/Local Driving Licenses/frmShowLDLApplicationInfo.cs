using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Local_Driving_Licenses
{
    public partial class frmShowLDLApplicationInfo : Form
    {
        int _LDLApllID = -1;
        public frmShowLDLApplicationInfo(int LDLApllID)
        {
            _LDLApllID = LDLApllID;
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowLDLApplicationInfo_Load(object sender, EventArgs e)
        {
            ctrlLocalDriverLicenseApplicationInfo1.SetApplicationID(_LDLApllID);
        }
    }
}
