using System;
using System.Collections.Generic;
using System.Linq;

namespace Kaspi_Lab_4
{
    public class Storage
    {
        public string Address { get; set; }
        public double Area { get; set; }
        public Employee ResponsiblePerson { get; set; }
        Dictionary<Product, int> Products = new Dictionary<Product, int>();
        public bool isCoveredStorage{ get; set; }
        public Storage()
        {
            
        }
        public bool AddToStorage(Product someProd, int Quantity)
        {
            if (someProd.isLooseType == true && this.isCoveredStorage==false)
            {
                return false;
            }
            else if (Products.ContainsKey(someProd))
            {
                Products[someProd] = Products[someProd] + Quantity;
                return true;
            }
            else
            {

                    Products.Add(someProd, Quantity);
                    return true;
                
            }
        }

        public bool MoveProductToStorage(Storage AnotherStorage, Product someProd, int Quantity)
        {
            if (Products.ContainsKey(someProd))
            {

                bool tempbool = AnotherStorage.AddToStorage(someProd, Quantity);
                if (Products[someProd] > Quantity)
                {
                    Products[someProd] -= Quantity;
                }
                else if (Products[someProd] == Quantity)
                    Products.Remove(someProd);
                else
                    throw new Exception(string.Format("Not enough of a quantity of {0} product", someProd.Name));
                return tempbool;
            }
            else
            {
                throw new Exception("There is no such an item");
            }
        }

        public Product SearchProductBySKU(string SKU)
        {
            List<Product> tempList = Products.Keys.ToList();
            Product tempPr = tempList.Where(x => x.SKU == SKU).FirstOrDefault();
            return tempPr;
        }

        public decimal TotalWorth()
        {
            List<Product> tempList = Products.Keys.ToList();
            decimal networth = 0;
            foreach(Product prod in tempList)
            {
                networth += prod.UnitPrice * Products[prod];
            }
            return networth;
        }

        public bool setStorageManager(Employee emp)
        {
            if (emp.Position == Position.IntermdeiateEmp || emp.Position == Position.AdvancedEmp)
                this.ResponsiblePerson = emp;
            else
                return false;
            return true;
        }

    }
}
