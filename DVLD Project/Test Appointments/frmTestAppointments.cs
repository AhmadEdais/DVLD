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
using static ConsoleApp1.clsTestTypes;

namespace DVLD_Project.Test_Appointments
{
    public partial class frmTestAppointments : Form
    {
        int _LocalDrivingLicenseApplicationID = -1;
        clsTestTypes.enTestTypeID _enTestType = clsTestTypes.enTestTypeID.Vision;
        private int _selectedRowIndex = -1;
        DataTable _dtAllAppointments;
        DataView _dvAppointments;
        public frmTestAppointments(int LocalDrivingLicenseApplicationID, clsTestTypes.enTestTypeID enTestType)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _enTestType = enTestType;
            ctrlLocalDriverLicenseApplicationInfo1.SetApplicationID(_LocalDrivingLicenseApplicationID);
        }

        private void _SetSettings(clsTestTypes.enTestTypeID enTestType)
        {
            switch (enTestType)
            {
                case clsTestTypes.enTestTypeID.Vision:
                    pbTestAppoitment.Image = DVLD_Project.Properties.Resources.Vision_512;
                    lblTestAppointmentName.Text = "Vision Test Appointments";
                    this.Text = "Vision Test Appointments";
                    break;
                case clsTestTypes.enTestTypeID.Written:
                    pbTestAppoitment.Image = DVLD_Project.Properties.Resources.Written_Test_512;
                    lblTestAppointmentName.Text = "Written Test Appointments";
                    this.Text = "Written Test Appointments";
                    break;
                case clsTestTypes.enTestTypeID.Road:
                    pbTestAppoitment.Image = DVLD_Project.Properties.Resources.driving_test_512;
                    lblTestAppointmentName.Text = "Street Test Appointmenst";
                    this.Text = "Street Test Appointments";
                    break;
            }
        }
        private void _RefreshAppointmentList(clsTestTypes.enTestTypeID enTestType)
        {
            _dtAllAppointments = clsTestAppointments.GetApplicationTestAppointmentsPerTestType(_LocalDrivingLicenseApplicationID, enTestType);
            _dvAppointments = _dtAllAppointments.DefaultView;
            dgvAllAppointments.DataSource = _dvAppointments;
            lblRecordsNumber.Text = dgvAllAppointments.Rows.Count.ToString();
        }
        private void frmTestAppointments_Load(object sender, EventArgs e)
        {
            _SetSettings(_enTestType);
            _RefreshAppointmentList(_enTestType);
        }

        private void btnManagePeopleClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNewAppointment_Click(object sender, EventArgs e)
        {
            if (clsTestAppointments.IsThereAnActiveScheduledTest(_LocalDrivingLicenseApplicationID, (int)_enTestType))
            {
                MessageBox.Show("There is already an active scheduled Road Test for this application.", "Cannot Schedule", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form frm = new frmAddEditTestAppointment(_LocalDrivingLicenseApplicationID, _enTestType);
            frm.ShowDialog();
            _RefreshAppointmentList(_enTestType);
        }
    }
}
