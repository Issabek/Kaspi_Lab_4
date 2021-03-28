using System;
using System.Collections.Generic;
namespace Kaspi_Lab_4
{
    public class Singleton
    {
        private Singleton()
        {
            Catalog = new Dictionary<string, Product>();
        }

        private static Singleton _instance = null;
        public static Singleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Singleton();
                }
                return _instance;
            }
        }
        public Dictionary<string, Product> Catalog;        
    }
}
