using Group1_POS.models.User;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyUser = Group1_POS.models.User;
using System.Windows.Forms;
using Group1_POS.models.Sale_SaleDetail;
using Group1_POS.models.Product;


namespace Group1_POS.Views
{
    public partial class DashBoard : Form
    {
        private int childFormNumber = 0;

        public DashBoard()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            if (MessageBox.Show("Are you sure you want to close?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

/*        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }*/

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void viewMenu_Click(object sender, EventArgs e)
        {

        }

        private void roleManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RoleForm roleForm = new RoleForm();
            roleForm.MdiParent = this;
            roleForm.Show();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DashBoard main = new DashBoard();
            main.Show();
            this.Hide();
        }

        private void userManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
           UserForm userForm = new UserForm();
            userForm.MdiParent = this;
            userForm.Show();
        }

        private void securityToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            RoleForm role = new RoleForm();
            role.Show();
        }

        private void toolStripStatusLabel1UserRole_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabelUserName_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UserBtn_Click(object sender, EventArgs e)
        {
            UserForm userForm = new UserForm();
            userForm.Show();
        }

        private void UserRoleBtn_Click(object sender, EventArgs e)
        {

            RoleForm roleForm = new RoleForm();
            roleForm.Show();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SupplierBtn_Click(object sender, EventArgs e)
        {

        }

        private void RoleBtn_Click(object sender, EventArgs e)
        {

        }

        private void ProductBtn_Click(object sender, EventArgs e)
        {
            ProductForm proform = new ProductForm();
            proform.Show();
        }

        private void CategoryvBtn_Click(object sender, EventArgs e)
        {

        }

        private void AddStockBtn_Click(object sender, EventArgs e)
        {

        }

        private void SaleBtn_Click(object sender, EventArgs e)
        {

        }

        private void SalDetailBtn_Click(object sender, EventArgs e)
        {

        }

        private void DashBoard_Load(object sender, EventArgs e)
        {
            User user = new User();
            if (user != null && !string.IsNullOrEmpty(User.PermissionRolename))
            {
                if (User.PermissionRolename.Equals("Admin"))
                {
                    UserBtn.Enabled = true;
                    UserRoleBtn.Enabled = true;
                }
            }
            else
            {
                UserBtn.Enabled = false;
            }
            Product pro = new Product();
            pro.getDataGrid(dg: dgProduct);
            pro.TranferToControls(dg: dgProduct, txtProductName, txtBarcode, txtSellPrice, cboProductName, PicPhoto);
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserForm form = new UserForm();
            form.Show();
           
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripTextRole_Click(object sender, EventArgs e)
        {

        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            LoginForm form = new LoginForm();
            DashBoard das = new DashBoard();
            form.Show();
            this.Hide();
            das.Hide();

        }

        private void LogoutBtn_Click_1(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            ProductForm productForm = new ProductForm();
            productForm.Show();
            this.Hide();
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            CategoryForm category = new CategoryForm();
            category.Show();
            this.Hide();
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            SaleForm sale = new SaleForm();
            sale.Show();
            this.Hide();

        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            SupplierForm supplier = new SupplierForm(); 
            supplier.Show();
            this.Hide();
        }

        private void btnSaleDetail_Click(object sender, EventArgs e)
        {
            
        }

        private void cboProductName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgProduct_DoubleClick(object sender, EventArgs e)
        {
            Product pro = new Product();

            pro.TranferToControls(dg: dgProduct, txtProductName, txtBarcode, txtSellPrice, cboProductName, PicPhoto);
        }
    }
}
