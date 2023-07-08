using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.DTO
{
    public class Menu
    {
        private int iD;
        private string name;
        private float price;

        public int ID { get => iD; set => iD = value; }
        public string Name { get => name; set => name = value; }
        public float Price { get => price; set => price = value; }
        public Menu(int iD, string name, float price)
        {
            this.ID = iD;
            this.Name = name;
            this.Price = price;
        }
        public Menu(DataRow row)
        {
            this.ID = Convert.ToInt32(row["ID"]);
            this.Name = row["Name"].ToString();
            this.Price = (float)Convert.ToDouble(row["Price"]);
        }
    }
}
