using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Divers
{
    public partial class frmListDrivers : Form
    {
        DataTable _dtAllDrivers;
        DataView _dvDrivers;
        public frmListDrivers()
        {
            InitializeComponent();
        }

        private void btnManagePeopleClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void _RefreshData()
        {
            _dtAllDrivers = clsDriver.GetAllDrivers();
            _dvDrivers = _dtAllDrivers.DefaultView;
            dgvAllDrivers.DataSource = _dvDrivers;
            lblRecordsNumber.Text = _dvDrivers.Count.ToString();
        }
        private void _HideFilterControls()
        {
            txtFilterByValue.Visible = false;
        }

        private void frmListDrivers_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedItem = "None";
            _HideFilterControls();
            _RefreshData();
            if (dgvAllDrivers.Rows.Count > 0)
            {
                dgvAllDrivers.Rows[0].Selected = true;
                dgvAllDrivers.Columns["Driver ID"].Width = 80;
                dgvAllDrivers.Columns["Person ID"].Width = 80;
                dgvAllDrivers.Columns["National No"].Width = 120;
                dgvAllDrivers.Columns["Full Name"].Width = 250;
                dgvAllDrivers.Columns["Date"].Width = 150;
                dgvAllDrivers.Columns["Active Licenses"].Width = 170;
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            _RefreshData();
            txtFilterByValue.Text = "";
            
            if (cbFilterBy.SelectedItem.ToString() == "None")
            {
                _HideFilterControls();
            }

            else
            {
                txtFilterByValue.Visible = true;

            }
        }

        private void txtFilterByValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            // 1. Map the selection from ComboBox to the actual Database Column Name
            switch (cbFilterBy.Text)
            {
                case "Driver ID":
                    FilterColumn = "Driver ID"; // Matches SQL Alias [Driver ID]
                    break;

                case "Person ID":
                    FilterColumn = "Person ID"; // Matches SQL Alias [Person ID]
                    break;

                case "National No.":
                    FilterColumn = "National No"; // Matches SQL Alias [National No]
                    break;

                case "Full Name":
                    FilterColumn = "Full Name";   // Matches SQL Alias [Full Name]
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            // 2. Reset Logic (If text is empty or "None" is selected)
            if (txtFilterByValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dvDrivers.RowFilter = "";
                lblRecordsNumber.Text = dgvAllDrivers.Rows.Count.ToString();
                return;
            }

            // 3. Apply Filter
            // "Driver ID" and "Person ID" are Numbers, the rest are Strings
            if (FilterColumn == "Driver ID" || FilterColumn == "Person ID")
            {
                // Numeric Filter Safety Check
                if (int.TryParse(txtFilterByValue.Text.Trim(), out int id))
                {
                    _dvDrivers.RowFilter = string.Format("[{0}] = {1}", FilterColumn, id);
                }
                else
                {
                    _dvDrivers.RowFilter = ""; // Clear filter if user types text in a number field
                }
            }
            else
            {
                // String Filter (National No, Full Name)
                _dvDrivers.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterByValue.Text.Trim());
            }

            // 4. Update the Label Count
            lblRecordsNumber.Text = dgvAllDrivers.Rows.Count.ToString();
        }
    }
}
