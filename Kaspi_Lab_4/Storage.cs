using System;
using System.Collections.Generic;
using System.Linq;

namespace Kaspi_Lab_4
{
    public delegate void StorageHandler(object sender, StorageEventArgs e);

    public class Storage
    {
        public event StorageHandler ProductAdding;
        public event StorageHandler WrongProductAdding;

        public string StorageName { get; set; }
        public Address StorageAddress { get; set; }
        public double Area { get; set; }
        public Employee ResponsiblePerson { get; set; }
        Dictionary<Product, int> Products = new Dictionary<Product, int>();
        public bool isClosedType{ get; set; }

        public Storage()
        {

        }
        public bool AddToStorage(Product someProd, int Quantity)
        {
            StorageEventArgs evt;
            if (someProd.isLooseType == true && this.isClosedType == false)
            {
                evt = new StorageEventArgs($"На склад {this.StorageName} попытались загрузить товар неподходящего типа {someProd.Name} {DateTime.Now}", this.StorageName, someProd, Quantity, DateTime.Now, StorageEventArgs.EventType.AddingWrongProduct);
                ProductAdding?.Invoke(this, evt);   
                return false;
            }
            else if (Products.ContainsKey(someProd))
            {
                Products[someProd] = Products[someProd] + Quantity;
                evt = new StorageEventArgs($"На склад {this.StorageName} поступил товар {someProd.Name} {DateTime.Now}", this.StorageName, someProd, Quantity, DateTime.Now, StorageEventArgs.EventType.AddingProduct);
                ProductAdding?.Invoke(this,evt );   
                return true;
            }
            else
            {
                Products.Add(someProd, Quantity);
                evt = new StorageEventArgs($"На склад {this.StorageName} поступил товар {someProd.Name} {DateTime.Now}", this.StorageName, someProd, Quantity, DateTime.Now, StorageEventArgs.EventType.AddingProduct);
                ProductAdding?.Invoke(this, evt);   
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
                int qty = Products[prod];
                networth += prod.UnitPrice * qty;
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
