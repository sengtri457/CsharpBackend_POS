using Group1_POS.models.Method;
using Group1_POS.models.Role;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Group1_POS.Views
{
    public partial class RoleForm : Form
    {
        public RoleForm()
        {
            InitializeComponent();
        }
        Role role;
        private void txtRole_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {

            if (HandleLogic.EmptytextBox(txtRole))
            {
                return;
            }
            role = new Role();
            role.RoleName = txtRole.Text.Trim();
            role.createRole();
            HandleLogic.ClearTextBox(txtRole);
        }

        private void RoleForm_Load(object sender, EventArgs e)
        {
            role = new Role();
            role.getDataGrid(dg: dgRole);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (HandleLogic.EmptytextBox(txtRole))
            {
                return;
            }
            role = new Role();
            role.RoleName = txtRole.Text.Trim();
            role.update(dg: dgRole);
            HandleLogic.ClearTextBox(txtRole);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            HandleLogic.ClearTextBox(txtRole);
            role = new Role();
            role.deleted(dg: dgRole);
            HandleLogic.ClearTextBox(txtRole);

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            HandleLogic.ClearTextBox(txtRole);
            role = new Role();
            role.RoleName = txtRole.Text.Trim();
            role.SearchData(dg: dgRole);
            HandleLogic.ClearTextBox(txtRole);

        }

        private void dgRole_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            role = new Role();
            role.TranferToControls(dg: dgRole, txtRole);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            role = new Role();
            role.SearchData(dg: dgRole);
            
        }
    }
}
