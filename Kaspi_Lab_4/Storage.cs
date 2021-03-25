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
        public Dictionary<Product, int> Products = new Dictionary<Product, int>();
        public bool isClosedType{ get; set; }

        public Storage()
        {

        }
        public bool AddToStorage(Product someProd, int Quantity)
        {
            if (someProd.isLooseType == true && this.isClosedType == false)
            {
                ProductAdding?.Invoke(this, new StorageEventArgs($"На склад {this.StorageName} попытались загрузить товар неподходящего типа {someProd.Name} {DateTime.Now}", this.StorageName, someProd, Quantity, DateTime.Now, StorageEventArgs.EventType.AddingWrongProduct));   
                return false;
            }
            else if (Products.ContainsKey(someProd))
            {
                Products[someProd] = Products[someProd] + Quantity;
                ProductAdding?.Invoke(this, new StorageEventArgs($"На склад {this.StorageName} поступил товар {someProd.Name} {DateTime.Now}", this.StorageName, someProd, Quantity, DateTime.Now, StorageEventArgs.EventType.AddingProduct) );   
                return true;
            }
            else
            {
                Products.Add(someProd, Quantity);
                ProductAdding?.Invoke(this, new StorageEventArgs($"На склад {this.StorageName} поступил товар {someProd.Name} {DateTime.Now}", this.StorageName, someProd, Quantity, DateTime.Now, StorageEventArgs.EventType.AddingProduct));   
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

    public static class StorageExtension
    {
        public static List<Product> GetAllProducts(this Storage store, Storage store2)
        {
            List<Product> StoreProd1 = store.Products.Keys.ToList();
            List<Product> StoreProd2 = store2.Products.Keys.ToList();
            return StoreProd1.Union(StoreProd2).ToList();
        }

        /// <summary>
        /// Moves half of all products from one storage if they do not exist in another
        /// </summary>
        public static bool MoveHalf(this Storage store2, Storage store)
        {
            try
            {
                foreach (KeyValuePair<Product, int> pair in store.Products)
                {
                    if (!store2.Products.ContainsKey(pair.Key) && pair.Value > 1)
                    {
                        store2.AddToStorage(pair.Key, pair.Value / 2);
                        store.Products[pair.Key] -= pair.Value / 2;
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            
        }
    }
}
