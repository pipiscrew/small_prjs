using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace north.Models.Base
{
     /// <summary>
     /// Base class for ShippersModel.  Do not make changes to this class,
     /// instead, put additional code in the ShippersModel class 
     /// </summary>
     public class ShippersModelBase
     {
         /// <summary> 
         /// Gets or Sets ShipperID 
         /// </summary> 
         [Display(Name = "Shipper ID")]
         public int ShipperID { get; set; } 

         /// <summary> 
         /// Gets or Sets CompanyName 
         /// </summary> 
         [Required(ErrorMessage = "{0} is required!")]
         [StringLength(40, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Company Name")]
         public string CompanyName { get; set; } 

         /// <summary> 
         /// Gets or Sets Phone 
         /// </summary> 
         [StringLength(24, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Phone")]
         public string Phone { get; set; } 

     }
}
