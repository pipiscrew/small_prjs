using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace north.Models.Base
{
     /// <summary>
     /// Base class for CategoriesModel.  Do not make changes to this class,
     /// instead, put additional code in the CategoriesModel class 
     /// </summary>
     public class CategoriesModelBase
     {
         /// <summary> 
         /// Gets or Sets CategoryID 
         /// </summary> 
         [Display(Name = "Category ID")]
         public int CategoryID { get; set; } 

         /// <summary> 
         /// Gets or Sets CategoryName 
         /// </summary> 
         [Required(ErrorMessage = "{0} is required!")]
         [StringLength(15, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Category Name")]
         public string CategoryName { get; set; } 

         /// <summary> 
         /// Gets or Sets Description 
         /// </summary> 
         [Display(Name = "Description")]
         public string Description { get; set; } 

     }
}
