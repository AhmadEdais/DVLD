using ConsoleApp1;
using DVLD_Project.Properties;
using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using DVLD.Classes;
using System.IO;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class ctrlAddUpdatePerson : UserControl
    {
        // Declare a delegate
        public delegate void DataBackEventHandler(object sender, int PersonID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;
        public event EventHandler CloseRequest;
        public enum enMode { AddNew = 0, Update = 1 };
        public enum enImageMode {Set = 0, Empty = 1};
        private enMode _Mode;
        private enImageMode _enImageMode = enImageMode.Empty;
     
        int _PersonID;
        clsPerson _Person;
        private void _FillCountriesInComoboBox()
        {
            DataTable dtCountries = Country.GetAllCountries();

            foreach (DataRow row in dtCountries.Rows)
            {

                cbCountries.Items.Add(row["CountryName"]);

            }

        }
        private void _SetSettings()
        {
            cbCountries.SelectedItem = "Jordan";
            dtDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            rbMale.Select();




        }
        private void _LoadInfo()
        {
            if (_Mode == enMode.AddNew)
            {
                lblAddEditPerson.Text = "Add New Person";
                _Person = new clsPerson();
                return;
            }
            _Person = clsPerson.Find(_PersonID);
            lblAddEditPerson.Text = "Edit Person ID " + _Person.ID.ToString();


            lblPersonID.Text = _Person.ID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            if (_Person.ThirdName != "")

            {
                txtThirdName.Text = _Person.ThirdName;
            }
            txtLastName.Text = _Person.LastName;
            txtNationallNO.Text = _Person.NationalNO;
            rtxtAddress.Text = _Person.Address;
            if (_Person.Email != "")
            {
                txtEmail.Text = _Person.Email;
            }
            dtDateOfBirth.Value = _Person.DateOfBirth;
            txtPhone.Text = _Person.Phone;
            cbCountries.SelectedText = Country.GetCountryNameByNationalityID(_Person.NatoinalityCountryID);
            if (_Person.ImagePath != "")
            {
                pbSelectImage.Load(_Person.ImagePath);
                llRemove.Visible = true;
            }
            if(_Person.Gendor == 0)
            {
                rbMale.Select();
            }
            else
            {
                rbFemale.Select();
            }
        }
        private void _ValidateRequiredAndLetters(object sender,System.ComponentModel.CancelEventArgs e)
        {
            TextBox currentTxtBox = (TextBox)sender;
            if (string.IsNullOrEmpty(currentTxtBox.Text))
            {
                errorProvider1.SetError(currentTxtBox, "This field is required");
                e.Cancel = true;
            }
            else
                if(!currentTxtBox.Text.All(c => char.IsLetter(c) ))
            {
                errorProvider1.SetError(currentTxtBox, "This field can only contain letters.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(currentTxtBox, null);
                e.Cancel = false;
            }

        }
        private void _ValidateNullableOrLetters(object sender,System.ComponentModel.CancelEventArgs e)
        {
            TextBox currentTxtBox = (TextBox)sender;
            if (string.IsNullOrEmpty(currentTxtBox.Text))
            {
                errorProvider1.SetError(currentTxtBox, null);
                e.Cancel = false;
            }
            else
                if (!currentTxtBox.Text.All(c => char.IsLetter(c)))
            {
                errorProvider1.SetError(currentTxtBox, "This field can only contain letters.");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(currentTxtBox, null);
                e.Cancel = false;
            }

        }
        private void _ValidateLastName(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox currentTxtBox = (TextBox)sender;
            if (string.IsNullOrEmpty(currentTxtBox.Text))
            {
                errorProvider1.SetError(currentTxtBox, "This field is required");
                e.Cancel = true;
            }
            else
                if (!currentTxtBox.Text.All(c => char.IsLetter(c) || char.IsWhiteSpace(c) || c == '-' || c == '_'))
            {
                errorProvider1.SetError(currentTxtBox, "This field contains invalid characters. (Allowed: a-z, -, _)"); e.Cancel = true;
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(currentTxtBox, null);
                e.Cancel = false;
            }

        }
        private void _ValidateNationalNO(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox currentTxtBox = (TextBox)sender;
            if (string.IsNullOrEmpty(currentTxtBox.Text))
            {
                errorProvider1.SetError(currentTxtBox, "This field is required");
                e.Cancel = true;
                return;
            }
            
            if (clsPerson.isPersonExistsByNationalNO(txtNationallNO.Text.Trim()) && _Person.NationalNO !=txtNationallNO.Text.Trim())
            {
                errorProvider1.SetError(currentTxtBox, "This NationalNO is used by another person");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(currentTxtBox, null);
                e.Cancel = false;
            }
        }
        private void _ValidatePhoneNumber(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox currentTxtBox = (TextBox)sender;
            if (string.IsNullOrEmpty(currentTxtBox.Text))
            {
                errorProvider1.SetError(currentTxtBox, "This field is required");
                e.Cancel = true;
            }
            else
                if (!currentTxtBox.Text.All(c => char.IsDigit(c) ))
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
        private void _ValidateNotEmpty(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox currentTxtBox = (TextBox)sender;
            if (string.IsNullOrEmpty(currentTxtBox.Text))
            {
                errorProvider1.SetError(currentTxtBox, "This field is required");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(currentTxtBox, null);
                e.Cancel = false;
            }
        }
        private void _ValidateRichTextBoxNotEmpty(object sender, CancelEventArgs e)
        {
            RichTextBox currentTxtBox = (RichTextBox)sender;
            if (string.IsNullOrEmpty(currentTxtBox.Text))
            {
                errorProvider1.SetError(currentTxtBox, "This field is required");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(currentTxtBox, null);
                e.Cancel = false;
            }
        }
        private void _ValidateEmail(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox currentTxtBox = (TextBox)sender;
            if (string.IsNullOrEmpty(currentTxtBox.Text))
            {
                errorProvider1.SetError(currentTxtBox, null);
                e.Cancel = false;
                return;
            }
            // --- Rule 2: Check format ---
            // Create an instance of the attribute
            var emailValidator = new EmailAddressAttribute();

            // Use its IsValid() method, which returns true or false.
            if (!emailValidator.IsValid(currentTxtBox.Text))
            {
                errorProvider1.SetError(currentTxtBox, "Please enter a valid email address (e.g., user@example.com)");
                e.Cancel = true;
            }
            else
            {
                // It's valid!
                errorProvider1.SetError(currentTxtBox, null);
                e.Cancel = false;
            }

        }
        /// <summary>
        /// Copies a file to the "Images" folder and returns the new full path.
        /// Returns null if the copy fails.
        /// </summary>
        private string _CopyImageToProjectFolder(string sourceFilePath)
        {
            // 1. Define the destination folder (e.g., "Images" folder in the app's directory)
            string destinationFolder = @"C:\DVLD_Images";

            // 2. Create the destination folder if it doesn't exist
            if (!Directory.Exists(destinationFolder))
            {
                try
                {
                    Directory.CreateDirectory(destinationFolder);
                    Directory.CreateDirectory(destinationFolder);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error creating directory: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }

            // 3. Create a new, unique filename to avoid overwriting files
            // (e.g., "e3b0c442-98fc-1c14-9afd-b0e266312177.png")
            string newFileName = Guid.NewGuid().ToString() + Path.GetExtension(sourceFilePath);

            // 4. Combine the folder and new filename to get the full destination path
            string destinationFilePath = Path.Combine(destinationFolder, newFileName);

            // 5. Copy the file
            try
            {
                File.Copy(sourceFilePath, destinationFilePath);

                // 6. Return the new full path
                return destinationFilePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error copying file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        private bool _HandlePersonImage()
        {
            if(_Person.ImagePath != pbSelectImage.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    try
                    {
                        //File.Delete(_Person.ImagePath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error deleting old image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                if (pbSelectImage.ImageLocation != null)
                {
                    string SourceImageFile = pbSelectImage.ImageLocation.ToString();
                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        pbSelectImage.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                }

            }
            return true;
        }
        public ctrlAddUpdatePerson()
        {
            InitializeComponent();
            
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
        private void ctrlAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _FillCountriesInComoboBox();
            _SetSettings();
            _LoadInfo();


        }

        private void btnManagePeopleClose_Click(object sender, EventArgs e)
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                // 2. If validation fails, show a message and stop
                MessageBox.Show("Invalid Inputs, Please fix the fields marked with the ⓘ error icon.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);

                // 3. Stop the save method right here.
                return;
            }
            if (!_HandlePersonImage())
                return;
            short CountryID = Country.GetCountryIdByCountryName(cbCountries.Text);
            _Person.FirstName = txtFirstName.Text;
            _Person.SecondName = txtSecondName.Text;
            _Person.ThirdName = txtThirdName.Text;
            _Person.LastName = txtLastName.Text;
            _Person.Email = txtEmail.Text;
            _Person.Phone = txtPhone.Text;
            _Person.NationalNO = txtNationallNO.Text;
            _Person.NatoinalityCountryID = CountryID;
            _Person.Address = rtxtAddress.Text;
            _Person.DateOfBirth = dtDateOfBirth.Value;
            if(rbMale.Checked)
            _Person.Gendor = 0 ;
            else
            _Person.Gendor = 1 ;
            if (pbSelectImage.Image != null)
            {
                _Person.ImagePath = pbSelectImage.ImageLocation;
            }
            else
                _Person.ImagePath = "";

            if (_Person.Save())
            {
                MessageBox.Show("Data Saved Successfully.","Saved",MessageBoxButtons.OK,MessageBoxIcon.Information);
                lblAddEditPerson.Text = "Edit Person ID " + _Person.ID.ToString();
                lblPersonID.Text = _Person.ID.ToString();
                DataBack?.Invoke(this, _Person.ID);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.");

            _Mode = enMode.Update;

           
            
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (_enImageMode == enImageMode.Empty)
            {
                pbSelectImage.Image = Resources.Male_512;
            }
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (_enImageMode == enImageMode.Empty)
            {
                pbSelectImage.Image = Resources.Female_512;
            }
        }

        private void linklblSelectImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string originalFilePath = openFileDialog1.FileName;
                pbSelectImage.ImageLocation = originalFilePath;
                pbSelectImage.Tag = originalFilePath;
                llRemove.Visible = true;
                _enImageMode = enImageMode.Set;



            }
        }

        private void llRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbSelectImage.ImageLocation = null;



            if (rbMale.Checked)
                pbSelectImage.Image = Resources.Male_512;
            else
                pbSelectImage.Image = Resources.Female_512;

            llRemove.Visible = false;


        }

        private void cbCountries_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
