using Group1_POS.models.Category;
using Group1_POS.models.Method;
using Group1_POS.models.Product;
using Group1_POS.models.Role;
using Group1_POS.models.Supplier;
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
    public partial class SupplierForm : Form
    {
        public SupplierForm() => InitializeComponent();
        Category category;
        Supplier supplier;
        private void txtRole_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {

            if (HandleLogic.EmptytextBox(txtSupplierName))
            {
                return;
            }
            supplier = new Supplier();
            supplier.Name = txtSupplierName.Text.Trim();
            supplier.Tell = txtTell.Text.Trim();
            supplier.createRole(dg: dgSuppliers);
            HandleLogic.ClearTextBox(txtSupplierName);
        }

        private void RoleForm_Load(object sender, EventArgs e)
        {
            supplier = new Supplier();
            supplier.getDataGrid(dg: dgSuppliers);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (HandleLogic.EmptytextBox(txtSupplierName))
            {
                return;
            }
            supplier = new Supplier();
            supplier.Name = txtSupplierName.Text.Trim();
            supplier.Tell = txtTell.Text.Trim();
            supplier.update(dg: dgSuppliers);
            HandleLogic.ClearTextBox(txtSupplierName,txtTell);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            HandleLogic.ClearTextBox(txtSupplierName, txtTell);
            supplier = new Supplier();
            supplier.deleted(dg: dgSuppliers);
            HandleLogic.ClearTextBox(txtSupplierName, txtTell);

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            HandleLogic.ClearTextBox(txtSupplierName);
            category = new Category();
            category.CategoryName = txtSupplierName.Text.Trim();
            category.SearchData(dg: dgSuppliers);
            HandleLogic.ClearTextBox(txtSupplierName);

        }

        private void dgRole_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            category = new Category();
            category.SearchData(dg: dgSuppliers);
            
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
                category.SearchData(dg: dgSuppliers);
            }
        }

        private void dgSuppliers_DoubleClick(object sender, EventArgs e)
        {
            supplier = new Supplier();
            supplier.TranferToControls(dg: dgSuppliers, txtSupplierName, txtTell);
        }
    }
}
