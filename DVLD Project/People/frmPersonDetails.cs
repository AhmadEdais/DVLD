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
    public partial class frmPersonDetails : Form
    {
        int _PersonID = -1;
        public frmPersonDetails(int PersonID)
        {
            InitializeComponent();
            _PersonID = PersonID;
            ctrlPersonDetails1.CloseRequest += CloseForm;
            ctrlPersonDetails1.SetID(PersonID);
        }
        private void CloseForm(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ctrlPersonDetails1_Load(object sender, EventArgs e)
        {

        }

        private void frmPersonDetails_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llblEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPersonInfo frm = new frmAddEditPersonInfo(_PersonID);
            frm.ShowDialog();
        }
    }
}
