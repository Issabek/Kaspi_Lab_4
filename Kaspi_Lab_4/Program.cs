using System;
using System.Collections.Generic;

namespace Kaspi_Lab_4
{
    class Program
    {
        static void Main(string[] args)
        {
            #region props
            List<Storage> myStors = new List<Storage>();
            List<Product> myProds = new List<Product>();
            myStors.Add(new Storage());
            myStors.Add(new Storage());
            Random rnd = new Random();

            Bulk tempProduct = new Bulk();
            Liquid tempProduct2 = new Liquid();
            Heavy tempProduct3 = new Heavy();
            Piecemeal tempProduct4 = new Piecemeal();
            Heavy tempProduct5 = new Heavy();
            Liquid tempProduct6 = new Liquid();
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

            tempProduct5.Description = "desc2";
            tempProduct5.isLooseType = false;
            tempProduct5.Name = "Divany";
            tempProduct5.SKU = "01C3H";
            tempProduct5.UNIT = "Piece";
            tempProduct5.UnitPrice = 160;

            tempProduct6.Description = "desc2";
            tempProduct6.isLooseType = false;
            tempProduct6.Name = "Cola";
            tempProduct6.SKU = "01C4H";
            tempProduct6.UNIT = "Piece";
            tempProduct6.UnitPrice = 40;

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
            for (int i = 0; i < myStors.Count; i++)
            {
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
            myStors[0].AddToStorage(tempProduct5, 2);
            myStors[0].AddToStorage(tempProduct6, 4);
            myStors[1].MoveHalf(myStors[0]);
            List<Product> temp = myStors[0].GetAllProducts(myStors[1]); //Gets all existing products from two storages

            Dictionary<Product, double> templist;
            Report.MeanProdsQuantity(myStors,out templist);
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
