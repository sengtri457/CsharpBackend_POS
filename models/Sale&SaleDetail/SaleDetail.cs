using Group1_POS.models.Product;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyActionProduct = Group1_POS.models.Product.Product;

namespace Group1_POS.models.Sale_SaleDetail
{
    internal class SaleDetail : MyActionProduct
    {
        public int SaleId { get; set; }
        private string _sql = "";
        public int Qty { get; set; }
        public  double CalculateAmount()
        {
            return this.Qty * this.SellPrice;
        }
        public void ScanBarcode(DataGridView dgSale, TextBox txtScan, Label TotalAmount)
        {
            try
            {
                this.Barcode = txtScan.Text.Trim();

                this._sql = "SELECT * FROM tblProducts WHERE Barcode = @Barcode";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@Barcode", this.Barcode);
                Database.ads = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ads.Fill(Database.tbl);
                if (Database.tbl.Rows.Count > 0)

                {

                    foreach (DataGridViewRow DGV in dgSale.Rows )
                    {
                        string ChecBarcode =DGV.Cells[1].Value.ToString();
                        int CatchQty = int.Parse(DGV.Cells[3].Value.ToString());    
                        double newSellPrice = double.Parse(DGV.Cells[4].Value.ToString());
                      
                        if (ChecBarcode == this.Barcode)
                        {
                            double total = this.CalculateAmount(); 
                            this.Qty = CatchQty + 1;
                            DGV.Cells[3].Value = this.Qty;
                            this.SellPrice = newSellPrice;
                            DGV.Cells[5].Value = this.CalculateAmount().ToString("#,##0.00");
                            TotalAmount.Text = CalculateAmount().ToString("#,##0.00")+" $";

                            GeneralFun.ClearTextBox(txtScan);
                            txtScan.Focus();

                            return;
                        }
                    }
                    this.Id = int.Parse(Database.tbl.Rows[0]["ID"].ToString());
                    this.Barcode = Database.tbl.Rows[0]["Barcode"].ToString();
                    this.Name = Database.tbl.Rows[0]["Name"].ToString();
                    this.Qty = 1;
                    this.SellPrice = double.Parse(Database.tbl.Rows[0]["SellPrice"].ToString());
                    TotalAmount.Text = CalculateAmount().ToString("#,##0.00") + " $";
                    object[] row = { this.Id, this.Barcode, this.Name, this.Qty, this.SellPrice.ToString("#,##0.00"), this.CalculateAmount().ToString("#,##0.00") };
                    dgSale.Rows.Add(row);
                    GeneralFun.ClearTextBox(txtScan);
                    txtScan.Focus();
                }
                else
                {
                    MessageBox.Show("Barcode Not Found!!!!");
                    txtScan.Clear();
                    txtScan.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error ScanBarcode: {ex.Message}");
            }
        }


        public void CreateSaleDetail()
        {
            this._sql = "INSERT INTO tblSaleDetail(SaleId, ProductId, TotalAmount, Qty, Price, Amount)VALUES (@SaleId, @ProductId, @TotalAmount, @Qty, @Price, @Amount)";
            Database.cmd = new SqlCommand(this._sql, Database.con);
            Database.cmd.Parameters.AddWithValue("@SaleId", 1);
            Database.cmd.Parameters.AddWithValue("@ProductId", this.Id);
            Database.cmd.Parameters.AddWithValue("@Qty", this.Qty);
            Database.cmd.Parameters.AddWithValue("@Price", this.SellPrice);
            Database.cmd.Parameters.AddWithValue("@TotalAmount", this.Qty * this.SellPrice);
            Database.cmd.Parameters.AddWithValue("@Amount", this.Qty * this.SellPrice);
            Database.cmd.ExecuteNonQuery();
            MessageBox.Show("Add SaleDetail Sucesss");

        }

    }
}
