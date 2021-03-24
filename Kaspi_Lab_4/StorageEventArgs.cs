using System;
namespace Kaspi_Lab_4
{
    public class StorageEventArgs
    {
        public enum EventType
        {
            AddingProduct,
            AddingWrongProduct
        }
        public string StorageName { get; set; }
        public Product Product { get; set; }
        public int  Quantity { get; set; }
        public DateTime EventDate { get; set; }
        public EventType Event { get; set; }
        public string Message { get; set; }

        public StorageEventArgs(string Message ,string StorageName, Product product,int quantity, DateTime EventDate,EventType eventType)
        {
            this.Message = Message;
            this.StorageName = StorageName;
            this.Product = product;
            this.Quantity = quantity;
            this.EventDate = EventDate;
            this.Event = eventType;
        }
    }
}
