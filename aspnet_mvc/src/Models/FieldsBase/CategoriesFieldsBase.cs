using System;

namespace north.Fields.Base
{
     /// <summary>
     /// Base class for CategoriesFields.  Do not make changes to this class,
     /// instead, put additional code in the CategoriesFields class. 
     /// Note: This class is identical to the CategoriesModelBase without/minus the data annotations.
     /// </summary>
     public class CategoriesFieldsBase
     {
         /// <summary> 
         /// Gets or Sets CategoryID 
         /// </summary> 
         public int CategoryID { get; set; } 

         /// <summary> 
         /// Gets or Sets CategoryName 
         /// </summary> 
         public string CategoryName { get; set; } 

         /// <summary> 
         /// Gets or Sets Description 
         /// </summary> 
         public string Description { get; set; } 

     }
}
