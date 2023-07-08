using System;

//using System.Dynamic;

namespace Model
{
    /// <summary>
    /// 
    /// </summary>
    
    public class Order_detailTable
    {

        /// <summary>
        /// Descript: 
        /// DbType: INTEGER
        /// AllowNull: 0
        /// Defaultval: 
        /// </summary>
        public long Odetail_id { get; set; }

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
        public long Product_id { get; set; }

        /// <summary>
        /// Descript: 
        /// DbType: NUMERIC
        /// AllowNull: 0
        /// Defaultval: 
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Descript: 
        /// DbType: NUMERIC
        /// AllowNull: 0
        /// Defaultval: 
        /// </summary>
        public decimal Tax { get; set; }

        /// <summary>
        /// Descript: 
        /// DbType: INTEGER
        /// AllowNull: 0
        /// Defaultval: 
        /// </summary>
        public long Quantity { get; set; }

        /// <summary>
        /// Descript: 
        /// DbType: NUMERIC
        /// AllowNull: 0
        /// Defaultval: 
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Descript: 
        /// DbType: NUMERIC
        /// AllowNull: 0
        /// Defaultval: 
        /// </summary>
        public decimal Price_b4_tax { get; set; }

        /// <summary>
        /// Descript: 
        /// DbType: NUMERIC
        /// AllowNull: 0
        /// Defaultval: 
        /// </summary>
        public decimal Final_price { get; set; }

        //[Igore]
        //public dynamic OtherData = new ExpandoObject();

    }

}