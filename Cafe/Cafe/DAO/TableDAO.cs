using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.DAO
{
    public static class TableDAO
    {
        public static int tableWeight = 90;
        public static int tableHeight = 90;
        //private static TableDAO instance;

        //public static TableDAO Instance
        //{
        //    get { if (instance == null) instance = new TableDAO(); return instance; }
        //    private set => instance = value;
        //}
        static TableDAO() { }
        public static List<DTO.Table> LoadTableList()
        {
            List<DTO.Table> tableList = new List<DTO.Table>();
            string query = "EXEC USP_GetTableList";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in data.Rows)
            {
                DTO.Table table = new DTO.Table(row);
                tableList.Add(table);
            }
            return tableList;
        }
        public static void SetTableStatus(int TableID, bool status)
        {
            string query = "exec USP_SetTableStatus " + TableID + ", " + status;
            DataProvider.Instance.ExecuteNonQuery(query);
        }
    }
}
