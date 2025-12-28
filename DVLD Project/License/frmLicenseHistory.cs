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
    public partial class frmLicenseHistory : Form
    {
        int _DriverID = -1;
        clsDriver _Driver;
        DataTable _dtAllLocalLicenses;
        DataTable _dtAllInternationalLicense;
        DataView _dvLocalLicenses;
        DataTable _dvInternationalLicense;
        public frmLicenseHistory(int DriverID)
        {
            InitializeComponent();
            _DriverID = DriverID;
            _Driver = clsDriver.FindByDriverID(_DriverID);

        }

        private void btnManagePeopleClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void _RefreshData()
        {
            _dtAllLocalLicenses = clsLicense.GetDriverLicenses(_DriverID);
            _dvLocalLicenses = _dtAllLocalLicenses.DefaultView;
            dgvLocalLicesesHistory.DataSource = _dvLocalLicenses;
            _dtAllInternationalLicense = clsInternationalLicense.GetDriverInternationalLicenses(_DriverID);
            _dvInternationalLicense = _dtAllInternationalLicense;
            dgvInternationalLicensesHistory.DataSource = _dvInternationalLicense;
            lblRecordsNumber.Text = tabControl1.SelectedTab == tpLocal ? _dvLocalLicenses.Count.ToString() : _dvInternationalLicense.Rows.Count.ToString();
        }
        private void frmLicenseHistory_Load(object sender, EventArgs e)
        {
            ctrlFindPerson1.SelectMode(_Driver.PersonID);
            _RefreshData();

        }
    }
}
