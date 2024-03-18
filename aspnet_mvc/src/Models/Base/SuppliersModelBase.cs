using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace north.Models.Base
{
     /// <summary>
     /// Base class for SuppliersModel.  Do not make changes to this class,
     /// instead, put additional code in the SuppliersModel class 
     /// </summary>
     public class SuppliersModelBase
     {
         /// <summary> 
         /// Gets or Sets SupplierID 
         /// </summary> 
         [Display(Name = "Supplier ID")]
         public int SupplierID { get; set; } 

         /// <summary> 
         /// Gets or Sets CompanyName 
         /// </summary> 
         [Required(ErrorMessage = "{0} is required!")]
         [StringLength(40, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Company Name")]
         public string CompanyName { get; set; } 

         /// <summary> 
         /// Gets or Sets ContactName 
         /// </summary> 
         [StringLength(30, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Contact Name")]
         public string ContactName { get; set; } 

         /// <summary> 
         /// Gets or Sets ContactTitle 
         /// </summary> 
         [StringLength(30, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Contact Title")]
         public string ContactTitle { get; set; } 

         /// <summary> 
         /// Gets or Sets Address 
         /// </summary> 
         [StringLength(60, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Address")]
         public string Address { get; set; } 

         /// <summary> 
         /// Gets or Sets City 
         /// </summary> 
         [StringLength(15, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "City")]
         public string City { get; set; } 

         /// <summary> 
         /// Gets or Sets Region1 
         /// </summary> 
         [StringLength(15, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Region1")]
         public string Region1 { get; set; } 

         /// <summary> 
         /// Gets or Sets PostalCode 
         /// </summary> 
         [StringLength(10, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Postal Code")]
         public string PostalCode { get; set; } 

         /// <summary> 
         /// Gets or Sets Country 
         /// </summary> 
         [StringLength(15, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Country")]
         public string Country { get; set; } 

         /// <summary> 
         /// Gets or Sets Phone 
         /// </summary> 
         [StringLength(24, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Phone")]
         public string Phone { get; set; } 

         /// <summary> 
         /// Gets or Sets Fax 
         /// </summary> 
         [StringLength(24, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Fax")]
         public string Fax { get; set; } 

         /// <summary> 
         /// Gets or Sets HomePage 
         /// </summary> 
         [Display(Name = "Home Page")]
         public string HomePage { get; set; } 

     }
}
