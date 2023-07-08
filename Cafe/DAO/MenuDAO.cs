using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cafe.DAO
{
    public class MenuDAO
    {
        private static MenuDAO instance;

        public static MenuDAO Instance
        {
            get { if (instance == null) instance = new MenuDAO(); return instance; }
            private set => instance = value;
        }
        private MenuDAO() { }
        public List<DTO.Menu> GetMenu()
        {
            List<DTO.Menu> list = new List<DTO.Menu>();
            string query = "select * from dbo.Menu where status = 1";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach(DataRow item in data.Rows)
            {
                DTO.Menu menu = new DTO.Menu(item);
                list.Add(menu);
            }
            return list;
        }
        public bool InsertMenu(string name, float price)
        {
            string query = string.Format("exec USP_InsertMenu N'{0}', {1}", name, price);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool UpdateMenu(int ID, string name, float price)
        {
            string query = string.Format("exec USP_UpdateMenu {0}, N'{1}', {2}", ID, name, price);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
        public bool DeleteMenu(int ID)
        {
            string query = string.Format("exec USP_DeleteMenu {0}", ID);
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            return result > 0;
        }
    }
}
