using Group1_POS.models.Method;
using Group1_POS.models.Role;
using Group1_POS.models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Group1_POS.Views
{
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
        }
        Role role;
        User user;
        private void txtRole_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {

            if (HandleLogic.EmptytextBox(txtUserName,txtEmail,textPass))
            {
                return;
            }
            user = new User();
            user.UserName = txtUserName.Text.Trim();
            user.Gender = cboGender.Text.Trim();
            user.Email = txtEmail.Text.Trim();
            user.Password = textPass.Text.Trim();
            if (rTrue.Checked)
            {
                user.Status = true;
            }
            else
            {
                user.Status = false;
            }
            user.RoleId = user.GetRoleId(cboRoleName);
            user.createRole();
            HandleLogic.ClearTextBox(txtUserName,txtEmail,textPass);
            HandleLogic.ClearComboBox(cboGender, cboRoleName);
            
        }

        private void RoleForm_Load(object sender, EventArgs e)
        {
            user = new User();
            user.SetRoleName(cboRoleName);
            user.getDataGrid(dg: dgUser);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (HandleLogic.EmptytextBox(txtUserName))
            {
                return;
            }
            user = new User();
            user.UserName = txtUserName.Text.Trim();
            user.Gender = cboGender.Text.Trim();
            user.Email = txtEmail.Text.Trim();
            user.Password = textPass.Text.Trim();
            if (rTrue.Checked)
            {
                user.Status = true;
            }
            else
            {
                user.Status = false;
            }
            user.RoleId = user.GetRoleId(cboRoleName);
            user.update(dg: dgUser);
            HandleLogic.ClearTextBox(txtUserName);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            HandleLogic.ClearTextBox(txtUserName);
            user = new User();
            user.deleted(dg: dgUser);
            HandleLogic.ClearTextBox(txtUserName);

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            HandleLogic.ClearTextBox(txtUserName);
            user = new User();
            user.UserName = txtUserName.Text.Trim();
            user.SearchData(dg: dgUser);
            HandleLogic.ClearTextBox(txtUserName);

        }

        private void dgRole_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            user = new User();
            user.TranferToControls(dg: dgUser, txtUserName ,  cboGender,  txtEmail, textPass, cboRoleName, rTrue, rFalse);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            DashBoard main = new DashBoard();
            main.Show();
            this.Hide();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)13)
            {
                User user = new User();
            user.UserName = txtSearch.Text.Trim();
                user.SearchData(dg: dgUser);
            }
        }
    }
}
