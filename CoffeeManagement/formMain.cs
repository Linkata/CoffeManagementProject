using CoffeeManagement.Model;
using CoffeeManagement.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeManagement
{
    public partial class formMain : Form
    {
        public formMain()
        {
            InitializeComponent();
        }
        static formMain _obj;
        public static formMain Instance
        {
            get
            {
                if (_obj == null)
                {
                    _obj = new formMain();
                }
                return _obj;
            }
        }

        public void AddControls(Form f)
        {
            guna2Panel3.Controls.Clear();
            f.Dock = DockStyle.Fill;
            f.TopLevel = false;
            guna2Panel3.Controls.Add(f);
            f.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            AddControls(new frmKitchenView());
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            frmPOS frm = new frmPOS();
            frm.Show();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            AddControls(new frmStaffView());
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            AddControls(new frmTableView());
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            AddControls(new frmProductView());
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            AddControls(new frmCategoryView());
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AddControls(new frmHome());
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void formMain_Load(object sender, EventArgs e)
        {
            lblUser.Text = MainClass.USER;
            _obj = this;
        }
    }
}
