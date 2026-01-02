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
        int _selectedRowIndex = -1;
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
            _Driver = clsDriver.Find(_DriverID);

        }

        private void btnManagePeopleClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void _SetWdth()
        {
            if(dgvLocalLicesesHistory.Rows.Count > 0)
            {
                dgvLocalLicesesHistory.Columns["Lic.ID"].Width = 100;
                dgvLocalLicesesHistory.Columns["App.ID"].Width = 100;
                dgvLocalLicesesHistory.Columns["Class Name"].Width = 250;
                dgvLocalLicesesHistory.Columns["Issue Date"].Width = 160;
                dgvLocalLicesesHistory.Columns["Expiration Date"].Width = 160;
                dgvLocalLicesesHistory.Columns["Is Active"].Width = 120;

            }
            if (dgvInternationalLicensesHistory.Rows.Count > 0)
            {
                dgvInternationalLicensesHistory.Columns["Int.License ID"].Width = 120;
                dgvInternationalLicensesHistory.Columns["App. ID"].Width = 100;
                dgvInternationalLicensesHistory.Columns["L.License ID"].Width = 120;
                dgvInternationalLicensesHistory.Columns["Issue Date"].Width = 160;
                dgvInternationalLicensesHistory.Columns["Expiration Date"].Width = 160;
                dgvInternationalLicensesHistory.Columns["Is Active"].Width = 120;

            }
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
            _SetWdth();


        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(_selectedRowIndex >= 0)
            {
                int LicenseID = Convert.ToInt32(dgvLocalLicesesHistory.Rows[_selectedRowIndex].Cells["Lic.ID"].Value);
                Form frm = new frmShowLicense(LicenseID);
                frm.ShowDialog();
            }
        }

        private void dgvLocalLicesesHistory_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                // Save the index
                _selectedRowIndex = e.RowIndex;

                // Optional: highlight the right-clicked row
                dgvLocalLicesesHistory.ClearSelection();
                dgvLocalLicesesHistory.Rows[e.RowIndex].Selected = true;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (_selectedRowIndex >= 0)
            {
                int LicenseID = Convert.ToInt32(dgvInternationalLicensesHistory.Rows[_selectedRowIndex].Cells["Int.License ID"].Value);
                Form frm = new frmShowInternationalLicense(LicenseID);
                frm.ShowDialog();
            }
        }

        private void dgvInternationalLicensesHistory_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                // Save the index
                _selectedRowIndex = e.RowIndex;

                // Optional: highlight the right-clicked row
                dgvLocalLicesesHistory.ClearSelection();
                dgvLocalLicesesHistory.Rows[e.RowIndex].Selected = true;
            }
        }
    }
}
