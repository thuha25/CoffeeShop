using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.DTO
{
    public class Account
    {
        private int memberID;
        private sbyte type;

        public int MemberID { get => memberID; set => memberID = value; }
        public sbyte Type { get => type; set => type = value; }
        public Account(int memberID, sbyte type)
        {
            this.MemberID = memberID;
            this.Type = type;
        }
        public Account(DataRow row)
        {
            this.MemberID = Convert.ToInt32(row["Member_ID"]);
            this.Type = Convert.ToSByte(row["Type"]);
        }
    }
}
