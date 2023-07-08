using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.DAO
{
    public class BillDAO
    {
        private static BillDAO instance;
        private BillDAO() { }

        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return instance; }
            private set => instance = value;
        }
        public int GetBillID(int ID)
        {
            string query = "select * from dbo.Bill where Table_ID = " + ID + " and status = 0";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            if (data.Rows.Count > 0)
            {
                DTO.Bill bill = new DTO.Bill(data.Rows[0]);
                return bill.ID;
            }
            return -1;
        }
        public void InsertBill(int idTable)
        {
            string query = "exec USP_InsertBill " + idTable + ", " + AccountDAO.Instance.GetMemberID();
            DataProvider.Instance.ExecuteNonQuery(query);
        }
        public int GetMaxID()
        {
            try
            {
                string query = "select MAX(ID) from dbo.Bill";
                return Convert.ToInt32(DataProvider.Instance.ExecuteScalar(query));
            }
            catch { return 1; }
        }
        public void SaveBill(int idTable)
        {
            string query = String.Format("exec USP_SaveBill {0}, {1}", idTable, AccountDAO.Instance.GetMemberID());
            DataProvider.Instance.ExecuteNonQuery(query);
        }
        public DataTable Report(DateTime startDay, DateTime endDay)
        {
            string query = "exec USP_Report @startDay , @endDay ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { startDay, endDay});
            return data;
        }
        public DataTable Trend(DateTime startDay, DateTime endDay)
        {
            string query = "exec USP_Trend @startDay , @endDay ";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { startDay, endDay });
            return data;
        }
    }
}
