using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyAction = Group1_POS.models.Interface.Action;
using Group1_POS.models.User;
/*using System.Data.SqlClient;
using System.Data;*/

namespace Group1_POS.models.Category
{
    internal class Category:MyAction
    {
        public int Id { set; get; }
        public string CategoryName { set; get; }
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
                this._sql = "select * from tblCategory where Name=@Name";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@Name", this.CategoryName);
                Database.cmd.ExecuteNonQuery();
                Database.ads = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ads.Fill(Database.tbl);
                if (Database.tbl.Rows.Count > 0)
                {
                    MessageBox.Show("Dubplicate CategoryName");
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error CategoryName{ex.Message}");
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
                this._sql = "insert into tblCategory(Name,CareateBy)Values(@Name,@CareateBy)";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@Name", this.CategoryName);
                Database.cmd.Parameters.AddWithValue("@CareateBy", User.User.UserId);
                this._Rowffecticted = Database.cmd.ExecuteNonQuery();
                if (this._Rowffecticted > 0)
                {
                    MessageBox.Show("Create Category Sucessful");
                    getDataGrid(dg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Create Category {ex.Message}");
            }
        }


        public override void getDataGrid(DataGridView dg)
        {
            try
            {
                Database.ConnectionDB();

                this._sql = "select * from tblCategory";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.ExecuteNonQuery();
                Database.ads = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ads.Fill(Database.tbl);
                dg.Rows.Clear();
                foreach (DataRow dr in Database.tbl.Rows)
                {

                    this.Id = int.Parse(dr["Id"].ToString());
                    this.CategoryName = dr["Name"].ToString();
                    object[] row = { this.Id, this.CategoryName };
                    dg.Rows.Add(row);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cannot Get Category: {ex.Message}");

            }
        }

        public override void SearchData(DataGridView dg)
        {
            try
            {
                this._sql = "SELECT * FROM tblCategory WHERE Name LIKE @Name";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@Name", "%" + this.CategoryName + "%");

                Database.ads = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ads.Fill(Database.tbl);
                dg.Rows.Clear();

                foreach (DataRow dr in Database.tbl.Rows)
                {
                    this.Id = Convert.ToInt32(dr["ID"]);
                    this.CategoryName = dr["Name"].ToString();
                    object[] row = { this.Id, this.CategoryName };
                    dg.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Search Category: {ex.Message}");
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
                    this._sql = "delete from tblCategory where Id=@Id";
                    Database.cmd = new SqlCommand(this._sql, Database.con);
                    Database.cmd.Parameters.AddWithValue("@Id", this.Id);
                    this._Rowffecticted = Database.cmd.ExecuteNonQuery();
                    if (this._Rowffecticted > 0)
                    {
                        MessageBox.Show("Delete Category Sucessful");
                        dg.Rows.Remove(DGV);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Deleted Category: {ex.Message}");

            }
        }

        public void TranferToControls(DataGridView dg, TextBox txtRole)
        {
            if (dg.Rows.Count <= 0)
            {
                return;
            }
            DataGridViewRow DGV = new DataGridViewRow();
            DGV = dg.SelectedRows[0];
            txtRole.Text = DGV.Cells[1].Value.ToString();
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
                this._sql = "update tblCategory set Name=@Name where Id=@Id";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@Name", this.CategoryName);
                Database.cmd.Parameters.AddWithValue("@Id", this.Id);

                this._Rowffecticted = Database.cmd.ExecuteNonQuery();
                if (this._Rowffecticted > 0)
                {
                    MessageBox.Show("Update Category Sucessful");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Update Category {ex.Message}");
            }
        }
    }
}
