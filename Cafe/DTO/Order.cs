using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.DTO
{
    public class Order
    {
        private string name;
        private int number;
        private float price;
        private float totalPrice;

        public string Name { get => name; set => name = value; }
        public int Number { get => number; set => number = value; }
        public float Price { get => price; set => price = value; }
        public float TotalPrice { get => totalPrice; set => totalPrice = value; }
        public Order(string name, int number, float price, float totalPrice)
        {
            this.Name = name;
            this.Number = number;
            this.Price = price;
            this.TotalPrice = totalPrice;
        }
        public Order(DataRow row)
        {
            this.Name = row["Name"].ToString();
            this.Number = Convert.ToInt32(row["Number"]);
            this.Price = (float)Convert.ToDouble(row["Price"]);
            this.TotalPrice = (float)Convert.ToDouble(row["TotalPrice"]);
        }
    }
}
