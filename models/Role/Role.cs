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
        private string _sql;
        private int _Rowffecticted;
        public string RoleName { set; get; }
        public bool IsDubplicateCheck()
        {
            try
            {
                this._sql = "select * from tblrRole where RoleName= @RoleName";
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
                this._sql = "insert into tblrRole(RoleName)Values(@RoleName)";
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

    }
}
