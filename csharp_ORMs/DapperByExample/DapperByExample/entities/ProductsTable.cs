using System;

//using System.Dynamic;

namespace Model
{
    /// <summary>
    /// 
    /// </summary>
    
    public class ProductsTable
    {

        /// <summary>
        /// Descript: 
        /// DbType: INTEGER
        /// AllowNull: 0
        /// Defaultval: 
        /// </summary>
        public long Product_id { get; set; }

        /// <summary>
        /// Descript: 
        /// DbType: TEXT
        /// AllowNull: 0
        /// Defaultval: 
        /// </summary>
        public string Prod_name { get; set; }

        /// <summary>
        /// Descript: 
        /// DbType: TEXT
        /// AllowNull: 0
        /// Defaultval: 
        /// </summary>
        public string Prod_code { get; set; }

        /// <summary>
        /// Descript: 
        /// DbType: NUMERIC
        /// AllowNull: 0
        /// Defaultval: 
        /// </summary>
        public decimal Prod_price { get; set; }

        /// <summary>
        /// Descript: 
        /// DbType: NUMERIC
        /// AllowNull: 0
        /// Defaultval: 
        /// </summary>
        public decimal Prod_tax { get; set; }

        //[Igore]
        //public dynamic OtherData = new ExpandoObject();

    }

}