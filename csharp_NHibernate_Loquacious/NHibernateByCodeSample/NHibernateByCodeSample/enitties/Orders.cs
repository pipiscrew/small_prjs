using System.Collections.Generic;


namespace NHibernateByCodeSample.Domain {
    
    public class Orders {
        public virtual int OrderId { get; set; }
        public virtual Customers Customers { get; set; }
        public virtual string OrderNo { get; set; }
        public virtual string OrderDate { get; set; }
        public virtual string Comment { get; set; }
        public virtual IList<OrderDetail> OrderDetail { get; set; }

    }
}
