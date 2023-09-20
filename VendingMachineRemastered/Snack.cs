using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachineRemastered
{
    internal class Snack
    {
        public string Name { get; private set; }
        public double Price { get; private set; }

        public int Stock { get; private set; }  

        public bool inStock { get {return Stock > 0;}}

        public Snack(string name, double price, int stock)
        {
            Name = name;
            Price = price;
            Stock = stock;
        }

        public void ReduceStock()
        {
            if (Stock > 0)
            {
                Stock--;
            }
        }

        public void Edit(double newPrice, int newStock)
        {
            Price = newPrice;
            Stock = newStock;
        }
    }
}
