using ConsoleApp1;
using DVLD_Project.Properties;
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

namespace DVLD_Project
{
    public partial class ctrlPersonDetails : UserControl
    {

        public event EventHandler CloseRequest;

        clsPerson _Person; 
        int _PersonID = -1;
        public int PersonID
        {
            get { return _PersonID; }
        }
        public ctrlPersonDetails()
        {
            InitializeComponent();
        }
        public bool EditLinkVisible
        {
            get
            {
                // llEditPersonInfo is the name of your "Edit Person Info" LinkLabel
                return llblEditPersonInfo.Visible;
            }
            set
            {
                llblEditPersonInfo.Visible = value;
            }
        }
        private void _LoadInfo(int PersonID)
        {
            _Person = clsPerson.FindPersonByID(PersonID);
            if (_Person != null)
            {
                lblPersonID.Text = _Person.ID.ToString();
                lblPersonName.Text = _Person.FirstName + " " + _Person.SecondName + " " + _Person.ThirdName + " " + _Person.LastName;
                lblNationalNO.Text = _Person.NationalNO;
                lblGendor.Text = (_Person.Gendor == 1) ? "Female" : "Male";
                pbGendor.Image = (_Person.Gendor == 1) ? Resources.Woman_32:Resources.Man_32;
                lblEmail.Text = _Person.Email;
                lblAddress.Text = _Person.Address;
                lblDateOfBirth.Text = _Person.DateOfBirth.ToString("dd'/'MM'/'yyyy");
                lblPhone.Text = _Person.Phone;
                lblCountry.Text = Country.GetCountryNameByNationalityID(_Person.NatoinalityCountryID);
                if(_Person.ImagePath == "")
                {
                    pbImage.Image = (_Person.Gendor == 1) ? Resources.Female_512 : Resources.Male_512;
                }
                else
                {
                    pbImage.Load(_Person.ImagePath);
                }

            }
        }
        public void SetID(int PersonID)
        {
            _PersonID = PersonID;
            _LoadInfo(PersonID);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }

        private void llblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo(_PersonID);
            frm.ShowDialog();
            _LoadInfo(_PersonID);
        }

        private void ctrlPersonDetails_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
