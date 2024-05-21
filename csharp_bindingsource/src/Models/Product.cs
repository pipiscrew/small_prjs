using System;

namespace Models
{
    public class Product
    {
        public Int64 productid { get; set; }
        public string productname { get; set; }
        public Int64? supplierid { get; set; }
        public Int64? categoryid { get; set; }
        public string quantityperunit { get; set; }
        public decimal? unitprice { get; set; }
        public Int64? unitsinstock { get; set; }
        public Int64? unitsonorder { get; set; }
        public Int64? reorderlevel { get; set; }
        public string discontinued { get; set; }
    }
}
