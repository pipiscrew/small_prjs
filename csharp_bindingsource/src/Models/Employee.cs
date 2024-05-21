using System;

namespace Models
{
    public class Employee
    {
        public Int64 employeeid { get; set; }
        public string lastname { get; set; }
        public string firstname { get; set; }
        public string title { get; set; }
        public string titleofcourtesy { get; set; }
        public DateTime? birthdate { get; set; }
        public DateTime? hiredate { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string region { get; set; }
        public string postalcode { get; set; }
        public string country { get; set; }
        public string homephone { get; set; }
        public string extension { get; set; }
        public Byte[] photo { get; set; }
        public string notes { get; set; }
        public Int64? reportsto { get; set; }
        public string photopath { get; set; }
    }
}
