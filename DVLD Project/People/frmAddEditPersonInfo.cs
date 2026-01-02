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

namespace DVLD_Project
{
    public partial class frmAddEditPersonInfo : Form
    {
        int _PersonID = -1;
        // Declare a delegate
        public delegate void DataBackEventHandler(object sender, int PersonID);

        // Declare an event using the delegate
        public event DataBackEventHandler DataBack;
        public frmAddEditPersonInfo(int PersonID = -1)
        {
            InitializeComponent();

            ctrlAddUpdatePerson1.CloseRequest += CloseForm;
            ctrlAddUpdatePerson1.DataBack += DataBackEvent; // Subscribe to the event
            ctrlAddUpdatePerson1.SelectMode(PersonID);

                       
        }
       private void CloseForm(object sender, EventArgs e)
       {
            if (_PersonID != -1)
            {
                DataBack?.Invoke(this, _PersonID);
            }
            this.Close();
       }
        private void DataBackEvent(object sender, int PersonID)
        {

            // Handle the data received
            _PersonID = PersonID;
        }
        private void ctrlAddUpdatePerson1_Load(object sender, EventArgs e)
        {
            
        }

        private void frmAddEditPersonInfo_Load(object sender, EventArgs e)
        {

        }

        private void frmAddEditPersonInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Raise the DataBack event to pass the PersonID back to the caller
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
