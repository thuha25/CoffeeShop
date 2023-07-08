using Cafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.DAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;

        public static BillInfoDAO Instance
        {
            get { if (instance == null) instance = new BillInfoDAO(); return instance; }
            private set => instance = value;
        }
        private BillInfoDAO() { }
        public List<DTO.BillInfo> GetListBillInfo(int ID)
        {
            List<DTO.BillInfo> listBillInfo = new List<DTO.BillInfo>();
            string query = "select * from dbo.BillInfo where Bill_ID = " + ID;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach(DataRow row in data.Rows)
            {
                DTO.BillInfo billinfo = new DTO.BillInfo(row);
                listBillInfo.Add(billinfo);
            }
            return listBillInfo;
        }
        public void InsertBillInfo(int Bill_ID, int Menu_ID, byte Number)
        {
            string query = "exec USP_InsertBillInfo " + Bill_ID + "," + Menu_ID + "," + Number;
            DataProvider.Instance.ExecuteNonQuery(query);
        }
    }
}
