using System;
namespace Kaspi_Lab_4
{
    public abstract class Product  {
        public string Name { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public string UNIT { get; set; }
        public bool isLooseType { get; set; }
    }

    public class Liquid : Product {
        public Liquid() { }
        public Liquid(string Name, string SKU, string Description,decimal UnitPrice, string UNIT, bool isLooseType)
        {
            this.Name = Name;
            this.SKU = SKU;
            this.Description = Description;
            this.UNIT = UNIT;
            this.UnitPrice = UnitPrice;
            this.isLooseType = isLooseType;
        }
        public Liquid(Product obj)
        {
                this.Name = obj.Name;
                this.SKU = obj.SKU;
                this.Description = obj.Description;
                this.UNIT = obj.UNIT;
                this.UnitPrice = obj.UnitPrice;
                this.isLooseType = obj.isLooseType;
        }
    }
    public class Bulk : Product {
        public Bulk() { }
        public Bulk(string Name, string SKU, string Description, decimal UnitPrice, string UNIT)
        {
            this.Name = Name;
            this.SKU = SKU;
            this.Description = Description;
            this.UNIT = UNIT;
            this.UnitPrice = UnitPrice;
            this.isLooseType = true;
        }
        public Bulk(Product obj)
        {
            if (obj.isLooseType)
            {
                this.Name = obj.Name;
                this.SKU = obj.SKU;
                this.Description = obj.Description;
                this.UNIT = obj.UNIT;
                this.UnitPrice = obj.UnitPrice;
                this.isLooseType = obj.isLooseType;
            }
            else
            {
                throw new Exception(string.Format("Object {0} cannot be copied into Bulk type of product", obj.GetType().Name));
            }
        }
    }
    public class Piecemeal : Product {
        public Piecemeal() { }
        public Piecemeal(string Name, string SKU, string Description, decimal UnitPrice, string UNIT, bool isLooseType)
        {
            this.Name = Name;
            this.SKU = SKU;
            this.Description = Description;
            this.UNIT = UNIT;
            this.UnitPrice = UnitPrice;
            this.isLooseType = isLooseType;
        }
        public Piecemeal(Product obj)
        {
            this.Name = obj.Name;
            this.SKU = obj.SKU;
            this.Description = obj.Description;
            this.UNIT = obj.UNIT;
            this.UnitPrice = obj.UnitPrice;
            this.isLooseType = obj.isLooseType;
        }
    }
    public class Heavy : Product {
        public Heavy() { }
        public Heavy(string Name, string SKU, string Description, decimal UnitPrice, string UNIT, bool isLooseType)
        {
            this.Name = Name;
            this.SKU = SKU;
            this.Description = Description;
            this.UNIT = UNIT;
            this.UnitPrice = UnitPrice;
            this.isLooseType = isLooseType;
        }
        public Heavy(Product obj)
        {
            this.Name = obj.Name;
            this.SKU = obj.SKU;
            this.Description = obj.Description;
            this.UNIT = obj.UNIT;
            this.UnitPrice = obj.UnitPrice;
            this.isLooseType = obj.isLooseType;
        }
    }

    public static class ProductExtension
    {
        public static string GetProductName(this Product prod)
        {
            return string.Format("{0} - {1}", prod.SKU, prod.Name);
        }
    }
}
