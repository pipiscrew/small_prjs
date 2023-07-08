

namespace NHibernateByCodeSample.Domain {
    
    public class OrderDetail {
        public virtual int OdetailId { get; set; }
        public virtual Orders Orders { get; set; }
        public virtual Products Products { get; set; }
        public virtual double? Price { get; set; }
        public virtual double? Tax { get; set; }
        public virtual int? Quantity { get; set; }
        public virtual double? Discount { get; set; }
        public virtual double? PriceB4Tax { get; set; }
        public virtual double? FinalPrice { get; set; }
    }
}
