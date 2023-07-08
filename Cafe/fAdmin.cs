using Cafe.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Cafe
{
    public partial class fAdmin : Form
    {
        public fAdmin()
        {
            InitializeComponent();
            LoadDateTimePickerReport();
            LoadListMenu();
        }
        //BindingSource listMenu = new BindingSource();
        private void btnReport_Click(object sender, EventArgs e)
        {
            LoadReport(dtpStart.Value, dtpEnd.Value);
            LoadTrend(dtpStart.Value, dtpEnd.Value);
        }
        void LoadReport(DateTime startDay, DateTime endDay)
        {
            DataTable data = BillDAO.Instance.Report(startDay, endDay);
            dgvReport.DataSource = data;
            float totalSoldMoney = 0;
            if (data.Rows.Count > 0)
            {
                foreach (DataRow item in data.Rows)
                {
                    totalSoldMoney += (float)Convert.ToDouble(item["Tổng tiền"]);
                }
                CultureInfo culture = new CultureInfo("vi-VN");
                txbTotalSoldMoney.Text = totalSoldMoney.ToString("c", culture);
            }
        }
        void LoadTrend(DateTime startDay, DateTime endDay)
        {
            DataTable data = BillDAO.Instance.Trend(startDay, endDay);
            dgvTrend.DataSource = data;
            dgvTrend.Columns["Giá bán"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvTrend.Columns["Giá bán"].Width = 80;
            dgvTrend.Columns["Tên sản phẩm"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvTrend.Columns["Tên sản phẩm"].Width = 140;
            int totalSoldNumber = 0;
            if (data.Rows.Count > 0)
            {
                foreach (DataRow item in data.Rows)
                {
                    totalSoldNumber += Convert.ToInt32(item["Tổng số lượng"]);
                }
                txbTotalSoldNumber.Text = totalSoldNumber.ToString();
            }
        }
        void LoadDateTimePickerReport()
        {
            DateTime today = DateTime.Today;
            dtpStart.Value = new DateTime(today.Year, today.Month, 1);
            dtpEnd.Value = dtpStart.Value.AddMonths(1).AddDays(-1);
            dtpStart.Format = DateTimePickerFormat.Custom;
            dtpStart.CustomFormat = "'Ngày 'dd' tháng 'MM' năm 'yyyy";
            dtpEnd.Format = DateTimePickerFormat.Custom;
            dtpEnd.CustomFormat = "'Ngày 'dd' tháng 'MM' năm 'yyyy";
        }
        void LoadListMenu()
        {
            //listMenu.DataSource = MenuDAO.Instance.GetMenu();
            //dgvListMenu.DataSource = listMenu.DataSource;
            dgvListMenu.DataSource = MenuDAO.Instance.GetMenu();
            //set column width on datagrid view
            dgvListMenu.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvListMenu.Columns["ID"].Width = 40;
            LoadMenuBinding();
        }
        void LoadMenuBinding()
        {        
            txbMenuName.DataBindings.Clear();
            txbMenuPrice.DataBindings.Clear();
            txbID.DataBindings.Clear();
            txbMenuName.DataBindings.Add(new Binding("Text", dgvListMenu.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txbMenuPrice.DataBindings.Add(new Binding("Text", dgvListMenu.DataSource, "Price", true, DataSourceUpdateMode.Never));
            txbID.DataBindings.Add(new Binding("Text", dgvListMenu.DataSource, "ID", true, DataSourceUpdateMode.Never));
        }

        private void dgvListMenu_SelectionChanged(object sender, EventArgs e)
        {
            //LoadMenuBinding();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            float price;
            bool check;
            txbID.Text = "Auto";
            check = float.TryParse(txbMenuPrice.Text, out price);
            if (!check)
            {
                MessageBox.Show("Nhập sai định dạng Đơn giá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string name = txbMenuName.Text;
            string query = String.Format("select * from dbo.Menu where Name = N'{0}' and status = 1", name);
            DataTable result = DataProvider.Instance.ExecuteQuery(query);
            if (result.Rows.Count > 0)
            {
                DialogResult wanna = MessageBox.Show("Đã tồn tại tên \"" + name + "\"!\n" +
                    "Bạn có muốn thêm sản phẩm cùng tên?", "Thông báo",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (wanna == DialogResult.OK)
                {
                    if (!MenuDAO.Instance.InsertMenu(name, price))
                        MessageBox.Show("Thêm không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadListMenu();
                    }
                }
            }
            else
            {
                if (!MenuDAO.Instance.InsertMenu(name, price))
                    MessageBox.Show("Thêm không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadListMenu();
                }
            }
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            float price;
            bool check;
            check = float.TryParse(txbMenuPrice.Text, out price);
            if (!check)
            {
                MessageBox.Show("Nhập sai định dạng Đơn giá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string name = txbMenuName.Text;
            int ID = Convert.ToInt32(txbID.Text);
            if (!MenuDAO.Instance.UpdateMenu(ID, name, price))
                MessageBox.Show("Sửa không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                MessageBox.Show("Sửa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListMenu();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txbID.Text == "Auto")
            {
                MessageBox.Show("Chọn món cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show("Xác nhận xóa?", "Cảnh báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.OK)
            {
                if (!MenuDAO.Instance.DeleteMenu(Convert.ToInt32(txbID.Text)))
                    MessageBox.Show("Xóa không thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadListMenu();
                }
            }
        }
    }
}
