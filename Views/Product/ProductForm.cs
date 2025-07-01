using Group1_POS.models.Method;
using Group1_POS.models.Product;
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
    public partial class ProductForm : Form
    {
        public ProductForm()
        {
            InitializeComponent();
        }
        Role role;
        User user;
        Product product;
        private void txtRole_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {

            if (HandleLogic.EmptytextBox(txtName,txtBarcode,txtsell))
            {
                return;
            }
            product = new Product();
            product.Name = txtName.Text.Trim();
            product.Barcode = txtBarcode.Text.Trim();   
            product.SellPrice = double.Parse(txtsell.Text.Trim());
            product.QtyInstock = int.Parse(txtQty.Text.Trim());
            product.CategoryId = int.Parse(txtCategoryId.Text.Trim());
            product.Photo = txtPhoto.Text.Trim();
            product.createRole();
            HandleLogic.ClearTextBox(txtName,txtBarcode,txtsell);
            
        }

        private void RoleForm_Load(object sender, EventArgs e)
        {
            user = new User();
            user.SetRoleName(cboRoleName);
            user.getDataGrid(dg: dgUser);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (HandleLogic.EmptytextBox(txtName))
            {
                return;
            }
            user = new User();
            user.UserName = txtName.Text.Trim();
            user.update(dg: dgUser);
            HandleLogic.ClearTextBox(txtName);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            HandleLogic.ClearTextBox(txtName);
            user = new User();
            user.deleted(dg: dgUser);
            HandleLogic.ClearTextBox(txtName);

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            HandleLogic.ClearTextBox(txtName);
            user = new User();
            user.UserName = txtName.Text.Trim();
            user.SearchData(dg: dgUser);
            HandleLogic.ClearTextBox(txtName);

        }

        private void dgRole_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            user = new User();
          /*  user.TranferToControls(dg: dgUser, txtName);*/
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            DashBoard main = new DashBoard();
            main.Show();
            this.Hide();
            DashBoard das = new DashBoard();
            user = new User();
            das.AdminLabel.Text = "User Management" + user.UserName;
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

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
