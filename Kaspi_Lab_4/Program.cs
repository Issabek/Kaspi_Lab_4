using System;
using System.Collections.Generic;

namespace Kaspi_Lab_4
{
    class Program
    {
        static void Main(string[] args)
        {
            #region props
            List<Storage> myStors = new List<Storage>
            {
                new Storage(),
                new Storage()
            };
            SomeTempInvoker myInvoker = new SomeTempInvoker();
            Random rnd = new Random();

            #endregion

            #region Продукты
            Singleton.Instance.Catalog.Add("001", new Bulk { 
                Description = "desc1",
                isLooseType = true,
                Name = "Pesok",
                SKU = "11F2D",
                UNIT = "KG",
                UnitPrice = 50
            });
            Singleton.Instance.Catalog.Add("002", new Liquid { 
                Description = "desc2",
                isLooseType = false,
                Name = "Voda",
                SKU = "12F2D",
                UNIT = "L",
                UnitPrice = 150
            });
            Singleton.Instance.Catalog.Add("003", new Heavy
            {
                Description = "desc2",
                isLooseType = false,
                Name = "Zhelezo",
                SKU = "21F2D",
                UNIT = "KG",
                UnitPrice = 450
            });

            Singleton.Instance.Catalog.Add("004", new Piecemeal
            {
                Description = "desc2",
                isLooseType = false,
                Name = "Banany",
                SKU = "01F2D",
                UNIT = "Piece",
                UnitPrice = 90
            });

            Singleton.Instance.Catalog.Add("005", new Heavy
            {
                Description = "desc2",
                isLooseType = false,
                Name = "Divany",
                SKU = "01C3H",
                UNIT = "Piece",
                UnitPrice = 160
            });

            Singleton.Instance.Catalog.Add("006", new Piecemeal
            {
                Description = "desc2",
                isLooseType = false,
                Name = "Cola",
                SKU = "01C4H",
                UNIT = "Piece",
                UnitPrice = 40
            });
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
                foreach (var pair in Singleton.Instance.Catalog)
                {
                    myInvoker.SetCommand(new StorageCommand(myStors[i], pair.Value, rnd.Next(4000)));
                    myInvoker.Run();
                }
            }
            myStors[1].MoveHalf(myStors[0]);//nothing happens because each storage has same products in it

            List<Product> temp = myStors[0].GetAllProducts(myStors[1]); //Gets all existing products from two storages

            Dictionary<Product, double> tempDict;
            Report.MeanProdsQuantity(myStors, out tempDict);//gets mean quantity value of all products among all storages

            Storage stor = myStors[0];
            Report.SerializeToCSV(ref stor, @"text.csv"); //saves specific storage products information in CSV file
        }
    }


    class SomeTempInvoker
    {
        private StorageCommand tempCommand;

        Stack<ICommand> CommandsHistory = new Stack<ICommand>();
        public void SetCommand(ICommand command)
        {
            this.tempCommand = command as StorageCommand;
        }
        public void Run()
        {
            CommandsHistory.Push(tempCommand);
            tempCommand.AddCommandEvent += DisplayMessage;
            tempCommand.WrongProductAdding += DisplayMessage;
            tempCommand.Execute();
        }
        private static void DisplayMessage(object sender, StorageEventArgs ev)
        {

            Console.WriteLine(ev.Message);
        }
    }
}
