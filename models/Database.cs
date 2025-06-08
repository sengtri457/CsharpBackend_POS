using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Group1_POS.models
{
    internal class Database
    {
        public static SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-QAFAUT7\SQLEXPRESS;Initial Catalog=POS_DB;Persist Security Info=True;User ID=sa;Password=123@;Encrypt=False");
        public static SqlCommand cmd = null;
        public static SqlDataAdapter ads = null;
        public static DataTable tbl = null;
      public static void ConnectionDB()
        {
            try
            {
                if (Database.con.State == System.Data.ConnectionState.Closed) { 
             
                    Database.con.Open();
/*                    MessageBox.Show("DataBase Connectes!!");*/
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error Connection {ex.Message}");
            }
        }
    }
}
