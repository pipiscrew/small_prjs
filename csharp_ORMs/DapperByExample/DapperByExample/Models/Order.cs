using System.Collections.Generic;

namespace DapperByExample.Models
{
    public class Order
    {
        //the classes inside Models namespace made as separate sample for splitOn, in normal application we have 1 class per Entity.
        public long Order_id { get; set; }
        public long Cust_id { get; set; } 
        public string Order_no { get; set; }
        public string Order_date { get; set; }  
        public string Comment { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }

}
