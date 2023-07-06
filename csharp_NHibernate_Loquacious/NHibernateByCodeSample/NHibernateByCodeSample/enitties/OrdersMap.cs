using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using NHibernateByCodeSample.Domain;
using Iesi.Collections.Generic;


namespace NHibernateByCodeSample.Maps {
    
    
    public class OrdersMap : ClassMapping<Orders> {
        
        public OrdersMap() {
            Table("orders");
			Lazy(true);
            Id(x => x.OrderId, map => { map.Column("order_id"); map.Generator(Generators.Identity); });
			Property(x => x.OrderNo, map => map.Column("order_no"));
			Property(x => x.OrderDate, map => map.Column("order_date"));
			Property(x => x.Comment);
			ManyToOne(x => x.Customers, map => 
			{
				map.Column("cust_id");
				map.Cascade(Cascade.None);
			});
            Bag(x => x.OrderDetail, m =>
            {
                m.Key(k => k.Column("order_id"));
                m.Cascade(Cascade.All | Cascade.DeleteOrphans);
                m.Inverse(true);
            }, r => r.OneToMany());


            /*
             * The Lazy function within the ClassMapping constructor is applicable for properties or components within the entity,
             * but it still does not apply to the associations specified by 
             * ManyToOne, OneToMany, OneToOne, and ManyToMany mappings. The Lazy function is used specifically for configuring association fetch strategies.
            */

        }
    }
}
