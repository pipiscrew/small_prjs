using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace north.Models.Base
{
     /// <summary>
     /// Base class for EmployeesModel.  Do not make changes to this class,
     /// instead, put additional code in the EmployeesModel class 
     /// </summary>
     public class EmployeesModelBase
     {
         /// <summary> 
         /// Gets or Sets EmployeeID 
         /// </summary> 
         [Display(Name = "Employee ID")]
         public int EmployeeID { get; set; } 

         /// <summary> 
         /// Gets or Sets LastName 
         /// </summary> 
         [Required(ErrorMessage = "{0} is required!")]
         [StringLength(20, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Last Name")]
         public string LastName { get; set; } 

         /// <summary> 
         /// Gets or Sets FirstName 
         /// </summary> 
         [Required(ErrorMessage = "{0} is required!")]
         [StringLength(10, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "First Name")]
         public string FirstName { get; set; } 

         /// <summary> 
         /// Gets or Sets Title 
         /// </summary> 
         [StringLength(30, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Title")]
         public string Title { get; set; } 

         /// <summary> 
         /// Gets or Sets TitleOfCourtesy 
         /// </summary> 
         [StringLength(25, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Title Of Courtesy")]
         public string TitleOfCourtesy { get; set; } 

         /// <summary> 
         /// Gets or Sets BirthDate 
         /// </summary> 
         [DataType(DataType.Date, ErrorMessage = "{0} must be a valid date!")]
         [Display(Name = "Birth Date")]
         public DateTime? BirthDate { get; set; } 

         /// <summary> 
         /// Gets or Sets HireDate 
         /// </summary> 
         [DataType(DataType.Date, ErrorMessage = "{0} must be a valid date!")]
         [Display(Name = "Hire Date")]
         public DateTime? HireDate { get; set; } 

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
         /// Gets or Sets HomePhone 
         /// </summary> 
         [StringLength(24, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Home Phone")]
         public string HomePhone { get; set; } 

         /// <summary> 
         /// Gets or Sets Extension 
         /// </summary> 
         [StringLength(4, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Extension")]
         public string Extension { get; set; } 

         /// <summary> 
         /// Gets or Sets Notes 
         /// </summary> 
         [Display(Name = "Notes")]
         public string Notes { get; set; } 

         /// <summary> 
         /// Gets or Sets ReportsTo 
         /// </summary> 
         [Display(Name = "Reports To")]
         public int? ReportsTo { get; set; } 

         /// <summary> 
         /// Gets or Sets PhotoPath 
         /// </summary> 
         [StringLength(255, ErrorMessage = "{0} must be a maximum of {1} characters long!")]
         [Display(Name = "Photo Path")]
         public string PhotoPath { get; set; } 

     }
}
