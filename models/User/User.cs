using Group1_POS.models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using MyAction = Group1_POS.models.Interface.Action;
using Group1_POS.Views;
using System.Linq.Expressions;

namespace Group1_POS.models.User
{
    internal class User : MyAction
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public  bool Status { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateAt { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateAt { get; set; }
        public  static int UserId { get; set; }
        private string _sql = "";
        public void LogIn( Form form)
        {
            try
            {
                Database.ConnectionDB();
                this._sql = "select * from tableUser where UserName=@UserName AND [Password]=@Password AND Status= 1";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@UserName", this.UserName);
                Database.cmd.Parameters.AddWithValue("@Password", this.Password);
                Database.cmd.ExecuteNonQuery();
                Database.ads = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ads.Fill(Database.tbl);


                if(Database.tbl.Rows.Count > 0)
                {
                    DashBoard das = new DashBoard();
                    das.Show();
                    das.AdminLabel.Text = "Welcome: " + this.UserName;
/*                    das.toolStripStatusLabelUserName.Text = "UserName: " + this.UserName;*/
                    User.UserId = int.Parse(Database.tbl.Rows[0]["Id"].ToString());
                    MessageBox.Show("Login Sucessful");
                    form.Hide();
                }
                else
                {
                    MessageBox.Show("Username and Password is invalid !!!", "Log In", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erroe Login: {ex.Message}");

            }
        }
        private int _Rowffecticted;

        public void SetRoleName( ComboBox cboRoleName)
        {
            try
            {

                this._sql = "select * from tblRole";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.ExecuteNonQuery();
                Database.ads = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ads.Fill(Database.tbl);
                cboRoleName.Items.Clear();
                foreach(DataRow r in Database.tbl.Rows)
                {
                    
                    object[] row = {};
                    cboRoleName.Items.Add(r["RoleName"].ToString());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        public int GetRoleId(ComboBox cboRoleName)
        {
                int id = 0;
            try
            {

                this._sql = "select * from tblRole where RoleName =@RoleName";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@RoleName", cboRoleName.Text);
                Database.cmd.ExecuteNonQuery();
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
        public bool IsDubplicateCheck()
        {
            try
            {
                this._sql = "select * from tableUser where UserName=@UserName";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@UserName", this.UserName);
                Database.cmd.ExecuteNonQuery();
                Database.ads = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ads.Fill(Database.tbl);
                if (Database.tbl.Rows.Count > 0)
                {
                    MessageBox.Show("Dubplicate UserName");
                    return true;
                }
            }
            catch (Exception ex)
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
                this._sql = "insert into tableUser(UserName,Password,Gender,Email,Status,CreateBy,CreateAt,UpdateBy,UpdateAt) Values(@UserName,@Password,@Gender,@Email,@Status,@CreateBy, GETDATE(),@UpdateBy, GETDATE())";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@UserName", this.UserName);
                Database.cmd.Parameters.AddWithValue("@Password", this.Password);
                Database.cmd.Parameters.AddWithValue("@Gender", this.Gender);
                Database.cmd.Parameters.AddWithValue("@Email", this.Email);
                Database.cmd.Parameters.AddWithValue("@Status", this.Status);
                Database.cmd.Parameters.AddWithValue("@CreateBy", User.UserId);
                Database.cmd.Parameters.AddWithValue("@UpdateBy", User.UserId);
                this._Rowffecticted = Database.cmd.ExecuteNonQuery();
                if (this._Rowffecticted > 0)
                {
                    MessageBox.Show("Create User Sucessful");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Create User {ex.Message}");
            }
        }


        public override void getDataGrid(DataGridView dg)
        {
            try
            {
                Database.ConnectionDB();

                this._sql = "SELECT * FROM tableUser";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.ads = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ads.Fill(Database.tbl);

                dg.Rows.Clear();

                foreach (DataRow dr in Database.tbl.Rows)
                {
                    this.Id = Convert.ToInt32(dr["ID"]);
                    this.UserName = dr["UserName"].ToString();
                    this.Email = dr["Email"].ToString();
                    this.Gender = dr["Gender"].ToString();
                    this.Status = Convert.ToBoolean(dr["Status"]);
                    this.Password = dr["Password"].ToString();
                    this.CreateBy = dr["CreateBy"].ToString();
                    this.CreateAt = Convert.ToDateTime(dr["CreateAt"]);
                    this.UpdateBy = dr["CreateBy"].ToString();
                    this.UpdateAt = Convert.ToDateTime(dr["CreateAt"]);
                    object[] row = { this.Id, this.UserName, this.Email, this.Gender, this.Password,this.Status ,this.CreateAt,this.CreateBy, this.UpdateBy,this.UpdateAt };
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
                this._sql = "SELECT * FROM tableUser WHERE UserName LIKE '%'+@UserName+'%'";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@UserName",this.UserName);
                Database.ads = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ads.Fill(Database.tbl);
                dg.Rows.Clear();

                foreach (DataRow dr in Database.tbl.Rows)
                {
                    this.Id = Convert.ToInt32(dr["ID"]);
                    this.UserName = dr["UserName"].ToString();
                    this.Email = dr["Email"].ToString();
                    this.Gender = dr["Gender"].ToString();
                    this.Status = Convert.ToBoolean(dr["Status"]);
                    this.Password = dr["Password"].ToString();
                    this.CreateBy = dr["CreateBy"].ToString();
                    this.CreateAt = Convert.ToDateTime(dr["CreateAt"]);
                    this.UpdateBy = dr["CreateBy"].ToString();
                    this.UpdateAt = Convert.ToDateTime(dr["CreateAt"]);
                    object[] row = { this.Id, this.UserName, this.Email, this.Gender, this.Password, this.Status, this.CreateAt, this.CreateBy, this.UpdateBy, this.UpdateAt };
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
                    this._sql = "delete from tableUser where Id=@Id";
                    Database.cmd = new SqlCommand(this._sql, Database.con);
                    Database.cmd.Parameters.AddWithValue("@Id", this.Id);
                    this._Rowffecticted = Database.cmd.ExecuteNonQuery();
                    if (this._Rowffecticted > 0)
                    {
                        MessageBox.Show("Delete Role Sucessful");
                        dg.Rows.Remove(DGV);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Deleted Data: {ex.Message}");

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
                this._sql = "update tableUser set UserName=@UserName where Id=@Id";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@UserName", this.UserName);
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

