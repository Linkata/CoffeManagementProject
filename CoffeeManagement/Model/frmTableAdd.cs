﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeManagement.Model
{
    public partial class frmTableAdd : SampleAdd
    {
        public frmTableAdd()
        {
            InitializeComponent();
        }
        public int id = 0;
        public override void btnSave_Click(object sender, EventArgs e)
        {
            string qry = "";
            Hashtable ht = new Hashtable();

            // Kiểm tra nếu bảng category không có dữ liệu
            string checkQry = "Select Count(*) From tables";
            int count = 0;

            using (SqlConnection con = new SqlConnection(MainClass.con_string))
            {
                SqlCommand cmd = new SqlCommand(checkQry, con);
                con.Open();
                count = (int)cmd.ExecuteScalar();
            }

            if (id == 0) // Insert
            {
                if (count == 0)
                {
                    // Nếu bảng trống, bật IDENTITY_INSERT và đặt catId = 1
                    qry = "SET IDENTITY_INSERT tables ON; " +
                          "Insert into tables (tid, tname) Values (1, @Name); " +
                          "SET IDENTITY_INSERT tables OFF;";
                }
                else
                {
                    qry = "Insert into tables (tname) Values (@Name)";
                }
            }
            else // Update
            {
                qry = "Update tables Set tname = @Name where tid = @id";
            }

            ht.Add("@id", id);
            ht.Add("@Name", txtName.Text);

            if (MainClass.SQL(qry, ht) > 0)
            {
                guna2MessageDialog1.Show("Saved successfully..");
                id = 0;
                txtName.Text = "";
                txtName.Focus();
            }
        }
    }
}
