using System;

namespace north.Fields.Base
{
     /// <summary>
     /// Base class for ShippersFields.  Do not make changes to this class,
     /// instead, put additional code in the ShippersFields class. 
     /// Note: This class is identical to the ShippersModelBase without/minus the data annotations.
     /// </summary>
     public class ShippersFieldsBase
     {
         /// <summary> 
         /// Gets or Sets ShipperID 
         /// </summary> 
         public int ShipperID { get; set; } 

         /// <summary> 
         /// Gets or Sets CompanyName 
         /// </summary> 
         public string CompanyName { get; set; } 

         /// <summary> 
         /// Gets or Sets Phone 
         /// </summary> 
         public string Phone { get; set; } 

     }
}
