using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.DTO
{
    public class Table
    {
        private int iD;
        private string status;
        private string name;

        public int ID { get => iD; set => iD = value; }
        public string Status { get => status; set => status = value; }
        public string Name { get => name; set => name = value; }

        public Table(int ID, string name, string status) 
        {
            this.ID = ID;
            this.Name = name;
            this.Status = status;
        }
        public Table(DataRow row)
        {
            this.ID = Convert.ToInt32(row["ID"]);
            this.Name = Convert.ToString(row["Name"]);
            this.Status = (Convert.ToBoolean(row["status"]))? "Bận" : "Trống";
        }
    }
}
