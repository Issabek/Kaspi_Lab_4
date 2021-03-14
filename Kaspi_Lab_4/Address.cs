using System;
namespace Kaspi_Lab_4
{
    public class Address
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string BuildingNumber { get; set; }
        public Address(string country,string city, string buildingNumber)
        {
            this.Country = country;
            this.City = city;
            this.BuildingNumber = buildingNumber;
        }
        public override string ToString()
        {
            return Country + ", " + City + ", " + BuildingNumber;
        }
    }
}
