using System;
using System.Collections.Generic;

namespace Kaspi_Lab_4
{

    interface ICommand
    {
        void Execute();
        void Undo();
    }

    class StorageCommand : ICommand
    {
        public event StorageHandler AddCommandEvent;
        public event StorageHandler WrongProductAdding;
        private Storage _receiver;
        private Product prod;
        private int Quantity;

        public StorageCommand(Storage receiver, Product p, int quantity)
        {
            this._receiver = receiver;
            this.prod = p;
            this.Quantity = quantity;
        }

        public void Execute()
        {
            if (this._receiver.AddToStorage(this.prod, this.Quantity))
                AddCommandEvent?.Invoke(this, new StorageEventArgs($"На склад {this._receiver} поступил товар {this.prod.Name} {DateTime.Now}", this._receiver.StorageName, this.prod, Quantity, DateTime.Now, StorageEventArgs.EventType.AddingProduct));
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                WrongProductAdding?.Invoke(this, new StorageEventArgs(string.Format("На склад {0} попытались загрузить товар неподходящего типа {1} {2}", this._receiver.StorageName, this.prod.Name, DateTime.Now), _receiver.StorageName, prod, Quantity, DateTime.Now, StorageEventArgs.EventType.AddingWrongProduct));
                Console.ResetColor();
            }
        }
        public void Undo()
        {

        }
    }
}
