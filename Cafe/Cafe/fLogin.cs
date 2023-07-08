using Cafe.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cafe
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txbUserName.Text;
            string password = txbPassword.Text;
            int login = Login(username, password);
            if (login == 0)
            {
                fTable open_fTable = new fTable();
                this.Hide();
                open_fTable.ShowDialog();
                this.Show();
            }
            else if (login == 1)
            {
                fAdmin open_Admin = new fAdmin();
                this.Hide();
                open_Admin.ShowDialog();
                this.Show();
            }
            else MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác", "Thông báo ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private sbyte Login(string username, string password)
        {
            
            return AccountDAO.Instance.Login(username, password);
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (MessageBox.Show("Bạn có muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.OK)
            //    e.Cancel = true;
        }
    }
}
