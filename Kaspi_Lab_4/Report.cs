using System;
using System.Linq;
using System.Collections.Generic;

namespace Kaspi_Lab_4
{
    public class Report
    {
        /// <summary>
        /// This static method gets you a list with products which quantity is less than 3
        /// </summary>
        /// <param name="storage">Takes storage from which you get report</param>
        /// <param name="YourDictionary">Stores the result in dictionay</param>
        /// <returns>true if success/false if fails</returns>
        public static bool GetLessThanThree(Storage storage, out Dictionary<Product, int> YourDictionary)
        {
            try
            {
                YourDictionary = (from entry in storage.Products where entry.Value < 3 orderby entry.Value ascending select entry)
                    .ToDictionary(pair => pair.Key, pair => pair.Value);
                return true;
            }
            catch(Exception ex)
            { 
                Console.WriteLine(ex.Message);
                YourDictionary=null;
                return false;
            }
        }
        /// <summary>
        /// This static method gets you a list with distinct produc names
        /// </summary>
        /// <param name="storage">Takes storage from which you get report</param>
        /// <param name="NamesList">Stores the result in dictionay</param>
        /// <returns>true if success/false if fails</returns>
        public static bool GetDistinctNames(Storage storage, out List<string> NamesList)
        {
            try
            {
                NamesList = (from entry in storage.Products.Keys.ToList() orderby entry.Name ascending select entry.Name).Distinct().ToList();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                NamesList = null;
                return false;
            }
        }
        /// <summary>
        /// This static method gets you a list with distinct produc names
        /// </summary>
        /// <param name="storage">Takes storage from which you get report</param>
        /// <param name="NamesList">Stores the result in dictionay</param>
        /// <returns>true if success/false if fails</returns>
        public static bool GetTopThree(Storage storage, out Dictionary<Product,int> TopThreeByQuantity)
        {
            try
            {
                TopThreeByQuantity = storage.Products.OrderBy(prod => prod.Value).Take(3).ToDictionary(pair=>pair.Key,pair=>pair.Value);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                TopThreeByQuantity = null;
                return false;
            }
        }
        /// <summary>
        /// This static method gets you a list with storages that has no loosy products(such as sand)
        /// </summary>
        /// <param name="storages">list of storages</param>
        /// <param name="result">empty list of storages where your result is being stored</param>
        /// <returns> true/false</returns>
        public static bool HasNoLooseType(List<Storage> storages, out List<Storage> result)
        {
            try
            {
                result = new List<Storage>();
                bool res = false;
                foreach (Storage storage in storages)
                {
                    if (!storage.Products.Keys.Any(pair => pair.isLooseType == true))
                    {
                        result.Add(storage);
                        res = true;
                    }
                }
                return res;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = null;
                return false;
            }
        }
        /// <summary>
        /// Gets you Storage with mean value of products quantity among all storages
        /// </summary>
        /// <param name="storages">list of storages</param>
        /// <param name="result">empty dictionary where your result is being stored</param>
        /// <returns>true/false</returns>
        public static bool MeanProdsQuantity(List<Storage> storages, out Dictionary<Product,double> result)
        {
            try
            {
                
                List<Dictionary<Product, int>> temp = new List<Dictionary<Product, int>>();
                foreach (Storage storage in storages)
                    temp.Add(storage.Products);
                result = temp.SelectMany(dict => dict).GroupBy(d => d.Key).ToDictionary(k => k.Key, v => v.Average(d=>d.Value));
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = null;
                return false;
            }
        }
    }
}
