using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyAction = Group1_POS.models.Interface.Action;
using System.Data.SqlClient;
using System.Data;
using Group1_POS.models.User;
using System.IO;
using System.Drawing;
namespace Group1_POS.models.Product
{
    internal class Product: MyAction
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string Barcode { get; set; }
        public double SellPrice { get; set; }
        public int QtyInstock { get; set; }
        public int CategoryId { get; set; }
        public int CategoryIdCatch { get; set; }
        private string _sql;
        private int _Rowffecticted;
        public static int UserId { get; set; }
        public DateTime CreateAt { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateAt { get; set; }
        public static string PathPhoto { get; set; }
        public string Photo { get; set; }


        public bool IsCheckDuplicate(string columnName, object Values, string MessageCheck)
        {
            try
            {
                Database.ConnectionDB();
                this._sql = $"select * from tblProducts where {columnName} = @Value";
                Database.cmd = new SqlCommand(this._sql, Database.con);

                if (Values == null)
                {
                    MessageBox.Show($"Error: Value for {columnName} is null");
                    return true; // stop to avoid crash
                }

                Database.cmd.Parameters.AddWithValue("@Value", Values);
                Database.ads = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ads.Fill(Database.tbl);

                if (Database.tbl.Rows.Count > 0)
                {
                    MessageBox.Show("Duplicate " + MessageCheck + "");
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error {ex.Message}");
            }
            return false;
        }

        public void SetProductName(ComboBox cboProductName)
        {
            try
            {

                this._sql = "select Name from tblCategory";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.ExecuteNonQuery();
                Database.ads = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ads.Fill(Database.tbl);
                cboProductName.Items.Clear();
                foreach (DataRow r in Database.tbl.Rows)
                {

                    object[] row = { };
                    cboProductName.Items.Add(r["Name"].ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: Product {ex.Message}");
            }
        }
        public int GetProductId(ComboBox cboProductName)
        {
            int id = 0;
            try
            {

                this._sql = "select * from tblCategory where Name =@Name";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@Name", cboProductName.Text);
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
        public override void createRole(DataGridView dg)
        {   
            try
            {

               
                if (IsCheckDuplicate("Name",this.Name,"Name") == true)
                {
                    return;
                }
                if (IsCheckDuplicate("Barcode", this.Barcode, "Barcode") == true)
                {
                    return;
                }
                    this._sql = "insert into tblProducts(Name, Barcode,SellPrice,UnitInstock,CareateBy,CreateAt,CategoryId,Photo) \r\nvalues (@Name,@Barcode,@SellPrice,@UnitInstock,@CareateBy,GETDATE(),@CategoryId,@Photo)";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@Name", this.Name);
                Database.cmd.Parameters.AddWithValue("@Barcode", this.Barcode);
                Database.cmd.Parameters.AddWithValue("@SellPrice", this.SellPrice);
                Database.cmd.Parameters.AddWithValue("@UnitInstock", this.QtyInstock);
                Database.cmd.Parameters.AddWithValue("@CareateBy", User.User.UserId);
                Database.cmd.Parameters.AddWithValue("@CategoryId", this.CategoryId);
                Database.cmd.Parameters.AddWithValue("@Photo", this.Photo ?? (object)DBNull.Value); // FIXED
                this._Rowffecticted = Database.cmd.ExecuteNonQuery();
                if (this._Rowffecticted > 0)
                {
                    MessageBox.Show("Product Created Sucessfully");
                    getDataGrid(dg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Product Created {ex.Message}");
            }
        }

        public override void getDataGrid(DataGridView dg)
        {
            try
            {
                Database.ConnectionDB();

                this._sql = "SELECT * FROM View_Prodcut_category";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.ads = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ads.Fill(Database.tbl);

                dg.Rows.Clear();

                foreach (DataRow dr in Database.tbl.Rows)
                {
                    this.Id = Convert.ToInt32(dr["ProductId"]);
                    this.Name = dr["ProductName"].ToString();
                    this.Barcode = dr["Barcode"].ToString();
                    this.SellPrice = double.Parse(dr["SellPrice"].ToString());
                    this.QtyInstock = int.Parse(dr["UnitInstock"].ToString());
                    string CategoryName = dr["CategoryName"].ToString();
                    this.Photo = dr["PhotoProduct"].ToString();
                    object[] row = { this.Id, this.Name, this.Barcode, this.SellPrice, this.QtyInstock, CategoryName,this.Photo,  };
                    dg.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cannot Get Data: {ex.Message}");
            }
        }

        public override void SearchData(DataGridView dg)
        {
            try
            {
                this._sql = "SELECT * FROM View_Prodcut_category WHERE ProductName LIKE '%'+@ProductName+'%'";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@ProductName", this.Name);
                Database.ads = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ads.Fill(Database.tbl);
                dg.Rows.Clear();

                foreach (DataRow dr in Database.tbl.Rows)
                {
                    this.Id = Convert.ToInt32(dr["ProductId"]);
                    this.Name = dr["ProductName"].ToString();
                    this.Barcode = dr["Barcode"].ToString();
                    this.SellPrice = double.Parse(dr["SellPrice"].ToString());
                    this.QtyInstock = int.Parse(dr["UnitInstock"].ToString());
                    this.Photo = dr["PhotoProduct"].ToString();
                    string CategoryName = dr["CategoryName"].ToString();
                    object[] row = { this.Id, this.Name, this.Barcode, this.SellPrice, this.QtyInstock, this.Photo, CategoryName };
                    dg.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cannot Get Data: {ex.Message}");
            }
        }

        public override void deleted(DataGridView dg)
        {
            try
            {

                if (dg.Rows.Count <= 0)
                {
                    return;
                }

                var click = MessageBox.Show("Do you Want To delete This Record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (click == DialogResult.Yes)
                {
                    DataGridViewRow DGV = new DataGridViewRow();
                    DGV = dg.SelectedRows[0];
                    this.Id = int.Parse(DGV.Cells[0].Value.ToString());
                    this._sql = "delete from tblProducts where Id=@Id";
                    Database.cmd = new SqlCommand(this._sql, Database.con);
                    Database.cmd.Parameters.AddWithValue("@Id", this.Id);
                    this._Rowffecticted = Database.cmd.ExecuteNonQuery();
                    if (this._Rowffecticted > 0)
                    {
                        MessageBox.Show("Delete Producted Sucessfuly");
                        dg.Rows.Remove(DGV);
                        getDataGrid(dg);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Deleted Product: {ex.Message}");

            }
        }


        public void TranferToControls(DataGridView dg, TextBox txtProductName, TextBox txtBarcode,TextBox txtSellPrice,ComboBox cboProductName, PictureBox PicPhoto)
        {
            if (dg.Rows.Count <= 0)
            {
                return;
            }
            DataGridViewRow DGV = new DataGridViewRow();
            DGV = dg.SelectedRows[0];
            this.Id = int.Parse(DGV.Cells[0].Value.ToString());
            this._sql = "select * from View_Prodcut_category where ProductId=@Id";
            Database.cmd = new SqlCommand(this._sql, Database.con);
            Database.cmd.Parameters.AddWithValue("@Id", this.Id);
            Database.cmd.ExecuteNonQuery();
            Database.ads = new SqlDataAdapter(Database.cmd);
            Database.tbl = new DataTable();
            Database.ads.Fill(Database.tbl);
            if(dg.Rows.Count > 0){

                txtProductName.Text = Database.tbl.Rows[0]["ProductName"].ToString();
                txtBarcode.Text = Database.tbl.Rows[0]["Barcode"].ToString();
                txtSellPrice.Text = Database.tbl.Rows[0]["SellPrice"].ToString();
                cboProductName.Text = Database.tbl.Rows[0]["CategoryName"].ToString();


                string Pic = DGV.Cells[6].Value?.ToString()?.ToLower();
                if (!Pic.Equals(""))
                {
                    PicPhoto.Image = Image.FromFile(Pic);
                }
                else
                {
                    PicPhoto.Image = null;
                }
            }

        }
        public override void update(DataGridView dg)
        {
            try
            {
                Database.ConnectionDB();

                if (dg.SelectedRows.Count <= 0)
                {
                    MessageBox.Show("Please select a row to update");
                    return;
                }

                DataGridViewRow dataGridView = dg.SelectedRows[0];
                this.Id = int.Parse(dataGridView.Cells[0].Value.ToString());

                if(IsCheckDuplicate("Name" , this.Name , "Name") == true)
                {
                    return;
                }
                if(IsCheckDuplicate("Barcode", this.Barcode , "Barcode") == true)
                {
                    return;
                }
                this._sql = "update tblProducts set Name = @Name, Barcode = @Barcode, SellPrice = @SellPrice, Photo = @Photo, CategoryId = @CategoryId, UnitInstock = @UnitInstock, UpdateBy = @UpdateBy, UpdateAt = GETDATE() where Id = @Id";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@Id", this.Id);
                Database.cmd.Parameters.AddWithValue("@Name", this.Name);
                Database.cmd.Parameters.AddWithValue("@Barcode", this.Barcode);
                Database.cmd.Parameters.AddWithValue("@SellPrice", this.SellPrice);
                Database.cmd.Parameters.AddWithValue("@UnitInstock", this.QtyInstock);
                Database.cmd.Parameters.AddWithValue("@CategoryId", this.CategoryId);
                Database.cmd.Parameters.AddWithValue("@Photo", this.Photo ?? (object)DBNull.Value);
                Database.cmd.Parameters.AddWithValue("@UpdateBy", User.User.UserId);

                this._Rowffecticted = Database.cmd.ExecuteNonQuery();

                if (this._Rowffecticted > 0)
                {
                    MessageBox.Show("Updated Product Successfully");
                    getDataGrid(dg);
                }
                else
                {
                    MessageBox.Show("No records were updated");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Update Product: {ex.Message}");
            }
        }


    }
}
