using System;

namespace north.Fields.Base
{
     /// <summary>
     /// Base class for SuppliersFields.  Do not make changes to this class,
     /// instead, put additional code in the SuppliersFields class. 
     /// Note: This class is identical to the SuppliersModelBase without/minus the data annotations.
     /// </summary>
     public class SuppliersFieldsBase
     {
         /// <summary> 
         /// Gets or Sets SupplierID 
         /// </summary> 
         public int SupplierID { get; set; } 

         /// <summary> 
         /// Gets or Sets CompanyName 
         /// </summary> 
         public string CompanyName { get; set; } 

         /// <summary> 
         /// Gets or Sets ContactName 
         /// </summary> 
         public string ContactName { get; set; } 

         /// <summary> 
         /// Gets or Sets ContactTitle 
         /// </summary> 
         public string ContactTitle { get; set; } 

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
         /// Gets or Sets Phone 
         /// </summary> 
         public string Phone { get; set; } 

         /// <summary> 
         /// Gets or Sets Fax 
         /// </summary> 
         public string Fax { get; set; } 

         /// <summary> 
         /// Gets or Sets HomePage 
         /// </summary> 
         public string HomePage { get; set; } 

     }
}
