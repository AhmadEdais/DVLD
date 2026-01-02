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
    public partial class ctrlFindLicense : UserControl
    {
        // 1. Define the Delegate
        public delegate void LicenseSelectedEventHandler(int LicenseID);

        // 2. Define the Event
        public event LicenseSelectedEventHandler OnLicenseSelected;

        private int _LicenseID = -1;
        private clsLicense _SelectedLicense;
        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set
            { 
               _FilterEnabled = value;
                gbFilter.Enabled = _FilterEnabled;

            }
        }
        public int LicenseID
        {
            get { return _LicenseID; }
        }
        public clsLicense SelectedLicenseInfo
        {
            get { return _SelectedLicense; }
        }
        public ctrlFindLicense()
        {
            InitializeComponent();
        }

        public void txtLicenseFocus()
        {
            txtFilterBy.Focus();
        }
        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the key is NOT a digit AND NOT a control key (like Backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                // If it's not a number, handle the event so the character is NOT entered
                e.Handled = true;
            }
            // Check if the pressed key is Enter (Character Code 13)
            if (e.KeyChar == (char)13)
            {
                btnFindLicense.PerformClick(); // Triggers the button click
                e.Handled = true;       // Stops the "ding" sound
            }
        }
        public void SetID(int ID)
        {
            txtFilterBy.Text = ID.ToString();
            btnFindLicense.PerformClick();
        }
        
        private void btnFindLicense_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtFilterBy.Text))
            {
                MessageBox.Show("Please enter a License ID to search.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(!clsLicense.IsLicenseExist(int.Parse(txtFilterBy.Text)))
            {
                MessageBox.Show("No license found with the provided License ID.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ctrlLicenseInfo1.SetLicenseID(int.Parse(txtFilterBy.Text));
            // Save the ID locally
            _LicenseID = int.Parse(txtFilterBy.Text);
            _SelectedLicense =  clsLicense.Find(_LicenseID);

            // 3. Trigger the Event if a valid license was found
            if (_LicenseID != -1 && OnLicenseSelected != null)
            {
                // This sends the ID back to the Parent Form
                OnLicenseSelected(_LicenseID);
            }
        }
        public void ResetControl()
        {
            txtFilterBy.Text = "";
            ctrlLicenseInfo1.SetLicenseID(-1);
            _LicenseID = -1;
            _SelectedLicense = null;
        }

        private void ctrlFindLicense_Load(object sender, EventArgs e)
        {

        }
    }
}
