

namespace NHibernateByCodeSample.Domain {
    
    public class Products {
        public virtual int ProductId { get; set; }
        public virtual string ProdName { get; set; }
        public virtual string ProdCode { get; set; }
        public virtual double? ProdPrice { get; set; }
        public virtual double? ProdTax { get; set; }
    }
}
