using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.DTO
{
    public class BillInfo
    {
        private int menuID;
        private int billID;
        private int number;

        public int BillID { get => billID; set => billID = value; }
        public int MenuID { get => menuID; set => menuID = value; }
        public int Number { get => number; set => number = value; }
        public BillInfo(int foodID, int billID, int number)
        {
            this.MenuID = foodID;
            this.BillID = billID;
            this.Number = number;
        }
        public BillInfo(DataRow row)
        {
            this.BillID = Convert.ToInt32(row["Bill_ID"]);
            this.MenuID = Convert.ToInt32(row["Menu_ID"]);
            this.Number = Convert.ToInt32(row["Number"]);
        }
    }
}
