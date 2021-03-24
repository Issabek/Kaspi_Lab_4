using System;
using System.Collections.Generic;

namespace Kaspi_Lab_4
{
    class Program
    {
        static void Main(string[] args)
        {
            #region props
            List<Storage> myStors = new List<Storage>(2);
            List<Product> myProds = new List<Product>(4);
            myStors.Add(new Storage());
            myStors.Add(new Storage());
            Random rnd = new Random();

            Bulk tempProduct = new Bulk();
            Liquid tempProduct2 = new Liquid();
            Heavy tempProduct3 = new Heavy();
            Piecemeal tempProduct4 = new Piecemeal();

            decimal tempDecimal = 0;
            #endregion

            #region Продукты
            tempProduct.Description = "desc1";
            tempProduct.isLooseType = true;
            tempProduct.Name = "Pesok";
            tempProduct.SKU = "11F2D";
            tempProduct.UNIT = "KG";
            tempProduct.UnitPrice = 50;

            tempProduct2.Description = "desc2";
            tempProduct2.isLooseType = false; ;
            tempProduct2.Name = "Voda";
            tempProduct2.SKU = "12F2D";
            tempProduct2.UNIT = "L";
            tempProduct2.UnitPrice = 150;

            tempProduct3.Description = "desc2";
            tempProduct3.isLooseType = false;
            tempProduct3.Name = "Zhelezo";
            tempProduct3.SKU = "21F2D";
            tempProduct3.UNIT = "KG";
            tempProduct3.UnitPrice = 450;

            tempProduct4.Description = "desc2";
            tempProduct4.isLooseType = false;
            tempProduct4.Name = "Banany";
            tempProduct4.SKU = "01F2D";
            tempProduct4.UNIT = "Piece";
            tempProduct4.UnitPrice = 90;

            myProds.Add(tempProduct);
            myProds.Add(tempProduct2);
            myProds.Add(tempProduct3);
            myProds.Add(tempProduct4);

            #endregion

            #region stores
            myStors[0].StorageAddress = new Address("ALmaty","Kablukova","5/2");
            myStors[0].Area = 1200;
            myStors[0].isClosedType = true;
            myStors[0].ResponsiblePerson = new Employee("Omarov Issabek", Position.AdvancedEmp);
            myStors[0].StorageName = "MagnumStorage1";

            myStors[1].StorageAddress = new Address("ALmaty","Momyshuly","122");
            myStors[1].Area = 800;
            myStors[1].isClosedType = false;
            myStors[1].ResponsiblePerson = new Employee("Denis", Position.IntermdeiateEmp);
            myStors[1].StorageName = "MagnumStorage2";
            #endregion
            Console.WriteLine("=================================");
            for (int i = 0; i < myStors.Count; i++) {
                foreach (Product prod in myProds)
                {
                    myStors[i].ProductAdding -= DisplayMessage;
                    myStors[i].ProductAdding -= DisplayRedMessage;
                    if (prod.isLooseType && !myStors[i].isClosedType)
                    {
                        myStors[i].ProductAdding += DisplayRedMessage;
                        myStors[i].AddToStorage(prod, rnd.Next(4000));
                    }
                    else
                    {
                        myStors[i].ProductAdding += DisplayMessage;
                        myStors[i].AddToStorage(prod, rnd.Next(4000));
                    }
                }
            }
            Console.WriteLine("\n");
            //myStors[0].AddToStorage(tempProduct, 100);
            //myStors[0].AddToStorage(tempProduct2, 2000);
            //myStors[0].AddToStorage(tempProduct3, 4000);
            //myStors[0].AddToStorage(tempProduct4, 900);
            //myStors[1].AddToStorage(tempProduct2, 2000);
            //myStors[1].AddToStorage(tempProduct3, 4000);
            //myStors[1].AddToStorage(tempProduct4, 900);
            //myStors[1].AddToStorage(tempProduct, 100);
            


            myStors[0].MoveProductToStorage(myStors[1], myStors[0].SearchProductBySKU("21F2D"), 500); // Поиск по SKU, перемещение товара из одного склада в другой
            foreach(Storage store in myStors)
            {
                decimal worthOfStorage = store.TotalWorth();
                Console.WriteLine("Total worth of storage at {0} is {1}", store.StorageAddress.ToString(), worthOfStorage); ;
                tempDecimal += worthOfStorage;

            }
            Console.WriteLine(tempDecimal);
            Console.WriteLine("Done");
        }
        private static void DisplayMessage(object sender, StorageEventArgs ev)
        {
            Console.WriteLine(ev.Message);
        }

        private static void DisplayRedMessage(object sender, StorageEventArgs ev)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ev.Message);
            Console.ResetColor();
        }
    }
}
