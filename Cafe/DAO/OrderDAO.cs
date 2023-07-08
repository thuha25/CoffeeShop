using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.DAO
{
    public class OrderDAO
    {
        private static OrderDAO instance;

        public static OrderDAO Instance
        {
            get { if (instance == null) instance = new OrderDAO(); return instance; }
            private set => instance = value;
        }
        public List<DTO.Order> GetOrderList(int tableID)
        {
            List<DTO.Order> orderList = new List<DTO.Order>();
            string query = "EXEC USP_GetOrderList " + tableID;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                DTO.Order order = new DTO.Order(item);
                orderList.Add(order);
            }
            return orderList;
        }
    }
}
