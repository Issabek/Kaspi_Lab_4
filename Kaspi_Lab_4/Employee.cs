using System;
namespace Kaspi_Lab_4
{
    public enum Position
    {
        BeginnerEmp,
        IntermdeiateEmp,
        AdvancedEmp
    }
    public class Employee
    {
        public string Fullname { get; set; }
        public Position Position { get; set; }
     
        public Employee(): this(null, Position.BeginnerEmp) { }
   
        public Employee(string FIO, Position pos)
        {
            Fullname = FIO;
            Position = pos;
        }
        public Employee(string FIO) : this(FIO, Position.BeginnerEmp) { }
    }
}
