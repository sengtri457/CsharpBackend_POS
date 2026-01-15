using Group1_POS.models.Method;
using Group1_POS.models.Product;
using Group1_POS.models.Role;
using Group1_POS.models.Stock;
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

            if (HandleLogic.EmptytextBox(txtProductName,txtBarcode,txtSellPrice))
            {
                return;
            }
            product = new Product();
            product.Name = txtProductName.Text.Trim();
            product.Barcode = txtBarcode.Text.Trim();   
            product.SellPrice = double.Parse(txtSellPrice.Text.Trim());
            product.Photo = Product.PathPhoto;
            product.CategoryId = product.GetProductId(cboProductName);
            product.createRole(dg: dgProduct);
            HandleLogic.ClearTextBox(txtProductName,txtBarcode,txtSellPrice);
            HandleLogic.ClearComboBox(cboProductName);
            PicPhoto.Image = null;
            Product.PathPhoto = "";
            txtProductName.Focus();

        }

        private void RoleForm_Load(object sender, EventArgs e)
        {
            product = new Product();
            product.SetProductName(cboProductName);
            product.getDataGrid(dg: dgProduct);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (HandleLogic.EmptytextBox(txtProductName))
            {
                return;
            }
            product = new Product();
            product.Name = txtProductName.Text.Trim();
            product.Barcode = txtBarcode.Text.Trim();
            product.SellPrice = double.Parse(txtSellPrice.Text.Trim());
            product.Photo = Product.PathPhoto;
            product.CategoryId = product.GetProductId(cboProductName);
            product.update(dg: dgProduct);
            HandleLogic.ClearTextBox(txtProductName, txtBarcode, txtSellPrice);
            HandleLogic.ClearComboBox(cboProductName);
            PicPhoto.Image = null;
            Product.PathPhoto = "";
            txtProductName.Focus();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            HandleLogic.ClearTextBox(txtProductName);
            product = new Product();
            product.deleted(dg: dgProduct);
            HandleLogic.ClearTextBox(txtProductName);

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            HandleLogic.ClearTextBox(txtProductName);
            user = new User();
            user.UserName = txtProductName.Text.Trim();
            user.SearchData(dg: dgProduct);
            HandleLogic.ClearTextBox(txtProductName);

        }

        private void dgRole_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            user = new User();
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
                Product product = new Product();
            product.Name = txtSearch.Text.Trim();
                product.SearchData(dg: dgProduct);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnBrower_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files (*.jpg; *.jpeg; *.png)|*.jpg;*.jpeg;*.png";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // Show selected image in PictureBox
                PicPhoto.Image = Image.FromFile(ofd.FileName);
                PicPhoto.SizeMode = PictureBoxSizeMode.StretchImage;

                // ✅ Store image path for later use
                Product.PathPhoto = ofd.FileName;
            }

        }

        private void cboProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Product product = new Product();
            if (cboProductName.SelectedIndex != -1)
            {
                int categoryId = product.GetProductId(cboProductName);
                product.CategoryId = categoryId;
            }        }

        private void dgProduct_DoubleClick(object sender, EventArgs e)
        {
            product = new Product();
            product.TranferToControls(dg: dgProduct, txtProductName, txtBarcode, txtSellPrice, cboProductName,PicPhoto);
        }

        private void addStockMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            AddStockForm addStockForm = new AddStockForm();
            AddStock stock = new AddStock();
            stock.TranserDataToControl(dgProduct, addStockForm.txtProductId, addStockForm.txtProductName);
            addStockForm.ShowDialog();
        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
