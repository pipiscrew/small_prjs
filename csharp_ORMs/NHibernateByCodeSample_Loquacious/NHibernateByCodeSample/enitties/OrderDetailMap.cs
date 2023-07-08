using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernateByCodeSample.Domain;


namespace NHibernateByCodeSample.Maps {
    
    
    public class OrderDetailMap : ClassMapping<OrderDetail> {
        
        public OrderDetailMap() {
			Table("order_detail");
			Lazy(true);
            Id(x => x.OdetailId, map => { map.Column("odetail_id"); map.Generator(Generators.Identity); });
			Property(x => x.Price);
			Property(x => x.Tax);
			Property(x => x.Quantity);
			Property(x => x.Discount);
			Property(x => x.PriceB4Tax, map => map.Column("price_b4_tax"));
			Property(x => x.FinalPrice, map => map.Column("final_price"));
			ManyToOne(x => x.Products, map => 
			{
				map.Column("product_id");
				map.Cascade(Cascade.None);
			});

			ManyToOne(x => x.Orders, map => 
			{
				map.Column("order_id");
				map.Cascade(Cascade.None);
			});

        }
    }
}
