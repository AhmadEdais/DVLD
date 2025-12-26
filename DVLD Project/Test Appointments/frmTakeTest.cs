using ConsoleApp1;
using DVLD_Buisness;
using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Test_Appointments
{
    public partial class frmTakeTest : Form
    {
        clsTest _Test;
        clsTestAppointments _TestAppointment;
        int _AppointmentID = -1;
        public frmTakeTest(int AppointmentID)
        {
            InitializeComponent();
            _Test = new clsTest();
            _AppointmentID = AppointmentID;
            _TestAppointment = clsTestAppointments.Find(AppointmentID);
        }
        private void _FillFormData()
        {
            switch (_TestAppointment.TestTypeID)
            {
                case 1:
                    pbAppointment.Image = DVLD_Project.Properties.Resources.Vision_512;
                    break;
                case 2:
                    pbAppointment.Image = DVLD_Project.Properties.Resources.Written_Test_512;
                    break;
                case 3:
                    pbAppointment.Image = DVLD_Project.Properties.Resources.driving_test_512;
                    break;
            }
             lblDLDAppID.Text = _TestAppointment.LocalDrivingLicenseApplicationID.ToString();
            lblClassName.Text = clsLocalDrivingLicenseApplication.GetClassNameByLocalDrivingLicenseAppID(_TestAppointment.LocalDrivingLicenseApplicationID);
            lblName.Text = clsLocalDrivingLicenseApplication.GetApplicantFullNameByLocalDrivingLicenseAppID(_TestAppointment.LocalDrivingLicenseApplicationID);
            lblTrial.Text = clsLocalDrivingLicenseApplication.GetTestTrials(_TestAppointment.LocalDrivingLicenseApplicationID, (clsTestTypes.enTestTypeID)_TestAppointment.TestTypeID).ToString();
            lblDate.Text = _TestAppointment.AppointmentDate.ToString("dd/MM/yyyy");
            lblFees.Text = clsTestTypes.GetTestTypeFees((clsTestTypes.enTestTypeID)_TestAppointment.TestTypeID).ToString("F2");
            lblTestID.Text = "Not Taken Yet";
            rbPass.Select();
        }
        private void _SetTestDataFromForm()
        {
            _Test.TestAppointmentID = _AppointmentID;
            _Test.TestResult = rbPass.Checked ? true : false;
            _Test.Notes = rtxtNotes.Text.Trim();
            _Test.CreatedByUserID = Global.CurrentUser.UserID;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            _FillFormData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _SetTestDataFromForm();
            if(MessageBox.Show("Are you sure you want to save the test result?", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (_Test.Save())
                {
                    MessageBox.Show("Test Result Saved Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _TestAppointment.Lock();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to Save Test Result.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
