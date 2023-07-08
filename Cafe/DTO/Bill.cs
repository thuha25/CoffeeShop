using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.DTO
{
    public class Bill
    {
        
        private int iD;
        public int ID { get => iD; set => iD = value; }
        
        private DateTime? date;
        public DateTime? Date { get => date; set => date = value; }

        public Bill(int iD, DateTime? date)
        {
            this.ID = iD;
            this.Date = date;
        }
        public Bill(DataRow row)
        {
            this.ID=Convert.ToInt32(row["ID"]);
            var DateT = row["Date"];
            if (DateT.ToString() != "" )
                this.Date = (DateTime?)(DateT);
        }
    }
}
