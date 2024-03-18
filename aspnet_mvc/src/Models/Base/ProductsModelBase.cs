using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace north.Models.Base
{
     /// <summary>
     /// Base class for ProductsModel.  Do not make changes to this class,
     /// instead, put additional code in the ProductsModel class 
     /// </summary>
     public class ProductsModelBase
     {
         /// <summary> 
         /// Gets or Sets ProductID 
         /// </summary> 
         [Display(Name = "Product ID")]
         public int ProductID { get; set; } 

         /// <summary> 
         /// Gets or Sets ProductName 
         /// </summary> 
         [Required(ErrorMessage = "{0} is required!")]
         [StringLength(40, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Product Name")]
         public string ProductName { get; set; } 

         /// <summary> 
         /// Gets or Sets SupplierID 
         /// </summary> 
         [Display(Name = "Supplier ID")]
         public int? SupplierID { get; set; } 

         /// <summary> 
         /// Gets or Sets CategoryID 
         /// </summary> 
         [Display(Name = "Category ID")]
         public int? CategoryID { get; set; } 

         /// <summary> 
         /// Gets or Sets QuantityPerUnit 
         /// </summary> 
         [StringLength(20, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Quantity Per Unit")]
         public string QuantityPerUnit { get; set; } 

         /// <summary> 
         /// Gets or Sets UnitPrice 
         /// </summary> 
         [RegularExpression(@"[-+]?[0-9]*\.?[0-9]?[0-9]", ErrorMessage = "{0} must be a valid decimal!")]
         [Display(Name = "Unit Price")]
         public decimal? UnitPrice { get; set; } 

         /// <summary> 
         /// Gets or Sets UnitPriceTotal
         /// </summary> 
         [Display(Name = "Unit Price Total")]
         public decimal UnitPriceTotal { get; set; } 

         /// <summary> 
         /// Gets or Sets UnitsInStock 
         /// </summary> 
         [Range(typeof(Int16), "-32768", "32767", ErrorMessage = "{0} must be an integer!")]
         [Display(Name = "Units In Stock")]
         public Int16? UnitsInStock { get; set; } 

         /// <summary> 
         /// Gets or Sets UnitsOnOrder 
         /// </summary> 
         [Range(typeof(Int16), "-32768", "32767", ErrorMessage = "{0} must be an integer!")]
         [Display(Name = "Units On Order")]
         public Int16? UnitsOnOrder { get; set; } 

         /// <summary> 
         /// Gets or Sets ReorderLevel 
         /// </summary> 
         [Range(typeof(Int16), "-32768", "32767", ErrorMessage = "{0} must be an integer!")]
         [Display(Name = "Reorder Level")]
         public Int16? ReorderLevel { get; set; } 

         /// <summary> 
         /// Gets or Sets Discontinued 
         /// </summary> 
         [Required(ErrorMessage = "{0} is required!")]
         [Display(Name = "Discontinued")]
         public bool Discontinued { get; set; } 

     }
}
