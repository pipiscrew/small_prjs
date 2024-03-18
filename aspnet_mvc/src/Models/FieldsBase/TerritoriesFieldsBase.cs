using System;

namespace north.Fields.Base
{
     /// <summary>
     /// Base class for TerritoriesFields.  Do not make changes to this class,
     /// instead, put additional code in the TerritoriesFields class. 
     /// Note: This class is identical to the TerritoriesModelBase without/minus the data annotations.
     /// </summary>
     public class TerritoriesFieldsBase
     {
         /// <summary> 
         /// Gets or Sets TerritoryID 
         /// </summary> 
         public string TerritoryID { get; set; } 

         /// <summary> 
         /// Gets or Sets TerritoryDescription 
         /// </summary> 
         public string TerritoryDescription { get; set; } 

         /// <summary> 
         /// Gets or Sets RegionID 
         /// </summary> 
         public int RegionID { get; set; } 

     }
}
