using System;

//using System.Dynamic;

namespace Model
{
    /// <summary>
    /// 
    /// </summary>
    
    public class OrdersTable
    {

        /// <summary>
        /// Descript: 
        /// DbType: INTEGER
        /// AllowNull: 0
        /// Defaultval: 
        /// </summary>
        public long Order_id { get; set; }

        /// <summary>
        /// Descript: 
        /// DbType: INTEGER
        /// AllowNull: 0
        /// Defaultval: 
        /// </summary>
        public long Cust_id { get; set; }

        /// <summary>
        /// Descript: 
        /// DbType: TEXT
        /// AllowNull: 0
        /// Defaultval: 
        /// </summary>
        public string Order_no { get; set; }

        /// <summary>
        /// Descript: 
        /// DbType: TEXT
        /// AllowNull: 0
        /// Defaultval: 
        /// </summary>
        public string Order_date { get; set; }

        /// <summary>
        /// Descript: 
        /// DbType: TEXT
        /// AllowNull: 0
        /// Defaultval: 
        /// </summary>
        public string Comment { get; set; }

        //[Igore]
        //public dynamic OtherData = new ExpandoObject();

    }

}