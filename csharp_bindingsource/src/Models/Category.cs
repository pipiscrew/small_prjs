using System;

namespace Models
{
    public class Category
    {
        public Int64 categoryid { get; set; }
        public string categoryname { get; set; }
        public string description { get; set; }
        public Byte[] picture { get; set; }
    }
}
