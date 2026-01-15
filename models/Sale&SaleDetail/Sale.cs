using Group1_POS.models.Product;
using Microsoft.Reporting.WinForms;
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
    internal class Sale : MyActionProduct
    {
        private string _sql = "";
        public int Qty { get; set; }

        private int SaleId;

        public double CashRecieve { get; set; }
        public double CashReturn { get; set; }




        public void CommitData(DataGridView dgSale, Label TotalAmount)
        {
            SqlTransaction sqlTransaction = null;

            try
            {
                    if(dgSale.Rows.Count == 0)
                {
                    return;
                }




                foreach (DataGridViewRow DGV in dgSale.Rows)
                {

                    //cheack stouck > qty
                    this.Qty = int.Parse(DGV.Cells[3].Value.ToString());
                    string sqlStock = "select * from tblProducts where UnitInstock < @Qty";
                    this.QtyInstock = int.Parse(Database.tbl.Rows[0]["UnitInstock"].ToString());

                    using (SqlCommand updateCmd = new SqlCommand(sqlStock, Database.con, sqlTransaction))
                    {
                        updateCmd.Parameters.AddWithValue("@Qty", this.Qty);
                        if (this.QtyInstock < Qty)
                        {
                        updateCmd.ExecuteNonQuery();
                        Database.ads = new SqlDataAdapter(Database.cmd);
                        Database.tbl = new DataTable();
                        Database.ads.Fill(Database.tbl);

                        if (Database.tbl.Rows.Count > 0)
                        {
                            MessageBox.Show("check Stock");
                                return;
                        }

                        }
                    }
                }

                // Insert into tblSale
                sqlTransaction = Database.con.BeginTransaction();

                string saleSql = "INSERT INTO tblSale(SaleDate,UserId,TotalAmount) VALUES(GETDATE(), @UserId, @TotalAmount); SELECT SCOPE_IDENTITY();";
                Database.cmd = new SqlCommand(saleSql, Database.con, sqlTransaction);
                Database.cmd.Parameters.AddWithValue("@UserId", User.User.UserId);
                Database.cmd.Parameters.AddWithValue("@TotalAmount", double.Parse(TotalAmount.Text));
                this.SaleId = Convert.ToInt32(Database.cmd.ExecuteScalar());


                
                // Insert into tblSaleDetail for each item
                foreach (DataGridViewRow DGV in dgSale.Rows)
                {
                   
                    if (DGV.IsNewRow) continue;

                    this.Id = int.Parse(DGV.Cells[0].Value.ToString());
                    this.Qty = int.Parse(DGV.Cells[3].Value.ToString());
                    this.SellPrice = double.Parse(DGV.Cells[4].Value.ToString());
                    //return items 
                    string detailSql = "INSERT INTO tblSaleDetail(SaleId,ProductId,Qty,Price,Amount) VALUES(@SaleId, @ProductId, @Qty, @Price, @Amount)";
                    using (SqlCommand detailCmd = new SqlCommand(detailSql, Database.con, sqlTransaction))
                    {
                        detailCmd.Parameters.AddWithValue("@SaleId", this.SaleId);
                        detailCmd.Parameters.AddWithValue("@ProductId", this.Id);
                        detailCmd.Parameters.AddWithValue("@Qty", this.Qty);
                        detailCmd.Parameters.AddWithValue("@Price", this.SellPrice);
                        detailCmd.Parameters.AddWithValue("@Amount", this.CalculateAmount());
                        detailCmd.ExecuteNonQuery();
                       
                    }

                    string updateSql = "UPDATE tblProducts SET UnitInStock = UnitInStock - @Qty WHERE Id = @Id";
                    using (SqlCommand updateCmd = new SqlCommand(updateSql, Database.con, sqlTransaction))
                    {
                        updateCmd.Parameters.AddWithValue("@Qty", this.Qty);
                        updateCmd.Parameters.AddWithValue("@Id", this.Id);
                        Database.ads = new SqlDataAdapter(Database.cmd);
                        Database.tbl = new DataTable();
                        Database.ads.Fill(Database.tbl);
                        updateCmd.ExecuteNonQuery();

                        //string updateSqlUnit = "select * from tblProducts";
                        //using (SqlCommand updateSqlInstaock = new SqlCommand(updateSqlUnit, Database.con, sqlTransaction))
                        //{
                        //    this.QtyInstock = int.Parse(Database.tbl.Rows[0]["UnitInstock"].ToString());
                        //    updateCmd.ExecuteNonQuery();

                        //}
                        //if (this.QtyInstock > this.Qty)
                        //{
                        //}
                        //else
                        //{
                        //    MessageBox.Show("cannot update Stock");
                        //}
                    }
                }

                sqlTransaction.Commit();
                this.PrintSaleReport(SaleId);
                MessageBox.Show("Sale Successful");
                dgSale.Rows.Clear();
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Commit SaleData: {ex.Message}");
                sqlTransaction?.Rollback();
            }
        }


        public void PrintSaleReport(int id)
        {
            try
            {
                this._sql = "select * from View_Sale_Report where SaleId=@SaleId";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@SaleId", id);
                Database.cmd.ExecuteNonQuery();
                Database.ads = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ads.Fill(Database.tbl);

                ReportDataSource rds = new ReportDataSource("DataSet_Report_Sale",
                                                                                               Database.tbl);
                LocalReport rpt = new LocalReport();
                rpt.ReportPath = Application.StartupPath + @"\Reports\Report_Sale.rdlc";

                // 5. Set parameters
                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("CashReceive",CashRecieve.ToString()),
                    new ReportParameter("CashReturn",CashReturn.ToString())
                };

                rpt.SetParameters(parameters);
                rpt.DataSources.Clear();
                rpt.DataSources.Add(rds);


                PrintReport objPrint = new PrintReport();
                objPrint.Export(rpt);
                objPrint.m_currentPageIndex = 0;
                objPrint.Print();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Report_Sale: {ex.Message}");
            }
        }



        public double CalculateAmount()
        {
            return this.Qty * this.SellPrice;
        }

        private double CalculateTotalAmount(DataGridView dgSale)
        {
            double sum = 0;

            foreach (DataGridViewRow DGV in dgSale.Rows)
            {
                if (DGV.IsNewRow) continue; // avoid empty rows
                sum += double.Parse(DGV.Cells[5].Value.ToString());
            }

            return sum;
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

                    this.QtyInstock = int.Parse(Database.tbl.Rows[0]["UnitInstock"].ToString());
                    if(this.QtyInstock > Product.Product.MaxStock)
                    {
                        MessageBox.Show("Stock Bigger Than Max");
                        return;
                    }

                    foreach (DataGridViewRow DGV in dgSale.Rows)
                    {
                        if (DGV.IsNewRow) continue;

                        string checkBarcode = DGV.Cells[1].Value.ToString();

                        if (checkBarcode == this.Barcode)
                        {
                            int oldQty = int.Parse(DGV.Cells[3].Value.ToString());
                            double unitPrice = double.Parse(DGV.Cells[4].Value.ToString());

                            this.Qty = oldQty + 1;
                            this.SellPrice = unitPrice;

                            DGV.Cells[3].Value = this.Qty;
                            DGV.Cells[5].Value = (this.Qty * this.SellPrice).ToString("#,##0.00");

                            TotalAmount.Text = CalculateTotalAmount(dgSale).ToString("#,##0.00");

                            GeneralFun.ClearTextBox(txtScan);
                            txtScan.Focus();
                            return;
                        }
                    }

                    // If product not already in grid
                    this.Id = int.Parse(Database.tbl.Rows[0]["ID"].ToString());
                    this.Barcode = Database.tbl.Rows[0]["Barcode"].ToString();
                    this.Name = Database.tbl.Rows[0]["Name"].ToString();
                    this.Qty = 1;
                    this.SellPrice = double.Parse(Database.tbl.Rows[0]["SellPrice"].ToString());

                    object[] row = {
                this.Id,
                this.Barcode,
                this.Name,
                this.Qty,
                this.SellPrice.ToString("#,##0.00"),
                this.CalculateAmount().ToString("#,##0.00")
            };
                    dgSale.Rows.Add(row);

                    TotalAmount.Text = CalculateTotalAmount(dgSale).ToString("#,##0.00");

                    GeneralFun.ClearTextBox(txtScan);
                    txtScan.Focus();
                }
                else
                {
                    MessageBox.Show("Barcode Not Found!");
                    txtScan.Clear();
                    txtScan.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error ScanBarcode: {ex.Message}");
            }
        }


    }
}
