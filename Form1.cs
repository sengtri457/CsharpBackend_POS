using Group1_POS.models;
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

namespace Group1_POS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }

        Role role;
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (GeneralFun.EmptytextBox(txtRole) == true)
            {
                return;
            }
            role = new Role();
            role.RoleName = txtRole.Text.Trim();
            role.createRole();
            GeneralFun.ClearTextBox(txtRole);
            txtRole.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
           
        }
    }
}
