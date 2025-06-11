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
                    das.toolStripStatusLabelUserName.Text = "UserName: " + this.UserName;
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
        }
    }
