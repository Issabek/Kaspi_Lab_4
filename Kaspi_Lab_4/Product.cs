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

    public class Liquid : Product { }
    public class Bulk : Product { }
    public class Piecemeal : Product { }
    public class Heavy : Product { }

    public static class ProductExtension
    {
        public static string GetProductName(this Product prod)
        {
            return string.Format("{0} - {1}", prod.SKU, prod.Name);
        }
    }
}
