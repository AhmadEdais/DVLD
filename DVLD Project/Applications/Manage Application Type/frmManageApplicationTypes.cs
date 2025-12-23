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

namespace DVLD_Project.Manage_Application_Type
{
    public partial class frmManageApplicationTypes : Form
    {
        int _selectedRowIndex = -1;
        int _ApplicationTypeID = -1;
        DataTable _dtAllTypes;
        DataView _dvTypes;
        public frmManageApplicationTypes()
        {
            InitializeComponent();
        }
        private void _RefreshTypes()
        {
            _dtAllTypes = clsApplicationTypes.GetAllApplicationTypes();

            // Create a DataView from the DataTable
            _dvTypes = _dtAllTypes.DefaultView;

            // Bind the DataView to the grid (not the DataTable directly)
            
            dgvAllApplicationTypes.DataSource = _dvTypes;

            lblLiveNumberOfRecrords.Text = dgvAllApplicationTypes.Rows.Count.ToString();

        }
        private void btnManagePeopleClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmManageApplicationTypes_Load(object sender, EventArgs e)
        {
            _RefreshTypes();

            dgvAllApplicationTypes.Columns[0].Width = 100;
            dgvAllApplicationTypes.Columns[1].Width = 500;
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. Check if the saved index is valid
            if (_selectedRowIndex >= 0)
            {
                // 2. Get the ID directly from the saved row index
                //    (Replace "PersonID" with the actual name of your ID column)
                int ID = (int)dgvAllApplicationTypes.Rows[_selectedRowIndex].Cells["ID"].Value;

                // 3. Send just the ID to your form

                if (ID != -1)
                {
                    Form frm = new frmUpdateApplicationTypes(ID);
                    frm.ShowDialog();
                    _RefreshTypes();
                }

                // (Optional) Refresh your grid after the edit
                // RefreshPeopleData();

                // 4. Reset the index so it's not used by accident
                _selectedRowIndex = -1;
            }
        }

        private void dgvAllApplicationTypes_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                // Save the index
                _selectedRowIndex = e.RowIndex;

                // Optional: highlight the right-clicked row
                dgvAllApplicationTypes.ClearSelection();
                dgvAllApplicationTypes.Rows[e.RowIndex].Selected = true;
            }
        }

     
    }
}
