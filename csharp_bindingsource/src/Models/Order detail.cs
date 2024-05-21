using System;

namespace Models
{
    public class Orderdetail
    {
        public Int64 orderid { get; set; }
        public Int64 productid { get; set; }
        public decimal unitprice { get; set; }
        public Int64 quantity { get; set; }
        public Double discount { get; set; }
    }
}
