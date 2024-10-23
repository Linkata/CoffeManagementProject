using CoffeeManagement.CMDataSet1TableAdapters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;
using System.Drawing;


namespace CoffeeManagement
{
    class MainClass
    {
        // chuỗi kết nối
        public static readonly string con_string = "Data Source=LAPTOP-EF65QNAB\\SQLEXPRESS;Initial Catalog=CM;Persist Security Info=True;User ID=sa;Password=sa;";
        public static SqlConnection con = new SqlConnection(con_string);


        // Method to check user validation
        public static bool IsValidUser(string user, string pass)
        {
            bool isValid = false;
            string qry = @"Select * from users where username ='" + user + "' and upass ='" + pass + "'";
            SqlCommand cmd = new SqlCommand(qry, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0) 
            {
                isValid = true;
                USER = dt.Rows[0]["uName"].ToString();
            }
            return isValid;
        }
        //create property for username
        public static string user;
        public static string USER
        {
            get { return user; }
            private set { user = value; }
        }
        //method to cord operation
        public static int SQL(string qry, Hashtable ht)
        {
            int res = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(qry, con);
                cmd.CommandType = CommandType.Text;
                foreach(DictionaryEntry item in ht)
                {
                    cmd.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                }
                if (con.State == ConnectionState.Closed) {con.Open();}
                
                res = cmd.ExecuteNonQuery();
              
                if (con.State == ConnectionState.Open) { con.Close(); }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                con.Close();
            }
            return res;
        }
        //for loading data from database
        public static void LoadData(string qry, DataGridView gv, ListBox lb)
        {
            gv.CellFormatting += new DataGridViewCellFormattingEventHandler(gv_CellFormatting);
            try
            {
                using (SqlConnection con = new SqlConnection(con_string))
                {
                    

                    SqlCommand cmd = new SqlCommand(qry, con);
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Kiểm tra nếu DataTable có dữ liệu
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < lb.Items.Count; i++)
                        {
                            string colName = ((DataGridViewColumn)lb.Items[i]).Name;
                            if (i < dt.Columns.Count)
                            {
                                gv.Columns[colName].DataPropertyName = dt.Columns[i].ColumnName;
                            }
                        }
                        gv.DataSource = dt;
                    }
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public static bool IsCategoryTableEmpty()
        {
            bool isEmpty = true;
            string qry = "SELECT COUNT(*) FROM category";
            using (SqlConnection con = new SqlConnection(con_string))
            {
                SqlCommand cmd = new SqlCommand(qry, con);
                con.Open();
                int count = (int)cmd.ExecuteScalar();
                if (count > 0)
                {
                    isEmpty = false;
                }
            }
            return isEmpty;
        }
        private static void gv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) { 
            Guna.UI2.WinForms.Guna2DataGridView gv = (Guna.UI2.WinForms.Guna2DataGridView)sender;
            int count = 0;
            foreach (DataGridViewRow row in gv.Rows) {
                count++;
                row.Cells[0].Value = count;
            }
        }
        public static void BlurBackground(Form Model)
        {
            Form Background = new Form();
            using (Model)
            {
                Background.StartPosition = FormStartPosition.Manual;
                Background.FormBorderStyle = FormBorderStyle.None;
                Background.Opacity = 0.5d;
                Background.BackColor = Color.Black;
                Background.Size = formMain.Instance.Size;
                Background.Location = formMain.Instance.Location ;
                Background.ShowInTaskbar = false;
                Background.Show();
                Model.Owner = Background;
                Model.ShowDialog(Background);
                Background.Dispose();
            }
        }
        //cb fill
        public static void CBFill(string qry, ComboBox cb)
        {
            SqlCommand cmd = new SqlCommand(qry,con);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cb.DisplayMember = "name";
            cb.ValueMember = "id";
            cb.DataSource = dt;
            cb.SelectedIndex = -1;
        
        }
    }

}
