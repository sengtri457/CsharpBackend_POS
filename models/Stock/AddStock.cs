using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Group1_POS.models.Stock
{
    internal class AddStock
    {
        private string _sql;
        public int Qty { get; set; }
        public double Cost { get; set; }
        public int SupplierId { get; set; }
        public int  ProductId { get; set; }
        private int _Rowffecticted;

        SqlTransaction _SqlTransaction = null;



        public void AddStockProduct()
        {
            try
            {
                _SqlTransaction = Database.con.BeginTransaction();
                this._sql = "insert into tblStock(SupplierId,ProductId,Qty,Cost,Total,CareateBy,CreateAt,UpdateAt,UpdateBy) Values(@SupplierId,@ProductId,@Qty,@Cost,@Total,@CareateBy,GETDATE(),GETDATE(),@UpdateBy)";
                Database.cmd = new SqlCommand ( this._sql, Database.con, _SqlTransaction);
                Database.cmd.Parameters.AddWithValue("@SupplierId", this.SupplierId);
                Database.cmd.Parameters.AddWithValue("@ProductId", ProductId);
                Database.cmd.Parameters.AddWithValue("@Qty", this.Qty);
                Database.cmd.Parameters.AddWithValue("@Cost", this.Cost);
                Database.cmd.Parameters.AddWithValue("@Total", this.Amount());
                Database.cmd.Parameters.AddWithValue("@CareateBy", User.User.UserId);
                Database.cmd.Parameters.AddWithValue("@UpdateBy", User.User.UserId);
                this._Rowffecticted = Database.cmd.ExecuteNonQuery();



                this._sql = "update tblProducts set UnitInStock = UnitInStock + @Qty where ID=@ProductId";
                Database.cmd = new SqlCommand(this._sql, Database.con, _SqlTransaction);
                Database.cmd.Parameters.AddWithValue("@Qty", this.Qty);
                Database.cmd.Parameters.AddWithValue("@ProductId", this.ProductId);
                this._Rowffecticted = Database.cmd.ExecuteNonQuery();

                _SqlTransaction.Commit();
                if (this._Rowffecticted > 0)
                {
                    MessageBox.Show("Update Stock Sucessfully");
                }

            } catch (Exception ex)
            {
                MessageBox.Show($"Error add Stock:{ex.Message}");
                _SqlTransaction.Rollback();
            }
        }

    








        public double Amount() {
            return this.Qty * this.Cost;
                }
        public void SetsupplierName(ComboBox cboSupplierName)
        {
            try
            {

                this._sql = "select Name from tblSupplier";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.ExecuteNonQuery();
                Database.ads = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ads.Fill(Database.tbl);
                cboSupplierName.Items.Clear();
                foreach (DataRow r in Database.tbl.Rows)
                {

                    object[] row = { };
                    cboSupplierName.Items.Add(r["Name"].ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: Supplier {ex.Message}");
            }
        }
        public int GetSupplierId(ComboBox cboSupplierName)
        {
            int id = 0;
            try
            {

                this._sql = "select * from tblSupplier where Name =@Name";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@Name", cboSupplierName.Text);
                //Database.cmd.ExecuteNonQuery();
                Database.ads = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ads.Fill(Database.tbl);

                if (Database.tbl.Rows.Count > 0)
                {
                    id = int.Parse(Database.tbl.Rows[0]["Id"].ToString());
                    return id;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            return id;
        }
         public void TranserDataToControl(DataGridView dg, TextBox txtProductId , TextBox txtProductName)
        {


            if (dg.Rows.Count <= 0) {

                return;
            }
            DataGridViewRow DGV = new DataGridViewRow();
            DGV = dg.SelectedRows[0];
            txtProductId.Text = DGV.Cells[0].Value.ToString();
            txtProductName.Text = DGV.Cells[1].Value.ToString();
        }
    }
}
