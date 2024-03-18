using System;

namespace north.Fields.Base
{
     /// <summary>
     /// Base class for CustomerDemographicsFields.  Do not make changes to this class,
     /// instead, put additional code in the CustomerDemographicsFields class. 
     /// Note: This class is identical to the CustomerDemographicsModelBase without/minus the data annotations.
     /// </summary>
     public class CustomerDemographicsFieldsBase
     {
         /// <summary> 
         /// Gets or Sets CustomerTypeID 
         /// </summary> 
         public string CustomerTypeID { get; set; } 

         /// <summary> 
         /// Gets or Sets CustomerDesc 
         /// </summary> 
         public string CustomerDesc { get; set; } 

     }
}
