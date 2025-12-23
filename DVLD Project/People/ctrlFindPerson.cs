using ConsoleApp1;
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

namespace DVLD_Project.People
{
    public partial class ctrlFindPerson : UserControl
    {
        int _PersonID = -1;
        int _UserID = -1;
        public enum enMode { AddNew = 0, Update = 1 };
        private enMode _Mode;
        //Declare a delegate
        public delegate void DataBackEventHandler(object sender, int PersonID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;
        // Define a custom event handler delegate with parameters
        public event Action<int> OnPersonSelected;
        // Create a protected method to raise the event with a parameter
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            {
                handler(PersonID); // Raise the event with the parameter
            }
        }


        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get
            {
                return _ShowAddPerson;
            }
            set
            {
                _ShowAddPerson = value;
                btnAdd.Visible = _ShowAddPerson;
            }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilteredBy.Enabled = _FilterEnabled;
            }
        }
        public ctrlFindPerson()
        {
            InitializeComponent();
        }
        private void _FillComboBox()
        {
            // Fill ComboBoxes if needed
        }
        private void _SetSettings()
        {
            if(_Mode == enMode.AddNew)
            {
                
                txtFindBy.Visible = true;
                ctrlPersonDetails1.EditLinkVisible = true;
            }
            else
            {
                txtFindBy.Text = _PersonID.ToString();
                FindNow();
                gbFilteredBy.Enabled = false;

            }


        }
        private void _ValidateID(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox currentTxtBox = (TextBox)sender;
            if (string.IsNullOrEmpty(currentTxtBox.Text))
            {
                errorProvider1.SetError(currentTxtBox, "This field is required");
                e.Cancel = true;
            }
            else
                if (!currentTxtBox.Text.All(c => char.IsDigit(c)))
            {
                errorProvider1.SetError(currentTxtBox, "This field can contain only numbers");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(currentTxtBox, null);
                e.Cancel = false;
            }

        }
        private void ctrlFindPerson_Load(object sender, EventArgs e)
        {
            _SetSettings();
        }
        public void SelectMode(int PersonID)
        {
            _PersonID = PersonID;
            if (_PersonID == -1)
            {
                _Mode = enMode.AddNew;

            }
            else
            {
                _Mode = enMode.Update;
            }
        }
        public void LoadPersonInfo(int PersonID)
        {

            cbFindBy.SelectedIndex = 1;
            txtFindBy.Text = PersonID.ToString();
            FindNow();

        }

        private void cbFindBy_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if(cbFindBy.SelectedItem.ToString() == "Person ID" || cbFindBy.SelectedItem.ToString() == "National NO")
            {
                txtFindBy.Visible = true;
                txtFindBy.Clear();
            }
            else
            {
                txtFindBy.Visible = false;
                txtFindBy.Clear();
            }
        }
        private void FindNow()
        {
            switch (cbFindBy.Text)
            {
                case "Person ID":
                    _PersonID = int.Parse(txtFindBy.Text);
                    ctrlPersonDetails1.SetID(_PersonID);
                    DataBack?.Invoke(this, _PersonID);

                    break;

                case "National NO":
                    _PersonID = clsPerson.FindPersonByNationalNO(txtFindBy.Text).ID;
                    ctrlPersonDetails1.SetID(_PersonID);
                    DataBack?.Invoke(this, _PersonID);
                    break;

                default:
                    break;
            }

            if (OnPersonSelected != null && FilterEnabled)
                // Raise the event with a parameter
                OnPersonSelected(ctrlPersonDetails1.PersonID);
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            if (string.IsNullOrEmpty(txtFindBy.Text.Trim()))
            {
                MessageBox.Show("Please enter a value to search.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FindNow();
        }
        private void ctrlPersonDetails1_Load(object sender, EventArgs e)
        {
            cbFindBy.SelectedIndex = 0;
            txtFindBy.Focus();
        }
        private void txtFindBy_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtFindBy.Text.Trim()))
            {
                //e.Cancel = true;
                errorProvider1.SetError(txtFindBy, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtFindBy, null);
            }
        }
        
        
        
        private void txtFindBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
            {
                btnFind.PerformClick(); 
            }

            //this will allow only digits if person id is selected
            if (cbFindBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddEditPersonInfo frm1 = new frmAddEditPersonInfo();
            frm1.DataBack += DataBackEvent; // Subscribe to the event
            frm1.ShowDialog();


        }
        private void DataBackEvent(object sender, int PersonID)
        {
            // Handle the data received

            cbFindBy.SelectedIndex = 1;
            txtFindBy.Text = PersonID.ToString();
            ctrlPersonDetails1.SetID(PersonID);
            _PersonID = PersonID;
            // --- ADD THIS LINE ---
            // Ensure the parent form gets notified even when adding a new person
            if (OnPersonSelected != null && FilterEnabled)
            {
                OnPersonSelected(_PersonID);
            }
        }

    }
}
