using System;

namespace north.Fields.Base
{
     /// <summary>
     /// Base class for ProductsFields.  Do not make changes to this class,
     /// instead, put additional code in the ProductsFields class. 
     /// Note: This class is identical to the ProductsModelBase without/minus the data annotations.
     /// </summary>
     public class ProductsFieldsBase
     {
         /// <summary> 
         /// Gets or Sets ProductID 
         /// </summary> 
         public int ProductID { get; set; } 

         /// <summary> 
         /// Gets or Sets ProductName 
         /// </summary> 
         public string ProductName { get; set; } 

         /// <summary> 
         /// Gets or Sets SupplierID 
         /// </summary> 
         public int? SupplierID { get; set; } 

         /// <summary> 
         /// Gets or Sets CategoryID 
         /// </summary> 
         public int? CategoryID { get; set; } 

         /// <summary> 
         /// Gets or Sets QuantityPerUnit 
         /// </summary> 
         public string QuantityPerUnit { get; set; } 

         /// <summary> 
         /// Gets or Sets UnitPrice 
         /// </summary> 
         public decimal? UnitPrice { get; set; } 

         /// <summary> 
         /// Gets or Sets UnitPriceTotal
         /// </summary> 
         public decimal UnitPriceTotal { get; set; } 

         /// <summary> 
         /// Gets or Sets UnitsInStock 
         /// </summary> 
         public Int16? UnitsInStock { get; set; } 

         /// <summary> 
         /// Gets or Sets UnitsOnOrder 
         /// </summary> 
         public Int16? UnitsOnOrder { get; set; } 

         /// <summary> 
         /// Gets or Sets ReorderLevel 
         /// </summary> 
         public Int16? ReorderLevel { get; set; } 

         /// <summary> 
         /// Gets or Sets Discontinued 
         /// </summary> 
         public bool Discontinued { get; set; } 

     }
}
