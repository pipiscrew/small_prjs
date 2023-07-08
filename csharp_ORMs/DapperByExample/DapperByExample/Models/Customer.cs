using System.Collections.Generic;

namespace DapperByExample.Models
{
    public class Customer
    {
        //the classes inside Models namespace made as separate sample for splitOn, in normal application we have 1 class per Entity.
        public long Cust_id { get; set; }
        public string Cust_name { get; set; }
        public string Cust_addess { get; set; }

        public List<Order> Orders { get; set; }
    }

}
