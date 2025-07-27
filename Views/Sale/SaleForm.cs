using Group1_POS.models.Method;
using Group1_POS.models.Product;
using Group1_POS.models.Role;
using Group1_POS.models.Sale_SaleDetail;
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
    public partial class SaleForm : Form
    {
        public SaleForm()
        {
            InitializeComponent();
        }
        private void txtRole_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {

         

        }

        private void RoleForm_Load(object sender, EventArgs e)
        {
            //product = new Product();
            //product.SetProductName(cboProductName);
            //product.getDataGrid(dg: dgSale);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
 

        }

        private void button1_Click(object sender, EventArgs e)
        {
            

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            

        }

        private void dgRole_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnBrower_Click(object sender, EventArgs e)
        {
           

        }

        private void cboProductName_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }


        private void dgProduct_DoubleClick(object sender, EventArgs e)
        {
         
        }

        private void addStockMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void txtScan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter)
            {
                Sale sale = new Sale();
                sale.ScanBarcode(dgSale, txtScan);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DashBoard main = new DashBoard();
            main.Show();
            this.Hide();
        }
    }
}
