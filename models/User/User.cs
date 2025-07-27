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
        public int RoleId { get; set; }
        public static string PermissionRolename { get; set; }
        private string _sql = "";
        public void LogIn( Form form)
        {
            try
            {
                Database.ConnectionDB();
                this._sql = "select * from view_User where UserName=@UserName AND [Password]=@Password AND Status= 1";
                Database.cmd = new SqlCommand(this._sql, Database.con);
                Database.cmd.Parameters.AddWithValue("@UserName", this.UserName);
                Database.cmd.Parameters.AddWithValue("@Password", this.Password);
                Database.cmd.ExecuteNonQuery();
                Database.ads = new SqlDataAdapter(Database.cmd);
                Database.tbl = new DataTable();
                Database.ads.Fill(Database.tbl);


                if(Database.tbl.Rows.Count > 0)
                {
                    GetUserName();
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

        public void GetUserName()
        {
            DashBoard das = new DashBoard();
            das.AdminLabel.Text = "Welcome: " + this.UserName;
            User.UserId = int.Parse(Database.tbl.Rows[0]["Id"].ToString());
            User.PermissionRolename = Database.tbl.Rows[0]["RoleName"].ToString();
            das.RoleLabel.Text = "Role: " + PermissionRolename;
            das.Show();

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
            SqlTransaction sqlTransaction = null;
        public override void createRole(DataGridView dg)
        {
            try
            {
                Database.ConnectionDB();
               
                if (IsDubplicateCheck() == true)
                {
                    return;
                }
                sqlTransaction = Database.con.BeginTransaction();
                this._sql = "insert into tableUser(UserName,Password,Gender,Email,Status,CreateBy,CreateAt,UpdateBy,UpdateAt) Values(@UserName,@Password,@Gender,@Email,@Status,@CreateBy, GETDATE(),@UpdateBy, GETDATE()) select SCOPE_IDENTITY()";
                Database.cmd = new SqlCommand(this._sql, Database.con, sqlTransaction); // Use the transaction
                Database.cmd.Parameters.AddWithValue("@UserName", this.UserName);
                Database.cmd.Parameters.AddWithValue("@Password", this.Password);
                Database.cmd.Parameters.AddWithValue("@Gender", this.Gender);
                Database.cmd.Parameters.AddWithValue("@Email", this.Email);
                Database.cmd.Parameters.AddWithValue("@Status", this.Status);
                Database.cmd.Parameters.AddWithValue("@CreateBy", User.UserId);
                Database.cmd.Parameters.AddWithValue("@UpdateBy", User.UserId);
                this.Id = Convert.ToInt32(Database.cmd.ExecuteScalar());
                this._sql = "insert into tblUserRole(UserId,RoleId) Values(@UserId,@RoleId)";
                Database.cmd = new SqlCommand(this._sql, Database.con, sqlTransaction); // Use the transaction
                Database.cmd.Parameters.AddWithValue("@UserId", this.Id);
                Database.cmd.Parameters.AddWithValue("@RoleId", this.RoleId);
                Database.cmd.ExecuteNonQuery();
                sqlTransaction.Commit(); // Commit the transaction

                MessageBox.Show("Create User Sucessful");
                getDataGrid(dg);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Create User {ex.Message}");
                sqlTransaction.Rollback(); // Rollback the transaction if an error occurs
            }
        }


        public override void getDataGrid(DataGridView dg)
        {
            try
            {
                Database.ConnectionDB();

                this._sql = "SELECT * FROM View_User";
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
                    string roleName = dr["RoleName"].ToString();
                    object[] row = { this.Id, this.UserName, this.Email, this.Gender, this.Password,this.Status ,this.CreateAt,this.CreateBy,roleName };
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
                this._sql = "SELECT * FROM View_User WHERE UserName LIKE '%'+@UserName+'%'";
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
                    string roleName = dr["RoleName"].ToString();
                    object[] row = { this.Id, this.UserName, this.Email, this.Gender, this.Password, this.Status, this.CreateAt, this.CreateBy, this.UpdateBy, this.UpdateAt,roleName };
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
                    this._sql = "update view_User set Status =0 where Id=@Id";
                    Database.cmd = new SqlCommand(this._sql, Database.con);
                    Database.cmd.Parameters.AddWithValue("@Id", this.Id);
                    this._Rowffecticted = Database.cmd.ExecuteNonQuery();
                    if (this._Rowffecticted > 0)
                    {
                        MessageBox.Show("Delete User Sucessful");
                        dg.Rows.Remove(DGV);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Deleted Data: {ex.Message}");

            }
        }

        public void TranferToControls(DataGridView dg, TextBox txtRole, ComboBox cboGender, TextBox txtEmail, TextBox txtPass, ComboBox cboRole, RadioButton rTrue, RadioButton rFalse)
        {
            if (dg.Rows.Count <= 0)
            {
                return;
            }
            DataGridViewRow DGV = new DataGridViewRow();
            DGV = dg.SelectedRows[0];
            txtRole.Text = DGV.Cells[1].Value.ToString();
            cboGender.Text = DGV.Cells[3].Value.ToString();
            txtEmail.Text = DGV.Cells[2].Value.ToString();
            txtPass.Text = DGV.Cells[4].Value.ToString();
            cboRole.Text = DGV.Cells[8].Value.ToString();

            string status = DGV.Cells[5].Value?.ToString()?.ToLower();

            if (status == "true")
            {
                rTrue.Checked = true;
                rFalse.Checked = false;
            }
            else
            {
                rTrue.Checked = false;
                rFalse.Checked = true;
            }
        }
            
        public override void update(DataGridView dg)
        {
            try
            {
                Database.ConnectionDB();

                if (IsDubplicateCheck() == true)
                {
                    return;
                }

                DataGridViewRow dataGridView = new DataGridViewRow();
                dataGridView = dg.SelectedRows[0];
                this.Id = int.Parse(dataGridView.Cells[0].Value.ToString());
                

                sqlTransaction = Database.con.BeginTransaction();
                this._sql = "update tableUser set UserName =@UserName , Gender=@Gender , Password=@Password , Email=@Email , Status=@Status , UpdateBy=@UpdateBy , UpdateAt= GETDATE() where Id= @Id";
                Database.cmd = new SqlCommand(this._sql, Database.con, sqlTransaction); // Use the transaction
                Database.cmd.Parameters.AddWithValue("@UserName", this.UserName);
                Database.cmd.Parameters.AddWithValue("@Password", this.Password);
                Database.cmd.Parameters.AddWithValue("@Gender", this.Gender);
                Database.cmd.Parameters.AddWithValue("@Email", this.Email);
                Database.cmd.Parameters.AddWithValue("@Status", this.Status);
                Database.cmd.Parameters.AddWithValue("@UpdateBy", User.UserId);
                Database.cmd.Parameters.AddWithValue("@Id", this.Id);
                Database.cmd.ExecuteNonQuery();

              /*  update Info UserRole*/

                this._sql = "update tblUserRole set RoleId =@RoleId where UserId=@UserId";
                Database.cmd = new SqlCommand(this._sql, Database.con, sqlTransaction);
                Database.cmd.Parameters.AddWithValue("@RoleId", this.RoleId);
                Database.cmd.Parameters.AddWithValue("@UserId", this.Id);
                Database.cmd.ExecuteNonQuery();
                sqlTransaction.Commit();
                MessageBox.Show("Updated User Sucessful");
                getDataGrid(dg);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Update Row {ex.Message}");
                sqlTransaction.Rollback(); 
            }
        }
    }
}

