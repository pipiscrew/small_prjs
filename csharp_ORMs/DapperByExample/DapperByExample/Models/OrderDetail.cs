
namespace DapperByExample.Models
{
    public class OrderDetail
    {
        //the classes inside Models namespace made as separate sample for splitOn, in normal application we have 1 class per Entity.
        public long Odetail_id { get; set; }
        public long Order_id { get; set; }
        public long Product_id { get; set; }
        public decimal Price { get; set; }
        public decimal Tax { get; set; }
        public long Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal Price_b4_tax { get; set; }
        public decimal Final_price { get; set; }
    }
}
