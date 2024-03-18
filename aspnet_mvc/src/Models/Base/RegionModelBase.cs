using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace north.Models.Base
{
     /// <summary>
     /// Base class for RegionModel.  Do not make changes to this class,
     /// instead, put additional code in the RegionModel class 
     /// </summary>
     public class RegionModelBase
     {
         /// <summary> 
         /// Gets or Sets RegionID 
         /// </summary> 
         [Required(ErrorMessage = "{0} is required!")]
         [Display(Name = "Region ID")]
         public int RegionID { get; set; } 

         /// <summary> 
         /// Gets or Sets RegionDescription 
         /// </summary> 
         [Required(ErrorMessage = "{0} is required!")]
         [StringLength(50, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Region Description")]
         public string RegionDescription { get; set; } 

     }
}
