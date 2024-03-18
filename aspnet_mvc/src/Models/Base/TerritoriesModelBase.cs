using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace north.Models.Base
{
     /// <summary>
     /// Base class for TerritoriesModel.  Do not make changes to this class,
     /// instead, put additional code in the TerritoriesModel class 
     /// </summary>
     public class TerritoriesModelBase
     {
         /// <summary> 
         /// Gets or Sets TerritoryID 
         /// </summary> 
         [Required(ErrorMessage = "{0} is required!")]
         [StringLength(20, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Territory ID")]
         public string TerritoryID { get; set; } 

         /// <summary> 
         /// Gets or Sets TerritoryDescription 
         /// </summary> 
         [Required(ErrorMessage = "{0} is required!")]
         [StringLength(50, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Territory Description")]
         public string TerritoryDescription { get; set; } 

         /// <summary> 
         /// Gets or Sets RegionID 
         /// </summary> 
         [Required(ErrorMessage = "{0} is required!")]
         [Display(Name = "Region ID")]
         public int RegionID { get; set; } 

     }
}
