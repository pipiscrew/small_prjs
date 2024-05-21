using System;

namespace Models
{
    public class Order
    {
        public Int64 orderid { get; set; }
        public string customerid { get; set; }
        public Int64? employeeid { get; set; }
        public DateTime? orderdate { get; set; }
        public DateTime? requireddate { get; set; }
        public DateTime? shippeddate { get; set; }
        public Int64? shipvia { get; set; }
        public decimal? freight { get; set; }
        public string shipname { get; set; }
        public string shipaddress { get; set; }
        public string shipcity { get; set; }
        public string shipregion { get; set; }
        public string shippostalcode { get; set; }
        public string shipcountry { get; set; }

        //public override string ToString()
        //{
        //    return new string[] { "takis" };
        //}
        //public override string[] ToString()
        //{
        //    return new string[] { orderid, customerid.ToString() };
        //}
        //public override string ToString()
        //{
        //    return "takis";
        //}
    }
}
