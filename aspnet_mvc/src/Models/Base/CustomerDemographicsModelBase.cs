using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace north.Models.Base
{
     /// <summary>
     /// Base class for CustomerDemographicsModel.  Do not make changes to this class,
     /// instead, put additional code in the CustomerDemographicsModel class 
     /// </summary>
     public class CustomerDemographicsModelBase
     {
         /// <summary> 
         /// Gets or Sets CustomerTypeID 
         /// </summary> 
         [Required(ErrorMessage = "{0} is required!")]
         [StringLength(10, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Customer Type ID")]
         public string CustomerTypeID { get; set; } 

         /// <summary> 
         /// Gets or Sets CustomerDesc 
         /// </summary> 
         [Display(Name = "Customer Desc")]
         public string CustomerDesc { get; set; } 

     }
}
