using ConsoleApp1;
using DVLD_Project.Manage_Application_Type;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Applications.Manage_Test_Types
{
    public partial class frmListTestTypes : Form
    {
        int _selectedRowIndex = -1;
        int _TestID = -1;
        DataTable _dtAllTests;
        DataView _dvTests;
        public frmListTestTypes()
        {
            InitializeComponent();
        }
        private void _RefreshTests()
        {
            _dtAllTests =clsTestTypes.GetAllTestTypes();

            // Create a DataView from the DataTable
            _dvTests = _dtAllTests.DefaultView;

            // Bind the DataView to the grid (not the DataTable directly)

            dgvTestTypes.DataSource = _dvTests;

            lblLiveNumberOfRecrords.Text = dgvTestTypes.Rows.Count.ToString();

        }
        private void btnManagePeopleClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListTestTypes_Load(object sender, EventArgs e)
        {
            _RefreshTests();
            if(dgvTestTypes.Rows.Count > 0)

            { 
                dgvTestTypes.Columns[0].Width = 100;
                dgvTestTypes.Columns[1].Width = 200;
                dgvTestTypes.Columns[2].Width = 600;
                dgvTestTypes.Columns[3].Width = 120;
            }
        }

        private void dgvTestTypes_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                // Save the index
                _selectedRowIndex = e.RowIndex;

                // Optional: highlight the right-clicked row
                dgvTestTypes.ClearSelection();
                dgvTestTypes.Rows[e.RowIndex].Selected = true;
            }
        }

        private void editTypeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. Check if the saved index is valid
            if (_selectedRowIndex >= 0)
            {
                // 2. Get the ID directly from the saved row index
                //    (Replace "PersonID" with the actual name of your ID column)
                int ID = (int)dgvTestTypes.Rows[_selectedRowIndex].Cells["ID"].Value;

                // 3. Send just the ID to your form

                if (ID != -1)
                {
                    Form frm = new frmUpdateTestType(ID);
                    frm.ShowDialog();
                    _RefreshTests();
                }

                // (Optional) Refresh your grid after the edit
                // RefreshPeopleData();

                // 4. Reset the index so it's not used by accident
                _selectedRowIndex = -1;
            }
        }
    }
}
