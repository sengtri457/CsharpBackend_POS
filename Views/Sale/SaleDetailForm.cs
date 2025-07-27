using Group1_POS.models.Sale_SaleDetail;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Group1_POS.Views.Sale
{
    public partial class SaleDetailForm : Form
    {
        SaleDetail saleDe;
        public SaleDetailForm()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            saleDe = new SaleDetail();
            saleDe.CreateSaleDetail();
        }
    }
}
