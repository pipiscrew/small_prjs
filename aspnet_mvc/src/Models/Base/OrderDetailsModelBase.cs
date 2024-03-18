using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace north.Models.Base
{
     /// <summary>
     /// Base class for OrderDetailsModel.  Do not make changes to this class,
     /// instead, put additional code in the OrderDetailsModel class 
     /// </summary>
     public class OrderDetailsModelBase
     {
         /// <summary> 
         /// Gets or Sets OrderID 
         /// </summary> 
         [Required(ErrorMessage = "{0} is required!")]
         [Display(Name = "Order ID")]
         public int OrderID { get; set; } 

         /// <summary> 
         /// Gets or Sets ProductID 
         /// </summary> 
         [Required(ErrorMessage = "{0} is required!")]
         [Display(Name = "Product ID")]
         public int ProductID { get; set; } 

         /// <summary> 
         /// Gets or Sets UnitPrice 
         /// </summary> 
         [Required(ErrorMessage = "{0} is required!")]
         [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
         [Display(Name = "Unit Price")]
         public decimal UnitPrice { get; set; } 

         /// <summary> 
         /// Gets or Sets UnitPriceTotal
         /// </summary> 
         [Display(Name = "Unit Price Total")]
         public decimal UnitPriceTotal { get; set; } 

         /// <summary> 
         /// Gets or Sets Quantity 
         /// </summary> 
         [Required(ErrorMessage = "{0} is required!")]
         [Range(typeof(Int16), "-32768", "32767", ErrorMessage = "{0} must be an integer!")]
         [Display(Name = "Quantity")]
         public Int16 Quantity { get; set; } 

         /// <summary> 
         /// Gets or Sets Discount 
         /// </summary> 
         [Required(ErrorMessage = "{0} is required!")]
         [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
         [Display(Name = "Discount")]
         public Single Discount { get; set; } 

     }
}
