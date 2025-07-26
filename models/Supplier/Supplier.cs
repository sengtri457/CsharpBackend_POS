using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyAction = Group1_POS.models.Interface.Action;

namespace Group1_POS.models.Supplier
{
    internal class Supplier : MyAction
    {
        public int Id { set; get; }
        public string Tell { get; set; }
        public string Name { get; set; }
        private string _sql;
        private int _Rowffecticted;
        public string CreateBy { get; set; }
        public DateTime CreateAt { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateAt { get; set; }
        public static int UserId { get; set; }
        public bool IsDubplicateCheck()
        {
            try
            {
                this._sql = "select * from tblSupplier where Name =@Name";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@Name", this.Name);
                Database.cmd.ExecuteNonQuery();
                Database.ads = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ads.Fill(Database.tbl);
                if (Database.tbl.Rows.Count > 0)
                {
                    MessageBox.Show("Suppliers CategoryName");
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Suppliers{ex.Message}");
            }
            return false;

        }
        public override void createRole(DataGridView dg)
        {
            try
            {
                Database.ConnectionDB();
                if (IsDubplicateCheck() == true)
                {
                    return;
                }
                this._sql = "insert into tblSupplier(Name,Tel,CareateBy)Values(@Name,@Tel,@CareateBy)";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@Name", this.Name);
                Database.cmd.Parameters.AddWithValue("@Tel", this.Tell);
                Database.cmd.Parameters.AddWithValue("@CareateBy", User.User.UserId);
                this._Rowffecticted = Database.cmd.ExecuteNonQuery();
                if (this._Rowffecticted > 0)
                {
                    MessageBox.Show("Create Supplier Sucessful");
                    getDataGrid(dg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Create Supplier {ex.Message}");
            }
        }
        public override void getDataGrid(DataGridView dg)
        {
            try
            {
                Database.ConnectionDB();

                this._sql = "select * from tblSupplier";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.ExecuteNonQuery();
                Database.ads = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ads.Fill(Database.tbl);
                dg.Rows.Clear();
                foreach (DataRow dr in Database.tbl.Rows)
                {

                    this.Id = int.Parse(dr["Id"].ToString());
                    this.Name = dr["Name"].ToString();
                    this.Tell = dr["Tel"].ToString();
                    this.CreateBy = dr["CareateBy"].ToString();
                    object[] row = { this.Id, this.Name,this.Tell,this.CreateBy};
                    dg.Rows.Add(row);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cannot Get Suppliers: {ex.Message}");

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
                    this._sql = "delete from tblSupplier where Id=@Id";
                    Database.cmd = new SqlCommand(this._sql, Database.con);
                    Database.cmd.Parameters.AddWithValue("@Id", this.Id);
                    this._Rowffecticted = Database.cmd.ExecuteNonQuery();
                    if (this._Rowffecticted > 0)
                    {
                        MessageBox.Show("Delete Supplier Sucessful");
                        dg.Rows.Remove(DGV);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Deleted Supplier: {ex.Message}");

            }
        }
        public void TranferToControls(DataGridView dg, TextBox txtSupplierName, TextBox txtTell)
        {
            if (dg.Rows.Count <= 0)
            {
                return;
            }
            DataGridViewRow DGV = new DataGridViewRow();
            DGV = dg.SelectedRows[0];
            this.Id = int.Parse(DGV.Cells[0].Value.ToString());
            this._sql = "select * from tblSupplier where Id=@Id";
            Database.cmd = new SqlCommand(this._sql, Database.con);
            Database.cmd.Parameters.AddWithValue("@Id", this.Id);
            Database.cmd.ExecuteNonQuery();
            Database.ads = new SqlDataAdapter(Database.cmd);
            Database.tbl = new DataTable();
            Database.ads.Fill(Database.tbl);
            if (dg.Rows.Count > 0)
            {

                txtSupplierName.Text = Database.tbl.Rows[0]["Name"].ToString();
                txtTell.Text = Database.tbl.Rows[0]["Tel"].ToString();
            }
        }
        public override void update(DataGridView dg)
        {
            try
            {
                if (dg.Rows.Count <= 0)
                {
                    return;
                }


                Database.ConnectionDB();
                if (IsDubplicateCheck() == true)
                {
                    return;
                }
                DataGridViewRow DGV = new DataGridViewRow();
                DGV = dg.SelectedRows[0];
                this.Id = int.Parse(DGV.Cells[0].Value.ToString());
                this._sql = "update tblSupplier set Name=@Name where Id=@Id";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@Name", this.Name);
                Database.cmd.Parameters.AddWithValue("@Id", this.Id);

                this._Rowffecticted = Database.cmd.ExecuteNonQuery();
                if (this._Rowffecticted > 0)
                {
                    MessageBox.Show("Update Supplier Sucessful");
                    getDataGrid(dg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Update Supplier {ex.Message}");
            }
        }
    }
}
