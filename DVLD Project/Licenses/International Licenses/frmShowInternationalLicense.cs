using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.License
{
    public partial class frmShowInternationalLicense : Form
    {
        int _IntLicenseID = -1;
        public frmShowInternationalLicense(int IntLicenseID)
        {
            InitializeComponent();
            _IntLicenseID= IntLicenseID;
        }

        private void btnManagePeopleClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowInternationalLicense_Load(object sender, EventArgs e)
        {
            ctrlShowInternationalLicenseInfo1.SetInternationalLicenseID(_IntLicenseID);
        }
    }
}
