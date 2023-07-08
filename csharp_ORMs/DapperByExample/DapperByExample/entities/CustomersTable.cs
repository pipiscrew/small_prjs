using System;

//using System.Dynamic;

namespace Model
{
    /// <summary>
    /// generator ref - https://github.com/znyet/DapperExtensions
    /// </summary>
    
    public class CustomersTable
    {

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
        public string Cust_name { get; set; }

        /// <summary>
        /// Descript: 
        /// DbType: TEXT
        /// AllowNull: 0
        /// Defaultval: 
        /// </summary>
        public string Cust_addess { get; set; }

        //[Igore]
        //public dynamic OtherData = new ExpandoObject();

    }

}