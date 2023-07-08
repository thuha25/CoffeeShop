using Cafe.DAO;
using Cafe.DTO;
using Cafe.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;

namespace Cafe
{
    public partial class fTable : Form
    {
        public fTable()
        {
            InitializeComponent();

            LoadTable();
            LoadIDStaff();
            LoadMenu();
        }
        void LoadIDStaff()
        {
            lbIDStaff.Text = AccountDAO.Instance.GetMemberID().ToString();
        }
        void LoadTable()
        {
            List<DTO.Table> tables = TableDAO.LoadTableList();
            int i = 0;
            foreach (DTO.Table table in tables)
            {
                Button button = new Button() { Width = TableDAO.tableWeight, Height = TableDAO.tableHeight };
                button.Text = "Bàn "+ table.Name + "\n\n" + table.Status;
                button.Tag = table;
                table_button.Add(table.ID, i++);
                button.BackColor = ColorAtStatus(table.Status);
                button.Click += Button_Click;
                flpTable.Controls.Add(button);
            }
        }
        Color ColorAtStatus(string status)
        {
            return (status == "Trống")? Color.AliceBlue : Color.Crimson;
        }
        private void Button_Click(object sender, EventArgs e)
        {
            Table table = (sender as Button).Tag as Table;
            lvBill.Tag = table;
            lbTable.Text = table.Name;
            ShowBill(table);
        }
        void ShowBill(Table table)
        {
            lvBill.Items.Clear();
            List<DTO.Order> listbillinfo = OrderDAO.Instance.GetOrderList(table.ID);
            float totalPrice = 0;
            foreach(DTO.Order item in listbillinfo)
            {
                ListViewItem listViewItem = new ListViewItem(item.Name.ToString());
                listViewItem.SubItems.Add(item.Price.ToString());
                listViewItem.SubItems.Add(item.Number.ToString());
                listViewItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
                lvBill.Items.Add(listViewItem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            txbTotalPrice.Text = totalPrice.ToString("c", culture);
            lbDoc.Text = "";
            if (totalPrice>0) lbDoc.Text = NumberToText.Instance.FuncNumberToText(totalPrice);
            ReloadTable(table);
        }
        void ReloadTable(Table table)
        {
            if (lvBill.Items.Count > 0)
            {
                TableDAO.SetTableStatus(table.ID, true);
                table.Status = "Bận";
                btnPreview.Enabled = true;
                btnPrint.Enabled = true;
            }
            else
            {
                TableDAO.SetTableStatus(table.ID, false);
                table.Status = "Trống";
                btnPreview.Enabled = false;
                btnPrint.Enabled = false;
            }
            flpTable.Controls[table_button[table.ID]].Tag = table;
            flpTable.Controls[table_button[table.ID]].BackColor = ColorAtStatus(table.Status);
            flpTable.Controls[table_button[table.ID]].Text = "Bàn " + table.Name + "\n\n" + table.Status;
        }
        void LoadMenu()
        {
            List<DTO.Menu> menus = MenuDAO.Instance.GetMenu();
            cbMenu.DataSource = menus;
            cbMenu.DisplayMember = "Name";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Table table = lvBill.Tag as Table;
            if (table != null)
            {
                int idBill = BillDAO.Instance.GetBillID(table.ID);
                int idMenu = (cbMenu.SelectedItem as DTO.Menu).ID;
                byte number = Convert.ToByte(nudNumber.Value);
                if (idBill == -1)
                {
                    BillDAO.Instance.InsertBill(table.ID);
                    BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxID(), idMenu, number);
                }
                else
                {
                    BillInfoDAO.Instance.InsertBillInfo(idBill, idMenu, number);
                }
                ShowBill(table);
            }
            else MessageBox.Show("Chọn bàn trước", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        Dictionary<int, int> table_button = new Dictionary<int, int>();

        PrintDocument printDocument1 = new PrintDocument();
        private void SetUpSize()
        {
            printDocument1.DefaultPageSettings.PaperSize
             = new System.Drawing.Printing.PaperSize("Custom", 840, 800+lvBill.Items.Count*30);
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);

        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Image image = Resources.cafe;
            int Offset = 30, deltaOffset = 30;
            int StartX = 80, StartY = 25;
            string line = "".PadRight(120,'-');
            int Align01 = 25, Align02 = 250, Align03 = 450, Align04 = 560;
            Font font1 = new Font("Arial", 12, FontStyle.Regular);
            Font font2 = new Font("Arial", 20, FontStyle.Bold);
            Font font3 = new Font("Arial", 16, FontStyle.Italic);
            StringFormat formatLeft = new StringFormat(StringFormatFlags.NoClip);
            StringFormat formatCenter = new StringFormat(formatLeft);
            //StringFormat formatRight = new StringFormat(formatLeft);

            formatCenter.Alignment = StringAlignment.Center;
            //formatRight.Alignment = StringAlignment.Far;
            //formatLeft.Alignment = StringAlignment.Near;
            #region draw header
            e.Graphics.DrawImage(image, (e.PageBounds.Width - image.Width/2)/2,
                StartY, image.Width / 2, image.Height / 2);
            Offset += image.Height / 2 + deltaOffset;
            string CenterString = "HÓA ĐƠN BÁN HÀNG";
            e.Graphics.DrawString(CenterString,
                font2,
                Brushes.Black, new Point((e.PageBounds.Width - CenterString.Length/8) / 2, StartY + Offset),
                formatCenter);
            Offset += font2.Height + deltaOffset;
            e.Graphics.DrawString("IN LÚC: " + DateTime.Now.ToString("HH'H'mm    dd/MM/yyyy"), 
                font1,
                Brushes.Black, new Point(StartX, StartY + Offset));
            Offset += deltaOffset;
            e.Graphics.DrawString("HÓA ĐƠN SỐ: " + BillDAO.Instance.GetBillID((lvBill.Tag as Table).ID).ToString(),
                font1,
                Brushes.Black, new Point(StartX, StartY + Offset));
            Offset += deltaOffset;
            e.Graphics.DrawString("BÀN: " + (lvBill.Tag as Table).Name,
                font1,
                Brushes.Black, new Point(StartX, StartY + Offset));
            Offset += deltaOffset;
            e.Graphics.DrawString("NHÂN VIÊN: " + AccountDAO.Instance.GetMemberID().ToString(),
                font1,
                Brushes.Black, new Point(StartX, StartY + Offset));
            Offset += 2*deltaOffset;

            e.Graphics.DrawString("#",
                font1,
                Brushes.Black, new Point(StartX, StartY + Offset));
            e.Graphics.DrawString("Sản phẩm",
                font1,
                Brushes.Black, new Point(StartX+Align01, StartY + Offset));
            e.Graphics.DrawString("Đơn giá",
                font1,
                Brushes.Black, new Point(StartX+Align02, StartY + Offset));
            e.Graphics.DrawString("SL",
                font1,
                Brushes.Black, new Point(StartX+Align03, StartY + Offset));
            e.Graphics.DrawString("Thành tiền",
                font1,
                Brushes.Black, new Point(StartX+Align04, StartY + Offset));
            Offset += deltaOffset;
            e.Graphics.DrawString(line,
                font1,
                Brushes.Black, new Point(StartX, StartY + Offset));
            Offset += deltaOffset;
            #endregion

            #region draw item
            int i = 1;
            foreach(ListViewItem item in lvBill.Items)
            {
                e.Graphics.DrawString(i++.ToString(),
                    font1,
                    Brushes.Black, new Point(StartX, StartY + Offset));
                e.Graphics.DrawString(item.SubItems[0].Text,
                    font1,
                    Brushes.Black, new Point(StartX + Align01, StartY + Offset));
                e.Graphics.DrawString(item.SubItems[1].Text,
                    font1,
                    Brushes.Black, new Point(StartX + Align02, StartY + Offset));
                e.Graphics.DrawString(item.SubItems[2].Text,
                    font1,
                    Brushes.Black, new Point(StartX + Align03, StartY + Offset));
                e.Graphics.DrawString(item.SubItems[3].Text,
                    font1,
                    Brushes.Black, new Point(StartX + Align04, StartY + Offset));
                Offset += deltaOffset;
            }
            #endregion

            #region endbill
            e.Graphics.DrawString(line,
                font1,
                Brushes.Black, new Point(StartX, StartY + Offset));
            Offset += deltaOffset;
            e.Graphics.DrawString("Tổng cộng",
                    font1,
                    Brushes.Black, new Point(StartX + Align03 - 50, StartY + Offset));
            e.Graphics.DrawString(txbTotalPrice.Text.Trim(),
                    font1,
                    Brushes.Black, new Point(StartX + Align03 + 50, StartY + Offset));
            Offset += 2*deltaOffset;
            e.Graphics.DrawString("CẢM ƠN QUÝ KHÁCH VÀ HẸN GẶP LẠI",
                font3,
                Brushes.Black, new Point((e.PageBounds.Width - CenterString.Length / 8) / 2, StartY + Offset),
                formatCenter);
            #endregion 
        }
        

        private void btnPreview_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.PrintPreviewControl.Zoom = 0.75;
            SetUpSize();
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            SetUpSize();
            Table table = lvBill.Tag as Table;
            #region auto save in pdf
            printDocument1.PrinterSettings.PrinterName = "Microsoft Print to PDF";
            printDocument1.PrinterSettings.PrintFileName
                = @"D:\bill.pdf";
            printDocument1.PrinterSettings.PrintToFile = true;
            printDocument1.Print();
            BillDAO.Instance.SaveBill(table.ID);
            ShowBill(table);
            #endregion

            //#region showDialog
            //printDialog1.UseEXDialog = false;
            //DialogResult userResp = printDialog1.ShowDialog();
            //if (userResp == DialogResult.OK)
            //{
            //    if (printDialog1.PrinterSettings.PrinterName == "Microsoft Print to PDF")
            //    {   // force a reasonable filename
            //        string basename = Path.GetFileNameWithoutExtension("bill.pdf");
            //        string directory = Path.GetDirectoryName("bill.pdf");
            //        printDocument1.PrinterSettings.PrintToFile = true;
            //        // confirm the user wants to use that name
            //        SaveFileDialog pdfSaveDialog = new SaveFileDialog();
            //        pdfSaveDialog.InitialDirectory = directory;
            //        pdfSaveDialog.FileName = basename + ".pdf";
            //        pdfSaveDialog.Filter = "PDF File|*.pdf";
            //        userResp = pdfSaveDialog.ShowDialog();
            //        if (userResp != DialogResult.Cancel)
            //            printDocument1.PrinterSettings.PrintFileName = pdfSaveDialog.FileName;
            //    }

            //    if (userResp != DialogResult.Cancel)  // in case they canceled the save as dialog
            //    {
            //        printDocument1.Print();
            //        BillDAO.Instance.SaveBill(table.ID);
            //        ShowBill(table);
            //    }
            //}
            //#endregion
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Icon = Resources.preview;
            ((ToolStrip)printPreviewDialog1.Controls[1]).Items[0].Enabled = false;
        }

        private void flpTable_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
