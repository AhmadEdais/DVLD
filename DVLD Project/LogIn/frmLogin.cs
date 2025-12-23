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
using System.IO;
using DVLD.Classes;

namespace DVLD_Project.LogIn
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            _LoadCredentialsFromFile();
            // Make "Enter" trigger the Save button
            this.AcceptButton = btnLogin;

            // Optional: Make "Escape" trigger the Close button
            this.CancelButton = btnExit;
        }
        
        private void _LoadCredentialsFromFile()
        {
            string filePath = "login_info.txt";

            // 1. Check if the file exists before trying to read
            if (File.Exists(filePath))
            {
                try
                {
                    // 2. Read all text from the file
                    string fileContent = File.ReadAllText(filePath);

                    // 3. Split the text using our separator '#'
                    // expected format: "username#password"
                    string[] result = fileContent.Split('#');

                    if (result.Length > 1) // Ensure we have both parts
                    {
                        txtUserName.Text = result[0];
                        txtPassword.Text = result[1];
                        ckRememberMe.Checked = true;
                    }
                }
                catch (Exception ex)
                {
                    // Ignore errors if file is corrupted
                }
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUser user = clsUser.FindByUsername(txtUserName.Text);
            if(user != null)
            {
                if(user.Password == txtPassword.Text)
                {
                    if(user.IsActive)
                    {
                        // Successful login
                        Global.CurrentUser = user;
                        
                        if(ckRememberMe.Checked)
                        {
                            // Save user credentials securely for future logins
                            clsUtil._SaveCredentialsToFile(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                        }
                        else
                        {
                            clsUtil._SaveCredentialsToFile("", "");
                        }
                        this.Hide();
                        frmMain frm = new frmMain(this); // Pass 'this' to handle logout later
                        frm.ShowDialog();
                    }
                    else
                    {
                        // User is inactive
                        MessageBox.Show("User account is inactive. Please contact support.", "Inactive Account", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    // Incorrect password
                    MessageBox.Show("Incorrect Password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
       
        public void ShowLogin()
        {
            this.Show();
            _LoadCredentialsFromFile();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
