using Group1_POS.models.Category;
using Group1_POS.models.Method;
using Group1_POS.models.Product;
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
    public partial class CategoryForm : Form
    {
        public CategoryForm() => InitializeComponent();
        Category category;
        private void txtRole_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {

            if (HandleLogic.EmptytextBox(txtCategory))
            {
                return;
            }
            category = new Category();
            category.CategoryName = txtCategory.Text.Trim();
            category.createRole(dg: dgCategory);
            HandleLogic.ClearTextBox(txtCategory);
        }

        private void RoleForm_Load(object sender, EventArgs e)
        {
            category = new Category();
            category.getDataGrid(dg: dgCategory);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (HandleLogic.EmptytextBox(txtCategory))
            {
                return;
            }
            category = new Category();
            category.CategoryName = txtCategory.Text.Trim();
            category.update(dg: dgCategory);
            HandleLogic.ClearTextBox(txtCategory);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            HandleLogic.ClearTextBox(txtCategory);
            category = new Category();
            category.deleted(dg: dgCategory);
            HandleLogic.ClearTextBox(txtCategory);

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            HandleLogic.ClearTextBox(txtCategory);
            category = new Category();
            category.CategoryName = txtCategory.Text.Trim();
            category.SearchData(dg: dgCategory);
            HandleLogic.ClearTextBox(txtCategory);

        }

        private void dgRole_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            category = new Category();
            category.TranferToControls(dg: dgCategory, txtCategory);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            category = new Category();
            category.SearchData(dg: dgCategory);
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            DashBoard main = new DashBoard();
            main.Show();
            this.Hide();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar < (char)100)
            {
                Category category = new Category();
                category.CategoryName = txtSearch.Text.Trim();
                category.SearchData(dg: dgCategory);
            }
        }
    }
}
