using System;

namespace north.Fields.Base
{
     /// <summary>
     /// Base class for OrdersFields.  Do not make changes to this class,
     /// instead, put additional code in the OrdersFields class. 
     /// Note: This class is identical to the OrdersModelBase without/minus the data annotations.
     /// </summary>
     public class OrdersFieldsBase
     {
         /// <summary> 
         /// Gets or Sets OrderID 
         /// </summary> 
         public int OrderID { get; set; } 

         /// <summary> 
         /// Gets or Sets CustomerID 
         /// </summary> 
         public string CustomerID { get; set; } 

         /// <summary> 
         /// Gets or Sets EmployeeID 
         /// </summary> 
         public int? EmployeeID { get; set; } 

         /// <summary> 
         /// Gets or Sets OrderDate 
         /// </summary> 
         public DateTime? OrderDate { get; set; } 

         /// <summary> 
         /// Gets or Sets RequiredDate 
         /// </summary> 
         public DateTime? RequiredDate { get; set; } 

         /// <summary> 
         /// Gets or Sets ShippedDate 
         /// </summary> 
         public DateTime? ShippedDate { get; set; } 

         /// <summary> 
         /// Gets or Sets ShipVia 
         /// </summary> 
         public int? ShipVia { get; set; } 

         /// <summary> 
         /// Gets or Sets Freight 
         /// </summary> 
         public decimal? Freight { get; set; } 

         /// <summary> 
         /// Gets or Sets FreightTotal
         /// </summary> 
         public decimal FreightTotal { get; set; } 

         /// <summary> 
         /// Gets or Sets ShipName 
         /// </summary> 
         public string ShipName { get; set; } 

         /// <summary> 
         /// Gets or Sets ShipAddress 
         /// </summary> 
         public string ShipAddress { get; set; } 

         /// <summary> 
         /// Gets or Sets ShipCity 
         /// </summary> 
         public string ShipCity { get; set; } 

         /// <summary> 
         /// Gets or Sets ShipRegion 
         /// </summary> 
         public string ShipRegion { get; set; } 

         /// <summary> 
         /// Gets or Sets ShipPostalCode 
         /// </summary> 
         public string ShipPostalCode { get; set; } 

         /// <summary> 
         /// Gets or Sets ShipCountry 
         /// </summary> 
         public string ShipCountry { get; set; } 

     }
}
