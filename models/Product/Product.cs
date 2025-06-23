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
namespace Group1_POS.models.Product
{
    internal class Product: MyAction
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public double SellPrice { get; set; }
        public int QtyInstock { get; set; }
        public string Photo { get; set; }
        public int CategoryId { get; set; }
        private string _sql;
        private int _Rowffecticted;


        public bool IsCheckDubplicate()
        {
            try
            {
                Database.ConnectionDB();
                this._sql = "select * from tblProducts where Name = @Name";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@Name", this.Name);
                Database.cmd.ExecuteNonQuery();
                Database.ads = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ads.Fill(Database.tbl);
                if (Database.tbl.Rows.Count > 0)
                {
                    MessageBox.Show("Dubplicate Name");
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Duplicate {ex.Message}");
            }
            return false;

        }
        public override void createRole()
        {   
            try
            {

               
                if (IsCheckDubplicate() == true)
                {
                    return;
                }
                this._sql = "insert into tblProducts (Name,Barcode,SellPrice,UnitInstock,CareateBy,CreateAt,CategoryId,Photo) values(@Name,@Barcode,@SellPrice,@UnitInstock,@CareateBy,GETDATE(),@CategoryId,@Photo)";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@Name", this.Name);
                Database.cmd.Parameters.AddWithValue("@Barcode", this.Barcode);
                Database.cmd.Parameters.AddWithValue("@SellPrice", this.SellPrice);
                Database.cmd.Parameters.AddWithValue("@UnitInstock", this.QtyInstock);
                Database.cmd.Parameters.AddWithValue("@CareateBy", this.CategoryId);
                Database.cmd.Parameters.AddWithValue("@CategoryId", this.CategoryId);
                Database.cmd.Parameters.AddWithValue("@Photo", this.Photo);
               
                this._Rowffecticted = Database.cmd.ExecuteNonQuery();
                if (this._Rowffecticted > 0)
                {
                    MessageBox.Show("Create Role Sucessful");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Create Row {ex.Message}");
            }
        }




    }
}
