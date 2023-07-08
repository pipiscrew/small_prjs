using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernateByCodeSample.Domain;


namespace NHibernateByCodeSample.Maps {
    
    
    public class ProductsMap : ClassMapping<Products> {
        
        public ProductsMap() {
            Table("products");
			Lazy(true);
			Id(x => x.ProductId, map => { map.Column("product_id"); map.Generator(Generators.Assigned); });
			Property(x => x.ProdName, map => map.Column("prod_name"));
			Property(x => x.ProdCode, map => map.Column("prod_code"));
			Property(x => x.ProdPrice, map => map.Column("prod_price"));
			Property(x => x.ProdTax, map => map.Column("prod_tax"));
        }
    }
}
