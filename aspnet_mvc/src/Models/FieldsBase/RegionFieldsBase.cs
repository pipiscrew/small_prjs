using System;

namespace north.Fields.Base
{
     /// <summary>
     /// Base class for RegionFields.  Do not make changes to this class,
     /// instead, put additional code in the RegionFields class. 
     /// Note: This class is identical to the RegionModelBase without/minus the data annotations.
     /// </summary>
     public class RegionFieldsBase
     {
         /// <summary> 
         /// Gets or Sets RegionID 
         /// </summary> 
         public int RegionID { get; set; } 

         /// <summary> 
         /// Gets or Sets RegionDescription 
         /// </summary> 
         public string RegionDescription { get; set; } 

     }
}
