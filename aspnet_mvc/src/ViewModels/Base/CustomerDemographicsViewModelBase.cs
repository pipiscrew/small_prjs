using System;
using north.Models;
using north.Domain;
using north.BusinessObject;

namespace north.ViewModels.Base
{ 
     /// <summary>
     /// Base class for CustomerDemographicsViewModel.  Do not make changes to this class,
     /// instead, put additional code in the CustomerDemographicsViewModel class 
     /// </summary>
     public class CustomerDemographicsViewModelBase
     { 
         public north.Models.CustomerDemographicsModel CustomerDemographicsModel { get; set; }
         public CrudOperation Operation { get; set; }
         public string ViewControllerName { get; set; }
         public string ViewActionName { get; set; }
         public string ViewReturnUrl { get; set; }
     } 
} 
