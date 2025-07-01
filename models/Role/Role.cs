using Group1_POS.models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyAction = Group1_POS.models.Interface.Action;
using System.Data.SqlClient;
using System.Data;
namespace Group1_POS.models.Role
{
    internal class Role : MyAction
    {
        public int Id { set; get; }
        public string RoleName { set; get; }
        private string _sql;
        private int _Rowffecticted;
        public bool IsDubplicateCheck()
        {
            try
            {
                this._sql = "select * from tblRole where RoleName=@RoleName" ;
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@RoleName", this.RoleName);
                Database.cmd.ExecuteNonQuery();
                Database.ads = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ads.Fill(Database.tbl);
                if (Database.tbl.Rows.Count > 0)
                {
                    MessageBox.Show("Dubplicate RoleName");
                    return true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error Duplicate{ex.Message}");
            }
            return false;

        }
        public override void createRole()
        {
          try
            {
                Database.ConnectionDB();
                if (IsDubplicateCheck() == true)
                {
                    return;
                }
                this._sql = "insert into tblRole(RoleName)Values(@RoleName)";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@RoleName", this.RoleName);
                this._Rowffecticted= Database.cmd.ExecuteNonQuery();
                if(this._Rowffecticted > 0)
                {
                    MessageBox.Show("Create Role Sucessful");
                }
            }
            catch (Exception ex) {
                MessageBox.Show($"Error Create Row {ex.Message}");
            }
        }


        public override void getDataGrid(DataGridView dg)
        {
            try
            {
                Database.ConnectionDB();

                this._sql = "select * from tblRole";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.ExecuteNonQuery();
                Database.ads = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ads.Fill(Database.tbl);
                dg.Rows.Clear();
                foreach (DataRow dr in Database.tbl.Rows) {

                    this.Id = int.Parse(dr["ID"].ToString());
                    this.RoleName = dr["RoleName"].ToString();
                    object[] row = { this.Id, this.RoleName };
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
                this._sql = "SELECT * FROM tblRole WHERE RoleName LIKE @RoleName";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@RoleName", "%" + this.RoleName + "%");

                Database.ads = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ads.Fill(Database.tbl);
                dg.Rows.Clear();

                foreach (DataRow dr in Database.tbl.Rows)
                {
                    this.Id = Convert.ToInt32(dr["ID"]);
                    this.RoleName = dr["RoleName"].ToString();
                    object[] row = { this.Id, this.RoleName };
                    dg.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Search Data: {ex.Message}");
            }

        }


        public override void deleted(DataGridView dg)
        {
            try
            {

                if(dg.Rows.Count <= 0)
                {
                    return;
                }

                var click = MessageBox.Show("Do you Want To delete This Record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (click == DialogResult.Yes)
                {
                    DataGridViewRow DGV = new DataGridViewRow();
                    DGV = dg.SelectedRows[0];
                    this.Id = int.Parse(DGV.Cells[0].Value.ToString());
                    this._sql = "delete from tblRole where Id=@Id";
                    Database.cmd = new SqlCommand(this._sql, Database.con);
                    Database.cmd.Parameters.AddWithValue("@Id", this.Id);
                    this._Rowffecticted=Database.cmd.ExecuteNonQuery();
                    if (this._Rowffecticted > 0)
                    {
                        MessageBox.Show("Delete Role Sucessful");
                        dg.Rows.Remove(DGV);
                    }
                }

                }catch(Exception ex)
            {
                MessageBox.Show($"Error Deleted Data: {ex.Message}");

            }
        }

        public void TranferToControls(DataGridView dg, TextBox txtRole)
        {
            if(dg.Rows.Count <= 0)
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
                this.Id =int.Parse(DGV.Cells[0].Value.ToString());
                this._sql = "update tblRole set RoleName=@RoleName where Id=@Id";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@RoleName", this.RoleName);
                Database.cmd.Parameters.AddWithValue("@Id", this.Id);

                this._Rowffecticted = Database.cmd.ExecuteNonQuery();
                if (this._Rowffecticted > 0)
                {
                    MessageBox.Show("Update Role Sucessful");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Update Row {ex.Message}");
            }
        }
    }
    }
