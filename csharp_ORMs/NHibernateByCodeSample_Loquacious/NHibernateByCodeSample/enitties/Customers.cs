using System.Collections.Generic;


namespace NHibernateByCodeSample.Domain
{

    public class Customers
    {
        public virtual int CustId { get; set; }
        public virtual string CustName { get; set; }
        public virtual string CustAddess { get; set; }
        public virtual IList<Orders> Orders { get; set; }

        //public Customers()
        //{
        //    Orders = new List<Orders>();
        //}

    }
}
