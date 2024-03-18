using System;

namespace north.Fields.Base
{
     /// <summary>
     /// Base class for OrderDetailsFields.  Do not make changes to this class,
     /// instead, put additional code in the OrderDetailsFields class. 
     /// Note: This class is identical to the OrderDetailsModelBase without/minus the data annotations.
     /// </summary>
     public class OrderDetailsFieldsBase
     {
         /// <summary> 
         /// Gets or Sets OrderID 
         /// </summary> 
         public int OrderID { get; set; } 

         /// <summary> 
         /// Gets or Sets ProductID 
         /// </summary> 
         public int ProductID { get; set; } 

         /// <summary> 
         /// Gets or Sets UnitPrice 
         /// </summary> 
         public decimal UnitPrice { get; set; } 

         /// <summary> 
         /// Gets or Sets UnitPriceTotal
         /// </summary> 
         public decimal UnitPriceTotal { get; set; } 

         /// <summary> 
         /// Gets or Sets Quantity 
         /// </summary> 
         public Int16 Quantity { get; set; } 

         /// <summary> 
         /// Gets or Sets Discount 
         /// </summary> 
         public Single Discount { get; set; } 

     }
}
