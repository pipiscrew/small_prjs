using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace north.Models.Base
{
     /// <summary>
     /// Base class for OrdersModel.  Do not make changes to this class,
     /// instead, put additional code in the OrdersModel class 
     /// </summary>
     public class OrdersModelBase
     {
         /// <summary> 
         /// Gets or Sets OrderID 
         /// </summary> 
         [Display(Name = "Order ID")]
         public int OrderID { get; set; } 

         /// <summary> 
         /// Gets or Sets CustomerID 
         /// </summary> 
         [StringLength(5, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Customer ID")]
         public string CustomerID { get; set; } 

         /// <summary> 
         /// Gets or Sets EmployeeID 
         /// </summary> 
         [Display(Name = "Employee ID")]
         public int? EmployeeID { get; set; } 

         /// <summary> 
         /// Gets or Sets OrderDate 
         /// </summary> 
         [DataType(DataType.Date, ErrorMessage = "{0} must be a valid date!")]
         [Display(Name = "Order Date")]
         public DateTime? OrderDate { get; set; } 

         /// <summary> 
         /// Gets or Sets RequiredDate 
         /// </summary> 
         [DataType(DataType.Date, ErrorMessage = "{0} must be a valid date!")]
         [Display(Name = "Required Date")]
         public DateTime? RequiredDate { get; set; } 

         /// <summary> 
         /// Gets or Sets ShippedDate 
         /// </summary> 
         [DataType(DataType.Date, ErrorMessage = "{0} must be a valid date!")]
         [Display(Name = "Shipped Date")]
         public DateTime? ShippedDate { get; set; } 

         /// <summary> 
         /// Gets or Sets ShipVia 
         /// </summary> 
         [Display(Name = "Ship Via")]
         public int? ShipVia { get; set; } 

         /// <summary> 
         /// Gets or Sets Freight 
         /// </summary> 
         [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
         [Display(Name = "Freight")]
         public decimal? Freight { get; set; } 

         /// <summary> 
         /// Gets or Sets FreightTotal
         /// </summary> 
         [Display(Name = "Freight Total")]
         public decimal FreightTotal { get; set; } 

         /// <summary> 
         /// Gets or Sets ShipName 
         /// </summary> 
         [StringLength(40, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Ship Name")]
         public string ShipName { get; set; } 

         /// <summary> 
         /// Gets or Sets ShipAddress 
         /// </summary> 
         [StringLength(60, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Ship Address")]
         public string ShipAddress { get; set; } 

         /// <summary> 
         /// Gets or Sets ShipCity 
         /// </summary> 
         [StringLength(15, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Ship City")]
         public string ShipCity { get; set; } 

         /// <summary> 
         /// Gets or Sets ShipRegion 
         /// </summary> 
         [StringLength(15, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Ship Region")]
         public string ShipRegion { get; set; } 

         /// <summary> 
         /// Gets or Sets ShipPostalCode 
         /// </summary> 
         [StringLength(10, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Ship Postal Code")]
         public string ShipPostalCode { get; set; } 

         /// <summary> 
         /// Gets or Sets ShipCountry 
         /// </summary> 
         [StringLength(15, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Ship Country")]
         public string ShipCountry { get; set; } 

     }
}
