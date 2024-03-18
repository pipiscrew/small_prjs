using System;

namespace north.Fields.Base
{
     /// <summary>
     /// Base class for EmployeesFields.  Do not make changes to this class,
     /// instead, put additional code in the EmployeesFields class. 
     /// Note: This class is identical to the EmployeesModelBase without/minus the data annotations.
     /// </summary>
     public class EmployeesFieldsBase
     {
         /// <summary> 
         /// Gets or Sets EmployeeID 
         /// </summary> 
         public int EmployeeID { get; set; } 

         /// <summary> 
         /// Gets or Sets LastName 
         /// </summary> 
         public string LastName { get; set; } 

         /// <summary> 
         /// Gets or Sets FirstName 
         /// </summary> 
         public string FirstName { get; set; } 

         /// <summary> 
         /// Gets or Sets Title 
         /// </summary> 
         public string Title { get; set; } 

         /// <summary> 
         /// Gets or Sets TitleOfCourtesy 
         /// </summary> 
         public string TitleOfCourtesy { get; set; } 

         /// <summary> 
         /// Gets or Sets BirthDate 
         /// </summary> 
         public DateTime? BirthDate { get; set; } 

         /// <summary> 
         /// Gets or Sets HireDate 
         /// </summary> 
         public DateTime? HireDate { get; set; } 

         /// <summary> 
         /// Gets or Sets Address 
         /// </summary> 
         public string Address { get; set; } 

         /// <summary> 
         /// Gets or Sets City 
         /// </summary> 
         public string City { get; set; } 

         /// <summary> 
         /// Gets or Sets Region1 
         /// </summary> 
         public string Region1 { get; set; } 

         /// <summary> 
         /// Gets or Sets PostalCode 
         /// </summary> 
         public string PostalCode { get; set; } 

         /// <summary> 
         /// Gets or Sets Country 
         /// </summary> 
         public string Country { get; set; } 

         /// <summary> 
         /// Gets or Sets HomePhone 
         /// </summary> 
         public string HomePhone { get; set; } 

         /// <summary> 
         /// Gets or Sets Extension 
         /// </summary> 
         public string Extension { get; set; } 

         /// <summary> 
         /// Gets or Sets Notes 
         /// </summary> 
         public string Notes { get; set; } 

         /// <summary> 
         /// Gets or Sets ReportsTo 
         /// </summary> 
         public int? ReportsTo { get; set; } 

         /// <summary> 
         /// Gets or Sets PhotoPath 
         /// </summary> 
         public string PhotoPath { get; set; } 

     }
}
